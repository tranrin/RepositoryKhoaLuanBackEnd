using Application.Core;
using Dapper;
using Domain;
using Domain.Request;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace Application.CongThuc
{
    public class CongThucAdd
    {
        public class Query : IRequest<Result<Table_CongThuc>>
        {
            public CongThucRequest data { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<Table_CongThuc>>
        {
   
            private readonly IConfiguration _configuration;

            public Handler(IConfiguration configuration)
            {
      ;
                _configuration = configuration;
            }
            public async Task<Result<Table_CongThuc>> Handle(Query request, CancellationToken cancellationToken)
            {
                //_context.Activities.Add(request.Activity);
                //await _context.SaveChangesAsync();
                //return Unit.Value;

                using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    await connection.OpenAsync();
                    var transaction = await connection.BeginTransactionAsync();

                    try
                    {
                        string spNameCongThuc = "USP_RecipeInformation_Step1_Insert";
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@TenCongThuc", request.data.ThongTinChung.TenCongThuc);
                        parameters.Add("@MoTa", request.data.ThongTinChung.MoTa);
                        parameters.Add("@IDTheLoai", request.data.ThongTinChung.IDCategory);
                        parameters.Add("@ThoiGianNau", request.data.ThongTinChung.ThoiGianNau);
                        parameters.Add("@ThoiGianChuanBi", request.data.ThongTinChung.ThoiGianChuanBi);
                        parameters.Add("@AnhKemTheo", request.data.ThongTinChung.AnhKemTheo);
                        parameters.Add("@IDUser", request.data.ThongTinChung.IDUser);

                        var InsertResult = await connection.QueryFirstOrDefaultAsync<Table_CongThuc>(spNameCongThuc, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);

                        if (InsertResult == null)
                        {
                            throw new Exception("Thêm mới không thành công");
                        }

                        string spname2 = "USP_RecipeInformation_Step2_Insert";
                        for (int i = 0; i < request.data.NguyenLieu.Count; i++)
                        {
                            DynamicParameters parameters2 = new DynamicParameters();

                            parameters2.Add("@TenNguyenLieu", request.data.NguyenLieu[i].TenNguyenLieu);
                            parameters2.Add("@IDCongThuc", InsertResult.ID);
                            var insertId = await connection.ExecuteScalarAsync<int>(spname2, parameters2, transaction, commandType: System.Data.CommandType.StoredProcedure);
                            if (insertId == 0)
                            {
                                throw new Exception("Thêm mới không thành công");
                            }

                        }
                        string spname3 = "USP_RecipeInformation_Step3_Insert";
                        for (int i = 0; i < request.data.BuocNau.Count; i++)
                        {
                            DynamicParameters parameters3 = new DynamicParameters();
                            parameters3.Add("@IDCongThuc", InsertResult.ID);
                            parameters3.Add("@MoTa", request.data.BuocNau[i].MoTa);
                            parameters3.Add("@ThuTu", i + 1);
                            var insertId = await connection.ExecuteScalarAsync<int>(spname3, parameters3, transaction, commandType: System.Data.CommandType.StoredProcedure);
                            if (insertId == 0)
                            {
                                throw new Exception("Thêm mới không thành công");
                            }

                        }

                        transaction.Commit();
                        return Result<Table_CongThuc>.Success(InsertResult);
                    }

                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return Result<Table_CongThuc>.Failure(ex.Message);
                    }


                }


            }
        }
    }
}

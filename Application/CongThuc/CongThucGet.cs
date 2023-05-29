using Application.Core;
using Dapper;
using Domain;
using Domain.Response;
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
    public class CongThucGet
    {
        public class Query : IRequest<Result<CongThucGetResponse>>
        {// su li tham so dau vao

            public int CongThucID { get; set; }
            public string IDUser { get; set; }
        
        }

        public class Handler : IRequestHandler<Query, Result<CongThucGetResponse>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<CongThucGetResponse>> Handle(Query request, CancellationToken cancellationToken)
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
                        string spNameCongThuc = "[USP_CongThuc_ByIDCongThuc_Get]";
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@CongThucID", request.CongThucID);
                        var ResultCongThuc = await connection.QueryFirstOrDefaultAsync<CongThucDetailResponse>(spNameCongThuc, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);
                           if (ResultCongThuc != null)
                        {
                                       
                                if (Enum.IsDefined(typeof(TheLoai), ResultCongThuc.IDCategory))
                                {
                                ResultCongThuc.NameCategory = StringEnum.GetStringValue(ResultCongThuc.IDCategory);
                                }
                                if (Enum.IsDefined(typeof(DoKho), ResultCongThuc.DoKho))
                                {
                                ResultCongThuc.NameDoKho = StringEnum.GetStringValue(ResultCongThuc.DoKho);
                                }
                        }
                        string spNameDanhGiaByUser = "[USP_Get_DanhGiaByUserCongThuc]";
                        DynamicParameters parametersDanhGiaGet = new DynamicParameters();
                        parametersDanhGiaGet.Add("@UserId", request.IDUser);
                        parametersDanhGiaGet.Add("@CongThucId", request.CongThucID);
                        var ResultDanhGia = await connection.QueryFirstOrDefaultAsync<Table_DanhGia>(spNameDanhGiaByUser, parametersDanhGiaGet, transaction, commandType: System.Data.CommandType.StoredProcedure);
                        string spNameNguyenLieu = "[USP_NguyenLieu_ByIDCongThuc_Get]";
                        DynamicParameters parametersNguyenLieu = new DynamicParameters();
                        parameters.Add("@CongThucID", request.CongThucID);
                        var ResultNguyenLieu = await connection.QueryAsync<Table_NguyenLieu>(spNameNguyenLieu, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);
                        string spNameBuocNau = "[USP_BuocNau_ByIDCongThuc_Get]";
                        DynamicParameters parametersBuocNau = new DynamicParameters();
                        parameters.Add("@CongThucID", request.CongThucID);
                        var ResultBuocNau = await connection.QueryAsync<Table_BuocNau>(spNameBuocNau, parameters, transaction, commandType: System.Data.CommandType.StoredProcedure);
                        transaction.Commit();
                        return Result<CongThucGetResponse>.Success(new CongThucGetResponse { BuocNau = ResultBuocNau.ToList(), NguyenLieu = ResultNguyenLieu.ToList(), ThongTinChung = ResultCongThuc, DanhGiaByUserId = ResultDanhGia });
                    }

                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return Result<CongThucGetResponse>.Failure(ex.Message);
                    }


                }


            }
        }
    }
}

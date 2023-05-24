using Application.Core;
using Dapper;
using Domain;
using Domain.Response;
using MediatR;
using Microsoft.Extensions.Configuration;

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.DanhGia
{
    public class DanhGiaGet
    {
        public class Query : IRequest<Result<DanhGiaResponse>>
        {

            public int CongThucID { get; set; }
            public string UserId { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<DanhGiaResponse>>
        {
            private readonly IConfiguration _configuration;
  

            public Handler( IConfiguration configuration)
            {
              
                _configuration = configuration;

            }

            public async Task<Result<DanhGiaResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@CongThucID", request.CongThucID);
                    string spName = "USP_GetDanhGiaByCongThucId";
                    using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                    {
                        connection.Open();
                        //var result = await connection.QueryAsync<Place>(spName);
                        var result = await connection.QueryFirstOrDefaultAsync<List<Table_DanhGia>>(new CommandDefinition(spName, parameters, commandType: System.Data.CommandType.StoredProcedure));
                        if (result != null)
                        {
                            var SaoTrungBinh = 0;
                            for (int i = 0; i < result.Count; i++)
                            {
                                SaoTrungBinh = result[i].Sao + SaoTrungBinh;
                            }
                            SaoTrungBinh = SaoTrungBinh / result.Count;
                            var Sao = 0;
                            var DanhGia = 0;
                            for (int i = 0; i < result.Count; i++)
                            {
                                if (result[i].UserID == request.UserId)
                                {
                                    Sao = result[i].Sao;
                                    DanhGia = result[i].ID;
                                    break;
                                }
                            }
                            return Result<DanhGiaResponse>.Success(new DanhGiaResponse { Sao = Sao, SaoTrungBinh = SaoTrungBinh, UserId = request.UserId, ID = DanhGia, SoLuong = result.Count });
                        }

                        return Result<DanhGiaResponse>.Success(new DanhGiaResponse());
                    }
                }
                catch (Exception e)
                {
                    return Result<DanhGiaResponse>.Failure(e.Message);
                }


            }
        }
    }
}

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

namespace Application.DanhGia
{
    public class DanhGiaAdd
    {
        public class Command : IRequest<Result<int>>
        {
            public DanhGiaRequest Request { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<int>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<int>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@Sao", request.Request.Sao);
                    dynamicParameters.Add("@CongThucID", request.Request.CongThucID);
                    dynamicParameters.Add("@UserID", request.Request.UserID);
                    string spName = "[USP_DanhGia_ADD]";

                    using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                    {
                        connection.Open();
                        var result = await connection.QueryFirstOrDefaultAsync<int>(new CommandDefinition(spName, parameters: dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));

                        return Result<int>.Success(result);
                    }
                }
                catch(Exception e)
                {
                    return Result<int>.Failure(e.Message);
                }

            }
        }
    }
}

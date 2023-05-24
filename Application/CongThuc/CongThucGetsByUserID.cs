using Application.Core;
using Dapper;
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
    public class CongThucGetsByUserID
    {
        public class Query : IRequest<Result<List<CongThucResponse>>>
        {// su li tham so dau vao

            public string IDUser { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<List<CongThucResponse>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<CongThucResponse>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@UserID", request.IDUser);
                    string spName = "USP_CongThucGetsByUserID";
                    using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                    {
                        connection.Open();
                        var result = await connection.QueryAsync<CongThucResponse>(new CommandDefinition(spName, parameters, commandType: System.Data.CommandType.StoredProcedure));
                        
                        return Result<List<CongThucResponse>>.Success(result.ToList());

                    }
                }
                catch (Exception e)
                {
                    return Result<List<CongThucResponse>>.Failure(e.Message);
                }
            }
        }
    }
}

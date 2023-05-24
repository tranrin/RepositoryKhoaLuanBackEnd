using Application.Core;
using Dapper;
using Domain;
using Domain.Request;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.LuotThich
{
    public class LuotThichAdd
    {
        public class Command : IRequest<Result<string>>
        {
            public CollectionRequest Request { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<string>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@CongThucID", request.Request.CongThucID);
                    dynamicParameters.Add("@NguoiDungID", request.Request.NguoiDungID);
                    string spName = "[USP_AddCollection]";

                    using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                    {
                        connection.Open();
                        var result = await connection.QueryFirstOrDefaultAsync<string>(new CommandDefinition(spName, parameters: dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));

                        return Result<string>.Success(result);
                    }
                }
                catch(Exception e)
                {
                    return Result<string>.Failure(e.Message);
                }

            }
        }
    }
}

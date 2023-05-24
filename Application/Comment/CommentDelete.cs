using Application.Core;
using Dapper;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Application.Comment
{
    public class CommentDelete
    {
        public class Command : IRequest<Result<int>>
        {
            public int Request { get; set; }
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
                    dynamicParameters.Add("@ID", request.Request);
                    string spName = "[USP_Comment_Delete]";

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

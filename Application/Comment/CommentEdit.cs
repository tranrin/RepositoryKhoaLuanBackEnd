using Application.Core;
using Dapper;
using Domain;
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
    public class CommentEdit
    {
        public class Command : IRequest<Result<int>>
        {
            public Table_Comment Request { get; set; }
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
                    dynamicParameters.Add("@Content", request.Request.Content);
                    dynamicParameters.Add("@ID", request.Request.ID);
                    string spName = "USP_Comment_Edit";

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

using Application.Core;
using Dapper;
using Domain;
using Domain.Request;
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

namespace Application.Comment
{
    public class CommentAdd
    {
        public class Query : IRequest<Result<CommentResponse>>
        {
            public CommentRequest Request { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<CommentResponse>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<CommentResponse>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {

                    DynamicParameters dynamicParameters = new DynamicParameters();
                    dynamicParameters.Add("@Content", request.Request.Content);
                    dynamicParameters.Add("@IDCongThuc", request.Request.CongThucId);
                    dynamicParameters.Add("@ParentID", request.Request.ParentId);
                    dynamicParameters.Add("@UserId", request.Request.UserId);
                    string spName = "[USP_Comment_ADD]";

                    using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                    {
                        connection.Open();
                        var result = await connection.QueryFirstOrDefaultAsync<CommentResponse>(new CommandDefinition(spName, parameters: dynamicParameters, commandType: System.Data.CommandType.StoredProcedure));

                        return Result<CommentResponse>.Success(result);
                    }
                }
                catch (Exception e)
                {
                    return Result<CommentResponse>.Failure(e.Message);
                }
              

            }
        }
    }
}

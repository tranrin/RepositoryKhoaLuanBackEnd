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
    public class CommentGetsByIDCongThuc
    {
        public class Query : IRequest<Result<List<Table_Comment>>>
        {

            public int CongThucID { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<List<Table_Comment>>>
        {
            private readonly IConfiguration _configuration;
 

            public Handler(IConfiguration configuration)
            {
               
                _configuration = configuration;

            }

            public async Task<Result<List<Table_Comment>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {

                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@CongThucID", request.CongThucID);
                    string spName = "USP_GetsCommentByCongThucId";
                    using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                    {
                        connection.Open();
                        //var result = await connection.QueryAsync<Place>(spName);
                        var result = await connection.QueryAsync<Table_Comment>(new CommandDefinition(spName, parameters, commandType: System.Data.CommandType.StoredProcedure));
                        return Result<List<Table_Comment>>.Success(result.ToList());
                    }
                }catch(Exception e)
                {
                    return Result<List<Table_Comment>>.Failure(e.Message);
                }
          


            }
        }
    }
}

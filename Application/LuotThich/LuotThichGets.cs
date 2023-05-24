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
using static Dapper.SqlMapper;
namespace Application.LuotThich
{
    public class LuotThichGets
    {
        public class Query : IRequest<Result<List<LuotSaveResponse>>>
        {

            public string userID { get; set; }
        }
        public class Handler : IRequestHandler<Query, Result<List<LuotSaveResponse>>>
        {
            private readonly IConfiguration _configuration;


            public Handler(IConfiguration configuration)
            {
              
                _configuration = configuration;

            }

            public async Task<Result<List<LuotSaveResponse>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    DynamicParameters parameters = new DynamicParameters();

                    parameters.Add("@UserId", request.userID);
                    string spName = "USP_GetsCollectionByUserId";
                    using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                    {
                        connection.Open();
                        //var result = await connection.QueryAsync<Place>(spName);
                        var result = await connection.QueryAsync<LuotSaveResponse>(new CommandDefinition(spName, parameters, commandType: System.Data.CommandType.StoredProcedure));
                        return Result<List<LuotSaveResponse>>.Success(result.ToList());
                    }
                }
                catch(Exception e)
                {
                    return Result<List<LuotSaveResponse>>.Failure(e.Message);

                }


            }
        }
    }
}

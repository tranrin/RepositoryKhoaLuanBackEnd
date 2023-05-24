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

namespace Application.User
{
    public class UserGet
    {
        public class Query : IRequest<Result<Table_User>>
        {
            public string IdUser { get; set; }
        }

        public class Handler : IRequestHandler<Query, Result<Table_User>>
        {
           
            private readonly IConfiguration _configuration;

            public Handler( IConfiguration configuration)
            {
               
                _configuration = configuration;
            }
            public async Task<Result<Table_User>> Handle(Query request, CancellationToken cancellationToken)
            {
                string spName = "[USP_UserGet]";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@IdUser", request.IdUser);
   
                try
                {
                    using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                    {
                        connection.Open();
                        var result = await connection.QueryFirstAsync<Table_User>(spName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                        return Result<Table_User>.Success(result);
                    }

                }catch(Exception e)
                {
                    return Result<Table_User>.Failure(e.Message);
                }
             
            }
        }
    }
}

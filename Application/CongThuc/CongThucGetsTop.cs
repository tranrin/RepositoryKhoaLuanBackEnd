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

namespace Application.CongThuc
{
    public class CongThucGetsTop
    {
        public class Query : IRequest<Result<List<Table_CongThuc>>>
        {// su li tham so dau vao

        
        }

        public class Handler : IRequestHandler<Query, Result<List<Table_CongThuc>>>
        {
            private readonly IConfiguration _configuration;
            public Handler(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task<Result<List<Table_CongThuc>>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                   
  
                    string spName = "USP_RecipeTop_Gets";
                    using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                    {
                        connection.Open();
                        var result = await connection.QueryAsync<Table_CongThuc>(new CommandDefinition(spName, commandType: System.Data.CommandType.StoredProcedure));
                        return Result<List<Table_CongThuc>>.Success(result.ToList());

                    }
                }
                catch (Exception ex)
                {
                    return Result<List<Table_CongThuc>>.Failure(ex.Message);
                }


            }
        }
    }
}

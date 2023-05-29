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
    public class UserAdd
    {
        public class Command : IRequest<Result<Table_User>>
        {
            public Table_User infor { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<Table_User>>
        {
      
            private readonly IConfiguration _configuration;

            public Handler( IConfiguration configuration)
            {

                _configuration = configuration;
            }
            public async Task<Result<Table_User>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    string spName = "[USP_LoginWithEmail]";
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@Email", request.infor.Email);
                    parameters.Add("@Name", request.infor.ScreenName);
                    parameters.Add("@HinhAnh", request.infor.Image);
                    using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                    {
                        connection.Open();
                        var result = await connection.QueryFirstAsync<Table_User>(spName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                        return Result<Table_User>.Success(result);
                    }
                }
                catch(Exception e)
                {
                    return Result<Table_User>.Failure(e.Message);
                }
            }
        }
    }
}

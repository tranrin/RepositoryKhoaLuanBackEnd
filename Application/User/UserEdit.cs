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

namespace Application.User
{
    public class UserEdit
    {
        public class Command : IRequest<Result<string>>
        {
            public UserEditRequest infor { get; set; }
        }

        public class Handler : IRequestHandler<Command, Result<string>>
        {
           
            private readonly IConfiguration _configuration;

            public Handler( IConfiguration configuration)
            {
    
                _configuration = configuration;
            }
            public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    string spName = "[USP_UpdateProfileUser]";
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@ScreenName", request.infor.ScreenName);
                    parameters.Add("@Image", request.infor.Image);
                    parameters.Add("@Bio", request.infor.Bio);
                    parameters.Add("@Location", request.infor.Location);
                    parameters.Add("@Facebook", request.infor.FacebookURL);
                    parameters.Add("@Twitter", request.infor.TwitterURL);
                    parameters.Add("@Instagram", request.infor.InstagramURL);
                    parameters.Add("@Website", request.infor.WebsiteURL);
                    parameters.Add("@UserId", request.infor.Email);
                    using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                    {
                        connection.Open();
                        var result = await connection.QueryFirstAsync<string>(spName, parameters, commandType: System.Data.CommandType.StoredProcedure);
                        return Result<string>.Success(result);
                    }
                }catch(Exception e)
                {
                    return Result<string>.Failure(e.Message);
                }
            }
        }
    }
}

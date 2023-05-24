using Application.Core;
using Dapper;
using Domain;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.CongThuc
{
    public class CongThucGetsByCategory
    {

        public class Query : IRequest<Result<List<Table_CongThuc>>>
        {// su li tham so dau vao

            public int data { get; set; }
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
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@CategoryId", request.data);
                    string spName = "USP_CongThucGetsByCategory";
                    using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                    {
                        connection.Open();
                        var result = await connection.QueryAsync<Table_CongThuc>(new CommandDefinition(spName, parameters, commandType: System.Data.CommandType.StoredProcedure));
                        if (result.ToList().Any())
                        {
                            var list = result.ToList();
                            foreach (Table_CongThuc item in list)
                            {
                                if (Enum.IsDefined(typeof(TheLoai), item.IDCategory))
                                {
                                    item.NameCategory = StringEnum.GetStringValue(item.IDCategory);
                                }
                                if (Enum.IsDefined(typeof(DoKho), item.DoKho))
                                {
                                    item.NameDoKho = StringEnum.GetStringValue(item.DoKho);
                                }
                            }
                            return Result<List<Table_CongThuc>>.Success(list);

                        }
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

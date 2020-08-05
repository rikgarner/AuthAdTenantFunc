using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AuthAdTenantFunc.CompanyListFetch;
using Dapper;
using MediatR;

namespace AuthAdTenantFunc
{
    public class CompanyListFetchQuery : RequestBase, IRequest<IEnumerable<CompanyModel>>
    {
        public int PageSize { get; set; } = 20;
        public int PageNumber { get; set; }
       
        protected override IEnumerable<string> Props()
        {
            yield return $"PageSize:{PageSize}";
            yield return $"PageNumber:{PageNumber}";
        }
    }

    public class CompanyListFetchQueryHandler : IRequestHandler<CompanyListFetchQuery, IEnumerable<CompanyModel>>
    {
        public async Task<IEnumerable<CompanyModel>> Handle(CompanyListFetchQuery request, CancellationToken cancellationToken)
        {

            SqlConnection dbConnection = new SqlConnection
            {
                ConnectionString = Environment.GetEnvironmentVariable("DatabaseConnectionString")
            };

            await using (dbConnection)
            {
                await dbConnection.OpenAsync();
                var results = await dbConnection.QueryAsync<CompanyModel>("SELECT * FROM Core.Company order by CompanyId ");
                await dbConnection.CloseAsync();
                return request.PageNumber == 0 ? results : results.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize);
            }
            
        }
    }
}

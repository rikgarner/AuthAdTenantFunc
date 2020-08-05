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
    public class CompanyListFetchQuery : IRequest<IEnumerable<CompanyModel>>, ILoggableData
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string Dump()
        {
            var b = Props().Aggregate("", (buffer, item) => string.Concat(buffer, $"{item};"));
            return b;
        }

        public string[] DumpProps()
        {
            return new []{$"PageSize:{PageSize}"};
        }

        internal IEnumerable<string> Props()
        {
            yield return $"PageSize:{PageSize}";
            yield return $"PageNumber:{PageNumber}";
        }
    }

    public class CompanyListFetchQueryHandler : IRequestHandler<CompanyListFetchQuery, IEnumerable<CompanyModel>>
    {
        public Task<IEnumerable<CompanyModel>> Handle(CompanyListFetchQuery request,
            CancellationToken cancellationToken)
        {
           return Handle2();
        }

        private async Task<IEnumerable<CompanyModel>> Handle2()
        {
            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = Environment.GetEnvironmentVariable("DatabaseConnectionString");

            await using (dbConnection)
            {
                await dbConnection.OpenAsync();
                var results = await dbConnection.QueryAsync<CompanyModel>("SELECT * FROM Core.Company ");
                await dbConnection.CloseAsync();
                return results.Take(6);
            }
            
        }

        private Task<IEnumerable<CompanyModel>> Handle1(){
        var user = new CompanyModel() { CompanyName = "Morrisons" };

            var result = new[] { user }
                .AsEnumerable();

            return Task.FromResult(result);
        }
    }
}

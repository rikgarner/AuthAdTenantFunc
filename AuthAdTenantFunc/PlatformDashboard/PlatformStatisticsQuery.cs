using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AuthAdTenantFunc.CompanyListFetch;
using AuthAdTenantFunc.PlatformDashboard;
using Dapper;
using MediatR;

namespace AuthAdTenantFunc
{
    public class PlatformStatisticsQuery : RequestBase, IRequest<PlatformStatisticsModel>
    {
       
    }

    public class PlatformStatisticsQueryHandler : IRequestHandler<PlatformStatisticsQuery, PlatformStatisticsModel>
    {
        
        public async Task<PlatformStatisticsModel> Handle(PlatformStatisticsQuery request, CancellationToken cancellationToken)
        {
            var dal = new PlatformDashboardDal();
            return await dal.Execute();

            
        }
    }
}

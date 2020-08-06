using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AuthAdTenantFunc
{
    public  class CompanyListFetchQueryFunction : FunctionBase
    {
        private readonly IMediator _mediator;

        public CompanyListFetchQueryFunction(IMediator mediator)
        {
            _mediator = mediator;
        }
        [FunctionName("CompanyListFetchQuery")]
        public  async Task<IActionResult> CompanyListFetchQuery(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
            CompanyListFetchQuery companyListFetchQuery,
            ILogger log, 
            ClaimsPrincipal principal
           )
        {
            const string functionName = "CompanyListFetchQuery";
            log.LogInformation($"Invoke:{functionName}");
            log.LogTrace( companyListFetchQuery.Dump());
            if (!CheckAuthenticated(principal, log)) return new UnauthorizedResult();
            var result = await _mediator.Send(companyListFetchQuery);
            return new OkObjectResult(result);

           
        }
    }
}

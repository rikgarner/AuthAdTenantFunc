using System;
using System.Collections.Generic;
using System.IO;
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
    public  class CompanyListFetchQueryFunction
    {
        private readonly IMediator _mediator;

        public CompanyListFetchQueryFunction(IMediator mediator)
        {
            _mediator = mediator;
        }
        [FunctionName("CompanyListFetchQuery")]
        public  async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] //HttpRequest req,
          //  UserGhostStateUpdateStateModel userGhostStateUpdateStateModel,
            CompanyListFetchQuery companyListFetchQuery,
            ILogger log, ClaimsPrincipal principal)
        {
            log.LogInformation("C# HTTP trigger function CompanyListFetch.");

            //var result = new List<string>();
            //result.Add("Tesco");

            var result = await _mediator.Send(companyListFetchQuery);
            return new OkObjectResult(result);

           
        }
    }
}

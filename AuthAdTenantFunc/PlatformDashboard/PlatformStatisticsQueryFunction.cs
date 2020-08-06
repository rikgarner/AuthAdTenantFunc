using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace AuthAdTenantFunc
{
    public class PlatformStatisticsQueryFunction : FunctionBase
    {
        private readonly IMediator _mediator;

        public PlatformStatisticsQueryFunction(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("PlatformStatisticsQuery")]
        public async Task<IActionResult> PlatformStatisticsQuery(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
            PlatformStatisticsQuery platformStatisticsQuery,
            ILogger log,
            ClaimsPrincipal principal
        )
        {
            log.LogInformation($"Invoke:PlatformStatisticsQuery");
            log.LogTrace(platformStatisticsQuery.Dump());
            if (!CheckAuthenticated(principal, log)) return new UnauthorizedResult();
            var result = await _mediator.Send(platformStatisticsQuery);
            return new OkObjectResult(result);
        }
    }
}

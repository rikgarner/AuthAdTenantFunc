using System;
using System.Data.SqlClient;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AuthAdTenantFunc
{
    public  class UserGhostStateUpdateCommandFunction : FunctionBase
    {
        private readonly IMediator _mediator;

        public UserGhostStateUpdateCommandFunction(IMediator mediator)
        {
            _mediator = mediator;
        }

        [FunctionName("UserGhostStateUpdateCommand")]
        public  async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
            UserGhostStateUpdateCommand userGhostStateUpdateCommand,
            ILogger log,
            ClaimsPrincipal principal)
        {
            const string functionName = "UserGhostStateUpdateCommand";
            log.LogInformation($"Invoke:{functionName}");
            log.LogTrace(userGhostStateUpdateCommand.Dump());

            if (!CheckAuthenticated(principal, log)) return new UnauthorizedResult();

            userGhostStateUpdateCommand.UserIdentifier = principal.Identity.Name;
            var result = await _mediator.Send(userGhostStateUpdateCommand);

            return new OkObjectResult(result);
        }

        
    }
}

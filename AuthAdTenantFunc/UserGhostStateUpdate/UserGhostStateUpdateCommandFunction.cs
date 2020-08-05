using System;
using System.Data.SqlClient;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AuthAdTenantFunc
{
    public static class UserGhostStateUpdateCommandFunction
    {
        [FunctionName("UserGhostStateUpdateCommand")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]
            UserGhostStateUpdateCommand userGhostStateUpdateCommand,
            ILogger log,
            ClaimsPrincipal principal)
        {
            const string functionName = "UserGhostStateUpdateCommand";
            log.LogInformation($"Invoke:{functionName}");
            log.LogTrace(userGhostStateUpdateCommand.Dump());

            if (principal == null)
            {
                log.LogWarning("No principal.");
                return new UnauthorizedResult();
            }

            if (principal.Identity == null)
            {
                log.LogWarning("No identity.");
                return new UnauthorizedResult();
            }

            if (!principal.Identity.IsAuthenticated)
            {
                log.LogWarning("Request was not authenticated.");
                log.LogWarning($"{principal.Identity.Name} {principal.Identity.AuthenticationType}");
                return new UnauthorizedResult();
            }

            var userId = principal.Identity.Name;

            log.LogInformation($"Authenticated: {userId}  ");

            SqlConnection dbConnection = new SqlConnection();
            dbConnection.ConnectionString = Environment.GetEnvironmentVariable("DatabaseConnectionString");

            await using (dbConnection)
            {
                await dbConnection.OpenAsync();

                await dbConnection.ExecuteAsync(
                    "Mgt.TalentProfileGhostStatusUpdate @TalentId, @ActionedUserEmail, @IsGhosted, @Reason", new
                    {
                        TalentId = userGhostStateUpdateCommand.TalentId,
                        ActionedUserEmail = userId,
                        IsGhosted = userGhostStateUpdateCommand.IsGhosted,
                        Reason = userGhostStateUpdateCommand.Reason
                    });
                await dbConnection.CloseAsync();
            }


            return new OkObjectResult(new CommandResult());
        }
    }
}

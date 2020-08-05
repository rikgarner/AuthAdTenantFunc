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
    public  class UserGhostStateUpdateState
    {
        [FunctionName("UserGhostStateUpdateState")]
        
        public  async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            UserGhostStateUpdateStateModel model,
            ILogger log, ClaimsPrincipal principal)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            SqlConnection dbConnection = new SqlConnection();

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

            dbConnection.ConnectionString = Environment.GetEnvironmentVariable("DatabaseConnectionString");

            await using (dbConnection)
            {
                await dbConnection.OpenAsync();

                await dbConnection.ExecuteAsync(
                    "Mgt.TalentProfileGhostStatusUpdate @TalentId, @ActionedUserEmail, @IsGhosted, @Reason", new
                    {
                        TalentId = 0, 
                        ActionedUserEmail = "",
                        IsGhosted = 1,
                        Reason = ""
                    });
                await dbConnection.CloseAsync();
            }




            return new OkObjectResult(6);
        }
    }

    public class UserGhostStateUpdateStateModel
    {
        public int TalentId { get; set; }
        public bool IsGhosted { get; set; }

        public string Reason { get; set; }
    }
}

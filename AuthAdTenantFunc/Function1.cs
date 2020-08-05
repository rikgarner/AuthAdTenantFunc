using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AuthAdTenantFunc
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log, ClaimsPrincipal principal)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (principal== null)
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

            log.LogInformation($"Authenticated: {principal.Identity.Name}  ");
            //string name = req.Query["name"];

            //string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            //dynamic data = JsonConvert.DeserializeObject(requestBody);
            //name = name ?? data?.name;
            //var name = principal.Identity.Name;

            var name = principal.FindFirst(ClaimTypes.NameIdentifier).Value;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";


            //responseMessage = principal.ToString();

            //responseMessage = JsonConvert.SerializeObject(principal);

            return new OkObjectResult( new MessagePayload{
                Message = responseMessage});
        }

        internal class MessagePayload
        {
            public string Message { get; set; }
        }
    }
}

using System.Security.Claims;
using Microsoft.Extensions.Logging;

namespace AuthAdTenantFunc
{
    public class FunctionBase
    {
        protected static bool CheckAuthenticated(ClaimsPrincipal principal, ILogger log)
        {
            if (principal == null)
            {
                log.LogWarning("No principal.");
                return false;
            }

            if (principal.Identity == null)
            {
                log.LogWarning("No identity.");
                return false;
            }

            if (!principal.Identity.IsAuthenticated)
            {
                log.LogWarning("Request was not authenticated.");
                log.LogWarning($"{principal.Identity.Name} {principal.Identity.AuthenticationType}");
                return false;
            }
            log.LogInformation($"Authenticated: {principal.Identity.Name}  ");
            return true;
        }
    }
}
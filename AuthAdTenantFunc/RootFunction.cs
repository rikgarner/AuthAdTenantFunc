using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;

namespace AuthAdTenantFunc
{
    public  class TenantFunc
    {
        public  IActionResult AssertAuth(ClaimsPrincipal principal, Microsoft.Extensions.Logging.ILogger log)
        {
            if (principal == null)
            {
                log.LogWarning("No principal.");
                return UnauthorizedResult();
            }

            if (principal.Identity == null)
            {
                log.LogWarning("No identity.");
                return  UnauthorizedResult();
            }

            if (!principal.Identity.IsAuthenticated)
            {
                log.LogWarning("Request was not authenticated.");
                log.LogWarning($"{principal.Identity.Name} {principal.Identity.AuthenticationType}");
                return  UnauthorizedResult();
            }

            log.LogInformation($"Authenticated: {principal.Identity.Name}  ");

        }
    }
}

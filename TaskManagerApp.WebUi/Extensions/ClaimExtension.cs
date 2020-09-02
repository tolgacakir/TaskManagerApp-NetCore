using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace TaskManagerApp.WebUi.Extensions
{
    public static class ClaimExtentions
    {

        public static int UserId(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst("UserId");
            return int.Parse(claim.Value);
        }
        public static string Username(this IIdentity identity)
        {
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            Claim claim = claimsIdentity?.FindFirst("Username");
            return claim.Value ?? string.Empty;
        }
    }
}

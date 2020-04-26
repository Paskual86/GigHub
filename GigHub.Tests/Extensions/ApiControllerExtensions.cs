using System.Security.Claims;
using System.Security.Principal;
using System.Web.Http;

namespace GigHub.Tests.Extensions
{
    public static class ApiControllerExtensions
    {
        public static void MockCurrentUser(this ApiController apiController, string userId, string userName) 
        {
            var identity = new GenericIdentity(userName);
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", userId));
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", userName));

            var principal = new GenericPrincipal(identity, null);
            apiController.User = principal;
        }
    }
}

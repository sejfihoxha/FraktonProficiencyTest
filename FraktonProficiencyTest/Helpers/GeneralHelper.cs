using Microsoft.AspNetCore.Http;
using System.Linq;

namespace FraktonProficiencyTest.Helpers
{
    public static class GeneralHelper
    {
       public static int GetUserId(this HttpContext httpContext)
        {
            if (httpContext.User == null) return 0;

            return int.Parse(httpContext.User.Claims.FirstOrDefault(x => x.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value);
        }
    }
}

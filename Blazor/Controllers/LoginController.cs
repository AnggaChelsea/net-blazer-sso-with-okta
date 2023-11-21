using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Blazor.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet("Login")]
        public IActionResult Login([FromQuery]string returnUrl)
        {
            var directUrl = returnUrl is null ? Url.Content("~/") : "/" + returnUrl;
            if (User.Identity.IsAuthenticated)
            {
                return LocalRedirect(directUrl);
            }
            return Challenge(); //disini dia di suruh masuk ke okta klw blum login
        }
        [HttpGet("Logout")]
        public async Task<IActionResult> Logout([FromQuery] string returnUrl)
        {
            var directUrl = returnUrl is null ? Url.Content("~/") : "/" + returnUrl;
            if (!User.Identity.IsAuthenticated)
            {
                return LocalRedirect(returnUrl);
            }
            await HttpContext.SignOutAsync();
            return LocalRedirect(returnUrl);
        }

    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BookApi.Controllers
{
    [Authorize]
    public class MvcController : Controller
    {
        [Route("mvc")]
        public string Index()
        {
            var user = User.Identity?.Name;

       
         
            var isInRole = User.IsInRole("Admin");
            var isInRolea = User.IsInRole("Author");
            var roels = User.Identity as ClaimsIdentity;
            return "Test";
        }
    }
}

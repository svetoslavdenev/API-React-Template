namespace MVCTemplate.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using MVCTemplate.Web.Models;

    using static MVCTemplate.Common.Identity.IdentityConstants.AdminRoleName;

    [Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var isAdmin = this.User.IsInRole(AdminRole);

            return this.View(isAdmin);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}

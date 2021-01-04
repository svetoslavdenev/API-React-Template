namespace APIReactTemplate.API.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using APIReactTemplate.Services.Identity.Interfaces;

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<string>> Login(string username, string password)
        {
            var jwt = await this.userService.Login(username, password);

            if (string.IsNullOrEmpty(jwt))
            {
                return Forbid();
            }

            return jwt;
        }

        [HttpPost("[action]")]
        [Authorize]
        public async Task<ActionResult> Logout()
        {
            var jwt = this.Request.Headers["Authorization"].ToString().Split()[1];

            await this.userService.Logout(jwt);

            return Ok();
        }

        [HttpGet("[action]")]
        public ActionResult<string> WhoAmI()
        {
            return "Username : " + this.User.Identity.Name;
        }
    }
}

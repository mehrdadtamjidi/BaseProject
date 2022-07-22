using Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Services.Interfaces;

namespace MyApi.Controllers
{
    public class UsersController : BaseController
    {
        #region constructor

        private IUserService userService;
        private IOptions<SiteSettings> siteSettings;

        public UsersController(IUserService userService, IOptions<SiteSettings> siteSettings)
        {
            this.userService = userService;
            this.siteSettings = siteSettings;
        }

        #endregion

        [HttpGet("Users")]
        public async Task<IActionResult> Users()
        {
            return Ok(await userService.GetAllUsers());
        }
    }
}

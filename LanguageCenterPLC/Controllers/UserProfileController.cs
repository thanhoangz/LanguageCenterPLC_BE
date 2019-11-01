using LanguageCenterPLC.Application.Interfaces;
using LanguageCenterPLC.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserProfileController : ControllerBase
    {
        private UserManager<AppUser> _userManager;
        private readonly IPermissionService _permissionService;
        public UserProfileController(UserManager<AppUser> userManager, IPermissionService permissionService)
        {
            _userManager = userManager;
            _permissionService = permissionService;
        }

        [HttpGet]
        [Authorize]
        //GET : /api/UserProfile
        public async Task<Object> GetUserProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserID").Value;
            var user = await _userManager.FindByIdAsync(userId);

            var permission = _permissionService.GetAllByUser(new Guid(userId));
            return new
            {
                user,
                permission
            };
        }
    }
}
using LanguageCenterPLC.Application.ViewModels.Categories;
using LanguageCenterPLC.Data;
using LanguageCenterPLC.Data.EF;
using LanguageCenterPLC.Data.Entities;
using LanguageCenterPLC.Infrastructure.Enums;
using LanguageCenterPLC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LanguageCenterPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        private readonly ApplicationSettings _appSettings;
        private readonly AppDbContext _context;

        public AppUsersController(UserManager<AppUser> userManager, IOptions<ApplicationSettings> appSettings, AppDbContext context)
        {
            _userManager = userManager;
         
            _appSettings = appSettings.Value;
            _context = context;
        }

        // GET: api/AppUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = _context.AppUsers.ToList();
            return await Task.FromResult(users);
        }


        // DELETE: api/AppUsers
        [HttpDelete("{id}")]
        public async Task<Object> DeleteUsers(Guid id)
        {
            var user = await _context.AppUsers.FindAsync(id);
            var result = await _userManager.DeleteAsync(user);
            return await Task.FromResult(result);
        }


        [HttpPost]
        [Route("Register")]
        //POST : api/AppUsers/Register
        public async Task<Object> PostApplicationUser(AppUserViewModel model)
        {
            var appnUser = new AppUser()
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                BirthDay = model.BirthDay,
                Balance = 0,
                Avatar = model.Avatar,
                DateCreated = model.DateCreated,
                DateModified = model.DateModified,
                Status = Status.Active,

            };

            try
            {
                var result = await _userManager.CreateAsync(appnUser, model.Password);

                var user = await _userManager.FindByNameAsync(appnUser.UserName);
                var functions = _context.Functions;
                foreach (var item in functions)
                {
                    Permission permission = new Permission
                    {
                        AppUserId = user.Id,
                        FunctionId = item.Id,
                        CanCreate = false,
                        CanUpdate = false,
                        CanDelete = false,
                        CanRead = false,
                        Status = Status.Active
                    };
                }

                return Ok(result);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [HttpPut]
        [Route("Update")]
        //POST : api/AppUsers/Register
        public async Task<Object> PutApplicationUser(AppUserViewModel user)
        {

            try
            {
                var userUpdate = await _userManager.FindByNameAsync(user.UserName);
                if (userUpdate.PasswordHash != null)
                {
                    await _userManager.RemovePasswordAsync(userUpdate);
                }

                await _userManager.AddPasswordAsync(userUpdate, user.Password);
                //var result = await _userManager.UpdateAsync(user);
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        [HttpPost]
        [Route("Login")]
        //POST : api/AppUsers/Login
        public async Task<IActionResult> Login(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {

                Const.tempUserId = user.Id;
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserID",user.Id.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.JWT_Secret)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return Ok(new { token });
            }
            else
                return BadRequest(new { message = "Username or password is incorrect." });
        }
    }
}

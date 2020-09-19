using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly DataContext context;

        public UserManager<User> UserManager { get; }

        public AdminController(DataContext context, UserManager<User> userManager)
        {

            this.context = context;
            UserManager = userManager;
        }

        [Authorize(Policy = "RequiredAdminRole")]
        [HttpGet("usersWithRoles")]
        public async Task<IActionResult> GetUsersWithRoles()
        {
            var userList = await this.context.Users.OrderBy(x => x.UserName).Select(user => new
            {
                Id = user.Id,
                UserName = user.UserName,
                Roles = (from userRole in user.UserRoles
                         join role in this.context.Roles
                         on userRole.RoleId equals role.Id
                         select role.Name
                            ).ToList()
            }).ToListAsync();

            return Ok(userList);
        }

        [Authorize(Policy = "RequiredAdminRole")]
        [HttpPost("editRoles/{userName}")]
        public async Task<IActionResult> EditRoles(string userName, RoleEditDto roleEditDto)
        {
            var user = await UserManager.FindByNameAsync(userName);

            var userRoles = await this.UserManager.GetRolesAsync(user);

            var selectedRoles = roleEditDto.RoleNames ?? new string[] { };

            var result = await this.UserManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded)
                return BadRequest("Failed to add to roles");

            result = await this.UserManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded)
                return BadRequest("Failed to add to roles");

            return Ok(await this.UserManager.GetRolesAsync(user));

        }

        [Authorize(Policy = "ModeratePhotoRole")]
        [HttpGet("photosForModeration")]
        public IActionResult GetPhotosForModeration()
        {
            return Ok("Admin or moderator can see this");
        }
    }
}
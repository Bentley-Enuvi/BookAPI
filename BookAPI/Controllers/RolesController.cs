using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContactBookAPI.Controllers
{
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        //Add Role
        [HttpPost("add-role")]
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
                return BadRequest("Sorry! Role name must not be empty.");

            var identityRole = new IdentityRole { Name = roleName };
            var roleResult = await _roleManager.CreateAsync(identityRole);

            if (roleResult.Succeeded)
            {
                return Ok("New role added!");
            }

            var errList = new List<string>();
            foreach (var err in roleResult.Errors)
            {
                errList.Add(err.Description);
            }

            return BadRequest(errList);
        }
    }
}

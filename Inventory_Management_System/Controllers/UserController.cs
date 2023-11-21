using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management_System.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;

    public UserController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("Roles")]
    public async Task<IActionResult> GetUserRoles([FromBody] string userName)
    {
        if (string.IsNullOrEmpty(userName))
        {
            return BadRequest("Invalid request. Provide a valid userName in the request body.");
        }

        var user = await _userManager.FindByNameAsync(userName);

        if (user == null)
        {
            return NotFound($"User with userName '{userName}' not found");
        }

        var roles = await _userManager.GetRolesAsync(user);

        return Ok(new { UserName = user.UserName, Roles = roles });
    }

}

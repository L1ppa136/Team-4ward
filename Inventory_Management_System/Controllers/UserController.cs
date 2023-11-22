using System.ComponentModel.DataAnnotations;
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

    [HttpPost("Role")]
    public async Task<IActionResult> GetUserRoles([Required] string userName)
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
        var role = roles[0];

        return Ok(role);
    }

}

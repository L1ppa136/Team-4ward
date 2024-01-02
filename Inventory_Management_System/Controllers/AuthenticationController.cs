using System.ComponentModel.DataAnnotations;
using Inventory_Management_System.Contracts;
using Inventory_Management_System.Service.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;

namespace Inventory_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly string _defaultRole = "User";
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result =
                await _authenticationService.RegisterAsync(request.Email, request.UserName, request.Password,
                    _defaultRole);

            if (!result.Success)
            {
                AddErrors(result);
                return BadRequest(ModelState);
            }

            return CreatedAtAction(nameof(Register), new RegistrationResponse(result.Email, result.UserName));
        }

        private void AddErrors(AuthenticationResult result)
        {
            foreach (var error in result.ErrorMessages)
            {
                ModelState.AddModelError(error.Key, error.Value);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AuthenticationResponse>> Authenticate([FromBody] AuthenticationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authenticationService.LoginAsync(request.UserName, request.Password);

            if (!result.Success)
            {
                AddErrors(result);
                return BadRequest(ModelState);
            }

            return Ok(new AuthenticationResponse(result.Email, result.UserName, result.Token));
        }

        [HttpPost("Roles")]
        public async Task<IActionResult> GetUserRoles([FromBody] RoleRequest request)
        {
            if (string.IsNullOrEmpty(request.userName))
            {
                return BadRequest("Invalid request. Provide a valid userName in the request body.");
            }

            var roles = await _authenticationService.GetRoles(request.userName);

            if (roles.IsNullOrEmpty())
            {
                return NotFound("Roles are not found.");
            }

            return Ok(roles);
        }
    }
}
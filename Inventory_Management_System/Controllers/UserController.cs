using Inventory_Management_System.Model;
using Inventory_Management_System.Model.InputModel;
using Inventory_Management_System.Service.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController
    {
        private readonly ILogger<UserController> _logger;
        //private readonly IUserRepository _userRepository = new UserRepository();
        //private readonly Admin _masterAdmin = new Admin("sa", "1234");


        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        [HttpPatch("UpdateUser")]
        public IActionResult UpdateAsync([FromBody]UserInput model)
        {
            //_masterAdmin.ModifyUserEssentials(model.Id, model.UserName, model.Password);
            //return Ok("User update successful!");
        }
    }
}

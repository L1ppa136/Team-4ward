using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Production Leader, Admin")]
    public class ProductionLeaderController : ControllerBase
    {
    }
}

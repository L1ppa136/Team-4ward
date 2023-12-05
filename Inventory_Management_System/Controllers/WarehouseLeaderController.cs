using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory_Management_System.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(Roles = "Warehouse Leader, Admin")]
    public class WarehouseLeaderController : ControllerBase
    {
    }
}

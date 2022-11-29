using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ProjectManagementSystem.Server.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdministrationController(RoleManager<IdentityRole> roleManager)
        {

            _roleManager = roleManager;
        }
       
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
    }
}

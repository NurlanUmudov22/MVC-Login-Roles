using Educal_MVC.Models;
using Educal_MVC.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Educal_MVC.Areas.Admin.Controllers
{
    [Area("Admin")]

    //[Authorize(Roles = "SuperAdmin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<AppUser> userManager,
                              RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();

            List<UserRoleVM> usersWithRoles = new();

            foreach (var item in users)
            {
                var roles = await _userManager.GetRolesAsync(item);
                var userRoles = String.Join(",", roles);
                usersWithRoles.Add(new UserRoleVM
                {
                    FullName = item.FullName,
                    Email = item.Email,
                    UserName = item.UserName,
                    Roles = userRoles
                });
            }


            return View(usersWithRoles);
        }
    }
}

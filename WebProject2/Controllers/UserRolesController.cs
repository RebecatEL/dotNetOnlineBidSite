using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject2.Models;

namespace WebProject2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserRolesController : Controller
    {
        private readonly UserManager<Client> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserRolesController(UserManager<Client> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userRolesViewModel = new List<UserRoleViewModel>();
            foreach(Client u in users)
            {
                var thisViewModel = new UserRoleViewModel();
                thisViewModel.UserId = u.Id;
                thisViewModel.FirstName = u.FirstName;
                thisViewModel.LastName = u.LastName;
                thisViewModel.Email = u.Email;
                thisViewModel.Roles = await GetUserRolesAsync(u);  
                userRolesViewModel.Add(thisViewModel);
            }

            return View(userRolesViewModel);
        }

        private async Task<List<string>> GetUserRolesAsync(Client user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }


        [HttpGet]
        public async Task<IActionResult> Manage(string userId)
        {
            ViewBag.UserId = userId;
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id = {userId} cannot be found";
                return View("NotFound");
            }

            ViewBag.UserName = user.UserName;
            var model = new List<ManageUserRolesViewModel>();
            foreach (var role in _roleManager.Roles)
            {
                var userRolesViewModel = new ManageUserRolesViewModel
                {
                    RolesId = role.Id,
                    RoleName = role.Name,
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRolesViewModel.Selected = true;
                }
                else
                {
                    userRolesViewModel.Selected = false;
                }
                model.Add(userRolesViewModel);
            }


            return View(model);
        }

        
        [HttpPost]
        public async Task<IActionResult> Manage(List<ManageUserRolesViewModel> model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                return View();
            }
            var roles = await _userManager.GetRolesAsync(user);
            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove user from roles");
                return View(model);
            }
            
            result = await _userManager.AddToRolesAsync(user, model.Where(x => x.Selected).Select(y=> y.RoleName));
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot add user to roles");

            }
            return RedirectToAction("Index");
        }
        


    }
}

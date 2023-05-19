using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject2.Enum;
using WebProject2.Models;

namespace WebProject2.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserLockdownController : Controller
    {
        private readonly UserManager<Client> _userManager;
        public UserLockdownController(UserManager<Client> userManager)
        {
            _userManager = userManager;  
        }

        public async Task<IActionResult> Index()
        {
            var users = await _userManager.Users.ToListAsync();
            var userLockdownViewModel = new List<UserLockdownViewModel>();
            foreach (Client u in users)
            {
                var thisViewModel = new UserLockdownViewModel();
                thisViewModel.UserId = u.Id;
                thisViewModel.FirstName = u.FirstName;
                thisViewModel.LastName = u.LastName;
                thisViewModel.Email = u.Email;
                if (u.LockoutEnd == null){
                    thisViewModel.isLockDown = false;
                }
                userLockdownViewModel.Add(thisViewModel);
            }

            return  View(userLockdownViewModel);
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

            var model = new ManageUserLockdownViewModel();
            model.Lockdown = "Looked down";
            if (user.LockoutEnd != null){ model.Selected = true; }
            else { model.Selected = false; }
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Manage(ManageUserLockdownViewModel model, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View();
            }

            var result = await _userManager.SetLockoutEndDateAsync(user,DateTime.Now);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Cannot remove lockdown from user");
                return View(model);
            }

            if (model.Selected == true)
            {
                var result2 = await _userManager.SetLockoutEndDateAsync(user,DateTime.Now.AddYears(100));
                if (!result2.Succeeded)
                {
                    ModelState.AddModelError("", "Cannot apply lockdown to user");
                }
            }

            return RedirectToAction("Index");
        }




    }
}

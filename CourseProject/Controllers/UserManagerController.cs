using CourseProject.Data;
using CourseProject.Models;
using CourseProject.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CourseProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserManagerController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly CustomFormsDbContext _context;
        public UserManagerController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, CustomFormsDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }
        
        [HttpGet]
        public async Task<IActionResult> ShowUsers()
        {
            var users = _userManager.Users.ToList();
            var userViewModels = new List<UserModel>(); 

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);

           
                var userViewModel = new UserModel
                {
                    Email = user.Email,
                    Role = roles.FirstOrDefault(),
                    Status = user.LockoutEnd
                };

                userViewModels.Add(userViewModel); // Добавляем каждый UserViewModel в список
            }
            
            
            return View(userViewModels);
        }

      
        private async Task<IActionResult> RedirectCurrentUser()
        {
            await _context.SaveChangesAsync();
            return RedirectToAction("Index","Home");
        }
        private async Task<List<User>> GetUsersToProcess(string[] selectedItems)
        {
            return await _userManager.Users
                .Where(user => selectedItems.Contains(user.Email))
                .ToListAsync();
        }
        private bool IsCurrentUserSelected(string[] selectedItems, string currentUserEmail)
        {
            return selectedItems.Contains(currentUserEmail);
        }

        [HttpGet]
        public IActionResult ShowMsg()
        {
            return Content("This feature will be implemented soon");
        }
        
        [HttpPost]
        public async Task<IActionResult> Block(string[] selectedItems)
        {
            var userCurrent = await _userManager.GetUserAsync(User);
            var userCurrentEmail = userCurrent.Email;
            var usersToBlock = await  GetUsersToProcess(selectedItems);
            foreach (var user in usersToBlock)
            {
                user.LockoutEnabled = true;
                user.LockoutEnd = DateTimeOffset.UtcNow.AddDays(5);
            }
            await _context.SaveChangesAsync();
            if (IsCurrentUserSelected(selectedItems, userCurrentEmail))
            {
                return await RedirectCurrentUser();
            }
            
            return RedirectToAction("ShowUsers");
        }

        [HttpPost]
        public async Task<IActionResult> Unblock(string[] selectedItems)
        {
            var usersToBlock = await  GetUsersToProcess(selectedItems);
            foreach (var user in usersToBlock)
            {
                user.LockoutEnabled = false;
                user.LockoutEnd = null;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("ShowUsers");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string[] selectedItems)
        {
            var userCurrent = await _userManager.GetUserAsync(User);
            var userCurrentEmail = userCurrent.Email;
            var usersToDelete = await  GetUsersToProcess(selectedItems);
            _context.Users.RemoveRange(usersToDelete);
            await _context.SaveChangesAsync();
            if (IsCurrentUserSelected(selectedItems, userCurrentEmail))
            {
                return await RedirectCurrentUser();
            }
            
            return RedirectToAction("ShowUsers");
        }

        public async Task ChangeRoles(string[] selectedItems, string newRole)
        {
            var admins = await GetUsersToProcess(selectedItems);
    
            foreach (var user in admins)
            {
                var currentRoles = await _userManager.GetRolesAsync(user);

                if (!currentRoles.Contains(newRole))
                {
                  
                    var resultRemove = await _userManager.RemoveFromRolesAsync(user, currentRoles);
                    if (!resultRemove.Succeeded)
                    {
                        foreach (var error in resultRemove.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }

                  
                    var resultAdd = await _userManager.AddToRoleAsync(user, newRole);
                    if (!resultAdd.Succeeded)
                    {
                        foreach (var error in resultAdd.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> MakeAdmin(string[] selectedItems)
        {
            await ChangeRoles(selectedItems, "Admin"); 
            return RedirectToAction("ShowUsers");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFromAdmins(string[] selectedItems)
        {
            await ChangeRoles(selectedItems, "User"); 
            return RedirectToAction("ShowUsers");
        }

    }
}



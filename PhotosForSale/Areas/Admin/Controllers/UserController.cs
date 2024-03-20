using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Photos.DataAccess.Data;
using Photos.DataAccess.Repository.IRepository;
using Photos.Models.Models;
using Photos.Models.Models.ViewModels;
using Photos.Utility;
using System.Data;

namespace PhotosForSale.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
    public class UserController : Controller
    {
        private readonly ApplicationDbConext _db;
        private readonly UserManager<IdentityUser> _userManager;
        public UserController(ApplicationDbConext db, UserManager<IdentityUser> userManager)
        {
            _db = db;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RoleManagement(string userId)
        {
            string roleId = _db.UserRoles.FirstOrDefault(u=>u.UserId == userId).RoleId;

            RoleManagementVM roleManagementVM = new RoleManagementVM() { 
                ApplicationUser = _db.ApplicationUsers.Include(u=>u.Company).FirstOrDefault(u=>u.Id==userId),
                RoleList = _db.Roles.Select(i=>new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Name
                }),
                CompanyList = _db.Companies.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            roleManagementVM.ApplicationUser.Role = _db.Roles.FirstOrDefault(u => u.Id == roleId).Name;

            return View(roleManagementVM);
        }

        [HttpPost]
        public IActionResult RoleManagement(RoleManagementVM roleManagementVM)
        {
            string roleId = _db.UserRoles.FirstOrDefault(u => u.UserId == roleManagementVM.ApplicationUser.Id).RoleId;
            string oldRole = _db.Roles.FirstOrDefault(u => u.Id == roleId).Name;

            if(!(roleManagementVM.ApplicationUser.Role == oldRole))//a role was updated
            {
                ApplicationUser applicationUser = _db.ApplicationUsers.FirstOrDefault(u=>u.Id==roleManagementVM.ApplicationUser.Id);
                if(roleManagementVM.ApplicationUser.Role == SD.Role_Company) 
                {
                    applicationUser.CompanyId = roleManagementVM.ApplicationUser.CompanyId;
                }
                if(oldRole == SD.Role_Company)
                {
                    applicationUser.CompanyId = null;
                }
                _db.SaveChanges();

                _userManager.RemoveFromRoleAsync(applicationUser, oldRole).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(applicationUser, roleManagementVM.ApplicationUser.Role).GetAwaiter().GetResult();
            }
            return RedirectToAction("Index");
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll() 
        {
            List<ApplicationUser> objAppUserList = _db.ApplicationUsers.Include(u=>u.Company).ToList();

            var userRoles = _db.UserRoles.ToList();
            var roles = _db.Roles.ToList();

            foreach (ApplicationUser user in objAppUserList)
            {
                var roleId = userRoles.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roles.FirstOrDefault(r => r.Id == roleId).Name;

                if(user.Company is null)
                {
                    user.Company = new() { Name = "" };
                }
            }
            return Json(new {data = objAppUserList});
        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody]string id)
        {
            var objFromDb = _db.ApplicationUsers.FirstOrDefault(u=>u.Id == id);
            if(objFromDb == null)
            {
                return Json(new {success = false, mesage = "Error while Locking/Unlocking"});
            }

            if(objFromDb.LockoutEnd is not null && objFromDb.LockoutEnd > DateTime.Now)//user is locked
            {
                objFromDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                objFromDb.LockoutEnd = DateTime.Now.AddYears(10);
            }
            _db.SaveChanges();

            return Json(new { success = true, message = "Operation successful"});
        }
        #endregion
    }
}

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
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public UserController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, IUnitOfWork unitOfWork)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RoleManagement(string userId)
        {
            RoleManagementVM roleManagementVM = new RoleManagementVM() { 
                ApplicationUser = _unitOfWork.ApplicationUser.Get(u=>u.Id==userId, includeProperties:"Company"),
                RoleList = _roleManager.Roles.Select(i=>new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Name
                }),
                CompanyList = _unitOfWork.Company.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                })
            };

            roleManagementVM.ApplicationUser.Role = _userManager.GetRolesAsync(_unitOfWork.ApplicationUser.Get(u => u.Id == userId))
                .GetAwaiter().GetResult().FirstOrDefault();

            return View(roleManagementVM);
        }

        [HttpPost]
        public IActionResult RoleManagement(RoleManagementVM roleManagementVM)
        {
            string oldRole = _userManager.GetRolesAsync(_unitOfWork.ApplicationUser.Get(u => u.Id == roleManagementVM.ApplicationUser.Id))
                .GetAwaiter().GetResult().FirstOrDefault();

            ApplicationUser applicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == roleManagementVM.ApplicationUser.Id);

            if (!(roleManagementVM.ApplicationUser.Role == oldRole))//a role was updated
            {
                
                if(roleManagementVM.ApplicationUser.Role == SD.Role_Company) 
                {
                    applicationUser.CompanyId = roleManagementVM.ApplicationUser.CompanyId;
                }
                if(oldRole == SD.Role_Company)
                {
                    applicationUser.CompanyId = null;
                }
                _unitOfWork.ApplicationUser.Update(applicationUser);
                _unitOfWork.Save();

                _userManager.RemoveFromRoleAsync(applicationUser, oldRole).GetAwaiter().GetResult();
                _userManager.AddToRoleAsync(applicationUser, roleManagementVM.ApplicationUser.Role).GetAwaiter().GetResult();
            }
            else
            {
                if(oldRole == SD.Role_Company && applicationUser.CompanyId != roleManagementVM.ApplicationUser.CompanyId)
                {
                    applicationUser.CompanyId = roleManagementVM.ApplicationUser.CompanyId;
                    _unitOfWork.ApplicationUser.Update(applicationUser);
                    _unitOfWork.Save();
                }
            }
            return RedirectToAction("Index");
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll() 
        {
            List<ApplicationUser> objAppUserList = _unitOfWork.ApplicationUser.GetAll(includeProperties: "Company").ToList();

            foreach (ApplicationUser user in objAppUserList)
            {
                user.Role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();

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
            var objFromDb = _unitOfWork.ApplicationUser.Get(u=>u.Id == id);
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
            _unitOfWork.ApplicationUser.Update(objFromDb);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Operation successful"});
        }
        #endregion
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Photos.DataAccess.Data;
using Photos.DataAccess.Repository.IRepository;
using Photos.Models.Models;
using Photos.Models.Models.ViewModels;

namespace PhotosForSale.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MyPhotoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public MyPhotoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<MyPhoto> objMyPhotoList = _unitOfWork.MyPhoto.GetAll().ToList();
            return View(objMyPhotoList);
        }

        public IActionResult Create()
        {
            MyPhotoViewModel myPhotoViewModel = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.Id.ToString()
                }),
                MyPhoto = new MyPhoto()
            };

            return View(myPhotoViewModel);
        }
        [HttpPost]
        public IActionResult Create(MyPhotoViewModel myPhotoViewModel)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.MyPhoto.Add(myPhotoViewModel.MyPhoto);
                _unitOfWork.Save();
                TempData["success"] = "Photo added successfuly";
                return RedirectToAction("Index", "MyPhoto");
            }
            else
            {
                myPhotoViewModel.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.Id.ToString()
                });
                return View(myPhotoViewModel);
            }
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            MyPhoto? myPhotoFromDb = _unitOfWork.MyPhoto.Get(u => u.Id == id);
            //Category? categoryFromDb_01 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category? categoryFromDb_02 = _db.Categories.Where(u=>u.Id == id).FirstOrDefault();
            if (myPhotoFromDb == null)
            {
                return NotFound();
            }
            return View(myPhotoFromDb);
        }

        [HttpPost]
        public IActionResult Edit(MyPhoto obj)
        {
            if (obj.Title == obj.Description.ToString())
            {
                ModelState.AddModelError("title", "The Description can't exactly match the Photo Title.");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.MyPhoto.Update(obj);
                _unitOfWork.Save();
                TempData["success"] = "Photo updated successfuly";
                return RedirectToAction("Index", "MyPhoto");
            }
            return View();

        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            MyPhoto? myPhotoFromDb = _unitOfWork.MyPhoto.Get(u => u.Id == id);
            //Category categoryFromDb_01 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category categoryFromDb_02 = _db.Categories.Where(u=>u.Id == id).FirstOrDefault();
            if (myPhotoFromDb == null)
            {
                return NotFound();
            }
            return View(myPhotoFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            MyPhoto? obj = _unitOfWork.MyPhoto.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.MyPhoto.Remove(obj);
            _unitOfWork.Save();
            TempData["success"] = "Photo deleted successfuly";
            return RedirectToAction("Index", "MyPhoto");

        }
    }
}

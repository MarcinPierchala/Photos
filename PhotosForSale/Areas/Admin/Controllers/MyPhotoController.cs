using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Photos.DataAccess.Data;
using Photos.DataAccess.Repository.IRepository;
using Photos.Models.Models;

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
            //Projection in EF Core HARD 2 remember
            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            {
                Text = u.CategoryName,
                Value = u.Id.ToString()
            });

            //declare ViewBag - temporary bag of data - exist only during this http request
            ViewBag.CategoryList = CategoryList;
            return View();
        }
        [HttpPost]
        public IActionResult Create(MyPhoto obj)
        {
            if (obj.Title == obj.Description.ToString())
            {
                ModelState.AddModelError("title", "The Description can't exactly match the Photo Title.");
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.MyPhoto.Add(obj);
                _unitOfWork.Save();
                TempData["success"] = "Photo added successfuly";
                return RedirectToAction("Index", "MyPhoto");
            }
            return View();

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

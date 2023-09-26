using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Photos.DataAccess.Repository.IRepository;
using Photos.Models.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace PhotosForSale.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<MyPhoto> photosList = _unitOfWork.MyPhoto.GetAll(includeProperties: "Category");
            return View(photosList);
        }

        public IActionResult Details(int photoId)
        {
            ShoppingCart shoppingCart = new()
            {
                MyPhoto = _unitOfWork.MyPhoto.Get(u => u.Id == photoId, includeProperties: "Category"),
                PhotoId = photoId
            };
            return View(shoppingCart);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId;

            ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(c=>c.ApplicationUserId == userId &&
            c.PhotoId == shoppingCart.PhotoId);

            if(cartFromDb != null)
            {
                //shopping cart already exist
                TempData["success"] = "To zdjęcie już znajduje się w koszyku";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                //must add new shopping cart
                TempData["success"] = "Super, zdjęcie zostało dodane do koszyka!";
                _unitOfWork.ShoppingCart.Add(shoppingCart);
            }
            _unitOfWork.ShoppingCart.Add(shoppingCart);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
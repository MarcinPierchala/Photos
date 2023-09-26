using Microsoft.AspNetCore.Mvc;
using Photos.DataAccess.Repository.IRepository;
using Photos.Models.Models;
using System.Diagnostics;

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
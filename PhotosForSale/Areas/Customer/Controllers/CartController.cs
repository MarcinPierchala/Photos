using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Photos.DataAccess.Repository.IRepository;
using Photos.Models.Models;
using Photos.Models.Models.ViewModels;
using System.Security.Claims;

namespace PhotosForSale.Areas.Customer.Controllers
{
    [Area("customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == userId,
                includeProperties: "MyPhoto")
            };

            foreach(var cart in  ShoppingCartVM.ShoppingCartList)
            {
                ShoppingCartVM.OrderTotal += cart.MyPhoto.Price;
            }
            return View(ShoppingCartVM);
        }
    }
}

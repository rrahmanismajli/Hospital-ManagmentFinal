using Hospital_Managment.Data;
using Hospital_Managment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;

namespace Hospital_Managment.Areas.Costumer.Controllers
{
    [Area("Costumer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Pharmacy()
        {
            var products = _context.PharmacyProducts.ToList();
            return View(products);
        }
        public async Task<IActionResult> Details(int? productId)
        {
            ShoppingCart cartObj = new()
            {
                Count = 1,
                ProductId = (int)productId,
                Product = _context.PharmacyProducts.FirstOrDefault(u => u.productId == productId),
            };
            return View(cartObj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.UserId = claim.Value;

            ShoppingCart cartfromDb = _context.ShoppingCarts.FirstOrDefault(u => u.UserId == claim.Value && u.ProductId == shoppingCart.ProductId);
            //ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.GetFirstOrDefault(
            //    u => u.ApplicationUserId == claim.Value && u.ProductId == shoppingCart.ProductId);


            if (cartfromDb == null)
           {

                _context.ShoppingCarts.Add(shoppingCart);
                
                //HttpContext.Session.SetInt32(SD.SessionCart,
                //    _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count);
            }
            else
            {
                cartfromDb.Count++;
              
            }
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }
        public IActionResult Index()
        {
            return View();
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
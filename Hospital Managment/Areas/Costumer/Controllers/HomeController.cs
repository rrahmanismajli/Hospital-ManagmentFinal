using Hospital_Managment.Data;
using Hospital_Managment.Models;
using Hospital_Managment.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Diagnostics;
using System.Security.Claims;

namespace Hospital_Managment.Areas.Costumer.Controllers
{
    [Area("Costumer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context,IEmailSender emailSender)
        {
            _logger = logger;
            _context = context;
            _emailSender=emailSender;
        }
        public IActionResult Pharmacy(Pagination pagination,string searchTerm)
        {
            
            
            if (!string.IsNullOrEmpty(searchTerm))
            {
               
                 var products = _context.PharmacyProducts.Where(p => p.productName.Contains(searchTerm));
                pagination.CalculateTotalPages(products.Count());
                pagination.CalculateStartItem();
                var pagedProducts = products.Skip(pagination.StartItem).Take(pagination.PageSize);
                return View(new Tuple<IEnumerable<PharmacyProduct>, Pagination>(pagedProducts, pagination));
            }
            else
            {
                    var products = _context.PharmacyProducts.ToList();
                    pagination.CalculateTotalPages(products.Count());
                    pagination.CalculateStartItem();
                    var pagedProducts = products.Skip(pagination.StartItem).Take(pagination.PageSize);
                    return View(new Tuple<IEnumerable<PharmacyProduct>, Pagination>(pagedProducts, pagination));
               
              
            }
            
            
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
                _context.SaveChanges();
                HttpContext.Session.SetInt32(RolesStrings.SessionCart,
                    _context.ShoppingCarts.Where(u => u.UserId == claim.Value).ToList().Count);
            }
            else
            {
                cartfromDb.Count++;
                _context.SaveChanges();

            }
           


            return RedirectToAction(nameof(Index));
        }
        public IActionResult Index()
        {
         
            return View();
        }
        [HttpPost]
        [ActionName("Index")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Indexed()
        {
            var emailer = Request.Form["Emailsub"];
            await _emailSender.SendEmailAsync(emailer, $"Thanks for Subscribing", "<p>You have been subscribed</p>");
            return View(nameof(Index));
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
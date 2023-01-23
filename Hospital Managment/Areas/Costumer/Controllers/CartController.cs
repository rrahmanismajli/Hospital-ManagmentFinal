using Hospital_Managment.Data;
using Hospital_Managment.Models;
using Hospital_Managment.Utilities;
using Hospital_Managment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe.Checkout;
using System.Security.Claims;

namespace Hospital_Managment.Areas.Costumer.Controllers
{
    [Area("Costumer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
           
           
            ShoppingCartVM shoppingCartVM = new ShoppingCartVM()
            {
                ListCart = _context.ShoppingCarts.Where(u => u.UserId == claim.Value).Include(u => u.Product).ToList(),
                OrderHeader = new()
            };
            
            

            foreach (var item in shoppingCartVM.ListCart)
            {
               
                shoppingCartVM.OrderHeader.OrderTotal  += (double)item.Product.price * item.Count;
     
            }
           
            return View(shoppingCartVM);
        }
        public IActionResult Plus(int cartId)
        {
            var cart = _context.ShoppingCarts.FirstOrDefault(u => u.Id == cartId);
            cart.Count++;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Minus(int cartId)
        {
            var cart = _context.ShoppingCarts.FirstOrDefault(u => u.Id == cartId);
            cart.Count--;
            if (cart.Count<=0)
            {
                _context.ShoppingCarts.Remove(cart);

            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(int cartId)
        {
            var cart = _context.ShoppingCarts.FirstOrDefault(u => u.Id == cartId);
            _context.ShoppingCarts.Remove(cart);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


            ShoppingCartVM shoppingCartVM = new ShoppingCartVM()
            {
                ListCart = _context.ShoppingCarts.Where(u => u.UserId == claim.Value).Include(u => u.Product).ToList(),
                OrderHeader = new()
            };
            shoppingCartVM.OrderHeader.ApplicationUser = _context.ApplicationUsers.FirstOrDefault(u => u.Id == claim.Value);
            shoppingCartVM.OrderHeader.Name = shoppingCartVM.OrderHeader.ApplicationUser.Name;
            shoppingCartVM.OrderHeader.PhoneNumber = shoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
            shoppingCartVM.OrderHeader.StreetAddress = shoppingCartVM.OrderHeader.ApplicationUser.StreetAdress;
            shoppingCartVM.OrderHeader.City = shoppingCartVM.OrderHeader.ApplicationUser.City;
            shoppingCartVM.OrderHeader.State = shoppingCartVM.OrderHeader.ApplicationUser.State;
            shoppingCartVM.OrderHeader.PostalCode = shoppingCartVM.OrderHeader.ApplicationUser.PostalCode;


            foreach (var item in shoppingCartVM.ListCart)
            {

                shoppingCartVM.OrderHeader.OrderTotal += (double)item.Product.price * item.Count;

            }
            return View(shoppingCartVM);
        }
        [HttpPost]
        [ActionName("Summary")]
        [ValidateAntiForgeryToken]
        public IActionResult SummaryPost(ShoppingCartVM ShoppingCartVM)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ShoppingCartVM.ListCart = _context.ShoppingCarts.Where(u => u.UserId == claim.Value).Include(u => u.Product).ToList();


            ShoppingCartVM.OrderHeader.PaymentStatus = RolesStrings.PaymentStatusPending;
            ShoppingCartVM.OrderHeader.OrderStatus = RolesStrings.StatusPending;
            ShoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
            ShoppingCartVM.OrderHeader.ApplicationUserId = claim.Value;

            foreach (var item in ShoppingCartVM.ListCart)
            {

                ShoppingCartVM.OrderHeader.OrderTotal += (double)item.Product.price * item.Count;

            }
            _context.OrderHeader.Add(ShoppingCartVM.OrderHeader);
            _context.SaveChanges();
            foreach (var cart in ShoppingCartVM.ListCart)
            {
                OrderDetails orderDetail = new()
                {
                    ProductId = cart.ProductId,
                    OrderId = ShoppingCartVM.OrderHeader.Id,
                    Price = (double)cart.Product.price,
                    Count = cart.Count,

                };
                _context.OrderDetail.Add(orderDetail);
                _context.SaveChanges();
            }

            //stripe
            var domain = "https://localhost:44361/";
            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>(),
        
                Mode = "payment",
                SuccessUrl = domain+$"costumer/cart/OrderConfirmation?id={ShoppingCartVM.OrderHeader.Id}",
                CancelUrl = domain+$"Costumer/cart/Index",
            };
            foreach (var item in ShoppingCartVM.ListCart)
            {

                var sessionLineItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(item.Product.price * 100),//20.00 -> 2000
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.productName
                        },

                    },
                    Quantity = item.Count,
                };
                options.LineItems.Add(sessionLineItem);

            }
            var service = new SessionService();
            Session session = service.Create(options);
            ShoppingCartVM.OrderHeader.SessionId = session.Id;
            ShoppingCartVM.OrderHeader.PaymentIntentId = session.PaymentIntentId;
            _context.SaveChanges();
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
            //_context.ShoppingCarts.RemoveRange(ShoppingCartVM.ListCart);
            //_context.SaveChanges();
            //return RedirectToAction("Index","Home");
        }
        public IActionResult OrderConfirmation(int id)
        {
            OrderHeader orderHeader = _context.OrderHeader.FirstOrDefault(u => u.Id == id);
            var service = new SessionService();
            Session session = service.Get(orderHeader.SessionId);
            if(session.PaymentStatus.ToLower() == "paid")
            {
                orderHeader.OrderStatus = RolesStrings.StatusApproved;
                orderHeader.PaymentStatus = RolesStrings.PaymentStatusApproved;
                orderHeader.PaymentDate= DateTime.Now;
                _context.SaveChanges();
            }
            List<ShoppingCart> shoppingCarts = _context.ShoppingCarts.Where(c => c.UserId == orderHeader.ApplicationUserId).ToList();
            _context.ShoppingCarts.RemoveRange(shoppingCarts);
            _context.SaveChanges();
            return View(id);
        }
        //pk_test_51MS91VF1ntIk5mUsWclhf9VIbolQz44QYFXc7VnOsqjyjRVHP3LsTxpHfXOGBcfUJMlwynGdaqAuLH8kQ21ULZD3000hqV5HXb
        //sk_test_51MS91VF1ntIk5mUsd5QZ2JBxqWTD96HHYD9KUNEiXWbeDV9rKGTYXn7ZlZxNAjvboIAE4L2kxjvG4n0atAH4Z6Zx00Fu1HHJxx
    }
}

using Hospital_Managment.Data;
using Hospital_Managment.Models;
using Hospital_Managment.Utilities;
using Hospital_Managment.ViewModels;
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
        public User_DoctorVm user_doc{ get; set; }
        private readonly IEmailSender _emailSender;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context,IEmailSender emailSender)
        {
            _logger = logger;
            _context = context;
            _emailSender=emailSender;
        }
        public IActionResult Pharmacy(Pagination pagination,string searchTerm,string status)
        {

            switch (status)
            {
                case "price_low_to_high":
                    if (!string.IsNullOrEmpty(searchTerm))
                    {

                        var products = _context.PharmacyProducts.OrderBy(p=>p.price).Where(p => p.productName.Contains(searchTerm));
                       
                        pagination.CalculateTotalPages(products.Count());
                        pagination.CalculateStartItem();
                        var pagedProducts = products.Skip(pagination.StartItem).Take(pagination.PageSize);
                        return View(new Tuple<IEnumerable<PharmacyProduct>, Pagination>(pagedProducts, pagination));
                    }
                    else
                    {
                        var products = _context.PharmacyProducts.OrderBy(p=>p.price).ToList();
                        pagination.CalculateTotalPages(products.Count());
                        pagination.CalculateStartItem();
                        var pagedProducts = products.Skip(pagination.StartItem).Take(pagination.PageSize);
                        return View(new Tuple<IEnumerable<PharmacyProduct>, Pagination>(pagedProducts, pagination));


                    }
                    break;
                    case "price_high_to_low":
                    if (!string.IsNullOrEmpty(searchTerm))
                    {

                        var products = _context.PharmacyProducts.OrderByDescending(p => p.price).Where(p => p.productName.Contains(searchTerm));

                        pagination.CalculateTotalPages(products.Count());
                        pagination.CalculateStartItem();
                        var pagedProducts = products.Skip(pagination.StartItem).Take(pagination.PageSize);
                        return View(new Tuple<IEnumerable<PharmacyProduct>, Pagination>(pagedProducts, pagination));
                    }
                    else
                    {
                        var products = _context.PharmacyProducts.OrderByDescending(p => p.price).ToList();
                        pagination.CalculateTotalPages(products.Count());
                        pagination.CalculateStartItem();
                        var pagedProducts = products.Skip(pagination.StartItem).Take(pagination.PageSize);
                        return View(new Tuple<IEnumerable<PharmacyProduct>, Pagination>(pagedProducts, pagination));


                    }
                    break;
                    default:
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
                    break;

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
                    _context.ShoppingCarts.Where(u => u.UserId == claim.Value).ToList().Count());
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
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var feedfromdb = _context.userFeedbacks.Include(u => u.ApplicationUser).ToList();


            if (claim != null)
            {


                User_DoctorVm user_doc = new User_DoctorVm()
                {

                 
                    UsersFeedback = _context.userFeedbacks.Include(u=>u.ApplicationUser).ToList(),
                    DoctorsVM = _context.Doctors.ToList()


                };
                foreach (var item in feedfromdb)
                {

                    user_doc._userFeed=item;
                }
                //user_doc._userFeed.Message = "Write your feedback...";
                var doctors = _context.Doctors.ToList();
                var doctorsVisible = doctors.Where(u => u.isVisible == true).ToList();

                ViewBag.AppointmentsDoc1 = _context.appointmentsList.Where(u => u.DoctorId == doctorsVisible.First().DoctorId).Count();
                ViewBag.AppointmentsDoc2 = _context.appointmentsList.Where(u => u.DoctorId == doctorsVisible[1].DoctorId).Count();
                ViewBag.AppointmentsDoc3 = _context.appointmentsList.Where(u => u.DoctorId == doctorsVisible[2].DoctorId).Count();


                return View(user_doc);
            }
            else
            {


                User_DoctorVm user_doc = new User_DoctorVm()
                {

                    _userFeed = _context.userFeedbacks.FirstOrDefault(u => u.id == 1),
                    UsersFeedback = _context.userFeedbacks.ToList(),
                    DoctorsVM = _context.Doctors.ToList()


                };
                var doctors = _context.Doctors.ToList();
                var doctorsVisible = doctors.Where(u => u.isVisible == true).ToList();

                ViewBag.AppointmentsDoc1 = _context.appointmentsList.Where(u => u.DoctorId == doctorsVisible.First().DoctorId).Count();
                ViewBag.AppointmentsDoc2 = _context.appointmentsList.Where(u => u.DoctorId == doctorsVisible[1].DoctorId).Count();
                ViewBag.AppointmentsDoc3 = _context.appointmentsList.Where(u => u.DoctorId == doctorsVisible[2].DoctorId).Count();


                return View(user_doc);
            }


        }
        [HttpPost]
        [Authorize]
        [ActionName("Index")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Indexed(User_DoctorVm userFeedback)
        {
            UserFeedback uf = new UserFeedback();
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var feedback = _context.userFeedbacks.FirstOrDefault(u => u.UserId == claim.Value);
            uf.Message = userFeedback._userFeed.Message;
            uf.UserId = claim.Value;
            _context.userFeedbacks.Add(uf);
            _context.SaveChanges();
           
            
            return RedirectToAction("Index","Home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubAsync()
        {
            var emailer = Request.Form["Emailsub"];
            if (string.IsNullOrEmpty(emailer))
            {

            }
            else
            {
                await _emailSender.SendEmailAsync(emailer, $"Thanks for Subscribing", "<p>You have been subscribed</p>");
            }
            return RedirectToAction("Index", "Home");
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
        public async Task<IActionResult> FeedBacks()
        {
            var feedsFromDb = _context.userFeedbacks.Include(u=>u.ApplicationUser).ToList();
            return View(feedsFromDb);

        }
        }
    }
using Hospital_Managment.Data;
using Hospital_Managment.Models;
using Hospital_Managment.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace Hospital_Managment.Areas.Dashboard.Controllers
{

    [Area("Dashboard")]
    [Authorize(Roles ="Admin")]
    public class DashboardController : Controller

    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        // GET: HomeController
        public ActionResult Index()
        {
            ViewBag.TotalDoctors = _context.Doctors.Count();
            var user = _userManager.GetUsersInRoleAsync("Customer");
            ViewBag.TotalPatients = user.Result.Count();
            ViewBag.TotalAppointments = _context.appointmentsList.Count();
            ViewBag.TotalDepartments = _context.Departments.Count();
            return View("~/Areas/Admin/Views/Dashboard/Index.cshtml");

        }
        //public async Task<IActionResult> Users()
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    var email = await _userManager.GetEmailAsync(user);
        //    var userName = await _userManager.GetUserNameAsync(user);
        //    var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
        //    var profileViewModel = new ProfileViewModel
        //    {
        //        Email = email,
        //        Username = userName,
        //        PhoneNumber = phoneNumber,
        //        FullName = user.Name,
        //        fullAdress = user.StreetAdress + user.City + user.State + user.PostalCode,
        //        ImageUrl = user.ImageUrl
        //    };


        //    return View(profileViewModel);
        //    //public async ActionResult Users()
        //    //{
        //    //    var user = await _appUser.GetUserAsync(User);

        //    //}
        //}
        // GET: HomeController/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeController/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }


        // POST: HomeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


    }
}

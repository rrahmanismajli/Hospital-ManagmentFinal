using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital_Managment.Data;
using Hospital_Managment.Models;
using Hospital_Managment.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Hospital_Managment.Utilities;

namespace Hospital_Managment.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public PatientsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Patients
        public async Task<IActionResult> Index()
        {
            return View();
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Patients == null)
            {
                return NotFound();
            }

            var patient = await _context.Patients
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }
     
        // GET: Patients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,DateOfBirth,Gender,PhoneNumber,Email,Address,InsuranceProvider,PrimaryCarePhysician")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null || _context.ApplicationUsers == null)
            {
                return NotFound();
            }

            var patient = await _context.ApplicationUsers.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id,ApplicationUser patient)
        {
            if (id.ToString()!=patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ApplicationUsers == null)
            {
                return NotFound();
            }

            var patient = await _context.ApplicationUsers
                .FirstOrDefaultAsync(m => m.Id == id.ToString());
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Patients == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Patients'  is null.");
            }
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                _context.Patients.Remove(patient);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(string id)
        {
            return _context.ApplicationUsers.Any(e => e.Id == id);
        }
        #region API CAllS
        [HttpGet]
        public IActionResult GetAll()
        {

            var patients = _context.Patients.ToList();
            return Json(new { data = patients });

        }
        public IActionResult GetAllUsers(string status)
        {

            var patients = _context.ApplicationUsers.ToList();
            var results = new List<Object>();

            var administrators = _userManager.GetUsersInRoleAsync(RolesStrings.Role_User_Admin).Result.ToList();
            var customers = _userManager.GetUsersInRoleAsync(RolesStrings.Role_User_Customer).Result.ToList();
            switch (status) {
                case "administrators":
                    foreach (var item in administrators)
                    {
                        var roles = _userManager.GetRolesAsync(item);
                        var role = roles.Result.Last();
                        results.Add(new { item.Id, item.Name, item.StreetAdress, item.City, item.ImageUrl, role, item.Email, item.PhoneNumber });


                    }
                    break;
                case "customers":
                    foreach (var item in customers)
                    {
                        var roles = _userManager.GetRolesAsync(item);
                        var role = roles.Result.Last();
                        results.Add(new { item.Id, item.Name, item.StreetAdress, item.City, item.ImageUrl, role, item.Email, item.PhoneNumber });


                    }
                    break;
                default:
                    foreach (var item in patients)
            {
                var roles = _userManager.GetRolesAsync(item);
                var role = roles.Result.Last();
                results.Add(new { item.Id, item.Name, item.StreetAdress, item.City, item.ImageUrl, role, item.Email, item.PhoneNumber });


            }break;

        }


            return Json(new { data = results });

        }

        #endregion
    }
}

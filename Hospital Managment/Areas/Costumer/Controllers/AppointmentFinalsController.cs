using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital_Managment.Data;
using Hospital_Managment.Models;
using Hospital_Managment.Utilities;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Hospital_Managment.Areas.Costumer.Controllers
{
    [Area("Costumer")]
    [Authorize]
    public class AppointmentFinalsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentFinalsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Costumer/AppointmentFinals
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.AppointmentFinal.Include(a => a.ApplicationUser).Include(a => a.Doctor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Costumer/AppointmentFinals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AppointmentFinal == null)
            {
                return NotFound();
            }

            var appointmentFinal = await _context.AppointmentFinal
                .Include(a => a.ApplicationUser)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointmentFinal == null)
            {
                return NotFound();
            }

            return View(appointmentFinal);
        }

        // GET: Costumer/AppointmentFinals/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "FirstName");
            return View();
        }

        // POST: Costumer/AppointmentFinals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,PhoneNumber,Email,ReasonForVisit,Notes,DoctorId,UserId")] AppointmentFinal appointmentFinal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointmentFinal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", appointmentFinal.UserId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "FirstName", appointmentFinal.DoctorId);
            return View(appointmentFinal);
        }

        // GET: Costumer/AppointmentFinals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AppointmentFinal == null)
            {
                return NotFound();
            }

            var appointmentFinal = await _context.AppointmentFinal.FindAsync(id);
            if (appointmentFinal == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", appointmentFinal.UserId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Firstname ", appointmentFinal.DoctorId);
            return View(appointmentFinal);
        }

        // POST: Costumer/AppointmentFinals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,PhoneNumber,Email,ReasonForVisit,Notes,DoctorId,UserId")] AppointmentFinal appointmentFinal)
        {
            if (id != appointmentFinal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointmentFinal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentFinalExists(appointmentFinal.Id))
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
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", appointmentFinal.UserId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "FirstName", appointmentFinal.DoctorId);
            return View(appointmentFinal);
        }

        // GET: Costumer/AppointmentFinals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AppointmentFinal == null)
            {
                return NotFound();
            }

            var appointmentFinal = await _context.AppointmentFinal
                .Include(a => a.ApplicationUser)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointmentFinal == null)
            {
                return NotFound();
            }

            return View(appointmentFinal);
        }

        // POST: Costumer/AppointmentFinals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AppointmentFinal == null)
            {
                return Problem("Entity set 'ApplicationDbContext.AppointmentFinal'  is null.");
            }
            var appointmentFinal = await _context.AppointmentFinal.FindAsync(id);
            if (appointmentFinal != null)
            {
                _context.AppointmentFinal.Remove(appointmentFinal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentFinalExists(int id)
        {
          return _context.AppointmentFinal.Any(e => e.Id == id);
        }
        #region API CAllS
        [HttpGet]
        public IActionResult GetAll(string status)
        {
            IEnumerable<AppointmentFinal> appointmentFinals;
            if (User.IsInRole(RolesStrings.Role_User_Admin))
            {
                appointmentFinals = _context.AppointmentFinal.Include(u => u.ApplicationUser).ToList();
            }
            else
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                appointmentFinals = _context.AppointmentFinal.Where(u => u.UserId == claim.Value).Include(u => u.ApplicationUser).ToList();
            }


       
            return Json(new { data = appointmentFinals});

        }

       #endregion
    }
}

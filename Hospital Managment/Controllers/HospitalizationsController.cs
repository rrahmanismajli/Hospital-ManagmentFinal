using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital_Managment.Data;
using Hospital_Managment.Models;

namespace Hospital_Managment.Controllers
{
    public class HospitalizationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HospitalizationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hospitalizations
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Hospitalizations.Include(h => h.Doctor).Include(h => h.Patient);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Hospitalizations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Hospitalizations == null)
            {
                return NotFound();
            }

            var hospitalization = await _context.Hospitalizations
                .Include(h => h.Doctor)
                .Include(h => h.Patient)
                .FirstOrDefaultAsync(m => m.HospitalizationId == id);
            if (hospitalization == null)
            {
                return NotFound();
            }

            return View(hospitalization);
        }

        // GET: Hospitalizations/Create
        public IActionResult Create()
        {
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "FirstName");
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Id");
            return View();
        }

        // POST: Hospitalizations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HospitalizationId,PatientId,DoctorId,RoomNumber,StartDate,EndDate")] Hospitalization hospitalization)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hospitalization);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "FirstName", hospitalization.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Id", hospitalization.PatientId);
            return View(hospitalization);
        }

        // GET: Hospitalizations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Hospitalizations == null)
            {
                return NotFound();
            }

            var hospitalization = await _context.Hospitalizations.FindAsync(id);
            if (hospitalization == null)
            {
                return NotFound();
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "FirstName", hospitalization.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Id", hospitalization.PatientId);
            return View(hospitalization);
        }

        // POST: Hospitalizations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HospitalizationId,PatientId,DoctorId,RoomNumber,StartDate,EndDate")] Hospitalization hospitalization)
        {
            if (id != hospitalization.HospitalizationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hospitalization);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HospitalizationExists(hospitalization.HospitalizationId))
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
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "FirstName", hospitalization.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Id", hospitalization.PatientId);
            return View(hospitalization);
        }

        // GET: Hospitalizations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Hospitalizations == null)
            {
                return NotFound();
            }

            var hospitalization = await _context.Hospitalizations
                .Include(h => h.Doctor)
                .Include(h => h.Patient)
                .FirstOrDefaultAsync(m => m.HospitalizationId == id);
            if (hospitalization == null)
            {
                return NotFound();
            }

            return View(hospitalization);
        }

        // POST: Hospitalizations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Hospitalizations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Hospitalizations'  is null.");
            }
            var hospitalization = await _context.Hospitalizations.FindAsync(id);
            if (hospitalization != null)
            {
                _context.Hospitalizations.Remove(hospitalization);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HospitalizationExists(int id)
        {
          return _context.Hospitalizations.Any(e => e.HospitalizationId == id);
        }
    }
}

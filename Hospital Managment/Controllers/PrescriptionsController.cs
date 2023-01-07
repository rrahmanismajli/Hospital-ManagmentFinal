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

namespace Hospital_Managment.Controllers
{
    public class PrescriptionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrescriptionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Prescriptions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Prescriptions.Include(p => p.Appointment).Include(p => p.Doctor).Include(p => p.Patient);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Prescriptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Prescriptions == null)
            {
                return NotFound();
            }

            var prescription = await _context.Prescriptions
                .Include(p => p.Appointment)
                .Include(p => p.Doctor)
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prescription == null)
            {
                return NotFound();
            }

            return View(prescription);
        }

        // GET: Prescriptions/Create
        public IActionResult Create()
        {
            List<SelectListItem> doctor = _context.Doctors.Select(x => new SelectListItem { Value = x.DoctorId.ToString(), Text = x.FirstName }).ToList();
            ViewBag.Doctor=doctor;
            List<SelectListItem>patient=_context.Patients.Select(x=>new SelectListItem { Value=x.Id.ToString(),Text=x.FirstName}).ToList();
            ViewBag.Patient = patient;
            return View();
            

        }

        // POST: Prescriptions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PrescriptionViewModel prescription)
        {
            Prescription insertPrescription = new Prescription
            {
                DoctorId = prescription.DoctorId,
                PatientId = prescription.PatientId,
                MedicineName = prescription.MedicineName,
                Dosage= prescription.Dosage,
                Frequency=prescription.Frequency,
                Duration=prescription.Duration,
                Notes=prescription.Notes,

                
            };
            _context.Add(insertPrescription);
            await _context.SaveChangesAsync();



            return RedirectToAction(nameof(Index));
        }

        // GET: Prescriptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Prescriptions == null)
            {
                return NotFound();
            }

            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription == null)
            {
                return NotFound();
            }
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "AppointmentId", "AppointmentId", prescription.AppointmentId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Address", prescription.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Address", prescription.PatientId);
            return View(prescription);
        }

        // POST: Prescriptions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PatientId,DoctorId,AppointmentId,MedicineName,Dosage,Frequency,Duration,Notes")] Prescription prescription)
        {
            if (id != prescription.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prescription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrescriptionExists(prescription.Id))
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
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "AppointmentId", "AppointmentId", prescription.AppointmentId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Address", prescription.DoctorId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Address", prescription.PatientId);
            return View(prescription);
        }

        // GET: Prescriptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Prescriptions == null)
            {
                return NotFound();
            }

            var prescription = await _context.Prescriptions
                .Include(p => p.Appointment)
                .Include(p => p.Doctor)
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (prescription == null)
            {
                return NotFound();
            }

            return View(prescription);
        }

        // POST: Prescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Prescriptions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Prescriptions'  is null.");
            }
            var prescription = await _context.Prescriptions.FindAsync(id);
            if (prescription != null)
            {
                _context.Prescriptions.Remove(prescription);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrescriptionExists(int id)
        {
          return _context.Prescriptions.Any(e => e.Id == id);
        }
    }
}

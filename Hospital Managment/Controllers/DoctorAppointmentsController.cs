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
    public class DoctorAppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DoctorAppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DoctorAppointments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.DoctorAppointments.Include(d => d.Appointment).Include(d => d.Doctor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: DoctorAppointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DoctorAppointments == null)
            {
                return NotFound();
            }

            var doctorAppointment = await _context.DoctorAppointments
                .Include(d => d.Appointment)
                .Include(d => d.Doctor)
                .FirstOrDefaultAsync(m => m.DoctorId == id);
            if (doctorAppointment == null)
            {
                return NotFound();
            }

            return View(doctorAppointment);
        }

        // GET: DoctorAppointments/Create
        public IActionResult Create()
        {
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "AppointmentId", "AppointmentId");
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "FirstName");
            return View();
        }

        // POST: DoctorAppointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DoctorId,AppointmentId")] DoctorAppointment doctorAppointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctorAppointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "AppointmentId", "AppointmentId", doctorAppointment.AppointmentId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "FirstName", doctorAppointment.DoctorId);
            return View(doctorAppointment);
        }

        // GET: DoctorAppointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DoctorAppointments == null)
            {
                return NotFound();
            }

            var doctorAppointment = await _context.DoctorAppointments.FindAsync(id);
            if (doctorAppointment == null)
            {
                return NotFound();
            }
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "AppointmentId", "AppointmentId", doctorAppointment.AppointmentId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "FirstName", doctorAppointment.DoctorId);
            return View(doctorAppointment);
        }

        // POST: DoctorAppointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DoctorId,AppointmentId")] DoctorAppointment doctorAppointment)
        {
            if (id != doctorAppointment.DoctorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctorAppointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorAppointmentExists(doctorAppointment.DoctorId))
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
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "AppointmentId", "AppointmentId", doctorAppointment.AppointmentId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "FirstName", doctorAppointment.DoctorId);
            return View(doctorAppointment);
        }

        // GET: DoctorAppointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DoctorAppointments == null)
            {
                return NotFound();
            }

            var doctorAppointment = await _context.DoctorAppointments
                .Include(d => d.Appointment)
                .Include(d => d.Doctor)
                .FirstOrDefaultAsync(m => m.DoctorId == id);
            if (doctorAppointment == null)
            {
                return NotFound();
            }

            return View(doctorAppointment);
        }

        // POST: DoctorAppointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DoctorAppointments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DoctorAppointments'  is null.");
            }
            var doctorAppointment = await _context.DoctorAppointments.FindAsync(id);
            if (doctorAppointment != null)
            {
                _context.DoctorAppointments.Remove(doctorAppointment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorAppointmentExists(int id)
        {
          return _context.DoctorAppointments.Any(e => e.DoctorId == id);
        }
    }
}

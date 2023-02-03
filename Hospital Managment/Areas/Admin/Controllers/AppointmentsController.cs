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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Hospital_Managment.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin")]
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        public AppointmentsController(ApplicationDbContext context,IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender; 
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.appointmentsList.Include(a => a.Doctor).Include(a => a.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.appointmentsList == null)
            {
                return NotFound();
            }

            var appointments = await _context.appointmentsList
                .Include(a => a.ApplicationUser)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointments == null)
            {
                return NotFound();
            }

            return View(appointments);
        }


        // GET: Appointments/Create
        public IActionResult Create()
        {
            List<SelectListItem> doctor=_context.Doctors.Select(x =>new SelectListItem { Value =x.DoctorId.ToString(),Text=x.FirstName+x.LastName }).ToList();
            ViewBag.Doctor = doctor;
            List<SelectListItem> nurse = _context.Nurses.Select(x => new SelectListItem { Value = x.NurseId.ToString(), Text = x.FirstName }).ToList();
            ViewBag.Nurse = nurse;
            List<SelectListItem> patient = _context.Patients.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.FirstName+x.LastName }).ToList();
            ViewBag.Patient = patient;
            List<SelectListItem> receptionist = _context.Receptionists.Select(x => new SelectListItem { Value = x.ReceptionistId.ToString(), Text = x.FirstName+x.LastName }).ToList();
            ViewBag.Receptionist = receptionist;
            return View();
           
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentViewModel appointment)
        {
            Appointment insertAppointment = new Appointment
            {
                AppointmentId = appointment.AppointmentId,
                PatientId = appointment.PatientId,
                DoctorId = appointment.DoctorId,
                DateTime = DateTime.Now,
                NurseId = appointment.NurseId,
                Bill = appointment.Bill,
                ReceptionistId = appointment.ReceptionistId,
                ReasonForVisit = appointment.ReasonForVisit,
                Notes = appointment.Notes
            };
            _context.Add(insertAppointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        // GET: Appointments/Edit/5
        // GET: Costumer/Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.appointmentsList == null)
            {
                return NotFound();
            }

            var appointments = await _context.appointmentsList.FindAsync(id);
            if (appointments == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", appointments.UserId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Address", appointments.DoctorId);
            return View(appointments);
        }

        // POST: Costumer/Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( Appointments appointments)
        {

            if (ModelState.IsValid)
            {
                _context.appointmentsList.Update(appointments);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", appointments.UserId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Address", appointments.DoctorId);
            return View(appointments);
        }

        // GET: Costumer/Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.appointmentsList == null)
            {
                return NotFound();
            }

            var appointments = await _context.appointmentsList
                .Include(a => a.ApplicationUser)
                .Include(a => a.Doctor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (appointments == null)
            {
                return NotFound();
            }

            return View(appointments);
        }

        // POST: Costumer/Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.appointmentsList == null)
            {
                return Problem("Entity set 'ApplicationDbContext.appointmentsList'  is null.");
            }
            var appointments = await _context.appointmentsList.FindAsync(id);
            if (appointments != null)
            {
                var htmlMessageDoctor = $"<p>{appointments.FullName} Cancel Appointment on this date/time:{appointments.DateTimeOfAppointment}</p>";
                var doctorFromdb = _context.Doctors.FirstOrDefault(u => u.DoctorId == appointments.DoctorId);
                await _emailSender.SendEmailAsync(doctorFromdb.Email, $"Canceled Appointment from {appointments.FullName}", htmlMessageDoctor);
                _context.appointmentsList.Remove(appointments);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentsExists(int id)
        {
            return _context.appointmentsList.Any(e => e.Id == id);
        }
    }
}

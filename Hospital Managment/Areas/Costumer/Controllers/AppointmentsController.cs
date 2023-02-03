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
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Hospital_Managment.Areas.Costumer.Controllers
{
    [Area("Costumer")]
    [Authorize]
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        public AppointmentsController(ApplicationDbContext context,IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        // GET: Costumer/Appointments
        public async Task<IActionResult> Index()
        {
            ViewBag.OrderCount = _context.OrderHeader.Count();
            ViewBag.AppointmentCount = _context.appointmentsList.Count();
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var applicationDbContext = _context.appointmentsList.Where(u => u.UserId == claim.Value).Include(u => u.ApplicationUser).Include(a => a.Doctor).ToList();
            return View( applicationDbContext);
        }

        // GET: Costumer/Appointments/Details/5
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

        // GET: Costumer/Appointments/Create
        [ActionName("Appointment")]
        public IActionResult Create(Appointments appointments)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            appointments.UserId = claim.Value;
            var thisUser = _context.ApplicationUsers.FirstOrDefault(u => u.Id == appointments.UserId);
            appointments.FullName = thisUser.Name;
            appointments.email = thisUser.Email;
            appointments.PhoneNumber = thisUser.PhoneNumber;
            appointments.fullAdress = thisUser.StreetAdress + " " + thisUser.City + " " + thisUser.State + " " + thisUser.PostalCode;
            appointments.DateTimeOfAppointment = DateTime.Now;
   
            var doctorList = _context.Doctors.Select(d => new SelectListItem
            {
                Value = d.DoctorId.ToString(),
                Text = d.FirstName + " " + d.LastName
            }).ToList();
            ViewData["DoctorId"] = doctorList;
            return View(appointments);
        }

        // POST: Costumer/Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Created([Bind("Id,FullName,DateTimeOfAppointment,email,PhoneNumber,fullAdress,ReasonOfVisiting,DoctorId")] Appointments appointments)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            appointments.UserId = claim.Value;
            var doctorFromdb = _context.Doctors.FirstOrDefault(u => u.DoctorId == appointments.DoctorId);
            
             
            var appointmentsFromDB = _context.appointmentsList.Where(u=>u.DateTimeOfAppointment==appointments.DateTimeOfAppointment).FirstOrDefault();
            if (appointmentsFromDB != null)
            {
                ModelState.AddModelError("DateTimeOfAppointment", "This time slot is not available. Please choose another time.");
                return RedirectToAction(nameof(Index));
            }
            _context.Add(appointments);
            await _context.SaveChangesAsync();
            var htmlMessage = $"<p>Thank you For your Appointment</p>";
            var htmlMessageDoctor = $"<p>You got a new Appointment on this date/time:{appointments.DateTimeOfAppointment}</p>";
            await _emailSender.SendEmailAsync(appointments.email , $"New Message from {appointments.FullName}", htmlMessage);
            await _emailSender.SendEmailAsync(doctorFromdb.Email, $"New Appointment from {appointments.FullName}", htmlMessageDoctor);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Address", appointments.DoctorId);
            return RedirectToAction(nameof(Index));
        }

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
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,DateTimeOfAppointment,email,PhoneNumber,fullAdress,ReasonOfVisiting,DoctorId,UserId")] Appointments appointments)
        {
            if (id != appointments.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentsExists(appointments.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
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

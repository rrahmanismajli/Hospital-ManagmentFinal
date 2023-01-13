﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital_Managment.Data;
using Hospital_Managment.Models;
using Hospital_Managment.ViewModels;

namespace Hospital_Managment.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Appointments.Include(a => a.Doctor).Include(a => a.Nurse).Include(a => a.Patient).Include(a => a.Receptionist);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Nurse)
                .Include(a => a.Patient)
                .Include(a => a.Receptionist)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }


        // GET: Appointments/Create
        public IActionResult Create()
        {
            List<SelectListItem> doctor=_context.Doctors.Select(x =>new SelectListItem { Value =x.DoctorId.ToString(),Text=x.FirstName }).ToList();
            ViewBag.Doctor = doctor;
            List<SelectListItem> nurse = _context.Nurses.Select(x => new SelectListItem { Value = x.NurseId.ToString(), Text = x.FirstName }).ToList();
            ViewBag.Nurse = nurse;
            List<SelectListItem> receptionist = _context.Receptionists.Select(x => new SelectListItem { Value = x.ReceptionistId.ToString(), Text = x.FirstName }).ToList();
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
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "FirstName", appointment.DoctorId);
            ViewData["NurseId"] = new SelectList(_context.Nurses, "NurseId", "NurseId", appointment.NurseId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Id", appointment.PatientId);
            ViewData["ReceptionistId"] = new SelectList(_context.Receptionists, "ReceptionistId", "ReceptionistId", appointment.ReceptionistId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,PatientId,DoctorId,DateTime,NurseId,ReceptionistId,ReasonForVisit,Notes")] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentId))
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
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "FirstName", appointment.DoctorId);
            ViewData["NurseId"] = new SelectList(_context.Nurses, "NurseId", "NurseId", appointment.NurseId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Id", appointment.PatientId);
            ViewData["ReceptionistId"] = new SelectList(_context.Receptionists, "ReceptionistId", "ReceptionistId", appointment.ReceptionistId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Appointments == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.Doctor)
                .Include(a => a.Nurse)
                .Include(a => a.Patient)
                .Include(a => a.Receptionist)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Appointments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Appointments'  is null.");
            }
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.AppointmentId == id);
        }
    }
}

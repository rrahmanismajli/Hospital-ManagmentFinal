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
    public class TestResultsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TestResultsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TestResults
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TestResults.Include(t => t.Appointment).Include(t => t.Patient);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TestResults/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TestResults == null)
            {
                return NotFound();
            }

            var testResult = await _context.TestResults
                .Include(t => t.Appointment)
                .Include(t => t.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testResult == null)
            {
                return NotFound();
            }

            return View(testResult);
        }

        // GET: TestResults/Create
        public IActionResult Create()
        {
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "AppointmentId", "AppointmentId");
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Address");
            return View();
        }

        // POST: TestResults/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatientId,AppointmentId,TestName,Result,Notes")] TestResult testResult)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testResult);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "AppointmentId", "AppointmentId", testResult.AppointmentId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Address", testResult.PatientId);
            return View(testResult);
        }

        // GET: TestResults/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TestResults == null)
            {
                return NotFound();
            }

            var testResult = await _context.TestResults.FindAsync(id);
            if (testResult == null)
            {
                return NotFound();
            }
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "AppointmentId", "AppointmentId", testResult.AppointmentId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Address", testResult.PatientId);
            return View(testResult);
        }

        // POST: TestResults/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PatientId,AppointmentId,TestName,Result,Notes")] TestResult testResult)
        {
            if (id != testResult.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testResult);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestResultExists(testResult.Id))
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
            ViewData["AppointmentId"] = new SelectList(_context.Appointments, "AppointmentId", "AppointmentId", testResult.AppointmentId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Address", testResult.PatientId);
            return View(testResult);
        }

        // GET: TestResults/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TestResults == null)
            {
                return NotFound();
            }

            var testResult = await _context.TestResults
                .Include(t => t.Appointment)
                .Include(t => t.Patient)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (testResult == null)
            {
                return NotFound();
            }

            return View(testResult);
        }

        // POST: TestResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TestResults == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TestResults'  is null.");
            }
            var testResult = await _context.TestResults.FindAsync(id);
            if (testResult != null)
            {
                _context.TestResults.Remove(testResult);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestResultExists(int id)
        {
          return _context.TestResults.Any(e => e.Id == id);
        }
    }
}

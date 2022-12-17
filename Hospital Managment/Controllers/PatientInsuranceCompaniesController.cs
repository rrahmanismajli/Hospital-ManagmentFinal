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
    public class PatientInsuranceCompaniesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientInsuranceCompaniesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PatientInsuranceCompanies
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PatientInsuranceCompanies.Include(p => p.InsuranceCompany).Include(p => p.Patient);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PatientInsuranceCompanies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PatientInsuranceCompanies == null)
            {
                return NotFound();
            }

            var patientInsuranceCompany = await _context.PatientInsuranceCompanies
                .Include(p => p.InsuranceCompany)
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.PatientId == id);
            if (patientInsuranceCompany == null)
            {
                return NotFound();
            }

            return View(patientInsuranceCompany);
        }

        // GET: PatientInsuranceCompanies/Create
        public IActionResult Create()
        {
            ViewData["InsuranceCompanyId"] = new SelectList(_context.InsuranceCompanies, "InsuranceCompanyId", "InsuranceCompanyId");
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Id");
            return View();
        }

        // POST: PatientInsuranceCompanies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientId,InsuranceCompanyId")] PatientInsuranceCompany patientInsuranceCompany)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patientInsuranceCompany);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["InsuranceCompanyId"] = new SelectList(_context.InsuranceCompanies, "InsuranceCompanyId", "InsuranceCompanyId", patientInsuranceCompany.InsuranceCompanyId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Id", patientInsuranceCompany.PatientId);
            return View(patientInsuranceCompany);
        }

        // GET: PatientInsuranceCompanies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PatientInsuranceCompanies == null)
            {
                return NotFound();
            }

            var patientInsuranceCompany = await _context.PatientInsuranceCompanies.FindAsync(id);
            if (patientInsuranceCompany == null)
            {
                return NotFound();
            }
            ViewData["InsuranceCompanyId"] = new SelectList(_context.InsuranceCompanies, "InsuranceCompanyId", "InsuranceCompanyId", patientInsuranceCompany.InsuranceCompanyId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Id", patientInsuranceCompany.PatientId);
            return View(patientInsuranceCompany);
        }

        // POST: PatientInsuranceCompanies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientId,InsuranceCompanyId")] PatientInsuranceCompany patientInsuranceCompany)
        {
            if (id != patientInsuranceCompany.PatientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patientInsuranceCompany);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientInsuranceCompanyExists(patientInsuranceCompany.PatientId))
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
            ViewData["InsuranceCompanyId"] = new SelectList(_context.InsuranceCompanies, "InsuranceCompanyId", "InsuranceCompanyId", patientInsuranceCompany.InsuranceCompanyId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "Id", "Id", patientInsuranceCompany.PatientId);
            return View(patientInsuranceCompany);
        }

        // GET: PatientInsuranceCompanies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PatientInsuranceCompanies == null)
            {
                return NotFound();
            }

            var patientInsuranceCompany = await _context.PatientInsuranceCompanies
                .Include(p => p.InsuranceCompany)
                .Include(p => p.Patient)
                .FirstOrDefaultAsync(m => m.PatientId == id);
            if (patientInsuranceCompany == null)
            {
                return NotFound();
            }

            return View(patientInsuranceCompany);
        }

        // POST: PatientInsuranceCompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PatientInsuranceCompanies == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PatientInsuranceCompanies'  is null.");
            }
            var patientInsuranceCompany = await _context.PatientInsuranceCompanies.FindAsync(id);
            if (patientInsuranceCompany != null)
            {
                _context.PatientInsuranceCompanies.Remove(patientInsuranceCompany);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientInsuranceCompanyExists(int id)
        {
          return _context.PatientInsuranceCompanies.Any(e => e.PatientId == id);
        }
    }
}

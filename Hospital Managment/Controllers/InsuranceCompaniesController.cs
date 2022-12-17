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
    public class InsuranceCompaniesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InsuranceCompaniesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InsuranceCompanies
        public async Task<IActionResult> Index()
        {
              return View(await _context.InsuranceCompanies.ToListAsync());
        }

        // GET: InsuranceCompanies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.InsuranceCompanies == null)
            {
                return NotFound();
            }

            var insuranceCompany = await _context.InsuranceCompanies
                .FirstOrDefaultAsync(m => m.InsuranceCompanyId == id);
            if (insuranceCompany == null)
            {
                return NotFound();
            }

            return View(insuranceCompany);
        }

        // GET: InsuranceCompanies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InsuranceCompanies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("InsuranceCompanyId,Name,PhoneNumber,Email")] InsuranceCompany insuranceCompany)
        {
            if (ModelState.IsValid)
            {
                _context.Add(insuranceCompany);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(insuranceCompany);
        }

        // GET: InsuranceCompanies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.InsuranceCompanies == null)
            {
                return NotFound();
            }

            var insuranceCompany = await _context.InsuranceCompanies.FindAsync(id);
            if (insuranceCompany == null)
            {
                return NotFound();
            }
            return View(insuranceCompany);
        }

        // POST: InsuranceCompanies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("InsuranceCompanyId,Name,PhoneNumber,Email")] InsuranceCompany insuranceCompany)
        {
            if (id != insuranceCompany.InsuranceCompanyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(insuranceCompany);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InsuranceCompanyExists(insuranceCompany.InsuranceCompanyId))
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
            return View(insuranceCompany);
        }

        // GET: InsuranceCompanies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.InsuranceCompanies == null)
            {
                return NotFound();
            }

            var insuranceCompany = await _context.InsuranceCompanies
                .FirstOrDefaultAsync(m => m.InsuranceCompanyId == id);
            if (insuranceCompany == null)
            {
                return NotFound();
            }

            return View(insuranceCompany);
        }

        // POST: InsuranceCompanies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.InsuranceCompanies == null)
            {
                return Problem("Entity set 'ApplicationDbContext.InsuranceCompanies'  is null.");
            }
            var insuranceCompany = await _context.InsuranceCompanies.FindAsync(id);
            if (insuranceCompany != null)
            {
                _context.InsuranceCompanies.Remove(insuranceCompany);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InsuranceCompanyExists(int id)
        {
          return _context.InsuranceCompanies.Any(e => e.InsuranceCompanyId == id);
        }
    }
}

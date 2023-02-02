using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital_Managment.Data;
using Hospital_Managment.Models;

namespace Hospital_Managment.Areas.Costumer.Controllers
{
    [Area("Costumer")]
    public class TakimisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TakimisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Costumer/Takimis
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Takimi.Include(t => t.ApplicationUser).Include(t => t.Doctor);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Costumer/Takimis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Takimi == null)
            {
                return NotFound();
            }

            var takimi = await _context.Takimi
                .Include(t => t.ApplicationUser)
                .Include(t => t.Doctor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (takimi == null)
            {
                return NotFound();
            }

            return View(takimi);
        }

        // GET: Costumer/Takimis/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "FirstName");
            return View();
        }

        // POST: Costumer/Takimis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,PhoneNumber,Email,ReasonForVisit,Notes,DoctorId,UserId")] Takimi takimi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(takimi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", takimi.UserId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "FirstName", takimi.DoctorId);
            return View(takimi);
        }

        // GET: Costumer/Takimis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Takimi == null)
            {
                return NotFound();
            }

            var takimi = await _context.Takimi.FindAsync(id);
            if (takimi == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", takimi.UserId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "Address", takimi.DoctorId);
            return View(takimi);
        }

        // POST: Costumer/Takimis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FullName,PhoneNumber,Email,ReasonForVisit,Notes,DoctorId,UserId")] Takimi takimi)
        {
            if (id != takimi.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(takimi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TakimiExists(takimi.Id))
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
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", takimi.UserId);
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "DoctorId", "FirstName", takimi.DoctorId);
            return View(takimi);
        }

        // GET: Costumer/Takimis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Takimi == null)
            {
                return NotFound();
            }

            var takimi = await _context.Takimi
                .Include(t => t.ApplicationUser)
                .Include(t => t.Doctor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (takimi == null)
            {
                return NotFound();
            }

            return View(takimi);
        }

        // POST: Costumer/Takimis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Takimi == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Takimi'  is null.");
            }
            var takimi = await _context.Takimi.FindAsync(id);
            if (takimi != null)
            {
                _context.Takimi.Remove(takimi);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TakimiExists(int id)
        {
          return _context.Takimi.Any(e => e.Id == id);
        }
    }
}

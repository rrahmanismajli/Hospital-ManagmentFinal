using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hospital_Managment.Data;
using Hospital_Managment.Models;

namespace Hospital_Managment.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserFeedbacksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserFeedbacksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/UserFeedbacks
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.userFeedbacks.Include(u => u.ApplicationUser);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/UserFeedbacks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.userFeedbacks == null)
            {
                return NotFound();
            }

            var userFeedback = await _context.userFeedbacks
                .Include(u => u.ApplicationUser)
                .FirstOrDefaultAsync(m => m.id == id);
            if (userFeedback == null)
            {
                return NotFound();
            }

            return View(userFeedback);
        }

        // GET: Admin/UserFeedbacks/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id");
            return View();
        }

        // POST: Admin/UserFeedbacks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Message,UserId")] UserFeedback userFeedback)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userFeedback);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", userFeedback.UserId);
            return View(userFeedback);
        }

        // GET: Admin/UserFeedbacks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.userFeedbacks == null)
            {
                return NotFound();
            }

            var userFeedback = await _context.userFeedbacks.FindAsync(id);
            if (userFeedback == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", userFeedback.UserId);
            return View(userFeedback);
        }

        // POST: Admin/UserFeedbacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Message,UserId")] UserFeedback userFeedback)
        {
            if (id != userFeedback.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userFeedback);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserFeedbackExists(userFeedback.id))
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
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", userFeedback.UserId);
            return View(userFeedback);
        }

        // GET: Admin/UserFeedbacks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.userFeedbacks == null)
            {
                return NotFound();
            }

            var userFeedback = await _context.userFeedbacks
                .Include(u => u.ApplicationUser)
                .FirstOrDefaultAsync(m => m.id == id);
            if (userFeedback == null)
            {
                return NotFound();
            }

            return View(userFeedback);
        }

        // POST: Admin/UserFeedbacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.userFeedbacks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.userFeedbacks'  is null.");
            }
            var userFeedback = await _context.userFeedbacks.FindAsync(id);
            if (userFeedback != null)
            {
                _context.userFeedbacks.Remove(userFeedback);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserFeedbackExists(int id)
        {
          return _context.userFeedbacks.Any(e => e.id == id);
        }
    }
}

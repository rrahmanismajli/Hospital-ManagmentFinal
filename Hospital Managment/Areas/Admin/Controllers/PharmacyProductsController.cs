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
    public class PharmacyProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PharmacyProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/PharmacyProducts
        public async Task<IActionResult> Index()
        {
              return View(await _context.PharmacyProducts.ToListAsync());
        }

        // GET: Admin/PharmacyProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PharmacyProducts == null)
            {
                return NotFound();
            }

            var pharmacyProduct = await _context.PharmacyProducts
                .FirstOrDefaultAsync(m => m.productId == id);
            if (pharmacyProduct == null)
            {
                return NotFound();
            }

            return View(pharmacyProduct);
        }

        // GET: Admin/PharmacyProducts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/PharmacyProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("productId,productName,BestBeforeDate,ImagePath,price,description,quantity,shipped,OriginFlagUrl")] PharmacyProduct pharmacyProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pharmacyProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pharmacyProduct);
        }

        // GET: Admin/PharmacyProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PharmacyProducts == null)
            {
                return NotFound();
            }

            var pharmacyProduct = await _context.PharmacyProducts.FindAsync(id);
            if (pharmacyProduct == null)
            {
                return NotFound();
            }
            return View(pharmacyProduct);
        }

        // POST: Admin/PharmacyProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("productId,productName,BestBeforeDate,ImagePath,price,description,quantity,shipped,OriginFlagUrl")] PharmacyProduct pharmacyProduct)
        {
            if (id != pharmacyProduct.productId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pharmacyProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PharmacyProductExists(pharmacyProduct.productId))
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
            return View(pharmacyProduct);
        }

        // GET: Admin/PharmacyProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PharmacyProducts == null)
            {
                return NotFound();
            }

            var pharmacyProduct = await _context.PharmacyProducts
                .FirstOrDefaultAsync(m => m.productId == id);
            if (pharmacyProduct == null)
            {
                return NotFound();
            }

            return View(pharmacyProduct);
        }

        // POST: Admin/PharmacyProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PharmacyProducts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PharmacyProducts'  is null.");
            }
            var pharmacyProduct = await _context.PharmacyProducts.FindAsync(id);
            if (pharmacyProduct != null)
            {
                _context.PharmacyProducts.Remove(pharmacyProduct);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PharmacyProductExists(int id)
        {
          return _context.PharmacyProducts.Any(e => e.productId == id);
        }
    }
}

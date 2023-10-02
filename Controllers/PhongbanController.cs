using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using qlnv.Models;

namespace qlnv.Controllers
{
    public class PhongbanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhongbanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Phongban
        public async Task<IActionResult> Index()
        {
              return _context.Phongban != null ? 
                          View(await _context.Phongban.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Phongban'  is null.");
        }

        // GET: Phongban/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Phongban == null)
            {
                return NotFound();
            }

            var phongban = await _context.Phongban
                .FirstOrDefaultAsync(m => m.Mapb == id);
            if (phongban == null)
            {
                return NotFound();
            }

            return View(phongban);
        }

        // GET: Phongban/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Phongban/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mapb,Tenpb")] Phongban phongban)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phongban);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(phongban);
        }

        // GET: Phongban/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Phongban == null)
            {
                return NotFound();
            }

            var phongban = await _context.Phongban.FindAsync(id);
            if (phongban == null)
            {
                return NotFound();
            }
            return View(phongban);
        }

        // POST: Phongban/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Mapb,Tenpb")] Phongban phongban)
        {
            if (id != phongban.Mapb)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phongban);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhongbanExists(phongban.Mapb))
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
            return View(phongban);
        }

        // GET: Phongban/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Phongban == null)
            {
                return NotFound();
            }

            var phongban = await _context.Phongban
                .FirstOrDefaultAsync(m => m.Mapb == id);
            if (phongban == null)
            {
                return NotFound();
            }

            return View(phongban);
        }

        // POST: Phongban/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Phongban == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Phongban'  is null.");
            }
            var phongban = await _context.Phongban.FindAsync(id);
            if (phongban != null)
            {
                _context.Phongban.Remove(phongban);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhongbanExists(string id)
        {
          return (_context.Phongban?.Any(e => e.Mapb == id)).GetValueOrDefault();
        }
    }
}

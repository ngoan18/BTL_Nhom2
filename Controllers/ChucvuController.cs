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
    public class ChucvuController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChucvuController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Chucvu
        public async Task<IActionResult> Index()
        {
              return _context.Chucvu != null ? 
                          View(await _context.Chucvu.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Chucvu'  is null.");
        }

        // GET: Chucvu/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Chucvu == null)
            {
                return NotFound();
            }

            var chucvu = await _context.Chucvu
                .FirstOrDefaultAsync(m => m.Macv == id);
            if (chucvu == null)
            {
                return NotFound();
            }

            return View(chucvu);
        }

        // GET: Chucvu/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Chucvu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Macv,Tencv")] Chucvu chucvu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chucvu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chucvu);
        }

        // GET: Chucvu/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Chucvu == null)
            {
                return NotFound();
            }

            var chucvu = await _context.Chucvu.FindAsync(id);
            if (chucvu == null)
            {
                return NotFound();
            }
            return View(chucvu);
        }

        // POST: Chucvu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Macv,Tencv")] Chucvu chucvu)
        {
            if (id != chucvu.Macv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chucvu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChucvuExists(chucvu.Macv))
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
            return View(chucvu);
        }

        // GET: Chucvu/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Chucvu == null)
            {
                return NotFound();
            }

            var chucvu = await _context.Chucvu
                .FirstOrDefaultAsync(m => m.Macv == id);
            if (chucvu == null)
            {
                return NotFound();
            }

            return View(chucvu);
        }

        // POST: Chucvu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Chucvu == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Chucvu'  is null.");
            }
            var chucvu = await _context.Chucvu.FindAsync(id);
            if (chucvu != null)
            {
                _context.Chucvu.Remove(chucvu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChucvuExists(string id)
        {
          return (_context.Chucvu?.Any(e => e.Macv == id)).GetValueOrDefault();
        }
    }
}

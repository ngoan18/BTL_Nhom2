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
    public class TrinhdoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrinhdoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Trinhdo
        public async Task<IActionResult> Index()
        {
              return _context.Trinhdo != null ? 
                          View(await _context.Trinhdo.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Trinhdo'  is null.");
        }

        // GET: Trinhdo/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Trinhdo == null)
            {
                return NotFound();
            }

            var trinhdo = await _context.Trinhdo
                .FirstOrDefaultAsync(m => m.Matd == id);
            if (trinhdo == null)
            {
                return NotFound();
            }

            return View(trinhdo);
        }

        // GET: Trinhdo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trinhdo/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Matd,Tentd")] Trinhdo trinhdo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trinhdo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(trinhdo);
        }

        // GET: Trinhdo/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Trinhdo == null)
            {
                return NotFound();
            }

            var trinhdo = await _context.Trinhdo.FindAsync(id);
            if (trinhdo == null)
            {
                return NotFound();
            }
            return View(trinhdo);
        }

        // POST: Trinhdo/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Matd,Tentd")] Trinhdo trinhdo)
        {
            if (id != trinhdo.Matd)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trinhdo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrinhdoExists(trinhdo.Matd))
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
            return View(trinhdo);
        }

        // GET: Trinhdo/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Trinhdo == null)
            {
                return NotFound();
            }

            var trinhdo = await _context.Trinhdo
                .FirstOrDefaultAsync(m => m.Matd == id);
            if (trinhdo == null)
            {
                return NotFound();
            }

            return View(trinhdo);
        }

        // POST: Trinhdo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Trinhdo == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Trinhdo'  is null.");
            }
            var trinhdo = await _context.Trinhdo.FindAsync(id);
            if (trinhdo != null)
            {
                _context.Trinhdo.Remove(trinhdo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrinhdoExists(string id)
        {
          return (_context.Trinhdo?.Any(e => e.Matd == id)).GetValueOrDefault();
        }
    }
}

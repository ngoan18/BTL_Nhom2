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
    public class KhenThuongController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KhenThuongController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: KhenThuong
        public async Task<IActionResult> Index()
        {
              return _context.KhenThuong != null ? 
                          View(await _context.KhenThuong.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.KhenThuong'  is null.");
        }

        // GET: KhenThuong/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.KhenThuong == null)
            {
                return NotFound();
            }

            var khenThuong = await _context.KhenThuong
                .FirstOrDefaultAsync(m => m.MaKT == id);
            if (khenThuong == null)
            {
                return NotFound();
            }

            return View(khenThuong);
        }

        // GET: KhenThuong/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KhenThuong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaKT,TenKT")] KhenThuong khenThuong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(khenThuong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(khenThuong);
        }

        // GET: KhenThuong/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.KhenThuong == null)
            {
                return NotFound();
            }

            var khenThuong = await _context.KhenThuong.FindAsync(id);
            if (khenThuong == null)
            {
                return NotFound();
            }
            return View(khenThuong);
        }

        // POST: KhenThuong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaKT,TenKT")] KhenThuong khenThuong)
        {
            if (id != khenThuong.MaKT)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(khenThuong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhenThuongExists(khenThuong.MaKT))
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
            return View(khenThuong);
        }

        // GET: KhenThuong/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.KhenThuong == null)
            {
                return NotFound();
            }

            var khenThuong = await _context.KhenThuong
                .FirstOrDefaultAsync(m => m.MaKT == id);
            if (khenThuong == null)
            {
                return NotFound();
            }

            return View(khenThuong);
        }

        // POST: KhenThuong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.KhenThuong == null)
            {
                return Problem("Entity set 'ApplicationDbContext.KhenThuong'  is null.");
            }
            var khenThuong = await _context.KhenThuong.FindAsync(id);
            if (khenThuong != null)
            {
                _context.KhenThuong.Remove(khenThuong);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KhenThuongExists(string id)
        {
          return (_context.KhenThuong?.Any(e => e.MaKT == id)).GetValueOrDefault();
        }
    }
}

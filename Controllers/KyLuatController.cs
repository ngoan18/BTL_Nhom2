using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using qlnv.Models;
using X.PagedList;

namespace qlnv.Controllers
{
    public class KyLuatController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KyLuatController(ApplicationDbContext context)
        {
            _context = context;
        }
        //phan trang
        public async Task<IActionResult> Index(int? page)
        {
            var model = _context.KyLuat.ToList().ToPagedList(page ?? 1, 5);
            return View(model);
        }

        // GET: KyLuat
        /*public async Task<IActionResult> Index()
        {
              return _context.KyLuat != null ? 
                          View(await _context.KyLuat.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.KyLuat'  is null.");
        }*/

        // GET: KyLuat/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.KyLuat == null)
            {
                return NotFound();
            }

            var kyLuat = await _context.KyLuat
                .FirstOrDefaultAsync(m => m.MaKL == id);
            if (kyLuat == null)
            {
                return NotFound();
            }

            return View(kyLuat);
        }

        // GET: KyLuat/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KyLuat/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaKL,TenKL")] KyLuat kyLuat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kyLuat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kyLuat);
        }

        // GET: KyLuat/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.KyLuat == null)
            {
                return NotFound();
            }

            var kyLuat = await _context.KyLuat.FindAsync(id);
            if (kyLuat == null)
            {
                return NotFound();
            }
            return View(kyLuat);
        }

        // POST: KyLuat/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("MaKL,TenKL")] KyLuat kyLuat)
        {
            if (id != kyLuat.MaKL)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kyLuat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KyLuatExists(kyLuat.MaKL))
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
            return View(kyLuat);
        }

        // GET: KyLuat/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.KyLuat == null)
            {
                return NotFound();
            }

            var kyLuat = await _context.KyLuat
                .FirstOrDefaultAsync(m => m.MaKL == id);
            if (kyLuat == null)
            {
                return NotFound();
            }

            return View(kyLuat);
        }

        // POST: KyLuat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.KyLuat == null)
            {
                return Problem("Entity set 'ApplicationDbContext.KyLuat'  is null.");
            }
            var kyLuat = await _context.KyLuat.FindAsync(id);
            if (kyLuat != null)
            {
                _context.KyLuat.Remove(kyLuat);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KyLuatExists(string id)
        {
          return (_context.KyLuat?.Any(e => e.MaKL == id)).GetValueOrDefault();
        }
    }
}

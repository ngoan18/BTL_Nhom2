using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using OfficeOpenXml;
using qlnv.Models;
using X.PagedList;

namespace qlnv.Controllers
{
    public class KhenThuongController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KhenThuongController(ApplicationDbContext context)
        {
            _context = context;
        }

         public async Task<IActionResult> Index( int? page, int? PageSize )
        {
            ViewBag.PageSize = new List<SelectListItem>()
        {
            new SelectListItem() {Value="3", Text = "3"},
            new SelectListItem() {Value="5", Text = "5"},
            new SelectListItem() {Value="10", Text = "10"},
            new SelectListItem() {Value="15", Text = "15"},
            new SelectListItem() {Value="25", Text = "25"},


        };
        int pagesize = (PageSize ?? 3);
        ViewBag.psize = pagesize;
        var model = _context.KhenThuong.ToList().ToPagedList (page ?? 1, pagesize);
        return View (model);
        }

        /*
        // phan trang
        public async Task<IActionResult> Index(int? page)
        {
            var model = _context.KhenThuong.ToList().ToPagedList(page ?? 1, 5);
            return View(model);
        }

        // GET: KhenThuong
        /*public async Task<IActionResult> Index()
        {
              return _context.KhenThuong != null ? 
                          View(await _context.KhenThuong.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.KhenThuong'  is null.");
        }*/

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
            ViewData["Manv"] = new SelectList(_context.Nhanvien, "Manv", "Manv");
            return View();
        }

        // POST: KhenThuong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaKT,TenKT,Manv")] KhenThuong khenThuong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(khenThuong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Manv"] = new SelectList(_context.Nhanvien, "Manv" , "Manv", khenThuong.Manv);
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

         public IActionResult Download()
        {
            var fileName = "YourFileName" + ".xlsx";
            using (ExcelPackage excelPackage =new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                worksheet.Cells["A1"].Value = "MaKT";
                worksheet.Cells["B1"].Value = "TenKT";
                worksheet.Cells["C1"].Value = "Manv";

                var personList = _context.KhenThuong.ToList();
                worksheet.Cells["A2"].LoadFromCollection(personList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File (stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
    }
}

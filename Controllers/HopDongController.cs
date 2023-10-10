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
    public class HopDongController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HopDongController(ApplicationDbContext context)
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
        var model = _context.HopDong.ToList().ToPagedList (page ?? 1, pagesize);
        return View (model);
        }


         /*
        public async Task<IActionResult> Index(int? page)
        {
            var model = _context.HopDong.ToList().ToPagedList(page ?? 1, 5);
            return View(model);
        }

        // GET: HopDong
        public async Task<IActionResult> Index()
        {
              return _context.HopDong != null ? 
                          View(await _context.HopDong.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.HopDong'  is null.");
        }

        
        */

        // GET: HopDong/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.HopDong == null)
            {
                return NotFound();
            }

            var hopDong = await _context.HopDong
                .FirstOrDefaultAsync(m => m.Mahd == id);
            if (hopDong == null)
            {
                return NotFound();
            }

            return View(hopDong);
        }

        // GET: HopDong/Create
        public IActionResult Create()
        {
            ViewData["Manv"] = new SelectList(_context.Nhanvien, "Manv", "Hoten");
            return View();
        }

        // POST: HopDong/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mahd,Tenhd,NgayBatDau,NgayKetThuc,ThoiHan,Manv,Hoten")] HopDong hopDong)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hopDong);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Manv"] = new SelectList(_context.Nhanvien, "Manv" , "Hoten", hopDong.Manv);
            return View(hopDong);
        }

        // GET: HopDong/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.HopDong == null)
            {
                return NotFound();
            }

            var hopDong = await _context.HopDong.FindAsync(id);
            if (hopDong == null)
            {
                return NotFound();
            }
            return View(hopDong);
        }

        // POST: HopDong/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Mahd,Tenhd,NgayBatDau,NgayKetThuc,ThoiHan")] HopDong hopDong)
        {
            if (id != hopDong.Mahd)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hopDong);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HopDongExists(hopDong.Mahd))
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
            return View(hopDong);
        }

        // GET: HopDong/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.HopDong == null)
            {
                return NotFound();
            }

            var hopDong = await _context.HopDong
                .FirstOrDefaultAsync(m => m.Mahd == id);
            if (hopDong == null)
            {
                return NotFound();
            }

            return View(hopDong);
        }

        // POST: HopDong/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.HopDong == null)
            {
                return Problem("Entity set 'ApplicationDbContext.HopDong'  is null.");
            }
            var hopDong = await _context.HopDong.FindAsync(id);
            if (hopDong != null)
            {
                _context.HopDong.Remove(hopDong);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HopDongExists(string id)
        {
          return (_context.HopDong?.Any(e => e.Mahd == id)).GetValueOrDefault();
        }
         public IActionResult Download()
        {
            var fileName = "YourFileName" + ".xlsx";
            using (ExcelPackage excelPackage =new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                worksheet.Cells["A1"].Value = "Mahd";
                worksheet.Cells["B1"].Value = "Tenhd";
                worksheet.Cells["C1"].Value = "NgayBatDau";
                worksheet.Cells["D1"].Value = "NgayKetThuc";
                worksheet.Cells["E1"].Value = "ThoiHan";
                worksheet.Cells["F1"].Value = "Manv";

                var personList = _context.HopDong.ToList();
                worksheet.Cells["A2"].LoadFromCollection(personList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File (stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }

    }
}

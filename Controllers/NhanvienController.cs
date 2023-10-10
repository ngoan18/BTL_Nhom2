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
    public class NhanvienController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NhanvienController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Nhanvien
       /*
        public async Task<IActionResult> Index()
        {
              return _context.Nhanvien != null ? 
                          View(await _context.Nhanvien.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Nhanvien'  is null.");
        }

        
         public async Task<IActionResult> Index (int? page)
         {
            var model = _context.Nhanvien.ToList().ToPagedList(page ?? 1, 5);
            return View(model);
         }
        */

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
        var model = _context.Nhanvien.ToList().ToPagedList (page ?? 1, pagesize);
        return View (model);
        }



        // GET: Nhanvien/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Nhanvien == null)
            {
                return NotFound();
            }

            var nhanvien = await _context.Nhanvien
                .FirstOrDefaultAsync(m => m.Manv == id);
            if (nhanvien == null)
            {
                return NotFound();
            }

            return View(nhanvien);
        }

        // GET: Nhanvien/Create
        public IActionResult Create()
        {
            ViewData["Macv"]= new SelectList (_context.Chucvu,"Macv","Tencv");
            ViewData["Matd"]= new SelectList (_context.Trinhdo,"Matd","Tentd");
            ViewData["Mabp"]= new SelectList (_context.Bophan,"Mabp","Tenbp");
            ViewData["Mapc"]= new SelectList (_context.PhuCap,"Mapc","Tenpc");
            return View();
        }

        // POST: Nhanvien/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Manv,Hoten,Gioitinh,Ngaysinh,CCCD,Sdt,Diachi,Email,Macv,Tencv,Matd,Tentd,Mabp,Tenbp,Mapc,Tenpc")] Nhanvien nhanvien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhanvien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Macv"]= new SelectList (_context.Chucvu,"Macv","Tencv",nhanvien.Macv);
            ViewData["Matd"]= new SelectList (_context.Trinhdo,"Matd","Tentd",nhanvien.Matd);
            ViewData["Mabp"]= new SelectList (_context.Bophan,"Mabp","Tenbp",nhanvien.Mabp);
            ViewData["Mapc"]= new SelectList (_context.PhuCap,"Mapc","Tenpc",nhanvien.Mapc);
            return View(nhanvien);
        }

        // GET: Nhanvien/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Nhanvien == null)
            {
                return NotFound();
            }

            var nhanvien = await _context.Nhanvien.FindAsync(id);
            if (nhanvien == null)
            {
                return NotFound();
            }
            return View(nhanvien);
        }

        // POST: Nhanvien/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Manv,Hoten,Gioitinh,Ngaysinh,CCCD,Sdt,Diachi,Email")] Nhanvien nhanvien)
        {
            if (id != nhanvien.Manv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhanvien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhanvienExists(nhanvien.Manv))
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
            return View(nhanvien);
        }

        // GET: Nhanvien/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Nhanvien == null)
            {
                return NotFound();
            }

            var nhanvien = await _context.Nhanvien
                .FirstOrDefaultAsync(m => m.Manv == id);
            if (nhanvien == null)
            {
                return NotFound();
            }

            return View(nhanvien);
        }

        // POST: Nhanvien/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Nhanvien == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Nhanvien'  is null.");
            }
            var nhanvien = await _context.Nhanvien.FindAsync(id);
            if (nhanvien != null)
            {
                _context.Nhanvien.Remove(nhanvien);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhanvienExists(string id)
        {
          return (_context.Nhanvien?.Any(e => e.Manv == id)).GetValueOrDefault();
        }
         public IActionResult Download()
        {
            var fileName = "YourFileName" + ".xlsx";
            using (ExcelPackage excelPackage =new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                worksheet.Cells["A1"].Value = "Manv";
                worksheet.Cells["B1"].Value = "Hoten";
                worksheet.Cells["C1"].Value = "Gioitinh";
                worksheet.Cells["D1"].Value = "Ngaysinh";
                worksheet.Cells["E1"].Value = "CCCD";
                worksheet.Cells["F1"].Value = "Sdt";
                worksheet.Cells["G1"].Value = "Diachi";
                worksheet.Cells["H1"].Value = "Email";
                worksheet.Cells["I1"].Value = "Macv";
                worksheet.Cells["J1"].Value = "Matd";
                worksheet.Cells["K1"].Value = "Mabp";
                worksheet.Cells["L1"].Value = "Mpc";

                var personList = _context.Nhanvien.ToList();
                worksheet.Cells["A2"].LoadFromCollection(personList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File (stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
    }
}

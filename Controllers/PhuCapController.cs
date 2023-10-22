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
using qlnv.Models.Process;
using X.PagedList;

namespace qlnv.Controllers
{
    public class PhuCapController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhuCapController(ApplicationDbContext context)
        {
            _context = context;
        }

        private ExcelProcess _excelProcess = new ExcelProcess();

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
        var model = _context.PhuCap.ToList().ToPagedList (page ?? 1, pagesize);
        return View (model);
        }
        /*
        //phan trang
        public async Task<IActionResult> Index(int? page)
        {
            var model = _context.PhuCap.ToList().ToPagedList(page ?? 1, 5);
            return View(model);
        }

        // GET: PhuCap
        /*public async Task<IActionResult> Index()
        {
              return _context.PhuCap != null ? 
                          View(await _context.PhuCap.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.PhuCap'  is null.");
        }*/

        // GET: PhuCap/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.PhuCap == null)
            {
                return NotFound();
            }

            var phuCap = await _context.PhuCap
                .FirstOrDefaultAsync(m => m.Mapc == id);
            if (phuCap == null)
            {
                return NotFound();
            }

            return View(phuCap);
        }

        // GET: PhuCap/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PhuCap/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mapc,Tenpc,SoTien")] PhuCap phuCap)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phuCap);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(phuCap);
        }

        // GET: PhuCap/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.PhuCap == null)
            {
                return NotFound();
            }

            var phuCap = await _context.PhuCap.FindAsync(id);
            if (phuCap == null)
            {
                return NotFound();
            }
            return View(phuCap);
        }

        // POST: PhuCap/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Mapc,Tenpc,SoTien")] PhuCap phuCap)
        {
            if (id != phuCap.Mapc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phuCap);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhuCapExists(phuCap.Mapc))
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
            return View(phuCap);
        }

        // GET: PhuCap/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.PhuCap == null)
            {
                return NotFound();
            }

            var phuCap = await _context.PhuCap
                .FirstOrDefaultAsync(m => m.Mapc == id);
            if (phuCap == null)
            {
                return NotFound();
            }

            return View(phuCap);
        }

        // POST: PhuCap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.PhuCap == null)
            {
                return Problem("Entity set 'ApplicationDbContext.PhuCap'  is null.");
            }
            var phuCap = await _context.PhuCap.FindAsync(id);
            if (phuCap != null)
            {
                _context.PhuCap.Remove(phuCap);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhuCapExists(string id)
        {
          return (_context.PhuCap?.Any(e => e.Mapc == id)).GetValueOrDefault();
        }

         public IActionResult Download()
        {
            var fileName = "YourFileName" + ".xlsx";
            using (ExcelPackage excelPackage =new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                worksheet.Cells["A1"].Value = "Mapc";
                worksheet.Cells["B1"].Value = "Tenpc";
                worksheet.Cells["C1"].Value = "SoTien";

                var personList = _context.PhuCap.ToList();
                worksheet.Cells["A2"].LoadFromCollection(personList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File (stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }

        public async Task<IActionResult> Upload()
        {
            return View();
        }
        [HttpPost]public async Task<IActionResult>Upload(IFormFile file)
        {
            if (file!=null)
            {
                string fileExtension = Path.GetExtension(file.FileName);
                if (fileExtension != ".xls" && fileExtension != ".xlsx")
                {
                    ModelState.AddModelError("", "Please choose excel file to upload!");
                }
                else
                {
                    //rename file when upload to sever
                    var fileName = DateTime.Now.ToShortTimeString() + fileExtension;
                    var filePath = Path.Combine(Directory.GetCurrentDirectory() + "/Uploads/Excels", fileName);
                    var fileLocation = new FileInfo(filePath).ToString();
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        //save file to server
                        await file.CopyToAsync(stream);
                        //read data from file and write to database
                        var dt = _excelProcess.ExcelToDataTable(fileLocation);
                        //dùng vòng l?p for d? d?c d? li?u d?ng hd
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            //create a new Student object
                            var phuCap = new PhuCap();
                            //set values for attribiutes
                            phuCap.Mapc = dt.Rows[i][0].ToString();
                            phuCap.Tenpc = dt.Rows[i][1].ToString();
                            phuCap.SoTien = dt.Rows[i][2].ToString();
                             
                            //add oject to context
                            _context.PhuCap.Add(phuCap);
                        }
                        //save to database
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View();
        }
    }
}

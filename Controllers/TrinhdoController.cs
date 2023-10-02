using System.Security.Cryptography.X509Certificates;
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
    public class TrinhdoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TrinhdoController(ApplicationDbContext context)
        {
            _context = context;
        }
        private ExcelProcess _excelProcess = new ExcelProcess();
        // GET: Trinhdo
         public async Task<IActionResult> Index( int? page, int? PageSize )
        {
            ViewBag.PageSize = new List<SelectListItem>()
        {
            new SelectListItem() {Value="3", Text = "3"},
            new SelectListItem() {Value="5", Text = "5"},
            new SelectListItem() {Value="10", Text = "10"},
    
        };
        int pagesize = (PageSize ?? 3);
        ViewBag.psize = pagesize;
        var model = _context.Trinhdo.ToList().ToPagedList (page ?? 1, pagesize);
        return View (model);
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

         public async Task<IActionResult> Upload()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Upload(IFormFile file)
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
                            var trinhdo = new Trinhdo();
                            //set values for attribiutes
                            trinhdo.Matd = dt.Rows[i][0].ToString();
                            trinhdo.Tentd = dt.Rows[i][1].ToString();
                             
                            //add oject to context
                            _context.Trinhdo.Add(trinhdo);
                        }
                        //save to database
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View();
        }


         public IActionResult Download()
        {
            var fileName = "YourFileName" + ".xlsx";
            using (ExcelPackage excelPackage =new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                worksheet.Cells["A1"].Value = "Matd";
                worksheet.Cells["B1"].Value = "Tentd";
                var trinhdoList = _context.Trinhdo.ToList();
                worksheet.Cells["A2"].LoadFromCollection(trinhdoList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File (stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
    
    }
}

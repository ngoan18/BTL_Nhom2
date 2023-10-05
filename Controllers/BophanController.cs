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

namespace qlnv.Controllers
{
    public class BophanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BophanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Bophan
        public async Task<IActionResult> Index()
        {
              return _context.Bophan != null ? 
                          View(await _context.Bophan.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Bophan'  is null.");
        }

        // GET: Bophan/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Bophan == null)
            {
                return NotFound();
            }

            var bophan = await _context.Bophan
                .FirstOrDefaultAsync(m => m.Mabp == id);
            if (bophan == null)
            {
                return NotFound();
            }

            return View(bophan);
        }

        // GET: Bophan/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bophan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mabp,Tenbp")] Bophan bophan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bophan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bophan);
        }

        // GET: Bophan/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Bophan == null)
            {
                return NotFound();
            }

            var bophan = await _context.Bophan.FindAsync(id);
            if (bophan == null)
            {
                return NotFound();
            }
            return View(bophan);
        }

        // POST: Bophan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Mabp,Tenbp")] Bophan bophan)
        {
            if (id != bophan.Mabp)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bophan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BophanExists(bophan.Mabp))
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
            return View(bophan);
        }

        // GET: Bophan/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Bophan == null)
            {
                return NotFound();
            }

            var bophan = await _context.Bophan
                .FirstOrDefaultAsync(m => m.Mabp == id);
            if (bophan == null)
            {
                return NotFound();
            }

            return View(bophan);
        }

        // POST: Bophan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Bophan == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Bophan'  is null.");
            }
            var bophan = await _context.Bophan.FindAsync(id);
            if (bophan != null)
            {
                _context.Bophan.Remove(bophan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BophanExists(string id)
        {
          return (_context.Bophan?.Any(e => e.Mabp == id)).GetValueOrDefault();
        }
        private ExcelProcess _excelProcess = new ExcelProcess();  
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
                            var Bp = new Bophan();
                            //set values for attribiutes
                            Bp.Mabp = dt.Rows[i][0].ToString();
                            Bp.Tenbp = dt.Rows[i][1].ToString();
                             
                            //add oject to context
                            _context.Bophan.Add(Bp);
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
                worksheet.Cells["A1"].Value = "PersomId";
                worksheet.Cells["B1"].Value = "FullName";
                worksheet.Cells["C1"].Value = "Address";

                var personList = _context.Bophan.ToList();
                worksheet.Cells["A2"].LoadFromCollection(personList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File (stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
    
       
    }
}

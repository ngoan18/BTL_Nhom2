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
    public class PhongbanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PhongbanController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Phongban
        public async Task<IActionResult> Index()
        {
              return _context.Phongban != null ? 
                          View(await _context.Phongban.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Phongban'  is null.");
        }

        // GET: Phongban/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Phongban == null)
            {
                return NotFound();
            }

            var phongban = await _context.Phongban
                .FirstOrDefaultAsync(m => m.Mapb == id);
            if (phongban == null)
            {
                return NotFound();
            }

            return View(phongban);
        }

        // GET: Phongban/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Phongban/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Mapb,Tenpb")] Phongban phongban)
        {
            if (ModelState.IsValid)
            {
                _context.Add(phongban);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(phongban);
        }

        // GET: Phongban/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Phongban == null)
            {
                return NotFound();
            }

            var phongban = await _context.Phongban.FindAsync(id);
            if (phongban == null)
            {
                return NotFound();
            }
            return View(phongban);
        }

        // POST: Phongban/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Mapb,Tenpb")] Phongban phongban)
        {
            if (id != phongban.Mapb)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(phongban);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhongbanExists(phongban.Mapb))
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
            return View(phongban);
        }

        // GET: Phongban/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Phongban == null)
            {
                return NotFound();
            }

            var phongban = await _context.Phongban
                .FirstOrDefaultAsync(m => m.Mapb == id);
            if (phongban == null)
            {
                return NotFound();
            }

            return View(phongban);
        }

        // POST: Phongban/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Phongban == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Phongban'  is null.");
            }
            var phongban = await _context.Phongban.FindAsync(id);
            if (phongban != null)
            {
                _context.Phongban.Remove(phongban);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhongbanExists(string id)
        {
          return (_context.Phongban?.Any(e => e.Mapb == id)).GetValueOrDefault();
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
                            var Pb = new Phongban();
                            //set values for attribiutes
                            Pb.Mapb = dt.Rows[i][0].ToString();
                            Pb.Tenpb = dt.Rows[i][1].ToString();
                             
                            //add oject to context
                            _context.Phongban.Add(Pb);
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

                var personList = _context.Phongban.ToList();
                worksheet.Cells["A2"].LoadFromCollection(personList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                return File (stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }
    }
}

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
    public class KyLuatController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KyLuatController(ApplicationDbContext context)
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
        var model = _context.KyLuat.ToList().ToPagedList (page ?? 1, pagesize);
        return View (model);
        }

        /*
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

         public IActionResult Download()
        {
            var fileName = "YourFileName" + ".xlsx";
            using (ExcelPackage excelPackage =new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                worksheet.Cells["A1"].Value = "MaKL";
                worksheet.Cells["B1"].Value = "TenKL";
               

                var personList = _context.KyLuat.ToList();
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
                            var kyLuat = new KyLuat();
                            //set values for attribiutes
                            kyLuat.MaKL = dt.Rows[i][0].ToString();
                            kyLuat.TenKL = dt.Rows[i][1].ToString();
                             
                            //add oject to context
                            _context.KyLuat.Add(kyLuat);
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

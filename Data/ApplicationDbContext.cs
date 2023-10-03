using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using qlnv.Models;

namespace MvcMovie.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<qlnv.Models.Nhanvien> Nhanvien { get; set; } = default!;

        public DbSet<qlnv.Models.Bophan> Bophan { get; set; } = default!;

        public DbSet<qlnv.Models.Chucvu> Chucvu { get; set; } = default!;

        public DbSet<qlnv.Models.Phongban> Phongban { get; set; } = default!;

        public DbSet<qlnv.Models.Trinhdo> Trinhdo { get; set; } = default!;
        public DbSet<qlnv.Models.HopDong> HopDong { get; set; } = default!;
        public DbSet<qlnv.Models.KhenThuong> KhenThuong { get; set; } = default!;
        public DbSet<qlnv.Models.Luong> Luong { get; set; } = default!;
        public DbSet<qlnv.Models.PhuCap> PhuCap { get; set; } = default!;
        public DbSet<qlnv.Models.KyLuat> KyLuat { get; set; } = default!;
    }
}

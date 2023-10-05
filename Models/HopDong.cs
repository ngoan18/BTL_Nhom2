using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace qlnv.Models;

    public class HopDong
    {
        [Key]
        public string Mahd { get; set; }
        public string Tenhd { get; set; }
        public string NgayBatDau { get; set; }
        public string NgayKetThuc { get; set; }
        public string ThoiHan { get; set; }
        public string Manv { get; set; }
        [ForeignKey("Manv")]
        public Nhanvien? Nhanvien { get; set; }
        
        
        
        
    }

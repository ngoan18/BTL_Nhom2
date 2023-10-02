using System.ComponentModel.DataAnnotations;

namespace qlnv.Models;

    public class HopDong
    {
        [Key]
        public string Mahd { get; set; }
        public string Tenhd { get; set; }
        public string NgayBatDau { get; set; }
        public string NgayKetThuc { get; set; }
        public string ThoiHan { get; set; }
    }

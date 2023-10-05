using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace qlnv.Models;
public class KhenThuong
{
    [Key]
    public string MaKT { get; set; }
    public string TenKT { get; set; }
    public string Manv { get; set; }
    [ForeignKey("Manv")]
    public Nhanvien? Nhanvien { get; set; }
    
    
    
    
    
    
}
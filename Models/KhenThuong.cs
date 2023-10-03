using System.ComponentModel.DataAnnotations;

namespace qlnv.Models;
public class KhenThuong
{
    [Key]
    public string MaKT { get; set; }
    public string TenKT { get; set; }
    
    
}
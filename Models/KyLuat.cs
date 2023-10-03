using System.ComponentModel.DataAnnotations;

namespace qlnv.Models;
public class KyLuat
{
    [Key]
    public string MaKL { get; set; }
    public string TenKL { get; set; }
    
    
}
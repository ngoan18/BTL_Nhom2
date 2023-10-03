using System.ComponentModel.DataAnnotations;

namespace qlnv.Models;
public class PhuCap
{
    [Key]
    public string Mapc { get; set; }
    public string Tenpc { get; set; }
    public string SoTien { get; set; }
    
    
    
    
}
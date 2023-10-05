using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace qlnv.Models;
public class Luong
{
    [Key]
    public string LuongCB { get; set; }
    
    public string Macv { get; set; }
    [ForeignKey("Macv")]
    public Chucvu? Chucvu { get; set; }
    
    
    
    

    
    
    
}
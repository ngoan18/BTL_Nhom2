using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace qlnv.Models;
public class Chucvu
{
    [Key]
    public string Macv { get; set; }
    public string Tencv { get; set; }
    public string Mapb { get; set; }
    [ForeignKey("Mapb")]
    public Phongban ? phongban{ get; set; }
}
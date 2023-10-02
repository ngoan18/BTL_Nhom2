
using System.ComponentModel.DataAnnotations;
namespace qlnv.Models;
public class Phongban
{
    [Key]
    public string Mapb {get; set; }
    public string Tenpb {get; set; }
}
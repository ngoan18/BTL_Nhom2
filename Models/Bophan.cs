using System.ComponentModel.DataAnnotations;

namespace qlnv.Models;
public class Bophan
{
    [Key]
    public string Mabp { get; set; }
    public string Tenbp {get; set; }
}
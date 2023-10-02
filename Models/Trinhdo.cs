using System.ComponentModel.DataAnnotations;
namespace qlnv.Models;
public class Trinhdo
{
    [Key]
    public string Matd  { get; set; }
    public string Tentd { get; set; }
}
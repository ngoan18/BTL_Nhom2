using System.ComponentModel.DataAnnotations;
namespace qlnv.Models;
public class Chucvu
{
    [Key]
    public string Macv { get; set; }
    public string Tencv { get; set; }
}
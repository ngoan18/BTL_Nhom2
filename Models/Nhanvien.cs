using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace qlnv.Models;
public class Nhanvien
{
    [Key]
    public string Manv {get; set;}
    public string Hoten {get; set;}
    public string Gioitinh {get; set;}
    public DateTime Ngaysinh {get; set;}
    public string CCCD {get; set;}
    public string Sdt {get; set;}
    public string Diachi {get; set;}
    public string Email{get; set;}
    public string Macv {get; set;}
    [ForeignKey("Macv")]
    public Chucvu ? chucvu {get; set;}   
    public string Matd {get ;set;}
    [ForeignKey("Matd")]
    public Trinhdo ? trinhdo {get; set;}
    public string Mabp {get ;set;}
    [ForeignKey("Mabp")]
    public Bophan ? bophan {get; set;}
    public string Mapc {get ;set;}
    [ForeignKey("Mapc")]
    public PhuCap ? phucap {get; set;}


}









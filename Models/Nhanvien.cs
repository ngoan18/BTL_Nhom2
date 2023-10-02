using System.ComponentModel.DataAnnotations;

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

}









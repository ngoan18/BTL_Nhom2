using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace qlnv.Migrations
{
    /// <inheritdoc />
    public partial class _Foreignkey_Nhanvien : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bophan",
                columns: table => new
                {
                    Mabp = table.Column<string>(type: "TEXT", nullable: false),
                    Tenbp = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bophan", x => x.Mabp);
                });

            migrationBuilder.CreateTable(
                name: "KyLuat",
                columns: table => new
                {
                    MaKL = table.Column<string>(type: "TEXT", nullable: false),
                    TenKL = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KyLuat", x => x.MaKL);
                });

            migrationBuilder.CreateTable(
                name: "Phongban",
                columns: table => new
                {
                    Mapb = table.Column<string>(type: "TEXT", nullable: false),
                    Tenpb = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phongban", x => x.Mapb);
                });

            migrationBuilder.CreateTable(
                name: "PhuCap",
                columns: table => new
                {
                    Mapc = table.Column<string>(type: "TEXT", nullable: false),
                    Tenpc = table.Column<string>(type: "TEXT", nullable: false),
                    SoTien = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhuCap", x => x.Mapc);
                });

            migrationBuilder.CreateTable(
                name: "Trinhdo",
                columns: table => new
                {
                    Matd = table.Column<string>(type: "TEXT", nullable: false),
                    Tentd = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trinhdo", x => x.Matd);
                });

            migrationBuilder.CreateTable(
                name: "Chucvu",
                columns: table => new
                {
                    Macv = table.Column<string>(type: "TEXT", nullable: false),
                    Tencv = table.Column<string>(type: "TEXT", nullable: false),
                    Mapb = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chucvu", x => x.Macv);
                    table.ForeignKey(
                        name: "FK_Chucvu_Phongban_Mapb",
                        column: x => x.Mapb,
                        principalTable: "Phongban",
                        principalColumn: "Mapb",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Luong",
                columns: table => new
                {
                    LuongCB = table.Column<string>(type: "TEXT", nullable: false),
                    Macv = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Luong", x => x.LuongCB);
                    table.ForeignKey(
                        name: "FK_Luong_Chucvu_Macv",
                        column: x => x.Macv,
                        principalTable: "Chucvu",
                        principalColumn: "Macv",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Nhanvien",
                columns: table => new
                {
                    Manv = table.Column<string>(type: "TEXT", nullable: false),
                    Hoten = table.Column<string>(type: "TEXT", nullable: false),
                    Gioitinh = table.Column<string>(type: "TEXT", nullable: false),
                    Ngaysinh = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CCCD = table.Column<string>(type: "TEXT", nullable: false),
                    Sdt = table.Column<string>(type: "TEXT", nullable: false),
                    Diachi = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Macv = table.Column<string>(type: "TEXT", nullable: false),
                    Matd = table.Column<string>(type: "TEXT", nullable: false),
                    Mabp = table.Column<string>(type: "TEXT", nullable: false),
                    Mapc = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nhanvien", x => x.Manv);
                    table.ForeignKey(
                        name: "FK_Nhanvien_Bophan_Mabp",
                        column: x => x.Mabp,
                        principalTable: "Bophan",
                        principalColumn: "Mabp",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Nhanvien_Chucvu_Macv",
                        column: x => x.Macv,
                        principalTable: "Chucvu",
                        principalColumn: "Macv",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Nhanvien_PhuCap_Mapc",
                        column: x => x.Mapc,
                        principalTable: "PhuCap",
                        principalColumn: "Mapc",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Nhanvien_Trinhdo_Matd",
                        column: x => x.Matd,
                        principalTable: "Trinhdo",
                        principalColumn: "Matd",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HopDong",
                columns: table => new
                {
                    Mahd = table.Column<string>(type: "TEXT", nullable: false),
                    Tenhd = table.Column<string>(type: "TEXT", nullable: false),
                    NgayBatDau = table.Column<string>(type: "TEXT", nullable: false),
                    NgayKetThuc = table.Column<string>(type: "TEXT", nullable: false),
                    ThoiHan = table.Column<string>(type: "TEXT", nullable: false),
                    Manv = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HopDong", x => x.Mahd);
                    table.ForeignKey(
                        name: "FK_HopDong_Nhanvien_Manv",
                        column: x => x.Manv,
                        principalTable: "Nhanvien",
                        principalColumn: "Manv",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KhenThuong",
                columns: table => new
                {
                    MaKT = table.Column<string>(type: "TEXT", nullable: false),
                    TenKT = table.Column<string>(type: "TEXT", nullable: false),
                    Manv = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhenThuong", x => x.MaKT);
                    table.ForeignKey(
                        name: "FK_KhenThuong_Nhanvien_Manv",
                        column: x => x.Manv,
                        principalTable: "Nhanvien",
                        principalColumn: "Manv",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chucvu_Mapb",
                table: "Chucvu",
                column: "Mapb");

            migrationBuilder.CreateIndex(
                name: "IX_HopDong_Manv",
                table: "HopDong",
                column: "Manv");

            migrationBuilder.CreateIndex(
                name: "IX_KhenThuong_Manv",
                table: "KhenThuong",
                column: "Manv");

            migrationBuilder.CreateIndex(
                name: "IX_Luong_Macv",
                table: "Luong",
                column: "Macv");

            migrationBuilder.CreateIndex(
                name: "IX_Nhanvien_Mabp",
                table: "Nhanvien",
                column: "Mabp");

            migrationBuilder.CreateIndex(
                name: "IX_Nhanvien_Macv",
                table: "Nhanvien",
                column: "Macv");

            migrationBuilder.CreateIndex(
                name: "IX_Nhanvien_Mapc",
                table: "Nhanvien",
                column: "Mapc");

            migrationBuilder.CreateIndex(
                name: "IX_Nhanvien_Matd",
                table: "Nhanvien",
                column: "Matd");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HopDong");

            migrationBuilder.DropTable(
                name: "KhenThuong");

            migrationBuilder.DropTable(
                name: "KyLuat");

            migrationBuilder.DropTable(
                name: "Luong");

            migrationBuilder.DropTable(
                name: "Nhanvien");

            migrationBuilder.DropTable(
                name: "Bophan");

            migrationBuilder.DropTable(
                name: "Chucvu");

            migrationBuilder.DropTable(
                name: "PhuCap");

            migrationBuilder.DropTable(
                name: "Trinhdo");

            migrationBuilder.DropTable(
                name: "Phongban");
        }
    }
}

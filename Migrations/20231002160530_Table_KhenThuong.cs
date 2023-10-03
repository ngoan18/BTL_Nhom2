using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace qlnv.Migrations
{
    /// <inheritdoc />
    public partial class Table_KhenThuong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "KhenThuong",
                columns: table => new
                {
                    MaKT = table.Column<string>(type: "TEXT", nullable: false),
                    TenKT = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhenThuong", x => x.MaKT);
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
                name: "Luong",
                columns: table => new
                {
                    LuongCB = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Luong", x => x.LuongCB);
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KhenThuong");

            migrationBuilder.DropTable(
                name: "KyLuat");

            migrationBuilder.DropTable(
                name: "Luong");

            migrationBuilder.DropTable(
                name: "PhuCap");
        }
    }
}

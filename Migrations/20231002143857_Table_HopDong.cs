using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace qlnv.Migrations
{
    /// <inheritdoc />
    public partial class Table_HopDong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HopDong",
                columns: table => new
                {
                    Mahd = table.Column<string>(type: "TEXT", nullable: false),
                    Tenhd = table.Column<string>(type: "TEXT", nullable: false),
                    NgayBatDau = table.Column<string>(type: "TEXT", nullable: false),
                    NgayKetThuc = table.Column<string>(type: "TEXT", nullable: false),
                    ThoiHan = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HopDong", x => x.Mahd);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HopDong");
        }
    }
}

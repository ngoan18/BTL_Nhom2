using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace qlnv.Migrations
{
    /// <inheritdoc />
    public partial class Create_Foreignkey_HopDong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Manv",
                table: "HopDong",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_HopDong_Manv",
                table: "HopDong",
                column: "Manv");

            migrationBuilder.AddForeignKey(
                name: "FK_HopDong_Nhanvien_Manv",
                table: "HopDong",
                column: "Manv",
                principalTable: "Nhanvien",
                principalColumn: "Manv",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HopDong_Nhanvien_Manv",
                table: "HopDong");

            migrationBuilder.DropIndex(
                name: "IX_HopDong_Manv",
                table: "HopDong");

            migrationBuilder.DropColumn(
                name: "Manv",
                table: "HopDong");
        }
    }
}

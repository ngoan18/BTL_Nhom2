using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace qlnv.Migrations
{
    /// <inheritdoc />
    public partial class Create_Foreignkey_KhenThuong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Manv",
                table: "KhenThuong",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_KhenThuong_Manv",
                table: "KhenThuong",
                column: "Manv");

            migrationBuilder.AddForeignKey(
                name: "FK_KhenThuong_Nhanvien_Manv",
                table: "KhenThuong",
                column: "Manv",
                principalTable: "Nhanvien",
                principalColumn: "Manv",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_KhenThuong_Nhanvien_Manv",
                table: "KhenThuong");

            migrationBuilder.DropIndex(
                name: "IX_KhenThuong_Manv",
                table: "KhenThuong");

            migrationBuilder.DropColumn(
                name: "Manv",
                table: "KhenThuong");
        }
    }
}

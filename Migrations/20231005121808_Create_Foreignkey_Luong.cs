using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace qlnv.Migrations
{
    /// <inheritdoc />
    public partial class Create_Foreignkey_Luong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Macv",
                table: "Luong",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Luong_Macv",
                table: "Luong",
                column: "Macv");

            migrationBuilder.AddForeignKey(
                name: "FK_Luong_Chucvu_Macv",
                table: "Luong",
                column: "Macv",
                principalTable: "Chucvu",
                principalColumn: "Macv",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Luong_Chucvu_Macv",
                table: "Luong");

            migrationBuilder.DropIndex(
                name: "IX_Luong_Macv",
                table: "Luong");

            migrationBuilder.DropColumn(
                name: "Macv",
                table: "Luong");
        }
    }
}

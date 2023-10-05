using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace qlnv.Migrations
{
    /// <inheritdoc />
    public partial class Create_Foreignkey_Chucvu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Mapb",
                table: "Chucvu",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Chucvu_Mapb",
                table: "Chucvu",
                column: "Mapb");

            migrationBuilder.AddForeignKey(
                name: "FK_Chucvu_Phongban_Mapb",
                table: "Chucvu",
                column: "Mapb",
                principalTable: "Phongban",
                principalColumn: "Mapb",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chucvu_Phongban_Mapb",
                table: "Chucvu");

            migrationBuilder.DropIndex(
                name: "IX_Chucvu_Mapb",
                table: "Chucvu");

            migrationBuilder.DropColumn(
                name: "Mapb",
                table: "Chucvu");
        }
    }
}

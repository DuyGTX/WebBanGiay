using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanGiay.Migrations
{
    public partial class addQuantityToShoe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "shoe",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Sold",
                table: "shoe",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "shoe");

            migrationBuilder.DropColumn(
                name: "Sold",
                table: "shoe");
        }
    }
}

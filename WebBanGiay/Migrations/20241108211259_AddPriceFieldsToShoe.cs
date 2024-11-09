using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanGiay.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceFieldsToShoe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__shoe_item__colou__44FF419A",
                table: "shoe_item");

            migrationBuilder.DropForeignKey(
                name: "FK__shoe_item__shoe___440B1D61",
                table: "shoe_item");

            migrationBuilder.DropForeignKey(
                name: "FK__shoe_item__size___45F365D3",
                table: "shoe_item");

            migrationBuilder.DropIndex(
                name: "IX_shoe_item_colour_id",
                table: "shoe_item");

            migrationBuilder.DropIndex(
                name: "IX_shoe_item_size_id",
                table: "shoe_item");

            migrationBuilder.DropColumn(
                name: "sort_order",
                table: "size");

            migrationBuilder.DropColumn(
                name: "colour_id",
                table: "shoe_item");

            migrationBuilder.DropColumn(
                name: "size_id",
                table: "shoe_item");

            migrationBuilder.DropColumn(
                name: "stock_quantity",
                table: "shoe_item");

            migrationBuilder.AlterColumn<int>(
                name: "shoe_id",
                table: "shoe_item",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "shoe_item_colour",
                columns: table => new
                {
                    shoe_item_id = table.Column<int>(type: "int", nullable: false),
                    colour_id = table.Column<int>(type: "int", nullable: false),
                    stock_quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__shoe_ite__AD441779031EC862", x => new { x.shoe_item_id, x.colour_id });
                    table.ForeignKey(
                        name: "FK__shoe_item__colou__3D2915A8",
                        column: x => x.colour_id,
                        principalTable: "colour",
                        principalColumn: "colour_id");
                    table.ForeignKey(
                        name: "FK__shoe_item__shoe___3C34F16F",
                        column: x => x.shoe_item_id,
                        principalTable: "shoe_item",
                        principalColumn: "shoe_item_id");
                });

            migrationBuilder.CreateTable(
                name: "shoe_item_size",
                columns: table => new
                {
                    shoe_item_id = table.Column<int>(type: "int", nullable: false),
                    size_id = table.Column<int>(type: "int", nullable: false),
                    stock_quantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__shoe_ite__9E11EE98D3C88260", x => new { x.shoe_item_id, x.size_id });
                    table.ForeignKey(
                        name: "FK__shoe_item__shoe___40058253",
                        column: x => x.shoe_item_id,
                        principalTable: "shoe_item",
                        principalColumn: "shoe_item_id");
                    table.ForeignKey(
                        name: "FK__shoe_item__size___40F9A68C",
                        column: x => x.size_id,
                        principalTable: "size",
                        principalColumn: "size_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_shoe_item_colour_colour_id",
                table: "shoe_item_colour",
                column: "colour_id");

            migrationBuilder.CreateIndex(
                name: "IX_shoe_item_size_size_id",
                table: "shoe_item_size",
                column: "size_id");

            migrationBuilder.AddForeignKey(
                name: "FK__shoe_item__shoe___44FF419A",
                table: "shoe_item",
                column: "shoe_id",
                principalTable: "shoe",
                principalColumn: "shoe_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__shoe_item__shoe___44FF419A",
                table: "shoe_item");

            migrationBuilder.DropTable(
                name: "shoe_item_colour");

            migrationBuilder.DropTable(
                name: "shoe_item_size");

            migrationBuilder.AddColumn<int>(
                name: "sort_order",
                table: "size",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "shoe_id",
                table: "shoe_item",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "colour_id",
                table: "shoe_item",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "size_id",
                table: "shoe_item",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "stock_quantity",
                table: "shoe_item",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_shoe_item_colour_id",
                table: "shoe_item",
                column: "colour_id");

            migrationBuilder.CreateIndex(
                name: "IX_shoe_item_size_id",
                table: "shoe_item",
                column: "size_id");

            migrationBuilder.AddForeignKey(
                name: "FK__shoe_item__colou__44FF419A",
                table: "shoe_item",
                column: "colour_id",
                principalTable: "colour",
                principalColumn: "colour_id");

            migrationBuilder.AddForeignKey(
                name: "FK__shoe_item__shoe___440B1D61",
                table: "shoe_item",
                column: "shoe_id",
                principalTable: "shoe",
                principalColumn: "shoe_id");

            migrationBuilder.AddForeignKey(
                name: "FK__shoe_item__size___45F365D3",
                table: "shoe_item",
                column: "size_id",
                principalTable: "size",
                principalColumn: "size_id");
        }
    }
}

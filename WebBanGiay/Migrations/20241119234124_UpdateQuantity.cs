using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanGiay.Migrations
{
    public partial class UpdateQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            

            migrationBuilder.CreateTable(
                name: "ProductQuantities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ShoeId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductQuantities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ShoeId",
                table: "OrderDetails",
                column: "ShoeId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_shoe_ShoeId",
                table: "OrderDetails",
                column: "ShoeId",
                principalTable: "shoe",
                principalColumn: "shoe_id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_shoe_ShoeId",
                table: "OrderDetails");

            migrationBuilder.DropTable(
                name: "ProductQuantities");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_ShoeId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "OrderDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "attribute",
                columns: table => new
                {
                    attribute_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    attribute_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__attribut__9090C9BB6D03B184", x => x.attribute_id);
                });

            migrationBuilder.CreateTable(
                name: "shoe_attribute",
                columns: table => new
                {
                    shoe_id = table.Column<int>(type: "int", nullable: false),
                    attribute_id = table.Column<int>(type: "int", nullable: false),
                    attribute_value = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__shoe_att__93C93DD5E2782B71", x => new { x.shoe_id, x.attribute_id });
                    table.ForeignKey(
                        name: "FK__shoe_attr__attri__4E88ABD4",
                        column: x => x.attribute_id,
                        principalTable: "attribute",
                        principalColumn: "attribute_id");
                    table.ForeignKey(
                        name: "FK__shoe_attr__shoe___4D94879B",
                        column: x => x.shoe_id,
                        principalTable: "shoe",
                        principalColumn: "shoe_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_shoe_attribute_attribute_id",
                table: "shoe_attribute",
                column: "attribute_id");
        }
    }
}

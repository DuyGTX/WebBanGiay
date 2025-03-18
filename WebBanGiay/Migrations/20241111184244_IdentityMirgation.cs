using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanGiay.Migrations
{
    /// <inheritdoc />
    public partial class IdentityMirgation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			
			

           

           

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

           
            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderKey = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LoginProvider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                });

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "shoe_colour");

            migrationBuilder.DropTable(
                name: "shoe_size");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogins");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropColumn(
                name: "price",
                table: "shoe");

            migrationBuilder.DropColumn(
                name: "sale_price",
                table: "shoe");

            migrationBuilder.DropColumn(
                name: "sku",
                table: "shoe");

            migrationBuilder.CreateTable(
                name: "shoe_item",
                columns: table => new
                {
                    shoe_item_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    shoe_id = table.Column<int>(type: "int", nullable: false),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    sale_price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    sku = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__shoe_ite__9ECD427BA415457D", x => x.shoe_item_id);
                    table.ForeignKey(
                        name: "FK__shoe_item__shoe___44FF419A",
                        column: x => x.shoe_id,
                        principalTable: "shoe",
                        principalColumn: "shoe_id");
                });

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
                name: "IX_order_item_shoe_item_id",
                table: "order_item",
                column: "shoe_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_shoe_item_shoe_id",
                table: "shoe_item",
                column: "shoe_id");

            migrationBuilder.CreateIndex(
                name: "IX_shoe_item_colour_colour_id",
                table: "shoe_item_colour",
                column: "colour_id");

            migrationBuilder.CreateIndex(
                name: "IX_shoe_item_size_size_id",
                table: "shoe_item_size",
                column: "size_id");

            migrationBuilder.AddForeignKey(
                name: "FK__order_ite__shoe___6D0D32F4",
                table: "order_item",
                column: "shoe_item_id",
                principalTable: "shoe_item",
                principalColumn: "shoe_item_id");
        }
    }
}

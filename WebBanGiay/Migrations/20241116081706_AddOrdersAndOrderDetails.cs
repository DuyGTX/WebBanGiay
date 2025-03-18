using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanGiay.Migrations
{
    public partial class AddOrdersAndOrderDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShoeId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.CreateTable(
                name: "order_status",
                columns: table => new
                {
                    status_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    status_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__order_st__3683B531643A7DD2", x => x.status_id);
                });

            migrationBuilder.CreateTable(
                name: "payment_method",
                columns: table => new
                {
                    payment_method_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    method_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__payment___8A3EA9EB458D075A", x => x.payment_method_id);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    coupon_id = table.Column<int>(type: "int", nullable: true),
                    customer_id = table.Column<int>(type: "int", nullable: true),
                    payment_method_id = table.Column<int>(type: "int", nullable: true),
                    shipping_address_id = table.Column<int>(type: "int", nullable: true),
                    status_id = table.Column<int>(type: "int", nullable: true),
                    order_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    total_amount = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__order__4659622958D90153", x => x.order_id);
                    table.ForeignKey(
                        name: "FK__order__coupon_id__693CA210",
                        column: x => x.coupon_id,
                        principalTable: "coupon",
                        principalColumn: "coupon_id");
                    table.ForeignKey(
                        name: "FK__order__customer___656C112C",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "customer_id");
                    table.ForeignKey(
                        name: "FK__order__payment_m__66603565",
                        column: x => x.payment_method_id,
                        principalTable: "payment_method",
                        principalColumn: "payment_method_id");
                    table.ForeignKey(
                        name: "FK__order__shipping___68487DD7",
                        column: x => x.shipping_address_id,
                        principalTable: "shipping_address",
                        principalColumn: "address_id");
                    table.ForeignKey(
                        name: "FK__order__status_id__6754599E",
                        column: x => x.status_id,
                        principalTable: "order_status",
                        principalColumn: "status_id");
                });

            migrationBuilder.CreateTable(
                name: "order_item",
                columns: table => new
                {
                    order_item_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<int>(type: "int", nullable: true),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    shoe_item_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__order_it__3764B6BC5E06BAA7", x => x.order_item_id);
                    table.ForeignKey(
                        name: "FK__order_ite__order__6C190EBB",
                        column: x => x.order_id,
                        principalTable: "order",
                        principalColumn: "order_id");
                });

            migrationBuilder.CreateTable(
                name: "order_notes",
                columns: table => new
                {
                    note_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<int>(type: "int", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    note_text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__order_no__CEDD0FA4EFC50179", x => x.note_id);
                    table.ForeignKey(
                        name: "FK__order_not__order__70DDC3D8",
                        column: x => x.order_id,
                        principalTable: "order",
                        principalColumn: "order_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_coupon_id",
                table: "order",
                column: "coupon_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_customer_id",
                table: "order",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_payment_method_id",
                table: "order",
                column: "payment_method_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_shipping_address_id",
                table: "order",
                column: "shipping_address_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_status_id",
                table: "order",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_item_order_id",
                table: "order_item",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_notes_order_id",
                table: "order_notes",
                column: "order_id");
        }
    }
}

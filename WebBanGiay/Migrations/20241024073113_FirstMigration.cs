using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebBanGiay.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "brand",
                columns: table => new
                {
                    brand_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    brand_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__brand__5E5A8E273CA2B74C", x => x.brand_id);
                });

            migrationBuilder.CreateTable(
                name: "colour",
                columns: table => new
                {
                    colour_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    colour_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__colour__38955022F3DFA4F0", x => x.colour_id);
                });

            migrationBuilder.CreateTable(
                name: "coupon",
                columns: table => new
                {
                    coupon_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    coupon_code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    discount_amount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    discount_percentage = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    valid_from = table.Column<DateTime>(type: "date", nullable: true),
                    valid_to = table.Column<DateTime>(type: "date", nullable: true),
                    is_active = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__coupon__58CF63895EE0BE10", x => x.coupon_id);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    cus_address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__customer__CD65CB85B82E4FE8", x => x.customer_id);
                });

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
                name: "role",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role_name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__role__760965CCED52F0EC", x => x.role_id);
                });

            migrationBuilder.CreateTable(
                name: "size",
                columns: table => new
                {
                    size_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    size_name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    sort_order = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__size__0DCACE31C597D616", x => x.size_id);
                });

            migrationBuilder.CreateTable(
                name: "shoe_category",
                columns: table => new
                {
                    category_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    brand_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__shoe_cat__D54EE9B4FFA3D24E", x => x.category_id);
                    table.ForeignKey(
                        name: "FK__shoe_cate__brand__398D8EEE",
                        column: x => x.brand_id,
                        principalTable: "brand",
                        principalColumn: "brand_id");
                });

            migrationBuilder.CreateTable(
                name: "shipping_address",
                columns: table => new
                {
                    address_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_id = table.Column<int>(type: "int", nullable: true),
                    street_address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    city = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    state = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    postal_code = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__shipping__CAA247C82568C55C", x => x.address_id);
                    table.ForeignKey(
                        name: "FK__shipping___custo__5EBF139D",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "customer_id");
                });

            migrationBuilder.CreateTable(
                name: "RoleAccount",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    AccountId = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__RoleAcco__F9B314400B671812", x => new { x.RoleId, x.AccountId });
                    table.ForeignKey(
                        name: "FK__RoleAccou__Accou__5812160E",
                        column: x => x.AccountId,
                        principalTable: "customer",
                        principalColumn: "customer_id");
                    table.ForeignKey(
                        name: "FK__RoleAccou__RoleI__571DF1D5",
                        column: x => x.RoleId,
                        principalTable: "role",
                        principalColumn: "role_id");
                });

            migrationBuilder.CreateTable(
                name: "shoe",
                columns: table => new
                {
                    shoe_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    category_id = table.Column<int>(type: "int", nullable: true),
                    brand_id = table.Column<int>(type: "int", nullable: true),
                    shoe_name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    shoe_description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    care_instructions = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__shoe__3AC0314EB0506C92", x => x.shoe_id);
                    table.ForeignKey(
                        name: "FK__shoe__brand_id__3D5E1FD2",
                        column: x => x.brand_id,
                        principalTable: "brand",
                        principalColumn: "brand_id");
                    table.ForeignKey(
                        name: "FK__shoe__category_i__3C69FB99",
                        column: x => x.category_id,
                        principalTable: "shoe_category",
                        principalColumn: "category_id");
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_id = table.Column<int>(type: "int", nullable: true),
                    order_date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    total_amount = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    payment_method_id = table.Column<int>(type: "int", nullable: true),
                    status_id = table.Column<int>(type: "int", nullable: true),
                    shipping_address_id = table.Column<int>(type: "int", nullable: true),
                    coupon_id = table.Column<int>(type: "int", nullable: true)
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

            migrationBuilder.CreateTable(
                name: "shoe_image",
                columns: table => new
                {
                    image_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    shoe_id = table.Column<int>(type: "int", nullable: true),
                    image_url = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__shoe_ima__DC9AC955AFF94A72", x => x.image_id);
                    table.ForeignKey(
                        name: "FK__shoe_imag__shoe___48CFD27E",
                        column: x => x.shoe_id,
                        principalTable: "shoe",
                        principalColumn: "shoe_id");
                });

            migrationBuilder.CreateTable(
                name: "shoe_item",
                columns: table => new
                {
                    shoe_item_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    shoe_id = table.Column<int>(type: "int", nullable: true),
                    colour_id = table.Column<int>(type: "int", nullable: true),
                    size_id = table.Column<int>(type: "int", nullable: true),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    sale_price = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    stock_quantity = table.Column<int>(type: "int", nullable: true),
                    sku = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__shoe_ite__9ECD427BA415457D", x => x.shoe_item_id);
                    table.ForeignKey(
                        name: "FK__shoe_item__colou__44FF419A",
                        column: x => x.colour_id,
                        principalTable: "colour",
                        principalColumn: "colour_id");
                    table.ForeignKey(
                        name: "FK__shoe_item__shoe___440B1D61",
                        column: x => x.shoe_id,
                        principalTable: "shoe",
                        principalColumn: "shoe_id");
                    table.ForeignKey(
                        name: "FK__shoe_item__size___45F365D3",
                        column: x => x.size_id,
                        principalTable: "size",
                        principalColumn: "size_id");
                });

            migrationBuilder.CreateTable(
                name: "order_notes",
                columns: table => new
                {
                    note_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<int>(type: "int", nullable: true),
                    note_text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_at = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
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

            migrationBuilder.CreateTable(
                name: "order_item",
                columns: table => new
                {
                    order_item_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<int>(type: "int", nullable: true),
                    shoe_item_id = table.Column<int>(type: "int", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: true),
                    price = table.Column<decimal>(type: "decimal(10,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__order_it__3764B6BC5E06BAA7", x => x.order_item_id);
                    table.ForeignKey(
                        name: "FK__order_ite__order__6C190EBB",
                        column: x => x.order_id,
                        principalTable: "order",
                        principalColumn: "order_id");
                    table.ForeignKey(
                        name: "FK__order_ite__shoe___6D0D32F4",
                        column: x => x.shoe_item_id,
                        principalTable: "shoe_item",
                        principalColumn: "shoe_item_id");
                });

            migrationBuilder.CreateIndex(
                name: "UQ__coupon__ADE5CBB7EF1D0423",
                table: "coupon",
                column: "coupon_code",
                unique: true,
                filter: "[coupon_code] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "UQ__customer__AB6E61644AEADEE4",
                table: "customer",
                column: "email",
                unique: true);

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
                name: "IX_order_item_shoe_item_id",
                table: "order_item",
                column: "shoe_item_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_notes_order_id",
                table: "order_notes",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAccount_AccountId",
                table: "RoleAccount",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_shipping_address_customer_id",
                table: "shipping_address",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_shoe_brand_id",
                table: "shoe",
                column: "brand_id");

            migrationBuilder.CreateIndex(
                name: "IX_shoe_category_id",
                table: "shoe",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_shoe_attribute_attribute_id",
                table: "shoe_attribute",
                column: "attribute_id");

            migrationBuilder.CreateIndex(
                name: "IX_shoe_category_brand_id",
                table: "shoe_category",
                column: "brand_id");

            migrationBuilder.CreateIndex(
                name: "IX_shoe_image_shoe_id",
                table: "shoe_image",
                column: "shoe_id");

            migrationBuilder.CreateIndex(
                name: "IX_shoe_item_colour_id",
                table: "shoe_item",
                column: "colour_id");

            migrationBuilder.CreateIndex(
                name: "IX_shoe_item_shoe_id",
                table: "shoe_item",
                column: "shoe_id");

            migrationBuilder.CreateIndex(
                name: "IX_shoe_item_size_id",
                table: "shoe_item",
                column: "size_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_item");

            migrationBuilder.DropTable(
                name: "order_notes");

            migrationBuilder.DropTable(
                name: "RoleAccount");

            migrationBuilder.DropTable(
                name: "shoe_attribute");

            migrationBuilder.DropTable(
                name: "shoe_image");

            migrationBuilder.DropTable(
                name: "shoe_item");

            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "role");

            migrationBuilder.DropTable(
                name: "attribute");

            migrationBuilder.DropTable(
                name: "colour");

            migrationBuilder.DropTable(
                name: "shoe");

            migrationBuilder.DropTable(
                name: "size");

            migrationBuilder.DropTable(
                name: "coupon");

            migrationBuilder.DropTable(
                name: "payment_method");

            migrationBuilder.DropTable(
                name: "shipping_address");

            migrationBuilder.DropTable(
                name: "order_status");

            migrationBuilder.DropTable(
                name: "shoe_category");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "brand");
        }
    }
}

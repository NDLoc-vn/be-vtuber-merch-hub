using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VtuberMerchHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateColumnNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Users_UserId",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_CartId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Customers_CustomerId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Genders_GenderId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Users_UserId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Vtubers_VtuberId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Merchandises_Vtubers_VtuberId",
                table: "Merchandises");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Merchandises_MerchandiseId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Vtubers_Companies_CompanyId",
                table: "Vtubers");

            migrationBuilder.DropForeignKey(
                name: "FK_Vtubers_Genders_VtuberGender",
                table: "Vtubers");

            migrationBuilder.DropForeignKey(
                name: "FK_Vtubers_Species_SpeciesId",
                table: "Vtubers");

            migrationBuilder.DropForeignKey(
                name: "FK_Vtubers_Users_UserId",
                table: "Vtubers");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Vtubers",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Channel",
                table: "Vtubers",
                newName: "channel");

            migrationBuilder.RenameColumn(
                name: "VtuberName",
                table: "Vtubers",
                newName: "vtuber_name");

            migrationBuilder.RenameColumn(
                name: "VtuberGender",
                table: "Vtubers",
                newName: "vtuber_gender");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Vtubers",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "SpeciesId",
                table: "Vtubers",
                newName: "species_id");

            migrationBuilder.RenameColumn(
                name: "RealName",
                table: "Vtubers",
                newName: "real_name");

            migrationBuilder.RenameColumn(
                name: "ModelUrl",
                table: "Vtubers",
                newName: "model_url");

            migrationBuilder.RenameColumn(
                name: "DebutDate",
                table: "Vtubers",
                newName: "debut_date");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Vtubers",
                newName: "company_id");

            migrationBuilder.RenameColumn(
                name: "VtuberId",
                table: "Vtubers",
                newName: "vtuber_id");

            migrationBuilder.RenameIndex(
                name: "IX_Vtubers_VtuberGender",
                table: "Vtubers",
                newName: "IX_Vtubers_vtuber_gender");

            migrationBuilder.RenameIndex(
                name: "IX_Vtubers_UserId",
                table: "Vtubers",
                newName: "IX_Vtubers_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Vtubers_SpeciesId",
                table: "Vtubers",
                newName: "IX_Vtubers_species_id");

            migrationBuilder.RenameIndex(
                name: "IX_Vtubers_CompanyId",
                table: "Vtubers",
                newName: "IX_Vtubers_company_id");

            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "role");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Users",
                newName: "email");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Users",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Users",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "AvatarUrl",
                table: "Users",
                newName: "avatar_url");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Species",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "SpeciesName",
                table: "Species",
                newName: "species_name");

            migrationBuilder.RenameColumn(
                name: "SpeciesId",
                table: "Species",
                newName: "species_id");

            migrationBuilder.RenameColumn(
                name: "Stock",
                table: "Products",
                newName: "stock");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Products",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Products",
                newName: "product_name");

            migrationBuilder.RenameColumn(
                name: "MerchandiseId",
                table: "Products",
                newName: "merchandise_id");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Products",
                newName: "image_url");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Products",
                newName: "category_id");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "product_id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_MerchandiseId",
                table: "Products",
                newName: "IX_Products_merchandise_id");

            migrationBuilder.RenameIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                newName: "IX_Products_category_id");

            migrationBuilder.RenameColumn(
                name: "TotalAmount",
                table: "Orders",
                newName: "total_amount");

            migrationBuilder.RenameColumn(
                name: "ShippingAddress",
                table: "Orders",
                newName: "shipping_address");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "Orders",
                newName: "order_date");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Orders",
                newName: "customer_id");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Orders",
                newName: "order_id");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                newName: "IX_Orders_customer_id");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "OrderDetails",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "OrderDetails",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "OrderDetails",
                newName: "product_id");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "OrderDetails",
                newName: "order_id");

            migrationBuilder.RenameColumn(
                name: "OrderDetailId",
                table: "OrderDetails",
                newName: "order_detail_id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_order_id");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Merchandises",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "VtuberId",
                table: "Merchandises",
                newName: "vtuber_id");

            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "Merchandises",
                newName: "start_date");

            migrationBuilder.RenameColumn(
                name: "MerchandiseName",
                table: "Merchandises",
                newName: "merchandise_name");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "Merchandises",
                newName: "end_date");

            migrationBuilder.RenameColumn(
                name: "MerchandiseId",
                table: "Merchandises",
                newName: "merchandise_id");

            migrationBuilder.RenameIndex(
                name: "IX_Merchandises_VtuberId",
                table: "Merchandises",
                newName: "IX_Merchandises_vtuber_id");

            migrationBuilder.RenameColumn(
                name: "GenderType",
                table: "Genders",
                newName: "gender");

            migrationBuilder.RenameColumn(
                name: "GenderId",
                table: "Genders",
                newName: "gender_id");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Events",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Events",
                newName: "date");

            migrationBuilder.RenameColumn(
                name: "VtuberId",
                table: "Events",
                newName: "vtuber_id");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Events",
                newName: "event_id");

            migrationBuilder.RenameIndex(
                name: "IX_Events_VtuberId",
                table: "Events",
                newName: "IX_Events_vtuber_id");

            migrationBuilder.RenameColumn(
                name: "Nickname",
                table: "Customers",
                newName: "nickname");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Customers",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Customers",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Customers",
                newName: "phone_number");

            migrationBuilder.RenameColumn(
                name: "GenderId",
                table: "Customers",
                newName: "gender_id");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "Customers",
                newName: "full_name");

            migrationBuilder.RenameColumn(
                name: "BirthDate",
                table: "Customers",
                newName: "birth_date");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Customers",
                newName: "customer_id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                newName: "IX_Customers_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_GenderId",
                table: "Customers",
                newName: "IX_Customers_gender_id");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Companies",
                newName: "address");

            migrationBuilder.RenameColumn(
                name: "ContactEmail",
                table: "Companies",
                newName: "contact_email");

            migrationBuilder.RenameColumn(
                name: "CompanyName",
                table: "Companies",
                newName: "company_name");

            migrationBuilder.RenameColumn(
                name: "CompanyId",
                table: "Companies",
                newName: "company_id");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Categories",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Categories",
                newName: "category_name");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "category_id");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "Carts",
                newName: "customer_id");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Carts",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "Carts",
                newName: "cart_id");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_CustomerId",
                table: "Carts",
                newName: "IX_Carts_customer_id");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "CartItems",
                newName: "quantity");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "CartItems",
                newName: "product_id");

            migrationBuilder.RenameColumn(
                name: "CartId",
                table: "CartItems",
                newName: "cart_id");

            migrationBuilder.RenameColumn(
                name: "CartItemId",
                table: "CartItems",
                newName: "cart_item_id");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                newName: "IX_CartItems_product_id");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                newName: "IX_CartItems_cart_id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Admins",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "AdminName",
                table: "Admins",
                newName: "admin_name");

            migrationBuilder.RenameColumn(
                name: "AdminId",
                table: "Admins",
                newName: "admin_id");

            migrationBuilder.RenameIndex(
                name: "IX_Admins_UserId",
                table: "Admins",
                newName: "IX_Admins_user_id");

            migrationBuilder.AlterColumn<string>(
                name: "channel",
                table: "Vtubers",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "vtuber_name",
                table: "Vtubers",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "real_name",
                table: "Vtubers",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "model_url",
                table: "Vtubers",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "updated_at",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<string>(
                name: "species_name",
                table: "Species",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "Products",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<string>(
                name: "product_name",
                table: "Products",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "image_url",
                table: "Products",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<decimal>(
                name: "total_amount",
                table: "Orders",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "order_date",
                table: "Orders",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<decimal>(
                name: "price",
                table: "OrderDetails",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(65,30)");

            migrationBuilder.AlterColumn<string>(
                name: "merchandise_name",
                table: "Merchandises",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "gender",
                table: "Genders",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "nickname",
                table: "Customers",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "phone_number",
                table: "Customers",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "full_name",
                table: "Customers",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "address",
                table: "Companies",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "contact_email",
                table: "Companies",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "company_name",
                table: "Companies",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "category_name",
                table: "Categories",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                table: "Carts",
                type: "datetime(6)",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AlterColumn<string>(
                name: "admin_name",
                table: "Admins",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Users_user_id",
                table: "Admins",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_cart_id",
                table: "CartItems",
                column: "cart_id",
                principalTable: "Carts",
                principalColumn: "cart_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_product_id",
                table: "CartItems",
                column: "product_id",
                principalTable: "Products",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Customers_customer_id",
                table: "Carts",
                column: "customer_id",
                principalTable: "Customers",
                principalColumn: "customer_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Genders_gender_id",
                table: "Customers",
                column: "gender_id",
                principalTable: "Genders",
                principalColumn: "gender_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Users_user_id",
                table: "Customers",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Vtubers_vtuber_id",
                table: "Events",
                column: "vtuber_id",
                principalTable: "Vtubers",
                principalColumn: "vtuber_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Merchandises_Vtubers_vtuber_id",
                table: "Merchandises",
                column: "vtuber_id",
                principalTable: "Vtubers",
                principalColumn: "vtuber_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_order_id",
                table: "OrderDetails",
                column: "order_id",
                principalTable: "Orders",
                principalColumn: "order_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Products_product_id",
                table: "OrderDetails",
                column: "product_id",
                principalTable: "Products",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_customer_id",
                table: "Orders",
                column: "customer_id",
                principalTable: "Customers",
                principalColumn: "customer_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_category_id",
                table: "Products",
                column: "category_id",
                principalTable: "Categories",
                principalColumn: "category_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Merchandises_merchandise_id",
                table: "Products",
                column: "merchandise_id",
                principalTable: "Merchandises",
                principalColumn: "merchandise_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vtubers_Companies_company_id",
                table: "Vtubers",
                column: "company_id",
                principalTable: "Companies",
                principalColumn: "company_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vtubers_Genders_vtuber_gender",
                table: "Vtubers",
                column: "vtuber_gender",
                principalTable: "Genders",
                principalColumn: "gender_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vtubers_Species_species_id",
                table: "Vtubers",
                column: "species_id",
                principalTable: "Species",
                principalColumn: "species_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vtubers_Users_user_id",
                table: "Vtubers",
                column: "user_id",
                principalTable: "Users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admins_Users_user_id",
                table: "Admins");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_cart_id",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Products_product_id",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Customers_customer_id",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Genders_gender_id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Users_user_id",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Events_Vtubers_vtuber_id",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Merchandises_Vtubers_vtuber_id",
                table: "Merchandises");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_order_id",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Products_product_id",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Customers_customer_id",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_category_id",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Merchandises_merchandise_id",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Vtubers_Companies_company_id",
                table: "Vtubers");

            migrationBuilder.DropForeignKey(
                name: "FK_Vtubers_Genders_vtuber_gender",
                table: "Vtubers");

            migrationBuilder.DropForeignKey(
                name: "FK_Vtubers_Species_species_id",
                table: "Vtubers");

            migrationBuilder.DropForeignKey(
                name: "FK_Vtubers_Users_user_id",
                table: "Vtubers");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Vtubers",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "channel",
                table: "Vtubers",
                newName: "Channel");

            migrationBuilder.RenameColumn(
                name: "vtuber_name",
                table: "Vtubers",
                newName: "VtuberName");

            migrationBuilder.RenameColumn(
                name: "vtuber_gender",
                table: "Vtubers",
                newName: "VtuberGender");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Vtubers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "species_id",
                table: "Vtubers",
                newName: "SpeciesId");

            migrationBuilder.RenameColumn(
                name: "real_name",
                table: "Vtubers",
                newName: "RealName");

            migrationBuilder.RenameColumn(
                name: "model_url",
                table: "Vtubers",
                newName: "ModelUrl");

            migrationBuilder.RenameColumn(
                name: "debut_date",
                table: "Vtubers",
                newName: "DebutDate");

            migrationBuilder.RenameColumn(
                name: "company_id",
                table: "Vtubers",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "vtuber_id",
                table: "Vtubers",
                newName: "VtuberId");

            migrationBuilder.RenameIndex(
                name: "IX_Vtubers_vtuber_gender",
                table: "Vtubers",
                newName: "IX_Vtubers_VtuberGender");

            migrationBuilder.RenameIndex(
                name: "IX_Vtubers_user_id",
                table: "Vtubers",
                newName: "IX_Vtubers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Vtubers_species_id",
                table: "Vtubers",
                newName: "IX_Vtubers_SpeciesId");

            migrationBuilder.RenameIndex(
                name: "IX_Vtubers_company_id",
                table: "Vtubers",
                newName: "IX_Vtubers_CompanyId");

            migrationBuilder.RenameColumn(
                name: "role",
                table: "Users",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Users",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Users",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "avatar_url",
                table: "Users",
                newName: "AvatarUrl");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Species",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "species_name",
                table: "Species",
                newName: "SpeciesName");

            migrationBuilder.RenameColumn(
                name: "species_id",
                table: "Species",
                newName: "SpeciesId");

            migrationBuilder.RenameColumn(
                name: "stock",
                table: "Products",
                newName: "Stock");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Products",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Products",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "product_name",
                table: "Products",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "merchandise_id",
                table: "Products",
                newName: "MerchandiseId");

            migrationBuilder.RenameColumn(
                name: "image_url",
                table: "Products",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "category_id",
                table: "Products",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_merchandise_id",
                table: "Products",
                newName: "IX_Products_MerchandiseId");

            migrationBuilder.RenameIndex(
                name: "IX_Products_category_id",
                table: "Products",
                newName: "IX_Products_CategoryId");

            migrationBuilder.RenameColumn(
                name: "total_amount",
                table: "Orders",
                newName: "TotalAmount");

            migrationBuilder.RenameColumn(
                name: "shipping_address",
                table: "Orders",
                newName: "ShippingAddress");

            migrationBuilder.RenameColumn(
                name: "order_date",
                table: "Orders",
                newName: "OrderDate");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                table: "Orders",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "order_id",
                table: "Orders",
                newName: "OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_customer_id",
                table: "Orders",
                newName: "IX_Orders_CustomerId");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "OrderDetails",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "OrderDetails",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "OrderDetails",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "order_id",
                table: "OrderDetails",
                newName: "OrderId");

            migrationBuilder.RenameColumn(
                name: "order_detail_id",
                table: "OrderDetails",
                newName: "OrderDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_product_id",
                table: "OrderDetails",
                newName: "IX_OrderDetails_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_order_id",
                table: "OrderDetails",
                newName: "IX_OrderDetails_OrderId");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Merchandises",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "vtuber_id",
                table: "Merchandises",
                newName: "VtuberId");

            migrationBuilder.RenameColumn(
                name: "start_date",
                table: "Merchandises",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "merchandise_name",
                table: "Merchandises",
                newName: "MerchandiseName");

            migrationBuilder.RenameColumn(
                name: "end_date",
                table: "Merchandises",
                newName: "EndDate");

            migrationBuilder.RenameColumn(
                name: "merchandise_id",
                table: "Merchandises",
                newName: "MerchandiseId");

            migrationBuilder.RenameIndex(
                name: "IX_Merchandises_vtuber_id",
                table: "Merchandises",
                newName: "IX_Merchandises_VtuberId");

            migrationBuilder.RenameColumn(
                name: "gender",
                table: "Genders",
                newName: "GenderType");

            migrationBuilder.RenameColumn(
                name: "gender_id",
                table: "Genders",
                newName: "GenderId");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Events",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "Events",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "vtuber_id",
                table: "Events",
                newName: "VtuberId");

            migrationBuilder.RenameColumn(
                name: "event_id",
                table: "Events",
                newName: "EventId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_vtuber_id",
                table: "Events",
                newName: "IX_Events_VtuberId");

            migrationBuilder.RenameColumn(
                name: "nickname",
                table: "Customers",
                newName: "Nickname");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Customers",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Customers",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "Customers",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "gender_id",
                table: "Customers",
                newName: "GenderId");

            migrationBuilder.RenameColumn(
                name: "full_name",
                table: "Customers",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "birth_date",
                table: "Customers",
                newName: "BirthDate");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                table: "Customers",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_user_id",
                table: "Customers",
                newName: "IX_Customers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Customers_gender_id",
                table: "Customers",
                newName: "IX_Customers_GenderId");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Companies",
                newName: "Address");

            migrationBuilder.RenameColumn(
                name: "contact_email",
                table: "Companies",
                newName: "ContactEmail");

            migrationBuilder.RenameColumn(
                name: "company_name",
                table: "Companies",
                newName: "CompanyName");

            migrationBuilder.RenameColumn(
                name: "company_id",
                table: "Companies",
                newName: "CompanyId");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Categories",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "category_name",
                table: "Categories",
                newName: "CategoryName");

            migrationBuilder.RenameColumn(
                name: "category_id",
                table: "Categories",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "customer_id",
                table: "Carts",
                newName: "CustomerId");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Carts",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "cart_id",
                table: "Carts",
                newName: "CartId");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_customer_id",
                table: "Carts",
                newName: "IX_Carts_CustomerId");

            migrationBuilder.RenameColumn(
                name: "quantity",
                table: "CartItems",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "product_id",
                table: "CartItems",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "cart_id",
                table: "CartItems",
                newName: "CartId");

            migrationBuilder.RenameColumn(
                name: "cart_item_id",
                table: "CartItems",
                newName: "CartItemId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_product_id",
                table: "CartItems",
                newName: "IX_CartItems_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_cart_id",
                table: "CartItems",
                newName: "IX_CartItems_CartId");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Admins",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "admin_name",
                table: "Admins",
                newName: "AdminName");

            migrationBuilder.RenameColumn(
                name: "admin_id",
                table: "Admins",
                newName: "AdminId");

            migrationBuilder.RenameIndex(
                name: "IX_Admins_user_id",
                table: "Admins",
                newName: "IX_Admins_UserId");

            migrationBuilder.AlterColumn<string>(
                name: "Channel",
                table: "Vtubers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "VtuberName",
                table: "Vtubers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "RealName",
                table: "Vtubers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ModelUrl",
                table: "Vtubers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<string>(
                name: "SpeciesName",
                table: "Species",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<string>(
                name: "ProductName",
                table: "Products",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Orders",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Orders",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "OrderDetails",
                type: "decimal(65,30)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AlterColumn<string>(
                name: "MerchandiseName",
                table: "Merchandises",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "GenderType",
                table: "Genders",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Nickname",
                table: "Customers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "Customers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)",
                oldMaxLength: 20)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Customers",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Companies",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "ContactEmail",
                table: "Companies",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyName",
                table: "Companies",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "CategoryName",
                table: "Categories",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Carts",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<string>(
                name: "AdminName",
                table: "Admins",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldMaxLength: 255)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Admins_Users_UserId",
                table: "Admins",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_CartId",
                table: "CartItems",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_ProductId",
                table: "CartItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Customers_CustomerId",
                table: "Carts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Genders_GenderId",
                table: "Customers",
                column: "GenderId",
                principalTable: "Genders",
                principalColumn: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Users_UserId",
                table: "Customers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Vtubers_VtuberId",
                table: "Events",
                column: "VtuberId",
                principalTable: "Vtubers",
                principalColumn: "VtuberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Merchandises_Vtubers_VtuberId",
                table: "Merchandises",
                column: "VtuberId",
                principalTable: "Vtubers",
                principalColumn: "VtuberId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Products_ProductId",
                table: "OrderDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Customers_CustomerId",
                table: "Orders",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Merchandises_MerchandiseId",
                table: "Products",
                column: "MerchandiseId",
                principalTable: "Merchandises",
                principalColumn: "MerchandiseId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vtubers_Companies_CompanyId",
                table: "Vtubers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vtubers_Genders_VtuberGender",
                table: "Vtubers",
                column: "VtuberGender",
                principalTable: "Genders",
                principalColumn: "GenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vtubers_Species_SpeciesId",
                table: "Vtubers",
                column: "SpeciesId",
                principalTable: "Species",
                principalColumn: "SpeciesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vtubers_Users_UserId",
                table: "Vtubers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

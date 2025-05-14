using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VtuberMerchHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class MerchImageURL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Merchandises",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Merchandises");
        }
    }
}

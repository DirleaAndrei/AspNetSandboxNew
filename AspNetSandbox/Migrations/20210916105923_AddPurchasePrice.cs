using Microsoft.EntityFrameworkCore.Migrations;

namespace AspNetSandbox.Migrations
{
    public partial class AddPurchasePrice : Migration
    {
        /// <inheritdoc/>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PurchasePrice",
                table: "Books",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc/>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchasePrice",
                table: "Books");
        }
    }
}

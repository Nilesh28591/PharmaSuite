using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmaSuiteWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class pharmasuitemedicinepurchasess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MinQuantity",
                table: "purchaseItem",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MinQuantity",
                table: "purchaseItem");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AVIV.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAdvertisementStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Advertisements",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Advertisements");
        }
    }
}

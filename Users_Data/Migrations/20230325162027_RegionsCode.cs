using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsersData.Migrations
{
    /// <inheritdoc />
    public partial class RegionsCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "state_code",
                table: "Regions",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "state_code",
                table: "Regions");
        }
    }
}

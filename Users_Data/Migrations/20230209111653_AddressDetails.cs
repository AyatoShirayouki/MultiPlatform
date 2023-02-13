using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsersData.Migrations
{
    /// <inheritdoc />
    public partial class AddressDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AddressInfo",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressInfo",
                table: "Addresses");
        }
    }
}

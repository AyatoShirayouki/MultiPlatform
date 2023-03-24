using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsersData.Migrations
{
    /// <inheritdoc />
    public partial class RemovingNullColumnsUsersDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_at",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "fips_code",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "flag",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "iso2",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "wikiDataId",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "flag",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "wikiDataId",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "flag",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "updated_at",
                table: "Cities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "created_at",
                table: "Regions",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "fips_code",
                table: "Regions",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "flag",
                table: "Regions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "iso2",
                table: "Regions",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "updated_at",
                table: "Regions",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "wikiDataId",
                table: "Regions",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "created_at",
                table: "Countries",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "flag",
                table: "Countries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "updated_at",
                table: "Countries",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "wikiDataId",
                table: "Countries",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "created_at",
                table: "Cities",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "flag",
                table: "Cities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "updated_at",
                table: "Cities",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsersData.Migrations
{
	/// <inheritdoc />
	public partial class Initial : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "AcademicFields",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AcademicFields", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Countries",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
					iso3 = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
					numericcode = table.Column<string>(name: "numeric_code", type: "nvarchar(3)", maxLength: 3, nullable: true),
					iso2 = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
					phonecode = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
					capital = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
					currency = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
					currencyname = table.Column<string>(name: "currency_name", type: "nvarchar(255)", maxLength: 255, nullable: true),
					currencysymbol = table.Column<string>(name: "currency_symbol", type: "nvarchar(255)", maxLength: 255, nullable: true),
					tld = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
					native = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
					region = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
					subregion = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
					timezones = table.Column<string>(type: "nvarchar(max)", nullable: true),
					translations = table.Column<string>(type: "nvarchar(max)", nullable: true),
					latitude = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
					longitude = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
					emoji = table.Column<string>(type: "nvarchar(191)", maxLength: 191, nullable: true),
					emojiU = table.Column<string>(type: "nvarchar(191)", maxLength: 191, nullable: true),
					createdat = table.Column<byte[]>(name: "created_at", type: "varbinary(max)", nullable: true),
					updatedat = table.Column<byte[]>(name: "updated_at", type: "varbinary(max)", nullable: false),
					flag = table.Column<int>(type: "int", nullable: false),
					wikiDataId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Countries", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Degrees",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Degrees", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "EducationalFacilityTypes",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_EducationalFacilityTypes", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "PricingPlans",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
					Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
					Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_PricingPlans", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Regions",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
					countryid = table.Column<int>(name: "country_id", type: "int", nullable: false),
					countrycode = table.Column<string>(name: "country_code", type: "nvarchar(2)", maxLength: 2, nullable: false),
					fipscode = table.Column<string>(name: "fips_code", type: "nvarchar(255)", maxLength: 255, nullable: true),
					iso2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
					type = table.Column<string>(type: "nvarchar(191)", maxLength: 191, nullable: true),
					latitude = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
					longitude = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
					createdat = table.Column<byte[]>(name: "created_at", type: "varbinary(max)", nullable: true),
					updatedat = table.Column<byte[]>(name: "updated_at", type: "varbinary(max)", nullable: false),
					flag = table.Column<int>(type: "int", nullable: false),
					wikiDataId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Regions", x => x.Id);
					table.ForeignKey(
						name: "FK_Regions_Countries_country_id",
						column: x => x.countryid,
						principalTable: "Countries",
						principalColumn: "Id",
						onDelete: ReferentialAction.NoAction);
				});

			migrationBuilder.CreateTable(
				name: "EducationalFacilities",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
					Website = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
					EducationalFacilityTypeId = table.Column<int>(type: "int", nullable: false),
					CountryId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_EducationalFacilities", x => x.Id);
					table.ForeignKey(
						name: "FK_EducationalFacilities_Countries_CountryId",
						column: x => x.CountryId,
						principalTable: "Countries",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_EducationalFacilities_EducationalFacilityTypes_EducationalFacilityTypeId",
						column: x => x.EducationalFacilityTypeId,
						principalTable: "EducationalFacilityTypes",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "PricingPlanFeatures",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					PricingPlanId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_PricingPlanFeatures", x => x.Id);
					table.ForeignKey(
						name: "FK_PricingPlanFeatures_PricingPlans_PricingPlanId",
						column: x => x.PricingPlanId,
						principalTable: "PricingPlans",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Cities",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
					regionid = table.Column<int>(name: "region_id", type: "int", nullable: false),
					regioncode = table.Column<int>(name: "region_code", type: "int", maxLength: 2, nullable: false),
					countryid = table.Column<int>(name: "country_id", type: "int", nullable: false),
					countrycode = table.Column<string>(name: "country_code", type: "nvarchar(2)", maxLength: 2, nullable: false),
					latitude = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
					longitude = table.Column<decimal>(type: "decimal(18,8)", nullable: false),
					createdat = table.Column<byte[]>(name: "created_at", type: "varbinary(max)", nullable: true),
					updatedat = table.Column<byte[]>(name: "updated_at", type: "varbinary(max)", nullable: false),
					flag = table.Column<int>(type: "int", nullable: false),
					wikiDataId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Cities", x => x.Id);
					table.ForeignKey(
						name: "FK_Cities_Countries_country_id",
						column: x => x.countryid,
						principalTable: "Countries",
						principalColumn: "Id",
						onDelete: ReferentialAction.NoAction);
					table.ForeignKey(
						name: "FK_Cities_Regions_region_id",
						column: x => x.regionid,
						principalTable: "Regions",
						principalColumn: "Id",
						onDelete: ReferentialAction.NoAction);
				});

			migrationBuilder.CreateTable(
				name: "Addresses",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					CountryId = table.Column<int>(type: "int", nullable: false),
					RegionId = table.Column<int>(type: "int", nullable: false),
					CityId = table.Column<int>(type: "int", nullable: false),
					AddressInfo = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Addresses", x => x.Id);
					table.ForeignKey(
						name: "FK_Addresses_Cities_CityId",
						column: x => x.CityId,
						principalTable: "Cities",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Addresses_Countries_CountryId",
						column: x => x.CountryId,
						principalTable: "Countries",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Addresses_Regions_RegionId",
						column: x => x.RegionId,
						principalTable: "Regions",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Users",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Gender = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
					PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
					Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
					LinkedInAccount = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: true),
					IsCompany = table.Column<bool>(type: "bit", nullable: false),
					CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
					AccountType = table.Column<int>(type: "int", nullable: false),
					AddressId = table.Column<int>(type: "int", nullable: false),
					PricingPlanId = table.Column<int>(type: "int", nullable: false),
					Email = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
					Password = table.Column<string>(type: "nvarchar(120)", maxLength: 120, nullable: false),
					FirstName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
					LastName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
					DOB = table.Column<DateTime>(type: "datetime2", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Users", x => x.Id);
					table.ForeignKey(
						name: "FK_Users_Addresses_AddressId",
						column: x => x.AddressId,
						principalTable: "Addresses",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Users_PricingPlans_PricingPlanId",
						column: x => x.PricingPlanId,
						principalTable: "PricingPlans",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "RefreshUserTokens",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserId = table.Column<int>(type: "int", nullable: false),
					Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
					JwtId = table.Column<string>(type: "nvarchar(max)", nullable: true),
					IsUsed = table.Column<bool>(type: "bit", nullable: false),
					IsRevorked = table.Column<bool>(type: "bit", nullable: false),
					AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
					ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_RefreshUserTokens", x => x.Id);
					table.ForeignKey(
						name: "FK_RefreshUserTokens_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "UserEducations",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Speacialty = table.Column<string>(type: "nvarchar(max)", nullable: true),
					UserId = table.Column<int>(type: "int", nullable: false),
					DegreeId = table.Column<int>(type: "int", nullable: false),
					AcademicFieldId = table.Column<int>(type: "int", nullable: false),
					EducationalFacilityId = table.Column<int>(type: "int", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UserEducations", x => x.Id);
					table.ForeignKey(
						name: "FK_UserEducations_AcademicFields_AcademicFieldId",
						column: x => x.AcademicFieldId,
						principalTable: "AcademicFields",
						principalColumn: "Id",
						onDelete: ReferentialAction.NoAction);
					table.ForeignKey(
						name: "FK_UserEducations_Degrees_DegreeId",
						column: x => x.DegreeId,
						principalTable: "Degrees",
						principalColumn: "Id",
						onDelete: ReferentialAction.NoAction);
					table.ForeignKey(
						name: "FK_UserEducations_EducationalFacilities_EducationalFacilityId",
						column: x => x.EducationalFacilityId,
						principalTable: "EducationalFacilities",
						principalColumn: "Id",
						onDelete: ReferentialAction.NoAction);
					table.ForeignKey(
						name: "FK_UserEducations_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.NoAction);
				});

			migrationBuilder.CreateTable(
				name: "UserFiles",
				columns: table => new
				{
					Id = table.Column<int>(type: "int", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserId = table.Column<int>(type: "int", nullable: false),
					IsProfilePhoto = table.Column<bool>(type: "bit", nullable: false),
					IsCoverPhoto = table.Column<bool>(type: "bit", nullable: false),
					IsCV = table.Column<bool>(type: "bit", nullable: false),
					FileName = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
					FileType = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_UserFiles", x => x.Id);
					table.ForeignKey(
						name: "FK_UserFiles_Users_UserId",
						column: x => x.UserId,
						principalTable: "Users",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateIndex(
				name: "IX_Addresses_CityId",
				table: "Addresses",
				column: "CityId");

			migrationBuilder.CreateIndex(
				name: "IX_Addresses_CountryId",
				table: "Addresses",
				column: "CountryId");

			migrationBuilder.CreateIndex(
				name: "IX_Addresses_RegionId",
				table: "Addresses",
				column: "RegionId");

			migrationBuilder.CreateIndex(
				name: "IX_Cities_country_id",
				table: "Cities",
				column: "country_id");

			migrationBuilder.CreateIndex(
				name: "IX_Cities_region_id",
				table: "Cities",
				column: "region_id");

			migrationBuilder.CreateIndex(
				name: "IX_EducationalFacilities_CountryId",
				table: "EducationalFacilities",
				column: "CountryId");

			migrationBuilder.CreateIndex(
				name: "IX_EducationalFacilities_EducationalFacilityTypeId",
				table: "EducationalFacilities",
				column: "EducationalFacilityTypeId");

			migrationBuilder.CreateIndex(
				name: "IX_PricingPlanFeatures_PricingPlanId",
				table: "PricingPlanFeatures",
				column: "PricingPlanId");

			migrationBuilder.CreateIndex(
				name: "IX_RefreshUserTokens_UserId",
				table: "RefreshUserTokens",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_Regions_country_id",
				table: "Regions",
				column: "country_id");

			migrationBuilder.CreateIndex(
				name: "IX_UserEducations_AcademicFieldId",
				table: "UserEducations",
				column: "AcademicFieldId");

			migrationBuilder.CreateIndex(
				name: "IX_UserEducations_DegreeId",
				table: "UserEducations",
				column: "DegreeId");

			migrationBuilder.CreateIndex(
				name: "IX_UserEducations_EducationalFacilityId",
				table: "UserEducations",
				column: "EducationalFacilityId");

			migrationBuilder.CreateIndex(
				name: "IX_UserEducations_UserId",
				table: "UserEducations",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_UserFiles_UserId",
				table: "UserFiles",
				column: "UserId");

			migrationBuilder.CreateIndex(
				name: "IX_Users_AddressId",
				table: "Users",
				column: "AddressId");

			migrationBuilder.CreateIndex(
				name: "IX_Users_PricingPlanId",
				table: "Users",
				column: "PricingPlanId");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "PricingPlanFeatures");

			migrationBuilder.DropTable(
				name: "RefreshUserTokens");

			migrationBuilder.DropTable(
				name: "UserEducations");

			migrationBuilder.DropTable(
				name: "UserFiles");

			migrationBuilder.DropTable(
				name: "AcademicFields");

			migrationBuilder.DropTable(
				name: "Degrees");

			migrationBuilder.DropTable(
				name: "EducationalFacilities");

			migrationBuilder.DropTable(
				name: "Users");

			migrationBuilder.DropTable(
				name: "EducationalFacilityTypes");

			migrationBuilder.DropTable(
				name: "Addresses");

			migrationBuilder.DropTable(
				name: "PricingPlans");

			migrationBuilder.DropTable(
				name: "Cities");

			migrationBuilder.DropTable(
				name: "Regions");

			migrationBuilder.DropTable(
				name: "Countries");
		}
	}
}

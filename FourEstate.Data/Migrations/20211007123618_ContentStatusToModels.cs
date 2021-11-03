using Microsoft.EntityFrameworkCore.Migrations;

namespace FourEstate.Data.Migrations
{
    public partial class ContentStatusToModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stauts",
                table: "RealEstates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stauts",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stauts",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stauts",
                table: "Contracts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stauts",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stauts",
                table: "Advertisements",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stauts",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "Stauts",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "Stauts",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Stauts",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "Stauts",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "Stauts",
                table: "Advertisements");
        }
    }
}

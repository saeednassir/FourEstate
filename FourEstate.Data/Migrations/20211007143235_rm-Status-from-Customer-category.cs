using Microsoft.EntityFrameworkCore.Migrations;

namespace FourEstate.Data.Migrations
{
    public partial class rmStatusfromCustomercategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Stauts",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Stauts",
                table: "Categories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Stauts",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stauts",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

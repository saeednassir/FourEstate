using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FourEstate.Data.Migrations
{
    public partial class RealEstateAttachment_Model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "RealEstates");

            migrationBuilder.CreateTable(
                name: "RealEstatetAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RealEstateId = table.Column<int>(type: "int", nullable: false),
                    AttachmentUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealEstatetAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RealEstatetAttachments_RealEstates_RealEstateId",
                        column: x => x.RealEstateId,
                        principalTable: "RealEstates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RealEstatetAttachments_RealEstateId",
                table: "RealEstatetAttachments",
                column: "RealEstateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RealEstatetAttachments");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "RealEstates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });
        }
    }
}

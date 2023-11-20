using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManoExperta.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProfessionalCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionalCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProfessionalCategoryProfessionalProfile",
                columns: table => new
                {
                    CategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfessionalsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionalCategoryProfessionalProfile", x => new { x.CategoriesId, x.ProfessionalsId });
                    table.ForeignKey(
                        name: "FK_ProfessionalCategoryProfessionalProfile_ProfessionalCategories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "ProfessionalCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProfessionalCategoryProfessionalProfile_ProfessionalProfiles_ProfessionalsId",
                        column: x => x.ProfessionalsId,
                        principalTable: "ProfessionalProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionalCategoryProfessionalProfile_ProfessionalsId",
                table: "ProfessionalCategoryProfessionalProfile",
                column: "ProfessionalsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProfessionalCategoryProfessionalProfile");

            migrationBuilder.DropTable(
                name: "ProfessionalCategories");
        }
    }
}

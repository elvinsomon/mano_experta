using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManoExperta.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Email_ContactInformation_ContactInformationId",
                table: "Email");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumber_ContactInformation_ContactInformationId",
                table: "PhoneNumber");

            migrationBuilder.DropTable(
                name: "ContactInformation");

            migrationBuilder.RenameColumn(
                name: "ContactInformationId",
                table: "PhoneNumber",
                newName: "ProfessionalProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneNumber_ContactInformationId",
                table: "PhoneNumber",
                newName: "IX_PhoneNumber_ProfessionalProfileId");

            migrationBuilder.RenameColumn(
                name: "ContactInformationId",
                table: "Email",
                newName: "ProfessionalProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Email_ContactInformationId",
                table: "Email",
                newName: "IX_Email_ProfessionalProfileId");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "ProfessionalCategories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserName",
                table: "Users",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionalCategories_Code",
                table: "ProfessionalCategories",
                column: "Code",
                unique: true,
                filter: "[Code] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Email_ProfessionalProfiles_ProfessionalProfileId",
                table: "Email",
                column: "ProfessionalProfileId",
                principalTable: "ProfessionalProfiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumber_ProfessionalProfiles_ProfessionalProfileId",
                table: "PhoneNumber",
                column: "ProfessionalProfileId",
                principalTable: "ProfessionalProfiles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Email_ProfessionalProfiles_ProfessionalProfileId",
                table: "Email");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumber_ProfessionalProfiles_ProfessionalProfileId",
                table: "PhoneNumber");

            migrationBuilder.DropIndex(
                name: "IX_Users_UserName",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_ProfessionalCategories_Code",
                table: "ProfessionalCategories");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "ProfessionalCategories");

            migrationBuilder.RenameColumn(
                name: "ProfessionalProfileId",
                table: "PhoneNumber",
                newName: "ContactInformationId");

            migrationBuilder.RenameIndex(
                name: "IX_PhoneNumber_ProfessionalProfileId",
                table: "PhoneNumber",
                newName: "IX_PhoneNumber_ContactInformationId");

            migrationBuilder.RenameColumn(
                name: "ProfessionalProfileId",
                table: "Email",
                newName: "ContactInformationId");

            migrationBuilder.RenameIndex(
                name: "IX_Email_ProfessionalProfileId",
                table: "Email",
                newName: "IX_Email_ContactInformationId");

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ContactInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfessionalProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactInformation_ProfessionalProfiles_ProfessionalProfileId",
                        column: x => x.ProfessionalProfileId,
                        principalTable: "ProfessionalProfiles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactInformation_ProfessionalProfileId",
                table: "ContactInformation",
                column: "ProfessionalProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Email_ContactInformation_ContactInformationId",
                table: "Email",
                column: "ContactInformationId",
                principalTable: "ContactInformation",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumber_ContactInformation_ContactInformationId",
                table: "PhoneNumber",
                column: "ContactInformationId",
                principalTable: "ContactInformation",
                principalColumn: "Id");
        }
    }
}

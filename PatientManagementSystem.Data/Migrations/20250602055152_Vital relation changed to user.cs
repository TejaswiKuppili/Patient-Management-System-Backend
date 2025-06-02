using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class Vitalrelationchangedtouser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vitals_Employees_CreatedBy",
                table: "Vitals");

            migrationBuilder.AddForeignKey(
                name: "FK_Vitals_ApplicationUsers_CreatedBy",
                table: "Vitals",
                column: "CreatedBy",
                principalTable: "ApplicationUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vitals_ApplicationUsers_CreatedBy",
                table: "Vitals");

            migrationBuilder.AddForeignKey(
                name: "FK_Vitals_Employees_CreatedBy",
                table: "Vitals",
                column: "CreatedBy",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

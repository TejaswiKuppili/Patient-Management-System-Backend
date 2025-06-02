using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateEmployeeToUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Employees_CreatedBy",
                table: "Patients");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_ApplicationUsers_CreatedBy",
                table: "Patients",
                column: "CreatedBy",
                principalTable: "ApplicationUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_ApplicationUsers_CreatedBy",
                table: "Patients");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Employees_CreatedBy",
                table: "Patients",
                column: "CreatedBy",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

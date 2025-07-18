using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class add_Collumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SpecialtyId",
                table: "ApplicationUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_SpecialtyId",
                table: "ApplicationUsers",
                column: "SpecialtyId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUsers_Specialties_SpecialtyId",
                table: "ApplicationUsers",
                column: "SpecialtyId",
                principalTable: "Specialties",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationUsers_Specialties_SpecialtyId",
                table: "ApplicationUsers");

            migrationBuilder.DropIndex(
                name: "IX_ApplicationUsers_SpecialtyId",
                table: "ApplicationUsers");

            migrationBuilder.DropColumn(
                name: "SpecialtyId",
                table: "ApplicationUsers");
        }
    }
}

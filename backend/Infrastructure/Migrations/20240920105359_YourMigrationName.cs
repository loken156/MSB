using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class YourMigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRoles_AspNetRoles_IdentityRoleId",
                table: "EmployeeRoles");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "RoleIds",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "IdentityRoleId",
                table: "EmployeeRoles",
                newName: "RolesId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeRoles_IdentityRoleId",
                table: "EmployeeRoles",
                newName: "IX_EmployeeRoles_RolesId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeRoles_AspNetRoles_RolesId",
                table: "EmployeeRoles",
                column: "RolesId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeRoles_AspNetRoles_RolesId",
                table: "EmployeeRoles");

            migrationBuilder.RenameColumn(
                name: "RolesId",
                table: "EmployeeRoles",
                newName: "IdentityRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeRoles_RolesId",
                table: "EmployeeRoles",
                newName: "IX_EmployeeRoles_IdentityRoleId");

            migrationBuilder.AddColumn<string>(
                name: "RoleId",
                table: "Employees",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "RoleIds",
                table: "Employees",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeRoles_AspNetRoles_IdentityRoleId",
                table: "EmployeeRoles",
                column: "IdentityRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

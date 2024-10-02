using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class boxfixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "Boxes");

            migrationBuilder.AddColumn<int>(
                name: "AvailableLargeSlots",
                table: "Shelves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AvailableMediumSlots",
                table: "Shelves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AvailableSmallSlots",
                table: "Shelves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ShelfModelShelfId",
                table: "BoxTypes",
                type: "char(36)",
                nullable: true,
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_BoxTypes_ShelfModelShelfId",
                table: "BoxTypes",
                column: "ShelfModelShelfId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoxTypes_Shelves_ShelfModelShelfId",
                table: "BoxTypes",
                column: "ShelfModelShelfId",
                principalTable: "Shelves",
                principalColumn: "ShelfId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoxTypes_Shelves_ShelfModelShelfId",
                table: "BoxTypes");

            migrationBuilder.DropIndex(
                name: "IX_BoxTypes_ShelfModelShelfId",
                table: "BoxTypes");

            migrationBuilder.DropColumn(
                name: "AvailableLargeSlots",
                table: "Shelves");

            migrationBuilder.DropColumn(
                name: "AvailableMediumSlots",
                table: "Shelves");

            migrationBuilder.DropColumn(
                name: "AvailableSmallSlots",
                table: "Shelves");

            migrationBuilder.DropColumn(
                name: "ShelfModelShelfId",
                table: "BoxTypes");

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Boxes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}

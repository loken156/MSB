using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddShelfFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShelfRow",
                table: "Shelves",
                newName: "SmallBoxCapacity");

            migrationBuilder.AddColumn<int>(
                name: "LargeBoxCapacity",
                table: "Shelves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MediumBoxCapacity",
                table: "Shelves",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Section",
                table: "Shelves",
                type: "varchar(1)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ShelfRows",
                table: "Shelves",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LargeBoxCapacity",
                table: "Shelves");

            migrationBuilder.DropColumn(
                name: "MediumBoxCapacity",
                table: "Shelves");

            migrationBuilder.DropColumn(
                name: "Section",
                table: "Shelves");

            migrationBuilder.DropColumn(
                name: "ShelfRows",
                table: "Shelves");

            migrationBuilder.RenameColumn(
                name: "SmallBoxCapacity",
                table: "Shelves",
                newName: "ShelfRow");
        }
    }
}

using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBoxModelSizeRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Size",
                table: "Boxes");

            migrationBuilder.AddColumn<int>(
                name: "BoxTypeId",
                table: "Boxes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BoxTypes",
                columns: table => new
                {
                    BoxTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Size = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoxTypes", x => x.BoxTypeId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Boxes_BoxTypeId",
                table: "Boxes",
                column: "BoxTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Boxes_BoxTypes_BoxTypeId",
                table: "Boxes",
                column: "BoxTypeId",
                principalTable: "BoxTypes",
                principalColumn: "BoxTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boxes_BoxTypes_BoxTypeId",
                table: "Boxes");

            migrationBuilder.DropTable(
                name: "BoxTypes");

            migrationBuilder.DropIndex(
                name: "IX_Boxes_BoxTypeId",
                table: "Boxes");

            migrationBuilder.DropColumn(
                name: "BoxTypeId",
                table: "Boxes");

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Boxes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}

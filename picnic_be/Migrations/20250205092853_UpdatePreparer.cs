using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace picnic_be.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePreparer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "PlanTools",
                newName: "Count");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "PlanFoods",
                newName: "Count");

            migrationBuilder.CreateTable(
                name: "PreparerFood",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PlanFoodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreparerFood", x => new { x.PlanFoodId, x.UserId });
                    table.ForeignKey(
                        name: "FK_PreparerFood_PlanFoods_PlanFoodId",
                        column: x => x.PlanFoodId,
                        principalTable: "PlanFoods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreparerFood_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreparerTool",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PlanToolId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreparerTool", x => new { x.PlanToolId, x.UserId });
                    table.ForeignKey(
                        name: "FK_PreparerTool_PlanTools_PlanToolId",
                        column: x => x.PlanToolId,
                        principalTable: "PlanTools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PreparerTool_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PreparerFood_UserId",
                table: "PreparerFood",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PreparerTool_UserId",
                table: "PreparerTool",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PreparerFood");

            migrationBuilder.DropTable(
                name: "PreparerTool");

            migrationBuilder.RenameColumn(
                name: "Count",
                table: "PlanTools",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Count",
                table: "PlanFoods",
                newName: "UserId");
        }
    }
}

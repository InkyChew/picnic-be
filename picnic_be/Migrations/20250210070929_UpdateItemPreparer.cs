using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace picnic_be.Migrations
{
    /// <inheritdoc />
    public partial class UpdateItemPreparer : Migration
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
                name: "FoodPreparers",
                columns: table => new
                {
                    FoodsId = table.Column<int>(type: "int", nullable: false),
                    PreparersPlanId = table.Column<int>(type: "int", nullable: false),
                    PreparersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodPreparers", x => new { x.FoodsId, x.PreparersPlanId, x.PreparersUserId });
                    table.ForeignKey(
                        name: "FK_FoodPreparers_PlanFoods_FoodsId",
                        column: x => x.FoodsId,
                        principalTable: "PlanFoods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodPreparers_PlanUsers_PreparersPlanId_PreparersUserId",
                        columns: x => new { x.PreparersPlanId, x.PreparersUserId },
                        principalTable: "PlanUsers",
                        principalColumns: new[] { "PlanId", "UserId" },
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ToolPreparers",
                columns: table => new
                {
                    ToolsId = table.Column<int>(type: "int", nullable: false),
                    PreparersPlanId = table.Column<int>(type: "int", nullable: false),
                    PreparersUserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolPreparers", x => new { x.ToolsId, x.PreparersPlanId, x.PreparersUserId });
                    table.ForeignKey(
                        name: "FK_ToolPreparers_PlanTools_ToolsId",
                        column: x => x.ToolsId,
                        principalTable: "PlanTools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ToolPreparers_PlanUsers_PreparersPlanId_PreparersUserId",
                        columns: x => new { x.PreparersPlanId, x.PreparersUserId },
                        principalTable: "PlanUsers",
                        principalColumns: new[] { "PlanId", "UserId" },
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FoodPreparers_PreparersPlanId_PreparersUserId",
                table: "FoodPreparers",
                columns: new[] { "PreparersPlanId", "PreparersUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_ToolPreparers_PreparersPlanId_PreparersUserId",
                table: "ToolPreparers",
                columns: new[] { "PreparersPlanId", "PreparersUserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodPreparers");

            migrationBuilder.DropTable(
                name: "ToolPreparers");

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

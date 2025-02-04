using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace picnic_be.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePlanItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "PlanTools",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "PlanFoods",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "PlanTools");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "PlanFoods");
        }
    }
}

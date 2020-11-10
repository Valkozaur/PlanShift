namespace PlanShift.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ShiftAddedDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Shifts",
                maxLength: 300,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Shifts");
        }
    }
}

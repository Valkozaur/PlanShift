using Microsoft.EntityFrameworkCore.Migrations;

namespace PlanShift.Data.Migrations
{
    public partial class ShiftChangesIsApprovedByOriginalEmployeeBoolean : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApprovedByOriginalEmployee",
                table: "ShiftChanges",
                type: "bit",
                nullable: true,
                defaultValue: null);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsApprovedByOriginalEmployee",
                table: "ShiftChanges");
        }
    }
}

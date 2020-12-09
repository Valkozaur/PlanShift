namespace PlanShift.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class EmployeeGroupIsManagementBoolean : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsHrManagement",
                table: "EmployeeGroups",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsHrManagement",
                table: "EmployeeGroups");
        }
    }
}

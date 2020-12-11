namespace PlanShift.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RemoveOfUnneededBooleansEmployeeGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "EmployeeGroups");

            migrationBuilder.DropColumn(
                name: "IsHrManagement",
                table: "EmployeeGroups");

            migrationBuilder.DropColumn(
                name: "IsScheduleManager",
                table: "EmployeeGroups");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "EmployeeGroups",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHrManagement",
                table: "EmployeeGroups",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsScheduleManager",
                table: "EmployeeGroups",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

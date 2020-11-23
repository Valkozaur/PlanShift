namespace PlanShift.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class EnumerationAddedToShiftApplicationAndShiftChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "ShiftChanges");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "ShiftApplications");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ShiftChanges",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ShiftApplications",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "ShiftChanges");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ShiftApplications");

            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "ShiftChanges",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "ShiftApplications",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

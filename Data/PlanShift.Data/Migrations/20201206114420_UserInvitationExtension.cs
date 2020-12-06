using Microsoft.EntityFrameworkCore.Migrations;

namespace PlanShift.Data.Migrations
{
    public partial class UserInvitationExtension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "InviteEmployeeVerifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroupId",
                table: "InviteEmployeeVerifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "InviteEmployeeVerifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "InviteEmployeeVerifications",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "InviteEmployeeVerifications");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "InviteEmployeeVerifications");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "InviteEmployeeVerifications");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "InviteEmployeeVerifications");
        }
    }
}

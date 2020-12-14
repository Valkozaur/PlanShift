namespace PlanShift.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class UserInvitationExtension : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "InviteEmployeeVerification",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GroupId",
                table: "InviteEmployeeVerification",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "InviteEmployeeVerification",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "InviteEmployeeVerification",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "InviteEmployeeVerification");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "InviteEmployeeVerification");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "InviteEmployeeVerification");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "InviteEmployeeVerification");
        }
    }
}

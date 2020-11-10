namespace PlanShift.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ShiftApplicationsIdKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShiftApplications",
                table: "ShiftApplications");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "ShiftApplications",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "ShiftApplications",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ShiftId",
                table: "ShiftApplications",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShiftApplications",
                table: "ShiftApplications",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ShiftApplications_ShiftId",
                table: "ShiftApplications",
                column: "ShiftId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShiftApplications",
                table: "ShiftApplications");

            migrationBuilder.DropIndex(
                name: "IX_ShiftApplications_ShiftId",
                table: "ShiftApplications");

            migrationBuilder.AlterColumn<string>(
                name: "ShiftId",
                table: "ShiftApplications",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EmployeeId",
                table: "ShiftApplications",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "ShiftApplications",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShiftApplications",
                table: "ShiftApplications",
                columns: new[] { "ShiftId", "EmployeeId" });
        }
    }
}

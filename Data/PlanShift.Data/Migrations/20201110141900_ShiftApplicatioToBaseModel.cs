namespace PlanShift.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ShiftApplicatioToBaseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "ShiftApplications",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "ShiftApplications",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "ShiftApplications");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "ShiftApplications");
        }
    }
}

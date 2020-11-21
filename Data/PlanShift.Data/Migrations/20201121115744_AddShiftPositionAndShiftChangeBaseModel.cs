namespace PlanShift.Data.Migrations
{
    using System;

    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddShiftPositionAndShiftChangeBaseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ShiftChanges_IsDeleted",
                table: "ShiftChanges");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "ShiftChanges");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ShiftChanges");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Shifts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "Shifts");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "ShiftChanges",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ShiftChanges",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_ShiftChanges_IsDeleted",
                table: "ShiftChanges",
                column: "IsDeleted");
        }
    }
}

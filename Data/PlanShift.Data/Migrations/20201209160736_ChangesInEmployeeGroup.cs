namespace PlanShift.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChangesInEmployeeGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsApprovedByOriginalEmployee",
                table: "ShiftChanges",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
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

            migrationBuilder.Sql(
                "CREATE TRIGGER InsertMandatoryGroups" +
                "ON dbo.Businesses" +
                "AFTER INSERT" +
                "AS" +
                "BEGIN" +
                "declare @BusinessId NVARCHAR(250)" +
                "SELECT @BusinessId = ID from inserted" +
                "declare @OwnerId NVARCHAR(250)" +
                "SELECT @OwnerId = OwnerId from inserted" +
                "declare @AdminGroupId NVARCHAR(250) = NEWID()" +
                "declare @HRGroupId NVARCHAR(250) = NEWID()" +
                "declare @SheduleManagerGroupId NVARCHAR(250) = NEWID()" +
                "INSERT INTO Groups(Id, CreatedOn, Name, StandardSalary, BusinessId, IsDeleted)" +
                "VALUES" +
                "(@AdminGroupId, GETDATE(), 'Admins', 0, @BusinessId, 0)," +
                "(@HRGroupId, GETDATE(), 'HR Managers', 0, @BusinessId, 0)," +
                "(@SheduleManagerGroupId, GETDATE(), 'Schedule Managers', 0, @BusinessId, 0)" +
                "INSERT INTO EmployeeGroups(Id, CreatedOn, EmployeeId, GroupId, Position, Salary, IsDeleted)" +
                "VALUES" +
                "(NEWID(), GETDATE(), @OwnerId, @AdminGroupId, 'Owner', 0, 0)," +
                "(NEWID(), GETDATE(), @OwnerId, @HRGroupId, 'Owner', 0, 0)," +
                "(NEWID(), GETDATE(), @OwnerId, @SheduleManagerGroupId, 'Owner', 0, 0)" +
                "END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "EmployeeGroups");

            migrationBuilder.DropColumn(
                name: "IsScheduleManager",
                table: "EmployeeGroups");

            migrationBuilder.AlterColumn<bool>(
                name: "IsApprovedByOriginalEmployee",
                table: "ShiftChanges",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.Sql("DROP TRIGGER InsertMandatoryGroups");
        }
    }
}

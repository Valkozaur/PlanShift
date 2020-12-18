namespace PlanShift.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class IntroduceTrigger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql(
            //    @"CREATE TRIGGER InsertMandatoryGroups" +
            //    @"ON dbo.Businesses" +
            //    @"AFTER INSERT" +
            //    @"AS" +
            //    @"BEGIN" +
            //    @"DECLARE @BusinessId NVARCHAR(450)" +
            //    @"SELECT @BusinessId = ID from inserted" +
            //    @"DECLARE @OwnerId NVARCHAR(450)" +
            //    @"DECLARE @OwnerId = OwnerId from inserted" +
            //    @"DECLARE @AdminGroupId NVARCHAR(450) = NEWID()" +
            //    @"DECLARE @HRGroupId NVARCHAR(450) = NEWID()" +
            //    @"DECLARE @SheduleManagerGroupId NVARCHAR(450) = NEWID()" +
            //    @"INSERT INTO Groups(Id, CreatedOn, Name, StandardSalary, BusinessId, IsDeleted)" +
            //    @"VALUES" +
            //    @"(@AdminGroupId, GETDATE(), 'Admins', 0, @BusinessId, 0)," +
            //    @"(@HRGroupId, GETDATE(), 'HR Managers', 0, @BusinessId, 0)," +
            //    @"(@SheduleManagerGroupId, GETDATE(), 'Schedule Managers', 0, @BusinessId, 0)" +
            //    @"INSERT INTO EmployeeGroups(Id, CreatedOn, UserId, GroupId, Position, Salary, IsDeleted)" +
            //    @"VALUES" +
            //    @"(NEWID(), GETDATE(), @OwnerId, @AdminGroupId, 'Owner', 0, 0)," +
            //    @"(NEWID(), GETDATE(), @OwnerId, @HRGroupId, 'Owner', 0, 0)," +
            //    @"(NEWID(), GETDATE(), @OwnerId, @SheduleManagerGroupId, 'Owner', 0, 0)" +
            //    @"END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            //migrationBuilder.Sql("DROP TRIGGER InsertMandatoryGroups");
        }
    }
}

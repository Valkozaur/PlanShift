namespace PlanShift.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class EntitiesPropertiesIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Businesses_Name",
                table: "Businesses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers",
                column: "UserName",
                unique: true,
                filter: "[UserName] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Businesses_Name",
                table: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserName",
                table: "AspNetUsers");
        }
    }
}

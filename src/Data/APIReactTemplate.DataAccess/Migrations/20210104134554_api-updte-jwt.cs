using Microsoft.EntityFrameworkCore.Migrations;

namespace APIReactTemplate.DataAccess.Migrations
{
    public partial class apiupdtejwt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsExpired",
                table: "JWT",
                newName: "IsActive");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "JWT",
                newName: "IsExpired");
        }
    }
}

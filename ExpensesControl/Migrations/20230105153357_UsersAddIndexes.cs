using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpensesControl.Migrations
{
    public partial class UsersAddIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex("Index_ID", "Users", "Id");
            migrationBuilder.CreateIndex("Index_Name", "Users", "Name");
            migrationBuilder.CreateIndex("Index_Email", "Users", "Email");
            migrationBuilder.CreateIndex("Index_Status", "Users", "Status");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace serverApp.Migrations
{
    public partial class updateProductDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE Products ADD Stock INT, Available BIT");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace SDGE.Infrastructure.Migrations
{
    public partial class DBTR : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Removido",
                table: "ComissaoOrganizadora",
                type: "varchar(250)",
                nullable: false,
                defaultValue: "False",
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Removido",
                table: "ComissaoOrganizadora",
                type: "bit",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(250)",
                oldDefaultValue: "False");
        }
    }
}

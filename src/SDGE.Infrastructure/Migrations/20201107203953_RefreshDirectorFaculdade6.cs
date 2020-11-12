using Microsoft.EntityFrameworkCore.Migrations;

namespace SDGE.Infrastructure.Migrations
{
    public partial class RefreshDirectorFaculdade6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Director_FaculdadeId",
                table: "Director");

            migrationBuilder.AlterColumn<string>(
                name: "Designacao",
                table: "Faculdade",
                type: "varchar(200)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(500)");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Director",
                type: "varchar(20)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Director",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(500)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Director",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(250)");

            migrationBuilder.CreateIndex(
                name: "IX_Director_FaculdadeId",
                table: "Director",
                column: "FaculdadeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Director_FaculdadeId",
                table: "Director");

            migrationBuilder.AlterColumn<string>(
                name: "Designacao",
                table: "Faculdade",
                type: "varchar(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(200)");

            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "Director",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(20)");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Director",
                type: "varchar(500)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Director",
                type: "varchar(250)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.CreateIndex(
                name: "IX_Director_FaculdadeId",
                table: "Director",
                column: "FaculdadeId",
                unique: true);
        }
    }
}

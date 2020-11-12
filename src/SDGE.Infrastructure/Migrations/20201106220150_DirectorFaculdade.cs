using Microsoft.EntityFrameworkCore.Migrations;

namespace SDGE.Infrastructure.Migrations
{
    public partial class DirectorFaculdade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Director",
                columns: table => new
                {
                    DirectorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(500)", nullable: false),
                    Email = table.Column<string>(type: "varchar(250)", nullable: false),
                    Telefone = table.Column<string>(type: "varchar(50)", nullable: false),
                    FaculdadeId = table.Column<int>(nullable: false),
                    Removido = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Director", x => x.DirectorId);
                });

            migrationBuilder.CreateTable(
                name: "Faculdade",
                columns: table => new
                {
                    FaculdadeId = table.Column<int>(nullable: false),
                    Designacao = table.Column<string>(type: "varchar(500)", nullable: false),
                    Removido = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Faculdade", x => x.FaculdadeId);
                    table.ForeignKey(
                        name: "FK_Faculdade_Director_FaculdadeId",
                        column: x => x.FaculdadeId,
                        principalTable: "Director",
                        principalColumn: "DirectorId",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Faculdade");

            migrationBuilder.DropTable(
                name: "Director");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace SDGE.Infrastructure.Migrations
{
    public partial class RefreshDirectorFaculdade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Faculdade_Director_FaculdadeId",
                table: "Faculdade");

            migrationBuilder.AlterColumn<int>(
                name: "FaculdadeId",
                table: "Faculdade",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateIndex(
                name: "IX_Director_FaculdadeId",
                table: "Director",
                column: "FaculdadeId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Director_Faculdade_FaculdadeId",
                table: "Director",
                column: "FaculdadeId",
                principalTable: "Faculdade",
                principalColumn: "FaculdadeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Director_Faculdade_FaculdadeId",
                table: "Director");

            migrationBuilder.DropIndex(
                name: "IX_Director_FaculdadeId",
                table: "Director");

            migrationBuilder.AlterColumn<int>(
                name: "FaculdadeId",
                table: "Faculdade",
                type: "int",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Faculdade_Director_FaculdadeId",
                table: "Faculdade",
                column: "FaculdadeId",
                principalTable: "Director",
                principalColumn: "DirectorId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendPTDetecta.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarRelacionSeguro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NU_ID_TIPO_SEGURO",
                table: "PACIENTES",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TIPOS_SEGUROS",
                columns: table => new
                {
                    NU_ID_TIPO_SEGURO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TX_NOM_SEGURO = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TIPOS_SEGUROS", x => x.NU_ID_TIPO_SEGURO);
                });

            migrationBuilder.InsertData(
                table: "TIPOS_SEGUROS",
                columns: new[] { "NU_ID_TIPO_SEGURO", "TX_NOM_SEGURO" },
                values: new object[,]
                {
                    { 1, "SIS" },
                    { 2, "EsSalud" },
                    { 3, "EPS Pacífico" },
                    { 4, "Rimac Seguros" },
                    { 5, "Particular" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PACIENTES_NU_ID_TIPO_SEGURO",
                table: "PACIENTES",
                column: "NU_ID_TIPO_SEGURO");

            migrationBuilder.AddForeignKey(
                name: "FK_PACIENTES_TIPOS_SEGUROS_NU_ID_TIPO_SEGURO",
                table: "PACIENTES",
                column: "NU_ID_TIPO_SEGURO",
                principalTable: "TIPOS_SEGUROS",
                principalColumn: "NU_ID_TIPO_SEGURO",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PACIENTES_TIPOS_SEGUROS_NU_ID_TIPO_SEGURO",
                table: "PACIENTES");

            migrationBuilder.DropTable(
                name: "TIPOS_SEGUROS");

            migrationBuilder.DropIndex(
                name: "IX_PACIENTES_NU_ID_TIPO_SEGURO",
                table: "PACIENTES");

            migrationBuilder.DropColumn(
                name: "NU_ID_TIPO_SEGURO",
                table: "PACIENTES");
        }
    }
}

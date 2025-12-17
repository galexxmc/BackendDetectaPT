using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendPTDetecta.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarTriggerCodigoPaciente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // SQL DIRECTO: Creamos el Trigger
            // Lógica: Cuando se inserte algo, toma el ID nuevo, ponle ceros a la izquierda y pégale "PAC-"
            migrationBuilder.Sql(@"
                CREATE TRIGGER TR_PACIENTES_GENERAR_CODIGO
                ON PACIENTES
                AFTER INSERT
                AS
                BEGIN
                    SET NOCOUNT ON;

                    UPDATE p
                    SET p.TX_CODIGO_PACIENTE = 'PAC-' + RIGHT('00000' + CAST(i.NU_ID_PACIENTE AS VARCHAR(10)), 5)
                    FROM PACIENTES p
                    INNER JOIN inserted i ON p.NU_ID_PACIENTE = i.NU_ID_PACIENTE
                END
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP TRIGGER TR_PACIENTES_GENERAR_CODIGO");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackendPTDetecta.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Nombres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellidos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodigoUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MAESTROS",
                columns: table => new
                {
                    NU_ID_MAESTRO = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TX_GRUPO = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TX_NOMBRE = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TX_USU_REG = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FE_REG = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TX_USU_MOD = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FE_MOD = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioEliminacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FechaEliminacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MotivoEliminacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NU_ESTADO = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MAESTROS", x => x.NU_ID_MAESTRO);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PACIENTES",
                columns: table => new
                {
                    NU_ID_PACIENTE = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TX_NOM_PACIEN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TX_APE_PACIEN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NU_DNI_PACIEN = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TX_CODIGO_PACIENTE = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TX_HISTORIA_CLINICA = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FE_NACIMIENTO = table.Column<DateOnly>(type: "date", nullable: false),
                    NU_ID_SEXO = table.Column<int>(type: "int", nullable: false),
                    TX_DIRECCION = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    TX_TELEFONO = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TX_EMAIL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NU_ID_TIPO_SEGURO = table.Column<int>(type: "int", nullable: false),
                    TX_USU_REG = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FE_REG = table.Column<DateTime>(type: "datetime2(0)", nullable: false),
                    TX_USU_MOD = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FE_MOD = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    TX_USU_ELI = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FE_ELI = table.Column<DateTime>(type: "datetime2(0)", nullable: true),
                    TX_MOTIVO_ELI = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NU_ESTADO = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PACIENTES", x => x.NU_ID_PACIENTE);
                    table.ForeignKey(
                        name: "FK_PACIENTES_MAESTROS_NU_ID_SEXO",
                        column: x => x.NU_ID_SEXO,
                        principalTable: "MAESTROS",
                        principalColumn: "NU_ID_MAESTRO",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PACIENTES_MAESTROS_NU_ID_TIPO_SEGURO",
                        column: x => x.NU_ID_TIPO_SEGURO,
                        principalTable: "MAESTROS",
                        principalColumn: "NU_ID_MAESTRO",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "MAESTROS",
                columns: new[] { "NU_ID_MAESTRO", "NU_ESTADO", "FechaEliminacion", "FE_MOD", "FE_REG", "TX_GRUPO", "MotivoEliminacion", "TX_NOMBRE", "UsuarioEliminacion", "TX_USU_MOD", "TX_USU_REG" },
                values: new object[,]
                {
                    { 1, 1, null, null, new DateTime(2025, 12, 23, 20, 51, 24, 988, DateTimeKind.Utc).AddTicks(5385), "SEXO", null, "Masculino", null, null, "SYSTEM" },
                    { 2, 1, null, null, new DateTime(2025, 12, 23, 20, 51, 24, 988, DateTimeKind.Utc).AddTicks(5581), "SEXO", null, "Femenino", null, null, "SYSTEM" },
                    { 3, 1, null, null, new DateTime(2025, 12, 23, 20, 51, 24, 988, DateTimeKind.Utc).AddTicks(5583), "SEGURO", null, "SIS", null, null, "SYSTEM" },
                    { 4, 1, null, null, new DateTime(2025, 12, 23, 20, 51, 24, 988, DateTimeKind.Utc).AddTicks(5591), "SEGURO", null, "EsSalud", null, null, "SYSTEM" },
                    { 5, 1, null, null, new DateTime(2025, 12, 23, 20, 51, 24, 988, DateTimeKind.Utc).AddTicks(5592), "SEGURO", null, "EPS Pacífico", null, null, "SYSTEM" },
                    { 6, 1, null, null, new DateTime(2025, 12, 23, 20, 51, 24, 988, DateTimeKind.Utc).AddTicks(5594), "SEGURO", null, "Rimac Seguros", null, null, "SYSTEM" },
                    { 7, 1, null, null, new DateTime(2025, 12, 23, 20, 51, 24, 988, DateTimeKind.Utc).AddTicks(5595), "SEGURO", null, "Particular", null, null, "SYSTEM" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PACIENTES_NU_ID_SEXO",
                table: "PACIENTES",
                column: "NU_ID_SEXO");

            migrationBuilder.CreateIndex(
                name: "IX_PACIENTES_NU_ID_TIPO_SEGURO",
                table: "PACIENTES",
                column: "NU_ID_TIPO_SEGURO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "PACIENTES");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "MAESTROS");
        }
    }
}

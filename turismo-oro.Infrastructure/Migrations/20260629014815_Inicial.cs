using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace turismo_oro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "FILE");

            migrationBuilder.EnsureSchema(
                name: "TUR");

            migrationBuilder.CreateTable(
                name: "Archivo",
                schema: "FILE",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    TamanioBytes = table.Column<long>(type: "bigint", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Extension = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    TipoArchivo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(307)", maxLength: 307, nullable: true),
                    CreatedIp = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(307)", maxLength: 307, nullable: true),
                    UpdatedIp = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(307)", maxLength: 307, nullable: true),
                    DeletedIp = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Archivo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TurismoLugar",
                schema: "TUR",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Categoria = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Latitud = table.Column<decimal>(type: "numeric(10,7)", precision: 10, scale: 7, nullable: false),
                    Longitud = table.Column<decimal>(type: "numeric(10,7)", precision: 10, scale: 7, nullable: false),
                    Direccion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    EtiquetaPrecio = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(4000)", maxLength: 4000, nullable: false),
                    Destacados = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(307)", maxLength: 307, nullable: true),
                    CreatedIp = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(307)", maxLength: 307, nullable: true),
                    UpdatedIp = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(307)", maxLength: 307, nullable: true),
                    DeletedIp = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurismoLugar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComentarioTurismo",
                schema: "TUR",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TurismoLugarId = table.Column<Guid>(type: "uuid", nullable: false),
                    Autor = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Calificacion = table.Column<int>(type: "integer", nullable: false),
                    Texto = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(307)", maxLength: 307, nullable: true),
                    CreatedIp = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(307)", maxLength: 307, nullable: true),
                    UpdatedIp = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(307)", maxLength: 307, nullable: true),
                    DeletedIp = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComentarioTurismo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComentarioTurismo_TurismoLugar_TurismoLugarId",
                        column: x => x.TurismoLugarId,
                        principalSchema: "TUR",
                        principalTable: "TurismoLugar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TurismoArchivo",
                schema: "FILE",
                columns: table => new
                {
                    TurismoLugarId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArchivoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Orden = table.Column<int>(type: "integer", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(307)", maxLength: 307, nullable: true),
                    CreatedIp = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedBy = table.Column<string>(type: "character varying(307)", maxLength: 307, nullable: true),
                    UpdatedIp = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<string>(type: "character varying(307)", maxLength: 307, nullable: true),
                    DeletedIp = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurismoArchivo", x => new { x.TurismoLugarId, x.ArchivoId });
                    table.ForeignKey(
                        name: "FK_TurismoArchivo_Archivo_ArchivoId",
                        column: x => x.ArchivoId,
                        principalSchema: "FILE",
                        principalTable: "Archivo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TurismoArchivo_TurismoLugar_TurismoLugarId",
                        column: x => x.TurismoLugarId,
                        principalSchema: "TUR",
                        principalTable: "TurismoLugar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComentarioTurismo_TurismoLugarId",
                schema: "TUR",
                table: "ComentarioTurismo",
                column: "TurismoLugarId");

            migrationBuilder.CreateIndex(
                name: "IX_TurismoArchivo_ArchivoId",
                schema: "FILE",
                table: "TurismoArchivo",
                column: "ArchivoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComentarioTurismo",
                schema: "TUR");

            migrationBuilder.DropTable(
                name: "TurismoArchivo",
                schema: "FILE");

            migrationBuilder.DropTable(
                name: "Archivo",
                schema: "FILE");

            migrationBuilder.DropTable(
                name: "TurismoLugar",
                schema: "TUR");
        }
    }
}

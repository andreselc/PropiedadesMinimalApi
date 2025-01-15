using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PropiedadesMinimalApi.Migrations
{
    /// <inheritdoc />
    public partial class CreacionTablaPropiedad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Propiedad",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    activa = table.Column<bool>(type: "bit", nullable: false),
                    fechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propiedad", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "Propiedad",
                columns: new[] { "id", "activa", "descripcion", "fechaCreacion", "nombre", "ubicacion" },
                values: new object[,]
                {
                    { 1, true, "Casa de campo en la montaña", new DateTime(2025, 1, 14, 17, 9, 11, 701, DateTimeKind.Local).AddTicks(8604), "Casa de campo", "Montaña" },
                    { 2, true, "Casa de playa en la costa", new DateTime(2025, 1, 14, 17, 9, 11, 701, DateTimeKind.Local).AddTicks(8645), "Casa de playa", "Costa" },
                    { 3, true, "Casa en la ciudad", new DateTime(2025, 1, 14, 17, 9, 11, 701, DateTimeKind.Local).AddTicks(8648), "Casa de ciudad", "Ciudad" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Propiedad");
        }
    }
}

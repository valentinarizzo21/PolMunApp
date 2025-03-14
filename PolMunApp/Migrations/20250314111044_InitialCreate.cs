using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PolMunApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trasgressori",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cognome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CodiceFiscale = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: false),
                    Indirizzo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trasgressori", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Violazioni",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descrizione = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Importo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PuntiDecurtati = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Violazioni", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Verbali",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrasgressoreID = table.Column<int>(type: "int", nullable: false),
                    ViolazioneID = table.Column<int>(type: "int", nullable: false),
                    DataViolazione = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Importo = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Verbali", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Verbali_Trasgressori_TrasgressoreID",
                        column: x => x.TrasgressoreID,
                        principalTable: "Trasgressori",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Verbali_Violazioni_ViolazioneID",
                        column: x => x.ViolazioneID,
                        principalTable: "Violazioni",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Verbali_TrasgressoreID",
                table: "Verbali",
                column: "TrasgressoreID");

            migrationBuilder.CreateIndex(
                name: "IX_Verbali_ViolazioneID",
                table: "Verbali",
                column: "ViolazioneID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Verbali");

            migrationBuilder.DropTable(
                name: "Trasgressori");

            migrationBuilder.DropTable(
                name: "Violazioni");
        }
    }
}

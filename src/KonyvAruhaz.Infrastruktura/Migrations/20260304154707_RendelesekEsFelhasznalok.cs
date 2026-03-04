using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KonyvAruhaz.Infrastruktura.Migrations
{
    /// <inheritdoc />
    public partial class RendelesekEsFelhasznalok : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Felhasznalok",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JelszoHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Szerepkor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aktiv = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Felhasznalok", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rendelesek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FelhasznaloId = table.Column<int>(type: "int", nullable: false),
                    LetrehozasIdeje = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Allapot = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rendelesek", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rendelesek_Felhasznalok_FelhasznaloId",
                        column: x => x.FelhasznaloId,
                        principalTable: "Felhasznalok",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RendelesTetelek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RendelesId = table.Column<int>(type: "int", nullable: false),
                    KonyvId = table.Column<int>(type: "int", nullable: false),
                    Mennyiseg = table.Column<int>(type: "int", nullable: false),
                    EgysegAr = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RendelesTetelek", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RendelesTetelek_Konyvek_KonyvId",
                        column: x => x.KonyvId,
                        principalTable: "Konyvek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RendelesTetelek_Rendelesek_RendelesId",
                        column: x => x.RendelesId,
                        principalTable: "Rendelesek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rendelesek_FelhasznaloId",
                table: "Rendelesek",
                column: "FelhasznaloId");

            migrationBuilder.CreateIndex(
                name: "IX_RendelesTetelek_KonyvId",
                table: "RendelesTetelek",
                column: "KonyvId");

            migrationBuilder.CreateIndex(
                name: "IX_RendelesTetelek_RendelesId",
                table: "RendelesTetelek",
                column: "RendelesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RendelesTetelek");

            migrationBuilder.DropTable(
                name: "Rendelesek");

            migrationBuilder.DropTable(
                name: "Felhasznalok");
        }
    }
}

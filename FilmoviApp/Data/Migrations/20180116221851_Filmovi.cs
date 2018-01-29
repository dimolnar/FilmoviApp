using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FilmoviApp.Data.Migrations
{
    public partial class Filmovi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateTable(
                name: "Glumac",
                columns: table => new
                {
                    GlumacId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatRod = table.Column<DateTime>(nullable: false),
                    Ime = table.Column<string>(nullable: false),
                    MjestoRod = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: false),
                    RodnoIme = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Glumac", x => x.GlumacId);
                });

            migrationBuilder.CreateTable(
                name: "Redatelj",
                columns: table => new
                {
                    RedateljId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DatRod = table.Column<DateTime>(nullable: false),
                    Ime = table.Column<string>(nullable: false),
                    MjestoRod = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Redatelj", x => x.RedateljId);
                });

            migrationBuilder.CreateTable(
                name: "Zanr",
                columns: table => new
                {
                    ZanrId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zanr", x => x.ZanrId);
                });

            migrationBuilder.CreateTable(
                name: "Film",
                columns: table => new
                {
                    FilmId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Godina = table.Column<int>(nullable: false),
                    IzvorniNaziv = table.Column<string>(nullable: true),
                    Naziv = table.Column<string>(nullable: false),
                    RedateljId = table.Column<int>(nullable: false),
                    Trajanje = table.Column<DateTime>(nullable: false),
                    ZanrId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Film", x => x.FilmId);
                    table.ForeignKey(
                        name: "FK_Film_Redatelj_RedateljId",
                        column: x => x.RedateljId,
                        principalTable: "Redatelj",
                        principalColumn: "RedateljId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Film_Zanr_ZanrId",
                        column: x => x.ZanrId,
                        principalTable: "Zanr",
                        principalColumn: "ZanrId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GlumacFilm",
                columns: table => new
                {
                    FilmId = table.Column<int>(nullable: false),
                    GlumacId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlumacFilm", x => new { x.FilmId, x.GlumacId });
                    table.ForeignKey(
                        name: "FK_GlumacFilm_Film_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Film",
                        principalColumn: "FilmId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GlumacFilm_Glumac_GlumacId",
                        column: x => x.GlumacId,
                        principalTable: "Glumac",
                        principalColumn: "GlumacId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Film_RedateljId",
                table: "Film",
                column: "RedateljId");

            migrationBuilder.CreateIndex(
                name: "IX_Film_ZanrId",
                table: "Film",
                column: "ZanrId");

            migrationBuilder.CreateIndex(
                name: "IX_GlumacFilm_GlumacId",
                table: "GlumacFilm",
                column: "GlumacId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "GlumacFilm");

            migrationBuilder.DropTable(
                name: "Film");

            migrationBuilder.DropTable(
                name: "Glumac");

            migrationBuilder.DropTable(
                name: "Redatelj");

            migrationBuilder.DropTable(
                name: "Zanr");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId",
                table: "AspNetUserRoles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName");
        }
    }
}

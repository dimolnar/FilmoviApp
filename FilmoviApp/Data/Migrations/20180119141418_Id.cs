using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace FilmoviApp.Data.Migrations
{
    public partial class Id : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GlumacFilm",
                table: "GlumacFilm");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "GlumacFilm",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GlumacFilm",
                table: "GlumacFilm",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_GlumacFilm_FilmId",
                table: "GlumacFilm",
                column: "FilmId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GlumacFilm",
                table: "GlumacFilm");

            migrationBuilder.DropIndex(
                name: "IX_GlumacFilm_FilmId",
                table: "GlumacFilm");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GlumacFilm");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GlumacFilm",
                table: "GlumacFilm",
                columns: new[] { "FilmId", "GlumacId" });
        }
    }
}

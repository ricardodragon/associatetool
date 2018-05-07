using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace associatetool.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tag_Address",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    codigo = table.Column<string>(nullable: true),
                    nome_ferramenta = table.Column<string>(nullable: true),
                    trigger = table.Column<string>(nullable: true),
                    vida_util = table.Column<string>(nullable: true),
                    vida_util_acumulado = table.Column<string>(nullable: true),
                    vida_util_max = table.Column<string>(nullable: true),
                    vida_util_status = table.Column<string>(nullable: true),
                    vida_util_unidade = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag_Address", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ToolTag",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    codigo = table.Column<string>(nullable: true),
                    trigger = table.Column<bool>(nullable: false),
                    vida_util = table.Column<int>(nullable: false),
                    vida_util_unidade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolTag", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tag_Address");

            migrationBuilder.DropTable(
                name: "ToolTag");
        }
    }
}

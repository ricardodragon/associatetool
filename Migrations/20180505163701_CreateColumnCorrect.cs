using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace associatetool.Migrations
{
    public partial class CreateColumnCorrect : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ferramenta_id",
                table: "Tag_Address",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ferramenta_id",
                table: "Tag_Address");
        }
    }
}

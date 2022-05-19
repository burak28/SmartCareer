using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartCareer.Migrations
{
    public partial class Update1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string[]>(
                name: "SkillSet",
                table: "WorkItems",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.AddColumn<string[]>(
                name: "Skills",
                table: "Users",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SkillSet",
                table: "WorkItems");

            migrationBuilder.DropColumn(
                name: "Skills",
                table: "Users");
        }
    }
}

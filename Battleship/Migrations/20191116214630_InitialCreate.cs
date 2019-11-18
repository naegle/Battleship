using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Battleship.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HighScores",
                columns: table => new
                {
                    PlayerId = table.Column<string>(nullable: false),
                    AccuracyScore = table.Column<double>(nullable: false),
                    Date_Of_Win = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HighScores", x => x.PlayerId);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    PlayerId = table.Column<string>(nullable: false),
                    Power1 = table.Column<int>(nullable: false),
                    Power2 = table.Column<int>(nullable: false),
                    Power3 = table.Column<int>(nullable: false),
                    Cash = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.PlayerId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HighScores");

            migrationBuilder.DropTable(
                name: "Inventories");
        }
    }
}

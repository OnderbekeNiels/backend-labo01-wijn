using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace backend_labo01_wijn.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wines",
                columns: table => new
                {
                    WineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Grapes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wines", x => x.WineId);
                });

            migrationBuilder.InsertData(
                table: "Wines",
                columns: new[] { "WineId", "Color", "Country", "Grapes", "Name", "Price", "Year" },
                values: new object[] { new Guid("0fa73038-3e10-42f6-a880-1cee8ec869fe"), "rosé", "FR", "bessen", "Terrases de la mer", 19.949999999999999, 2018 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Wines");
        }
    }
}

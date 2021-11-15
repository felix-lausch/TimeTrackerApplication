using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTrackerApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StartHours = table.Column<int>(type: "int", nullable: false),
                    StartMinutes = table.Column<int>(type: "int", nullable: false),
                    StartMinutesAlt = table.Column<double>(type: "float", nullable: false),
                    EndHours = table.Column<int>(type: "int", nullable: false),
                    EndMinutes = table.Column<int>(type: "int", nullable: false),
                    EndMinutesAlt = table.Column<double>(type: "float", nullable: false),
                    PauseHours = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeEntries", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeEntries");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeTrackerApi.Migrations
{
    public partial class AddDayEntry : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DayEntryId",
                table: "TimeEntries",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DayEntries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayEntries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntries_DayEntryId",
                table: "TimeEntries",
                column: "DayEntryId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeEntries_DayEntries_DayEntryId",
                table: "TimeEntries",
                column: "DayEntryId",
                principalTable: "DayEntries",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeEntries_DayEntries_DayEntryId",
                table: "TimeEntries");

            migrationBuilder.DropTable(
                name: "DayEntries");

            migrationBuilder.DropIndex(
                name: "IX_TimeEntries_DayEntryId",
                table: "TimeEntries");

            migrationBuilder.DropColumn(
                name: "DayEntryId",
                table: "TimeEntries");
        }
    }
}

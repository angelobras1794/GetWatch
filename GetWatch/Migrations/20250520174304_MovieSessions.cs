using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetWatch.Migrations
{
    /// <inheritdoc />
    public partial class MovieSessions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DbMovieSessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    MovieId = table.Column<int>(type: "INTEGER", nullable: false),
                    AvailableSeats = table.Column<string>(type: "TEXT", nullable: true),
                    MovieType = table.Column<string>(type: "TEXT", nullable: true),
                    CinemaName = table.Column<string>(type: "TEXT", nullable: true),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Updated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbMovieSessions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbMovieSessions");
        }
    }
}

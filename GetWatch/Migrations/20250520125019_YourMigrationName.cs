using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetWatch.Migrations
{
    /// <inheritdoc />
    public partial class YourMigrationName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DbCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    cardNumber = table.Column<long>(type: "INTEGER", nullable: false),
                    cardOwner = table.Column<string>(type: "TEXT", nullable: false),
                    expiryDate = table.Column<string>(type: "TEXT", nullable: false),
                    cvv = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Updated = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DbCards_DbUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "DbUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DbCards_UserId",
                table: "DbCards",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbCards");
        }
    }
}

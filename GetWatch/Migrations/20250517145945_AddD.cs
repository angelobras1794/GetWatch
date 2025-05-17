using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetWatch.Migrations
{
    /// <inheritdoc />
    public partial class AddD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "DbPurchases",
                type: "TEXT",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PersonAmount",
                table: "DbPurchases",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PurchaseType",
                table: "DbPurchases",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RentalEndDate",
                table: "DbPurchases",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Seats",
                table: "DbPurchases",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "DbPurchases");

            migrationBuilder.DropColumn(
                name: "PersonAmount",
                table: "DbPurchases");

            migrationBuilder.DropColumn(
                name: "PurchaseType",
                table: "DbPurchases");

            migrationBuilder.DropColumn(
                name: "RentalEndDate",
                table: "DbPurchases");

            migrationBuilder.DropColumn(
                name: "Seats",
                table: "DbPurchases");
        }
    }
}

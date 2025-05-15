using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GetWatch.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCartItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DbRentItem_PurchaseType",
                table: "DbCartItem",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DbTicketCart_PurchaseType",
                table: "DbCartItem",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "DbCartItem",
                type: "TEXT",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PersonAmount",
                table: "DbCartItem",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PurchaseType",
                table: "DbCartItem",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RentDate",
                table: "DbCartItem",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Seats",
                table: "DbCartItem",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DbRentItem_PurchaseType",
                table: "DbCartItem");

            migrationBuilder.DropColumn(
                name: "DbTicketCart_PurchaseType",
                table: "DbCartItem");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "DbCartItem");

            migrationBuilder.DropColumn(
                name: "PersonAmount",
                table: "DbCartItem");

            migrationBuilder.DropColumn(
                name: "PurchaseType",
                table: "DbCartItem");

            migrationBuilder.DropColumn(
                name: "RentDate",
                table: "DbCartItem");

            migrationBuilder.DropColumn(
                name: "Seats",
                table: "DbCartItem");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace restaurantAPI.Migrations
{
    /// <inheritdoc />
    public partial class update2607 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Calories",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Dishes");

            migrationBuilder.DropColumn(
                name: "IsVegetarian",
                table: "Dishes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Calories",
                table: "Dishes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Dishes",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<bool>(
                name: "IsVegetarian",
                table: "Dishes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

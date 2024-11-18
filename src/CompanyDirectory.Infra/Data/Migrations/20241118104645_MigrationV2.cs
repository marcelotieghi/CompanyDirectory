using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyDirectory.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigrationV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Personnel");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Departments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Personnel",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Locations",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Departments",
                type: "datetime2",
                nullable: true);
        }
    }
}

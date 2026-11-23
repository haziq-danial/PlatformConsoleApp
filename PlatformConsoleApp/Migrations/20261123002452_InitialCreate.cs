using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace PlatformConsoleApp.Migrations;

/// <inheritdoc />
public partial class _20261123002452_InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "platforms",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                UniqueName = table.Column<string>(type: "longtext", nullable: false),
                Latitude = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                Longitude = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                LastUpdate = table.Column<DateTime>(type: "datetime(3)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_platforms", x => x.Id);
            })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "wells",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                PlatformId = table.Column<int>(type: "int", nullable: false),
                UniqueName = table.Column<string>(type: "longtext", nullable: false),
                Latitude = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                Longitude = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                LastUpdate = table.Column<DateTime>(type: "datetime(3)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_wells", x => x.Id);
                table.ForeignKey(
                    name: "FK_wells_platforms_PlatformId",
                    column: x => x.PlatformId,
                    principalTable: "platforms",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySQL:Charset", "utf8mb4");

        migrationBuilder.CreateIndex(
            name: "IX_wells_PlatformId",
            table: "wells",
            column: "PlatformId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "wells");

        migrationBuilder.DropTable(
            name: "platforms");
    }
}

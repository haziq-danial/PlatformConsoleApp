using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatformConsoleApp.Migrations;

/// <inheritdoc />
public partial class _20261123023524_ReplaceLastUpdateWithCreatedAndUpdatedAt : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "LastUpdate",
            table: "wells",
            newName: "UpdatedAt");

        migrationBuilder.RenameColumn(
            name: "LastUpdate",
            table: "platforms",
            newName: "UpdatedAt");

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedAt",
            table: "wells",
            type: "datetime(3)",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedAt",
            table: "platforms",
            type: "datetime(3)",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "CreatedAt",
            table: "wells");

        migrationBuilder.DropColumn(
            name: "CreatedAt",
            table: "platforms");

        migrationBuilder.RenameColumn(
            name: "UpdatedAt",
            table: "wells",
            newName: "LastUpdate");

        migrationBuilder.RenameColumn(
            name: "UpdatedAt",
            table: "platforms",
            newName: "LastUpdate");
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatformConsoleApp.Migrations;

/// <inheritdoc />
public partial class _20261123020157_MakeLocationAndUpdateFieldsNullable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<decimal>(
            name: "Longitude",
            table: "wells",
            type: "decimal(18,6)",
            precision: 18,
            scale: 6,
            nullable: true,
            oldClrType: typeof(decimal),
            oldType: "decimal(18,6)",
            oldPrecision: 18,
            oldScale: 6);

        migrationBuilder.AlterColumn<decimal>(
            name: "Latitude",
            table: "wells",
            type: "decimal(18,6)",
            precision: 18,
            scale: 6,
            nullable: true,
            oldClrType: typeof(decimal),
            oldType: "decimal(18,6)",
            oldPrecision: 18,
            oldScale: 6);

        migrationBuilder.AlterColumn<DateTime>(
            name: "LastUpdate",
            table: "wells",
            type: "datetime(3)",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime(3)");

        migrationBuilder.AlterColumn<decimal>(
            name: "Longitude",
            table: "platforms",
            type: "decimal(18,6)",
            precision: 18,
            scale: 6,
            nullable: true,
            oldClrType: typeof(decimal),
            oldType: "decimal(18,6)",
            oldPrecision: 18,
            oldScale: 6);

        migrationBuilder.AlterColumn<decimal>(
            name: "Latitude",
            table: "platforms",
            type: "decimal(18,6)",
            precision: 18,
            scale: 6,
            nullable: true,
            oldClrType: typeof(decimal),
            oldType: "decimal(18,6)",
            oldPrecision: 18,
            oldScale: 6);

        migrationBuilder.AlterColumn<DateTime>(
            name: "LastUpdate",
            table: "platforms",
            type: "datetime(3)",
            nullable: true,
            oldClrType: typeof(DateTime),
            oldType: "datetime(3)");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<decimal>(
            name: "Longitude",
            table: "wells",
            type: "decimal(18,6)",
            precision: 18,
            scale: 6,
            nullable: false,
            defaultValue: 0m,
            oldClrType: typeof(decimal),
            oldType: "decimal(18,6)",
            oldPrecision: 18,
            oldScale: 6,
            oldNullable: true);

        migrationBuilder.AlterColumn<decimal>(
            name: "Latitude",
            table: "wells",
            type: "decimal(18,6)",
            precision: 18,
            scale: 6,
            nullable: false,
            defaultValue: 0m,
            oldClrType: typeof(decimal),
            oldType: "decimal(18,6)",
            oldPrecision: 18,
            oldScale: 6,
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "LastUpdate",
            table: "wells",
            type: "datetime(3)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "datetime(3)",
            oldNullable: true);

        migrationBuilder.AlterColumn<decimal>(
            name: "Longitude",
            table: "platforms",
            type: "decimal(18,6)",
            precision: 18,
            scale: 6,
            nullable: false,
            defaultValue: 0m,
            oldClrType: typeof(decimal),
            oldType: "decimal(18,6)",
            oldPrecision: 18,
            oldScale: 6,
            oldNullable: true);

        migrationBuilder.AlterColumn<decimal>(
            name: "Latitude",
            table: "platforms",
            type: "decimal(18,6)",
            precision: 18,
            scale: 6,
            nullable: false,
            defaultValue: 0m,
            oldClrType: typeof(decimal),
            oldType: "decimal(18,6)",
            oldPrecision: 18,
            oldScale: 6,
            oldNullable: true);

        migrationBuilder.AlterColumn<DateTime>(
            name: "LastUpdate",
            table: "platforms",
            type: "datetime(3)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
            oldClrType: typeof(DateTime),
            oldType: "datetime(3)",
            oldNullable: true);
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Uow.Data.Migrations;

public partial class Addedmoredetailstouserentity : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "CreatedBy",
            table: "Users",
            type: "TEXT",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "CreationDate",
            table: "Users",
            type: "TEXT",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "ExpirationDate",
            table: "Users",
            type: "TEXT",
            nullable: true);

        migrationBuilder.AddColumn<bool>(
            name: "IsDeleted",
            table: "Users",
            type: "INTEGER",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<bool>(
            name: "IsEmailConfirmed",
            table: "Users",
            type: "INTEGER",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<bool>(
            name: "IsLocked",
            table: "Users",
            type: "INTEGER",
            nullable: false,
            defaultValue: false);

        migrationBuilder.AddColumn<Guid>(
            name: "ModifiedBy",
            table: "Users",
            type: "TEXT",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "ModifiedDate",
            table: "Users",
            type: "TEXT",
            nullable: true);

        migrationBuilder.AddColumn<string>(
            name: "Password",
            table: "Users",
            type: "TEXT",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "Salt",
            table: "Users",
            type: "TEXT",
            nullable: false,
            defaultValue: "");

        migrationBuilder.CreateTable(
            name: "UserDto",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "TEXT", nullable: false),
                Email = table.Column<string>(type: "TEXT", nullable: true),
                Password = table.Column<string>(type: "TEXT", nullable: true),
                Salt = table.Column<string>(type: "TEXT", nullable: true),
                ExpirationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                IsLocked = table.Column<bool>(type: "INTEGER", nullable: true),
                IsDeleted = table.Column<bool>(type: "INTEGER", nullable: true),
                IsEmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: true),
                CreationDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                CreatedBy = table.Column<Guid>(type: "TEXT", nullable: true),
                ModifiedDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                ModifiedBy = table.Column<Guid>(type: "TEXT", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserDto", x => x.Id);
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "UserDto");

        migrationBuilder.DropColumn(
            name: "CreatedBy",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "CreationDate",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "ExpirationDate",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "IsDeleted",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "IsEmailConfirmed",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "IsLocked",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "ModifiedBy",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "ModifiedDate",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "Password",
            table: "Users");

        migrationBuilder.DropColumn(
            name: "Salt",
            table: "Users");
    }
}
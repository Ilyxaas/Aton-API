using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AtonWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class Data_v20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Admin",
                schema: "public",
                table: "User",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                schema: "public",
                table: "User",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "public",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "public",
                table: "User",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                schema: "public",
                table: "User",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Login",
                schema: "public",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                schema: "public",
                table: "User",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                schema: "public",
                table: "User",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "public",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Password",
                schema: "public",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RevokedBy",
                schema: "public",
                table: "User",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RevokedOn",
                schema: "public",
                table: "User",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Admin",
                schema: "public",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Birthday",
                schema: "public",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "public",
                table: "User");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "public",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Gender",
                schema: "public",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Login",
                schema: "public",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                schema: "public",
                table: "User");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                schema: "public",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Name",
                schema: "public",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Password",
                schema: "public",
                table: "User");

            migrationBuilder.DropColumn(
                name: "RevokedBy",
                schema: "public",
                table: "User");

            migrationBuilder.DropColumn(
                name: "RevokedOn",
                schema: "public",
                table: "User");
        }
    }
}

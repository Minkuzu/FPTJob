using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FPTJob.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixJobSeeker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                schema: "dbo",
                table: "JobSeeker");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                schema: "dbo",
                table: "JobSeeker");

            migrationBuilder.DropColumn(
                name: "Email",
                schema: "dbo",
                table: "JobSeeker");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                schema: "dbo",
                table: "JobSeeker");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                schema: "dbo",
                table: "JobSeeker");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                schema: "dbo",
                table: "JobSeeker");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                schema: "dbo",
                table: "JobSeeker");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                schema: "dbo",
                table: "JobSeeker");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                schema: "dbo",
                table: "JobSeeker");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                schema: "dbo",
                table: "JobSeeker");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                schema: "dbo",
                table: "JobSeeker");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                schema: "dbo",
                table: "JobSeeker");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                schema: "dbo",
                table: "JobSeeker");

            migrationBuilder.DropColumn(
                name: "UserName",
                schema: "dbo",
                table: "JobSeeker");

            migrationBuilder.AddForeignKey(
                name: "FK_JobSeeker_User_Id",
                schema: "dbo",
                table: "JobSeeker",
                column: "Id",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobSeeker_User_Id",
                schema: "dbo",
                table: "JobSeeker");

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                schema: "dbo",
                table: "JobSeeker",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                schema: "dbo",
                table: "JobSeeker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                schema: "dbo",
                table: "JobSeeker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                schema: "dbo",
                table: "JobSeeker",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                schema: "dbo",
                table: "JobSeeker",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                schema: "dbo",
                table: "JobSeeker",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                schema: "dbo",
                table: "JobSeeker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                schema: "dbo",
                table: "JobSeeker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                schema: "dbo",
                table: "JobSeeker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                schema: "dbo",
                table: "JobSeeker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                schema: "dbo",
                table: "JobSeeker",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                schema: "dbo",
                table: "JobSeeker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                schema: "dbo",
                table: "JobSeeker",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                schema: "dbo",
                table: "JobSeeker",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

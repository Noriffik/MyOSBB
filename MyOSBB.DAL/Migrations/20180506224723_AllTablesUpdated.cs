using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyOSBB.DAL.Migrations
{
    public partial class AllTablesUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contributions_AspNetUsers_UserId",
                table: "Contributions");

            migrationBuilder.DropIndex(
                name: "IX_Contributions_UserId",
                table: "Contributions");

            migrationBuilder.DropColumn(
                name: "Sum",
                table: "Contributions");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Contributions",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForPeriod",
                table: "Contributions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Payment",
                table: "Contributions",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PaymentDate",
                table: "Contributions",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Announcements",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Announcements",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ForPeriod",
                table: "Contributions");

            migrationBuilder.DropColumn(
                name: "Payment",
                table: "Contributions");

            migrationBuilder.DropColumn(
                name: "PaymentDate",
                table: "Contributions");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Announcements");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Contributions",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Sum",
                table: "Contributions",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Contributions_UserId",
                table: "Contributions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contributions_AspNetUsers_UserId",
                table: "Contributions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

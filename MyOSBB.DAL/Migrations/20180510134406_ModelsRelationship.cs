using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyOSBB.DAL.Migrations
{
    public partial class ModelsRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "InvoiceWaters",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "InvoiceTels",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "InvoiceServices",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "InvoiceGazs",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "InvoiceElectros",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Contributions",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ForPeriod",
                table: "Contributions",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Announcements",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceWaters_UserId",
                table: "InvoiceWaters",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceTels_UserId",
                table: "InvoiceTels",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceServices_UserId",
                table: "InvoiceServices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceGazs_UserId",
                table: "InvoiceGazs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceElectros_UserId",
                table: "InvoiceElectros",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Contributions_UserId",
                table: "Contributions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_UserId",
                table: "Announcements",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_AspNetUsers_UserId",
                table: "Announcements",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contributions_AspNetUsers_UserId",
                table: "Contributions",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceElectros_AspNetUsers_UserId",
                table: "InvoiceElectros",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceGazs_AspNetUsers_UserId",
                table: "InvoiceGazs",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceServices_AspNetUsers_UserId",
                table: "InvoiceServices",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceTels_AspNetUsers_UserId",
                table: "InvoiceTels",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceWaters_AspNetUsers_UserId",
                table: "InvoiceWaters",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_AspNetUsers_UserId",
                table: "Announcements");

            migrationBuilder.DropForeignKey(
                name: "FK_Contributions_AspNetUsers_UserId",
                table: "Contributions");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceElectros_AspNetUsers_UserId",
                table: "InvoiceElectros");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceGazs_AspNetUsers_UserId",
                table: "InvoiceGazs");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceServices_AspNetUsers_UserId",
                table: "InvoiceServices");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceTels_AspNetUsers_UserId",
                table: "InvoiceTels");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceWaters_AspNetUsers_UserId",
                table: "InvoiceWaters");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceWaters_UserId",
                table: "InvoiceWaters");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceTels_UserId",
                table: "InvoiceTels");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceServices_UserId",
                table: "InvoiceServices");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceGazs_UserId",
                table: "InvoiceGazs");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceElectros_UserId",
                table: "InvoiceElectros");

            migrationBuilder.DropIndex(
                name: "IX_Contributions_UserId",
                table: "Contributions");

            migrationBuilder.DropIndex(
                name: "IX_Announcements_UserId",
                table: "Announcements");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "InvoiceWaters",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "InvoiceTels",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "InvoiceServices",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "InvoiceGazs",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "InvoiceElectros",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Contributions",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ForPeriod",
                table: "Contributions",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Announcements",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

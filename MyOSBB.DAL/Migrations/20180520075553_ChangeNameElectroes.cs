using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyOSBB.DAL.Migrations
{
    public partial class ChangeNameElectroes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceElectros_Months_MonthId",
                table: "InvoiceElectros");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceElectros_AspNetUsers_UserId",
                table: "InvoiceElectros");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceElectros",
                table: "InvoiceElectros");

            migrationBuilder.RenameTable(
                name: "InvoiceElectros",
                newName: "InvoiceElectroes");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceElectros_UserId",
                table: "InvoiceElectroes",
                newName: "IX_InvoiceElectroes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceElectros_MonthId",
                table: "InvoiceElectroes",
                newName: "IX_InvoiceElectroes_MonthId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceElectroes",
                table: "InvoiceElectroes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceElectroes_Months_MonthId",
                table: "InvoiceElectroes",
                column: "MonthId",
                principalTable: "Months",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceElectroes_AspNetUsers_UserId",
                table: "InvoiceElectroes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceElectroes_Months_MonthId",
                table: "InvoiceElectroes");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceElectroes_AspNetUsers_UserId",
                table: "InvoiceElectroes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InvoiceElectroes",
                table: "InvoiceElectroes");

            migrationBuilder.RenameTable(
                name: "InvoiceElectroes",
                newName: "InvoiceElectros");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceElectroes_UserId",
                table: "InvoiceElectros",
                newName: "IX_InvoiceElectros_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_InvoiceElectroes_MonthId",
                table: "InvoiceElectros",
                newName: "IX_InvoiceElectros_MonthId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InvoiceElectros",
                table: "InvoiceElectros",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceElectros_Months_MonthId",
                table: "InvoiceElectros",
                column: "MonthId",
                principalTable: "Months",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceElectros_AspNetUsers_UserId",
                table: "InvoiceElectros",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

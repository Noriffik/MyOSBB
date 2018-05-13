using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyOSBB.DAL.Migrations
{
    public partial class ModelsRelationshipMonth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contributions_Month_MonthId",
                table: "Contributions");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceElectros_Month_MonthId",
                table: "InvoiceElectros");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceGazs_Month_MonthId",
                table: "InvoiceGazs");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceServices_Month_MonthId",
                table: "InvoiceServices");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceTels_Month_MonthId",
                table: "InvoiceTels");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceWaters_Month_MonthId",
                table: "InvoiceWaters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Month",
                table: "Month");

            migrationBuilder.RenameTable(
                name: "Month",
                newName: "Months");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Months",
                table: "Months",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contributions_Months_MonthId",
                table: "Contributions",
                column: "MonthId",
                principalTable: "Months",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceElectros_Months_MonthId",
                table: "InvoiceElectros",
                column: "MonthId",
                principalTable: "Months",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceGazs_Months_MonthId",
                table: "InvoiceGazs",
                column: "MonthId",
                principalTable: "Months",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceServices_Months_MonthId",
                table: "InvoiceServices",
                column: "MonthId",
                principalTable: "Months",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceTels_Months_MonthId",
                table: "InvoiceTels",
                column: "MonthId",
                principalTable: "Months",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceWaters_Months_MonthId",
                table: "InvoiceWaters",
                column: "MonthId",
                principalTable: "Months",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contributions_Months_MonthId",
                table: "Contributions");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceElectros_Months_MonthId",
                table: "InvoiceElectros");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceGazs_Months_MonthId",
                table: "InvoiceGazs");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceServices_Months_MonthId",
                table: "InvoiceServices");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceTels_Months_MonthId",
                table: "InvoiceTels");

            migrationBuilder.DropForeignKey(
                name: "FK_InvoiceWaters_Months_MonthId",
                table: "InvoiceWaters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Months",
                table: "Months");

            migrationBuilder.RenameTable(
                name: "Months",
                newName: "Month");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Month",
                table: "Month",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contributions_Month_MonthId",
                table: "Contributions",
                column: "MonthId",
                principalTable: "Month",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceElectros_Month_MonthId",
                table: "InvoiceElectros",
                column: "MonthId",
                principalTable: "Month",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceGazs_Month_MonthId",
                table: "InvoiceGazs",
                column: "MonthId",
                principalTable: "Month",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceServices_Month_MonthId",
                table: "InvoiceServices",
                column: "MonthId",
                principalTable: "Month",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceTels_Month_MonthId",
                table: "InvoiceTels",
                column: "MonthId",
                principalTable: "Month",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InvoiceWaters_Month_MonthId",
                table: "InvoiceWaters",
                column: "MonthId",
                principalTable: "Month",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

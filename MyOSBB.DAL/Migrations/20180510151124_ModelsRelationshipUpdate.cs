using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyOSBB.DAL.Migrations
{
    public partial class ModelsRelationshipUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlatNumber",
                table: "InvoiceWaters");

            migrationBuilder.DropColumn(
                name: "ForPeriod",
                table: "InvoiceWaters");

            migrationBuilder.DropColumn(
                name: "FlatNumber",
                table: "InvoiceTels");

            migrationBuilder.DropColumn(
                name: "ForPeriod",
                table: "InvoiceTels");

            migrationBuilder.DropColumn(
                name: "FlatNumber",
                table: "InvoiceServices");

            migrationBuilder.DropColumn(
                name: "ForPeriod",
                table: "InvoiceServices");

            migrationBuilder.DropColumn(
                name: "FlatNumber",
                table: "InvoiceGazs");

            migrationBuilder.DropColumn(
                name: "ForPeriod",
                table: "InvoiceGazs");

            migrationBuilder.DropColumn(
                name: "FlatNumber",
                table: "InvoiceElectros");

            migrationBuilder.DropColumn(
                name: "ForPeriod",
                table: "InvoiceElectros");

            migrationBuilder.DropColumn(
                name: "FlatNumber",
                table: "Contributions");

            migrationBuilder.RenameColumn(
                name: "ForPeriod",
                table: "Contributions",
                newName: "MonthId");

            migrationBuilder.AddColumn<int>(
                name: "MonthId",
                table: "InvoiceWaters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MonthId",
                table: "InvoiceTels",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MonthId",
                table: "InvoiceServices",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MonthId",
                table: "InvoiceGazs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MonthId",
                table: "InvoiceElectros",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Month",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Month", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceWaters_MonthId",
                table: "InvoiceWaters",
                column: "MonthId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceTels_MonthId",
                table: "InvoiceTels",
                column: "MonthId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceServices_MonthId",
                table: "InvoiceServices",
                column: "MonthId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceGazs_MonthId",
                table: "InvoiceGazs",
                column: "MonthId");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceElectros_MonthId",
                table: "InvoiceElectros",
                column: "MonthId");

            migrationBuilder.CreateIndex(
                name: "IX_Contributions_MonthId",
                table: "Contributions",
                column: "MonthId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropTable(
                name: "Month");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceWaters_MonthId",
                table: "InvoiceWaters");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceTels_MonthId",
                table: "InvoiceTels");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceServices_MonthId",
                table: "InvoiceServices");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceGazs_MonthId",
                table: "InvoiceGazs");

            migrationBuilder.DropIndex(
                name: "IX_InvoiceElectros_MonthId",
                table: "InvoiceElectros");

            migrationBuilder.DropIndex(
                name: "IX_Contributions_MonthId",
                table: "Contributions");

            migrationBuilder.DropColumn(
                name: "MonthId",
                table: "InvoiceWaters");

            migrationBuilder.DropColumn(
                name: "MonthId",
                table: "InvoiceTels");

            migrationBuilder.DropColumn(
                name: "MonthId",
                table: "InvoiceServices");

            migrationBuilder.DropColumn(
                name: "MonthId",
                table: "InvoiceGazs");

            migrationBuilder.DropColumn(
                name: "MonthId",
                table: "InvoiceElectros");

            migrationBuilder.RenameColumn(
                name: "MonthId",
                table: "Contributions",
                newName: "ForPeriod");

            migrationBuilder.AddColumn<string>(
                name: "FlatNumber",
                table: "InvoiceWaters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForPeriod",
                table: "InvoiceWaters",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FlatNumber",
                table: "InvoiceTels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForPeriod",
                table: "InvoiceTels",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FlatNumber",
                table: "InvoiceServices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForPeriod",
                table: "InvoiceServices",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FlatNumber",
                table: "InvoiceGazs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForPeriod",
                table: "InvoiceGazs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FlatNumber",
                table: "InvoiceElectros",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ForPeriod",
                table: "InvoiceElectros",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FlatNumber",
                table: "Contributions",
                nullable: true);
        }
    }
}

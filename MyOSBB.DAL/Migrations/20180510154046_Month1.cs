using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyOSBB.DAL.Migrations
{
    public partial class Month1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contributions_Months_MonthId",
                table: "Contributions");

            migrationBuilder.DropIndex(
                name: "IX_Contributions_MonthId",
                table: "Contributions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Contributions_MonthId",
                table: "Contributions",
                column: "MonthId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contributions_Months_MonthId",
                table: "Contributions",
                column: "MonthId",
                principalTable: "Months",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

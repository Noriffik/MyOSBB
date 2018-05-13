using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MyOSBB.DAL.Migrations
{
    public partial class AllTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.CreateTable(
                name: "InvoiceElectros",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CurrentNumber = table.Column<string>(nullable: true),
                    Debt = table.Column<string>(nullable: true),
                    FlatNumber = table.Column<string>(nullable: true),
                    ForPeriod = table.Column<string>(nullable: true),
                    InvoiceDate = table.Column<DateTime>(nullable: false),
                    Overpaid = table.Column<string>(nullable: true),
                    Payment = table.Column<string>(nullable: true),
                    PrevNumber = table.Column<string>(nullable: true),
                    ProviderName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceElectros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceGazs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CurrentNumber = table.Column<string>(nullable: true),
                    Debt = table.Column<string>(nullable: true),
                    FlatNumber = table.Column<string>(nullable: true),
                    ForPeriod = table.Column<string>(nullable: true),
                    InvoiceDate = table.Column<DateTime>(nullable: false),
                    Overpaid = table.Column<string>(nullable: true),
                    Payment = table.Column<string>(nullable: true),
                    PrevNumber = table.Column<string>(nullable: true),
                    ProviderName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceGazs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceServices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Debt = table.Column<string>(nullable: true),
                    FlatNumber = table.Column<string>(nullable: true),
                    ForPeriod = table.Column<string>(nullable: true),
                    InvoiceDate = table.Column<DateTime>(nullable: false),
                    Overpaid = table.Column<string>(nullable: true),
                    Payment = table.Column<string>(nullable: true),
                    ProviderName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceTels",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Debt = table.Column<string>(nullable: true),
                    FlatNumber = table.Column<string>(nullable: true),
                    ForPeriod = table.Column<string>(nullable: true),
                    InvoiceDate = table.Column<DateTime>(nullable: false),
                    Overpaid = table.Column<string>(nullable: true),
                    Payment = table.Column<string>(nullable: true),
                    ProviderName = table.Column<string>(nullable: true),
                    TelNumber = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceTels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InvoiceWaters",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Debt = table.Column<string>(nullable: true),
                    FlatNumber = table.Column<string>(nullable: true),
                    ForPeriod = table.Column<string>(nullable: true),
                    InvoiceDate = table.Column<DateTime>(nullable: false),
                    Overpaid = table.Column<string>(nullable: true),
                    Payment = table.Column<string>(nullable: true),
                    ProviderName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceWaters", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceElectros");

            migrationBuilder.DropTable(
                name: "InvoiceGazs");

            migrationBuilder.DropTable(
                name: "InvoiceServices");

            migrationBuilder.DropTable(
                name: "InvoiceTels");

            migrationBuilder.DropTable(
                name: "InvoiceWaters");

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    InvoceNumber = table.Column<string>(nullable: true),
                    TotalSum = table.Column<decimal>(nullable: false),
                    CurrentNumber = table.Column<int>(nullable: true),
                    PrevNumber = table.Column<int>(nullable: true),
                    InvoiceGaz_CurrentNumber = table.Column<int>(nullable: true),
                    InvoiceGaz_PrevNumber = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                });
        }
    }
}

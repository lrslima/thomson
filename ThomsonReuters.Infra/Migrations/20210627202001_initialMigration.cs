using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThomsonReuters.Infra.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LegalCase",
                columns: table => new
                {
                    CaseNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourtName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameResponsible = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RegistrationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalCase", x => x.CaseNumber);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LegalCase");
        }
    }
}

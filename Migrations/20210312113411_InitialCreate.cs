using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AtmDynamicTerminalListWorker.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RPA_ATM_Dynamic_List",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TerminalID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OnlineStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BranchCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TerminalBrand = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RPA_ATM_Dynamic_List", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RPA_ATM_GL",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ATM_TerminalID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ATM_GL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RPA_ATM_GL", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RPA_ATM_Dynamic_List");

            migrationBuilder.DropTable(
                name: "RPA_ATM_GL");
        }
    }
}

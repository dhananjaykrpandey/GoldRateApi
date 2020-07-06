using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GoldRateApi.Migrations
{
    public partial class DbGoldRateContextMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_UaeGoldRate",
                columns: table => new
                {
                    iID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cGoldCarat = table.Column<string>(type: "Varchar(20)", nullable: true),
                    rGoldPriceMorning = table.Column<decimal>(type: "numeric(9,3)", nullable: true),
                    rGoldPriceAfternoon = table.Column<decimal>(type: "numeric(9,3)", nullable: true),
                    rGoldPriceEvening = table.Column<decimal>(type: "numeric(9,3)", nullable: true),
                    rGoldPriceYesterday = table.Column<decimal>(type: "numeric(9,3)", nullable: true),
                    dUpdatedDateTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_UaeGoldRate", x => x.iID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_UaeGoldRate");
        }
    }
}

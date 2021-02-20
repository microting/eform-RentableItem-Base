using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Microting.eFormRentableItemBase.Migrations
{
    public partial class AddingContractInspectionItemEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string autoIDGenStrategy = "SqlServer:ValueGenerationStrategy";
            object autoIDGenStrategyValue= MySqlValueGenerationStrategy.IdentityColumn;

            // Setup for MySQL Provider
            if (migrationBuilder.ActiveProvider=="Pomelo.EntityFrameworkCore.MySql")
            {
                DbConfig.IsMySQL = true;
                autoIDGenStrategy = "MySql:ValueGenerationStrategy";
                autoIDGenStrategyValue = MySqlValueGenerationStrategy.IdentityColumn;
            }

            migrationBuilder.DropColumn(
                name: "SDKCaseId",
                table: "ContractInspection");

            migrationBuilder.DropColumn(
                name: "SiteId",
                table: "ContractInspection");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ContractInspection");

            migrationBuilder.AddColumn<int>(
                name: "eFormID",
                table: "RentableItemsVersion",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ContractInspectionItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation(autoIDGenStrategy, autoIDGenStrategyValue),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    WorkflowState = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: false),
                    UpdatedByUserId = table.Column<int>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    ContractInspectionId = table.Column<int>(nullable: false),
                    RentableItemId = table.Column<int>(nullable: false),
                    SDKCaseId = table.Column<int>(nullable: false),
                    SiteId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractInspectionItem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractInspectionItemVersion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation(autoIDGenStrategy, autoIDGenStrategyValue),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: true),
                    WorkflowState = table.Column<string>(maxLength: 255, nullable: true),
                    CreatedByUserId = table.Column<int>(nullable: false),
                    UpdatedByUserId = table.Column<int>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    ContractInspectionId = table.Column<int>(nullable: false),
                    RentableItemId = table.Column<int>(nullable: false),
                    SDKCaseId = table.Column<int>(nullable: false),
                    SiteId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: true),
                    ContractInspectionItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractInspectionItemVersion", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContractInspectionItem");

            migrationBuilder.DropTable(
                name: "ContractInspectionItemVersion");

            migrationBuilder.DropColumn(
                name: "eFormID",
                table: "RentableItemsVersion");

            migrationBuilder.AddColumn<int>(
                name: "SDKCaseId",
                table: "ContractInspection",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SiteId",
                table: "ContractInspection",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ContractInspection",
                nullable: true);
        }
    }
}

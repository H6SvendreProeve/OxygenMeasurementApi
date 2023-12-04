using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OxygenMeasurementApi.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiKeys",
                columns: table => new
                {
                    ApiKeyId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApiKeyValue = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiKeys", x => x.ApiKeyId);
                });

            migrationBuilder.CreateTable(
                name: "OxygenMeasurementSystems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SystemName = table.Column<string>(type: "text", nullable: false),
                    Zipcode = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    ApiKeyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OxygenMeasurementSystems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OxygenMeasurementSystems_ApiKeys_ApiKeyId",
                        column: x => x.ApiKeyId,
                        principalTable: "ApiKeys",
                        principalColumn: "ApiKeyId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OxygenMeasurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OxygenValue = table.Column<decimal>(type: "numeric(4,2)", precision: 4, scale: 2, nullable: false),
                    MeasurementTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OxygenMeasurementSystemId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OxygenMeasurements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OxygenMeasurements_OxygenMeasurementSystems_OxygenMeasureme~",
                        column: x => x.OxygenMeasurementSystemId,
                        principalTable: "OxygenMeasurementSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemNotificationAdvisor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    OxygenMeasurementSystemId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemNotificationAdvisor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SystemNotificationAdvisor_OxygenMeasurementSystems_OxygenMe~",
                        column: x => x.OxygenMeasurementSystemId,
                        principalTable: "OxygenMeasurementSystems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OxygenMeasurements_OxygenMeasurementSystemId",
                table: "OxygenMeasurements",
                column: "OxygenMeasurementSystemId");

            migrationBuilder.CreateIndex(
                name: "IX_OxygenMeasurementSystems_ApiKeyId",
                table: "OxygenMeasurementSystems",
                column: "ApiKeyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SystemNotificationAdvisor_OxygenMeasurementSystemId",
                table: "SystemNotificationAdvisor",
                column: "OxygenMeasurementSystemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OxygenMeasurements");

            migrationBuilder.DropTable(
                name: "SystemNotificationAdvisor");

            migrationBuilder.DropTable(
                name: "OxygenMeasurementSystems");

            migrationBuilder.DropTable(
                name: "ApiKeys");
        }
    }
}

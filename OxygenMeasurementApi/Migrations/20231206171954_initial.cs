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
                    OxygenMeasurementSystemId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SystemName = table.Column<string>(type: "text", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    ApiKeyId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OxygenMeasurementSystems", x => x.OxygenMeasurementSystemId);
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
                    OxygenMeasurementId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OxygenValue = table.Column<decimal>(type: "numeric(4,2)", precision: 4, scale: 2, nullable: false),
                    MeasurementTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OxygenMeasurementSystemId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OxygenMeasurements", x => x.OxygenMeasurementId);
                    table.ForeignKey(
                        name: "FK_OxygenMeasurements_OxygenMeasurementSystems_OxygenMeasureme~",
                        column: x => x.OxygenMeasurementSystemId,
                        principalTable: "OxygenMeasurementSystems",
                        principalColumn: "OxygenMeasurementSystemId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SystemNotificationAdvisors",
                columns: table => new
                {
                    SystemNotificationAdvisorId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    OxygenMeasurementSystemId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemNotificationAdvisors", x => x.SystemNotificationAdvisorId);
                    table.ForeignKey(
                        name: "FK_SystemNotificationAdvisors_OxygenMeasurementSystems_OxygenM~",
                        column: x => x.OxygenMeasurementSystemId,
                        principalTable: "OxygenMeasurementSystems",
                        principalColumn: "OxygenMeasurementSystemId",
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
                name: "IX_SystemNotificationAdvisors_OxygenMeasurementSystemId",
                table: "SystemNotificationAdvisors",
                column: "OxygenMeasurementSystemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OxygenMeasurements");

            migrationBuilder.DropTable(
                name: "SystemNotificationAdvisors");

            migrationBuilder.DropTable(
                name: "OxygenMeasurementSystems");

            migrationBuilder.DropTable(
                name: "ApiKeys");
        }
    }
}

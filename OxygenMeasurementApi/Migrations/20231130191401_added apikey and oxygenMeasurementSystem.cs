using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OxygenMeasurementApi.Migrations
{
    /// <inheritdoc />
    public partial class addedapikeyandoxygenMeasurementSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiKeys",
                columns: table => new
                {
                    ApiKeyId = table.Column<string>(type: "text", nullable: false)
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
                    AdminstratorEmail = table.Column<string>(type: "text", nullable: false),
                    ApiKeyId = table.Column<string>(type: "text", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_OxygenMeasurementSystems_ApiKeyId",
                table: "OxygenMeasurementSystems",
                column: "ApiKeyId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OxygenMeasurementSystems");

            migrationBuilder.DropTable(
                name: "ApiKeys");
        }
    }
}

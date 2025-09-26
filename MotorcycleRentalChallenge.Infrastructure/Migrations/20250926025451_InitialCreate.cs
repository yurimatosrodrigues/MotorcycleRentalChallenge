using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MotorcycleRentalChallenge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeliveryDrivers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Cnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    Birthdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CnhNumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    CnhType = table.Column<int>(type: "integer", nullable: false),
                    CnhImagePath = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryDrivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Motorcycles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Model = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false),
                    Plate = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motorcycles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RentalPlan",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Days = table.Column<int>(type: "integer", nullable: false),
                    DailyRate = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    PenaltyPercentageForUnusedDays = table.Column<decimal>(type: "numeric(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalPlan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MotorcycleId = table.Column<Guid>(type: "uuid", nullable: false),
                    DeliveryDriverId = table.Column<Guid>(type: "uuid", nullable: false),
                    RentalPlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpectedEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DailyRate = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    TotalCost = table.Column<decimal>(type: "numeric(18,2)", nullable: false, defaultValue: 0m),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rentals_DeliveryDrivers_DeliveryDriverId",
                        column: x => x.DeliveryDriverId,
                        principalTable: "DeliveryDrivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rentals_Motorcycles_MotorcycleId",
                        column: x => x.MotorcycleId,
                        principalTable: "Motorcycles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Rentals_RentalPlan_RentalPlanId",
                        column: x => x.RentalPlanId,
                        principalTable: "RentalPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "RentalPlan",
                columns: new[] { "Id", "CreatedAt", "DailyRate", "Days", "PenaltyPercentageForUnusedDays" },
                values: new object[,]
                {
                    { new Guid("1d761336-3a80-44e5-859b-9f7108aff485"), new DateTime(2025, 9, 26, 2, 54, 49, 586, DateTimeKind.Utc).AddTicks(708), 22m, 30, null },
                    { new Guid("39ded9a8-4373-4a0a-97d7-37ddb2173bf1"), new DateTime(2025, 9, 26, 2, 54, 49, 586, DateTimeKind.Utc).AddTicks(710), 20m, 45, null },
                    { new Guid("7ab0ba26-53b4-4d95-bab3-9cc4dc026f97"), new DateTime(2025, 9, 26, 2, 54, 49, 586, DateTimeKind.Utc).AddTicks(706), 28m, 15, 0.4m },
                    { new Guid("d3b8c856-d4dd-4ea4-871b-054c8b2ac14c"), new DateTime(2025, 9, 26, 2, 54, 49, 586, DateTimeKind.Utc).AddTicks(694), 30m, 7, 0.2m },
                    { new Guid("f321d945-5f63-4770-815e-894c43cc7abe"), new DateTime(2025, 9, 26, 2, 54, 49, 586, DateTimeKind.Utc).AddTicks(711), 18m, 50, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryDrivers_CnhNumber",
                table: "DeliveryDrivers",
                column: "CnhNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryDrivers_Cnpj",
                table: "DeliveryDrivers",
                column: "Cnpj",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Motorcycles_Plate",
                table: "Motorcycles",
                column: "Plate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_DeliveryDriverId",
                table: "Rentals",
                column: "DeliveryDriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_MotorcycleId",
                table: "Rentals",
                column: "MotorcycleId");

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_RentalPlanId",
                table: "Rentals",
                column: "RentalPlanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "DeliveryDrivers");

            migrationBuilder.DropTable(
                name: "Motorcycles");

            migrationBuilder.DropTable(
                name: "RentalPlan");
        }
    }
}

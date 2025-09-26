using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MotorcycleRentalChallenge.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentifierToMotoAndDeliveryDriver : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RentalPlan",
                keyColumn: "Id",
                keyValue: new Guid("1d761336-3a80-44e5-859b-9f7108aff485"));

            migrationBuilder.DeleteData(
                table: "RentalPlan",
                keyColumn: "Id",
                keyValue: new Guid("39ded9a8-4373-4a0a-97d7-37ddb2173bf1"));

            migrationBuilder.DeleteData(
                table: "RentalPlan",
                keyColumn: "Id",
                keyValue: new Guid("7ab0ba26-53b4-4d95-bab3-9cc4dc026f97"));

            migrationBuilder.DeleteData(
                table: "RentalPlan",
                keyColumn: "Id",
                keyValue: new Guid("d3b8c856-d4dd-4ea4-871b-054c8b2ac14c"));

            migrationBuilder.DeleteData(
                table: "RentalPlan",
                keyColumn: "Id",
                keyValue: new Guid("f321d945-5f63-4770-815e-894c43cc7abe"));

            migrationBuilder.AddColumn<string>(
                name: "Identifier",
                table: "Motorcycles",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Identifier",
                table: "DeliveryDrivers",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "RentalPlan",
                columns: new[] { "Id", "CreatedAt", "DailyRate", "Days", "PenaltyPercentageForUnusedDays" },
                values: new object[,]
                {
                    { new Guid("41e5d448-9f0d-4992-a6c9-5676d6871fc4"), new DateTime(2025, 9, 26, 20, 25, 11, 315, DateTimeKind.Utc).AddTicks(9702), 18m, 50, null },
                    { new Guid("426a33dc-7b4c-4b55-a195-e9b82e8098d3"), new DateTime(2025, 9, 26, 20, 25, 11, 315, DateTimeKind.Utc).AddTicks(9700), 22m, 30, null },
                    { new Guid("5b4b1cb5-8d2c-4b1b-9553-03b61cb0e44f"), new DateTime(2025, 9, 26, 20, 25, 11, 315, DateTimeKind.Utc).AddTicks(9701), 20m, 45, null },
                    { new Guid("ec5d34e7-ed4e-427f-a28e-4c58c9c94193"), new DateTime(2025, 9, 26, 20, 25, 11, 315, DateTimeKind.Utc).AddTicks(9698), 28m, 15, 0.4m },
                    { new Guid("fc520cfd-ce80-4a0f-9a0d-da6cc2612fd5"), new DateTime(2025, 9, 26, 20, 25, 11, 315, DateTimeKind.Utc).AddTicks(9694), 30m, 7, 0.2m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RentalPlan",
                keyColumn: "Id",
                keyValue: new Guid("41e5d448-9f0d-4992-a6c9-5676d6871fc4"));

            migrationBuilder.DeleteData(
                table: "RentalPlan",
                keyColumn: "Id",
                keyValue: new Guid("426a33dc-7b4c-4b55-a195-e9b82e8098d3"));

            migrationBuilder.DeleteData(
                table: "RentalPlan",
                keyColumn: "Id",
                keyValue: new Guid("5b4b1cb5-8d2c-4b1b-9553-03b61cb0e44f"));

            migrationBuilder.DeleteData(
                table: "RentalPlan",
                keyColumn: "Id",
                keyValue: new Guid("ec5d34e7-ed4e-427f-a28e-4c58c9c94193"));

            migrationBuilder.DeleteData(
                table: "RentalPlan",
                keyColumn: "Id",
                keyValue: new Guid("fc520cfd-ce80-4a0f-9a0d-da6cc2612fd5"));

            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "Motorcycles");

            migrationBuilder.DropColumn(
                name: "Identifier",
                table: "DeliveryDrivers");

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
        }
    }
}

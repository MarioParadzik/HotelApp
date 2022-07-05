using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelApp.Api.Migrations
{
    public partial class Final : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "48853a88-29d7-49dd-8c53-169c27fba43a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "93aaa716-58d6-439f-a714-eb047d23df42");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "7e02b366-6eab-4d49-b7f9-8d78396943bb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "192a6ea3-4745-4042-a2ac-f3ea61b77573");

            migrationBuilder.UpdateData(
                table: "ReservationStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Rejected");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "99700ab7-364c-40c9-a5c1-72fe93e853dc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "3fbc8f86-c310-4f08-a743-e9dd55862ef9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "6580e1c3-a6a1-471e-94a1-145611919832");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                column: "ConcurrencyStamp",
                value: "4410d78e-671d-4cf4-9eb4-e8873520df3e");

            migrationBuilder.UpdateData(
                table: "ReservationStatus",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Inactive");
        }
    }
}

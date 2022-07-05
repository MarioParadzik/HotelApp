using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelApp.Api.Migrations
{
    public partial class updatedSeededRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6580e1c3-a6a1-471e-94a1-145611919832", "Hotel Manager", "HOTEL MANAGER" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4410d78e-671d-4cf4-9eb4-e8873520df3e", "Registered User", "REGISTERED USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "deb5c592-32ad-4477-aaf1-1eb449d7f250");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "5d519e09-a0e7-4684-bece-db4642ea84d1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5ced0808-2253-438d-99c6-5ebc6326f240", "Upravitelj Hotela", "UPRAVITELJ HOTELA" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "09d52086-1814-4cc6-ad20-01f835135ea4", "Registrirani korisnik", "REGISTRIRANI KORISNIK" });
        }
    }
}

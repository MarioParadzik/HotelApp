using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelApp.Api.Migrations
{
    public partial class seededRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Room",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ReservationStatus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Reservation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PostOffice",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Location",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "HotelUser",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "HotelStatus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Hotel",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Configuration",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "deb5c592-32ad-4477-aaf1-1eb449d7f250", "Role", "Super Administrator", "SUPER ADMINISTRATOR" },
                    { 2, "5d519e09-a0e7-4684-bece-db4642ea84d1", "Role", "Administrator", "ADMINISTRATOR" },
                    { 3, "5ced0808-2253-438d-99c6-5ebc6326f240", "Role", "Upravitelj Hotela", "UPRAVITELJ HOTELA" },
                    { 4, "09d52086-1814-4cc6-ad20-01f835135ea4", "Role", "Registrirani korisnik", "REGISTRIRANI KORISNIK" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Room");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ReservationStatus");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PostOffice");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Location");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "HotelUser");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "HotelStatus");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Hotel");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Configuration");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");
        }
    }
}

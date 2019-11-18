using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TourMarket.Migrations
{
    public partial class Init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "Date", "TourId" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 11, 18, 22, 4, 56, 536, DateTimeKind.Local).AddTicks(3214), 1 },
                    { 2, new DateTime(2019, 11, 17, 22, 4, 56, 536, DateTimeKind.Local).AddTicks(7020), 2 },
                    { 3, new DateTime(2019, 11, 16, 22, 4, 56, 536, DateTimeKind.Local).AddTicks(7049), 3 },
                    { 4, new DateTime(2019, 11, 15, 22, 4, 56, 536, DateTimeKind.Local).AddTicks(7053), 4 }
                });

            migrationBuilder.InsertData(
                table: "OrderManagers",
                columns: new[] { "OrderId", "ManagerId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 }
                });

            migrationBuilder.InsertData(
                table: "OrderTourist",
                columns: new[] { "OrderId", "TouristId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "OrderManagers",
                keyColumns: new[] { "OrderId", "ManagerId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "OrderManagers",
                keyColumns: new[] { "OrderId", "ManagerId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "OrderManagers",
                keyColumns: new[] { "OrderId", "ManagerId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "OrderManagers",
                keyColumns: new[] { "OrderId", "ManagerId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "OrderTourist",
                keyColumns: new[] { "OrderId", "TouristId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "OrderTourist",
                keyColumns: new[] { "OrderId", "TouristId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "OrderTourist",
                keyColumns: new[] { "OrderId", "TouristId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "OrderTourist",
                keyColumns: new[] { "OrderId", "TouristId" },
                keyValues: new object[] { 4, 4 });

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}

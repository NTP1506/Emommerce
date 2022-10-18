using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceAPI.Migrations
{
    public partial class data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 10, 18, 16, 51, 2, 586, DateTimeKind.Local).AddTicks(9929), new DateTime(2022, 10, 18, 16, 51, 2, 586, DateTimeKind.Local).AddTicks(9937) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 10, 18, 16, 51, 2, 586, DateTimeKind.Local).AddTicks(9966), new DateTime(2022, 10, 18, 16, 51, 2, 586, DateTimeKind.Local).AddTicks(9966) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 10, 18, 16, 51, 2, 586, DateTimeKind.Local).AddTicks(9976), new DateTime(2022, 10, 18, 16, 51, 2, 586, DateTimeKind.Local).AddTicks(9976) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 10, 18, 16, 51, 2, 586, DateTimeKind.Local).AddTicks(9985), new DateTime(2022, 10, 18, 16, 51, 2, 586, DateTimeKind.Local).AddTicks(9985) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 10, 18, 16, 51, 2, 586, DateTimeKind.Local).AddTicks(9993), new DateTime(2022, 10, 18, 16, 51, 2, 586, DateTimeKind.Local).AddTicks(9994) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 10, 18, 16, 51, 2, 587, DateTimeKind.Local).AddTicks(4), new DateTime(2022, 10, 18, 16, 51, 2, 587, DateTimeKind.Local).AddTicks(5) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 10, 18, 16, 51, 2, 587, DateTimeKind.Local).AddTicks(13), new DateTime(2022, 10, 18, 16, 51, 2, 587, DateTimeKind.Local).AddTicks(13) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 10, 18, 16, 51, 2, 587, DateTimeKind.Local).AddTicks(21), new DateTime(2022, 10, 18, 16, 51, 2, 587, DateTimeKind.Local).AddTicks(22) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 10, 18, 16, 31, 31, 553, DateTimeKind.Local).AddTicks(7923), new DateTime(2022, 10, 18, 16, 31, 31, 553, DateTimeKind.Local).AddTicks(7940) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 10, 18, 16, 31, 31, 553, DateTimeKind.Local).AddTicks(7969), new DateTime(2022, 10, 18, 16, 31, 31, 553, DateTimeKind.Local).AddTicks(7969) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 3,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 10, 18, 16, 31, 31, 553, DateTimeKind.Local).AddTicks(7978), new DateTime(2022, 10, 18, 16, 31, 31, 553, DateTimeKind.Local).AddTicks(7979) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 4,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 10, 18, 16, 31, 31, 553, DateTimeKind.Local).AddTicks(7987), new DateTime(2022, 10, 18, 16, 31, 31, 553, DateTimeKind.Local).AddTicks(7988) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 5,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 10, 18, 16, 31, 31, 553, DateTimeKind.Local).AddTicks(7995), new DateTime(2022, 10, 18, 16, 31, 31, 553, DateTimeKind.Local).AddTicks(7996) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 6,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 10, 18, 16, 31, 31, 553, DateTimeKind.Local).AddTicks(8006), new DateTime(2022, 10, 18, 16, 31, 31, 553, DateTimeKind.Local).AddTicks(8006) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 7,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 10, 18, 16, 31, 31, 553, DateTimeKind.Local).AddTicks(8014), new DateTime(2022, 10, 18, 16, 31, 31, 553, DateTimeKind.Local).AddTicks(8015) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: 8,
                columns: new[] { "DateCreated", "DateModified" },
                values: new object[] { new DateTime(2022, 10, 18, 16, 31, 31, 553, DateTimeKind.Local).AddTicks(8023), new DateTime(2022, 10, 18, 16, 31, 31, 553, DateTimeKind.Local).AddTicks(8023) });
        }
    }
}

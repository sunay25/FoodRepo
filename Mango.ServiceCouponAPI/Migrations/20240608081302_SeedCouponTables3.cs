﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mango.ServiceCouponAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedCouponTables3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "CouponCode", "DiscountAmount", "MinimumAmount" },
                values: new object[] { 1, "10OFF", 10.0, 20 });
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mango.ServiceCouponAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedCouponTables2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Coupons",
                columns: new[] { "CouponId", "CouponCode", "DiscountAmount", "MinimumAmount" },
                values: new object[] { 2, "20OFF", 20.0, 40 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Coupons",
                keyColumn: "CouponId",
                keyValue: 2);
        }
    }
}

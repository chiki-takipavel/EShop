using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Discount.Grpc.Migrations
{
    /// <inheritdoc />
    public partial class RenameAmountColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Coupones",
                newName: "Percentage");

            migrationBuilder.UpdateData(
                table: "Coupones",
                keyColumn: "Id",
                keyValue: 1,
                column: "Percentage",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Coupones",
                keyColumn: "Id",
                keyValue: 2,
                column: "Percentage",
                value: 4);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Percentage",
                table: "Coupones",
                newName: "Amount");

            migrationBuilder.UpdateData(
                table: "Coupones",
                keyColumn: "Id",
                keyValue: 1,
                column: "Amount",
                value: 200);

            migrationBuilder.UpdateData(
                table: "Coupones",
                keyColumn: "Id",
                keyValue: 2,
                column: "Amount",
                value: 150);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingCustomerApi.Migrations
{
    /// <inheritdoc />
    public partial class addnewuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerID", "CustomerName", "Email", "Password", "PhoneNumber" },
                values: new object[] { 1, "Antony", "anto@gmail.com", "abce", "9874526965" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "CustomerID",
                keyValue: 1);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class BringDBHere : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9d9d966-a949-4e6d-88e8-a8d962351c04");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd57ad4e-1bcf-4b34-b3cd-411f4ef29030");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "245aceec-f8c6-4f00-b49d-77cb6c72817e", null, "Admin", "ADMIN" },
                    { "5eca7826-52d4-442b-a7ea-46f8e6a11ac3", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "245aceec-f8c6-4f00-b49d-77cb6c72817e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5eca7826-52d4-442b-a7ea-46f8e6a11ac3");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b9d9d966-a949-4e6d-88e8-a8d962351c04", null, "Admin", "ADMIN" },
                    { "fd57ad4e-1bcf-4b34-b3cd-411f4ef29030", null, "User", "USER" }
                });
        }
    }
}

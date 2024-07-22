using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class ReturnToStateBeforeUserComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_CommentByUserId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CommentByUserId",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73cfab37-d238-4a40-a685-ba05e66c3f83");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a0fb9a43-d456-4682-ba8d-f4818a929f59");

            migrationBuilder.DropColumn(
                name: "CommentByUserId",
                table: "Comments");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b9d9d966-a949-4e6d-88e8-a8d962351c04", null, "Admin", "ADMIN" },
                    { "fd57ad4e-1bcf-4b34-b3cd-411f4ef29030", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b9d9d966-a949-4e6d-88e8-a8d962351c04");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd57ad4e-1bcf-4b34-b3cd-411f4ef29030");

            migrationBuilder.AddColumn<string>(
                name: "CommentByUserId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "73cfab37-d238-4a40-a685-ba05e66c3f83", null, "Admin", "ADMIN" },
                    { "a0fb9a43-d456-4682-ba8d-f4818a929f59", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentByUserId",
                table: "Comments",
                column: "CommentByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_CommentByUserId",
                table: "Comments",
                column: "CommentByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

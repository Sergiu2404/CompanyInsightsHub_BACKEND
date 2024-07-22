using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentBySpecifiedUser2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7e1d9784-c4d9-4bf4-8721-aa09f0946a1c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "efeff806-711b-4664-9cb9-b9a4a79d38ae");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Comments",
                newName: "CommentByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                newName: "IX_Comments_CommentByUserId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "73cfab37-d238-4a40-a685-ba05e66c3f83", null, "Admin", "ADMIN" },
                    { "a0fb9a43-d456-4682-ba8d-f4818a929f59", null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_CommentByUserId",
                table: "Comments",
                column: "CommentByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_CommentByUserId",
                table: "Comments");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "73cfab37-d238-4a40-a685-ba05e66c3f83");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a0fb9a43-d456-4682-ba8d-f4818a929f59");

            migrationBuilder.RenameColumn(
                name: "CommentByUserId",
                table: "Comments",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_CommentByUserId",
                table: "Comments",
                newName: "IX_Comments_UserId");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7e1d9784-c4d9-4bf4-8721-aa09f0946a1c", null, "Admin", "ADMIN" },
                    { "efeff806-711b-4664-9cb9-b9a4a79d38ae", null, "User", "USER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

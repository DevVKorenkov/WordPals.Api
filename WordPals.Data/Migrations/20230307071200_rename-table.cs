using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordPals.Data.Migrations
{
    /// <inheritdoc />
    public partial class renametable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_AppUserId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Vocabulary_AspNetUsers_AppUserId",
                table: "Vocabulary");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "Vocabulary",
                newName: "AppIdentityUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Vocabulary_AppUserId",
                table: "Vocabulary",
                newName: "IX_Vocabulary_AppIdentityUserId");

            migrationBuilder.RenameColumn(
                name: "AppUserId",
                table: "AspNetUsers",
                newName: "AppIdentityUserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_AppUserId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_AppIdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_AppIdentityUserId",
                table: "AspNetUsers",
                column: "AppIdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vocabulary_AspNetUsers_AppIdentityUserId",
                table: "Vocabulary",
                column: "AppIdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_AppIdentityUserId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Vocabulary_AspNetUsers_AppIdentityUserId",
                table: "Vocabulary");

            migrationBuilder.RenameColumn(
                name: "AppIdentityUserId",
                table: "Vocabulary",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Vocabulary_AppIdentityUserId",
                table: "Vocabulary",
                newName: "IX_Vocabulary_AppUserId");

            migrationBuilder.RenameColumn(
                name: "AppIdentityUserId",
                table: "AspNetUsers",
                newName: "AppUserId");

            migrationBuilder.RenameIndex(
                name: "IX_AspNetUsers_AppIdentityUserId",
                table: "AspNetUsers",
                newName: "IX_AspNetUsers_AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_AppUserId",
                table: "AspNetUsers",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vocabulary_AspNetUsers_AppUserId",
                table: "Vocabulary",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}

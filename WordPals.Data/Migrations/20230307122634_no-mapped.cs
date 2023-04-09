using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WordPals.Data.Migrations
{
    /// <inheritdoc />
    public partial class nomapped : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vocabulary_AspNetUsers_AppIdentityUserId",
                table: "Vocabulary");

            migrationBuilder.DropIndex(
                name: "IX_Vocabulary_AppIdentityUserId",
                table: "Vocabulary");

            migrationBuilder.DropColumn(
                name: "AppIdentityUserId",
                table: "Vocabulary");

            migrationBuilder.AddColumn<bool>(
                name: "IsAllowToShowEmail",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FavoriteWords",
                columns: table => new
                {
                    FavoriteWordsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UsersWhoHaveWordId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteWords", x => new { x.FavoriteWordsId, x.UsersWhoHaveWordId });
                    table.ForeignKey(
                        name: "FK_FavoriteWords_AspNetUsers_UsersWhoHaveWordId",
                        column: x => x.UsersWhoHaveWordId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteWords_Vocabulary_FavoriteWordsId",
                        column: x => x.FavoriteWordsId,
                        principalTable: "Vocabulary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteWords_UsersWhoHaveWordId",
                table: "FavoriteWords",
                column: "UsersWhoHaveWordId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteWords");

            migrationBuilder.DropColumn(
                name: "IsAllowToShowEmail",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AppIdentityUserId",
                table: "Vocabulary",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vocabulary_AppIdentityUserId",
                table: "Vocabulary",
                column: "AppIdentityUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vocabulary_AspNetUsers_AppIdentityUserId",
                table: "Vocabulary",
                column: "AppIdentityUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}

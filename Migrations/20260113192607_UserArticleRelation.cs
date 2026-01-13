using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GData.Migrations
{
    /// <inheritdoc />
    public partial class UserArticleRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleUser",
                columns: table => new
                {
                    FavouriteArticlesId = table.Column<Guid>(type: "uuid", nullable: false),
                    UsersFavoringArticleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleUser", x => new { x.FavouriteArticlesId, x.UsersFavoringArticleId });
                    table.ForeignKey(
                        name: "FK_ArticleUser_Articles_FavouriteArticlesId",
                        column: x => x.FavouriteArticlesId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleUser_Users_UsersFavoringArticleId",
                        column: x => x.UsersFavoringArticleId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleUser_UsersFavoringArticleId",
                table: "ArticleUser",
                column: "UsersFavoringArticleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleUser");
        }
    }
}

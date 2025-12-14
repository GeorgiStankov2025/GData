using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GData.Migrations
{
    /// <inheritdoc />
    public partial class AddArticleTagsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleTags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ArticleArticleTag",
                columns: table => new
                {
                    ArticleTagsId = table.Column<Guid>(type: "uuid", nullable: false),
                    ArticlesId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleArticleTag", x => new { x.ArticleTagsId, x.ArticlesId });
                    table.ForeignKey(
                        name: "FK_ArticleArticleTag_ArticleTags_ArticleTagsId",
                        column: x => x.ArticleTagsId,
                        principalTable: "ArticleTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleArticleTag_Articles_ArticlesId",
                        column: x => x.ArticlesId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleArticleTag_ArticlesId",
                table: "ArticleArticleTag",
                column: "ArticlesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleArticleTag");

            migrationBuilder.DropTable(
                name: "ArticleTags");
        }
    }
}

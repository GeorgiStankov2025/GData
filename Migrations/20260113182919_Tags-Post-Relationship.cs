using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GData.Migrations
{
    /// <inheritdoc />
    public partial class TagsPostRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleTagPost",
                columns: table => new
                {
                    PostTagsId = table.Column<Guid>(type: "uuid", nullable: false),
                    PostsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTagPost", x => new { x.PostTagsId, x.PostsId });
                    table.ForeignKey(
                        name: "FK_ArticleTagPost_ArticleTags_PostTagsId",
                        column: x => x.PostTagsId,
                        principalTable: "ArticleTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleTagPost_Posts_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTagPost_PostsId",
                table: "ArticleTagPost",
                column: "PostsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleTagPost");
        }
    }
}

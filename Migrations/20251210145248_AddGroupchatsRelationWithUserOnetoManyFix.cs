using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GData.Migrations
{
    /// <inheritdoc />
    public partial class AddGroupchatsRelationWithUserOnetoManyFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groupchats_Users_creatorId",
                table: "Groupchats");

            migrationBuilder.RenameColumn(
                name: "creatorId",
                table: "Groupchats",
                newName: "CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Groupchats_creatorId",
                table: "Groupchats",
                newName: "IX_Groupchats_CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groupchats_Users_CreatorId",
                table: "Groupchats",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groupchats_Users_CreatorId",
                table: "Groupchats");

            migrationBuilder.RenameColumn(
                name: "CreatorId",
                table: "Groupchats",
                newName: "creatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Groupchats_CreatorId",
                table: "Groupchats",
                newName: "IX_Groupchats_creatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groupchats_Users_creatorId",
                table: "Groupchats",
                column: "creatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

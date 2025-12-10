using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GData.Migrations
{
    /// <inheritdoc />
    public partial class AddGroupchatsRelationWithUserOnetoMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "creatorId",
                table: "Groupchats",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Groupchats_creatorId",
                table: "Groupchats",
                column: "creatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groupchats_Users_creatorId",
                table: "Groupchats",
                column: "creatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groupchats_Users_creatorId",
                table: "Groupchats");

            migrationBuilder.DropIndex(
                name: "IX_Groupchats_creatorId",
                table: "Groupchats");

            migrationBuilder.DropColumn(
                name: "creatorId",
                table: "Groupchats");
        }
    }
}

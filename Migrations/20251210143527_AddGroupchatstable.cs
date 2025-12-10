using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GData.Migrations
{
    /// <inheritdoc />
    public partial class AddGroupchatstable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groupchats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatName = table.Column<string>(type: "text", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groupchats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupchatUser",
                columns: table => new
                {
                    ChatMembersId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserGroupchatsId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupchatUser", x => new { x.ChatMembersId, x.UserGroupchatsId });
                    table.ForeignKey(
                        name: "FK_GroupchatUser_Groupchats_UserGroupchatsId",
                        column: x => x.UserGroupchatsId,
                        principalTable: "Groupchats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupchatUser_Users_ChatMembersId",
                        column: x => x.ChatMembersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupchatUser_UserGroupchatsId",
                table: "GroupchatUser",
                column: "UserGroupchatsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupchatUser");

            migrationBuilder.DropTable(
                name: "Groupchats");
        }
    }
}

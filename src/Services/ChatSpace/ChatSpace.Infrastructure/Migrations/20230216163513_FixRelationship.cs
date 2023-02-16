using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Corpspace.ChatSpace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChannelMembers");

            migrationBuilder.CreateIndex(
                name: "IX_ChatUsers_ChannelId",
                table: "ChatUsers",
                column: "ChannelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatUsers_ChatSpace_Channel_ChannelId",
                table: "ChatUsers",
                column: "ChannelId",
                principalTable: "ChatSpace_Channel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatUsers_ChatSpace_Channel_ChannelId",
                table: "ChatUsers");

            migrationBuilder.DropIndex(
                name: "IX_ChatUsers_ChannelId",
                table: "ChatUsers");

            migrationBuilder.CreateTable(
                name: "ChannelMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChannelId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DeletionAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ModificationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChannelMembers_ChatSpace_Channel_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "ChatSpace_Channel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChannelMembers_ChatUsers_ChatUserId",
                        column: x => x.ChatUserId,
                        principalTable: "ChatUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChannelMembers_ChannelId",
                table: "ChannelMembers",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_ChannelMembers_ChatUserId",
                table: "ChannelMembers",
                column: "ChatUserId");
        }
    }
}

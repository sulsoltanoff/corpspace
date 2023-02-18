using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Corpspace.ChatSpace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChannelType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatSpace_User_ChatSpace_Team_UserTeamId",
                table: "ChatSpace_User");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserTeamId",
                table: "ChatSpace_User",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<DateTime>(
                name: "JoinTime",
                table: "ChatSpace_User",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "ChatSpace_User",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "ChannelsType",
                table: "ChatSpace_Channel",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "ChannelType",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    ModificationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DeletionAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChannelType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserChannel",
                columns: table => new
                {
                    ChannelId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChannel", x => new { x.ChannelId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserChannel_ChatSpace_Channel_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "ChatSpace_Channel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChannel_ChatSpace_User_UserId",
                        column: x => x.UserId,
                        principalTable: "ChatSpace_User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "UserChannel",
                columns: new[] { "ChannelId", "UserId" },
                values: new object[] { new Guid("c73e09c9-2427-4e3d-bb08-244bb93f1f23"), new Guid("206b50ae-13da-4e23-9483-350c65794917") });

            migrationBuilder.CreateIndex(
                name: "IX_ChatSpace_User_UserId",
                table: "ChatSpace_User",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChannel_UserId",
                table: "UserChannel",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatSpace_User_ChatSpace_Team_UserTeamId",
                table: "ChatSpace_User",
                column: "UserTeamId",
                principalTable: "ChatSpace_Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_ChatSpace_User_ChatSpace_User_UserId",
                table: "ChatSpace_User",
                column: "UserId",
                principalTable: "ChatSpace_User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatSpace_User_ChatSpace_Team_UserTeamId",
                table: "ChatSpace_User");

            migrationBuilder.DropForeignKey(
                name: "FK_ChatSpace_User_ChatSpace_User_UserId",
                table: "ChatSpace_User");

            migrationBuilder.DropTable(
                name: "ChannelType");

            migrationBuilder.DropTable(
                name: "UserChannel");

            migrationBuilder.DropIndex(
                name: "IX_ChatSpace_User_UserId",
                table: "ChatSpace_User");

            migrationBuilder.DropColumn(
                name: "JoinTime",
                table: "ChatSpace_User");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ChatSpace_User");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserTeamId",
                table: "ChatSpace_User",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ChannelsType",
                table: "ChatSpace_Channel",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatSpace_User_ChatSpace_Team_UserTeamId",
                table: "ChatSpace_User",
                column: "UserTeamId",
                principalTable: "ChatSpace_Team",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Corpspace.ChatSpace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChatSpace_Channel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: true),
                    ChannelsType = table.Column<int>(type: "integer", nullable: true),
                    DisplayName = table.Column<string>(type: "character varying(76)", maxLength: 76, nullable: true),
                    Description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "character varying(76)", maxLength: 76, nullable: true),
                    LastPostAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: true),
                    ModificationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DeletionAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatSpace_Channel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatSpace_Draft",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChannelId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    ModificationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DeletionAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatSpace_Draft", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatSpace_Image",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ModificationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DeletionAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatSpace_Image", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatSpace_Metadata",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Embeds = table.Column<string>(type: "text", nullable: false),
                    Emojis = table.Column<string>(type: "text", nullable: false),
                    Files = table.Column<string>(type: "text", nullable: false),
                    Images = table.Column<string>(type: "text", nullable: false),
                    Reactions = table.Column<string>(type: "text", nullable: false),
                    Priority = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Acknowledgements = table.Column<string>(type: "text", nullable: false),
                    ModificationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DeletionAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatSpace_Metadata", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatSpace_Team",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(76)", maxLength: 76, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(76)", maxLength: 76, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true, defaultValue: "512"),
                    ModificationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DeletionAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatSpace_Team", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatSpace_Threads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Total = table.Column<long>(type: "bigint", nullable: false),
                    TotalUnreadThreads = table.Column<long>(type: "bigint", nullable: false),
                    TotalUnreadMentions = table.Column<long>(type: "bigint", nullable: false),
                    TotalUnreadUrgentMentions = table.Column<long>(type: "bigint", nullable: false),
                    ModificationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DeletionAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatSpace_Threads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatSpace_Message",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChannelId = table.Column<Guid>(type: "uuid", nullable: false),
                    OriginalId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsPinned = table.Column<bool>(type: "boolean", nullable: false),
                    Content = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Props = table.Column<string>(type: "text", nullable: false),
                    Hashtags = table.Column<string>(type: "text", nullable: false),
                    FileIds = table.Column<string>(type: "text", nullable: false),
                    HasReactions = table.Column<bool>(type: "boolean", nullable: false),
                    ReplyCount = table.Column<long>(type: "bigint", nullable: false),
                    Participants = table.Column<string>(type: "text", nullable: false),
                    IsFollowing = table.Column<bool>(type: "boolean", nullable: true),
                    MetadataId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModificationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DeletionAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatSpace_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatSpace_Message_ChatSpace_Metadata_MetadataId",
                        column: x => x.MetadataId,
                        principalTable: "ChatSpace_Metadata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatSpace_ThreadResponse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageId = table.Column<Guid>(type: "uuid", nullable: false),
                    ReplyCount = table.Column<long>(type: "bigint", nullable: false),
                    LastReplyAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    LastViewedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ThreadsId = table.Column<Guid>(type: "uuid", nullable: false),
                    UnreadReplies = table.Column<long>(type: "bigint", nullable: false),
                    UnreadMentions = table.Column<long>(type: "bigint", nullable: false),
                    IsUrgent = table.Column<bool>(type: "boolean", nullable: false),
                    ModificationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DeletionAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatSpace_ThreadResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatSpace_ThreadResponse_ChatSpace_Message_MessageId",
                        column: x => x.MessageId,
                        principalTable: "ChatSpace_Message",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatSpace_ThreadResponse_ChatSpace_Threads_ThreadsId",
                        column: x => x.ThreadsId,
                        principalTable: "ChatSpace_Threads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatSpace_User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    EmailVerified = table.Column<bool>(type: "boolean", nullable: true, defaultValue: false),
                    FirstName = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    LastName = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Position = table.Column<string>(type: "text", nullable: false),
                    Roles = table.Column<string>(type: "text", nullable: false),
                    ChannelId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserTeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: true),
                    Props = table.Column<Dictionary<string, string>>(type: "jsonb", nullable: false),
                    NotifyProps = table.Column<Dictionary<string, string>>(type: "jsonb", nullable: false),
                    LastPictureUpdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    FailedAttempts = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    Locale = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    LastActivityAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsBot = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    BotDescription = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    BotLastIconUpdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    ModificationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DeletionAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    ThreadResponseId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatSpace_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatSpace_User_ChatSpace_Channel_ChannelId",
                        column: x => x.ChannelId,
                        principalTable: "ChatSpace_Channel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatSpace_User_ChatSpace_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "ChatSpace_Team",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatSpace_User_ChatSpace_Team_UserTeamId",
                        column: x => x.UserTeamId,
                        principalTable: "ChatSpace_Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ChatSpace_User_ChatSpace_ThreadResponse_ThreadResponseId",
                        column: x => x.ThreadResponseId,
                        principalTable: "ChatSpace_ThreadResponse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatSpace_Message_ChannelId",
                table: "ChatSpace_Message",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatSpace_Message_CreationAt",
                table: "ChatSpace_Message",
                column: "CreationAt");

            migrationBuilder.CreateIndex(
                name: "IX_ChatSpace_Message_IsDeleted",
                table: "ChatSpace_Message",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ChatSpace_Message_MetadataId",
                table: "ChatSpace_Message",
                column: "MetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatSpace_ThreadResponse_MessageId",
                table: "ChatSpace_ThreadResponse",
                column: "MessageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatSpace_ThreadResponse_ThreadsId",
                table: "ChatSpace_ThreadResponse",
                column: "ThreadsId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatSpace_User_ChannelId",
                table: "ChatSpace_User",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatSpace_User_TeamId",
                table: "ChatSpace_User",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatSpace_User_ThreadResponseId",
                table: "ChatSpace_User",
                column: "ThreadResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatSpace_User_UserTeamId",
                table: "ChatSpace_User",
                column: "UserTeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatSpace_Draft");

            migrationBuilder.DropTable(
                name: "ChatSpace_Image");

            migrationBuilder.DropTable(
                name: "ChatSpace_User");

            migrationBuilder.DropTable(
                name: "ChatSpace_Channel");

            migrationBuilder.DropTable(
                name: "ChatSpace_Team");

            migrationBuilder.DropTable(
                name: "ChatSpace_ThreadResponse");

            migrationBuilder.DropTable(
                name: "ChatSpace_Message");

            migrationBuilder.DropTable(
                name: "ChatSpace_Threads");

            migrationBuilder.DropTable(
                name: "ChatSpace_Metadata");
        }
    }
}

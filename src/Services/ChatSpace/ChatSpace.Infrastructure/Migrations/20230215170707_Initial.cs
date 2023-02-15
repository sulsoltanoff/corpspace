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
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChannelsType = table.Column<int>(type: "integer", nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(76)", maxLength: 76, nullable: false),
                    Description = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Name = table.Column<string>(type: "character varying(76)", maxLength: 76, nullable: false),
                    LastPostAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModificationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeletionAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsPublic = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatSpace_Channel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatSpace_Image",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ModificationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeletionAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatSpace_Image", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drafts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChannelId = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false),
                    ModificationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeletionAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drafts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Metadatas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Embeds = table.Column<List<string>>(type: "text[]", nullable: false),
                    Emojis = table.Column<List<string>>(type: "text[]", nullable: false),
                    Reactions = table.Column<List<string>>(type: "text[]", nullable: false),
                    Priority = table.Column<string>(type: "text", nullable: false),
                    Acknowledgements = table.Column<List<string>>(type: "text[]", nullable: false),
                    ModificationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeletionAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metadatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    DisplayName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    Description = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    ModificationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DeletionAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Threads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Total = table.Column<long>(type: "bigint", nullable: false),
                    TotalUnreadThreads = table.Column<long>(type: "bigint", nullable: false),
                    TotalUnreadMentions = table.Column<long>(type: "bigint", nullable: false),
                    TotalUnreadUrgentMentions = table.Column<long>(type: "bigint", nullable: false),
                    ModificationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeletionAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Threads", x => x.Id);
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
                    Hashtags = table.Column<string>(type: "text", nullable: false),
                    FileIds = table.Column<List<string>>(type: "text[]", nullable: false),
                    HasReactions = table.Column<bool>(type: "boolean", nullable: false),
                    ReplyCount = table.Column<long>(type: "bigint", nullable: false),
                    IsFollowing = table.Column<bool>(type: "boolean", nullable: true),
                    MetadataId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModificationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeletionAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatSpace_Message", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatSpace_Message_Metadatas_MetadataId",
                        column: x => x.MetadataId,
                        principalTable: "Metadatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ThreadResponse",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageId = table.Column<Guid>(type: "uuid", maxLength: 200, nullable: false),
                    ReplyCount = table.Column<long>(type: "bigint", nullable: false),
                    LastReplyAt = table.Column<long>(type: "bigint", nullable: false),
                    LastViewedAt = table.Column<long>(type: "bigint", nullable: false),
                    ThreadsId = table.Column<Guid>(type: "uuid", nullable: false),
                    UnreadReplies = table.Column<long>(type: "bigint", nullable: false),
                    UnreadMentions = table.Column<long>(type: "bigint", nullable: false),
                    IsUrgent = table.Column<bool>(type: "boolean", nullable: false),
                    ModificationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeletionAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreadResponse", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThreadResponse_ChatSpace_Message_MessageId",
                        column: x => x.MessageId,
                        principalTable: "ChatSpace_Message",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ThreadResponse_Threads_ThreadsId",
                        column: x => x.ThreadsId,
                        principalTable: "Threads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    EmailVerified = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    FirstName = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    LastName = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Position = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Roles = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    ChannelId = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: false),
                    Props = table.Column<Dictionary<string, string>>(type: "jsonb", nullable: false),
                    NotifyProps = table.Column<Dictionary<string, string>>(type: "jsonb", nullable: false),
                    LastPictureUpdate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    FailedAttempts = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    Locale = table.Column<string>(type: "character varying(5)", maxLength: 5, nullable: false),
                    LastActivityAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsBot = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    BotDescription = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: false),
                    BotLastIconUpdate = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    ModificationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    CreationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "NOW()"),
                    DeletionAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    MessageId = table.Column<Guid>(type: "uuid", nullable: true),
                    ThreadResponseId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatUsers_ChatSpace_Message_MessageId",
                        column: x => x.MessageId,
                        principalTable: "ChatSpace_Message",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatUsers_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatUsers_ThreadResponse_ThreadResponseId",
                        column: x => x.ThreadResponseId,
                        principalTable: "ThreadResponse",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChannelMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ChannelId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChatUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModificationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    CreationAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    DeletionAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_ChatSpace_Message_MetadataId",
                table: "ChatSpace_Message",
                column: "MetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatUsers_MessageId",
                table: "ChatUsers",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatUsers_TeamId",
                table: "ChatUsers",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatUsers_ThreadResponseId",
                table: "ChatUsers",
                column: "ThreadResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadResponse_MessageId",
                table: "ThreadResponse",
                column: "MessageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ThreadResponse_ThreadsId",
                table: "ThreadResponse",
                column: "ThreadsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChannelMembers");

            migrationBuilder.DropTable(
                name: "ChatSpace_Image");

            migrationBuilder.DropTable(
                name: "Drafts");

            migrationBuilder.DropTable(
                name: "ChatSpace_Channel");

            migrationBuilder.DropTable(
                name: "ChatUsers");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "ThreadResponse");

            migrationBuilder.DropTable(
                name: "ChatSpace_Message");

            migrationBuilder.DropTable(
                name: "Threads");

            migrationBuilder.DropTable(
                name: "Metadatas");
        }
    }
}

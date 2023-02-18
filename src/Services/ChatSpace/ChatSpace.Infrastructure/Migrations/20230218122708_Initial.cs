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
                name: "AppChannels",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TeamId = table.Column<Guid>(type: "uuid", nullable: true),
                    ChannelsType = table.Column<string>(type: "text", nullable: false),
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
                    table.PrimaryKey("PK_AppChannels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppChannelType",
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
                    table.PrimaryKey("PK_AppChannelType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppTeams",
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
                    table.PrimaryKey("PK_AppTeams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatThreads",
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
                    table.PrimaryKey("PK_ChatThreads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drafts",
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
                    table.PrimaryKey("PK_Drafts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Images",
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
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Metadatas",
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
                    table.PrimaryKey("PK_Metadatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
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
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_Metadatas_MetadataId",
                        column: x => x.MetadataId,
                        principalTable: "Metadatas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatThreadResponses",
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
                    table.PrimaryKey("PK_ChatThreadResponses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatThreadResponses_ChatThreads_ThreadsId",
                        column: x => x.ThreadsId,
                        principalTable: "ChatThreads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatThreadResponses_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
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
                    UserTeamId = table.Column<Guid>(type: "uuid", nullable: true),
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
                    ChatThreadResponseId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUsers_AppTeams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "AppTeams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppUsers_AppTeams_UserTeamId",
                        column: x => x.UserTeamId,
                        principalTable: "AppTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_AppUsers_ChatThreadResponses_ChatThreadResponseId",
                        column: x => x.ChatThreadResponseId,
                        principalTable: "ChatThreadResponses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserChannel",
                columns: table => new
                {
                    AppChannel = table.Column<Guid>(type: "uuid", nullable: false),
                    AppUser = table.Column<Guid>(type: "uuid", nullable: false),
                    JoinTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChannel", x => new { x.AppChannel, x.AppUser });
                    table.ForeignKey(
                        name: "FK_UserChannel_AppChannels_AppChannel",
                        column: x => x.AppChannel,
                        principalTable: "AppChannels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChannel_AppUsers_AppUser",
                        column: x => x.AppUser,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_ChatThreadResponseId",
                table: "AppUsers",
                column: "ChatThreadResponseId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_TeamId",
                table: "AppUsers",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_UserTeamId",
                table: "AppUsers",
                column: "UserTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatThreadResponses_MessageId",
                table: "ChatThreadResponses",
                column: "MessageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ChatThreadResponses_ThreadsId",
                table: "ChatThreadResponses",
                column: "ThreadsId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ChannelId",
                table: "Messages",
                column: "ChannelId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_CreationAt",
                table: "Messages",
                column: "CreationAt");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_IsDeleted",
                table: "Messages",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_MetadataId",
                table: "Messages",
                column: "MetadataId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChannel_AppUser",
                table: "UserChannel",
                column: "AppUser");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppChannelType");

            migrationBuilder.DropTable(
                name: "Drafts");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "UserChannel");

            migrationBuilder.DropTable(
                name: "AppChannels");

            migrationBuilder.DropTable(
                name: "AppUsers");

            migrationBuilder.DropTable(
                name: "AppTeams");

            migrationBuilder.DropTable(
                name: "ChatThreadResponses");

            migrationBuilder.DropTable(
                name: "ChatThreads");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Metadatas");
        }
    }
}

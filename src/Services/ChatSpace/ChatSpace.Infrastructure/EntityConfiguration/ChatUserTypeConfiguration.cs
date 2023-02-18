#region Corpspace© Apache-2.0
// Copyright © 2023 Sultan Soltanov. All rights reserved.
// Author: Sultan Soltanov
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
#endregion

using ChatSpace.Domain.Constants;
using ChatSpace.Domain.Entities.Channels;
using ChatSpace.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Corpspace.ChatSpace.Infrastructure.EntityConfiguration;

public class ChatUserTypeConfiguration : IEntityTypeConfiguration<ChatUser>
{
    public void Configure(EntityTypeBuilder<ChatUser> builder)
    {
        // Set primary key
        builder.HasKey(chatUser => chatUser.Id);
        
        // Set properties
        builder.Property(chatUser => chatUser.Username)
            .IsRequired()
            .HasMaxLength(GeneralConstants.UserNameMaxLenght);
        
        builder.Property(chatUser => chatUser.Email)
            .IsRequired()
            .HasMaxLength(GeneralConstants.EmailMaxLenght);
        
        builder.Property(chatUser => chatUser.EmailVerified)
            .IsRequired(false)
            .HasDefaultValue(false);
        
        builder.Property(chatUser => chatUser.FirstName)
            .HasMaxLength(GeneralConstants.NameMaxLight);
        
        builder.Property(chatUser => chatUser.LastName)
            .HasMaxLength(GeneralConstants.NameMaxLight);

        builder.Property(chatUser => chatUser.Position);

        builder.Property(chatUser => chatUser.Roles);

        builder.Property(chatUser => chatUser.ChannelId)
            .IsRequired();

        builder.Property(chatUser => chatUser.Props)
            .HasColumnType("jsonb");
        
        builder.Property(chatUser => chatUser.NotifyProps)
            .HasColumnType("jsonb");
        
        builder.Property(chatUser => chatUser.LastPictureUpdate)
            .IsRequired(false);
        
        builder.Property(chatUser => chatUser.FailedAttempts)
            .HasDefaultValue(0);
        
        builder.Property(chatUser => chatUser.Locale)
            .IsRequired()
            .HasMaxLength(GeneralConstants.LocaleLenght);
        
        builder.Property(chatUser => chatUser.LastActivityAt)
            .IsRequired(false);
        
        builder.Property(chatUser => chatUser.IsBot)
            .IsRequired()
            .HasDefaultValue(false);
        
        builder.Property(chatUser => chatUser.BotDescription)
            .HasMaxLength(GeneralConstants.ChannelDescriptionMaxLenght);
        
        builder.Property(chatUser => chatUser.BotLastIconUpdate)
            .IsRequired(false);
        
        builder.Property(chatUser => chatUser.ModificationAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(chatUser => chatUser.CreationAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(chatUser => chatUser.DeletionAt)
            .IsRequired(false);
        
        builder.Property(chatUser => chatUser.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);
        
        builder.HasMany(x => x.AppChannel)
            .WithMany(x => x.ChannelMembers)
            .UsingEntity<Dictionary<string, object>>(
                "UserChannel",
                j => j.HasOne<AppChannel>().WithMany().HasForeignKey("ChannelId").OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<ChatUser>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade),
                j =>
                {
                    j.HasKey("ChannelId", "UserId");
                    j.HasData(
                        new { ChannelId = Guid.NewGuid(), UserId = Guid.NewGuid() });
                });

        builder.HasOne(x => x.Team)
            .WithMany(x => x.Members)
            .HasForeignKey(x => x.UserTeamId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}
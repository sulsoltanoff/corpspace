#region Corpspace© Apache-2.0
// Copyright © 2023 The Corpspace Technologies. All rights reserved.
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

using ChatSpace.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Corpspace.ChatSpace.Infrastructure.EntityConfiguration;

public class ChatUserTypeConfiguration : IEntityTypeConfiguration<ChatUser>
{
    public void Configure(EntityTypeBuilder<ChatUser> builder)
    {
        builder.ToTable("ChatUsers");
        
        builder.HasKey(chatUser => chatUser.Id);

        builder.Property(chatUser => chatUser.Username)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(chatUser => chatUser.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(chatUser => chatUser.EmailVerified)
            .IsRequired();

        builder.Property(chatUser => chatUser.FirstName)
            .HasMaxLength(256);

        builder.Property(chatUser => chatUser.LastName)
            .HasMaxLength(256);

        builder.Property(chatUser => chatUser.Position)
            .HasMaxLength(256);

        builder.Property(chatUser => chatUser.Roles)
            .HasMaxLength(256);

        builder.OwnsOne(chatUser => chatUser.Props);

        builder.OwnsOne(chatUser => chatUser.NotifyProps);

        builder.Property(chatUser => chatUser.LastPictureUpdate)
            .IsRequired();

        builder.Property(chatUser => chatUser.FailedAttempts)
            .IsRequired();

        builder.Property(chatUser => chatUser.Locale)
            .HasMaxLength(256);

        builder.Property(chatUser => chatUser.LastActivityAt)
            .IsRequired();

        builder.Property(chatUser => chatUser.IsBot)
            .IsRequired();

        builder.Property(chatUser => chatUser.BotDescription)
            .HasMaxLength(512);

        builder.Property(chatUser => chatUser.BotLastIconUpdate)
            .IsRequired();
        
        builder.Property(chatUser => chatUser.ModificationAt)
            .IsRequired();

        builder.Property(chatUser => chatUser.CreationAt)
            .IsRequired();

        builder.Property(chatUser => chatUser.DeletionAt);

        builder.Property(chatUser => chatUser.IsDeleted)
            .IsRequired();
    }
}
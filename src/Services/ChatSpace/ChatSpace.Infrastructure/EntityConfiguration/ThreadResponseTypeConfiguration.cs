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

using ChatSpace.Domain.Entities.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Corpspace.ChatSpace.Infrastructure.EntityConfiguration;

public class ThreadResponseTypeConfiguration : IEntityTypeConfiguration<ThreadResponse>
{
    public void Configure(EntityTypeBuilder<ThreadResponse> builder)
    {
        // Set primary key
        builder.HasKey(x => x.Id);

        // Set properties
        builder.Property(x => x.MessageId)
            .IsRequired();

        builder.Property(x => x.ReplyCount)
            .IsRequired();

        builder.Property(x => x.LastReplyAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(x => x.LastViewedAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.HasOne(x => x.AppThreads)
            .WithMany(x => x.ThreadResponses)
            .HasForeignKey(x => x.ThreadsId);

        builder.HasOne(x => x.Message)
            .WithOne()
            .HasForeignKey<ThreadResponse>(x => x.MessageId);

        builder.Property(x => x.UnreadReplies)
            .IsRequired();

        builder.Property(x => x.UnreadMentions)
            .IsRequired();

        builder.Property(x => x.IsUrgent)
            .IsRequired();

        builder.Property(x => x.ModificationAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(x => x.CreationAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(x => x.DeletionAt)
            .IsRequired(false);

        builder.Property(x => x.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasMany(x => x.Participants)
            .WithOne();
    }
}
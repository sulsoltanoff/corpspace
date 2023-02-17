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

public class ThreadsTypeConfiguration : IEntityTypeConfiguration<Threads>
{
    public void Configure(EntityTypeBuilder<Threads> builder)
    {
        // Set primary key
        builder.HasKey(x => x.Id);
    
        // Set properties
        builder.Property(x => x.Total)
            .IsRequired();

        builder.Property(x => x.TotalUnreadThreads)
            .IsRequired();

        builder.Property(x => x.TotalUnreadMentions)
            .IsRequired();

        builder.Property(x => x.TotalUnreadUrgentMentions)
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

        builder.HasMany(x => x.ThreadResponses)
            .WithOne(x => x.AppThreads)
            .HasForeignKey(x => x.ThreadsId);
    }
}
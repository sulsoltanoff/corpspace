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
using ChatSpace.Domain.Entities.Messages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Corpspace.ChatSpace.Infrastructure.EntityConfiguration;

public class DraftTypeConfiguration : IEntityTypeConfiguration<Draft>
{
    public void Configure(EntityTypeBuilder<Draft> builder)
    {
        // Set primary key
        builder.HasKey(draft => draft.Id);

        // Set properties
        builder.Property(draft => draft.UserId)
            .IsRequired();

        builder.Property(draft => draft.ChannelId)
            .IsRequired();

        builder.Property(draft => draft.Content)
            .IsRequired()
            .HasMaxLength(GeneralConstants.MaxMessageLength);

        builder.Property(draft => draft.ModificationAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");;

        builder.Property(draft => draft.CreationAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");;

        builder.Property(draft => draft.DeletionAt)
            .IsRequired(false);

        builder.Property(draft => draft.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);
    }
}
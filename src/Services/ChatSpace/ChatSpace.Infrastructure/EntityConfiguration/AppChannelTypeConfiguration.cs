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
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Corpspace.ChatSpace.Infrastructure.EntityConfiguration;

public class AppChannelTypeConfiguration : IEntityTypeConfiguration<AppChannelType>
{
    public void Configure(EntityTypeBuilder<AppChannelType> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Value)
            .IsRequired()
            .HasMaxLength(GeneralConstants.ChannelTypeMaxLenght);

        builder.Property(e => e.CreationAt)
            .IsRequired();

        builder.Property(e => e.ModificationAt)
            .IsRequired(false);

        builder.Property(e => e.DeletionAt)
            .IsRequired(false);

        builder.Property(e => e.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasData(AppChannelType.All);

        builder.HasIndex(e => e.Value)
            .IsUnique();
    }
}
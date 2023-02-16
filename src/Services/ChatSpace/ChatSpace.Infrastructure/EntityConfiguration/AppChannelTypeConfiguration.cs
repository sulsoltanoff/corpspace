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

public class AppChannelTypeConfiguration : IEntityTypeConfiguration<AppChannel>
{
    public void Configure(EntityTypeBuilder<AppChannel> builder)
    {
        builder.HasKey(appChannel => appChannel.Id);

        builder.Property(appChannel => appChannel.TeamId)
            .IsRequired();

        builder.Property(appChannel => appChannel.ChannelsType)
            .IsRequired();

        builder.Property(appChannel => appChannel.DisplayName)
            .IsRequired()
            .HasMaxLength(GeneralConstants.ChannelNameMaxLenght);

        builder.Property(appChannel => appChannel.Description)
            .HasMaxLength(GeneralConstants.ChannelDescriptionMaxLenght);

        builder.Property(appChannel => appChannel.Name)
            .IsRequired()
            .HasMaxLength(GeneralConstants.ChannelNameMaxLenght);

        builder.Property(appChannel => appChannel.LastPostAt)
            .IsRequired();

        builder.Property(appChannel => appChannel.CreatorId)
            .IsRequired();

        builder.HasMany(appChannel => appChannel.ChannelMembers)
            .WithOne(member => member.AppChannel)
            .HasForeignKey(cm => cm.ChannelId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(appChannel => appChannel.ModificationAt)
            .IsRequired();

        builder.Property(appChannel => appChannel.CreationAt)
            .IsRequired();

        builder.Property(appChannel => appChannel.DeletionAt)
            .IsRequired(false);

        builder.Property(appChannel => appChannel.IsDeleted)
            .IsRequired();
    }
}
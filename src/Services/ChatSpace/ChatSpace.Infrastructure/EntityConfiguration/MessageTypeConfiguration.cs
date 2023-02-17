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

using System.Text.Json;
using ChatSpace.Domain.Constants;
using ChatSpace.Domain.Entities.Messages;
using ChatSpace.Domain.Entities.User;
using ChatSpace.Domain.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;

namespace Corpspace.ChatSpace.Infrastructure.EntityConfiguration;

public class MessageTypeConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        // Set primary key
        builder.HasKey(m => m.Id);
        
        // Set properties
        builder.Property(m => m.Id);
        
        builder.Property(m => m.UserId)
            .IsRequired();
        
        builder.Property(m => m.ChannelId)
            .IsRequired();
        
        builder.Property(m => m.Content)
            .HasMaxLength(GeneralConstants.MaxMessageLength);
        
        builder.Property(m => m.Type)
            .IsRequired();
        
        builder.Property(m => m.CreationAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        builder.Property(m => m.ModificationAt)
            .IsRequired()
            .HasDefaultValueSql("CURRENT_TIMESTAMP");
        
        builder.Property(x => x.DeletionAt)
            .IsRequired(false);
        
        builder.Property(m => m.IsDeleted)
            .IsRequired()
            .HasDefaultValue(false);
        
        builder.Property(m => m.IsRead)
            .IsRequired()
            .HasDefaultValue(false);
        
        builder.Property(m => m.Participants)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<ChatUser>>(v, new JsonSerializerSettings { EqualityComparer = EqualityComparer<ChatUser>.Default })!)
            .Metadata
            .SetValueComparer(new ListValueComparer<ChatUser>());


        builder.Property(m => m.FileIds)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<string>>(v)!)
            .Metadata
            .SetValueComparer(new ListValueComparer<string>());
        
        builder.Property(m => m.Props)
            .HasConversion(
                v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<Dictionary<string, object>>(v)!)
            .Metadata
            .SetValueComparer(new DictionaryValueComparer<string, object>());
        
        builder.Property(m => m.Hashtags).HasConversion(
            v => string.Join(',', v),
            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList(), new ValueComparer<List<string>>(
                (c1, c2) => c1.SequenceEqual(c2),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToList()));
        
        builder.HasIndex(m => m.ChannelId);

        builder.HasIndex(m => m.CreationAt);
        
        builder.HasIndex(m => m.IsDeleted);
    }
}
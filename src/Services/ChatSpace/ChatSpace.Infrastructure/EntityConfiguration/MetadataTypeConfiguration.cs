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

public class MetadataTypeConfiguration : IEntityTypeConfiguration<Metadata>
{
    public void Configure(EntityTypeBuilder<Metadata> builder)
    {
        builder.ToTable("Metadatas");

        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Embeds)
            .HasColumnName("Embeds")
            .IsRequired();
        
        builder.Property(x => x.Emojis)
            .HasColumnName("Emojis")
            .IsRequired();

        // builder.Property(x => x.Images)
        //     .HasColumnName("Images")
        //     .IsRequired();
        
        builder.Property(x => x.Reactions)
            .HasColumnName("Reactions")
            .IsRequired();
        
        builder.Property(x => x.Priority)
            .HasColumnName("Priority")
            .IsRequired();
        
        builder.Property(x => x.Acknowledgements)
            .HasColumnName("Acknowledgements")
            .IsRequired();
        
        builder.Property(x => x.ModificationAt)
            .HasColumnName("ModificationAt")
            .IsRequired();
        
        builder.Property(x => x.CreationAt)
            .HasColumnName("CreationAt")
            .IsRequired();
        
        builder.Property(x => x.DeletionAt)
            .HasColumnName("DeletionAt");
        
        builder.Property(x => x.IsDeleted)
            .HasColumnName("IsDeleted")
            .IsRequired();
    }
}
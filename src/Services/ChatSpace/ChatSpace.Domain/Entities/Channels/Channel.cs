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

using System.ComponentModel.DataAnnotations;
using ChatSpace.Domain.Constants;
using Corpspace.Commons.Domain.Entities;
using Corpspace.Commons.Domain.Entities.Auditing;

namespace ChatSpace.Domain.Entities.Channels;

public class Channel : Entity<Guid>, IHasModificationTime
{
    [Required]
    public Guid TeamId { get; set; }
    
    [Required]
    public ChannelsType ChannelsType { get; set; }
    
    [Required]
    public string DisplayName { get; set; }
    
    public string Description { get; set; }
    
    [Required]
    [StringLength(GeneralConstants.ChannelNameMaxLenght)]
    public string Name { get; set; }
    
    public DateTime LastPostAt { get; set; }

    [Required]
    public Guid CreatorId { get; set; }

    public DateTime ModificationTime { get; set; }
    
    [Required]
    public DateTime CreationTime { get; set; }
    
    public DateTime? DeletionTime { get; set; }
    
    public bool IsDeleted { get; set; }
}
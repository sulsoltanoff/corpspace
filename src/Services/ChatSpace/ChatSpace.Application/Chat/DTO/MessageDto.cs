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

using ChatSpace.Domain.Entities.Messages;
using Corpspace.Commons.Applications.Services.DTO;

namespace ChatSpace.Application.Chat.DTO;

public class MessageDto : BaseDto<Guid>
{
    public Guid UserId { get; set; }
    
    public Guid ChannelId { get; set; }
    
    public Guid OriginalId { get; set; }
    
    public bool IsPinned { get; set; }
    
    public string Content { get; set; }
    
    public string Type { get; set; }
    
    public Dictionary<string, object> Props { get; set; }
    
    public string Hashtags { get; set; }
    
    public List<string> FileIds { get; set; }
    
    public bool HasReactions { get; set; }
    
    public long ReplyCount { get; set; }
    
    public List<UserDto> Participants { get; set; }
    
    public bool? IsFollowing { get; set; }
    
    public Metadata Metadata { get; set; }
    
    public DateTime ModificationAt { get; set; }
    
    public DateTime CreationAt { get; set; }
    
    public DateTime? DeletionAt { get; set; }
    
    public bool IsDeleted { get; set; }
}
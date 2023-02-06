#region Corpspace© Apache-2.0
// Copyright 2023 The Corpspace Technologies
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

using ChatSpace.Domain.Entities.SeedWork;
using ChatSpace.Domain.Entities.User;

namespace ChatSpace.Domain.Entities.Messages;

public class ThreadResponse : EntityBase<Guid>
{
    public string MessageId { get; set; }
    
    public long ReplyCount { get; set; }
    
    public long LastReplyAt { get; set; }
    
    public long LastViewedAt { get; set; }
    
    public List<ChatUser> Participants { get; set; }
    
    public MessagePost MessagePost { get; set; }
    
    public long UnreadReplies { get; set; }
    
    public long UnreadMentions { get; set; }
    
    public bool IsUrgent { get; set; }
    
    public long DeleteAt { get; set; }
}
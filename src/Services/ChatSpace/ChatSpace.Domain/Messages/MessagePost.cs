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

using ChatSpace.Domain.SeedWork;
using ChatSpace.Domain.User;

namespace ChatSpace.Domain.Messages;

public class MessagePost : EntityBase
{
    public long EditAt { get; set; }
    public long DeleteAt { get; set; }
    public bool IsPinned { get; set; }
    public string UserId { get; set; }
    public string ChannelId { get; set; }
    public string RootId { get; set; }
    public string OriginalId { get; set; }

    public string Message { get; set; }
    public string MessageSource { get; set; }

    public string Type { get; set; }
    public Dictionary<string, object> Props { get; set; }
    public string Hashtags { get; set; }
    public List<string> FileIds { get; set; }
    public string PendingPostId { get; set; }
    public bool HasReactions { get; set; }
    public string RemoteId { get; set; }

    public long ReplyCount { get; set; }
    public long LastReplyAt { get; set; }
    public List<ChatUser> Participants { get; set; }
    public bool? IsFollowing { get; set; }
    public MessageMetadata Metadata { get; set; }
}

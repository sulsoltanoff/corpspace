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

using System.ComponentModel.DataAnnotations.Schema;
using ChatSpace.Domain.Constants;
using ChatSpace.Domain.Entities.Channels;
using Corpspace.Commons.Domain.Entities;
using Corpspace.Commons.Domain.Entities.Auditing;

namespace ChatSpace.Domain.Entities.User;

[Table($"{GeneralConstants.ServiceName}_User")]
public class ChatUser : Entity<Guid>, IHasModificationTime
{
    public string Username { get; set; }
    
    public string Email { get; set; }
    
    public bool? EmailVerified { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Position { get; set; }
    
    public string Roles { get; set; }
    
    public Guid? ChannelId { get; set; }
    
    public IEnumerable<AppChannel>? AppChannel { get; set; }
    
    public Guid? UserTeamId { get; set; }
    
    public Team.Team? Team { get; set; }

    public Dictionary<string, string> Props { get; set; }
    
    public Dictionary<string, string> NotifyProps { get; set; }
    
    public DateTime? LastPictureUpdate { get; set; }
    
    public int FailedAttempts { get; set; }
    
    public string Locale { get; set; }
    
    public DateTime? LastActivityAt { get; set; }
    
    public bool IsBot { get; set; }
    
    public string BotDescription { get; set; }
    
    public DateTime? BotLastIconUpdate { get; set; }

    public DateTime? ModificationAt { get; set; }
    
    public DateTime? CreationAt { get; set; }
    
    public DateTime? DeletionAt { get; set; }
    
    public bool IsDeleted { get; set; }
}
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

using System.ComponentModel.DataAnnotations.Schema;
using ChatSpace.Domain.Constants;
using Corpspace.Commons.Domain.Entities;
using Corpspace.Commons.Domain.Entities.Auditing;

namespace ChatSpace.Domain.Entities.User;

[Table($"{GeneralConstants.ServiceName}_User")]
public class ChatUser : Entity<Guid>, IHasModificationTime
{
    public ChatUser(string username, string email, bool emailVerified, string firstName, 
        string lastName, string position, string roles, Dictionary<string, string> props, Dictionary<string, string> 
            notifyProps, DateTime lastPictureUpdate, int failedAttempts, string locale, DateTime lastActivityAt, 
        bool isBot, string botDescription, long botLastIconUpdate, DateTime modificationAt, DateTime creationAt, 
        DateTime? deletionAt, bool isDeleted)
    {
        Username = username;
        Email = email;
        EmailVerified = emailVerified;
        FirstName = firstName;
        LastName = lastName;
        Position = position;
        Roles = roles;
        Props = props;
        NotifyProps = notifyProps;
        LastPictureUpdate = lastPictureUpdate;
        FailedAttempts = failedAttempts;
        Locale = locale;
        LastActivityAt = lastActivityAt;
        IsBot = isBot;
        BotDescription = botDescription;
        BotLastIconUpdate = botLastIconUpdate;
        ModificationAt = modificationAt;
        CreationAt = creationAt;
        DeletionAt = deletionAt;
        IsDeleted = isDeleted;
    }

    public string Username { get; set; }
    
    public string Email { get; set; }
    
    public bool EmailVerified { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Position { get; set; }
    
    public string Roles { get; set; }

    public Dictionary<string, string> Props { get; set; }
    
    public Dictionary<string, string> NotifyProps { get; set; }
    
    public DateTime LastPictureUpdate { get; set; }
    
    public int FailedAttempts { get; set; }
    
    public string Locale { get; set; }
    
    public DateTime LastActivityAt { get; set; }
    
    public bool IsBot { get; set; }
    
    public string BotDescription { get; set; }
    
    public long BotLastIconUpdate { get; set; }

    public DateTime ModificationAt { get; set; }
    
    public DateTime CreationAt { get; set; }
    
    public DateTime? DeletionAt { get; set; }
    
    public bool IsDeleted { get; set; }
}
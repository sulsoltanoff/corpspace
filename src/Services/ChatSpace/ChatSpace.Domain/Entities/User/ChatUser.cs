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

namespace ChatSpace.Domain.Entities.User;

public class ChatUser : EntityBase<Guid>
{
    public string Username { get; set; }
    
    public string Password { get; set; }
    
    public string AuthData { get; set; }
    
    public string AuthService { get; set; }
    
    public string Email { get; set; }
    
    public bool EmailVerified { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Position { get; set; }
    
    public string Roles { get; set; }
    
    public DateTime DeleteAt { get; set; }
    
    public Dictionary<string, string> Props { get; set; }
    
    public Dictionary<string, string> NotifyProps { get; set; }
    
    public DateTime LastPictureUpdate { get; set; }
    
    public int FailedAttempts { get; set; }
    
    public string Locale { get; set; }
    
    public Dictionary<string, string> Timezone { get; set; }
    
    public DateTime LastActivityAt { get; set; }
    
    public bool IsBot { get; set; }
    
    public string BotDescription { get; set; }
    
    public long BotLastIconUpdate { get; set; }
    
    
}
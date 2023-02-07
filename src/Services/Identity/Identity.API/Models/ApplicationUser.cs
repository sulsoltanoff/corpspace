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

namespace Corpspace.Services.Identity.API.Models;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [Required]
    public string Name { get; set; }
    
    [Required]
    public string LastName { get; set; }
    
    public string FullName { get; set; }

    public DateTime JoinedDateTime { get; set; }
    
    [MaxLength(120)]
    public string ApiKey { get; set; }

    public bool IsActive { get; set; }
   
    public bool IsAdmin { get; set; }
     
    public bool IsSuperAdmin { get; set; }
    
    public bool IsBot { get; set; }
    
    public ApplicationUser BotOwner { get; set; }
    
    public IdentityRole Role { get; set; }
    
    public TimeZoneInfo TimeZone { get; set; }
    
    public string AvatarSource { get; set; }
    
    public string AvatarUrl { get; set; }
    
    public string AvatarHash { get; set; }
    
    
    [Required] 
    public string Country { get; set; }
}
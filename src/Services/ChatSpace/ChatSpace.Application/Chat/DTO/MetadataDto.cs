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

using Corpspace.Commons.Applications.Services.DTO;

namespace ChatSpace.Application.Chat.DTO;

public class MetadataDto : BaseDto<Guid>
{
    public List<string> Embeds { get; set; }
    
    public List<string> Emojis { get; set; }
    
    public Dictionary<string, ImageDto> Images { get; set; }
    
    public List<string> Reactions { get; set; }
    
    public string Priority { get; set; }
    
    public List<string> Acknowledgements { get; set; }
    
    public DateTime ModificationAt { get; set; }
    
    public DateTime CreationAt { get; set; }
    
    public DateTime? DeletionAt { get; set; }
    
    public bool IsDeleted { get; set; }
}
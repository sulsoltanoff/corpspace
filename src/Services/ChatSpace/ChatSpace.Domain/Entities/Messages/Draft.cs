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

using Corpspace.Commons.Domain.Entities;
using Corpspace.Commons.Domain.Entities.Auditing;

namespace ChatSpace.Domain.Entities.Messages;

public class Draft : Entity<Guid>, IHasModificationTime
{
    public Guid UserId { get; set; }
    
    public Guid ChannelId { get; set; }
    
    public string Content { get; set; }
    
    public DateTime ModificationTime { get; set; }
    
    public DateTime CreationTime { get; set; }
    
    public DateTime? DeletionTime { get; set; }
    
    public bool IsDeleted { get; set; }
}
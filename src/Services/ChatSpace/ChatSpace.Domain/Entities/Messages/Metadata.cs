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

namespace ChatSpace.Domain.Entities.Messages;

[Table($"{GeneralConstants.ServiceName}_Metadata")]
public class Metadata : Entity<Guid>, IHasModificationTime
{
    public Metadata(List<string> embeds, List<string> emojis, List<FileInfo> files, Dictionary<string, Image> images, 
        List<string> reactions, string priority, List<string> acknowledgements, DateTime modificationAt, 
        DateTime creationAt, DateTime? deletionAt, bool isDeleted)
    {
        Embeds = embeds;
        Emojis = emojis;
        Files = files;
        Images = images;
        Reactions = reactions;
        Priority = priority;
        Acknowledgements = acknowledgements;
        ModificationAt = modificationAt;
        CreationAt = creationAt;
        DeletionAt = deletionAt;
        IsDeleted = isDeleted;
    }

    public List<string> Embeds { get; set; }
    
    public List<string> Emojis { get; set; }
    
    public List<FileInfo> Files { get; set; }
    
    public Dictionary<string, Image> Images { get; set; }
    
    public List<string> Reactions { get; set; }
    
    public string Priority { get; set; }
    
    public List<string> Acknowledgements { get; set; }
    
    public DateTime ModificationAt { get; set; }
    
    public DateTime CreationAt { get; set; }
    
    public DateTime? DeletionAt { get; set; }
    
    public bool IsDeleted { get; set; }
}
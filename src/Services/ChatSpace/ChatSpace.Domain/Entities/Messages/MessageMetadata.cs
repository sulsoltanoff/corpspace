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

namespace ChatSpace.Domain.Entities.Messages;

public class MessageMetadata : EntityBase<Guid>
{
    public List<string> Embeds { get; set; }
    public List<string> Emojis { get; set; }
    public List<FileInfo> Files { get; set; }
    public Dictionary<string, MessageImage> Images { get; set; }
    public List<string> Reactions { get; set; }
    public string Priority { get; set; }
    public List<string> Acknowledgements { get; set; }
}
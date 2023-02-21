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
using Corpspace.Commons.Domain.Entities;
using Corpspace.Commons.Domain.Entities.Auditing;

namespace ChatSpace.Domain.Entities.Channels;

public class AppChannelType : Entity<Guid>, IHasModificationTime
{
    private AppChannelType(string value) => Value = value;

    public string Value { get; }

    public static AppChannelType Open { get; } = new("Open");
    public static AppChannelType Private { get; } = new("Private");
    public static AppChannelType Group { get; } = new("Group");
    public static AppChannelType OneToOne { get; } = new("OneToOne");

    public static readonly IReadOnlyCollection<AppChannelType> All = new[]
    {
        Open,
        Private,
        Group,
        OneToOne
    };

    public static AppChannelType FromString(string value)
    {
        return All.FirstOrDefault(x => string.Equals(x.Value, value, StringComparison.OrdinalIgnoreCase))
               ?? throw new InvalidOperationException(
                   $"Possible values: {string.Join(", ", All.Select(x => x.Value))}");
    }

    public static implicit operator string(AppChannelType type) => type.Value;

    public static implicit operator AppChannelType(string value) => FromString(value);

    public DateTime? ModificationAt { get; set; }

    public DateTime? CreationAt { get; set; }

    public DateTime? DeletionAt { get; set; }

    public bool IsDeleted { get; set; }
}


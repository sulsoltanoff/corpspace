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

namespace ChatSpace.Domain.Entities;

/// <summary>
/// Defines interface for base entity type. All entities in the system must implement this interface.
/// </summary>
/// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
public interface IEntity<TPrimaryKey>
{
    /// <summary>
    /// Unique identifier for this entity.
    /// </summary>
    TPrimaryKey Id { get; set; }

    /// <summary>
    /// Checks if this entity is transient (not persisted to database and it has not an <see cref="Id"/>).
    /// </summary>
    /// <returns>True, if this entity is transient</returns>
    bool IsTransient();
}
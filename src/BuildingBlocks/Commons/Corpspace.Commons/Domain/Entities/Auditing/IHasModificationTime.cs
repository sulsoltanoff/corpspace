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

namespace Corpspace.Commons.Domain.Entities.Auditing;

/// <summary>
/// This interface can be implemented by an entity to store its modification time.
/// The creation time is set automatically when the entity is saved to a database and can be accessed through.
/// </summary>
public interface IHasModificationTime
{
    /// <summary>
    /// The date and time that the object was last modified.
    /// </summary>
    DateTime? ModificationAt { get; set; }

    /// <summary>
    /// The date and time that the object was created.
    /// </summary>
    DateTime? CreationAt { get; set; }

    /// <summary>
    /// The date and time that the object was deleted.
    /// </summary>
    DateTime? DeletionAt { get; set; }

    /// <summary>
    /// Indicates whether the object has been deleted.
    /// </summary>
    bool IsDeleted { get; set; }
}
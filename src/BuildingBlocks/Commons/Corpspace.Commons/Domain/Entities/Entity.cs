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

using System.Reflection;

namespace Corpspace.Commons.Domain.Entities;

/// <summary>
/// A shortcut of <see cref="Entity{TPrimaryKey}"/> for most used primary key type (<see cref="long"/>).
/// </summary>
[Serializable]
public abstract class Entity : Entity<long>, IEntity
{

}

/// <summary>
/// Basic implementation of IEntity interface.
/// An entity can inherit this class of directly implement to IEntity interface.
/// </summary>
/// <typeparam name="TPrimaryKey">Type of the primary key of the entity</typeparam>
[Serializable]
public abstract class Entity<TPrimaryKey> : IEntity<TPrimaryKey>
{
    /// <summary>
    /// Unique identifier for this entity.
    /// </summary>
    public virtual TPrimaryKey Id { get; set; }

    /// <summary>
    /// Checks if this entity is transient (it has not an Id).
    /// </summary>
    /// <returns>True, if this entity is transient</returns>
    public virtual bool IsTransient()
    {
        if (EqualityComparer<TPrimaryKey>.Default.Equals(Id, default(TPrimaryKey)))
        {
            return true;
        }

        //Workaround for EF Core since it sets int/long to min value when attaching to dbcontext
        if (typeof(TPrimaryKey) == typeof(int))
        {
            return Convert.ToInt32(Id) <= 0;
        }

        if (typeof(TPrimaryKey) == typeof(long))
        {
            return Convert.ToInt64(Id) <= 0;
        }

        return false;
    }

    /// <inheritdoc/>
    public virtual bool EntityEquals(object obj)
    {
        if (obj == null || !(obj is Entity<TPrimaryKey>))
        {
            return false;
        }

        //Same instances must be considered as equal
        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        //Transient objects are not considered as equal
        var other = (Entity<TPrimaryKey>)obj;
        if (IsTransient() && other.IsTransient())
        {
            return false;
        }

        //Must have a IS-A relation of types or must be same type
        var typeOfThis = GetType();
        var typeOfOther = other.GetType();
        if (!typeOfThis.GetTypeInfo().IsAssignableFrom(typeOfOther) && !typeOfOther.GetTypeInfo().IsAssignableFrom(typeOfThis))
        {
            return false;
        }
        
        return Id.Equals(other.Id);
    }
 
    public override string ToString()
    {
        return $"[{GetType().Name} {Id}]";
    }
}
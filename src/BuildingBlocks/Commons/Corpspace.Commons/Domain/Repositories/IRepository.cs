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

using System.Linq.Expressions;
using Corpspace.Commons.Domain.Entities;

namespace Corpspace.Commons.Domain.Repositories;

/// <summary>
/// This interface must be implemented by all repositories to identify them by convention.
/// Implement generic version instead of this one.
/// </summary>
public interface IRepository
{
}

/// <summary>
/// Shortcut for the most used primary key type <see cref="long"/> .
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public interface IRepository<TEntity> : IRepository<TEntity, long> where TEntity : class, IEntity<long>
{
}

/// <summary>
/// This interface must be implemented by all repositories to identify them by convention.
/// </summary>
/// <typeparam name="TEntity">Main Entity type this repository works on </typeparam>
/// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
public interface IRepository<TEntity, in TPrimaryKey> : IRepository where TEntity : class, IEntity<TPrimaryKey>
{
    #region Get operations
    IQueryable<TEntity> GetAll();
    
    List<TEntity> GetAllList();
    
    Task<List<TEntity>> GetAllListAsync();
    
    TEntity Get(TPrimaryKey id);
    
    Task<TEntity> GetAsync(TPrimaryKey id);
    #endregion
    
    
    #region Insert operations
    TEntity Insert(TEntity entity);
    
    Task<TEntity> InsertAsync(TEntity entity);
    #endregion
    
    
    #region Update operations
    TEntity Update(TEntity entity);
    
    Task<TEntity> UpdateAsync(TEntity entity);
    #endregion
    
    
    #region Delete operations
    void Delete(TEntity entity);
    
    Task DeleteAsync(TEntity entity);
    #endregion


    #region Exists operations
    bool Exists(TPrimaryKey id);
    
    Task<bool> ExistsAsync(TPrimaryKey id);
    #endregion


    #region Find operations
    TEntity Find(Expression<Func<TEntity, bool>> predicate);
    
    Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> predicate);
    #endregion
}
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

using System.Linq.Expressions;
using ChatSpace.Domain.Entities.Channels;
using Corpspace.ChatSpace.Infrastructure;
using Corpspace.Commons.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChatSpace.Application.Channel.Repository;

/// <summary>
/// Represents a repository for managing <see cref="AppChannel"/> entities.
/// </summary>
public class AppChannelRepository : IRepository<AppChannel, Guid>
{
    private readonly ChatAppContext _dbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppChannelRepository"/> class with the specified database context.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    public AppChannelRepository(ChatAppContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Gets all <see cref="AppChannel"/> entities.
    /// </summary>
    /// <returns>An <see cref="IQueryable{T}"/> of <see cref="AppChannel"/>.</returns>
    public IQueryable<AppChannel> GetAll()
    {
        return _dbContext.AppChannels;
    }

    /// <summary>
    /// Gets all <see cref="AppChannel"/> entities as a list.
    /// </summary>
    /// <returns>A <see cref="List{T}"/> of <see cref="AppChannel"/>.</returns>
    public List<AppChannel> GetAllList()
    {
        return GetAll().ToList();
    }

    /// <summary>
    /// Gets all <see cref="AppChannel"/> entities as a list asynchronously.
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> of <see cref="List{T}"/> of <see cref="AppChannel"/>.</returns>
    public async Task<List<AppChannel>> GetAllListAsync()
    {
        return await GetAll().ToListAsync();
    }

    /// <summary>
    /// Gets a list of <see cref="AppChannel"/> entities that satisfy the specified predicate asynchronously.
    /// </summary>
    /// <param name="predicate">The predicate to filter the <see cref="AppChannel"/> entities.</param>
    /// <returns>A <see cref="Task{TResult}"/> of <see cref="List{T}"/> of <see cref="AppChannel"/>.</returns>
    public async Task<List<AppChannel>> GetListAsync(Expression<Func<AppChannel, bool>> predicate)
    {
        return await GetAll().Where(predicate).ToListAsync();
    }

    /// <summary>
    /// Gets the <see cref="AppChannel"/> entity with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the <see cref="AppChannel"/> entity to retrieve.</param>
    /// <returns>The <see cref="AppChannel"/> entity.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no <see cref="AppChannel"/> entity with the specified ID is found.</exception>
    public AppChannel Get(Guid id)
    {
        return _dbContext.AppChannels.FirstOrDefault(x => x.Id == id) ?? throw new InvalidOperationException();
    }

    /// <summary>
    /// Gets the <see cref="AppChannel"/> entity with the specified ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the <see cref="AppChannel"/> entity to retrieve.</param>
    /// <returns>A <see cref="Task{TResult}"/> of <see cref="AppChannel"/>.</returns>
    /// <exception cref="InvalidOperationException">Thrown when no <see cref="AppChannel"/> entity with the specified ID is found.</exception>
    public async Task<AppChannel> GetAsync(Guid id)
    {
        return await _dbContext.AppChannels.FirstOrDefaultAsync(x => x.Id == id) ?? throw new InvalidOperationException();
    }


    /// <summary>
    /// Returns the first element of a sequence that satisfies a specified condition or a default value if no such element is found.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <returns>The first element in the sequence that passes the test in the specified predicate function or a default value if no element satisfies the condition.</returns>
    public async Task<AppChannel> FirstOrDefaultAsync(Expression<Func<AppChannel, bool>> predicate)
    {
        return await _dbContext.Set<AppChannel>().FirstOrDefaultAsync(predicate) ?? throw new InvalidOperationException();
    }

    /// <summary>
    /// Adds the specified entity to the context and saves changes asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add to the context.</param>
    /// <returns>The entity added to the context.</returns>
    public async Task<AppChannel> CreateAsync(AppChannel entity)
    {
        var result = await _dbContext.Set<AppChannel>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return result.Entity;
    }

    /// <summary>
    /// Adds the specified entity to the context and saves changes.
    /// </summary>
    /// <param name="entity">The entity to add to the context.</param>
    /// <returns>The entity added to the context.</returns>
    public AppChannel Create(AppChannel entity)
    {
        var result = _dbContext.Set<AppChannel>().Add(entity);
        _dbContext.SaveChanges();
        return result.Entity;
    }

    /// <summary>
    /// Adds the specified entity to the context and saves changes.
    /// </summary>
    /// <param name="entity">The entity to add to the context.</param>
    /// <returns>The entity added to the context.</returns>
    public AppChannel Insert(AppChannel entity)
    {
        _dbContext.Set<AppChannel>().Add(entity);
        _dbContext.SaveChanges();
        return entity;
    }

    /// <summary>
    /// Adds the specified entity to the context and saves changes asynchronously.
    /// </summary>
    /// <param name="entity">The entity to add to the context.</param>
    /// <returns>The entity added to the context.</returns>
    public async Task<AppChannel> InsertAsync(AppChannel entity)
    {
        _dbContext.Set<AppChannel>().Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Updates the specified entity in the context and saves changes.
    /// </summary>
    /// <param name="entity">The entity to update in the context.</param>
    /// <returns>The updated entity.</returns>
    public AppChannel Update(AppChannel entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        _dbContext.SaveChanges();
        return entity;
    }

    /// <summary>
    /// Updates the specified entity in the context and saves changes asynchronously.
    /// </summary>
    /// <param name="entity">The entity to update in the context.</param>
    /// <returns>The updated entity.</returns>
    public async Task<AppChannel> UpdateAsync(AppChannel entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    
    /// <summary>
    /// Removes the specified entity from the context and saves changes.
    /// </summary>
    /// <param name="entity">The entity to remove from the context.</param>
    public void Delete(AppChannel entity)
    {
        _dbContext.Set<AppChannel>().Remove(entity);
        _dbContext.SaveChanges();
    }

    /// <summary>
    /// Removes the entity with the specified identifier from the context and saves changes.
    /// </summary>
    /// <param name="id">The identifier of the entity to remove from the context.</param>
    public void Delete(Guid id)
    {
        var entity = Get(id);
        Delete(entity);
    }

    /// <summary>
    /// Deletes the entity with the given ID from the database.
    /// </summary>
    /// <param name="id">The ID of the entity to delete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetAsync(id);
        Delete(entity);
    }

    /// <summary>
    /// Deletes the given entity from the database.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public async Task DeleteAsync(AppChannel entity)
    {
        _dbContext.Set<AppChannel>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Determines whether an entity with the given ID exists in the database.
    /// </summary>
    /// <param name="id">The ID of the entity to check for.</param>
    /// <returns>True if an entity with the given ID exists in the database; otherwise, false.</returns>
    public bool Exists(Guid id)
    {
        return _dbContext.Set<AppChannel>().Any(e => e.Id == id);
    }

    /// <summary>
    /// Determines whether an entity with the given ID exists in the database.
    /// </summary>
    /// <param name="id">The ID of the entity to check for.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result contains true if an entity with the given ID exists in the database; otherwise, false.</returns>
    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbContext.Set<AppChannel>().AnyAsync(e => e.Id == id);
    }

    /// <summary>
    /// Finds the first entity in the database that matches the given predicate.
    /// </summary>
    /// <param name="predicate">The predicate to match against the entities in the database.</param>
    /// <returns>The first entity in the database that matches the given predicate, or null if no matching entity is found.</returns>
    public AppChannel Find(Expression<Func<AppChannel, bool>> predicate)
    {
        return _dbContext.Set<AppChannel>().FirstOrDefault(predicate) ?? throw new InvalidOperationException();
    }

    
    /// <summary>
    /// Finds the first entity in the database that matches the given predicate.
    /// </summary>
    /// <param name="predicate">The predicate to match against the entities in the database.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the asynchronous operation. The task result contains the first entity in the database that matches the given predicate, or null if no matching entity is found.</returns>
    public async Task<AppChannel> FindAsync(Expression<Func<AppChannel, bool>> predicate)
    {
        return await _dbContext.Set<AppChannel>().FirstOrDefaultAsync(predicate) ?? throw new InvalidOperationException();
    }
}
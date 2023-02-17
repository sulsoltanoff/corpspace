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
using Corpspace.Commons.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Corpspace.ChatSpace.Infrastructure.Repositories;

public class ChannelRepository : IRepository<AppChannel, Guid>
{
    private readonly DbContext _dbContext;

    public ChannelRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<AppChannel> GetAll()
    {
        return _dbContext.Set<AppChannel>().Where(x => !x.IsDeleted);
    }

    public List<AppChannel> GetAllList()
    {
        return GetAll().ToList();
    }

    public async Task<List<AppChannel>> GetAllListAsync()
    {
        return await GetAll().ToListAsync();
    }

    public async Task<List<AppChannel>> GetListAsync(Expression<Func<AppChannel, bool>> predicate)
    {
        return await GetAll().Where(predicate).ToListAsync();
    }

    public AppChannel Get(Guid id)
    {
        return GetAll().FirstOrDefault(x => x.Id == id) ?? throw new InvalidOperationException();
    }

    public async Task<AppChannel> GetAsync(Guid id)
    {
        return await GetAll().FirstOrDefaultAsync(x => x.Id == id) ?? throw new InvalidOperationException();
    }

    public async Task<AppChannel> FirstOrDefaultAsync(Expression<Func<AppChannel, bool>> predicate)
    {
        return await GetAll().FirstOrDefaultAsync(predicate) ?? throw new InvalidOperationException();
    }

    public async Task<AppChannel> CreateAsync(AppChannel entity)
    {
        var result = await _dbContext.Set<AppChannel>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public AppChannel Create(AppChannel entity)
    {
        var result = _dbContext.Set<AppChannel>().Add(entity);
        _dbContext.SaveChanges();
        return result.Entity;
    }

    public AppChannel Insert(AppChannel entity)
    {
        entity.CreationAt = DateTime.UtcNow;
        _dbContext.Set<AppChannel>().Add(entity);
        _dbContext.SaveChanges();
        return entity;
    }

    public async Task<AppChannel> InsertAsync(AppChannel entity)
    {
        entity.CreationAt = DateTime.UtcNow;
        _dbContext.Set<AppChannel>().Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public AppChannel Update(AppChannel entity)
    {
        entity.ModificationAt = DateTime.UtcNow;
        _dbContext.Set<AppChannel>().Update(entity);
        _dbContext.SaveChanges();
        return entity;
    }

    public async Task<AppChannel> UpdateAsync(AppChannel entity)
    {
        entity.ModificationAt = DateTime.UtcNow;
        _dbContext.Set<AppChannel>().Update(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public void Delete(AppChannel entity)
    {
        entity.IsDeleted = true;
        entity.DeletionAt = DateTime.UtcNow;
        _dbContext.Set<AppChannel>().Update(entity);
        _dbContext.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var entity = Get(id);
        entity.IsDeleted = true;
        entity.DeletionAt = DateTime.UtcNow;
        _dbContext.Set<AppChannel>().Update(entity);
        _dbContext.SaveChanges();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetAsync(id);
        entity.IsDeleted = true;
        _dbContext.Set<AppChannel>().Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(AppChannel entity)
    {
        entity.IsDeleted = true;
        _dbContext.Set<AppChannel>().Update(entity);
        await _dbContext.SaveChangesAsync();
    }

    public bool Exists(Guid id)
    {
        return GetAll().Any(x => x.Id == id);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await GetAll().AnyAsync(x => x.Id == id);
    }

    public AppChannel Find(Expression<Func<AppChannel, bool>> predicate)
    {
        return GetAll().FirstOrDefault(predicate)!;
    }

    public Task<AppChannel> FindAsync(Expression<Func<AppChannel, bool>> predicate)
    {
        return GetAll().FirstOrDefaultAsync(predicate)!;
    }
}
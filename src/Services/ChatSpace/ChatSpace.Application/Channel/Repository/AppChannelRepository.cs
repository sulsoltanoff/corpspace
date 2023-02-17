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

public class AppChannelRepository : IRepository<AppChannel, Guid>
{
    private readonly ChatAppContext _dbContext;

    public AppChannelRepository(ChatAppContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<AppChannel> GetAll()
    {
        return _dbContext.AppChannels;
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
        return _dbContext.AppChannels.FirstOrDefault(x => x.Id == id) ?? throw new InvalidOperationException();
    }

    public async Task<AppChannel> GetAsync(Guid id)
    {
        return await _dbContext.AppChannels.FirstOrDefaultAsync(x => x.Id == id) ?? throw new InvalidOperationException();
    }

    public async Task<AppChannel> FirstOrDefaultAsync(Expression<Func<AppChannel, bool>> predicate)
    {
        return await _dbContext.Set<AppChannel>().FirstOrDefaultAsync(predicate) ?? throw new InvalidOperationException();
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
        _dbContext.Set<AppChannel>().Add(entity);
        _dbContext.SaveChanges();
        return entity;
    }

    public async Task<AppChannel> InsertAsync(AppChannel entity)
    {
        _dbContext.Set<AppChannel>().Add(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public AppChannel Update(AppChannel entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        _dbContext.SaveChanges();
        return entity;
    }

    public async Task<AppChannel> UpdateAsync(AppChannel entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public void Delete(AppChannel entity)
    {
        _dbContext.Set<AppChannel>().Remove(entity);
        _dbContext.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var entity = Get(id);
        Delete(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetAsync(id);
        Delete(entity);
    }

    public async Task DeleteAsync(AppChannel entity)
    {
        _dbContext.Set<AppChannel>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public bool Exists(Guid id)
    {
        return _dbContext.Set<AppChannel>().Any(e => e.Id == id);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbContext.Set<AppChannel>().AnyAsync(e => e.Id == id);
    }

    public AppChannel Find(Expression<Func<AppChannel, bool>> predicate)
    {
        return _dbContext.Set<AppChannel>().FirstOrDefault(predicate) ?? throw new InvalidOperationException();
    }

    public async Task<AppChannel> FindAsync(Expression<Func<AppChannel, bool>> predicate)
    {
        return await _dbContext.Set<AppChannel>().FirstOrDefaultAsync(predicate) ?? throw new InvalidOperationException();
    }
}
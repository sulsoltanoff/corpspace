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
using ChatSpace.Domain.Entities.User;
using Corpspace.ChatSpace.Infrastructure;
using Corpspace.Commons.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChatSpace.Application.Channel.Repository;

public class AppChannelMemberRepository : IRepository<AppUser, Guid>
{
    private readonly ChatAppContext _dbContext;

    public AppChannelMemberRepository(ChatAppContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<AppUser> GetAll()
    {
        return _dbContext.AppChannels.SelectMany(ac => ac.ChannelMembers);
    }

    public List<AppUser> GetAllList()
    {
        return _dbContext.AppChannels.SelectMany(ac => ac.ChannelMembers).ToList();
    }

    public async Task<List<AppUser>> GetAllListAsync()
    {
        return await _dbContext.AppChannels.SelectMany(ac => ac.ChannelMembers).ToListAsync();
    }

    public async Task<List<AppUser>> GetListAsync(Expression<Func<AppUser, bool>> predicate)
    {
        return await _dbContext.AppUsers.Where(predicate).ToListAsync();
    }

    public AppUser Get(Guid id)
    {
        return _dbContext.AppUsers.Find(id) ?? throw new InvalidOperationException();
    }

    public async Task<AppUser> GetAsync(Guid id)
    {
        return await _dbContext.AppUsers.FindAsync(id) ?? throw new InvalidOperationException();
    }

    public async Task<AppUser> FirstOrDefaultAsync(Expression<Func<AppUser, bool>> predicate)
    {
        return await _dbContext.AppUsers.FirstOrDefaultAsync(predicate) ?? throw new InvalidOperationException();
    }

    public async Task<AppUser> CreateAsync(AppUser entity)
    {
        var result = await _dbContext.Set<AppUser>().AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public AppUser Create(AppUser entity)
    {
        var result = _dbContext.Set<AppUser>().Add(entity);
        _dbContext.SaveChanges();
        return result.Entity;
    }

    public AppUser Insert(AppUser entity)
    {
        return _dbContext.AppUsers.Add(entity).Entity;
    }

    public async Task<AppUser> InsertAsync(AppUser entity)
    {
        await _dbContext.AppUsers.AddAsync(entity);
        return entity;
    }

    public AppUser Update(AppUser entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        _dbContext.SaveChanges();
        return entity;
    }

    public async Task<AppUser> UpdateAsync(AppUser entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public void Delete(AppUser entity)
    {
        _dbContext.AppUsers.Remove(entity);
        _dbContext.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var entity = _dbContext.AppUsers.Find(id);
        _dbContext.AppUsers.Remove(entity);
        _dbContext.SaveChanges();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _dbContext.AppUsers.FindAsync(id);
        _dbContext.AppUsers.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(AppUser entity)
    {
        _dbContext.AppUsers.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public bool Exists(Guid id)
    {
        return _dbContext.AppUsers.Any(e => e.Id == id);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbContext.AppUsers.AnyAsync(e => e.Id == id);
    }

    public AppUser Find(Expression<Func<AppUser, bool>> predicate)
    {
        return _dbContext.AppUsers.FirstOrDefault(predicate) ?? throw new InvalidOperationException();
    }

    public async Task<AppUser> FindAsync(Expression<Func<AppUser, bool>> predicate)
    {
        return await _dbContext.AppUsers.FirstOrDefaultAsync(predicate) ?? throw new InvalidOperationException();
    }
}
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

public class ChannelMemberRepository : IRepository<ChatUser, Guid>
{
    private readonly ChatAppContext _dbContext;

    public ChannelMemberRepository(ChatAppContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<ChatUser> GetAll()
    {
        return _dbContext.AppChannels.SelectMany(ac => ac.ChannelMembers);
    }

    public List<ChatUser> GetAllList()
    {
        return _dbContext.AppChannels.SelectMany(ac => ac.ChannelMembers).ToList();
    }

    public async Task<List<ChatUser>> GetAllListAsync()
    {
        return await _dbContext.AppChannels.SelectMany(ac => ac.ChannelMembers).ToListAsync();
    }

    public async Task<List<ChatUser>> GetListAsync(Expression<Func<ChatUser, bool>> predicate)
    {
        return await _dbContext.ChatUsers.Where(predicate).ToListAsync();
    }

    public ChatUser Get(Guid id)
    {
        return _dbContext.ChatUsers.Find(id) ?? throw new InvalidOperationException();
    }

    public async Task<ChatUser> GetAsync(Guid id)
    {
        return await _dbContext.ChatUsers.FindAsync(id) ?? throw new InvalidOperationException();
    }

    public async Task<ChatUser> FirstOrDefaultAsync(Expression<Func<ChatUser, bool>> predicate)
    {
        return await _dbContext.ChatUsers.FirstOrDefaultAsync(predicate) ?? throw new InvalidOperationException();
    }

    public ChatUser Insert(ChatUser entity)
    {
        return _dbContext.ChatUsers.Add(entity).Entity;
    }

    public async Task<ChatUser> InsertAsync(ChatUser entity)
    {
        await _dbContext.ChatUsers.AddAsync(entity);
        return entity;
    }

    public ChatUser Update(ChatUser entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        _dbContext.SaveChanges();
        return entity;
    }

    public async Task<ChatUser> UpdateAsync(ChatUser entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public void Delete(ChatUser entity)
    {
        _dbContext.ChatUsers.Remove(entity);
        _dbContext.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var entity = _dbContext.ChatUsers.Find(id);
        _dbContext.ChatUsers.Remove(entity);
        _dbContext.SaveChanges();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _dbContext.ChatUsers.FindAsync(id);
        _dbContext.ChatUsers.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(ChatUser entity)
    {
        _dbContext.ChatUsers.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public bool Exists(Guid id)
    {
        return _dbContext.ChatUsers.Any(e => e.Id == id);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbContext.ChatUsers.AnyAsync(e => e.Id == id);
    }

    public ChatUser Find(Expression<Func<ChatUser, bool>> predicate)
    {
        return _dbContext.ChatUsers.FirstOrDefault(predicate) ?? throw new InvalidOperationException();
    }

    public async Task<ChatUser> FindAsync(Expression<Func<ChatUser, bool>> predicate)
    {
        return await _dbContext.ChatUsers.FirstOrDefaultAsync(predicate) ?? throw new InvalidOperationException();
    }
}
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
using ChatSpace.Application.Channel.Impl;
using ChatSpace.Domain.Entities.Channels;
using Corpspace.ChatSpace.Infrastructure;
using Corpspace.Commons.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChatSpace.Application.Channel.Repository;

public class ChannelMemberRepository : IRepository<ChannelMember, Guid>
{
    private readonly ChatAppContext _dbContext;

    public ChannelMemberRepository(ChatAppContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<ChannelMember> GetAll()
    {
        return _dbContext.ChannelMembers;
    }

    public List<ChannelMember> GetAllList()
    {
        return _dbContext.ChannelMembers.ToList();
    }

    public async Task<List<ChannelMember>> GetAllListAsync()
    {
        return await _dbContext.ChannelMembers.ToListAsync();
    }

    public async Task<List<ChannelMember>> GetListAsync(Expression<Func<ChannelMember, bool>> predicate)
    {
        return await _dbContext.ChannelMembers.Where(predicate).ToListAsync();
    }

    public ChannelMember Get(Guid id)
    {
        return _dbContext.ChannelMembers.Find(id) ?? throw new InvalidOperationException();
    }

    public async Task<ChannelMember> GetAsync(Guid id)
    {
        return await _dbContext.ChannelMembers.FindAsync(id) ?? throw new InvalidOperationException();
    }

    public async Task<ChannelMember> FirstOrDefaultAsync(Expression<Func<ChannelMember, bool>> predicate)
    {
        return await _dbContext.ChannelMembers.FirstOrDefaultAsync(predicate) ?? throw new InvalidOperationException();
    }

    public ChannelMember Insert(ChannelMember entity)
    {
        return _dbContext.ChannelMembers.Add(entity).Entity;
    }

    public async Task<ChannelMember> InsertAsync(ChannelMember entity)
    {
        await _dbContext.ChannelMembers.AddAsync(entity);
        return entity;
    }

    public ChannelMember Update(ChannelMember entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        _dbContext.SaveChanges();
        return entity;
    }

    public async Task<ChannelMember> UpdateAsync(ChannelMember entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public void Delete(ChannelMember entity)
    {
        _dbContext.ChannelMembers.Remove(entity);
        _dbContext.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var entity = _dbContext.ChannelMembers.Find(id);
        _dbContext.ChannelMembers.Remove(entity);
        _dbContext.SaveChanges();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _dbContext.ChannelMembers.FindAsync(id);
        _dbContext.ChannelMembers.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(ChannelMember entity)
    {
        _dbContext.ChannelMembers.Remove(entity);
        await _dbContext.SaveChangesAsync();
    }

    public bool Exists(Guid id)
    {
        return _dbContext.ChannelMembers.Any(e => e.Id == id);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbContext.ChannelMembers.AnyAsync(e => e.Id == id);
    }

    public ChannelMember Find(Expression<Func<ChannelMember, bool>> predicate)
    {
        return _dbContext.ChannelMembers.FirstOrDefault(predicate) ?? throw new InvalidOperationException();
    }

    public async Task<ChannelMember> FindAsync(Expression<Func<ChannelMember, bool>> predicate)
    {
        return await _dbContext.ChannelMembers.FirstOrDefaultAsync(predicate) ?? throw new InvalidOperationException();
    }
}
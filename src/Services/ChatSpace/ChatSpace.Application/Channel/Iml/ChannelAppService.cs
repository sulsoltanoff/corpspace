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

using AutoMapper.Internal.Mappers;
using ChatSpace.Application.Channel.DTO;
using ChatSpace.Application.Chat.DTO;
using ChatSpace.Domain.Entities.Channels;
using ChatSpace.Domain.Exceptions;
using Corpspace.Commons.Domain.Repositories;

namespace ChatSpace.Application.Channel.Iml;

public class ChannelAppService : ChannelServiceBase, IChannelService
{
    private readonly IRepository<Domain.Entities.Channels.Channel, Guid> _channelRepository;

    public ChannelAppService(IRepository<Domain.Entities.Channels.Channel, Guid> channelRepository)
    {
        _channelRepository = channelRepository;
    }

    public async Task<ChannelDto> GetChannelByIdAsync(Guid id)
    {
        var channel = await _channelRepository.GetAsync(id);
        return ObjectMapper.Map<ChannelDto>(channel);
    }

    public ChannelDto GetChannelById(Guid id)
    {
        var channel = _channelRepository.Get(id);
        return ObjectMapper.Map<ChannelDto>(channel);
    }

    public async Task<ChannelDto> GetDirectChannelAsync(Guid userId1, Guid userId2)
    {
        var channel = await _channelRepository.FirstOrDefaultAsync(x => x.ChannelsType == ChannelsType.Direct && (x.CreatorId == userId1 || x.CreatorId == userId2));
        return ObjectMapper.Map<ChannelDto>(channel);
    }

    public async Task<UserDto> GetChannelMemberAsync(Guid channelId, Guid userId)
    {
        // implementation pending
        throw new NotImplementedException();
    }

    public async Task<List<UserDto>> GetChannelMembersAsync(Guid channelId, Guid userId)
    {
        // implementation pending
        throw new NotImplementedException();
    }

    public async Task<List<ChannelDto>> GetListChannelAsync()
    {
        var channels = await _channelRepository.GetAllListAsync();
        return ObjectMapper.Map<List<ChannelDto>>(channels);
    }

    public async Task<ChannelDto> CreateChannelAsync(CreateChannelDto input)
    {
        var channel = ObjectMapper<Domain.Entities.Channels.Channel, ChannelDto>.Map<Domain.Entities.Channels.Channel>(input);
        channel.Id = Guid.NewGuid();
        channel.CreationAt = DateTime.Now;
        channel.ModificationAt = DateTime.Now;
        await _channelRepository.InsertAsync(channel);
        return ObjectMapper.Map<ChannelDto>(channel);
    }

    public async Task UpdateChannelAsync(Guid id, UpdateChannelDto input)
    {
        var channel = await _channelRepository.GetAsync(id);
        ObjectMapper.Map(input, channel);
        channel.ModificationAt = DateTime.Now;
        await _channelRepository.UpdateAsync(channel);
    }

    public async Task DeleteChannelAsync(Guid id)
    {
        var channel = await _channelRepository.GetAsync(id);
        channel.IsDeleted = true;
        channel.DeletionAt = DateTime.Now;
        await _channelRepository.UpdateAsync(channel);
    }

    public async Task<List<ChannelDto>> AddUserChannel(Guid channelId, Guid userId)
    {
        var channel = await _channelRepository.GetAsync(channelId);
        if (channel == null)
        {
            throw new EntityNotFoundException($"{nameof(Domain.Entities.Channels.Channel)} with id: {channelId} could not be found.");
        }

        // Add the user to the channel's members list. 
        channel.ChannelMembers.Add(userId);

        await _channelRepository.UpdateAsync(channel);

        return await GetListChannelAsync();
    }

    public async Task<List<ChannelDto>> RemoveUserChannel(Guid channelId, Guid userId)
    {
        var channel = await _channelRepository.GetAsync(channelId);
        if (channel == null)
        {
            throw new EntityNotFoundException($"{nameof(Domain.Entities.Channels.Channel)} with id: {channelId} could not be found.");
        }

        // Remove the user from the channel's members list. TODO: Need to implement a service that takes an `id` and returns an entity
        channel.ChannelMembers.Remove(userId);

        await _channelRepository.UpdateAsync(channel);

        return await GetListChannelAsync();
    }
}
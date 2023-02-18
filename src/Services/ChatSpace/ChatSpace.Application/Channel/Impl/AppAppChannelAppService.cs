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

using AutoMapper;
using ChatSpace.Application.Channel.DTO;
using ChatSpace.Application.Chat.DTO;
using ChatSpace.Domain.Entities.Channels;
using ChatSpace.Domain.Entities.User;
using ChatSpace.Domain.Exceptions;
using Corpspace.Commons.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ChatSpace.Application.Channel.Impl;

public class AppAppChannelAppService : AppChannelServiceBase, IAppChannelService
{
    private readonly IRepository<AppChannel, Guid> _channelRepository;
    private readonly IRepository<AppUser, Guid> _chatMemberRepository;
    private readonly IMapper _mapper;

    public AppAppChannelAppService(IRepository<AppChannel, Guid> channelRepository, 
        IMapper mapper, IRepository<AppUser, Guid> chatMemberRepository)
    {
        _channelRepository = channelRepository;
        _mapper = mapper;
        _chatMemberRepository = chatMemberRepository;
    }

    public async Task<AppChannelDto> GetChannelByIdAsync(Guid id)
    {
        var channel = await _channelRepository.GetAsync(id);
        return _mapper.Map<AppChannelDto>(channel);
    }

    public AppChannelDto GetChannelById(Guid id)
    {
        var channel = _channelRepository.Get(id);
        return _mapper.Map<AppChannelDto>(channel);
    }

    public async Task<AppChannelDto> GetDirectChannelAsync(Guid userId1, Guid userId2)
    {
        var channel = await _channelRepository.FirstOrDefaultAsync(x => x.ChannelsType == AppChannelType.OneToOne && (x.CreatorId == userId1 || x.CreatorId == userId2));
        return _mapper.Map<AppChannelDto>(channel);
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

    public async Task<List<AppChannelDto>> GetListChannelAsync()
    {
        var channels = await _channelRepository.GetAllListAsync();
        return _mapper.Map<List<AppChannelDto>>(channels);
    }

    public async Task<AppChannelDto> CreateChannelAsync(CreateAppChannelDto input)
    {
        var channelExists = await _channelRepository.GetAll()
            .AnyAsync(x => x.Name == input.Name);
        
        if (channelExists)
        {
            throw new AppChannelAlreadyExistsException("Channel with this name already exists.");
        }
        
        var channel = _mapper.Map<AppChannel>(input);
        channel.Id = Guid.NewGuid();
        channel.CreationAt = DateTime.Now;
        channel.ModificationAt = DateTime.Now;
        await _channelRepository.InsertAsync(channel);
        return _mapper.Map<AppChannelDto>(channel);
    }


    public async Task<AppChannelDto> CreateOneToOneChannelAsync(CreateOneToOneAppChannelDto oneToOneChannelDto)
    {
        var userOne = await _chatMemberRepository.GetAsync(oneToOneChannelDto.UserIdOne);
        var userTwo = await _chatMemberRepository.GetAsync(oneToOneChannelDto.UserIdTwo);
        if (userOne == null || userTwo == null)
        {
            throw new ArgumentException("One or both of the specified users do not exist in the system.");
        }
        
        var channel = new AppChannel
        {
            ChannelsType = AppChannelType.OneToOne,
            ChannelMembers = new List<AppUser> { userOne, userTwo }
        };
        var createdChannel = await _channelRepository.CreateAsync(channel);
        
        return _mapper.Map<AppChannelDto>(createdChannel);
    }

    public async Task<AppChannel> UpdateChannelAsync(Guid id, UpdateAppChannelDto input)
    {
        var channel = await _channelRepository.GetAsync(id);
        _mapper.Map(input, channel);
        channel.ModificationAt = DateTime.Now;
        return await _channelRepository.UpdateAsync(channel);
    }

    public async Task<bool> DeleteChannelAsync(Guid id)
    {
        var channel = await _channelRepository.GetAsync(id);
        if (channel == null) return false;

        channel.IsDeleted = true;
        channel.DeletionAt = DateTime.Now;
        await _channelRepository.UpdateAsync(channel);

        return true;
    }

    public async Task<List<AppChannelDto>> AddUserChannel(Guid channelId, Guid userId)
    {
        var channelMember = await _chatMemberRepository.GetAsync(userId);
        var channel = await _channelRepository.GetAsync(channelId);
        if (channel == null)
        {
            throw new EntityNotFoundException($"{nameof(AppChannel)} with id: {channelId} could not be found.");
        }
        
        var isUserAlreadyAMember = channel.ChannelMembers.Any(x => x.Id == userId);
        if (isUserAlreadyAMember) return await GetListChannelAsync();
        
        channel.ChannelMembers.Add(channelMember);

        await _channelRepository.UpdateAsync(channel);

        return await GetListChannelAsync();
    }

    public async Task<List<AppChannelDto>> RemoveUserChannel(Guid channelId, Guid userId)
    {
        var channelMember = await _chatMemberRepository.GetAsync(userId);
        var channel = await _channelRepository.GetAsync(channelId);
        if (channel == null)
        {
            throw new EntityNotFoundException($"{nameof(AppChannel)} with id: {channelId} could not be found.");
        }

        // Remove the user from the channel's members list. TODO: Need to implement a service that takes an `id` and returns an entity
        channel.ChannelMembers.Remove(channelMember);

        await _channelRepository.UpdateAsync(channel);

        return await GetListChannelAsync();
    }

    public async Task<List<AppChannelDto>> SearchChannelsAsync(SearchAppChannelDto searchAppDto)
    {
        var channels = await _channelRepository.GetAllListAsync();

        if (!string.IsNullOrEmpty(searchAppDto.Name))
        {
            channels = channels.Where(c => c.Name.Contains(searchAppDto.Name, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        if (!string.IsNullOrEmpty(searchAppDto.Description))
        {
            channels = channels.Where(c => c.Description.Contains(searchAppDto.Description, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        if (searchAppDto.IsPublic.HasValue)
        {
            channels = channels.Where(c => c.IsPublic == searchAppDto.IsPublic).ToList();
        }

        return _mapper.Map<List<AppChannelDto>>(channels);
    }
}
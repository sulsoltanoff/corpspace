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
using Microsoft.Extensions.Logging;

namespace ChatSpace.Application.Channel.Impl;

/// <summary>
/// Implements the application service for managing channels in the chat application.
/// </summary>
public class AppAppChannelAppService : AppChannelServiceBase, IAppChannelService
{
    private readonly IRepository<AppChannel, Guid> _channelRepository;
    private readonly IRepository<AppUser, Guid> _chatMemberRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<AppAppChannelAppService> _logger;

    public AppAppChannelAppService(IRepository<AppChannel, Guid> channelRepository, 
        IMapper mapper, IRepository<AppUser, Guid> chatMemberRepository, ILogger<AppAppChannelAppService> logger)
    {
        _channelRepository = channelRepository;
        _mapper = mapper;
        _chatMemberRepository = chatMemberRepository;
        _logger = logger;
    }

    /// <summary>
    /// Returns a channel by its ID.
    /// </summary>
    /// <param name="id">The ID of the channel to return.</param>
    /// <returns>The channel with the specified ID.</returns>
    /// <exception cref="ChatAppException">Thrown if the channel is not found.</exception>
    public async Task<AppChannelDto> GetChannelByIdAsync(Guid id)
    {
        try
        {
            var channel = await _channelRepository.FirstOrDefaultAsync(x => x.Id == id);

            if (channel == null)
            {
                _logger.LogInformation("Channel not found.");
                throw new ChatAppException("Channel not found.");
            }

            return _mapper.Map<AppChannelDto>(channel);
        }
        catch (InvalidOperationException e)
        {
            _logger.LogError(e, "InvalidOperationException");
            throw new ChatAppException("Channel not found by id.");
        }
    }

    /// <summary>
    /// Gets a channel by its unique identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the channel to get.</param>
    /// <returns>The channel with the specified unique identifier, as a <see cref="AppChannelDto"/>.</returns>
    /// <exception cref="ChatAppException">Thrown when a channel with the specified unique identifier cannot be found.</exception>
    public AppChannelDto GetChannelById(Guid id)
    {
        try
        {
            var channel = _channelRepository.Get(id);
            return _mapper.Map<AppChannelDto>(channel);
        }
        catch (InvalidOperationException e)
        {
            _logger.LogError(e, "InvalidOperationException");
            throw new ChatAppException("Channel not found.");
        }
    }

    /// <summary>
    /// Returns a direct channel between two users.
    /// </summary>
    /// <param name="userId1">The GUID of the first user.</param>
    /// <param name="userId2">The GUID of the second user.</param>
    /// <returns>An AppChannelDto object representing the direct channel between the two users.</returns>
    /// <exception cref="ChatAppException">ChatAppException if no direct channel is found.</exception>
    public async Task<AppChannelDto> GetDirectChannelAsync(Guid userId1, Guid userId2)
    {
        var channel = await _channelRepository.FirstOrDefaultAsync(x => x.AppChannelType == AppChannelType.OneToOne && (x.CreatorId == userId1 || x.CreatorId == userId2));
        if (channel == null)
        {
            _logger.LogInformation("Direct channel not found.");
            throw new ChatAppException("Direct channel not found.");
        }
        return _mapper.Map<AppChannelDto>(channel);
    }

    /// <summary>
    /// Gets the channel member by channel ID and user ID.
    /// </summary>
    /// <param name="channelId">The ID of the channel to get the member from.</param>
    /// <param name="userId">The ID of the user to get from the channel.</param>
    /// <returns>A UserDto object representing the member.</returns>
    /// <exception cref="ChatAppException">Thrown when the channel with the specified ID is not found or when the user with the specified ID is not a member of the channel.</exception>
    public async Task<UserDto> GetChannelMemberAsync(Guid channelId, Guid userId)
    {
        var channel = await _channelRepository.GetAsync(channelId);
        
        if (channel == null)
        {
            _logger.LogInformation($"Channel '{channel}' not found.");
            throw new ChatAppException($"Channel with ID '{channelId}' not found.");
        }
        
        var member = channel.ChannelMembers.FirstOrDefault(m => m.Id == userId);
        
        if (member == null)
        {
            _logger.LogInformation($"User '{userId}' is not a member of channel '{channel.Name}'.");
            throw new ChatAppException($"User with ID '{userId}' is not a member of channel '{channel.Name}'.");
        }
        
        return _mapper.Map<UserDto>(member);

    }

    /// <summary>
    /// Get a direct channel between two users.
    /// </summary>
    /// <param name="channelId">The ID of the first user.</param>
    /// <param name="userId">The ID of the second user.</param>
    /// <returns>The direct channel between the two users.</returns>
    /// <exception cref="ChatAppException"> Thrown when the channel with the specified ID is not found or when the user with the specified ID is not a member of the channel. </exception>
    public async Task<List<UserDto>> GetChannelMembersAsync(Guid channelId, Guid userId)
    {
        try
        {
            var channel = await _channelRepository.GetAsync(channelId);
            var members = channel.ChannelMembers;
            return _mapper.Map<List<UserDto>>(members);
        }
        catch (InvalidOperationException e)
        {
            _logger.LogError(e, "InvalidOperationException");
            throw new ChatAppException("Channel not found.");
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error while getting channel members.");
            throw new ChatAppException("Error while getting channel members.");
        }
    }

    /// <summary>
    /// Gets a list of all channels.
    /// </summary>
    /// <returns>The list of all channels.</returns>
    /// <exception cref="ChatAppException">Thrown when an error occurs while getting the channels.</exception>
    public async Task<List<AppChannelDto>> GetListChannelAsync()
    {
        try
        {
            var channels = await _channelRepository.GetAllListAsync();
            return _mapper.Map<List<AppChannelDto>>(channels);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting channels");
            throw new ChatAppException("Error getting channels", ex);
        }
    }

    /// <summary>
    /// Creates a new channel.
    /// </summary>
    /// <param name="input">The data for the new channel.</param>
    /// <returns>The newly created channel as an AppChannelDto object.</returns>
    /// <exception cref="AppChannelAlreadyExistsException">Thrown if a channel with the same name already exists.</exception>
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

    /// <summary>
    /// Creates a one-to-one channel between two users.
    /// </summary>
    /// <param name="oneToOneChannelDto">The DTO containing information about the two users.</param>
    /// <returns>The created one-to-one channel DTO.</returns>
    /// <exception cref="ArgumentException">Thrown when one or both of the specified users do not exist in the system.</exception>
    public async Task<AppChannelDto> CreateOneToOneChannelAsync(CreateOneToOneAppChannelDto oneToOneChannelDto)
    {
        var userOne = await _chatMemberRepository.GetAsync(oneToOneChannelDto.UserIdOne);
        var userTwo = await _chatMemberRepository.GetAsync(oneToOneChannelDto.UserIdTwo);
        if (userOne == null || userTwo == null)
        {
            _logger.LogInformation("One or both of the specified users do not exist in the system.");
            throw new ArgumentException("One or both of the specified users do not exist in the system.");
        }
        
        var channel = new AppChannel
        {
            AppChannelType = AppChannelType.OneToOne,
            ChannelMembers = new List<AppUser> { userOne, userTwo }
        };
        var createdChannel = await _channelRepository.CreateAsync(channel);
        
        return _mapper.Map<AppChannelDto>(createdChannel);
    }

    /// <summary>
    /// Updates the specified channel with the provided input.
    /// </summary>
    /// <param name="id">The ID of the channel to update.</param>
    /// <param name="input">The input used to update the channel.</param>
    /// <returns>The updated channel.</returns>
    /// <exception cref="ChatAppException">Thrown when the specified channel is not found.</exception>
    public async Task<AppChannel> UpdateChannelAsync(Guid id, UpdateAppChannelDto input)
    {
        var channel = await _channelRepository.GetAsync(id);
        if (channel == null)
        {
            _logger.LogInformation("Channel not found.");
            throw new ChatAppException("Channel not found.");
        }
    
        _mapper.Map(input, channel);
        channel.ModificationAt = DateTime.Now;

        try
        {
            return await _channelRepository.UpdateAsync(channel);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error updating channel.");
            throw new ChatAppException("Error updating channel.");
        }
    }

    /// <summary>
    /// Deletes a channel with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the channel to delete.</param>
    /// <returns>Returns a boolean value indicating whether the channel was successfully deleted.</returns>
    /// <exception cref="ChatAppException">Thrown when an error occurs while deleting the channel.</exception>
    public async Task<bool> DeleteChannelAsync(Guid id)
    {
        try
        {
            var channel = await _channelRepository.GetAsync(id);
            if (channel.Equals(null)) 
            {
                _logger.LogInformation("Channel not found.");
                return false;
            }

            channel.IsDeleted = true;
            channel.DeletionAt = DateTime.Now;
            await _channelRepository.UpdateAsync(channel);

            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting channel with ID: {0}", id);
            throw new ChatAppException("Error deleting channel.");
        }
    }

    /// <summary>
    /// Adds a user to a channel.
    /// </summary>
    /// <param name="channelId">The id of the channel to add the user to.</param>
    /// <param name="userId">The id of the user to add to the channel.</param>
    /// <returns>A list of all channels, including the one the user was added to.</returns>
    /// <exception cref="EntityNotFoundException">Thrown when the channel or user cannot be found.</exception>
    public async Task<List<AppChannelDto>> AddUserChannel(Guid channelId, Guid userId)
    {
        var channel = await _channelRepository.GetAsync(channelId);
        if (channel == null)
        {
            throw new EntityNotFoundException($"{nameof(AppChannel)} with id: {channelId} could not be found.");
        }

        var channelMember = await _chatMemberRepository.FirstOrDefaultAsync(x => x.Id == userId);
        if (channelMember == null)
        {
            throw new EntityNotFoundException($"{nameof(AppUser)} with id: {userId} could not be found.");
        }

        var isUserAlreadyAMember = channel.ChannelMembers.Any(x => x.Id == userId);
        if (isUserAlreadyAMember)
        {
            return await GetListChannelAsync();
        }

        channel.ChannelMembers.Add(channelMember);

        await _channelRepository.UpdateAsync(channel);

        return await GetListChannelAsync();
    }

    /// <summary>
    /// Removes a user from a channel.
    /// </summary>
    /// <param name="channelId">The id of the channel to remove the user from.</param>
    /// <param name="userId">The id of the user to be removed from the channel.</param>
    /// <returns>A list of all channels after the user is removed from the specified channel.</returns>
    /// <exception cref="EntityNotFoundException">Thrown if either the channel or user cannot be found.</exception>
    public async Task<List<AppChannelDto>> RemoveUserChannel(Guid channelId, Guid userId)
    {
        var channel = await _channelRepository.GetAsync(channelId);
        if (channel == null)
        {
            throw new EntityNotFoundException($"{nameof(AppChannel)} with id: {channelId} could not be found.");
        }

        var channelMember = channel.ChannelMembers.FirstOrDefault(x => x.Id == userId);
        if (channelMember == null)
        {
            _logger.LogInformation($"User with id {userId} is not a member of channel with id {channelId}.");
            return await GetListChannelAsync();
        }

        channel.ChannelMembers.Remove(channelMember);

        await _channelRepository.UpdateAsync(channel);

        return await GetListChannelAsync();
    }

    /// <summary>
    /// Searches channels by name, description and public status.
    /// </summary>
    /// <param name="searchAppDto">The parameters to search channels with.</param>
    /// <returns>A list of <see cref="AppChannelDto"/> objects that match the search criteria.</returns>
    /// <exception cref="ChatAppException">Thrown when an error occurs while searching for channels.</exception>
    public async Task<List<AppChannelDto>> SearchChannelsAsync(SearchAppChannelDto searchAppDto)
    {
        try
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
        catch (Exception ex)
        {
            _logger.LogError(ex, $"An error occurred while searching for channels: {ex.Message}");
            throw new ChatAppException("An error occurred while searching for channels.");
        }
    }
}
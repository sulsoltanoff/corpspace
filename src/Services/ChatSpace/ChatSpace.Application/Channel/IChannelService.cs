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

using ChatSpace.Application.Channel.DTO;
using ChatSpace.Application.Chat.DTO;
using ChatSpace.Domain.Entities.Channels;
using Corpspace.Commons.Applications.Services;

namespace ChatSpace.Application.Channel;

public interface IChannelService : IApplicationService
{
    Task<ChannelDto> GetChannelByIdAsync(Guid id);
    
    ChannelDto GetChannelById(Guid id);

    Task<ChannelDto> GetDirectChannelAsync(Guid userId1, Guid userId2);

    Task<UserDto> GetChannelMemberAsync(Guid channelId, Guid userId);
    
    Task<List<UserDto>> GetChannelMembersAsync(Guid channelId, Guid userId);

    Task<List<ChannelDto>> GetListChannelAsync();
    
    Task<ChannelDto> CreateChannelAsync(CreateChannelDto input);
    
    Task<AppChannel> UpdateChannelAsync(Guid id, UpdateChannelDto input);
    
    Task<bool> DeleteChannelAsync(Guid id);
    
    Task<List<ChannelDto>> AddUserChannel(Guid channelId, Guid userId);
    
    Task<List<ChannelDto>> RemoveUserChannel(Guid channelId, Guid userId);
    
    Task<List<ChannelDto>> SearchChannelsAsync(SearchChannelDto searchDto);
}
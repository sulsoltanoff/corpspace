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

public interface IAppChannelService : IApplicationService
{
    Task<AppChannelDto> GetChannelByIdAsync(Guid id);
    
    AppChannelDto GetChannelById(Guid id);

    Task<AppChannelDto> GetDirectChannelAsync(Guid userId1, Guid userId2);

    Task<UserDto> GetChannelMemberAsync(Guid channelId, Guid userId);
    
    Task<List<UserDto>> GetChannelMembersAsync(Guid channelId, Guid userId);

    Task<List<AppChannelDto>> GetListChannelAsync();
    
    Task<AppChannelDto> CreateChannelAsync(CreateAppChannelDto input);

    Task<AppChannelDto> CreateOneToOneChannelAsync(CreateOneToOneAppChannelDto createOneToOneAppChannel);

    Task<AppChannel> UpdateChannelAsync(Guid id, UpdateAppChannelDto input);
    
    Task<bool> DeleteChannelAsync(Guid id);
    
    Task<List<AppChannelDto>> AddUserChannel(Guid channelId, Guid userId);
    
    Task<List<AppChannelDto>> RemoveUserChannel(Guid channelId, Guid userId);
    
    Task<List<AppChannelDto>> SearchChannelsAsync(SearchAppChannelDto searchAppDto);
}
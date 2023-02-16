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
using ChatSpace.Domain.Entities.Messages;
using ChatSpace.Domain.Entities.User;

namespace Corpspace.ChatSpace.API.Infrastructure.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AppChannel, ChannelDto>();
        CreateMap<ChannelDto, AppChannel>();
        CreateMap<ChatUser, UserDto>();
        CreateMap<UserDto, ChatUser>();
        CreateMap<Message, MessageDto>();
        CreateMap<MessageDto, Message>();
        CreateMap<ChannelsType, ChannelsTypeDto>();
    }
}
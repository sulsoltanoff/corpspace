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
using ChatSpace.Application.Chat.DTO;
using ChatSpace.Domain.Entities.Messages;
using Corpspace.Commons.Domain.Repositories;

namespace ChatSpace.Application.Chat.Iml;

public class ChatAppService : ChatAppServiceBase, IChatAppService
{
    private readonly IRepository<Message, Guid> _messageRepository;

    public ChatAppService(IRepository<Message, Guid> messageRepository)
    {
        _messageRepository = messageRepository;
    }

    public async Task<List<MessageDto>> GetMessages()
    {
        var messages = await _messageRepository.GetAllListAsync();
        return messages.Select(m => ObjectMapper<Message, MessageDto>.Map(m)).ToList();
    }

    public Task SendMessage(string message, Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task MarkMessageAsRead(Guid messageId)
    {
        throw new NotImplementedException();
    }

    public Task MarkMessageAsUnread(Guid messageId)
    {
        throw new NotImplementedException();
    }

    public Task DeleteMessage(Guid messageId)
    {
        throw new NotImplementedException();
    }
}
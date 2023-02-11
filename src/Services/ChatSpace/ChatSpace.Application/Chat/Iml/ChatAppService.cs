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
using ChatSpace.Domain.Exceptions;
using Corpspace.Commons.Domain.Repositories;

namespace ChatSpace.Application.Chat.Iml;

public class ChatAppService : ChatAppServiceBase, IChatAppService
{
    private readonly IRepository<Message, Guid> _messageRepository;

    public ChatAppService(IRepository<Message, Guid> messageRepository)
    {
        _messageRepository = messageRepository;
    }

public async Task<MessageDto> CreateMessage(CreateMessageDto input)
{
    var message = new Message
    {
        Content = input.Content,
        ChannelId = input.ChannelId,
        UserId = input.UserId
    };

    await _messageRepository.InsertAsync(message);

    return new MessageDto
    {
        Id = message.Id,
        Content = message.Content,
        ChannelId = message.ChannelId,
        UserId = input.UserId
    };
}

public async Task<MessageDto> GetMessageById(Guid messageId)
{
    var message = await _messageRepository.FirstOrDefaultAsync(messageId);

    if (message == null)
    {
        throw new EntityNotFoundException(nameof(Message), messageId);
    }

    return new MessageDto
    {
        Id = message.Id,
        Content = message.Content,
        ChannelId = message.ChannelId,
        UserId = message.UserId,
    };
}

public async Task<List<MessageDto>> GetMessagesByIds(List<Guid> messageIds)
{
    var messages = await _messageRepository.GetListAsync(m => messageIds.Contains(m.Id));

    return messages.Select(m => new MessageDto
    {
        Id = m.Id,
        Content = m.Content,
        ChannelId = m.ChannelId,
        UserId = m.UserId,
    }).ToList();
}

public async Task<List<MessageDto>> GetMessagesByChannelId(Guid channelId)
{
    var messages = await _messageRepository.GetListAsync(m => m.ChannelId == channelId);

    return messages.Select(m => new MessageDto
    {
        Id = m.Id,
        Content = m.Content,
        ChannelId = m.ChannelId,
        UserId = m.UserId,
    }).ToList();
}

public async Task<List<MessageDto>> GetMessagesByChannelIds(List<Guid> channelIds)
{
    var messages = await _messageRepository.GetListAsync(m => channelIds.Contains(m.ChannelId));

    return messages.Select(m => new MessageDto
    {
        Id = m.Id,
        Content = m.Content,
        ChannelId = m.ChannelId,
        UserId = m.UserId,
    }).ToList();
}

public async Task DeleteMessage(Guid messageId)
{
    var message = await _messageRepository.FirstOrDefaultAsync(messageId);

    if (message == null)
    {
        throw new EntityNotFoundException(nameof(Message), messageId);
    }

    await _messageRepository.DeleteAsync(message);
}
public async Task<MessageDto> UpdateMessage(Guid messageId)
{
    var message = await _messageRepository.FirstOrDefaultAsync(messageId);

    if (message == null)
    {
        throw new EntityNotFoundException(nameof(Message), messageId);
    }

    // Update message properties here

    await _messageRepository.UpdateAsync(message);

    return ObjectMapper.Map<MessageDto>(message);
}
}
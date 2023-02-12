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

using System.Net;
using ChatSpace.Application.Channel.DTO;
using ChatSpace.Application.Channel.Impl;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Corpspace.ChatSpace.API.Controllers;

[Route("api/v1/channels")]
// [Authorize]
[ApiController]
public class ChannelsController : Controller
{
    private readonly IMediator _mediator;
    private readonly ILogger<ChannelsController> _logger;
    private readonly ChannelAppService _channelAppService;

    public ChannelsController(IMediator mediator, ILogger<ChannelsController> logger, ChannelAppService channelAppService)
    {
        _mediator = mediator;
        _logger = logger;
        _channelAppService = channelAppService;
    }

    [Route("channels")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreatePublicChannel([FromBody] CreateChannelDto channelDto)
    {
        _logger.LogDebug("CreatePublicChannel");
        await _channelAppService.CreateChannelAsync(channelDto);
        return Ok();
    }
}
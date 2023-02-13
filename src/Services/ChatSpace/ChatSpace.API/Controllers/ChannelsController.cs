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

using System.Net;
using ChatSpace.Application.Channel.DTO;
using ChatSpace.Application.Channel.Impl;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Corpspace.ChatSpace.API.Controllers;

/// <summary>
/// ChannelsController is responsible for handling HTTP requests related to channels.
/// </summary>
[Route("api/v1/channels")]
// [Authorize]
[ApiController]
public class ChannelsController : Controller
{
    /// <summary>
    /// IMediator interface is used to send commands and publish events.
    /// </summary>
    private readonly IMediator _mediator;
    
    /// <summary>
    /// ILogger<ChannelsController> is used to log messages for debugging and diagnostic purposes.
    /// </summary>
    private readonly ILogger<ChannelsController> _logger;
    
    /// <summary>
    /// ChannelAppService is a service for managing channels.
    /// </summary>
    private readonly ChannelAppService _channelAppService;

    /// <summary>
    /// ChannelsController constructor.
    /// </summary>
    /// <param name="mediator">IMediator instance for sending commands and publishing events.</param>
    /// <param name="logger">ILogger<ChannelsController> instance for logging messages.</param>
    /// <param name="channelAppService">ChannelAppService instance for managing channels.</param>
    public ChannelsController(IMediator mediator, ILogger<ChannelsController> logger, ChannelAppService channelAppService)
    {
        _mediator = mediator;
        _logger = logger;
        _channelAppService = channelAppService;
    }

    /// <summary>
    /// Creates a public channel.
    /// </summary>
    /// <param name="channelDto">CreateChannelDto object containing channel data.</param>
    /// <returns>HTTP status code OK (200) on success, HTTP status code Bad Request (400) on failure.</returns>
    [Route("")]
    [HttpPost]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreatePublicChannel([FromBody] CreateChannelDto channelDto)
    {
        _logger.LogDebug("CreatePublicChannel");
        await _channelAppService.CreateChannelAsync(channelDto);
        return Ok();
    }
    
    /// <summary>
    /// Gets a channel by its ID.
    /// </summary>
    /// <param name="id">ID of the channel to get.</param>
    /// <returns>HTTP status code OK (200) with the channel data on success, HTTP status code Not Found (404) if the channel was not found.</returns>
    [Route("{id:Guid}")]
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetChannelById(Guid id)
    {
        var channel = await _channelAppService.GetChannelByIdAsync(id);
        if (channel == null) return NotFound();

        return Ok(channel);
    }
    
    /// <summary>
    /// Updates a channel with the specified id and information in the `UpdateChannelDto` object.
    /// </summary>
    /// <param name="id">The unique identifier of the channel to be updated.</param>
    /// <param name="channelDto">The information to be used for updating the channel.</param>
    /// <returns>An `IActionResult` indicating the result of the update operation.
    /// If the channel was successfully updated, returns `Ok` with the updated channel.
    /// If the channel was not found, returns `NotFound`.
    /// </returns>
    [Route("{id:Guid}")]
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> UpdateChannel(Guid id, [FromBody] UpdateChannelDto channelDto)
    {
        var channel = await _channelAppService.UpdateChannelAsync(id, channelDto);
        if (channel == null) return NotFound();

        return Ok(channel);
    }


    /// <summary>
    /// Retrieves a list of channels based on the search criteria provided in the `searchDto` parameter.
    /// </summary>
    /// <param name="searchDto">An instance of `SearchChannelDto` that contains the search criteria for the channels.</param>
    /// <returns>Returns a list of channels that match the search criteria.</returns>
    /// <response code="200">Returns the list of channels.</response>
    [Route("")]
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> SearchChannels([FromQuery] SearchChannelDto searchDto)
    {
        var channels = await _channelAppService.SearchChannelsAsync(searchDto);
        return Ok(channels);
    }

    /// <summary>
    /// Deletes a channel with the specified ID.
    /// </summary>
    /// <param name="id">The ID of the channel to delete.</param>
    /// <returns>A `204 No Content` response if the channel was successfully deleted,
    /// or a `404 Not Found` response if the channel does not exist.</returns>
    [Route("{id:Guid}")]
    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> DeleteChannel(Guid id)
    {
        var success = await _channelAppService.DeleteChannelAsync(id);
        if (!success) return NotFound();

        return NoContent();
    }
}
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
using ChatSpace.Application.Channel;
using ChatSpace.Application.Channel.DTO;
using ChatSpace.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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
    private readonly IAppChannelService _appChannelAppService;

    /// <summary>
    /// ChannelsController constructor.
    /// </summary>
    /// <param name="mediator">IMediator instance for sending commands and publishing events.</param>
    /// <param name="logger">ILogger<ChannelsController> instance for logging messages.</param>
    /// <param name="appChannelAppService">ChannelAppService instance for managing channels.</param>
    public ChannelsController(IMediator mediator, ILogger<ChannelsController> logger, IAppChannelService appChannelAppService)
    {
        _mediator = mediator;
        _logger = logger;
        _appChannelAppService = appChannelAppService;
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
    [ProducesResponseType((int)HttpStatusCode.Conflict)]
    public async Task<IActionResult> CreatePublicChannel([FromBody] CreateAppChannelDto channelDto)
    {
        _logger.LogDebug("CreatePublicChannel");
        try
        {
            await _appChannelAppService.CreateChannelAsync(channelDto);
            return Ok();
        }
        catch (AppChannelAlreadyExistsException ex)
        {
            return Conflict(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating public channel");
            return BadRequest("Error creating public channel");
        }
    }
    
    /// <summary>
    /// Gets all channels list.
    /// </summary>
    /// <returns>HTTP status code OK (200) with the channel data on success, HTTP status code Not Found (404) if the channel was not found.</returns>
    [Route("")]
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAllChannels()
    {
        _logger.LogDebug("GetAllChannels");
        var channel = await _appChannelAppService.GetListChannelAsync();
        if (channel.IsNullOrEmpty()) return NotFound();

        return Ok(channel);
    }
    
    
    /// <summary>
    /// Gets a channel by its ID.
    /// </summary>
    /// <param name="channelId">ID of the channel to get.</param>
    /// <returns>HTTP status code OK (200) with the channel data on success, HTTP status code Not Found (404) if the channel was not found.</returns>
    [Route("{channelId:Guid}")]
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetChannelById(Guid channelId)
    {
        var channel = await _appChannelAppService.GetChannelByIdAsync(channelId);
        if (channel.Equals(null)) return NotFound();

        return Ok(channel);
    }

    /// <summary>
    /// Creating a new one-to-one message between two users.
    /// </summary>
    /// <param name="channelDto">App channel dto.</param>
    /// <returns>HTTP status code OK (200) on success, HTTP status code Bad Request (400) on failure.</returns>
    [Route("one-to-one/")]
    [HttpPost]
    [ProducesResponseType(typeof(CreateOneToOneAppChannelDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateOneToOneChat(CreateOneToOneAppChannelDto channelDto)
    {
        var channel = await _appChannelAppService.CreateOneToOneChannelAsync(channelDto);
        if (channel == null)
        {
            return BadRequest();
        }

        return Ok(channel);
    }
    
    /// <summary>
    /// Updates a channel with the specified id and information in the `UpdateChannelDto` object.
    /// </summary>
    /// <param name="id">The unique identifier of the channel to be updated.</param>
    /// <param name="appChannelDto">The information to be used for updating the channel.</param>
    /// <returns>An `IActionResult` indicating the result of the update operation.
    /// If the channel was successfully updated, returns `Ok` with the updated channel.
    /// If the channel was not found, returns `NotFound`.
    /// </returns>
    [Route("{id:Guid}")]
    [HttpPut]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> UpdateChannel(Guid id, [FromBody] UpdateAppChannelDto appChannelDto)
    {
        var channel = await _appChannelAppService.UpdateChannelAsync(id, appChannelDto);
        if (channel == null) return NotFound();

        return Ok(channel);
    }


    /// <summary>
    /// Gets a list of all channels based on the search criteria specified in the `searchDto` parameter.
    /// </summary>
    /// <param name="searchAppDto">An instance of `SearchChannelDto` that contains the search criteria for the channels.</param>
    /// <returns>Returns a list of channels that match the search criteria.</returns>
    /// <response code="200">Returns the list of channels.</response>
    [Route("search/")]
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> SearchChannels([FromQuery] SearchAppChannelDto searchAppDto)
    {
        var channels = await _appChannelAppService.SearchChannelsAsync(searchAppDto);
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
        var success = await _appChannelAppService.DeleteChannelAsync(id);
        if (!success) return NotFound();

        return NoContent();
    }
}
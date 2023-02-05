#region Corpspace© Apache-2.0
// Copyright 2023 The Corpspace Technologies
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

using Corpspace.Mobile.HttpAggregator.Models;
using Corpspace.Mobile.HttpAggregator.Services;

namespace Corpspace.Mobile.HttpAggregator.Controllers;

[Route("api/v1/[controller]")]
[Authorize]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IBasketService _basketService;
    private readonly IOrderingService _orderingService;

    public OrderController(IBasketService basketService, IOrderingService orderingService)
    {
        _basketService = basketService;
        _orderingService = orderingService;
    }

    [Route("draft/{basketId}")]
    [HttpGet]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(OrderData), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<OrderData>> GetOrderDraftAsync(string basketId)
    {
        if (string.IsNullOrEmpty(basketId))
        {
            return BadRequest("Need a valid basketid");
        }
        // Get the basket data and build a order draft based on it
        var basket = await _basketService.GetByIdAsync(basketId);

        if (basket == null)
        {
            return BadRequest($"No basket found for id {basketId}");
        }

        return await _orderingService.GetOrderDraftAsync(basket);
    }
}

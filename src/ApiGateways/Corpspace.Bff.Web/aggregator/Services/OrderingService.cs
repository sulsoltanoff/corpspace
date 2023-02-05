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

using Corpspace.Web.Shopping.HttpAggregator.Models;

namespace Corpspace.Web.Shopping.HttpAggregator.Services;

public class OrderingService : IOrderingService
{
    private readonly OrderingGrpc.OrderingGrpcClient _orderingGrpcClient;
    private readonly ILogger<OrderingService> _logger;

    public OrderingService(OrderingGrpc.OrderingGrpcClient orderingGrpcClient, ILogger<OrderingService> logger)
    {
        _orderingGrpcClient = orderingGrpcClient;
        _logger = logger;
    }

    public async Task<OrderData> GetOrderDraftAsync(BasketData basketData)
    {
        _logger.LogDebug(" grpc client created, basketData={@basketData}", basketData);

        var command = MapToOrderDraftCommand(basketData);
        var response = await _orderingGrpcClient.CreateOrderDraftFromBasketDataAsync(command);
        _logger.LogDebug(" grpc response: {@response}", response);

        return MapToResponse(response, basketData);
    }

    private OrderData MapToResponse(GrpcOrdering.OrderDraftDTO orderDraft, BasketData basketData)
    {
        if (orderDraft == null)
        {
            return null;
        }

        var data = new OrderData
        {
            Buyer = basketData.BuyerId,
            Total = (decimal)orderDraft.Total,
        };

        orderDraft.OrderItems.ToList().ForEach(o => data.OrderItems.Add(new OrderItemData
        {
            Discount = (decimal)o.Discount,
            PictureUrl = o.PictureUrl,
            ProductId = o.ProductId,
            ProductName = o.ProductName,
            UnitPrice = (decimal)o.UnitPrice,
            Units = o.Units,
        }));

        return data;
    }

    private CreateOrderDraftCommand MapToOrderDraftCommand(BasketData basketData)
    {
        var command = new CreateOrderDraftCommand
        {
            BuyerId = basketData.BuyerId,
        };

        basketData.Items.ForEach(i => command.Items.Add(new BasketItem
        {
            Id = i.Id,
            OldUnitPrice = (double)i.OldUnitPrice,
            PictureUrl = i.PictureUrl,
            ProductId = i.ProductId,
            ProductName = i.ProductName,
            Quantity = i.Quantity,
            UnitPrice = (double)i.UnitPrice,
        }));

        return command;
    }

}

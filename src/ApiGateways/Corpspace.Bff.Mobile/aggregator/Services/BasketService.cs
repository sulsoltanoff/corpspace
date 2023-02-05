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

namespace Corpspace.Mobile.HttpAggregator.Services;

public class BasketService : IBasketService
{
    private readonly Basket.BasketClient _basketClient;
    private readonly ILogger<BasketService> _logger;

    public BasketService(Basket.BasketClient basketClient, ILogger<BasketService> logger)
    {
        _basketClient = basketClient;
        _logger = logger;
    }

    public async Task<BasketData> GetByIdAsync(string id)
    {
        _logger.LogDebug("grpc client created, request = {@id}", id);
        var response = await _basketClient.GetBasketByIdAsync(new BasketRequest { Id = id });
        _logger.LogDebug("grpc response {@response}", response);

        return MapToBasketData(response);
    }

    public async Task UpdateAsync(BasketData currentBasket)
    {
        _logger.LogDebug("Grpc update basket currentBasket {@currentBasket}", currentBasket);
        var request = MapToCustomerBasketRequest(currentBasket);
        _logger.LogDebug("Grpc update basket request {@request}", request);

        await _basketClient.UpdateBasketAsync(request);
    }

    private BasketData MapToBasketData(CustomerBasketResponse customerBasketRequest)
    {
        if (customerBasketRequest == null)
        {
            return null;
        }

        var map = new BasketData
        {
            BuyerId = customerBasketRequest.Buyerid
        };

        customerBasketRequest.Items.ToList().ForEach(item => map.Items.Add(new BasketDataItem
        {
            Id = item.Id,
            OldUnitPrice = (decimal)item.Oldunitprice,
            PictureUrl = item.Pictureurl,
            ProductId = item.Productid,
            ProductName = item.Productname,
            Quantity = item.Quantity,
            UnitPrice = (decimal)item.Unitprice
        }));

        return map;
    }

    private CustomerBasketRequest MapToCustomerBasketRequest(BasketData basketData)
    {
        if (basketData == null)
        {
            return null;
        }

        var map = new CustomerBasketRequest
        {
            Buyerid = basketData.BuyerId
        };

        basketData.Items.ToList().ForEach(item => map.Items.Add(new BasketItemResponse
        {
            Id = item.Id,
            Oldunitprice = (double)item.OldUnitPrice,
            Pictureurl = item.PictureUrl,
            Productid = item.ProductId,
            Productname = item.ProductName,
            Quantity = item.Quantity,
            Unitprice = (double)item.UnitPrice
        }));

        return map;
    }
}

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

public class CatalogService : ICatalogService
{
    private readonly Catalog.CatalogClient _client;
    private readonly ILogger<CatalogService> _logger;

    public CatalogService(Catalog.CatalogClient client, ILogger<CatalogService> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<CatalogItem> GetCatalogItemAsync(int id)
    {
        var request = new CatalogItemRequest { Id = id };
        _logger.LogInformation("grpc request {@request}", request);
        var response = await _client.GetItemByIdAsync(request);
        _logger.LogInformation("grpc response {@response}", response);
        return MapToCatalogItemResponse(response);

    }

    public async Task<IEnumerable<CatalogItem>> GetCatalogItemsAsync(IEnumerable<int> ids)
    {
        var request = new CatalogItemsRequest { Ids = string.Join(",", ids), PageIndex = 1, PageSize = 10 };
        _logger.LogInformation("grpc request {@request}", request);
        var response = await _client.GetItemsByIdsAsync(request);
        _logger.LogInformation("grpc response {@response}", response);
        return response.Data.Select(this.MapToCatalogItemResponse);

    }

    private CatalogItem MapToCatalogItemResponse(CatalogItemResponse catalogItemResponse)
    {
        return new CatalogItem
        {
            Id = catalogItemResponse.Id,
            Name = catalogItemResponse.Name,
            PictureUri = catalogItemResponse.PictureUri,
            Price = (decimal)catalogItemResponse.Price
        };
    }
}

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

using Corpspace.Mobile.HttpAggregator.Config;
using Corpspace.Mobile.HttpAggregator.Models;

namespace Corpspace.Mobile.HttpAggregator.Services;

public class OrderApiClient : IOrderApiClient
{
    private readonly HttpClient _apiClient;
    private readonly ILogger<OrderApiClient> _logger;
    private readonly UrlsConfig _urls;

    public OrderApiClient(HttpClient httpClient, ILogger<OrderApiClient> logger, IOptions<UrlsConfig> config)
    {
        _apiClient = httpClient;
        _logger = logger;
        _urls = config.Value;
    }

    public async Task<OrderData> GetOrderDraftFromBasketAsync(BasketData basket)
    {
        var uri = _urls.Orders + UrlsConfig.OrdersOperations.GetOrderDraft();
        var content = new StringContent(JsonSerializer.Serialize(basket), System.Text.Encoding.UTF8, "application/json");
        var response = await _apiClient.PostAsync(uri, content);

        response.EnsureSuccessStatusCode();

        var ordersDraftResponse = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<OrderData>(ordersDraftResponse, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
    }
}

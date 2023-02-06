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

namespace Corpspace.WebhookClient.Services;

public class WebhooksClient : IWebhooksClient
{

    private readonly IHttpClientFactory _httpClientFactory;
    private readonly Settings _settings;
    public WebhooksClient(IHttpClientFactory httpClientFactory, IOptions<Settings> settings)
    {
        _httpClientFactory = httpClientFactory;
        _settings = settings.Value;
    }
    public async Task<IEnumerable<WebhookResponse>> LoadWebhooks()
    {
        var client = _httpClientFactory.CreateClient("GrantClient");
        var response = await client.GetAsync(_settings.WebhooksUrl + "/api/v1/webhooks");
        var json = await response.Content.ReadAsStringAsync();
        var subscriptions = JsonSerializer.Deserialize<IEnumerable<WebhookResponse>>(json, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        return subscriptions;
    }
}

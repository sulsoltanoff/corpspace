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

namespace Corpspace.Services.Webhooks.API.Services;

public class WebhooksSender : IWebhooksSender
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger _logger;
    public WebhooksSender(IHttpClientFactory httpClientFactory, ILogger<WebhooksSender> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task SendAll(IEnumerable<WebhookSubscription> receivers, WebhookData data)
    {
        var client = _httpClientFactory.CreateClient();
        var json = JsonSerializer.Serialize(data);
        var tasks = receivers.Select(r => OnSendData(r, json, client));
        await Task.WhenAll(tasks.ToArray());
    }

    private Task OnSendData(WebhookSubscription subs, string jsonData, HttpClient client)
    {
        var request = new HttpRequestMessage()
        {
            RequestUri = new Uri(subs.DestUrl, UriKind.Absolute),
            Method = HttpMethod.Post,
            Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
        };

        if (!string.IsNullOrWhiteSpace(subs.Token))
        {
            request.Headers.Add("X-csp-whtoken", subs.Token);
        }
        _logger.LogDebug("Sending hook to {DestUrl} of type {Type}", subs.Type.ToString(), subs.Type.ToString());
        return client.SendAsync(request);
    }

}

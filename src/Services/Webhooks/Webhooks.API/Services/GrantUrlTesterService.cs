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

namespace Corpspace.Services.Webhooks.API.Services;

internal class GrantUrlTesterService : IGrantUrlTesterService
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly ILogger _logger;
    public GrantUrlTesterService(IHttpClientFactory factory, ILogger<IGrantUrlTesterService> logger)
    {
        _clientFactory = factory;
        _logger = logger;
    }

    public async Task<bool> TestGrantUrl(string urlHook, string url, string token)
    {
        if (!CheckSameOrigin(urlHook, url))
        {
            _logger.LogWarning("Url of the hook ({UrlHook} and the grant url ({Url} do not belong to same origin)", urlHook, url);
            return false;
        }


        var client = _clientFactory.CreateClient("GrantClient");
        var msg = new HttpRequestMessage(HttpMethod.Options, url);
        msg.Headers.Add("X-csp-whtoken", token);
        _logger.LogInformation("Sending the OPTIONS message to {Url} with token \"{Token}\"", url, token ?? string.Empty);
        try
        {
            var response = await client.SendAsync(msg);
            var tokenReceived = response.Headers.TryGetValues("X-csp-whtoken", out var tokenValues) ? tokenValues.FirstOrDefault() : null;
            var tokenExpected = string.IsNullOrWhiteSpace(token) ? null : token;
            _logger.LogInformation("Response code is {StatusCode} for url {Url} and token in header was {TokenReceived} (expected token was {TokenExpected})", response.StatusCode, url, tokenReceived, tokenExpected);
            return response.IsSuccessStatusCode && tokenReceived == tokenExpected;
        }
        catch (Exception ex)
        {
            _logger.LogWarning("Exception {TypeName} when sending OPTIONS request. Url can't be granted.", ex.GetType().Name);
            return false;
        }
    }

    private bool CheckSameOrigin(string urlHook, string url)
    {
        var firstUrl = new Uri(urlHook, UriKind.Absolute);
        var secondUrl = new Uri(url, UriKind.Absolute);

        return firstUrl.Scheme == secondUrl.Scheme &&
            firstUrl.Port == secondUrl.Port &&
            firstUrl.Host == secondUrl.Host;
    }
}

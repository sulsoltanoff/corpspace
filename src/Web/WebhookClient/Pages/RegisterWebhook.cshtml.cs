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

using System.Net.Http.Formatting;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Corpspace.WebhookClient.Pages;

[Authorize]

public class RegisterWebhookModel : PageModel
{
    private readonly Settings _settings;
    private readonly IHttpClientFactory _httpClientFactory;

    [BindProperty] public string Token { get; set; }

    public int ResponseCode { get; set; }
    public string RequestUrl { get; set; }
    public string GrantUrl { get; set; }
    public string ResponseMessage { get; set; }
    public string RequestBodyJson { get; set; }

    public RegisterWebhookModel(IOptions<Settings> settings, IHttpClientFactory httpClientFactory)
    {
        _settings = settings.Value;
        _httpClientFactory = httpClientFactory;
    }

    public void OnGet()
    {
        ResponseCode = (int)HttpStatusCode.OK;
        Token = _settings.Token;
    }

    public async Task<IActionResult> OnPost()
    {
        ResponseCode = (int)HttpStatusCode.OK;
        var protocol = Request.IsHttps ? "https" : "http";
        var selfurl = !string.IsNullOrEmpty(_settings.SelfUrl) ? _settings.SelfUrl : $"{protocol}://{Request.Host}/{Request.PathBase}";
        if (!selfurl.EndsWith("/"))
        {
            selfurl = selfurl + "/";
        }
        var granturl = $"{selfurl}check";
        var url = $"{selfurl}webhook-received";
        var client = _httpClientFactory.CreateClient("GrantClient");

        var payload = new WebhookSubscriptionRequest()
        {
            Event = "OrderPaid",
            GrantUrl = granturl,
            Url = url,
            Token = Token
        };
        var response = await client.PostAsync<WebhookSubscriptionRequest>(_settings.WebhooksUrl + "/api/v1/webhooks", payload, new JsonMediaTypeFormatter());

        if (response.IsSuccessStatusCode)
        {
            return RedirectToPage("WebhooksList");
        }
        else
        {
            RequestBodyJson = JsonSerializer.Serialize(payload);
            ResponseCode = (int)response.StatusCode;
            ResponseMessage = response.ReasonPhrase;
            GrantUrl = granturl;
            RequestUrl = $"{response.RequestMessage.Method} {response.RequestMessage.RequestUri}";
        }

        return Page();
    }
}
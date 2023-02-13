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

namespace Corpspace.WebhookClient.Controllers;

[ApiController]
[Route("webhook-received")]
public class WebhooksReceivedController : Controller
{

    private readonly Settings _settings;
    private readonly ILogger _logger;
    private readonly IHooksRepository _hooksRepository;

    public WebhooksReceivedController(IOptions<Settings> settings, ILogger logger, IHooksRepository hooksRepository)
    {
        _settings = settings.Value;
        _logger = logger;
        _hooksRepository = hooksRepository;
    }

    [HttpPost]
    public async Task<IActionResult> NewWebhook(WebhookData hook)
    {
        var header = Request.Headers[HeaderNames.WebHookCheckHeader];
        var token = header.FirstOrDefault();

        _logger.LogInformation("Received hook with token {Token}. My token is {MyToken}. Token validation is set to {ValidateToken}", token, _settings.Token, _settings.ValidateToken);

        if (!_settings.ValidateToken || _settings.Token == token)
        {
            _logger.LogInformation("Received hook is going to be processed");
            var newHook = new WebHookReceived()
            {
                Data = hook.Payload,
                When = hook.When,
                Token = token
            };
            await _hooksRepository.AddNew(newHook);
            _logger.LogInformation("Received hook was processed.");
            return Ok(newHook);
        }

        _logger.LogInformation("Received hook is NOT processed - Bad Request returned.");
        return BadRequest();
    }
}

﻿#region Corpspace© Apache-2.0
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

using Corpspace.Services.Webhooks.API.Infrastructure;
using Corpspace.Services.Webhooks.API.Model;

namespace Corpspace.Services.Webhooks.API.Services;

public class WebhooksRetriever : IWebhooksRetriever
{
    private readonly WebhooksContext _db;
    public WebhooksRetriever(WebhooksContext db)
    {
        _db = db;
    }
    public async Task<IEnumerable<WebhookSubscription>> GetSubscriptionsOfType(WebhookType type)
    {
        var data = await _db.Subscriptions.Where(s => s.Type == type).ToListAsync();
        return data;
    }
}

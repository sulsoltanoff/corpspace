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

using Corpspace.BuildingBlocks.EventBus.Abstractions;
using Corpspace.Services.Webhooks.API.Model;
using Corpspace.Services.Webhooks.API.Services;

namespace Corpspace.Services.Webhooks.API.IntegrationEvents;

public class OrderStatusChangedToShippedIntegrationEventHandler : IIntegrationEventHandler<OrderStatusChangedToShippedIntegrationEvent>
{
    private readonly IWebhooksRetriever _retriever;
    private readonly IWebhooksSender _sender;
    private readonly ILogger _logger;
    public OrderStatusChangedToShippedIntegrationEventHandler(IWebhooksRetriever retriever, IWebhooksSender sender, ILogger<OrderStatusChangedToShippedIntegrationEventHandler> logger)
    {
        _retriever = retriever;
        _sender = sender;
        _logger = logger;
    }

    public async Task Handle(OrderStatusChangedToShippedIntegrationEvent @event)
    {
        var subscriptions = await _retriever.GetSubscriptionsOfType(WebhookType.OrderShipped);
        _logger.LogInformation("Received OrderStatusChangedToShippedIntegrationEvent and got {SubscriptionCount} subscriptions to process", subscriptions.Count());
        var whook = new WebhookData(WebhookType.OrderShipped, @event);
        await _sender.SendAll(subscriptions, whook);
    }
}

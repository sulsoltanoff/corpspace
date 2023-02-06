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

using System.Linq;
using Corpspace.BuildingBlocks.EventBus;
using Xunit;

namespace EventBus.Tests;

public class InMemorySubscriptionManagerTests
{
    [Fact]
    public void After_Creation_Should_Be_Empty()
    {
        var manager = new InMemoryEventBusSubscriptionsManager();
        Assert.True(manager.IsEmpty);
    }

    [Fact]
    public void After_One_Event_Subscription_Should_Contain_The_Event()
    {
        var manager = new InMemoryEventBusSubscriptionsManager();
        manager.AddSubscription<TestIntegrationEvent,TestIntegrationEventHandler>();
        Assert.True(manager.HasSubscriptionsForEvent<TestIntegrationEvent>());
    }

    [Fact]
    public void After_All_Subscriptions_Are_Deleted_Event_Should_No_Longer_Exists()
    {
        var manager = new InMemoryEventBusSubscriptionsManager();
        manager.AddSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();
        manager.RemoveSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();
        Assert.False(manager.HasSubscriptionsForEvent<TestIntegrationEvent>());
    }

    [Fact]
    public void Deleting_Last_Subscription_Should_Raise_On_Deleted_Event()
    {
        bool raised = false;
        var manager = new InMemoryEventBusSubscriptionsManager();
        manager.OnEventRemoved += (o, e) => raised = true;
        manager.AddSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();
        manager.RemoveSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();
        Assert.True(raised);
    }

    [Fact]
    public void Get_Handlers_For_Event_Should_Return_All_Handlers()
    {
        var manager = new InMemoryEventBusSubscriptionsManager();
        manager.AddSubscription<TestIntegrationEvent, TestIntegrationEventHandler>();
        manager.AddSubscription<TestIntegrationEvent, TestIntegrationOtherEventHandler>();
        var handlers = manager.GetHandlersForEvent<TestIntegrationEvent>();
        Assert.Equal(2, handlers.Count());
    }

}
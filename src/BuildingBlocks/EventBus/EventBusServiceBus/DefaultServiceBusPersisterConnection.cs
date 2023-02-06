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

namespace Corpspace.BuildingBlocks.EventBusServiceBus;

public class DefaultServiceBusPersisterConnection : IServiceBusPersisterConnection
{
    private readonly string _serviceBusConnectionString;
    private ServiceBusClient _topicClient;
    private readonly ServiceBusAdministrationClient _subscriptionClient;

    private bool _disposed;

    public DefaultServiceBusPersisterConnection(string serviceBusConnectionString)
    {
        _serviceBusConnectionString = serviceBusConnectionString;
        _subscriptionClient = new ServiceBusAdministrationClient(_serviceBusConnectionString);
        _topicClient = new ServiceBusClient(_serviceBusConnectionString);
    }

    public ServiceBusClient TopicClient
    {
        get
        {
            if (_topicClient.IsClosed)
            {
                _topicClient = new ServiceBusClient(_serviceBusConnectionString);
            }
            return _topicClient;
        }
    }

    public ServiceBusAdministrationClient AdministrationClient => 
        _subscriptionClient;

    public ServiceBusClient CreateModel()
    {
        if (_topicClient.IsClosed)
        {
            _topicClient = new ServiceBusClient(_serviceBusConnectionString);
        }

        return _topicClient;
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed) return;

        _disposed = true;
        await _topicClient.DisposeAsync();
    }
}

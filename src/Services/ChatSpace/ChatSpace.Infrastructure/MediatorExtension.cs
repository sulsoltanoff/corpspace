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

using Corpspace.Commons.Domain.Entities;
using MediatR;

namespace Corpspace.ChatSpace.Infrastructure;

internal static class MediatorExtension
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, ChatAppContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<Entity<Guid>>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents.ToList())
            .ToList();

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());
    }
}
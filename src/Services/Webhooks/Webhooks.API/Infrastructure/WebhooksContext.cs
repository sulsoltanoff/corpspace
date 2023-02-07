﻿#region Corpspace© Apache-2.0
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

namespace Corpspace.Services.Webhooks.API.Infrastructure;

public class WebhooksContext : DbContext
{

    public WebhooksContext(DbContextOptions<WebhooksContext> options) : base(options)
    {
    }
    public DbSet<WebhookSubscription> Subscriptions { get; set; }
}

public class WebhooksContextDesignFactory : IDesignTimeDbContextFactory<WebhooksContext>
{
    public WebhooksContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<WebhooksContext>()
            .UseNpgsql("Database=Corpspace.Services.ChatMessage;User Id=corpspace;Password=corpspace;Host=localhost;Port=5432;sslmode=false;");

        return new WebhooksContext(optionsBuilder.Options);
    }
}

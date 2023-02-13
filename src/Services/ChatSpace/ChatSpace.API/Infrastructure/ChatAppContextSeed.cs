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

using ChatSpace.Domain.Entities.User;
using Corpspace.ChatSpace.Infrastructure;
using Corpspace.Commons.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;
using Polly;
using Polly.Retry;

namespace Corpspace.ChatSpace.API.Infrastructure;

public class ChatAppContextSeed
{
    public async Task SeedAsync(ChatAppContext context, IWebHostEnvironment env, IOptions<ChatAppSettings> settings, ILogger<ChatAppContextSeed> logger)
    {
        var policy = CreatePolicy(logger, nameof(ChatAppContextSeed));

        await policy.ExecuteAsync(async () =>
        {

            var useCustomizationData = settings.Value
                .UseCustomizationData;

            var contentRootPath = env.ContentRootPath;


            using (context)
            {
                context.Database.Migrate();

                if (!context.ChatUsers.Any())
                {
                    context.ChatUsers.AddRange(GetPredefinedCardTypes());

                    await context.SaveChangesAsync();
                }

                await context.SaveChangesAsync();
            }
        });
    }
    
    private AsyncRetryPolicy CreatePolicy(ILogger<ChatAppContextSeed> logger, string prefix, int retries = 3)
    {
        return Policy.Handle<NpgsqlException>().
            WaitAndRetryAsync(
                retryCount: retries,
                sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                onRetry: (exception, timeSpan, retry, ctx) =>
                {
                    logger.LogWarning(exception, "[{prefix}] Exception {ExceptionType} with message {Message} detected on attempt {retry} of {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                }
            );
    }
    
    private IEnumerable<ChatUser> GetPredefinedCardTypes()
    {
        return Entity.GetAll<ChatUser>();
    }
}
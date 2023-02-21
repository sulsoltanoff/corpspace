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

using ChatSpace.Domain.Entities.Channels;
using ChatSpace.Domain.Entities.Team;
using ChatSpace.Domain.Entities.User;
using Corpspace.ChatSpace.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;
using Polly;
using Polly.Retry;

namespace Corpspace.ChatSpace.API.Infrastructure;

public class ChatAppContextSeed : IChatAppContextSeed
{
    private readonly ChatAppContext _context;
    private readonly ILogger<ChatAppContextSeed> _logger;
    private readonly IWebHostEnvironment _env;
    private readonly IOptions<ChatAppSettings> _settings;

    public ChatAppContextSeed(ChatAppContext context, ILogger<ChatAppContextSeed> logger, IWebHostEnvironment env, IOptions<ChatAppSettings> settings)
    {
        _context = context;
        _logger = logger;
        _env = env;
        _settings = settings;
    }
    
    public async Task SeedAsync()
    {
        var policy = CreatePolicy(_logger, nameof(ChatAppContextSeed));

        await policy.ExecuteAsync(async () =>
        {

            var useCustomizationData = _settings.Value
                .UseCustomizationData;

            var contentRootPath = _env.ContentRootPath;


            using (_context)
            {
                _context.Database.Migrate();

                if (!_context.AppTeams.Any())
                {
                    _context.AppTeams.AddRange(GetPredefinedTeams(_context));

                    await _context.SaveChangesAsync();
                }
                
                if (!_context.AppChannels.Any())
                {
                    _context.AppChannels.AddRange(GetPredefinedChannels(_context));
                
                    await _context.SaveChangesAsync();
                }
                
                if (!_context.AppUsers.Any())
                {
                    _context.AppUsers.AddRange(GetPredefinedUsers(_context));
                
                    await _context.SaveChangesAsync();
                }

                await _context.SaveChangesAsync();
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

    private IEnumerable<AppTeam> GetPredefinedTeams(ChatAppContext context)
    {
        var teams = new List<AppTeam>
        {
            new ()
            {
                Name = "team1",
                DisplayName = "Team 1",
                Description = "First team",
                Members = new List<AppUser>(),
                CreationAt = DateTime.Now,
                ModificationAt = DateTime.Now,
                IsDeleted = false
            }
        };
            
        return teams;
    }
    
    private IEnumerable<AppChannel> GetPredefinedChannels(ChatAppContext context)
    {
        var teamId = context.Set<AppTeam>().First().Id;
        
        var channels = new List<AppChannel>
        {
            new()
            {
                TeamId = teamId,
                AppChannelType = AppChannelType.Open,
                AppChannelTypeId = Guid.Empty,
                DisplayName = "General",
                Description = "General channel",
                Name = "general",
                LastPostAt = DateTime.Now,
                CreatorId = Guid.Empty,
                ChannelMembers = new List<AppUser>(),
                ModificationAt = DateTime.Now,
                CreationAt = DateTime.Now,
                DeletionAt = null,
                IsDeleted = false,
                IsPublic = true
            }
        };
            
        return channels;
    }
    
    private IEnumerable<AppUser> GetPredefinedUsers(ChatAppContext context)
    {
        var team = context.AppTeams.FirstOrDefault(t => t.Name == "team1");
        var channel = context.AppChannels.FirstOrDefault(t => t.Name == "general");
        if (team == null || channel == null)
        {
            throw new Exception("Cannot seed users without team or channel");
        }
        
        var chatUsers = new List<AppUser>
        {
            new()
            {
                Username = "JohnDoe",
                Email = "johndoe@example.com",
                EmailVerified = true,
                FirstName = "John",
                LastName = "Doe",
                Position = "Developer",
                Roles = "user",
                ChannelId = channel.Id,
                UserTeamId = team.Id,
                Props = new Dictionary<string, string>(),
                NotifyProps = new Dictionary<string, string>(),
                LastPictureUpdate = DateTime.Now,
                FailedAttempts = 0,
                Locale = "en-US",
                LastActivityAt = DateTime.Now,
                IsBot = false,
                BotDescription = "test",
                BotLastIconUpdate = DateTime.Now,
                ModificationAt = DateTime.Now,
                CreationAt = DateTime.Now,
                DeletionAt = null,
                IsDeleted = false
            }
        };
    
        return chatUsers;
    }
}
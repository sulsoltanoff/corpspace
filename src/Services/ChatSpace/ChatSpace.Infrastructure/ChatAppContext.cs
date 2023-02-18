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
using ChatSpace.Domain.Entities.Messages;
using ChatSpace.Domain.Entities.Team;
using ChatSpace.Domain.Entities.User;
using Corpspace.ChatSpace.Infrastructure.EntityConfiguration;
using Corpspace.ChatSpace.Infrastructure.Utils;
using Corpspace.Commons.Domain.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Corpspace.ChatSpace.Infrastructure;

public class ChatAppContext : DbContext, IUnitOfWork
{
    private readonly IMediator _mediator;

    public ChatAppContext(DbContextOptions<ChatAppContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
    }
    
    public ChatAppContext(DbContextOptions<ChatAppContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
        System.Diagnostics.Debug.WriteLine("ChatAppContext::ctor ->" + GetHashCode());
    }

    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<AppTeam> AppTeams { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<AppChannel> AppChannels { get; set; }
    public DbSet<Metadata> Metadatas { get; set; }
    public DbSet<Draft> Drafts { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<ChatThreads> ChatThreads { get; set; }
    public DbSet<ChatThreadResponse> ChatThreadResponses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new AppChannelTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ChannelTypeConfiguration());
        modelBuilder.ApplyConfiguration(new AppUserTypeConfiguration());
        modelBuilder.ApplyConfiguration(new DraftTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ImageTypeConfiguration());
        modelBuilder.ApplyConfiguration(new MessageTypeConfiguration());
        modelBuilder.ApplyConfiguration(new MetadataTypeConfiguration());
        modelBuilder.ApplyConfiguration(new AppTeamTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ChatThreadResponseTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ThreadsTypeConfiguration());
        
        modelBuilder.Entity<AppChannel>()
            .Property(x => x.ChannelsType)
            .HasConversion(new ChannelTypeConverter());
    }

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEventsAsync(this);
        return await base.SaveChangesAsync(cancellationToken) > 0;
    }
    
    public class ChatAppContextDesignFactory : IDesignTimeDbContextFactory<ChatAppContext>
    {
        public ChatAppContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ChatAppContext>()
                .UseNpgsql("Database=Corpspace.Services.ChatSpace;User Id=corpspace;Password=corpspace;Host=localhost;Port=5432;sslmode=disable;");

            return new ChatAppContext(optionsBuilder.Options, new NoMediator());
        }

        class NoMediator : IMediator
        {
            public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
            {
                return default(IAsyncEnumerable<TResponse>);
            }

            public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default)
            {
                return default(IAsyncEnumerable<object?>);
            }

            public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
            {
                return Task.CompletedTask;
            }

            public Task Publish(object notification, CancellationToken cancellationToken = default)
            {
                return Task.CompletedTask;
            }

            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
            {
                return Task.FromResult<TResponse>(default(TResponse));
            }

            public Task<object> Send(object request, CancellationToken cancellationToken = default)
            {
                return Task.FromResult(default(object));
            }
        }
    }
}
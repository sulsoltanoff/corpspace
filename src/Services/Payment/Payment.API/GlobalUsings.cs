#region Corpspace© Apache-2.0
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

global using Autofac.Extensions.DependencyInjection;
global using Autofac;
global using Azure.Core;
global using Azure.Identity;
global using HealthChecks.UI.Client;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore;
global using Microsoft.Extensions.Diagnostics.HealthChecks;
global using Microsoft.Extensions.Options;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using RabbitMQ.Client;
global using Serilog.Context;
global using Serilog;
global using System.Threading.Tasks;
global using System;
global using System.IO;
global using Corpspace.BuildingBlocks.EventBus;
global using Corpspace.BuildingBlocks.EventBus.Abstractions;
global using Corpspace.BuildingBlocks.EventBus.Events;
global using Corpspace.BuildingBlocks.EventBusRabbitMQ;
global using Corpspace.BuildingBlocks.EventBusServiceBus;
global using Corpspace.Services.Payment.API.IntegrationEvents.EventHandling;
global using Corpspace.Services.Payment.API.IntegrationEvents.Events;
global using Microsoft.AspNetCore.Hosting;
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
global using HealthChecks.UI.Client;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Diagnostics.HealthChecks;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.Mvc.Filters;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore;
global using Microsoft.EntityFrameworkCore.Design;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Diagnostics.HealthChecks;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Microsoft.OpenApi.Models;
global using RabbitMQ.Client;
global using Swashbuckle.AspNetCore.SwaggerGen;
global using System.Collections.Generic;
global using System.ComponentModel.DataAnnotations;
global using System.Data.Common;
global using System.IdentityModel.Tokens.Jwt;
global using System.Linq;
global using System.Net.Http;
global using System.Net;
global using System.Reflection;
global using System.Text.Json;
global using System.Text;
global using System.Threading.Tasks;
global using System.Threading;
global using System;
global using Corpspace.BuildingBlocks.EventBus;
global using Corpspace.BuildingBlocks.EventBus.Abstractions;
global using Corpspace.BuildingBlocks.EventBus.Events;
global using Corpspace.BuildingBlocks.EventBusRabbitMQ;
global using Corpspace.BuildingBlocks.EventBusServiceBus;
global using Corpspace.BuildingBlocks.IntegrationEventLogEF.Services;
global using Corpspace.Devspaces.Support;
global using Corpspace.Services.Webhooks.API.Exceptions;
global using Corpspace.Services.Webhooks.API.Infrastructure;
global using Corpspace.Services.Webhooks.API.Infrastructure.ActionResult;
global using Corpspace.Services.Webhooks.API.IntegrationEvents;
global using Corpspace.Services.Webhooks.API.Model;
global using Corpspace.Services.Webhooks.API.Services;
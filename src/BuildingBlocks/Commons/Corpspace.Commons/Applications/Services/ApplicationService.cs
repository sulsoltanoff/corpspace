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

using Corpspace.Commons.Applications.Features;
using Corpspace.Commons.Authorization;
using Corpspace.Commons.Domain.UnitOfWork;
using Serilog;

namespace Corpspace.Commons.Applications.Services;

public abstract class ApplicationService : IApplicationService, IServiceBase
{
    public IUnitOfWork UnitOfWork { get; set; }
    
    public ILogger Logger { get; set; }
    
    public IPermissionChecker PermissionChecker { protected get; set; }
    
    public IFeatureChecker FeatureChecker { get; set; }

    protected virtual Task<bool> IsEnabledAsync(string featureName)
    {
        return FeatureChecker.IsEnabledAsync(featureName);
    }
    
    protected virtual bool IsEnabled(string featureName)
    {
        return FeatureChecker.IsEnabled(featureName);
    }
    
    protected virtual Task<bool> IsGrantedAsync(string permissionName)
    {
        return PermissionChecker.IsGrantedAsync(permissionName);
    }
    
    protected virtual bool IsGranted(string permissionName)
    {
        return PermissionChecker.IsGranted(permissionName);
    }
}
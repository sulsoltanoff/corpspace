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

namespace Corpspace.WebSPA;

public class AppSettings
{
    public string IdentityUrl { get; set; }
    public string BasketUrl { get; set; }
    public string MarketingUrl { get; set; }

    public string PurchaseUrl { get; set; }
    public string SignalrHubUrl { get; set; }

    public string ActivateCampaignDetailFunction { get; set; }
    public bool UseCustomizationData { get; set; }
}

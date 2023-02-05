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

namespace Corpspace.Services.Identity.API.Models.ManageViewModels;

public record IndexViewModel
{
    public bool HasPassword { get; init; }

    public IList<UserLoginInfo> Logins { get; init; }

    public string PhoneNumber { get; init; }

    public bool TwoFactor { get; init; }

    public bool BrowserRemembered { get; init; }
}
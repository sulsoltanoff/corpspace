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

namespace Corpspace.Services.Identity.API.Models.AccountViewModels;

public record VerifyCodeViewModel
{
    [Required]
    public string Provider { get; init; }

    [Required]
    public string Code { get; init; }

    public string ReturnUrl { get; init; }

    [Display(Name = "Remember this browser?")]
    public bool RememberBrowser { get; init; }

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; init; }
}
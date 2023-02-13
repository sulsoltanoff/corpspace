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

namespace Corpspace.Services.Identity.API.Services;

public class RedirectService : IRedirectService
{
    public string ExtractRedirectUriFromReturnUrl(string url)
    {
        var decodedUrl = System.Net.WebUtility.HtmlDecode(url);
        var results = Regex.Split(decodedUrl, "redirect_uri=");
        if (results.Length < 2)
            return "";

        string result = results[1];

        string splitKey;
        if (result.Contains("signin-oidc"))
            splitKey = "signin-oidc";
        else
            splitKey = "scope";

        results = Regex.Split(result, splitKey);
        if (results.Length < 2)
            return "";

        result = results[0];

        return result.Replace("%3A", ":").Replace("%2F", "/").Replace("&", "");
    }
}
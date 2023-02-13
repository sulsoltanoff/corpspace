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

using System.Text;
using System.Text.Json;

namespace Corpspace.Services.Identity.API.Quickstart.Diagnostics;

public class DiagnosticsViewModel
{
    public DiagnosticsViewModel(AuthenticateResult result)
    {
        AuthenticateResult = result;

        if (!result.Properties!.Items.ContainsKey("client_list")) return;
        var encoded = result.Properties.Items["client_list"];
        var bytes = Base64Url.Decode(encoded);
        var value = Encoding.UTF8.GetString(bytes);

        Clients = JsonSerializer.Deserialize<string[]>(value);
    }

    public AuthenticateResult AuthenticateResult { get; }
    public IEnumerable<string> Clients { get; } = new List<string>();
}

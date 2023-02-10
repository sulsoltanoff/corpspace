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

namespace ChatSpace.Domain.Constants;

public static class GeneralConstants
{
    public const string LoginRegex = "^[_.@A-Za-z0-9-]*$";  // Regex for acceptable logins
    public const string SystemAccount = "system";
    public const string AnonymousUser = "anonymoususer";
    public const string DefaultLangKey = "en";
    public const int MaxMessageLength = 2 * 1024; // 2KB
    public const int ChannelNameMaxLenght = 76;
    public const string ServiceName = "ChatSpace";
}

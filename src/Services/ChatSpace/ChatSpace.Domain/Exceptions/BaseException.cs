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

namespace ChatSpace.Domain.Exceptions;

public abstract class BaseException : Exception
{
    protected BaseException(string type, string detail) : this(type, detail, null, null)
    {
    }

    protected BaseException(string type, string detail, string entityName) : this(type, detail, entityName, null)
    {
    }

    protected BaseException(string type, string detail, string entityName, string errorKey) : base(detail)
    {
        Type = type;
        Detail = detail;
        EntityName = entityName;
        ErrorKey = errorKey;
    }

    private string Type { get; }
    private string Detail { get; }
    private string EntityName { get; }
    private string ErrorKey { get; }
}

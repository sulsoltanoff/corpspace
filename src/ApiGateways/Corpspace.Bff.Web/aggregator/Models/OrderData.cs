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

namespace Corpspace.Web.Shopping.HttpAggregator.Models;

public class OrderData
{
    public string OrderNumber { get; set; }

    public DateTime Date { get; set; }

    public string Status { get; set; }

    public decimal Total { get; set; }

    public string Description { get; set; }

    public string City { get; set; }

    public string Street { get; set; }

    public string State { get; set; }

    public string Country { get; set; }

    public string ZipCode { get; set; }

    public string CardNumber { get; set; }

    public string CardHolderName { get; set; }

    public bool IsDraft { get; set; }

    public DateTime CardExpiration { get; set; }

    public string CardExpirationShort { get; set; }

    public string CardSecurityNumber { get; set; }

    public int CardTypeId { get; set; }

    public string Buyer { get; set; }

    public List<OrderItemData> OrderItems { get; } = new();
}


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

namespace Corpspace.Mobile.HttpAggregator.Config;

public class UrlsConfig
{
    public class CatalogOperations
    {
        public static string GetItemById(int id) => $"/api/v1/catalog/items/{id}";

        public static string GetItemsById(IEnumerable<int> ids) => $"/api/v1/catalog/items?ids={string.Join(',', ids)}";
    }

    public class BasketOperations
    {
        public static string GetItemById(string id) => $"/api/v1/basket/{id}";

        public static string UpdateBasket() => "/api/v1/basket";
    }

    public class OrdersOperations
    {
        public static string GetOrderDraft() => "/api/v1/orders/draft";
    }

    public string Basket { get; set; }

    public string Catalog { get; set; }

    public string Orders { get; set; }

    public string GrpcBasket { get; set; }

    public string GrpcCatalog { get; set; }

    public string GrpcOrdering { get; set; }
}

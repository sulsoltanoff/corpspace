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

namespace Corpspace.Services.Identity.API.Configuration;

public static class Config
{
    // ApiResources define the apis in your system
    public static IEnumerable<ApiResource> GetApis()
    {
        return new List<ApiResource>
        {
            new("mobile-agg", "Mobile Aggregator"),
            new("web-agg", "Web Aggregator"),
            new("webhooks", "Webhooks registration Service"),
        };
    }

    // ApiScope is used to protect the API 
    //The effect is the same as that of API resources in IdentityServer 3.x
    public static IEnumerable<ApiScope> GetApiScopes()
    {
        return new List<ApiScope>
        {
            new("mobile-agg", "Mobile Aggregator"),
            new("web-agg", "Web Aggregator"),
            new("webhooks", "Webhooks registration Service"),
        };
    }

    // Identity resources are data like user ID, name, or email address of a user
    // see: http://docs.identityserver.io/en/release/configuration/resources.html
    public static IEnumerable<IdentityResource> GetResources()
    {
        return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
    }

    // client want to access resources (aka scopes)
    public static IEnumerable<Client> GetClients(IConfiguration configuration)
    {
        return new List<Client>
        {
            // JavaScript Client
            new()
            {
                ClientId = "angular",
                ClientName = "Corpspace Angular OpenId Client",
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowAccessTokensViaBrowser = true,
                RedirectUris =           { $"{configuration["SpaClient"]}/" },
                RequireConsent = false,
                PostLogoutRedirectUris = { $"{configuration["SpaClient"]}/" },
                AllowedCorsOrigins =     { $"{configuration["SpaClient"]}" },
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "web-agg",
                    "webhooks"
                },
            },
            new()
            {
                ClientId = "webhooks-client",
                ClientName = "Webhooks Client",
                ClientSecrets = new List<Secret>
                {
                    new("secret".Sha256())
                },
                ClientUri = $"{configuration["WebhooksWebClient"]}",                             // public uri of the client
                AllowedGrantTypes = GrantTypes.Code,
                AllowAccessTokensViaBrowser = false,
                RequireConsent = false,
                AllowOfflineAccess = true,
                AlwaysIncludeUserClaimsInIdToken = true,
                RedirectUris = new List<string>
                {
                    $"{configuration["WebhooksWebClient"]}/signin-oidc"
                },
                PostLogoutRedirectUris = new List<string>
                {
                    $"{configuration["WebhooksWebClient"]}/signout-callback-oidc"
                },
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    "webhooks"
                },
                AccessTokenLifetime = 60*60*2, // 2 hours
                IdentityTokenLifetime= 60*60*2 // 2 hours
            },
            new()
            {
                ClientId = "mobile-agg-swagger-ui",
                ClientName = "Mobile Aggregator Swagger UI",
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowAccessTokensViaBrowser = true,

                RedirectUris = { $"{configuration["MobileAggClient"]}/swagger/oauth2-redirect.html" },
                PostLogoutRedirectUris = { $"{configuration["MobileAggClient"]}/swagger/" },

                AllowedScopes =
                {
                    "mobile-agg"
                }
            },
            new()
            {
                ClientId = "web-agg-swagger-ui",
                ClientName = "Web Aggregator Swagger UI",
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowAccessTokensViaBrowser = true,

                RedirectUris = { $"{configuration["WebShoppingAggClient"]}/swagger/oauth2-redirect.html" },
                PostLogoutRedirectUris = { $"{configuration["WebShoppingAggClient"]}/swagger/" },

                AllowedScopes =
                {
                    "web-agg",
                    "basket"
                }
            },
            new()
            {
                ClientId = "webhooks-swagger-ui",
                ClientName = "WebHooks Service Swagger UI",
                AllowedGrantTypes = GrantTypes.Implicit,
                AllowAccessTokensViaBrowser = true,

                RedirectUris = { $"{configuration["WebhooksApiClient"]}/swagger/oauth2-redirect.html" },
                PostLogoutRedirectUris = { $"{configuration["WebhooksApiClient"]}/swagger/" },

                AllowedScopes =
                {
                    "webhooks"
                }
            }
        };
    }
}
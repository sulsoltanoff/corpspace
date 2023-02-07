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

using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Corpspace.WebSPA.Server.Infrastructure;

public class WebContextSeed
{
    public static void Seed(IApplicationBuilder applicationBuilder, IWebHostEnvironment env, ILoggerFactory loggerFactory)
    {
        var log = loggerFactory.CreateLogger<WebContextSeed>();

        var settings = applicationBuilder
            .ApplicationServices.GetRequiredService<IOptions<AppSettings>>().Value;

        var useCustomizationData = settings.UseCustomizationData;
        var contentRootPath = env.ContentRootPath;
        var webroot = env.WebRootPath;

        if (useCustomizationData)
        {
            GetPreconfiguredImages(contentRootPath, webroot, log);
        }
    }

    private static void GetPreconfiguredImages(string contentRootPath, string webroot, ILogger log)
    {
        try
        {
            var imagesZipFile = Path.Combine(contentRootPath, "Setup", "images.zip");
            if (!File.Exists(imagesZipFile))
            {
                log.LogError("Zip file '{ZipFileName}' does not exists.", imagesZipFile);
                return;
            }

            var imagePath = Path.Combine(webroot, "assets", "images");
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }
            var imageFiles = Directory.GetFiles(imagePath).Select(file => Path.GetFileName(file)).ToArray();

            using var zip = ZipFile.Open(imagesZipFile, ZipArchiveMode.Read);
            foreach (var entry in zip.Entries)
            {
                if (!imageFiles.Contains(entry.Name))
                {
                    var destinationFilename = Path.Combine(imagePath, entry.Name);
                    if (File.Exists(destinationFilename))
                    {
                        File.Delete(destinationFilename);
                    }
                    entry.ExtractToFile(destinationFilename);
                }
                else
                {
                    log.LogWarning("Skipped file '{FileName}' in zipfile '{ZipFileName}'", entry.Name, imagesZipFile);
                }
            }
        }
        catch (Exception ex)
        {
            log.LogError(ex, "ERROR in GetPreconfiguredImages: {Message}", ex.Message);
        }
    }
}

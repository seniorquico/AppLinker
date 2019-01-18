using System;
using System.IO.Compression;
using AppLinker.Api.BrowserCapabilities;
using AppLinker.Api.Services;
using BrowscapNet;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AppLinker
{
    public static class BrowserCapabilitiesBuilderExtensions
    {
        public static IAppLinkerBuilder AddBrowserCapabilitiesPlatformResolver(this IAppLinkerBuilder builder, BrowserCapabilitiesLevel level = BrowserCapabilitiesLevel.Full)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            // Load browscap.ini and add service to dependency injection container.
            var assembly = typeof(BrowserCapabilitiesBuilderExtensions).Assembly;

            string resourceName;
            switch (level)
            {
                case BrowserCapabilitiesLevel.Lite:
                    resourceName = "AppLinker.Api.BrowserCapabilities.Data.lite_asp_browscap.ini.gz";
                    break;

                case BrowserCapabilitiesLevel.Standard:
                    resourceName = "AppLinker.Api.BrowserCapabilities.Data.browscap.ini.gz";
                    break;

                default:
                    resourceName = "AppLinker.Api.BrowserCapabilities.Data.full_asp_browscap.ini.gz";
                    break;
            }

            var service = new BrowserCapabilitiesService();
            using (var resourceStream = assembly.GetManifestResourceStream(resourceName))
            using (var gzipStream = new GZipStream(resourceStream, CompressionMode.Decompress))
            {
                service.LoadBrowscap(gzipStream);
            }

            builder.Services.TryAddSingleton<IBrowserCapabilitiesService>(service);
            builder.Services.TryAddSingleton<IPlatformResolver, BrowserCapabilitiesPlatformResolver>();

            return builder;
        }
    }
}

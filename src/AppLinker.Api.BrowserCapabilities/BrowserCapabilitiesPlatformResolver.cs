using System;
using System.Linq;
using AppLinker.Api.Models;
using AppLinker.Api.Services;
using BrowscapNet;
using Microsoft.AspNetCore.Http;

namespace AppLinker.Api.BrowserCapabilities
{
    public class BrowserCapabilitiesPlatformResolver : IPlatformResolver
    {
        /// <summary>The browser capabilities service.</summary>
        private readonly IBrowserCapabilitiesService service;

        /// <summary>
        ///     Initializes a new instance of the <see cref="BrowserCapabilitiesPlatformResolver"/> class.
        /// </summary>
        /// <param name="service">The browser capabilities service.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="service"/> is <c>null</c>.</exception>
        public BrowserCapabilitiesPlatformResolver(IBrowserCapabilitiesService service) =>
            this.service = service ?? throw new ArgumentNullException(nameof(service));

        /// <summary>Resolves the platform of the user agent of the specified HTTP request.</summary>
        /// <param name="request">The HTTP request.</param>
        /// <returns>The platform.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="request"/> is <c>null</c>.</exception>
        public Platform ResolvePlatform(HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var userAgent = request.Headers["User-Agent"].FirstOrDefault();
            if (!string.IsNullOrEmpty(userAgent))
            {
                var info = this.service.Find(userAgent);
                if (info != null)
                {
                    if ("Android".Equals(info.Platform, StringComparison.OrdinalIgnoreCase))
                    {
                        return Platform.Android;
                    }

                    if ("iOS".Equals(info.Platform, StringComparison.OrdinalIgnoreCase))
                    {
                        return Platform.iOS;
                    }
                }
            }

            return Platform.Unknown;
        }
    }
}

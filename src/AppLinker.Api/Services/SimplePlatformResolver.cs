using System;
using System.Linq;
using AppLinker.Api.Models;
using Microsoft.AspNetCore.Http;

namespace AppLinker.Api.Services
{
    public class SimplePlatformResolver : IPlatformResolver
    {
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
                if (userAgent.Contains("Android", StringComparison.OrdinalIgnoreCase))
                {
                    return Platform.Android;
                }

                if (userAgent.Contains("iOS", StringComparison.OrdinalIgnoreCase))
                {
                    return Platform.iOS;
                }
            }

            return Platform.Unknown;
        }
    }
}

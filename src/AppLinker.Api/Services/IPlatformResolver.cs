using System;
using AppLinker.Api.Models;
using Microsoft.AspNetCore.Http;

namespace AppLinker.Api.Services
{
    /// <summary>Represents a service that resolves the platform of the user agent making a HTTP request.</summary>
    public interface IPlatformResolver
    {
        /// <summary>Resolves the platform of the user agent of the specified HTTP request.</summary>
        /// <param name="request">The HTTP request.</param>
        /// <returns>The platform.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="request"/> is <c>null</c>.</exception>
        /// <remarks>
        ///     Generally, implementations will inspect the <c>User-Agent</c> header to determine the platform. Some
        ///     custom applications may have more accurate means to determine the platform (e.g. inspecting the user
        ///     claims of an authenticated request). However, implementors should bear in mind that Android and iOS
        ///     always trigger the initial association request unauthenticated. As such, implementors should consider
        ///     extending one of the built-in resolvers and falling back to the <c>User-Agent</c> header if other means
        ///     are unavailable on a particular request.
        /// </remarks>
        Platform ResolvePlatform(HttpRequest request);
    }
}

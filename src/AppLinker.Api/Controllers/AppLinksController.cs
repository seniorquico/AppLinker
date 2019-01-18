using System;
using System.Net;
using AppLinker.Api.Models;
using AppLinker.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AppLinker.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    public sealed class AppLinksController : ControllerBase
    {
        /// <summary>The service options.</summary>
        private readonly AppLinkerOptions options;

        /// <summary>The service that resolves the platform of the user agent making a HTTP request.</summary>
        private readonly IPlatformResolver platformResolver;

        /// <summary>Initializes a new instance of the <see cref="AppLinksController"/> class.</summary>
        /// <param name="optionsRetriever">The service options retriever.</param>
        /// <param name="platformResolver">
        ///     The service that resolves the platform of the user agent making a HTTP request.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="optionsRetriever"/> or <paramref name="platformResolver"/> are <c>null</c>.
        /// </exception>
        public AppLinksController(IOptions<AppLinkerOptions> optionsRetriever, IPlatformResolver platformResolver)
        {
            if (optionsRetriever == null)
            {
                throw new ArgumentNullException(nameof(optionsRetriever));
            }

            this.options = optionsRetriever.Value;
            if (this.options == null)
            {
                throw new ArgumentException("The App Linker service options must not be null.", nameof(optionsRetriever));
            }

            this.platformResolver = platformResolver ?? throw new ArgumentNullException(nameof(platformResolver));
        }

        /// <summary>
        ///     Gets a redirect to the appropriate app store for mobile user agents or gets a landing page for non-mobile
        ///     user agents.
        /// </summary>
        /// <response code="200">
        ///     If the request is successful. Returns the list of promotions in the response body.
        /// </response>
        [HttpGet("{*path}", Order = int.MaxValue)]
        [ProducesDefaultResponseType(typeof(void))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status307TemporaryRedirect)]
        public ActionResult<string> Get()
        {
            switch (this.platformResolver.ResolvePlatform(this.Request))
            {
                case Platform.Android:
                    var packageName = this.options.Android?.PackageName;
                    if (!string.IsNullOrEmpty(packageName))
                    {
                        return this.RedirectPreserveMethod($"https://play.google.com/store/apps/details?id={WebUtility.UrlEncode(packageName)}");
                    }

                    goto default;
                case Platform.iOS:
                    var bundleId = this.options.iOS?.BundleId;
                    if (!string.IsNullOrEmpty(bundleId))
                    {
                        return this.RedirectPreserveMethod("https://itunes.apple.com/us/app/clash-of-clans/id529479190?mt=8");
                    }

                    goto default;
                default:
                    return this.Ok("I'll show you a landing page!");
            }
        }
    }
}

using System;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AppLinker.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    public sealed class AppLinkAssociationsController : ControllerBase
    {
        private readonly string androidFile;
        private readonly string iosFile;

        /// <summary>Initializes a new instance of the <see cref="AppLinkAssociationsController"/> class.</summary>
        /// <param name="optionsRetriever">The service options retriever.</param>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="optionsRetriever"/> is <c>null</c>.
        /// </exception>
        public AppLinkAssociationsController(IOptions<AppLinkerOptions> optionsRetriever)
        {
            if (optionsRetriever == null)
            {
                throw new ArgumentNullException(nameof(optionsRetriever));
            }

            var options = optionsRetriever.Value;
            if (options == null)
            {
                throw new ArgumentException("The App Linker service options must not be null.", nameof(optionsRetriever));
            }

            this.androidFile = new JArray
            {
                new JObject
                {
                    { "relation", new JArray { "delegate_permission/common.handle_all_urls" } },
                    { "target", new JObject
                    {
                        { "namespace", "android_app" },
                        { "package_name", options.Android.PackageName },
                        { "sha256_cert_fingerprints", new JArray { options.Android.CertificateFingerprint } },
                    }
                    },
                },
            }.ToString(Formatting.None);

            this.iosFile = new JObject
            {
                { "applinks", new JObject
                {
                    { "apps", new JArray() },
                    { "details", new JArray
                    {
                        new JObject
                        {
                            { "appID", "24QXGCES3E.com.cheesypixel.slippy" },
                            { "paths", new JArray { "*" } },
                        },
                    }
                    },
                }
                },
            }.ToString(Formatting.None);
        }

        [HttpGet("/.well-known/apple-app-site-association")]
        [Produces("application/json")]
        public IActionResult GetAppSiteAssociation() =>
            this.iosFile == null
                ? (IActionResult)this.NotFound()
                : this.Content(this.iosFile, "application/json", Encoding.UTF8);

        [HttpGet("/.well-known/assetlinks.json")]
        [Produces("application/json")]
        public IActionResult GetDigitalAssetLinks() =>
            this.androidFile == null
                ? (IActionResult)this.NotFound()
                : this.Content(this.androidFile, "application/json", Encoding.UTF8);
    }
}

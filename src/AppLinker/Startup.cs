using System;
using AppLinker.Api;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace AppLinker
{
    /// <summary>Configures a web application to run the AppLinker services.</summary>
    public class Startup
    {
        /// <summary>The application configuration.</summary>
        private readonly IConfiguration configuration;

        /// <summary>The web hosting environment.</summary>
        private readonly IHostingEnvironment environment;

        /// <summary>Initializes a new instance of the <see cref="Startup"/> class.</summary>
        /// <param name="configuration">The application configuration.</param>
        /// <param name="environment">The web hosting environment.</param>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="configuration"/> or <paramref name="environment"/> are <c>null</c>.
        /// </exception>
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        /// <summary>Called by the runtime to configure the web application.</summary>
        /// <param name="app">The web application builder.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="app"/> is <c>null</c>.</exception>
        public void Configure(IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            // Adds middleware to the pipeline that filters requests based on the Host header.
            app.UseHostFiltering();

            // Adds middleware to the pipeline that transforms requests based on the X-Forwarded-* headers.
            app.UseForwardedHeaders();

            // Adds middleware to the pipeline that serves developer-focused error pages on uncaught exceptions.
            if (this.environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Adds middleware to the pipeline that injects HSTS headers into responses.
            if (!this.environment.IsDevelopment())
            {
                app.UseHsts();
            }

            // Adds middleware to the pipeline that redirects non-HTTPS requests to the HTTPs equivalent.
            app.UseHttpsRedirection();

            // Adds middleware to the pipeline that handles MVC/Web API routes.
            app.UseMvc();
        }

        /// <summary>Called by the runtime to configure the dependency injection container.</summary>
        /// <param name="configuration">The application configuration.</param>
        /// <param name="services">The dependency injection container.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="services"/> is <c>null</c>.</exception>
        public void ConfigureServices(IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddHsts(options =>
            {
                options.MaxAge = TimeSpan.FromDays(180);
            });

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    options.UseCamelCasing(true);
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                })
                .AddAppLinker(options =>
                {
                    options.Android = new GooglePlayApp
                    {
                        CertificateFingerprint = "14:6D:E9:83:C5:73:06:50:D8:EE:B9:95:2F:34:FC:64:16:A0:83:42:E6:1D:BE:A8:8A:04:96:B2:3F:CF:44:E5",
                        PackageName = "com.cheesypixel.slippy",
                    };
                    options.iOS = new AppleAppStoreApp
                    {
                        BundleId = "com.cheesypixel.slippy",
                        TeamId = "ABCDEFGHIJ",
                    };
                });
        }
    }
}

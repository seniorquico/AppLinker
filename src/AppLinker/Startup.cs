using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace AppLinker
{
    /// <summary>Configures a web application to run the AppLinker services.</summary>
    public class Startup
    {
        /// <summary>Called by the runtime to configure the web application.</summary>
        /// <param name="app">The web application builder.</param>
        /// <param name="env">The web hosting environment.</param>
        /// <exception cref="ArgumentNullException">
        ///     If <paramref name="app"/> or <paramref name="env"/> are <c>null</c>.
        /// </exception>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (env == null)
            {
                throw new ArgumentNullException(nameof(env));
            }

            // Adds middleware to the pipeline that filters requests based on the Host header.
            app.UseHostFiltering();

            // Adds middleware to the pipeline that transforms requests based on the X-Forwarded-* headers.
            app.UseForwardedHeaders();

            // Adds middleware to the pipeline that serves developer-focused error pages on uncaught exceptions.
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Adds middleware to the pipeline that injects HSTS headers into responses.
            if (!env.IsDevelopment())
            {
                app.UseHsts();
            }

            // Adds middleware to the pipeline that redirects non-HTTPS requests to the HTTPs equivalent.
            app.UseHttpsRedirection();

            // Adds middleware to the pipeline that handles MVC/Web API routes.
            app.UseMvc();
        }

        /// <summary>Called by the runtime to configure the dependency injection container.</summary>
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
                .AddJsonOptions(options =>
                {
                    options.UseCamelCasing(true);
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
    }
}

using System;
using AppLinker.Api;
using AppLinker.Api.Services;
using Microsoft.Extensions.DependencyInjection;

namespace AppLinker
{
    public static class BuilderExtensions
    {
        public static IAppLinkerBuilder AddAppLinker(this IMvcBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            // Add application part to load controllers.
            var assembly = typeof(BuilderExtensions).Assembly;
            builder.AddApplicationPart(assembly);

            builder.Services.AddSingleton<IPlatformResolver, SimplePlatformResolver>();

            return new AppLinkerBuilder(builder);
        }

        public static IAppLinkerBuilder AddAppLinker(this IMvcBuilder builder, Action<AppLinkerOptions> setupAction)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }

            var appLinkerBuilder = builder.AddAppLinker();
            appLinkerBuilder.Services.Configure(setupAction);

            return appLinkerBuilder;
        }
    }
}

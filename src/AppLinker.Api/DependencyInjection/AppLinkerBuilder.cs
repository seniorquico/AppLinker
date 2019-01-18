using System;
using Microsoft.Extensions.DependencyInjection;

namespace AppLinker
{
    internal sealed class AppLinkerBuilder : IAppLinkerBuilder
    {
        private readonly IMvcBuilder builder;

        public AppLinkerBuilder(IMvcBuilder builder) =>
            this.builder = builder ?? throw new ArgumentNullException(nameof(builder));

        public IServiceCollection Services => this.builder.Services;
    }
}

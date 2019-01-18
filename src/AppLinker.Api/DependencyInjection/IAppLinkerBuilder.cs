using Microsoft.Extensions.DependencyInjection;

namespace AppLinker
{
    public interface IAppLinkerBuilder
    {
        IServiceCollection Services { get; }
    }
}

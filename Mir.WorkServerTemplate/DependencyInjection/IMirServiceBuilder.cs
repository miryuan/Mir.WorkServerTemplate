using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mir.WorkServer.DependencyInjection
{
    public interface IMirServiceBuilder
    {
        IServiceCollection Services { get; }

        IConfiguration Configuration { get; }
    }
}

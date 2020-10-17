using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mir.WorkServer.DependencyInjection
{
    public class MirServiceBuilder : IMirServiceBuilder
    {
        public IServiceCollection Services { get; private set; }
        public IConfiguration Configuration { get; private set; }
        public MirServiceBuilder(IServiceCollection services, IConfiguration configurationRoot)
        {
            Services = services;
            Configuration = configurationRoot;
        }
    }
}

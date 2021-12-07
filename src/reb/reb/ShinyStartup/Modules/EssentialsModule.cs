using Microsoft.Extensions.DependencyInjection;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;

namespace reb.ShinyStartup.Modules
{
    public class EssentialsModule : ShinyModule
    {
        public override void Register(IServiceCollection services)
        {
            services.AddSingleton<IFileSystem, FileSystemImplementation>();

        }
    }
}
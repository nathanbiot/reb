using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Prism.Ioc;
using Prism.Navigation;
using Shiny;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace reb
{
    public class Startup : FrameworkStartup
    {

        public override void ConfigureApp(Application app, IContainerRegistry containerRegistry)
        {
            // register your viewmodels and any services that are specific only to your UI
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
        }

        public override Task RunApp(INavigationService navigator)
        {
            // perform your inital navigation here
            return navigator.Navigate("/NavigationPage/MainPage");
        }

        // all of your prism & shiny registrations in one place
        protected override void Configure(ILoggingBuilder builder, IServiceCollection services)
        {
            
        }
    }
}

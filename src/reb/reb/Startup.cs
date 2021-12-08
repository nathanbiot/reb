using Prism.Ioc;
using reb.Logging;
using System.Reflection;

namespace reb;

public class Startup : FrameworkStartup
{
    public override Task RunApp(INavigationService navigator)
    {
        // perform your inital navigation here
        return navigator.Navigate($"/NavigationPage/{NavigationKeys.MotorList}");
    }

    // all of your prism & shiny registrations in one place
    protected override void Configure(ILoggingBuilder builder, IServiceCollection services)
    {
        builder.RegisterLoggers();       

        services.UseResxLocalization(this.GetType().Assembly, "reb.Resources.Text.Strings");
        services.UseXfMaterialDialogs();

        // command exception handling - writes to your error log & displays to a dialog, depending on how you configure below
        services.UseGlobalCommandExceptionHandler(cfg =>
        {
            cfg.LogError = true;
            cfg.IgnoreTokenCancellations = true; // these are generally page cancellations or http timeouts - you generally want to ignore task cancellation exceptions
#if DEBUG
                cfg.AlertType = ErrorAlertType.FullError; // show us the entire error in our alert
#else
                // this will pull out of you ILocalize which is registered with UseResxLocatization below
                cfg.LocalizeErrorBodyKey = "ErrorBody";
                cfg.LocalizeErrorTitleKey = "ErrorTitle";
                cfg.AlertType = ErrorAlertType.Localize;
#endif            
            });

        services.RegisterModule<EssentialsModule>();
        services.RegisterModule<SettingsModule>();        
    }

    public override void ConfigureApp(Application app, IContainerRegistry containerRegistry)
    {
        // register your viewmodels and any services that are specific only to your UI
        containerRegistry.RegisterForNavigation<NavigationPage>();
        containerRegistry.RegisterForNavigation<MotorListPage, MotorListPageViewModel>(NavigationKeys.MotorList);
    }
}

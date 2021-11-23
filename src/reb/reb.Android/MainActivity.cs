using Android.App;
using Android.Content.PM;
using Shiny;

[assembly: ShinyApplication(XamarinFormsAppTypeName = "reb.App", ShinyStartupTypeName = "reb.Startup")]
namespace reb.Droid
{
    [Activity(Label = "reb", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public partial class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
    }
}
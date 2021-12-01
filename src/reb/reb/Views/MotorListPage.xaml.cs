using Shiny;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace reb.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MotorListPage : ContentPage
    {
        public MotorListPage()
        {
            this.InitializeComponent();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            (this.BindingContext as ViewModel)?.OnAppearing();
        }


        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            (this.BindingContext as ViewModel)?.OnDisappearing();
        }
    }
}
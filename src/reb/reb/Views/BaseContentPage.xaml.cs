namespace reb.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class BaseContentPage : ContentPage
{
    public BaseContentPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ViewModel vm)
            vm.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        if (BindingContext is ViewModel vm)
            vm.OnDisappearing();
    }
}

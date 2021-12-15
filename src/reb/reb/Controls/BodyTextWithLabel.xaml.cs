namespace reb.Controls;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class BodyTextWithLabel : StackLayout
{
    public BodyTextWithLabel()
    {
        InitializeComponent();
    }


    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(BodyTextWithLabel), null, propertyChanged: OnTextChanged);

    private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not BodyTextWithLabel view)
            return;

        view.TextLabel.Text = (string)newValue;
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }



    public static readonly BindableProperty LabelProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(BodyTextWithLabel), null, propertyChanged: OnLabelChanged);

    private static void OnLabelChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is not BodyTextWithLabel view)
            return;

        view.LabelLabel.Text = (string)newValue;
    }

    public string Label
    {
        get => (string)GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }
}

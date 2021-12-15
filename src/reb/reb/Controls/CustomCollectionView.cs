namespace reb.Controls;

internal class CustomCollectionView : CollectionView, IDisposable
{
    public CustomCollectionView()
    {
        SelectionChanged += CustomCollectionView_SelectionChanged;
    }    

    private void CustomCollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (sender is not CollectionView collectionView)
            return;

        collectionView.SelectedItem = null;
    }

    public void Dispose()
    {
        SelectionChanged -= CustomCollectionView_SelectionChanged;
    }

    public static readonly BindableProperty ResetSelectedItemAfterSelectionChangedProperty =
       BindableProperty.Create(nameof(ResetSelectedItemAfterSelectionChanged), typeof(bool), typeof(CustomCollectionView), true);

    public bool ResetSelectedItemAfterSelectionChanged
    {
        get => (bool)GetValue(ResetSelectedItemAfterSelectionChangedProperty);
        set => SetValue(ResetSelectedItemAfterSelectionChangedProperty, value);
    }

}

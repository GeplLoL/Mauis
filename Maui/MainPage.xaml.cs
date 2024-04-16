namespace Maui;

public partial class MainPage : ContentPage
{
    string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    string fileName;
    Label editText;

    private string filename;

    protected override void OnAppearing()
    {
        base.OnAppearing();
        UpdateFilesList();
    }
    private void UpdateFilesList()
    {
        FilesList.ItemsSource = Directory.GetFiles(folderPath).Select(f => Path.GetFileName(f));
        FilesList.SelectedItem = null;
    }

    private void ToList_Clicked(object sender, EventArgs e)
    {
        fileName = (string)((MenuItem)sender).BindingContext;
        List<string> list = File.ReadLines(Path.Combine(folderPath, fileName)).ToList();
        listFailist.ItemsSource = list;
    }

    private void FilesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null)
        {
            return;
        }
        fileName = e.SelectedItem.ToString();
        editText.Text = File.ReadAllText(Path.Combine(folderPath, fileName));
        FilesList.SelectedItem = null;
    }

    public MainPage()
    {
        CarouselView carousel = new CarouselView
        {
            VerticalOptions = LayoutOptions.Center,
        };
        carousel.ItemsSource = new List<Product>
        {
            new Product{Name=filename, Image="dotnet_bot.svg"},
            new Product{Name="dis2", Image="dotnet_bot.svg"},
            new Product{Name="dis3", Image="dotnet_bot.svg"},
        };
        carousel.ItemTemplate = new DataTemplate(() =>
        {
            Label header = new Label
            {

            };
            header.SetBinding(Label.TextProperty, "Name");
            Image image = new Image { WidthRequest = 200, HeightRequest = 200 };
            image.SetBinding(Image.SourceProperty, "Image");
            StackLayout stackLayout = new StackLayout() { Children = { header, image } };
            Frame frame = new Frame();
            frame.Content = stackLayout;
            return frame;
        });
        Content = new StackLayout
        {
            Children = { carousel, FilesList, new Label { Text = "Faili sisu loendus:", FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)) }, listFailist }
        };

        editText = new Label();
        FilesList = new ListView();
        listFailist = new ListView();
    }

}

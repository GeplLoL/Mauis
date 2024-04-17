namespace Maui;

public partial class MainPage : ContentPage
{
    string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
    string fileName;
    string fileContent;
    Label editText;

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
        fileContent = File.ReadAllText(Path.Combine(folderPath, fileName));
        listFailist.ItemsSource = File.ReadLines(Path.Combine(folderPath, fileName)).ToList();
    }

    private void FilesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null)
        {
            return;
        }
        fileName = e.SelectedItem.ToString();
        fileContent = File.ReadAllText(Path.Combine(folderPath, fileName));
        editText.Text = fileContent;
        FilesList.SelectedItem = null;
    }

    public MainPage()
    {
        CarouselView carousel = new CarouselView
        {
            VerticalOptions = LayoutOptions.Center,
        };

        List<string> fileNames = Directory.GetFiles(folderPath).Select(f => Path.GetFileName(f)).ToList();

        carousel.ItemsSource = fileNames.Select(name => new { Name = name, Content = File.ReadAllText(Path.Combine(folderPath, name)) });

        carousel.ItemTemplate = new DataTemplate(() =>
        {
            Label header = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label))
            };
            header.SetBinding(Label.TextProperty, "Name");

            Label contentLabel = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
            };
            contentLabel.SetBinding(Label.TextProperty, new Binding("Content"));

            StackLayout stackLayout = new StackLayout()
            {
                Children = { header, contentLabel }
            };

            Frame frame = new Frame
            {
                CornerRadius = 10,
                Margin = 20,
                WidthRequest = 300,
                HeightRequest = 400,
                Content = stackLayout,
                BackgroundColor = Colors.LightBlue,
            };

            return frame;
        });

        editText = new Label();
        FilesList = new ListView();
        listFailist = new ListView();

        Content = new StackLayout
        {
            Children = { carousel }
        };
    }
}
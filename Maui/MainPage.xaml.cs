namespace Maui;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        CarouselView carousel = new CarouselView
		{
			VerticalOptions = LayoutOptions.Center,
		};
		carousel.ItemsSource = new List<Product>
		{
			new Product{Name="dis1", Image="dotnet_bot.svg"},
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
			StackLayout stackLayout = new StackLayout() { header, image };
			Frame frame = new Frame();
			frame.Content = stackLayout;
			return frame;
		});
		Content = carousel;
    }


}
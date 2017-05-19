using cardkeeper.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cardkeeper.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
        MainViewModel _viewModel;
		public MainPage ()
        { 
            InitializeComponent();
            _viewModel = new MainViewModel();
            Content = LayoutMainPage();
            _viewModel.Navigation = Navigation;
            BindingContext = _viewModel;
        }
        public View LayoutMainPage()
        {
            var layout = new StackLayout();


            var header = new Grid() { ColumnSpacing = 10, RowSpacing = 10 };
            header.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            header.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            header.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            header.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            header.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            header.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            header.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            header.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });


            var logo = new Image
            {
                Source = "cklogo.png",
                Aspect = Aspect.Fill
            };
            var weather = new Label
            {
                Text = "Weather",
                HorizontalTextAlignment = TextAlignment.End,
                VerticalTextAlignment = TextAlignment.Center,
                TextColor = Color.White,
            };

            header.BackgroundColor = Color.FromHex("#2a2a2a");


            header.Children.Add(logo, 0, 0);
            header.Children.Add(weather, 4, 3);
            Grid.SetColumnSpan(logo, 3);
            Grid.SetRowSpan(logo, 4);

            var viewCards = new Button
            {
                Text = "VIEW CARDS",
                FontSize = 30,
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BorderColor = Color.Black,
                BorderRadius = 5,
                BorderWidth = 2,
                BackgroundColor = Color.Blue,
                Command = _viewModel.ViewCardsCommand,
            };

            var addCards = new Button
            {
                Text = "ADD CARDS",
                FontSize = 30,
                FontAttributes = FontAttributes.Bold,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BorderColor = Color.Black,
                BorderRadius = 5,
                BorderWidth = 2,
                BackgroundColor = Color.Green,
                Command = _viewModel.AddCardCommand
            };

            layout.Children.Add(header);
            layout.Children.Add(viewCards);
            layout.Children.Add(addCards);


            return layout;
        }
    }
}
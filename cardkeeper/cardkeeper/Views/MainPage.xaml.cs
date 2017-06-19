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
            var submitItem = new ToolbarItem
            {
                Text = "About",
                Command = _viewModel.AboutCommand,
            };
            this.ToolbarItems.Add(submitItem);

            var layout = new StackLayout()
            {
                BackgroundColor = Color.FromHex("#2a2a2a"),
            };


            var header = new Grid() { ColumnSpacing = 10, RowSpacing = 10 };
            header.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            header.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            header.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            header.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            header.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            header.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            header.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });


            var logo = new Image
            {
                Source = "cklogo.png",
                Aspect = Aspect.Fill
            };

            Label keeperLabel = new Label()
            {
                Text = "Card Keeper",
                FontSize = 25,
                TextColor = Color.FromHex("#cc9933"),
                FontAttributes = FontAttributes.Bold,
                HorizontalTextAlignment = TextAlignment.Center,
                Margin = new Thickness(-10, 0, 0, 0)
            };
            var weather = new Label
            {
                Text = "",
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.White,
            };

            header.Children.Add(logo, 0, 0);
            header.Children.Add(keeperLabel, 2, 1);
            header.Children.Add(weather, 3, 0);
            Grid.SetColumnSpan(keeperLabel, 3);
            Grid.SetColumnSpan(logo, 2);
            Grid.SetRowSpan(logo, 2);

            var giftCardButton = new Button
            {
                Text = "Gift",
                FontSize = 30,
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = Color.FromHex("#cc9933"),
                Command = _viewModel.GiftCardsCommand,
            };
            var giftCardFrame = new Frame()
            {
                Content = giftCardButton,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HasShadow = true,
                BackgroundColor = Color.FromHex("#d8d8d8"),
                CornerRadius = 10,
                Padding = 1,
                Margin = new Thickness(20, 10),
            };
            var loyaltyCardButton = new Button
            {
                Text = "Loyalty",
                FontSize = 30,
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = Color.FromHex("#cc9933"),
                Command = _viewModel.LoyaltyCardsCommand,
            };
            var loyaltyCardFrame = new Frame()
            {
                Content = loyaltyCardButton,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HasShadow = true,
                BackgroundColor = Color.FromHex("#d8d8d8"),
                CornerRadius = 10,
                Padding = 1,
                Margin = new Thickness(20, 10),
            };
            var membershipCardButton = new Button
            {
                Text = "Membership",
                FontSize = 30,
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = Color.FromHex("#cc9933"),
                Command = _viewModel.MembershipCardsCommand,
            };
            var membershipCardFrame = new Frame()
            {
                Content = membershipCardButton,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HasShadow = true,
                BackgroundColor = Color.FromHex("#d8d8d8"),
                CornerRadius = 10,
                Padding = 1,
                Margin = new Thickness(20, 10),
            };
            var otherCardButton = new Button
            {
                Text = "Other",
                FontSize = 30,
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = Color.FromHex("#cc9933"),
                Command = _viewModel.OtherCardsCommand,
            };
            var otherCardFrame = new Frame()
            {
                Content = otherCardButton,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HasShadow = true,
                BackgroundColor = Color.FromHex("#d8d8d8"),
                CornerRadius = 10,
                Padding = 1,
                Margin = new Thickness(20, 10),
            };

            layout.Children.Add(header);
            layout.Children.Add(giftCardFrame);
            layout.Children.Add(loyaltyCardFrame);
            layout.Children.Add(membershipCardFrame);
            layout.Children.Add(otherCardFrame);

            return layout;
        }
    }
}
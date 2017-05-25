
using cardkeeper.Helpers;
using cardkeeper.Models;
using cardkeeper.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cardkeeper.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CardDetailPage : ContentPage
	{
        CardDetailViewModel _viewModel;
		public CardDetailPage (Card card)
		{
			InitializeComponent ();
            _viewModel = new CardDetailViewModel(card);
            BindingContext = _viewModel;
            Content = LayoutCardDetailPage();
            _viewModel.Navigation = Navigation;
        }
        public View LayoutCardDetailPage()
        {
            Grid pageLayout = new Grid()
            {
                BackgroundColor = Color.FromHex("#2a2a2a"),
            };
            pageLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            pageLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            pageLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            pageLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            pageLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            pageLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            pageLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            pageLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            pageLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            pageLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            pageLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            pageLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            
            // Account Row Layout
            var accountRow = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
            };
            var accountLabel = new Label
            {
                Text = "Account:",
                FontSize = 12,
                TextColor = Color.White,
                Margin = new Thickness(0, 0, 0, -15)
            };
            var accountNumber = new Label
            {
                Text = $"{_viewModel.Card.AccountNumber}",
                FontSize = 30,
                TextColor = Color.White,
            };
            accountRow.Children.Add(accountLabel);
            accountRow.Children.Add(accountNumber);

            var applyPurchase = new Button()
            {
                Text = "Apply Purchase",
                Command = _viewModel.ApplyPurchaseButtonCommand,
            };
            var balance = new Label
            {
                FontSize = 30,
                TextColor = Color.White,
            };
            balance.SetBinding(Label.TextProperty, "DisplayThisBalance");

            if (_viewModel.Card.Type != "Gift")
            {
                applyPurchase.IsVisible = false;
                balance.IsVisible = false;
            }

            // Other Elements
            var flipLabel = new Label()
            {
                Text = "Tap card to flip.",
                FontSize = 12,
                TextColor = Color.White,
                Margin = new Thickness(0, 0, 0, -15),
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.End,
            };
            var removeButton = new Button
            {
                Text = "Remove",
                Command = _viewModel.RemoveButtonCommand,
                HorizontalOptions = LayoutOptions.End,
            };
            pageLayout.Children.Add(balance, 0,0);
            pageLayout.Children.Add(applyPurchase, 3, 0);
            Grid.SetColumnSpan(balance, 5);
            Grid.SetColumnSpan(applyPurchase, 3);


            pageLayout.Children.Add(flipLabel,2,0);
            Grid.SetColumnSpan(flipLabel, 2);
            View thisCardLayout = CardLayout();
            pageLayout.Children.Add(thisCardLayout, 0,1);
            Grid.SetColumnSpan(thisCardLayout, 6);
            Grid.SetRowSpan(thisCardLayout, 4);


            pageLayout.Children.Add(accountRow, 0, 5);
            Grid.SetColumnSpan(accountRow, 3);

            pageLayout.Children.Add(removeButton, 4,7);
            Grid.SetColumnSpan(removeButton, 2);



            return pageLayout;

        }
        private View CardLayout()
        {
            int cardTaps = 0;         
            Image detailCard = new Image()
            {
                Source = _viewModel.Card.DisplayFrontImage,
                Aspect = Aspect.Fill,
            };
            var tapGestureOnImage = new TapGestureRecognizer();
            tapGestureOnImage.Tapped += (s, e) =>
            {
                cardTaps++;
                if (cardTaps % 2 == 0)
                {
                    detailCard.FadeTo(0, 250);
                    detailCard.Source = _viewModel.Card.DisplayFrontImage;
                    detailCard.FadeTo(1, 250);
                }
                else
                {
                    detailCard.FadeTo(0, 250);
                    detailCard.Source = Converter.ByteToImage(_viewModel.Card.QRCode);
                    detailCard.FadeTo(1, 250);
                }
            };
            detailCard.GestureRecognizers.Add(tapGestureOnImage);

            var frame = new Frame()
            {
                Content = detailCard,
                OutlineColor = Color.Black,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HasShadow = true,
                BackgroundColor = Color.White,
                CornerRadius = 20,
                Padding = -10,
                Margin = new Thickness(10, 10),
            };
            var tapGestureOnFrame = new TapGestureRecognizer();
            tapGestureOnFrame.Tapped += (s, e) =>
            {
                if (cardTaps % 2 == 0)
                {
                    frame.Padding = -10;
                }
                else
                {
                    frame.Padding = new Thickness(90, 40);
                }
            };
            detailCard.GestureRecognizers.Add(tapGestureOnFrame);

            return frame;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadCardCommand.Execute(null);
        }
    }
}

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
                TextColor = Color.FromHex("#cc9933"),
                Margin = new Thickness(0, 0, 0, -15)
            };
            var accountNumber = new Label
            {
                Text = $"{_viewModel.Card.AccountNumber}",
                FontSize = 25,
                TextColor = Color.FromHex("#cc9933"),
            };
            accountRow.Children.Add(accountLabel);
            accountRow.Children.Add(accountNumber);

            if(_viewModel.Card.Label != null)
            {
                var labelRow = new StackLayout()
                {
                    Orientation = StackOrientation.Vertical,
                };
                var labelValue = new Label
                {
                    Text = $"{_viewModel.Card.Label}",
                    FontSize = 25,
                    TextColor = Color.FromHex("#cc9933"),
                };
                labelRow.Children.Add(labelValue);
                pageLayout.Children.Add(labelRow, 0, 6);
                Grid.SetColumnSpan(labelRow, 5);
            }
            

            var applyPurchase = new Button()
            {
                Text = "Apply Purchase",
                Command = _viewModel.ApplyPurchaseButtonCommand,
                BackgroundColor = Color.FromHex("#cc9933"),
                BorderColor = Color.FromHex("#d8d8d8"),
            };
            var balance = new Label
            {
                FontSize = 30,
                TextColor = Color.FromHex("#cc9933"),
            };
            balance.SetBinding(Label.TextProperty, "DisplayThisBalance");

            // Other Elements
            var flipLabel = new Label()
            {
                Text = "Tap card to flip.",
                FontSize = 12,
                TextColor = Color.FromHex("#cc9933"),
                Margin = new Thickness(0, 0, 0, -15),
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.End,
            };
            var removeButton = new Button
            {
                Text = "Remove",
                Command = _viewModel.RemoveButtonCommand,
                HorizontalOptions = LayoutOptions.End,
                BackgroundColor = Color.FromHex("#cc9933"),
                BorderColor = Color.FromHex("#d8d8d8"),
            };

            var pinButton = new Button
            {
                Text = "See PIN",
                Command = _viewModel.SeePinCommand,
                HorizontalOptions = LayoutOptions.Start,
                BackgroundColor = Color.FromHex("#cc9933"),
                BorderColor = Color.FromHex("#d8d8d8"),
            };
            if (_viewModel.Card.Type != "Gift")
            {
                applyPurchase.IsVisible = false;
                balance.IsVisible = false;
                pinButton.IsVisible = false;
            }
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
            Grid.SetColumnSpan(accountRow, 5);


            pageLayout.Children.Add(removeButton, 4,7);
            Grid.SetColumnSpan(removeButton, 2);
            pageLayout.Children.Add(pinButton, 0, 7);
            Grid.SetColumnSpan(pinButton, 2);

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
                    if (_viewModel.Card.Type == "Other")
                        detailCard.Source = Converter.ByteToImage(_viewModel.Card.BackImage);
                     
                    else
                    {
                        detailCard.Source = Converter.ByteToImage(_viewModel.Card.ScanCode);
                    }

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
                    detailCard.Aspect = Aspect.Fill;

                }
                else
                {
                    if (_viewModel.Card.Type == "Other")
                    {
                        frame.Padding = -10;
                        detailCard.Aspect = Aspect.Fill;
                    }
                    else
                    {
                        if (_viewModel.Card.IsQRCode)
                        {
                            detailCard.HeightRequest = 75;
                            detailCard.WidthRequest = 75;
                        }
                        else
                        {
                            detailCard.HeightRequest = 50;
                            detailCard.WidthRequest = 125;
                            frame.Padding = new Thickness(10, 0);
                        }
                        detailCard.Aspect = Aspect.AspectFit;
                    }

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
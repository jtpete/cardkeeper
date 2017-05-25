
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

            var pageLayout = new StackLayout()
            {
                BackgroundColor = Color.FromHex("#2a2a2a"),
            };
            

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
                Margin = new Thickness(0, 20, 0, -15)
            };
            var accountNumber = new Label
            {
                Text = $"{_viewModel.Card.AccountNumber}",
                FontSize = 30,
                TextColor = Color.White,
                Margin = new Thickness(0,0,0,25)
            };
            accountRow.Children.Add(accountLabel);
            accountRow.Children.Add(accountNumber);


            // Balance Row Layout
            var balanceRow = new Grid() { Margin = new Thickness(0,0,0,30)};
            balanceRow.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            balanceRow.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            balanceRow.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
       
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
            balanceRow.Children.Add(balance, 0, 0);
            balanceRow.Children.Add(applyPurchase, 1, 0);

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

            pageLayout.Children.Add(balanceRow);
            pageLayout.Children.Add(flipLabel);
            pageLayout.Children.Add(CardLayout());
            pageLayout.Children.Add(accountRow);
            pageLayout.Children.Add(removeButton);


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
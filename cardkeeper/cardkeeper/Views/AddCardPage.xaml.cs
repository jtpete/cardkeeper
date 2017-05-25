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
	public partial class AddCardPage : ContentPage
	{

        AddCardViewModel _viewModel;
        public AddCardPage (string cardType)
		{
			InitializeComponent ();
            _viewModel = new AddCardViewModel(cardType);
            Content = LayoutAddCardPage();
            _viewModel.Navigation = Navigation;
            BindingContext = _viewModel;
        }
        public View LayoutAddCardPage()
        {
            var layout = new StackLayout();


            // ACCOUNT NUMBER 
            var accountLabel = new Label
            {
                Text = "Account Number"
            };
            var accountNumber = new Entry
            {
                Text = "Account Number",
                Keyboard = Keyboard.Numeric,
            };
            accountNumber.SetBinding(Entry.TextProperty, new Binding(path: "AccountNumber", source: _viewModel.Card));
            layout.Children.Add(accountLabel);
            layout.Children.Add(accountNumber);

            // BALANCE
            if(_viewModel.CardType == "Gift")
            {
                var balanceLabel = new Label
                {
                    Text = "Card Balance"
                };
                var balance = new Entry
                {
                    Text = "Card Balance",
                    Keyboard = Keyboard.Numeric,
                };
                balance.SetBinding(Entry.TextProperty, new Binding(path: "Balance", source: _viewModel.Balance));
                layout.Children.Add(balanceLabel);
                layout.Children.Add(balance);
            }
            Grid frontPhotoLayout = new Grid();
            frontPhotoLayout.Padding = 10;
            frontPhotoLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(125) });
            frontPhotoLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            frontPhotoLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(175) });

            Frame imageHolder = new Frame()
            {
                Padding = -10,
                CornerRadius = 20,
                HasShadow = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            Image frontOfCard = new Image()
            {
                Aspect = Aspect.AspectFill
            };
            frontOfCard.SetBinding(Image.SourceProperty, "DisplayFrontImage");
            imageHolder.Content = frontOfCard;
            Button frontPhotoButton = new Button()
            {
                Text = "Take Photo",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Command = _viewModel.TakeFrontCardPhoto
            };
            frontPhotoLayout.Children.Add(frontPhotoButton,0,0);
            frontPhotoLayout.Children.Add(imageHolder, 1,0);
            Grid.SetColumnSpan(imageHolder, 2);

            Label takePhoto = new Label()
            {
                Text = "Take Front Of Card Photo:"
            };
            layout.Children.Add(takePhoto);
            layout.Children.Add(frontPhotoLayout);


            if (_viewModel.CardType == "Other")
            {
                Label backPhotoLabel = new Label()
                {
                    Text = "Back Photo Needed"
                };
                layout.Children.Add(backPhotoLabel);
            }

            var submitButton = new Button
            {
                Text = "Sumbit",
                Command = _viewModel.SubmitButtonCommand
            };
            layout.Children.Add(submitButton);

            return layout;
        }

    }
}
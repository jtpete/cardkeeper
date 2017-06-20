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
        public AddCardPage(string cardType)
        {
            InitializeComponent();
            _viewModel = new AddCardViewModel(cardType);
            Content = LayoutAddCardPage();
            _viewModel.Navigation = Navigation;
            BindingContext = _viewModel;
        }
        public View LayoutAddCardPage()
        {
            var submitItem = new ToolbarItem
            {
                Text = "Submit",
                Command = _viewModel.SubmitButtonCommand,
            };
            this.ToolbarItems.Add(submitItem);
            var layout = new StackLayout()
            {
                Padding = 5,
            };

            if(_viewModel.CardType != "Other")
            {
               //Button Stack
                StackLayout scanButtons = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal,
                };
                var accountScan = new Button
                {
                    Text = "Scan Code",
                    Command = _viewModel.ScanBarCode,
                    BackgroundColor = Color.FromHex("#cc9933"),
                };
                var noScan = new Button
                {
                    Text = "No Scan Code",
                    Command = _viewModel.NoScanBarCode,
                    BackgroundColor = Color.FromHex("#cc9933"),
                };
                var helpButton = new Button
                {
                    Text = "Help",
                    Command = _viewModel.HelpCard,
                    BackgroundColor = Color.FromHex("#cc9933"),
                };
                scanButtons.Children.Add(accountScan);
                scanButtons.Children.Add(noScan);
                scanButtons.Children.Add(helpButton);
                layout.Children.Add(scanButtons);
            }
            else
            {
                var helpButton = new Button
                {
                    Text = "Help",
                    Command = _viewModel.HelpCard,
                    BackgroundColor = Color.FromHex("#cc9933"),

                };
                layout.Children.Add(helpButton);
            }

            // ACCOUNT NUMBER 
            StackLayout accountRow = new StackLayout();
            accountRow.Orientation = StackOrientation.Horizontal;
            var accountLabel = new Label
            {
                Text = "Account:",
                HorizontalOptions = LayoutOptions.Start

            };
            var accountEntry = new Entry
            {
                Keyboard = Keyboard.Default,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            accountEntry.SetBinding(Entry.TextProperty, new Binding(path: "AccountNumber", source: _viewModel.Card));
            accountRow.Children.Add(accountLabel);
            accountRow.Children.Add(accountEntry);
            layout.Children.Add(accountRow);

            if(_viewModel.CardType == "Gift")
            {
                // PIN 
                StackLayout pinRow = new StackLayout();
                pinRow.Orientation = StackOrientation.Horizontal;
                var pinLabel = new Label
                {
                    Text = "PIN:",
                    HorizontalOptions = LayoutOptions.Start
                };
                var pinEntry = new Entry
                {
                    Keyboard = Keyboard.Numeric,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                pinEntry.SetBinding(Entry.TextProperty, new Binding(path: "Pin", source: _viewModel.Card));
                pinRow.Children.Add(pinLabel);
                pinRow.Children.Add(pinEntry);
                layout.Children.Add(pinRow);
            }

            StackLayout labelRow = new StackLayout();
            labelRow.Orientation = StackOrientation.Horizontal;
            var customLabelLabel = new Label
            {
                Text = "Card Label:",
                HorizontalOptions = LayoutOptions.Start

            };
            var labelEntry = new Entry
            {
                Keyboard = Keyboard.Default,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            labelEntry.SetBinding(Entry.TextProperty, new Binding(path: "Label", source: _viewModel.Card));
            labelRow.Children.Add(customLabelLabel);
            labelRow.Children.Add(labelEntry);
            layout.Children.Add(labelRow);

            // BALANCE
            if (_viewModel.CardType == "Gift")
            {
                StackLayout balanceRow = new StackLayout();
                balanceRow.Orientation = StackOrientation.Horizontal;
                var balanceLabel = new Label
                {
                    Text = "Balance:",
                    HorizontalOptions = LayoutOptions.Start

                };
                var balanceEntry = new Entry
                {
                    Keyboard = Keyboard.Numeric,
                    HorizontalOptions = LayoutOptions.FillAndExpand
                };
                balanceEntry.SetBinding(Entry.TextProperty, new Binding(path: "Balance", source: _viewModel.Balance));
                balanceRow.Children.Add(balanceLabel);
                balanceRow.Children.Add(balanceEntry);
                layout.Children.Add(balanceRow);
            }

            Label takeFrontPhoto = new Label()
            {
                Text = "Take Front Of Card Photo:"
            };
            View frontPhotoLayout = LayoutFrontPhotoView();
            layout.Children.Add(takeFrontPhoto);
            layout.Children.Add(frontPhotoLayout);


            if (_viewModel.CardType == "Other")
            {
                Label takeBackPhoto = new Label()
                {
                    Text = "Take Back Of Card Photo:"
                };
                View backPhotoLayout = LayoutBackPhotoView();
                layout.Children.Add(takeBackPhoto);
                layout.Children.Add(backPhotoLayout);
            }
            else
            {
                Label scanCodeLabel = new Label()
                {
                    Text = "Scan Code:"
                };
                View backPhotoLayout = LayoutBackPhotoView();
                layout.Children.Add(scanCodeLabel);
                layout.Children.Add(backPhotoLayout);
            }
            ScrollView scrollView = new ScrollView()
            {
                Content = layout,
            };

            return scrollView;
        }
        public View LayoutFrontPhotoView()
        {
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
                Command = _viewModel.TakeFrontCardPhoto,
                BackgroundColor = Color.FromHex("#cc9933"),

            };
            frontPhotoLayout.Children.Add(frontPhotoButton, 0, 0);
            frontPhotoLayout.Children.Add(imageHolder, 1, 0);
            Grid.SetColumnSpan(imageHolder, 2);


            return (frontPhotoLayout);
        }
        public View LayoutBackPhotoView()
        {
            Grid backPhotoLayout = new Grid();
            backPhotoLayout.Padding = 10;
            backPhotoLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(125) });
            backPhotoLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            backPhotoLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(175) });

            Frame imageHolder = new Frame()
            {
                Padding = -10,
                CornerRadius = 20,
                HasShadow = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            if(_viewModel.CardType == "Other")
            {
                Image backOfCard = new Image()
                {
                    Aspect = Aspect.AspectFill
                };
                backOfCard.SetBinding(Image.SourceProperty, "DisplayBackImage");
                imageHolder.Content = backOfCard;
                Button backPhotoButton = new Button()
                {
                    Text = "Take Photo",
                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Command = _viewModel.TakeBackCardPhoto,
                    BackgroundColor = Color.FromHex("#cc9933"),
                };
                backPhotoLayout.Children.Add(backPhotoButton, 0, 0);
                backPhotoLayout.Children.Add(imageHolder, 1, 0);
                Grid.SetColumnSpan(imageHolder, 2);
            }
            else
            {
                Image backOfCard = new Image()
                {
                    Aspect = Aspect.AspectFit
                };
                backOfCard.SetBinding(Image.SourceProperty, "DisplayScanCode");
                imageHolder.Padding = 10;
                imageHolder.Content = backOfCard;
                backPhotoLayout.Children.Add(imageHolder, 1, 0);
                Grid.SetColumnSpan(imageHolder, 2);
            }


            return (backPhotoLayout);
        }
    }
}
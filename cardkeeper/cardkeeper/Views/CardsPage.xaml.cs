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
    
    public partial class CardsPage : ContentPage
    {
        CardsViewModel _viewModel;
        private string cardType;
        public CardsPage(string cardType)
        {
            InitializeComponent();
            this.cardType = cardType;
            _viewModel = new CardsViewModel(cardType);
            _viewModel.Navigation = Navigation;
            Content = LayoutCardsPage();
            BindingContext = _viewModel;

        }
        public View LayoutCardsPage()
        {
            StackLayout layout = new StackLayout() { BackgroundColor = Color.FromHex("#2a2a2a") };

            Label emptyLabel = new Label()
            {
                FontSize = 25,
                Text = "No Cards",
                TextColor = Color.White,
            };
            emptyLabel.SetBinding(Label.IsVisibleProperty, "IsEmpty");
            layout.Children.Add(emptyLabel);

            ListView lv = new ListView();
            lv.VerticalOptions = LayoutOptions.FillAndExpand;
            lv.ItemSelected += OnItemSelected;
            lv.ItemsSource = _viewModel.Cards;
            lv.RefreshCommand = _viewModel.LoadCardsCommand;
            lv.IsPullToRefreshEnabled = true;
            lv.HasUnevenRows = true;
            lv.SetBinding(ListView.IsRefreshingProperty, "IsBusy");



            DataTemplate dt = new DataTemplate(()=>
            {
                Grid cellLayout = new Grid();
                cellLayout.Padding = 10;
                cellLayout.RowDefinitions.Add(new RowDefinition { Height = new GridLength(125) });
                cellLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(175) });
                cellLayout.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });


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
                Label balanceLabel = new Label()
                {
                    FontSize = 25,
                    TextColor = Color.White,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center
                };
                balanceLabel.SetBinding(Label.TextProperty, "DisplayBalance");
                cellLayout.Children.Add(balanceLabel, 1,0);
                cellLayout.Children.Add(imageHolder,0,0);

                return new ViewCell { View = cellLayout };
            });
            lv.ItemTemplate = dt;
            layout.Children.Add(lv);

            return layout;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var card = args.SelectedItem as Card;
            if (card == null)
                return;

            await Navigation.PushAsync(new CardDetailPage(card));
       //     CardsListView.SelectedItem = null;

        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.LoadCardsCommand.Execute(null);
        }
        async void AddCardClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddCardPage(cardType));
        }
    }
}
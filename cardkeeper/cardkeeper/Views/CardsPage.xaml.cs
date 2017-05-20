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
        public CardsPage()
        {
            InitializeComponent();
            _viewModel = new CardsViewModel();
            Content = LayoutCardsPage();
            _viewModel.Navigation = Navigation;
            BindingContext = _viewModel;
        }
        public View LayoutCardsPage()
        {
            var layout = new StackLayout();

            var cardsList = new ListView()
            {
                ItemsSource = _viewModel.Cards,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HasUnevenRows = true,
                RefreshCommand = _viewModel.LoadCardsCommand,
                IsPullToRefreshEnabled = true,
                IsRefreshing = IsBusy,
            };
            cardsList.ItemSelected += OnItemSelected;

            foreach (var card in _viewModel.Cards)
            {
                var aCard = new ViewCell();
                var orderOfCard = new StackLayout()
                {
                    Padding = 10,
                };
                var accountNumberTextLabel = new Label
                {
                    Text = "Account Number: "
                };
                var accountNumberLabel = new Label
                {
                Text = card.AccountNumber.ToString(),
                };
                var balanceTextLabel = new Label
                {
                    Text = "Balance: ",
                };
                var balanceLabel = new Label
                {
                    Text = $"${card.Balance.ToString("0.00")}"
                };
                orderOfCard.Children.Add(accountNumberTextLabel);
                orderOfCard.Children.Add(accountNumberLabel);
                orderOfCard.Children.Add(balanceTextLabel);
                orderOfCard.Children.Add(balanceLabel);
                aCard.View = orderOfCard;
            }

            return layout;
        }
        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var card = args.SelectedItem as Card;
            if (card == null)
                return;

            await Navigation.PushAsync(new CardDetailPage(card));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (_viewModel.Cards.Count == 0)
                _viewModel.LoadCardsCommand.Execute(null);
        }
    }
}
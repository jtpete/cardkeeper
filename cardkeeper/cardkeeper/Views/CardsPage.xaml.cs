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
           BindingContext = _viewModel;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var card = args.SelectedItem as Card;
            if (card == null)
                return;

            await Navigation.PushAsync(new CardDetailPage(card));
            CardsListView.SelectedItem = null;

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
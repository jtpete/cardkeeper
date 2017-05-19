using cardkeeper.Models;
using cardkeeper.Services;
using cardkeeper.Views;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;



namespace cardkeeper.ViewModels
{

    public class AddCardViewModel : ContentPage
    {
        public string Balance { get; set; }
        public Card Card
        {
            get { return (Card)GetValue(CardProperty); }
            set { SetValue(CardProperty, value); }
        }
        public static readonly BindableProperty CardProperty = BindableProperty.Create<AddCardViewModel, Card>(c => c.Card, new Card());


        public ICommand SubmitButtonCommand { get; set; }
        public INavigation Navigation { get; set; }

        public AddCardViewModel()
        {
            Card = new Card();
            SubmitButtonCommand = new Command(AddCardToDatabaseAsync);
        }

        public async void AddCardToDatabaseAsync()
        {
            double dec;
            if (double.TryParse(Balance as string, out dec))
                Card.Balance = Math.Round(dec, 2);
            var action = await DisplayAlert("Please confirm:", $"\nAccount Number: {Card.AccountNumber}\nBalance: ${Card.Balance.ToString("0.00")}", "No", "Yes");
            if (!action)
                await Navigation.PushModalAsync(new NavigationPage(new CardDetailPage(Card)));
            else
                await Navigation.PopModalAsync();
        }

    }
}

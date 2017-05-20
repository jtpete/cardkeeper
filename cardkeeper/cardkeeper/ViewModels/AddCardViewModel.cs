using cardkeeper.Helpers;
using cardkeeper.Models;
using cardkeeper.Services;
using cardkeeper.Views;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.IO;
using SQLite;



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
            
            Card.Balance = Converter.ConvertStringToDouble(Balance);
            var action = await DisplayAlert("Please confirm:", $"\nAccount Number: {Card.AccountNumber}\nBalance: ${Card.Balance.ToString("0.00")}", "No", "Yes");

            if (!action)
            {
                MakeDatabaseCall();
                await Navigation.PushModalAsync(new NavigationPage(new CardDetailPage(Card)));
            }
            else
                await Navigation.PopModalAsync();
        }
        private void MakeDatabaseCall()
        {
            //path string for database file
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "cardKeeperDB.db3");

            //setup the db connection
            var db = new SQLiteConnection(dbPath);

            //setup the table
            db.CreateTable<Card>();

            //
            db.Insert(Card);

        }

    }
}

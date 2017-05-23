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
using System.Reflection;

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
                Card.QRCode = await API.GetQRCode(API.GetGoogleQRCodeService(Card.AccountNumber));
                if (Card.QRCode != null)
                {
                    Database.AddCard(Card);
                    await Navigation.PopAsync();
                }
                else
                {
                    Card.QRCode = await API.GetQRCode(API.GetOtherQRCodeService(Card.AccountNumber));
                    if (Card.QRCode != null)
                    {
                        Database.AddCard(Card);
                        await Navigation.PopAsync();
                    }
                    else
                    {
                        await DisplayAlert("Error With Card.", "It seems we had an error adding this card.  Please try back later.", "Ok");
                        await Navigation.PopToRootAsync();
                    }
                }
            }
            else
                await Navigation.PopAsync();
        }

    }
}

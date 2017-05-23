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
using System.Collections.Generic;

namespace cardkeeper.ViewModels
{

    public class AddCardViewModel : ContentPage
    {
        public string Balance { get; set; }
        private string cardType;
        public string CardType { get { return cardType; } set { cardType = value; } }

        public Card Card
        {
            get { return (Card)GetValue(CardProperty); }
            set { SetValue(CardProperty, value); }
        }
        public static readonly BindableProperty CardProperty = BindableProperty.Create<AddCardViewModel, Card>(c => c.Card, new Card());


        public ICommand SubmitButtonCommand { get; set; }
        public INavigation Navigation { get; set; }

        public AddCardViewModel(string cardType)
        {
            Card = new Card();
            this.cardType = cardType;
            SubmitButtonCommand = new Command(ValidateCard);
        }
        public async void ValidateCard()
        {
            bool addThisCard = false;
            switch (cardType)
            {
                case "Gift":
                    {
                        if (Balance == null || Card.AccountNumber == null || Card.AccountNumber == "")
                            await DisplayAlert("Add Incomplete", "Please include values for each field.", "Ok");
                        else
                        {
                            Card.Type = cardType;
                            Card.Balance = Converter.ConvertStringToDouble(Balance);
                            addThisCard = await DisplayAlert("Please confirm:", $"\nCard Type: {Card.Type}\nAccount Number: {Card.AccountNumber}\nBalance: ${Card.Balance.ToString("0.00")}", "Yes", "No");
                        }
                        break;
                    }
                case "Loyalty":
                case "Membership":
                case "Other":
                    {
                        if(Card.AccountNumber == null || Card.AccountNumber == "")
                        {
                            await DisplayAlert("Add Incomplete", "Please include values for each field.", "Ok");
                        }
                        else
                        {
                            Card.Type = cardType;
                            addThisCard = await DisplayAlert("Please confirm:", $"\nCard Type: {Card.Type}\nAccount Number: {Card.AccountNumber}", "Yes", "No");
                        }
                        break;
                    }
            }
            if (addThisCard)
            {
                AddCardToDatabaseAsync();
            }
        }

        public async void AddCardToDatabaseAsync()
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

    }
}

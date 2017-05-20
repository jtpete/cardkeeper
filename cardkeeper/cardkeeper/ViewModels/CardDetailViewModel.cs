using cardkeeper.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using cardkeeper.Helpers;
using System.Diagnostics;
using cardkeeper.Views;

namespace cardkeeper.ViewModels
{
    class CardDetailViewModel : ContentPage
    {
        public Card Card
        {
            get { return (Card)GetValue(CardProperty); }
            set { SetValue(CardProperty, value); }
        }
        public static readonly BindableProperty CardProperty = BindableProperty.Create<AddCardViewModel, Card>(c => c.Card, new Card());


        public ICommand RemoveButtonCommand { get; set; }
        public INavigation Navigation { get; set; }

        public CardDetailViewModel(Card card)
        {
            Card = card;
            RemoveButtonCommand = new Command(RemoveThisCard);
        }
        public async void RemoveThisCard()
        {
            var action = await DisplayAlert("Are you sure you want to remove this card?", $"\nAccount Number: {Card.AccountNumber}\nBalance: ${Card.Balance.ToString("0.00")}", "No", "Yes");
            if (!action)
            {
                try
                {
                    await Database.DeleteCardAsync(Card);
                    await Navigation.PopAsync();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }
    }
}

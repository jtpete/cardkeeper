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
using System.ComponentModel;

namespace cardkeeper.ViewModels
{
    class CardDetailViewModel : ContentPage, INotifyPropertyChanged
    {
        private Card card;
        public Card Card
        {
            get { return card; }
            set
            {
                if (card != value)
                {
                    card = value;
                    OnPropertyChanged("Card");
                }
            }
        }
        private string displayThisBalance;
        public string DisplayThisBalance
        {
            get { return displayThisBalance; }
            set
            {
                if (displayThisBalance != value)
                {
                    displayThisBalance = value;
                    OnPropertyChanged("DisplayThisBalance");
                }
            }
        }

        public ICommand RemoveButtonCommand { get; set; }
        public ICommand ApplyPurchaseButtonCommand { get; set; }
        public ICommand LoadCardCommand { get; set; }
        public ICommand SeePinCommand { get; set; }
        public INavigation Navigation { get; set; }

        public CardDetailViewModel(Card card)
        {
            Card = card;
            displayThisBalance = card.DisplayBalance;
            RemoveButtonCommand = new Command(RemoveThisCard);
            ApplyPurchaseButtonCommand = new Command(ApplyPurchase);
            LoadCardCommand = new Command(LoadCard);
            SeePinCommand = new Command(ShowPinNumber);

        }
        public async void ShowPinNumber()
        {
            await DisplayAlert("Pin Number:", Card.Pin, "Ok");
        }
        public async void RemoveThisCard()
        {
            var answerNo = await DisplayAlert("Are you sure you want to remove this card?", $"\nAccount Number: {Card.AccountNumber}", "No", "Yes");
            if (!answerNo)
            {
                try
                {
                    Database.DeleteCard(Card);
                    await Navigation.PopAsync();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }
        public async void ApplyPurchase()
        {
            await Navigation.PushAsync(new ApplyPurchasePage(Card));
        }
        public void LoadCard()
        {
            Card card = new Card();
            if (Card.AccountNumber != null)
            {
                card = Database.GetCard(Card.AccountNumber);
                Card = null;
                Card = card;
                DisplayThisBalance = card.DisplayBalance;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

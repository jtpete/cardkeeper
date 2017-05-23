using cardkeeper.Helpers;
using cardkeeper.Models;
using cardkeeper.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace cardkeeper.ViewModels
{
    class CardsViewModel : ContentPage, INotifyPropertyChanged
    {
        public ObservableRangeCollection<Card> Cards { get; set; }
        private ImageSource defaultFrontPicture;
        public ImageSource DefaultFrontPicture
        {
            get { return defaultFrontPicture; }
            set
            {
                if (defaultFrontPicture != value)
                {
                    defaultFrontPicture = value;
                    OnPropertyChanged("DefaultFrontPicture");
                }
            }
        }
        private bool isEmpty;
     
        public bool IsEmpty { get { return isEmpty; }
            set { if (isEmpty != value)
                {
                    isEmpty = value;
                    OnPropertyChanged("IsEmpty");
                } } }
        private string cardType;
        public string CardType
        {
            get { return cardType; }
            set
            {
                if (cardType != value)
                {
                    cardType = value;
                    OnPropertyChanged("CardType");
                }
            }
        }

        public ICommand LoadCardsCommand { get; set; }
        public INavigation Navigation { get; set; }


        public CardsViewModel(string cardType)
        {
            Cards = new ObservableRangeCollection<Card>();
            this.cardType = cardType;
            SetDefaultPhoto();
            isEmpty = false;
            LoadCardsCommand = new Command(ExecuteLoadCardsCommand);
        }
        private void SetDefaultPhoto()
        {
            switch (cardType)
            {
                case "Gift":
                    {
                        defaultFrontPicture = ImageSource.FromFile("giftcard.png");
                        break;
                    }
                case "Loyalty":
                    {
                        defaultFrontPicture = ImageSource.FromFile("loyaltycard.png");
                        break;
                    }
                case "Membership":
                    {
                        defaultFrontPicture = ImageSource.FromFile("memberscard.png");
                        break;
                    }
                case "Other":
                    {
                        defaultFrontPicture = ImageSource.FromFile("healthcard.png");
                        break;
                    }
            }
        }
        void ExecuteLoadCardsCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            Cards.Clear();
            var cards = Database.GetCards();
            if (cards != null)
            {
                Cards.ReplaceRange(cards);
                isEmpty = false;
            }
            else
                isEmpty = true;
            IsBusy = false;
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

﻿using cardkeeper.Helpers;
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
            IsEmpty = false;
            LoadCardsCommand = new Command(ExecuteLoadCardsCommand);
        }
        void ExecuteLoadCardsCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            Cards.Clear();
            IsEmpty = false;
            var cards = Database.GetCards(cardType);
            if (cards != null)
            {
                Cards.ReplaceRange(cards);
                IsEmpty = false;
            }
            else
                IsEmpty = true;
            IsBusy = false;
        }
    }
}

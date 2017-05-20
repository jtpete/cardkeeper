using cardkeeper.Helpers;
using cardkeeper.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace cardkeeper.ViewModels
{
    class CardsViewModel : ContentPage
    {
        public ObservableRangeCollection<Card> Cards { get; set; }
        public ICommand LoadCardsCommand { get; set; }
        public INavigation Navigation { get; set; }


        public CardsViewModel()
        {
            Cards = new ObservableRangeCollection<Card>();
            LoadCardsCommand = new Command( () => ExecuteLoadCardsCommand());
        }
        void ExecuteLoadCardsCommand()
        {
            if (IsBusy)
                return;
            IsBusy = true;
            Cards.Clear();
            var cards = Database.GetCards();
            Cards.ReplaceRange(cards);
            IsBusy = false;
        }
    }
}

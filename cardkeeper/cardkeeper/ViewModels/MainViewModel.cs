using cardkeeper.Helpers;
using cardkeeper.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace cardkeeper.ViewModels
{
    class MainViewModel : ContentPage
    {
        public ICommand AddCardCommand { get; set; }
        public ICommand ViewCardsCommand { get; set; }

        public INavigation Navigation { get; set; }

        public MainViewModel()
        {
      //      File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "cardKeeperDB.db3"));
            Database.InitializeDatabase();
            AddCardCommand = new Command(GoToAddCardPage);
            ViewCardsCommand = new Command(GoToViewCardsPage);
        }
        public async void GoToAddCardPage()
        {
            await Navigation.PushAsync(new AddCardPage());
        }
        public async void GoToViewCardsPage()
        {
           await Navigation.PushAsync(new CardsPage());
        }
    }
}

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
        public ICommand GiftCardsCommand { get; set; }
        public ICommand LoyaltyCardsCommand { get; set; }
        public ICommand MembershipCardsCommand { get; set; }
        public ICommand OtherCardsCommand { get; set; }
        public ICommand AboutCommand { get; set; }

        public INavigation Navigation { get; set; }

        public MainViewModel()
        {
     //       File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "cardKeeperDB.db3"));
            Database.InitializeDatabase();
            GiftCardsCommand = new Command(GiftViewCardsPage);
            LoyaltyCardsCommand = new Command(LoyaltyViewCardsPage);
            MembershipCardsCommand = new Command(MembershipViewCardsPage);
            OtherCardsCommand = new Command(OtherViewCardsPage);
            AboutCommand = new Command(AboutViewPage);
        }
        public async void GiftViewCardsPage()
        {
            string cardType = "Gift";
           await Navigation.PushAsync(new CardsPage(cardType));
        }
        public async void LoyaltyViewCardsPage()
        {
            string cardType = "Loyalty";
            await Navigation.PushAsync(new CardsPage(cardType));
        }
        public async void MembershipViewCardsPage()
        {
            string cardType = "Membership";
            await Navigation.PushAsync(new CardsPage(cardType));
        }
        public async void OtherViewCardsPage()
        {
            string cardType = "Other";
            await Navigation.PushAsync(new CardsPage(cardType));
        }
        public async void AboutViewPage()
        {
            await Navigation.PushAsync(new AboutPage());
        }

    }
}

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
        string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "cardKeeperDB.db3");
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

            try
            {
                Cards.Clear();
                var db = new SQLiteConnection(dbPath);
                var table = db.Table<Card>();
                Cards.ReplaceRange(table);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}

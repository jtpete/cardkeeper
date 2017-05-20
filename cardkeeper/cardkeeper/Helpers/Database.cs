using cardkeeper.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using cardkeeper.Models;
using System.Threading.Tasks;
using SQLite;
using System.Diagnostics;
using Xamarin.Forms;
using System.Windows.Input;


namespace cardkeeper.Helpers
{
    public static class Database
    {
        
        private static string GetDBPath()
        {
            const string databaseFile = "cardKeeperDB.db3";
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), databaseFile);
            return dbPath;
        }
        public static async Task<bool> AddCardAsync(Card card)
        {
            try
            {
                string dbPath = GetDBPath();
                var db = new SQLiteConnection(dbPath);
                await Task.Run(() => db.CreateTable<Card>());
         //       await Task.Run(() => db.Insert(card));
                var item = db.Insert(card);

                return true;
            }
            catch(Exception ex)
			{
                    Debug.WriteLine(ex);
                    MessagingCenter.Send(new MessagingCenterAlert
                    {
                        Title = "Error",
                        Message = "Unable to add card.",
                        Cancel = "OK"
                    }, "message");
                return false;
            }
        }
        public static async Task<bool> UpdateCardAsync(Card card)
        {
            try
            {
                string dbPath = GetDBPath();
                var db = new SQLiteConnection(dbPath);
                await Task.Run(() => db.Update(card));
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to update card.",
                    Cancel = "OK"
                }, "message");
                return false;
            }
        }
        public static async Task<bool> DeleteCardAsync(Card card)
        {
            try
            {
                string dbPath = GetDBPath();
                var db = new SQLiteConnection(dbPath);
                await Task.Run(() => db.Delete(card));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to delete item.",
                    Cancel = "OK"
                }, "message");
                return false;
            }
            return true;
        }
        public static async Task<Card> GetCardAsync(string id)
        {
            try
            {
                string dbPath = GetDBPath();
                var db = new SQLiteConnection(dbPath);
                var card = db.Table<Card>().Where(c => c.AccountNumber == id).FirstOrDefault();
                return card;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to find card.",
                    Cancel = "OK"
                }, "message");
                return null;
            }
        }
        public static IEnumerable<Card> GetCards(bool forceRefresh = false)
        {
            try
            {
                string dbPath = GetDBPath();
                var db = new SQLiteConnection(dbPath);
                var card = db.Table<Card>();
                return card;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }

        public static Task InitializeAsync()
        {
            return null;
        }
        public static async Task<bool> PullLatestAsync()
        {
            return true;
        }
        public static async Task<bool> SyncAsync()
        {
            return true;
        }
        public static async void Seed()
        {
            //List<Card> Cards = new List<Card>();
            //Card card1 = new Card() { AccountNumber = "12345678", Balance = 93.76 };
            //Card card2 = new Card() { AccountNumber = "555938366224", Balance = 23.76 };
            //Card card3 = new Card() { AccountNumber = "2555478838534523451", Balance = 555.20 };
            //Card card4 = new Card() { AccountNumber = "886653784945", Balance = 200.77 };
            //Card card5 = new Card() { AccountNumber = "99922626674", Balance = 10.00 };
            //Card card6 = new Card() { AccountNumber = "76652788483423423423", Balance = 1000.00 };
            //Card card7 = new Card() { AccountNumber = "123432352355678", Balance = 13.76 };
            //Card card8 = new Card() { AccountNumber = "3566512345678", Balance = 65.50 };
            //Cards.Add(card1);
            //Cards.Add(card2);
            //Cards.Add(card3);
            //Cards.Add(card4);
            //Cards.Add(card5);
            //Cards.Add(card6);
            //Cards.Add(card7);
            //Cards.Add(card8);
            //foreach(var card in Cards)
            //{
            //   try
            //   {
            //      string dbPath = GetDBPath();
            //       var db = new SQLiteConnection(dbPath);
            //       await Task.Run(() => db.CreateTable<Card>());
            //       await Task.Run(() => db.Insert(card));
            //   }
            //   catch (Exception ex)
            //   {
            //       Debug.WriteLine(ex);
            //   }
            //}
        }
    }
}

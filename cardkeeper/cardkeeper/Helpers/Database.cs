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
        const string databaseFile = "cardKeeperDB.db3";
        private static string GetDBPath()
        { 
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), databaseFile);
            return dbPath;
        }
        public static bool AddCard(Card card)
        {
            try
            {
                string dbPath = GetDBPath();
                var db = new SQLiteConnection(dbPath);
                db.CreateTable<Card>();
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
        public static bool UpdateCard(Card card)
        {
            try
            {
                string dbPath = GetDBPath();
                var db = new SQLiteConnection(dbPath);
                db.Update(card);
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
        public static bool DeleteCard(Card card)
        {
            try
            {
                string dbPath = GetDBPath();
                var db = new SQLiteConnection(dbPath);
                db.Delete(card);
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
        public static Card GetCard(string id)
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
        public static IEnumerable<Card> GetCards(string cardType)
        {
            try
            {
                string dbPath = GetDBPath();
                var db = new SQLiteConnection(dbPath);
                var card = db.Table<Card>().Where(c => c.Type == cardType);
                if (card.Count() != 0)
                    return card;
                else
                    return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
        public static bool InitializeDatabase()
        {
            if (!File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), databaseFile)))
            {
                string dbPath = GetDBPath();
                var db = new SQLiteConnection(dbPath);
                db.CreateTable<Card>();
            };
            return true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace cardkeeper.Models
{
    public class Card : INotifyPropertyChanged
    {
     
        public Card()
        {
        }

        private int accountNumber;
        public int AccountNumber
        {
            get { return accountNumber; }
            set { accountNumber = value;
                OnPropertyChanged("AccountNumber");
            }
        }
        public byte[] FrontImage { get; set; }
        public byte[] QRCode { get; set; }
        private double balance;
        public double Balance
        {
            get { return balance; }
            set
            {
                balance = value;
                OnPropertyChanged("Balance");
            }
        }
        private string type;
        public string Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged("Type");
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using SQLite;
using cardkeeper.Helpers;

namespace cardkeeper.Models
{
    public class Card : INotifyPropertyChanged
    {
     
        public Card()
        {
        }
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        private string accountNumber;
        public string AccountNumber
        {
            get { return accountNumber; }
            set { accountNumber = value;
                OnPropertyChanged("AccountNumber");
            }
        }
        private byte[] frontImage;
        public ImageSource FrontImage
        {
            get
            {
                if (frontImage != null)
                    return Converter.ByteToImage(frontImage);
                else
                {
                    if (type == "Gift")
                        return ImageSource.FromFile("giftcard.png");
                    else if (type == "Loyalty")
                        return ImageSource.FromFile("loyaltycard.png");
                    else if (type == "Membership")
                        return ImageSource.FromFile("memberscard.png");
                    else if (type == "Other")
                        return ImageSource.FromFile("healthcard.png");
                    else
                        return null;
                }
            }
            set
            {
             
            }
        }
        public byte[] BackImage { get; set; }
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

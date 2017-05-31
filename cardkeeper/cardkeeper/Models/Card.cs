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
        
        public byte[] FrontImage { get {return frontImage;} set{frontImage = value;} }
        [Ignore]
        public ImageSource DisplayFrontImage
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
        private byte[] backImage { get; set; }
        public byte[] BackImage { get { return backImage; } set { backImage = value; } }
        [Ignore]
        public ImageSource DisplayBackImage
        {
            get
            {
                if (backImage != null)
                    return Converter.ByteToImage(backImage);
                else
                    return null;
            }
            set
            {

            }
        }
        public byte[] ScanCode { get; set; }
        public string ScanCodeNumber { get; set; }
        private double balance;
        public double Balance
        {
            get { return balance; }
            set
            {
                balance = Math.Round(value, 2);
                OnPropertyChanged("Balance");
                DisplayBalance = balance.ToString();
            }
        }
        private string displayBalance;
        [Ignore]
        public string DisplayBalance
        {
            get
            {
                if (Type == "Gift")
                    return "$" + displayBalance;
                else
                    return "";
            }
            set
            {
                if (displayBalance != value)
                {
                    displayBalance = value;
                    OnPropertyChanged("DisplayBalance");
                }
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
        private string label;
        public string Label
        {
            get { return label; }
            set
            {
                label = value;
                OnPropertyChanged("Label");
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

﻿using cardkeeper.Helpers;
using cardkeeper.Models;
using cardkeeper.Services;
using cardkeeper.Views;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using System.IO;
using SQLite;
using System.Reflection;
using System.Collections.Generic;
using Plugin.Media;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;
using Android.Graphics;
using System.Text;
using Java.Nio;

namespace cardkeeper.ViewModels
{

    public class AddCardViewModel : ContentPage, INotifyPropertyChanged
    {
        private string balance;
        public string Balance
        {
            get { return balance; }
            set
            {
                balance = value;
                OnPropertyChanged("Balance");
            }
        }
        private string cardType;
        public string CardType { get { return cardType; } set { cardType = value; } }
        private byte[] frontImage;
        private ImageSource displayFrontImage;
        public ImageSource DisplayFrontImage
        {
            get
            {
                if (displayFrontImage != null)
                    return displayFrontImage;
                else
                    return null;
            }
            set
            {
                if(displayFrontImage != value)
                    displayFrontImage = value;
                OnPropertyChanged("DisplayFrontImage");
            }
        }
        private byte[] backImage;
        private ImageSource displayBackImage;
        public ImageSource DisplayBackImage
        {
            get
            {
                if (displayBackImage != null)
                    return displayBackImage;
                else
                    return null;
            }
            set
            {
                if (displayBackImage != value)
                    displayBackImage = value;
                OnPropertyChanged("DisplayBackImage");
            }
        }
        private ImageSource displayScanCode;
        public ImageSource DisplayScanCode
        {
            get
            {
                if (displayScanCode != null)
                    return displayScanCode;
                else
                    return null;
            }
            set
            {
                if (displayScanCode != value)
                    displayScanCode = value;
                OnPropertyChanged("DisplayScanCode");
            }
        }
        public Card Card
        {
            get { return (Card)GetValue(CardProperty); }
            set { SetValue(CardProperty, value); }
        }
        public static readonly BindableProperty CardProperty = BindableProperty.Create<AddCardViewModel, Card>(c => c.Card, new Card());


        public ICommand SubmitButtonCommand { get; set; }
        public ICommand TakeFrontCardPhoto { get; set; }
        public ICommand TakeBackCardPhoto { get; set; }
        public ICommand ScanBarCode { get; set; }
        public ICommand NoScanBarCode { get; set; }

        public INavigation Navigation { get; set; }

        public AddCardViewModel(string cardType)
        {
            Card = new Card();
            this.cardType = cardType;
            Card.Type = cardType;
            DisplayFrontImage = Card.DisplayFrontImage;
            DisplayBackImage = Card.DisplayBackImage;
            SubmitButtonCommand = new Command(ValidateCard);
            TakeFrontCardPhoto = new Command(TakeFrontPhoto);
            TakeBackCardPhoto = new Command(TakeBackPhoto);
            ScanBarCode = new Command(ScanTheBarCode);
            NoScanBarCode = new Command(NoScanCodeOnCard);


        }
        public async void ValidateCard()
        {
            bool doNotAddCard = false;
            switch (cardType)
            {
                case "Gift":
                    {
                        if (balance == null || Card.AccountNumber == null || Card.AccountNumber == "")
                            await DisplayAlert("Add Incomplete", "Please include values for each field.", "Ok");
                        else
                        {
                            if (frontImage != null)
                                Card.FrontImage = frontImage;
                            Card.Type = cardType;
                            Card.Balance = Converter.ConvertStringToDouble(balance);
                            doNotAddCard = await DisplayAlert("Please confirm:", $"\nCard Type: {Card.Type}\nAccount Number: {Card.AccountNumber}\nBalance: {Card.DisplayBalance}", "No", "Yes");
                        }
                        break;
                    }
                case "Loyalty":
                case "Membership":
                    {
                        if(Card.AccountNumber == null || Card.AccountNumber == "")
                        {
                            await DisplayAlert("Add Incomplete", "Please include values for each field.", "Ok");
                        }
                        else
                        {
                            if (frontImage != null)
                                Card.FrontImage = frontImage;
                            Card.Type = cardType;
                            doNotAddCard = await DisplayAlert("Please confirm:", $"\nCard Type: {Card.Type}\nAccount Number: {Card.AccountNumber}", "No", "Yes");
                        }
                        break;
                    }
                case "Other":
                    {
                        if (Card.AccountNumber == null || Card.AccountNumber == "")
                        {
                            await DisplayAlert("Add Incomplete", "Please include values for each field.", "Ok");
                        }
                        else
                        {
                            if (frontImage != null)
                                Card.FrontImage = frontImage;
                            if (backImage != null)
                                Card.BackImage = backImage;
                            Card.Type = cardType;
                            doNotAddCard = await DisplayAlert("Please confirm:", $"\nCard Type: {Card.Type}\nAccount Number: {Card.AccountNumber}", "No", "Yes");
                        }
                        break;
                    }
            }
            if (!doNotAddCard)
            {
                AddCardToDatabaseAsync();
            }
        }

        public async void AddCardToDatabaseAsync()
        {
            if(Card.ScanCode == null && Card.AccountNumber != null)
            {
                Card.ScanCode = await API.GetScanCode(API.GetBarCodeService(Card.AccountNumber));
                Card.ScanCodeNumber = Card.AccountNumber;
                Card.ScanCodeType = "CODE_128";
            }
            if (Card.ScanCode != null)
            {
                Database.AddCard(Card);
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error With Card.", "It seems we had an error adding this card.  Please try back later.", "Ok");
                await Navigation.PopToRootAsync();
            }
        }
        public async void TakeFrontPhoto()
        {
            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
                // Supply media options for saving our photo after it's taken.
                var mediaOptions = new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Cards",
                    Name = $"{DateTime.UtcNow}.jpg",
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Custom,
                    CustomPhotoSize = 40,
                    AllowCropping = true,
                };
                
                // Take a photo of the business receipt.
                var file = await CrossMedia.Current.TakePhotoAsync(mediaOptions);
                if (file == null)
                    return;
                frontImage = System.IO.File.ReadAllBytes(file.Path);
                DisplayFrontImage = Converter.ByteToImage(frontImage);
                File.Delete(file.Path);
                file.Dispose();
            }

        }
        public async void TakeBackPhoto()
        {
            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
                // Supply media options for saving our photo after it's taken.
                var mediaOptions = new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Cards",
                    Name = $"{DateTime.UtcNow}.jpg",
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Custom,
                    CustomPhotoSize = 40,
                    AllowCropping = true,
                };

                var file = await CrossMedia.Current.TakePhotoAsync(mediaOptions);
                if (file == null)
                    return;
                backImage = System.IO.File.ReadAllBytes(file.Path);
                DisplayBackImage = Converter.ByteToImage(backImage);
                File.Delete(file.Path);
                file.Dispose();
            }
        }
        public async void NoScanCodeOnCard()
        {
            await DisplayAlert("Please type in account number on card", null, "OK");
        }
        public async void ScanTheBarCode()
        {
            var scanPage = new ZXingScannerPage();
            // Navigate to our scanner page
            await Navigation.PushAsync(scanPage);
            


            scanPage.OnScanResult += (result) =>
            {                
                // Stop scanning
                scanPage.IsScanning = false;

                // Pop the page and show the result
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PopAsync();
                    var action = await DisplayAlert("Is this the account number on your card?", result.Text, "Yes", "No");
                    if (action)
                    {
                        Card.AccountNumber = result.Text;
                    }
                    else
                    {
                        await DisplayAlert("Please type in account number", null, "OK");
                    }
                    Card.ScanCodeNumber = result.Text;
                    Card.ScanCodeType = result.BarcodeFormat.ToString();
                    
                    GenerateBarCodeFormated(result);
                });

            };


        }
        private void GenerateBarCodeFormated(ZXing.Result scanResult)
        {
            var barcode = new BarcodeWriter();

            barcode.Format = scanResult.BarcodeFormat;
            Card.ScanCodeType = barcode.Format.ToString();
            if(Card.IsQRCode)
            {
                barcode.Options.Height = 75;
                barcode.Options.Width = 75;
            }
            else
            {
                barcode.Options.Height = 50;
                barcode.Options.Width = 125;
            }
            var result = barcode.Write(scanResult.Text);
            Card.ScanCode = ImageToByte(result);
            DisplayScanCode = Converter.ByteToImage(Card.ScanCode);
        }
        public static byte[] ImageToByte(Bitmap img)
        {
            var sdCardPath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            var filePath = System.IO.Path.Combine(sdCardPath, "test.png");
            var stream = new FileStream(filePath, FileMode.Create);
            img.Compress(Bitmap.CompressFormat.Png, 100, stream);
            stream.Close();
            byte[] file = System.IO.File.ReadAllBytes(filePath);
            File.Delete(filePath);
            if (file == null)
                return null;
            else
            {
                return file;
            }
        }
    }
}

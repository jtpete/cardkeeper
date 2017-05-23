﻿using cardkeeper.Helpers;
using cardkeeper.Models;
using cardkeeper.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace cardkeeper.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ApplyPurchasePage : ContentPage
	{
        public Card Card { get; set; }
		public ApplyPurchasePage (Card card)
		{
			InitializeComponent ();
            Card = card;
            Content = Layout();

        }
        public View Layout()
        {
            StackLayout layout = new StackLayout()
            {
                BackgroundColor = Color.FromHex("#2a2a2a"),
            };

            Label purchaseLabel = new Label()
            {
                Text = "Pruchase Price: ",
                TextColor = Color.White,
            };
            Editor purchasePrice = new Editor()
            {
                Keyboard = Keyboard.Numeric,
                TextColor = Color.White,
            };
            purchasePrice.Completed += ApplyPurchase;

            layout.Children.Add(purchaseLabel);
            layout.Children.Add(purchasePrice);

            return layout;
            
        }
        public async void ApplyPurchase(object sender, EventArgs e)
        {
            string text = ((Editor)sender).Text;
            double purchaseAmount = Converter.ConvertStringToDouble(text);
            double newBalance = Card.Balance;
            if (newBalance - purchaseAmount > 0)
                newBalance -= purchaseAmount;
            else
                newBalance = 0;
            var action = await DisplayAlert("Please confirm:", $"\nPurchase Amount: {purchaseAmount.ToString("0.00")}\nNew Balance: ${newBalance.ToString("0.00")}", "No", "Yes");
            if (!action)
            {
                Card.Balance = newBalance;
                Database.UpdateCard(Card);
                await Navigation.PopAsync();
            }
        }
	}
}
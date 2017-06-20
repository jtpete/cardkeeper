using cardkeeper.Helpers;
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
        Editor purchasePrice;

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
                BackgroundColor = Color.White,
            };

            Label purchaseLabel = new Label()
            {
                Text = "Purchase Price: ",
                TextColor = Color.Black,
            };
            purchasePrice = new Editor()
            {
                Keyboard = Keyboard.Numeric,
                TextColor = Color.Black,
            };

            Button doneButton = new Button()
            {
                Text = "Done",
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            doneButton.Clicked += ApplyPurchase;

            layout.Children.Add(purchaseLabel);
            layout.Children.Add(purchasePrice);
            layout.Children.Add(doneButton);

            return layout;
            
        }
        public async void ApplyPurchase(object sender, EventArgs e)
        {
            if (purchasePrice.Text == null)
            {
                purchasePrice.Text = "0";
            }
            double purchaseAmount = Converter.ConvertStringToDouble(purchasePrice.Text);
            double newBalance = Card.Balance;
            if (newBalance - purchaseAmount > 0)
                newBalance -= purchaseAmount;
            else
                newBalance = 0;
            var answerNo = await DisplayAlert("Please confirm:", $"\nPurchase Amount: {purchaseAmount.ToString("0.00")}\nNew Balance: ${newBalance.ToString("0.00")}", "No", "Yes");
            if (!answerNo)
            {
                Card.Balance = newBalance;
                Database.UpdateCard(Card);
                await Navigation.PopAsync();
            }
        }
	}
}
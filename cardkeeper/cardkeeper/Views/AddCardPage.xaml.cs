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
	public partial class AddCardPage : ContentPage
	{

        AddCardViewModel _viewModel;
        public AddCardPage (string cardType)
		{
			InitializeComponent ();
            _viewModel = new AddCardViewModel(cardType);
            Content = LayoutAddCardPage();
            _viewModel.Navigation = Navigation;
            BindingContext = _viewModel;
        }
        public View LayoutAddCardPage()
        {
            var layout = new StackLayout();


            // ACCOUNT NUMBER 
            var accountLabel = new Label
            {
                Text = "Account Number"
            };
            var accountNumber = new Entry
            {
                Text = "Account Number",
                Keyboard = Keyboard.Numeric,
            };
            accountNumber.SetBinding(Entry.TextProperty, new Binding(path: "AccountNumber", source: _viewModel.Card));
            layout.Children.Add(accountLabel);
            layout.Children.Add(accountNumber);

            // BALANCE
            if(_viewModel.CardType == "Gift")
            {
                var balanceLabel = new Label
                {
                    Text = "Card Balance"
                };
                var balance = new Entry
                {
                    Text = "Card Balance",
                    Keyboard = Keyboard.Numeric,
                };
                balance.SetBinding(Entry.TextProperty, new Binding(path: "Balance", source: _viewModel.Balance));
                layout.Children.Add(balanceLabel);
                layout.Children.Add(balance);
            }

            Label frontPhotoLabel = new Label()
            {
                Text = "Front Photo Needed"
            };
            layout.Children.Add(frontPhotoLabel);


            if (_viewModel.CardType == "Other")
            {
                Label backPhotoLabel = new Label()
                {
                    Text = "Back Photo Needed"
                };
                layout.Children.Add(backPhotoLabel);
            }

            var submitButton = new Button
            {
                Text = "Sumbit",
                Command = _viewModel.SubmitButtonCommand
            };
            layout.Children.Add(submitButton);

            return layout;
        }

    }
}
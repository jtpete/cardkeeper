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
        public AddCardPage ()
		{
			InitializeComponent ();
            _viewModel = new AddCardViewModel();
            Content = LayoutAddCardPage();
            _viewModel.Navigation = Navigation;
            BindingContext = _viewModel;
        }
        public View LayoutAddCardPage()
        {
            var layout = new StackLayout();

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

        //     balance.SetBinding(Entry.TextProperty, new Binding(path: "Balance", converter: new DecimalConverter(), source: _viewModel.Card));

            var submitButton = new Button
            {
                Text = "Sumbit",
                Command = _viewModel.SubmitButtonCommand
            };

            layout.Children.Add(accountLabel);
            layout.Children.Add(accountNumber);
            layout.Children.Add(balanceLabel);
            layout.Children.Add(balance);
            layout.Children.Add(submitButton);

            return layout;
        }

    }
}
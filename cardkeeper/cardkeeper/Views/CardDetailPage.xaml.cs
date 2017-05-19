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
	public partial class CardDetailPage : ContentPage
	{
        CardDetailViewModel _viewModel;
		public CardDetailPage (Card card)
		{
			InitializeComponent ();
            _viewModel = new CardDetailViewModel(card);
            Content = LayoutCardDetailPage();
            _viewModel.Navigation = Navigation;
            BindingContext = _viewModel;
        }
        public View LayoutCardDetailPage()
        {
            var layout = new StackLayout();

            var row1 = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
            };
            var accountLabel = new Label
            {
                Text = "Account Number:"
            };
            var accountNumber = new Label
            {
                Text = $"{_viewModel.Card.AccountNumber}",
            };
            row1.Children.Add(accountLabel);
            row1.Children.Add(accountNumber);
            var row2 = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
            };
            var balanceLabel = new Label
            {
                Text = "Balance: $"
            };
            var balance = new Label
            {
                Text = $"{_viewModel.Card.Balance}",
            };
            row2.Children.Add(balanceLabel);
            row2.Children.Add(balance);

            layout.Children.Add(row1);
            layout.Children.Add(row2);

            return layout;

        }
	}
}
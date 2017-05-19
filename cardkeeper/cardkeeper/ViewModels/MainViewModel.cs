using cardkeeper.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace cardkeeper.ViewModels
{
    class MainViewModel : ContentPage
    {
        public ICommand AddCardCommand { get; set; }
        public ICommand ViewCardsCommand { get; set; }

        public INavigation Navigation { get; set; }

        public MainViewModel()
        {
            AddCardCommand = new Command(GoToAddCardPage);
            ViewCardsCommand = new Command(GoToViewCardsPage);
        }
        public async void GoToAddCardPage()
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddCardPage()));
        }
        public async void GoToViewCardsPage()
        {
            await Navigation.PopToRootAsync();
           // await Navigation.PushAsync(new NavigationPage(new ViewCardsPage()));
        }
    }
}

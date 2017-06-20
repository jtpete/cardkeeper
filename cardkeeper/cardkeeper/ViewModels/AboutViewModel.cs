using cardkeeper.Helpers;
using cardkeeper.Models;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace cardkeeper.ViewModels
{
    class AboutViewModel : ContentPage
    {
        public Feedback Feedback
        {
            get { return (Feedback)GetValue(CardProperty); }
            set { SetValue(CardProperty, value); }
        }
        public static readonly BindableProperty CardProperty = BindableProperty.Create<AboutViewModel, Feedback>(f => f.Feedback, new Feedback());

        public INavigation Navigation { get; set; }
        public ICommand SendFeedback { get; set; }
        public ICommand ShowDetails { get; set; }
        public AboutViewModel()
        {
            Feedback = new Feedback();
            SendFeedback = new Command(SendThisFeedback);
            ShowDetails = new Command(DisplayDetails);
        }
        public async void SendThisFeedback()
        {
            if (Feedback.Message != null && Feedback.Message.Trim() != "")
            {
                MailMessage message = new MailMessage();
                message.To.Add(new MailAddress(Email.FeedbackEmail));
                message.Subject = "Card Keeper User Feedback";
                message.Body = $"Name: {Feedback.Name}\nEmail: {Feedback.Email}\nMessage: {Feedback.Message}";
                if (Email.Send(message))
                    await DisplayAlert("Thank You for the Feedback!", $"Name: {Feedback.Name}\nEmail: {Feedback.Email}\nMessage: {Feedback.Message}", "Ok");
                else
                    await DisplayAlert("Feedback Not Sent", $"I'm sorry, it doesn't seem your feedback was sent.  Please try again at another time.", "Ok");
            }
            await Navigation.PopAsync();
        }
        public async void DisplayDetails()
        {
            await DisplayAlert("Card Keeper 1.0", $"Publisher: Creation Studio \n\nPurpose: Keeping those plastic cards digital and ready to use.", "Ok");
        }
    }
}

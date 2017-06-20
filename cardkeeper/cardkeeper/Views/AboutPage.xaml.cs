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
	public partial class AboutPage : ContentPage
	{
        AboutViewModel _viewModel;
		public AboutPage ()
		{
			InitializeComponent ();
            _viewModel = new AboutViewModel();
            Content = LayoutPage();
            _viewModel.Navigation = Navigation;
            BindingContext = _viewModel;
        }

        public View LayoutPage()
        {
            StackLayout layout = new StackLayout()
            {
                BackgroundColor = Color.FromHex("#2a2a2a"),
            };

            Label versionLabel = new Label()
            {
                Text = "Card Keeper 1.0",
                FontSize = 25,
                TextColor = Color.FromHex("#cc9933"),
            };
            layout.Children.Add(versionLabel);
            Button detailsButton = new Button()
            {
                Text = "Details",
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Command = _viewModel.ShowDetails,
                BackgroundColor = Color.FromHex("#cc9933"),

            };
            layout.Children.Add(detailsButton);           
            Label feedbackText = new Label()
            {
                Text = "Help Card Keeper get better.",
                FontSize = 20,
                TextColor = Color.FromHex("#cc9933"),
            };
            layout.Children.Add(feedbackText);
            Button submitButton = new Button()
            {
                Text = "Submit",
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Command = _viewModel.SendFeedback,
                BackgroundColor = Color.FromHex("#cc9933"),

            };
            layout.Children.Add(submitButton);
            // Name 
            StackLayout nameRow = new StackLayout();
            nameRow.Orientation = StackOrientation.Horizontal;
            var nameLabel = new Label
            {
                Text = "Name:",
                FontSize = 20,
                TextColor = Color.FromHex("#cc9933"),
                HorizontalOptions = LayoutOptions.Start,

            };
            var nameEntry = new Entry
            {
                Keyboard = Keyboard.Default,
                TextColor = Color.FromHex("#cc9933"),
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            nameEntry.SetBinding(Entry.TextProperty, new Binding(path: "Name", source: _viewModel.Feedback));
            nameRow.Children.Add(nameLabel);
            nameRow.Children.Add(nameEntry);
            layout.Children.Add(nameRow);

            // Email Address 
            StackLayout emailRow = new StackLayout();
            emailRow.Orientation = StackOrientation.Horizontal;
            var emailLabel = new Label
            {
                Text = "Email:",
                FontSize = 20,
                TextColor = Color.FromHex("#cc9933"),
                HorizontalOptions = LayoutOptions.Start

            };
            var emailEntry = new Entry
            {
                Keyboard = Keyboard.Email,
                TextColor = Color.FromHex("#cc9933"),
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            emailEntry.SetBinding(Entry.TextProperty, new Binding(path: "Email", source: _viewModel.Feedback));
            emailRow.Children.Add(emailLabel);
            emailRow.Children.Add(emailEntry);
            layout.Children.Add(emailRow);

            // Message 

            var messageLabel = new Label
            {
                Text = "Feedback:",
                FontSize = 20,
                TextColor = Color.FromHex("#cc9933"),
                HorizontalOptions = LayoutOptions.Start

            };
            var messageEditor = new Editor()
            {
                Keyboard = Keyboard.Default,
                FontSize = 12,
                HeightRequest = 200,
                TextColor = Color.FromHex("#cc9933"),
                HorizontalOptions = LayoutOptions.FillAndExpand
            };
            messageEditor.SetBinding(Editor.TextProperty, new Binding(path: "Message", source: _viewModel.Feedback));
            layout.Children.Add(messageLabel);
            layout.Children.Add(messageEditor);



            ScrollView scrollView = new ScrollView()
            {
                Content = layout,
            };

            return scrollView;
        }
	}
}
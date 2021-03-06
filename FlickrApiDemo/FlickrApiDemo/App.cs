﻿using Xamarin.Forms;

namespace FlickrApiDemo
{
    public class App : Application
    {
        public App()
        {
            var button = new Button()
            {
                Text = "Click"
            };


            // The root page of your application
            var content = new ContentPage
            {
                Title = "FlickrApiDemo",
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        new Label {
                            HorizontalTextAlignment = TextAlignment.Center,
                            Text = "Welcome to Xamarin Forms!"
                        },
                    }
                }
            };

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

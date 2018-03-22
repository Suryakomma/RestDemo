using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Plugin.Connectivity.Abstractions;
using Plugin.Connectivity;

namespace RestDemoN
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

             MainPage = new RestDemoN.MainPage();
              //MainPage = CrossConnectivity.Current.IsConnected
              //  ? (Page) new NetworkViewPage()
              //  : new NoNetworkPage(); 
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

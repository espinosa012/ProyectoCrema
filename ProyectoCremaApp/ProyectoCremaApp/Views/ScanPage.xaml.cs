using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProyectoCremaApp.Views
{
    public partial class ScanPage : ContentPage
    {
        public ScanPage()
        {
            InitializeComponent();
        }
        async void OnButtonClicked(object sender, EventArgs e)
        {
            // Launch the specified URL in the system browser.
            await Launcher.OpenAsync("https://aka.ms/xamarin-quickstart");
        }
    }
}
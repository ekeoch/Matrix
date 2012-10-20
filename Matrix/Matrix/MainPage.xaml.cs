using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls;

namespace Matrix
{
    public partial class MainPage : PhoneApplicationPage
    {
        private readonly bool _dark;
        private const string Black = "/Matrix;component/Images/numbers_I.jpg";
        private const string White = "/Matrix;component/Images/numbers1.jpg";
        public MainPage()
        {
            _dark = ((Visibility)Application.Current.Resources["PhoneDarkThemeVisibility"] == Visibility.Visible);
            InitializeComponent();
            this.image1.Source = !_dark ? new BitmapImage() { UriSource = new System.Uri(White, UriKind.Relative) } : new BitmapImage() { UriSource = new System.Uri(Black, UriKind.Relative) };
        }

        private void Button1Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/AddPage.xaml", UriKind.Relative));
        }

        private void Button2Click(object sender, RoutedEventArgs e)
        {
            
            NavigationService.Navigate(new Uri("/Pages/OperationsPage.xaml", UriKind.Relative));
        }
    }
}
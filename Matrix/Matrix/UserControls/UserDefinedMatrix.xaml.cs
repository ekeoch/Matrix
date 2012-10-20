using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Matrix
{
    public partial class UserDefinedMatrix : UserControl
    {
        private readonly bool _dark;
        private const string BlackBracket = "/Matrix;component/black/Untitled_black.png";
        private const string WhiteBracket = "/Matrix;component/Images/Untitled1.png";
        public UserDefinedMatrix()
        {
            _dark = ((Visibility)Application.Current.Resources["PhoneDarkThemeVisibility"] == Visibility.Visible);
            InitializeComponent();
            if (!_dark)
            {
                this.image1.Source = new BitmapImage() { UriSource = new System.Uri(BlackBracket, UriKind.Relative) };
                this.image2.Source = new BitmapImage() { UriSource = new System.Uri(BlackBracket, UriKind.Relative) };
            }
            else
            {
                this.image1.Source = new BitmapImage() { UriSource = new System.Uri(WhiteBracket, UriKind.Relative) };
                this.image2.Source = new BitmapImage() { UriSource = new System.Uri(WhiteBracket, UriKind.Relative) };
            }
        }
    }
}

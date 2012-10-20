using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Matrix
{
    public partial class ImageButtonDelete : UserControl
    {
        private readonly bool _dark;
        private const string Black = "/Matrix;component/black/appbar.cancel.rest.png";
        private const string White = "/Matrix;component/Images/cancel.png";
        public ImageButtonDelete()
        {
            _dark = ((Visibility)Application.Current.Resources["PhoneDarkThemeVisibility"] == Visibility.Visible);
            InitializeComponent();
            this.image1.Source = !_dark ? new BitmapImage() { UriSource = new System.Uri(Black, UriKind.Relative) } : new BitmapImage() { UriSource = new System.Uri(White, UriKind.Relative) };
        }
    }
}

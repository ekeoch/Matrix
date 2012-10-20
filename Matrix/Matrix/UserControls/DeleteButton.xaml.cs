using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Matrix.UserControls
{
    public partial class DeleteButton : UserControl
    {
        private readonly bool _dark;
        private const string Black = "/Matrix;component/black/appbar.cancel.rest.png";
        private const string White = "/Matrix;component/Images/cancel.png";
        public DeleteButton()
        {
            _dark = ((Visibility)Application.Current.Resources["PhoneDarkThemeVisibility"] == Visibility.Visible);
            InitializeComponent();
            if (!_dark)
            {
                this.image1.Source = new BitmapImage() { UriSource = new System.Uri(Black, UriKind.Relative) };
            }
            else
            {
                this.image1.Source = new BitmapImage() { UriSource = new System.Uri(White, UriKind.Relative) };
            }
        }
    }
}

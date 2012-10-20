using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Matrix.UserControls
{
    public partial class NextButton : UserControl
    {
        private readonly bool _dark;
        private const string Black = "/Matrix;component/black/appbar.next.rest.png";
        private const string White = "/Matrix;component/Images/appbar.next.rest.png";
        public NextButton()
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

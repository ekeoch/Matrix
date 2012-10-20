﻿using System;
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

namespace Matrix
{
    public partial class ImageButton : UserControl
    {
        private readonly bool _dark;
        private const string Black = "/Matrix;component/black/appbar.add.rest.png";
        private const string White = "/Matrix;component/Images/appbar.add.rest.png";
        public ImageButton()
        {
            _dark = ((Visibility)Application.Current.Resources["PhoneDarkThemeVisibility"] == Visibility.Visible);
            InitializeComponent();
            this.image1.Source = !_dark ? new BitmapImage() { UriSource = new System.Uri(Black, UriKind.Relative) } : new BitmapImage() { UriSource = new System.Uri(White, UriKind.Relative) };
        }
    }
}

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
using System.Windows.Shapes;

namespace Matrix.UserControls
{
    public partial class SingleResult : UserControl
    {
        private readonly bool _dark;
        public SingleResult()
        {
            _dark = ((Visibility)Application.Current.Resources["PhoneDarkThemeVisibility"] == Visibility.Visible);
            InitializeComponent();
            this.textBox1.Foreground = _dark ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Black);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Matrix
{
    public partial class PopupControl : UserControl
    {
        #region FIELDS
        private const string One = "/Matrix;component/Numbers/1.jpg";
        private const string Two = "/Matrix;component/Numbers/2.jpg";
        private const string Three = "/Matrix;component/Numbers/3.jpg";
        private const string Four = "/Matrix;component/Numbers/4.jpg";
        private const string Five = "/Matrix;component/Numbers/5.png";
        private readonly List<BitmapImage> _paths;
        private int _selectedRow = 1;
        private int _selectedColumn = 1;
        /// <summary>
        /// Gets the selected row.
        /// </summary>
        public int SelectedRow
        {
            get { return _selectedRow; }
        }
        /// <summary>
        /// Gets the selected column.
        /// </summary>
        public int SelectedColumn
        {
            get { return _selectedColumn; }
        }
        /// <summary>
        /// Gets the name of the selected.
        /// </summary>
        /// <value>
        /// The name of the selected.
        /// </value>
        public string SelectedName
        {
            get { return this.textBox1.Text; }
        }
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Initializes a new instance of the <see cref="PopupControl"/> class.
        /// </summary>
        public PopupControl()
        {
            InitializeComponent();
            _paths = new List<BitmapImage>
                         {
                             new BitmapImage(new Uri(One, UriKind.Relative)),
                             new BitmapImage(new Uri(Two, UriKind.Relative)),
                             new BitmapImage(new Uri(Three, UriKind.Relative)),
                             new BitmapImage(new Uri(Four, UriKind.Relative)),
                             new BitmapImage(new Uri(Five, UriKind.Relative))
                         };
            
        }
        #endregion

        #region METHODS
        private void TextBox1GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void Slider1ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (image1 != null && image2 != null)
            {
                double value = Math.Round(e.NewValue);
                int i, j;
                int k = 0;
                for (i = 0; i < 5; i++)
                {
                    for (j = 0; j < 5; j++)
                    {
                        k++;
                        if (value.Equals(Convert.ToDouble(k)))
                        {
                            image1.Source = _paths[i];
                            image2.Source = _paths[j];
                            _selectedRow = i+1;
                            _selectedColumn = j + 1;
                            return;
                        }
                    }
                }
            }

        }
        #endregion
    }
}

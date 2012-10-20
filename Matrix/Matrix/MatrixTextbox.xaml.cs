using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Matrix
{
    public class MatrixTextbox : TextBox
    {
        #region FIELDS
        private readonly int _row;
        private readonly bool _dark;
        private readonly int _col;
        /// <summary>
        /// Gets the row.
        /// </summary>
        public int Row
        {
            get { return _row; }
        }

        /// <summary>
        /// Gets the column.
        /// </summary>
        public int Column
        {
            get { return _col; }
        }

        /// <summary>
        /// Gets the value of the Textbox converted
        /// to double
        /// </summary>
        public double Value
        {
            get
            {
                if (this.Text.Equals(string.Empty) || this.Text.Equals("-"))
                    return 0;
                else
                    try
                    { return Convert.ToDouble(this.Text); }
                    catch (Exception execption)
                    {
                        MessageBox.Show(execption.Message, "Error!", MessageBoxButton.OK);
                        this.Text = "0";
                        return 0;
                    }
            }
        }
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixTextbox"/> class.
        /// </summary>
        /// <param name="row">The row.</param>
        /// <param name="col">The col.</param>
        /// <param name="readOnly">if set to <c>true</c> [read only].</param>
        public MatrixTextbox(int row, int col, bool readOnly)
        {
             _dark = ((Visibility) Application.Current.Resources["PhoneDarkThemeVisibility"] == Visibility.Visible);
            if (readOnly)
            { this.IsReadOnly = true; }
            _row = row;
            _col = col;
            this.InputScope = new InputScope() { Names = { new InputScopeName() { NameValue = InputScopeNameValue.TelephoneNumber } } };
            this.KeyUp += new KeyEventHandler(MatrixTextboxKeyUp);
            this.HorizontalAlignment = HorizontalAlignment.Left;
            this.VerticalAlignment = VerticalAlignment.Top;
            this.Text = "0";
            this.TextAlignment = TextAlignment.Center;
            this.MaxLength = 7;
            this.Width = 110;
            this.Height = 72;           
            this.BorderBrush = null;
            this.Background = null;
            this.Foreground = _dark ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Black);          
            this.GotFocus += new RoutedEventHandler(MatrixTextboxGotFocus);
            this.LostFocus += new RoutedEventHandler(MatrixTextboxLostFocus);
            
        }
        #endregion

        #region METHODS
        void MatrixTextboxLostFocus(object sender, RoutedEventArgs e)
        {
            if (_dark) this.Foreground = new SolidColorBrush(Colors.White);
            else this.Foreground = new SolidColorBrush(Colors.Black);
        }

        void MatrixTextboxGotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
            this.Foreground = new SolidColorBrush(Colors.Black);
        }

        void MatrixTextboxKeyUp(object sender, KeyEventArgs e)
        {
            MaskNumericInput(this);
        }
        private static void MaskNumericInput(TextBox textbox)
        {
            string[] invalidChars = { "*", "#", ",", "+", "@" };
            foreach (string t in invalidChars)
            {
                textbox.Text = textbox.Text.Replace(t, "");
            }
        }
        #endregion
    }
}
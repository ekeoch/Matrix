using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Microsoft.Devices;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Matrix.Pages
{
    public partial class AddPage : PhoneApplicationPage
    {
        #region FIELDS
        private  Popup _pop;
        private readonly ListBoxMatrixItem _sampleA;
        private readonly ListBoxMatrixItem _sampleB;
        private readonly List<ListBoxMatrixItem> _listboxMatrixItems;
        readonly VibrateController _vibrate = VibrateController.Default;
        #endregion

        #region CONSTRUCTOR
        public AddPage()
        {
            InitializeComponent();
            
            button1.Content = new ImageButton();
            button2.Content = new ImageButtonDelete();
            _sampleA = new ListBoxMatrixItem(new MatrixView(2, 2, false) { MatrixName = "SampleA" });
            _sampleB = new ListBoxMatrixItem(new MatrixView(3, 3, false) { MatrixName = "SampleB" });
            _listboxMatrixItems = new List<ListBoxMatrixItem> {_sampleA, _sampleB};

            if (IsolatedStorageSettings.ApplicationSettings.Contains("userData"))
            {
                List<PortableMatrix> matrixList = IsolatedStorageSettings.ApplicationSettings["userData"] as List<PortableMatrix>;
                if (matrixList != null)
                    foreach (PortableMatrix matrix in matrixList)
                    {
                        MatrixLibrary.Matrix newMatrix = new MatrixLibrary.Matrix(matrix.ToTwoDimensionalArray(matrix.MatrixArray));
                        MatrixView mView = new MatrixView(newMatrix.NoCols, newMatrix.NoRows, false);
                        mView.UpdateMatrix(newMatrix);
                        mView.MatrixName = matrix.MatrixName;
                        _listboxMatrixItems.Add(new ListBoxMatrixItem(mView));
                    }
            }
            listBox1.ItemsSource = _listboxMatrixItems;
            this.listBox1.SelectedItem = _sampleB;          
            ApplicationBar = new ApplicationBar {IsVisible = true, Mode = ApplicationBarMode.Default, Opacity = 0.5};
            ApplicationBarIconButton saveButton = new ApplicationBarIconButton(){IconUri = new Uri("/Image/save.png", UriKind.Relative), IsEnabled = true, Text = "Save"};
            saveButton.Click += new EventHandler(SaveButtonClick);
            ApplicationBar.Buttons.Add(saveButton);
        }
        #endregion

        #region METHODS
        private void SaveButtonClick(object sender, EventArgs e)
        {
            IsolatedStorageSettings  settings = IsolatedStorageSettings.ApplicationSettings;
            List<PortableMatrix> portableMatrixList = (from ListBoxMatrixItem matrixitem in listBox1.Items.Where(param => !((ListBoxMatrixItem) param).Equals(_sampleA) && !((ListBoxMatrixItem) param).Equals(_sampleB))
                                                       select new PortableMatrix(matrixitem.MatrixView.Matrix, matrixitem.MatrixView.MatrixName)).ToList();
            if (!settings.Contains("userData"))
            {
                settings.Add("userData", portableMatrixList);
            }
            else
            {
                settings["userData"] = portableMatrixList;
            }
            settings.Save();
            MessageBox.Show("Settings Saved!", "Matrix", MessageBoxButton.OK);
        }
        private void ListBox1SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.grid1.Children.Clear();
            if (((ListBox)sender).SelectedItem != null)
                this.grid1.Children.Add(((ListBoxMatrixItem)((ListBox)sender).SelectedItem).MatrixView);
        }
        private void Button1Click(object sender, RoutedEventArgs e)
        {
            _vibrate.Start(TimeSpan.FromMilliseconds(10));
            _pop = new Popup
                       {
                           Child = new PopupControl(),
                           VerticalAlignment = System.Windows.VerticalAlignment.Center,
                           HorizontalAlignment = System.Windows.HorizontalAlignment.Center,
                           Margin = new Thickness(100, 100, 0, 0)
                       };
            this.IsEnabled = false;
            _pop.IsOpen = true;
            ((PopupControl)_pop.Child).button1.Click += new RoutedEventHandler(Bbutton1Click);
        }
        private void Bbutton1Click(object sender, RoutedEventArgs e)
        {
            _vibrate.Start(TimeSpan.FromMilliseconds(10));
            if (((PopupControl)_pop.Child).textBox1.Text == string.Empty)
            {
                ((PopupControl) _pop.Child).textBox1.Text = "Unamed Matrix";
            }
            MatrixView matrix = new MatrixView(((PopupControl) _pop.Child).SelectedColumn,
                                               ((PopupControl) _pop.Child).SelectedRow, false)
                                    {
                                        MatrixName = ((PopupControl) _pop.Child).SelectedName
                                    };
            ListBoxMatrixItem listboxmatrix = new ListBoxMatrixItem(matrix);
            _listboxMatrixItems.Add(listboxmatrix);
            listBox1.ItemsSource = null;
            listBox1.ItemsSource = _listboxMatrixItems;
            listBox1.SelectedItem = listboxmatrix;
            _pop.IsOpen = false;
            this.IsEnabled = true;
        }
        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            if (_pop != null)
                if (_pop.IsOpen)
                {
                    _pop.IsOpen = false;
                    this.IsEnabled = true;
                    e.Cancel = true;
                }
            base.OnBackKeyPress(e);
        }
        private void Button2Click(object sender, RoutedEventArgs e)
        {
            _vibrate.Start(TimeSpan.FromMilliseconds(10));
            if (listBox1.SelectedItem.Equals(_sampleA) || listBox1.SelectedItem.Equals(_sampleB))
                return;
            if (MessageBox.Show("Click Ok to confirm Delete", "Delete", MessageBoxButton.OKCancel).Equals(MessageBoxResult.OK))
            {
                _listboxMatrixItems.Remove((ListBoxMatrixItem)listBox1.SelectedItem);
                listBox1.ItemsSource = null;
                listBox1.ItemsSource = _listboxMatrixItems;
                listBox1.SelectedItem = _sampleB;
            }
        }
        #endregion
    }
}
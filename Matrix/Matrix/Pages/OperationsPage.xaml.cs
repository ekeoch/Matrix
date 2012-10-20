using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Matrix.Operations;
using Matrix.UserControls;
using Microsoft.Devices;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace Matrix.Pages
{
    public partial class OperationsPage : PhoneApplicationPage
    {
        #region FIELDS
        private readonly List<PortableMatrix> _matrixList;
        private readonly List<MatrixView> _matrixViews;
        private readonly Dictionary<int, SelectionOptions> _assignment;
        private readonly SoundEffect _effect;
        private readonly VibrateController _vibrate;
        private int _currentIndex;
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationsPage"/> class.
        /// </summary>
        public OperationsPage()
        {
            InitializeComponent();
            SetColors();
            _vibrate = VibrateController.Default;
            Stream stream = TitleContainer.OpenStream("Sounds/Windows Balloon.wav");
            _effect = SoundEffect.FromStream(stream);
            FrameworkDispatcher.Update();
            _assignment = new Dictionary<int, SelectionOptions>
                              {
                                  {0, new SelectionOptions(){MatrixView = GetEmptyMatrix(true), HasValue = false , Selected = false} },
                                  {1, new SelectionOptions(){MatrixView = GetEmptyMatrix(true), HasValue = false , Selected = false} }
                              };
            EnableMatrixA();
            button4.Content = new BackButton();
            button5.Content = new NextButton();
            button1.Content = new AddButton();
            button2.Content = new DeleteButton();
            button6.Content = new AddButton();
            button7.Content = new DeleteButton();

            List<Operations.Operations> source = new List<Operations.Operations>
                                          {
                                              new AddOperations(_assignment) ,
                                              new SubtractOperations(_assignment),
                                              new MultiplyOperations(_assignment) ,
                                              new SolveOperations(_assignment) ,
                                              new LuOperations(_assignment) ,
                                              new DeterminantOperations(_assignment) ,
                                              new EigenvaluesOperations(_assignment) ,
                                              new TransposeOperations(_assignment) , 
                                              new InverseOperations(_assignment)
                                          };
            this.listPicker1.ItemsSource = source;
            ApplicationBar = new ApplicationBar { IsVisible = true, Mode = ApplicationBarMode.Default, Opacity = 0.9 };
            listPicker1.SelectionChanged += new SelectionChangedEventHandler(ListPicker1SelectionChanged);
            ApplicationBarIconButton acceptButton = new ApplicationBarIconButton() { IconUri = new Uri("/Image/check.png", UriKind.Relative), IsEnabled = true, Text = "Accept" };
            acceptButton.Click += new EventHandler(AcceptButtonClick);
            ApplicationBar.Buttons.Add(acceptButton);
            if (IsolatedStorageSettings.ApplicationSettings.Contains("userData"))
            {
                _matrixList = IsolatedStorageSettings.ApplicationSettings["userData"] as List<PortableMatrix>;
                if (_matrixList != null && _matrixList.Count > 0)
                {
                    _matrixViews = new List<MatrixView>();
                    foreach (PortableMatrix portableMatrix in _matrixList)
                    {
                        _matrixViews.Add(ToMatrixView(portableMatrix));
                    }
                    this.grid3.Children.Add(_matrixViews[0]);
                    this.textBlock2.Text = _matrixViews[0].MatrixName;
                    _currentIndex = 0;
                }
            }
        }
        #endregion

        #region METHODS
        void ListPicker1SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplicationBar.MenuItems.Clear();
            foreach (ApplicationBarMenuItem applicationBarMenuItem in ((Operations.Operations)this.listPicker1.SelectedItem).ApplicationbarItems)
            {
                ApplicationBar.MenuItems.Add(applicationBarMenuItem);
            }
        }

        void AcceptButtonClick(object sender, EventArgs e)
        {
            VibrateController vibrate = VibrateController.Default;
            vibrate.Start(TimeSpan.FromMilliseconds(10));
            if (grid3.Children.Count.Equals(0))
            {
                MessageBox.Show("Select a Matrix First!", "Error", MessageBoxButton.OK);
                return;
            }
            if (grid3.Children[0] is SingleResult)
                return;
            IEnumerable<KeyValuePair<int, SelectionOptions>> keyValuePairs = _assignment.Where(p => p.Value.Selected);
            if (keyValuePairs.Count().Equals(1))
            {
                foreach (KeyValuePair<int, SelectionOptions> keyValuePair in keyValuePairs)
                {
                    if (keyValuePair.Key.Equals(0))
                    {
                        textBlock5.Text = ((MatrixView)grid3.Children[0]).MatrixName;
                        keyValuePair.Value.MatrixView = (MatrixView)grid3.Children[0];
                        keyValuePair.Value.HasValue = true;
                    }
                    else
                    {
                        textBlock6.Text = ((MatrixView)grid3.Children[0]).MatrixName;
                        keyValuePair.Value.MatrixView = (MatrixView)grid3.Children[0];
                        keyValuePair.Value.HasValue = true;
                    }
                }
            }
        }

        private void Button3Click(object sender, RoutedEventArgs e)
        {            
            _vibrate.Start(TimeSpan.FromMilliseconds(10));
            if (_matrixViews == null)
            {
                MessageBox.Show("Select a Matrix First!", "Error", MessageBoxButton.OK);
                return;
            }
            DeterminantOperations determinantOperations = listPicker1.SelectedItem as DeterminantOperations;
            if (determinantOperations != null)
            {
                SingleResult singleResult = determinantOperations.EvaluateSingleResult();             
                if (singleResult != null)
                {
                    grid3.Children.Clear();
                    grid3.Children.Add(singleResult);
                    textBlock2.Text = "Determinant";
                    _effect.Play();
                }
            }
            else
            {
                Result.Result result = ((Operations.Operations) listPicker1.SelectedItem).EvaluateResult();          
                if (result != null)
                {
                    grid3.Children.Clear();
                    grid3.Children.Add(result);
                    result.MatrixName = "Result" + DateTime.Now.ToLongTimeString();  
                    textBlock2.Text = result.MatrixName;                   
                    _matrixViews.Add(result);
                    _effect.Play();             
                }
            }

        }

        private static MatrixView ToMatrixView(PortableMatrix portableMatrix)
        {
            MatrixView mView = new MatrixView(portableMatrix.Columns, portableMatrix.Rows, false);
            mView.UpdateMatrix(new MatrixLibrary.Matrix(portableMatrix.ToTwoDimensionalArray(portableMatrix.MatrixArray)));
            mView.MatrixName = portableMatrix.MatrixName;
            return mView;
        }

        private void Button4Click(object sender, RoutedEventArgs e)
        {
            if (_matrixViews != null)
            {
                if (_currentIndex > 0)
                {
                    _currentIndex = _currentIndex - 1;
                    grid3.Children.Clear();
                    grid3.Children.Add(_matrixViews[_currentIndex]);
                    textBlock2.Text = _matrixViews[_currentIndex].MatrixName;
                }
                else
                {
                    _vibrate.Start(TimeSpan.FromMilliseconds(10));
                }
            }
            else
            {
                _vibrate.Start(TimeSpan.FromMilliseconds(10));
            }
        }

        private void Button5Click(object sender, RoutedEventArgs e)
        {
            if (_matrixViews != null)
            {
                if (_currentIndex < (_matrixViews.Count - 1))
                {
                    _currentIndex = _currentIndex + 1;
                    grid3.Children.Clear();
                    grid3.Children.Add(_matrixViews[_currentIndex]);
                    textBlock2.Text = _matrixViews[_currentIndex].MatrixName;
                }
                else
                {
                    _vibrate.Start(TimeSpan.FromMilliseconds(10));
                }
            }
            else
            {
                _vibrate.Start(TimeSpan.FromMilliseconds(10));
            }
        }

        private void Button1Click(object sender, RoutedEventArgs e)
        {
            _vibrate.Start(TimeSpan.FromMilliseconds(10));
            EnableMatrixA();
        }

        private void Button6Click(object sender, RoutedEventArgs e)
        {
            _vibrate.Start(TimeSpan.FromMilliseconds(10));
            EnableMatrixB();
        }

        private void Button2Click(object sender, RoutedEventArgs e)
        {
            _vibrate.Start(TimeSpan.FromMilliseconds(10));
            if (_assignment[0].Selected)
            {
                _assignment[0] = new SelectionOptions() { MatrixView = GetEmptyMatrix(true), Selected = true, HasValue = false };
                textBlock5.Text = _assignment[0].MatrixView.MatrixName;
                EnableMatrixA();
            }
            else
            {
                _assignment[1].Selected = false;
                _assignment[0] = new SelectionOptions() { MatrixView = GetEmptyMatrix(true), Selected = true, HasValue = false };
                textBlock5.Text = _assignment[0].MatrixView.MatrixName;
                EnableMatrixA();
            }
        }

        private void Button7Click(object sender, RoutedEventArgs e)
        {
            _vibrate.Start(TimeSpan.FromMilliseconds(10));
            if (_assignment[1].Selected)
            {
                _assignment[1] = new SelectionOptions() { MatrixView = GetEmptyMatrix(true), Selected = true, HasValue = false };
                textBlock6.Text = _assignment[1].MatrixView.MatrixName;
                EnableMatrixB();
            }
            else
            {
                _assignment[0].Selected = false;
                _assignment[1] = new SelectionOptions() { MatrixView = GetEmptyMatrix(true), Selected = true, HasValue = false };
                textBlock6.Text = _assignment[1].MatrixView.MatrixName;
                EnableMatrixB();
            }
        }

        private static MatrixView GetEmptyMatrix(bool isreadonly)
        {
            return new MatrixView(1, 1, isreadonly) { MatrixName = "Empty" };
        }

        private void EnableMatrixA()
        {
            button1.BorderBrush = new SolidColorBrush(Colors.Green);
            button6.BorderBrush = null;
            _assignment[0].Selected = true;
            _assignment[1].Selected = false;
        }

        private void EnableMatrixB()
        {
            button6.BorderBrush = new SolidColorBrush(Colors.Green);
            button1.BorderBrush = null;
            _assignment[1].Selected = true;
            _assignment[0].Selected = false;
        }

        private void SetColors()
        {
            bool dark = ((Visibility)Application.Current.Resources["PhoneDarkThemeVisibility"] == Visibility.Visible);
            if(dark)
            {
                this.textBlock5.Foreground = new SolidColorBrush(Colors.Yellow);
                this.textBlock6.Foreground = new SolidColorBrush(Colors.Yellow);
                this.textBlock2.Foreground = new SolidColorBrush(Colors.Green);
                this.listPicker1.Foreground = new SolidColorBrush(Colors.White);
            }else
            {
                this.textBlock5.Foreground = new SolidColorBrush(Colors.Red);
                this.textBlock6.Foreground = new SolidColorBrush(Colors.Red);
                this.textBlock2.Foreground = new SolidColorBrush(Colors.Purple);
                this.listPicker1.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private void TextBlock5Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            _vibrate.Start(TimeSpan.FromMilliseconds(10));
            EnableMatrixA();
        }

        private void TextBlock6Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            _vibrate.Start(TimeSpan.FromMilliseconds(10));
            EnableMatrixB();
        }
        #endregion
    }
}
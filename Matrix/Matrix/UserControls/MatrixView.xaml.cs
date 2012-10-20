using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MatrixLibrary;


namespace Matrix
{
    public partial class MatrixView : UserControl
    {
        #region FIELDS
        private readonly UserDefinedMatrix _userDefinedMatrix;
        private readonly MatrixLibrary.Matrix _matrix;
        private string _matrixName = "NONAME";
        /// <summary>
        /// Gets the matrix.
        /// </summary>
        public MatrixLibrary.Matrix Matrix
        {
            get { return _matrix; }
        }

        /// <summary>
        /// Gets or sets the name of the matrix.
        /// </summary>
        /// <value>
        /// The name of the matrix.
        /// </value>
        public string MatrixName
        {
            get { return _matrixName; }
            set { _matrixName = value; }
        }
        private const double XMargin = 20;
        private const double YMargin = 12;
        private readonly int _i;
        private readonly int _j;
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Initializes a new instance of the <see cref="MatrixView"/> class.
        /// </summary>
        /// <param name="cols">The cols.</param>
        /// <param name="rows">The rows.</param>
        /// <param name="readOnly">if set to <c>true</c> [read only].</param>
        public MatrixView(int cols, int rows, bool readOnly)
            : base()
        {
            InitializeComponent();
            _userDefinedMatrix = new UserDefinedMatrix();
            _userDefinedMatrix.image1.Height = rows * 72 + 40;
            _userDefinedMatrix.image2.Height = _userDefinedMatrix.image1.Height;
            _userDefinedMatrix.grid1.Width = cols * 110 + 40;
            _userDefinedMatrix.grid1.Height = _userDefinedMatrix.image1.Height;
            _userDefinedMatrix.image2.Margin =
                new Thickness(_userDefinedMatrix.grid1.Margin.Left + _userDefinedMatrix.grid1.Width + 6,
                              _userDefinedMatrix.image2.Margin.Top, 0, 0);
            _userDefinedMatrix.Width = _userDefinedMatrix.image2.Margin.Left + _userDefinedMatrix.image2.Width + 6;
            _userDefinedMatrix.Height = _userDefinedMatrix.image1.Height + 6;
            _matrix = new MatrixLibrary.Matrix(rows, cols);
            this.scrollViewer1.Content = _userDefinedMatrix;
            for (_i = 0; _i < cols; _i++)
            {
                for (_j = 0; _j < rows; _j++)
                {
                    MatrixTextbox matrixTextbox = new MatrixTextbox(_j, _i, readOnly);
                    matrixTextbox.Margin = new Thickness(XMargin + (_i * 116), YMargin + (_j * 78), 0, 0);
                    matrixTextbox.TextChanged += new TextChangedEventHandler(MatrixTextboxTextChanged);
                    _userDefinedMatrix.grid1.Children.Add(matrixTextbox);
                }
            }
        }

        #endregion

        #region METHODS
        void MatrixTextboxTextChanged(object sender, TextChangedEventArgs e)
        {
            _matrix[((MatrixTextbox)sender).Row, ((MatrixTextbox)sender).Column] = ((MatrixTextbox)sender).Value;
        }

        /// <summary>
        /// Updates the matrix.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        public void UpdateMatrix(MatrixLibrary.Matrix matrix)
        {
            foreach (MatrixTextbox matrixBox in _userDefinedMatrix.grid1.Children)
            {
                matrixBox.Text = matrix[matrixBox.Row, matrixBox.Column].ToString("#0.00");
                _matrix[matrixBox.Row, matrixBox.Column] = matrixBox.Value;
            }
        }
        #endregion
    }
}



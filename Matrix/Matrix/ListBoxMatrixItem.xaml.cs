namespace Matrix
{
    public class ListBoxMatrixItem
    {
        #region FIELDS
        /// <summary>
        /// Gets the name of the matrix.
        /// </summary>
        /// <value>
        /// The name of the matrix.
        /// </value>
        public string MatrixName
        {
            get { return _matrixView.MatrixName; }
        }

        private MatrixView _matrixView;
        /// <summary>
        /// Gets or sets the matrix view.
        /// </summary>
        /// <value>
        /// The matrix view.
        /// </value>
        public MatrixView MatrixView
        {
            get { return _matrixView; }
            set { _matrixView = value; }
        }

        /// <summary>
        /// Gets the size.
        /// </summary>
        public string Size
        {
            get { return GetSize(); }
        }
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Initializes a new instance of the <see cref="ListBoxMatrixItem"/> class.
        /// </summary>
        /// <param name="matrixView">The matrix view.</param>
        public ListBoxMatrixItem(MatrixView matrixView)
        {
            _matrixView = matrixView;
        }
        #endregion

        #region METHODS
        private string GetSize()
        {
            string temp = "[";
            temp += _matrixView.Matrix.NoRows;
            temp += "x";
            temp += _matrixView.Matrix.NoCols;
            temp += "]";
            return temp;
        }
        #endregion
    }
}
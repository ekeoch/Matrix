using System.Runtime.Serialization;

namespace Matrix
{
    [DataContract(Name = "portableMatrix", Namespace = "http://cldc.howard.edu")]
    public class PortableMatrix
    {
        #region FIELDS
        private string _matrixname;
        private  int _rows;
        private  int _cols;
        private double[] _matrixArray;

        /// <summary>
        /// Gets the name of the matrix.
        /// </summary>
        /// <value>
        /// The name of the matrix.
        /// </value>
        [DataMember(Name = "MatrixName")]
        public string MatrixName
        {
            get { return _matrixname; }
            set { _matrixname = value; }
        }

        /// <summary>
        /// Gets the matrix array.
        /// </summary>
        [DataMember(Name = "MatrixArray")]
        public double[] MatrixArray
        {
            get { return _matrixArray; }
            set { _matrixArray = value; }
        }

        /// <summary>
        /// Gets or sets the rows.
        /// </summary>
        /// <value>
        /// The rows.
        /// </value>
        [DataMember(Name = "Rows")]
        public int Rows
        {
            get { return _rows; }
            set { _rows = value; }
        }

        /// <summary>
        /// Gets or sets the columns.
        /// </summary>
        /// <value>
        /// The columns.
        /// </value>
        [DataMember(Name = "Columns")]
        public int Columns
        {
            get { return _cols; }
            set { _cols = value; }
        }
        #endregion

        #region CONSTRUCTOR
        /// <summary>
        /// Initializes a new instance of the <see cref="PortableMatrix"/> class.
        /// </summary>
        /// <param name="matrix">The matrix.</param>
        /// <param name="matrixname">The matrixname.</param>
        public PortableMatrix(MatrixLibrary.Matrix matrix, string matrixname)
        {
            _matrixname = matrixname;
            _rows = matrix.NoRows;
            _cols = matrix.NoCols;
            _matrixArray = ToSingleDimensionArray(matrix.toArray);
        }
        #endregion

        #region METHODS
        private static double[] ToSingleDimensionArray(double[,] array)
        {
            int width = array.GetLength(0);
            int height = array.GetLength(1);
            int k = 0;
            double[] tempArray = new double[width * height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    k++;
                    tempArray[k-1] = array[i, j];
                }
            }
            return tempArray;
        }

        /// <summary>
        /// Toes the two dimensional array.
        /// </summary>
        /// <param name="array">The array.</param>
        /// <returns></returns>
        public double[,] ToTwoDimensionalArray(double[] array)
        {
            double[,] tempArray = new double[_rows,_cols];
            int k = 0;
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _cols; j++)
                {
                    k++;
                    tempArray[i, j] = array[k-1];
                }
            }
            return tempArray;
        }
        #endregion
    }
}
namespace Matrix
{
    public class SelectionOptions
    {
        /// <summary>
        /// Gets or sets the matrix view.
        /// </summary>
        /// <value>
        /// The matrix view.
        /// </value>
        public MatrixView MatrixView
        { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SelectionOptions"/> is selected.
        /// </summary>
        /// <value>
        ///   <c>true</c> if selected; otherwise, <c>false</c>.
        /// </value>
        public bool Selected
        { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has value.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has value; otherwise, <c>false</c>.
        /// </value>
        public bool HasValue
        { get; set; }
    }
}
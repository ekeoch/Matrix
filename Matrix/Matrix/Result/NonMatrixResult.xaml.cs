using System.Collections.Generic;
using Matrix.UserControls;

namespace Matrix.Result
{
    public class NonMatrixResult : SingleResult
    {
        private readonly string _string;
        public NonMatrixResult(double result)
        {
            this.textBox1.Text = result.ToString("#0.00");
        }

        public NonMatrixResult(IEnumerable<double> result)
        {
            foreach (double d in result)
            {
                _string = _string + " " + d.ToString("#0.00");
            }
            this.textBox1.Text = _string;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Matrix.Pages;

namespace Matrix.Operations
{
    public class SolveOperations : Operations
    {
        public SolveOperations(Dictionary<int, SelectionOptions> matrices)
            : base(matrices)
        {
            this.Name = "Solve";
            this.Color = new SolidColorBrush(Colors.Magenta);
            this.Mode = OperatonMode.Na;
        }

        public override Result.Result EvaluateResult()
        {
            try
            {
                MatrixLibrary.Matrix matrix = MatrixLibrary.Matrix.SolveLinear(Matrices[0].MatrixView.Matrix, Matrices[1].MatrixView.Matrix);
                Result.Result matrixResult = new Result.Result(matrix.NoCols, matrix.NoRows, false);
                matrixResult.UpdateMatrix(matrix);
                return matrixResult;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK);
                return null;
            }
        }
    }
}
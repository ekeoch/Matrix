using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Matrix.Pages;
using Matrix.Result;

namespace Matrix.Operations
{
    public class MultiplyOperations : Operations
    {
        public MultiplyOperations(Dictionary<int, SelectionOptions> matrices)
            : base(matrices)
        {
            this.Name = "Multiply";
            this.Color = new SolidColorBrush(Colors.Purple);
            this.Mode = OperatonMode.Na;
        }

        public override Result.Result EvaluateResult()
        {
            try
            {
                MatrixLibrary.Matrix matrix = MatrixLibrary.Matrix.Multiply(Matrices[0].MatrixView.Matrix, Matrices[1].MatrixView.Matrix);
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
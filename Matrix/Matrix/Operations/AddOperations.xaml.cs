using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Matrix.Pages;

namespace Matrix.Operations
{
    public class AddOperations : Operations
    {
        public AddOperations(Dictionary<int, SelectionOptions> matrices)
            : base(matrices)
        {
            this.Name = "Add";
            this.Color = new SolidColorBrush(Colors.Green);
            this.Mode = OperatonMode.Na;
            this.Matrices = matrices;
        }

        public override Result.Result EvaluateResult()
        {
            try
            {
                MatrixLibrary.Matrix matrix = MatrixLibrary.Matrix.Add(Matrices[0].MatrixView.Matrix, Matrices[1].MatrixView.Matrix);
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
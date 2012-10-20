using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Matrix.Pages;
using Microsoft.Phone.Shell;

namespace Matrix.Operations
{
    public class TransposeOperations : Operations
    {
        public TransposeOperations(Dictionary<int, SelectionOptions> matrices)
            : base(matrices)
        {
            this.Name = "Transpose";
            this.Color = new SolidColorBrush(Colors.Brown);
            this.Mode = OperatonMode.A;
            ApplicationBarMenuItem matrixA = new ApplicationBarMenuItem("Transpose A");
            ApplicationBarMenuItem matrixB = new ApplicationBarMenuItem("Transpose B");
            matrixA.Click += new EventHandler(MatrixAClick);
            matrixB.Click += new EventHandler(MatrixBClick);
            this.MenuItems.Add(matrixA);
            this.MenuItems.Add(matrixB);
        }

        void MatrixBClick(object sender, EventArgs e)
        {
            this.Mode = OperatonMode.B;
        }

        void MatrixAClick(object sender, EventArgs e)
        {
            this.Mode = OperatonMode.A;
        }

        public override Result.Result EvaluateResult()
        {
            if (this.Mode.Equals(OperatonMode.A))
            {
                try
                {
                    MatrixLibrary.Matrix matrix = MatrixLibrary.Matrix.Transpose(Matrices[0].MatrixView.Matrix);
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
            else
            {
                try
                {
                    MatrixLibrary.Matrix matrix = MatrixLibrary.Matrix.Transpose(Matrices[1].MatrixView.Matrix);
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
}
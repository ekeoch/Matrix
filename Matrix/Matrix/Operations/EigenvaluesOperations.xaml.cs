using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Matrix.Pages;
using Microsoft.Phone.Shell;

namespace Matrix.Operations
{
    public class EigenvaluesOperations : Operations
    {
        public EigenvaluesOperations(Dictionary<int, SelectionOptions> matrices)
            : base(matrices)
        {
            this.Name = "Eigenvalues";
            this.Color = new SolidColorBrush(Colors.Brown);
            this.Mode = OperatonMode.A;
            ApplicationBarMenuItem matrixA = new ApplicationBarMenuItem("Eigen Values A");
            ApplicationBarMenuItem matrixB = new ApplicationBarMenuItem("Eigen Values B");
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
                    MatrixLibrary.Matrix eMatrix;
                    MatrixLibrary.Matrix vMatrix;
                    MatrixLibrary.Matrix.Eigen(Matrices[0].MatrixView.Matrix, out eMatrix, out vMatrix);
                    Result.Result result = new Result.Result(eMatrix.NoCols, eMatrix.NoRows, false);
                    result.UpdateMatrix(eMatrix);
                    return result;
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
                    MatrixLibrary.Matrix eMatrix;
                    MatrixLibrary.Matrix vMatrix;
                    MatrixLibrary.Matrix.Eigen(Matrices[1].MatrixView.Matrix, out eMatrix, out vMatrix);
                    Result.Result result = new Result.Result(eMatrix.NoCols, eMatrix.NoRows, false);
                    result.UpdateMatrix(eMatrix);
                    return result;
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
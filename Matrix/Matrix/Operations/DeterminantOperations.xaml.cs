using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Matrix.Pages;
using Matrix.Result;
using Matrix.UserControls;
using Microsoft.Phone.Shell;

namespace Matrix.Operations
{
    public class DeterminantOperations : Operations
    {
        public DeterminantOperations(Dictionary<int, SelectionOptions> matrices)
            : base(matrices)
        {
            this.Name = "Determinant";
            this.Color = new SolidColorBrush(Colors.Orange);
            this.Mode = OperatonMode.A;
            ApplicationBarMenuItem matrixA = new ApplicationBarMenuItem("Determinant A");
            ApplicationBarMenuItem matrixB = new ApplicationBarMenuItem("Determinant B");
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
            return null;
        }

        public SingleResult EvaluateSingleResult()
        {
            if (this.Mode.Equals(OperatonMode.A))
            {
                try
                {
                    double det = MatrixLibrary.Matrix.Det(Matrices[0].MatrixView.Matrix);
                    return new NonMatrixResult(det);
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
                    double det = MatrixLibrary.Matrix.Det(Matrices[1].MatrixView.Matrix);
                    return new NonMatrixResult(det);
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
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Matrix.Pages;
using Microsoft.Phone.Shell;

namespace Matrix.Operations
{
    public class LuOperations : Operations
    {
        public LuOperations(Dictionary<int, SelectionOptions> matrices)
            : base(matrices)
        {
            this.Name = "LU";
            this.Color = new SolidColorBrush(Colors.Cyan);
            this.Mode = OperatonMode.La;
            ApplicationBarMenuItem matrixLa = new ApplicationBarMenuItem("L from LU A");
            ApplicationBarMenuItem matrixUa = new ApplicationBarMenuItem("U from LU A");
            ApplicationBarMenuItem matrixLb = new ApplicationBarMenuItem("L from LU B");
            ApplicationBarMenuItem matrixUb = new ApplicationBarMenuItem("U from LU B");
            matrixUa.Click += new EventHandler(MatrixUaClick);
            matrixUb.Click += new EventHandler(MatrixUbClick);
            matrixLa.Click += new EventHandler(MatrixLaClick);
            matrixLb.Click += new EventHandler(MatrixLbClick);
            this.MenuItems.Add(matrixLa);
            this.MenuItems.Add(matrixLb);
            this.MenuItems.Add(matrixUa);
            this.MenuItems.Add(matrixUb);
        }

        void MatrixLbClick(object sender, EventArgs e)
        {
            this.Mode = OperatonMode.Lb;
        }

        void MatrixLaClick(object sender, EventArgs e)
        {
            this.Mode = OperatonMode.La;
        }

        void MatrixUbClick(object sender, EventArgs e)
        {
            this.Mode = OperatonMode.Ub;
        }

        void MatrixUaClick(object sender, EventArgs e)
        {
            this.Mode = OperatonMode.Ua;
        }

        public override Result.Result EvaluateResult()
        {
            MatrixLibrary.Matrix bMatrix;
            switch (this.Mode)
            {
                case OperatonMode.La:
                    bMatrix = Matrices[0].MatrixView.Matrix;
                    break;
                case OperatonMode.Lb:
                    bMatrix = Matrices[1].MatrixView.Matrix;
                    break;
                case OperatonMode.Ua:
                    bMatrix = Matrices[0].MatrixView.Matrix;
                    break;
                case OperatonMode.Ub:
                    bMatrix = Matrices[1].MatrixView.Matrix;
                    break;
                default:
                    bMatrix = Matrices[0].MatrixView.Matrix;
                    break;
            }
            try
            {
                MatrixLibrary.Matrix lMatrix;
                MatrixLibrary.Matrix uMatrix;
                MatrixLibrary.Matrix pMatrix;
                MatrixLibrary.Matrix.LU(bMatrix, out lMatrix, out uMatrix, out pMatrix);
                switch (this.Mode)
                {
                    case OperatonMode.La:
                        Result.Result resultla = new Result.Result(lMatrix.NoCols, lMatrix.NoRows, false);
                        resultla.UpdateMatrix(lMatrix);
                        return resultla;
                    case OperatonMode.Lb:
                        Result.Result resultlb = new Result.Result(lMatrix.NoCols, lMatrix.NoRows, false);
                        resultlb.UpdateMatrix(lMatrix);
                        return resultlb;
                    case OperatonMode.Ua:
                        Result.Result resultua = new Result.Result(uMatrix.NoCols, uMatrix.NoRows, false);
                        resultua.UpdateMatrix(uMatrix);
                        return resultua;
                    case OperatonMode.Ub:
                        Result.Result resultub = new Result.Result(uMatrix.NoCols, uMatrix.NoRows, false);
                        resultub.UpdateMatrix(uMatrix);
                        return resultub;
                    default:
                        Result.Result resultdla = new Result.Result(lMatrix.NoCols, lMatrix.NoRows, false);
                        resultdla.UpdateMatrix(lMatrix);
                        return resultdla;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK);
                return null;
            }
        }
    }
}
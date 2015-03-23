using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neural_Network_Package
{
    public partial class ConfusionMatrixForm : Form
    {
        public int[][] confusionMatrix;
        public string firstClassName, secondClassName;
        double overallAccuracy;
        int numClasses;
        string[] className;
        int[] classIndex;
        public ConfusionMatrixForm()
        {
            InitializeComponent();
        }

        public void setData()
        {
            ConfusionMatrixGridView.Columns.Add("", "");
            ConfusionMatrixGridView.Columns.Add(firstClassName, firstClassName);
            ConfusionMatrixGridView.Columns.Add(secondClassName, secondClassName);
            ConfusionMatrixGridView.Rows.Add(2);
            ConfusionMatrixGridView[0, 0].Value = firstClassName;
            ConfusionMatrixGridView[0, 1].Value = secondClassName;
            int correctCount = 0, allCount = 0;
            for (int row = 0; row < 2; row++)
            {
                for (int col = 1; col < 3; col++)
                {
                    ConfusionMatrixGridView[col, row].Value = confusionMatrix[row][col - 1];
                    allCount += confusionMatrix[row][col - 1];
                    if (row == col - 1)
                        correctCount += confusionMatrix[row][col - 1];
                }
            }

            overallAccuracy = 100 * correctCount;
            overallAccuracy /= allCount;
            //ConfusionMatrixGridView[1, 0].Value = confusionMatrix[0][0];
            //ConfusionMatrixGridView[1, 1].Value = confusionMatrix[0][1];
            //ConfusionMatrixGridView[2, 0].Value = confusionMatrix[1][0];
            //ConfusionMatrixGridView[2, 1].Value = confusionMatrix[1][1];
            //overallAccuracy = ((double)100.0 * confusionMatrix[0][0] + confusionMatrix[1][1]) / ((double)confusionMatrix[0][0] + confusionMatrix[0][1] + confusionMatrix[1][0] + confusionMatrix[1][1]);
            overallAccuracyTextBox.Text = overallAccuracy.ToString() + " %";
        }
    }
}

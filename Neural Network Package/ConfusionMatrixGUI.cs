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
        int classesCount;
        public int[][] confusionMatrix;
        //public string firstClassName, secondClassName;
        double overallAccuracy;
        string[] className;
        int[] classIndex;
        public ConfusionMatrixForm()
        {
            InitializeComponent();
        }

        public void setData(string [] classesNames)
        {

            classesCount = classesNames.Length;
            className = classesNames;
            ConfusionMatrixGridView.Columns.Add("", "");
            for (int i = 0; i < classesCount; i++)
            {
                ConfusionMatrixGridView.Columns.Add(className[i], className[i]);
            }
            ConfusionMatrixGridView.Rows.Add(classesCount);
            for (int i = 0; i < classesCount; i++)
            {
                ConfusionMatrixGridView[0, i].Value = className[i];
            }
            
            int correctCount = 0, allCount = 0;
            for (int row = 0; row < classesCount; row++)
            {
                for (int col = 1; col < classesCount + 1; col++)
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

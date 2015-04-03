using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Neural_Network_Package
{
    public partial class Single_layer_perceptron_GUI : Form
    {
        int[][] confusionMatrix;
        double[][] setosa;
        double[][] versicolor;
        double[][] virginica;
        double[][] trainingSet;
        double[][] testingSet;
        double[] minX, maxX;
        int[] trainingSetClasses;
        int[] testingSetClasses;
        int[] testingSetClassificationResult;
        int firstFeatureIndex, secondFeatureIndex;
        Dictionary<double, double> normalized;
        int firstClassIndex, secondClassIndex;
        string firstClassName, secondClassName;
        int trainingCount = 30, testingCount = 20;
        Single_Layer_Perceptron perceptron;

        public Single_layer_perceptron_GUI()
        {
            InitializeComponent();
            ClassesToClassifyComboBox.SelectedIndex = 0;
            firstFeatureCompoBox.SelectedIndex = 0;
            secondFeatureComboBox.SelectedIndex = 1;
            drawCharts();
        }

        public void drawCharts()
        {
            helperClass.readIrisFiles(ref setosa, ref versicolor,ref virginica);
            double[][] allData;
            allData = new double[setosa.Length + versicolor.Length + virginica.Length][];
            for (int i = 0; i < allData.Length; i++)
            {
                allData[i] = new double[4];
                for (int j = 0; j < 4; j++)
                {
                    if (i < setosa.Length)
                    {
                        allData[i][j] = setosa[i][j];
                    }
                    else if (i >= setosa.Length && i < setosa.Length + versicolor.Length)
                    {
                        allData[i][j] = versicolor[i - setosa.Length][j];
                    }
                    else
                    {
                        allData[i][j] = virginica[i - setosa.Length - versicolor.Length][j];
                    }
                }
            }

            helperClass.normalize(ref allData);

            for (int i = 0; i < allData.Length; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (i < setosa.Length)
                    {
                        setosa[i][j] = allData[i][j];
                    }
                    else if (i >= setosa.Length && i < setosa.Length + versicolor.Length)
                    {
                        versicolor[i - setosa.Length][j] = allData[i][j];
                    }
                    else
                    {
                        virginica[i - setosa.Length - versicolor.Length][j] = allData[i][j];
                    }
                }
            }

            //clear the graphs
            for (int i = 0; i < 4; i++)
            {
                x1x2Chart.Series[i].Points.Clear();
                x1x3Chart.Series[i].Points.Clear();
                x1x4Chart.Series[i].Points.Clear();
                x2x3Chart.Series[i].Points.Clear();
                x2x4Chart.Series[i].Points.Clear();
                x3x4Chart.Series[i].Points.Clear();
            }
            minX = new double[3];
            maxX = new double[3];
            for (int i = 0; i < 3; i++)
            {
                minX[i] = 100;
                maxX[i] = 0;
            }

            //draw the graphs
            for (int k = 0; k < 50; k++)
            {
                x1x2Chart.Series["setosa"].Points.AddXY(setosa[k][0], setosa[k][1]);
                x1x3Chart.Series["setosa"].Points.AddXY(setosa[k][0], setosa[k][2]);
                x1x4Chart.Series["setosa"].Points.AddXY(setosa[k][0], setosa[k][3]);
                x2x3Chart.Series["setosa"].Points.AddXY(setosa[k][1], setosa[k][2]);
                x2x4Chart.Series["setosa"].Points.AddXY(setosa[k][1], setosa[k][3]);
                x3x4Chart.Series["setosa"].Points.AddXY(setosa[k][2], setosa[k][3]);

                x1x2Chart.Series["versicolor"].Points.AddXY(versicolor[k][0], versicolor[k][1]);
                x1x3Chart.Series["versicolor"].Points.AddXY(versicolor[k][0], versicolor[k][2]);
                x1x4Chart.Series["versicolor"].Points.AddXY(versicolor[k][0], versicolor[k][3]);
                x2x3Chart.Series["versicolor"].Points.AddXY(versicolor[k][1], versicolor[k][2]);
                x2x4Chart.Series["versicolor"].Points.AddXY(versicolor[k][1], versicolor[k][3]);
                x3x4Chart.Series["versicolor"].Points.AddXY(versicolor[k][2], versicolor[k][3]);

                x1x2Chart.Series["virginica"].Points.AddXY(virginica[k][0], virginica[k][1]);
                x1x3Chart.Series["virginica"].Points.AddXY(virginica[k][0], virginica[k][2]);
                x1x4Chart.Series["virginica"].Points.AddXY(virginica[k][0], virginica[k][3]);
                x2x3Chart.Series["virginica"].Points.AddXY(virginica[k][1], virginica[k][2]);
                x2x4Chart.Series["virginica"].Points.AddXY(virginica[k][1], virginica[k][3]);
                x3x4Chart.Series["virginica"].Points.AddXY(virginica[k][2], virginica[k][3]);

                for (int z = 0; z < 3; z++)
                {
                    minX[z] = Math.Min(minX[z], setosa[k][z]);
                    minX[z] = Math.Min(minX[z], versicolor[k][z]);
                    minX[z] = Math.Min(minX[z], virginica[k][z]);

                    maxX[z] = Math.Max(maxX[z], setosa[k][z]);
                    maxX[z] = Math.Max(maxX[z], versicolor[k][z]);
                    maxX[z] = Math.Max(maxX[z], virginica[k][z]);
                }
                 
             }

        }

        private void assignClasses()
        {
            firstFeatureIndex = firstFeatureCompoBox.SelectedIndex;
            secondFeatureIndex = secondFeatureComboBox.SelectedIndex;
            if (firstFeatureIndex > secondFeatureIndex)
            {
                int temp = firstFeatureIndex;
                firstFeatureIndex = secondFeatureIndex;
                secondFeatureIndex = temp;
            }
   
            if (ClassesToClassifyComboBox.Text == "setosa and versicolor")
            {
                firstClassIndex = 0;
                secondClassIndex = 1;
                firstClassName = "setosa";
                secondClassName = "versicolor";   
            }
            else if (ClassesToClassifyComboBox.Text == "setosa and virginica")
            {
                firstClassIndex = 0;
                secondClassIndex = 2;
                firstClassName = "setosa";
                secondClassName = "virginica";
            }
            else
            {
                firstClassIndex = 1;
                secondClassIndex = 2;
                firstClassName = "versicolor";
                secondClassName = "virginica";
            }
        }

        private void fillData(ref double[][] X, ref int[] classes, int countToFill, int startIndex)
        {
            for (int sample = 0; sample < 2 * countToFill; sample++)
            {
                X[sample] = new double[2];
                if (sample < countToFill)
                {
                    classes[sample] = firstClassIndex;
                    if (firstClassIndex == 0)
                    {
                        X[sample][0] = setosa[startIndex + sample][firstFeatureIndex];
                        X[sample][1] = setosa[startIndex + sample][secondFeatureIndex];       
                    }
                    else if (firstClassIndex == 1)
                    {
                        X[sample][0] = versicolor[startIndex + sample][firstFeatureIndex];
                        X[sample][1] = versicolor[startIndex + sample][secondFeatureIndex];
                
                    }
                    else
                    {
                        X[sample][0] = virginica[startIndex + sample][firstFeatureIndex];
                        X[sample][1] = virginica[startIndex + sample][secondFeatureIndex];
                   
                    }

                }
                else
                {
                    classes[sample] = secondClassIndex;
                   
                    if (secondClassIndex == 0)
                    {
                        X[sample][0] = setosa[startIndex + sample - countToFill][firstFeatureIndex];
                        X[sample][1] = setosa[startIndex + sample - countToFill][secondFeatureIndex];
                    }
                    else if (secondClassIndex == 1)
                    {
                        X[sample][0] = versicolor[startIndex + sample - countToFill][firstFeatureIndex];
                        X[sample][1] = versicolor[startIndex + sample - countToFill][secondFeatureIndex];

                    }
                    else
                    {
                        X[sample][0] = virginica[startIndex + sample - countToFill][firstFeatureIndex];
                        X[sample][1] = virginica[startIndex + sample - countToFill][secondFeatureIndex];
                    }
                
                }

            }

        }

        private void startTrainingTextBox_Click(object sender, EventArgs e)
        {
            assignClasses();
            perceptron = new Single_Layer_Perceptron(2, 0.47, 300, firstClassIndex, secondClassIndex, 0.001);
            trainingSetClasses = new int[trainingCount * 2];
            trainingSet = new double[trainingCount * 2][];
            for (int i = 0; i < trainingSet.Length; i++)
                trainingSet[i] = new double[2];
            fillData(ref trainingSet, ref trainingSetClasses, trainingCount, 0);
            //normalize(ref trainingSet);
            perceptron.train(trainingSet, trainingSetClasses);
            double[] weights = perceptron.getWeights();
            //clear the drawn lines
            x1x2Chart.Series[3].Points.Clear();
            x1x3Chart.Series[3].Points.Clear();
            x1x4Chart.Series[3].Points.Clear();
            x2x3Chart.Series[3].Points.Clear();
            x2x4Chart.Series[3].Points.Clear();
            x3x4Chart.Series[3].Points.Clear();

            if (firstFeatureIndex == 0 && secondFeatureIndex == 1)
            {
                x1x2Chart.Series["Lines"].Points.AddXY(minX[0], -(weights[0] + weights[1] * minX[0]) / weights[2]);
                x1x2Chart.Series["Lines"].Points.AddXY(maxX[0], -(weights[0] + weights[1] * maxX[0]) / weights[2]);
            }
            else if (firstFeatureIndex == 0 && secondFeatureIndex == 2)
            {
                x1x3Chart.Series["Lines"].Points.AddXY(minX[0], -(weights[0] + weights[1] * minX[0]) / weights[2]);
                x1x3Chart.Series["Lines"].Points.AddXY(maxX[0], -(weights[0] + weights[1] * maxX[0]) / weights[2]);
            
            }
            else if (firstFeatureIndex == 0 && secondFeatureIndex == 3)
            {
                x1x4Chart.Series["Lines"].Points.AddXY(minX[0], -(weights[0] + weights[1] * minX[0]) / weights[2]);
                x1x4Chart.Series["Lines"].Points.AddXY(maxX[0], -(weights[0] + weights[1] * maxX[0]) / weights[2]);
            }
            else if (firstFeatureIndex == 1 && secondFeatureIndex == 2)
            {
                x2x3Chart.Series["Lines"].Points.AddXY(minX[1], -(weights[0] + weights[1] * minX[1]) / weights[2]);
                x2x3Chart.Series["Lines"].Points.AddXY(maxX[1], -(weights[0] + weights[1] * maxX[1]) / weights[2]);
            }
            else if (firstFeatureIndex == 1 && secondFeatureIndex == 3)
            {
                x2x4Chart.Series["Lines"].Points.AddXY(minX[1], -(weights[0] + weights[1] * minX[1]) / weights[2]);
                x2x4Chart.Series["Lines"].Points.AddXY(maxX[1], -(weights[0] + weights[1] * maxX[1]) / weights[2]);
            }
            else if (firstFeatureIndex == 2 && secondFeatureIndex == 3)
            {
                x3x4Chart.Series["Lines"].Points.AddXY(minX[2], -(weights[0] + weights[1] * minX[2]) / weights[2]);
                x3x4Chart.Series["Lines"].Points.AddXY(maxX[2], -(weights[0] + weights[1] * maxX[2]) / weights[2]);
            }        
        }

        private void startTestingTextBox_Click(object sender, EventArgs e)
        {
            assignClasses();
            testingSetClasses = new int[testingCount * 2];
            testingSet = new double[testingCount * 2][];
            for (int i = 0; i < testingSet.Length; i++)
                testingSet[i] = new double[2];

            fillData(ref testingSet, ref testingSetClasses, testingCount, trainingCount);
            //normalize(ref testingSet);
            testingSetClassificationResult = perceptron.testMultiple(testingSet);
            //to ensure that classes indices fits the confusion matrix indices
            for (int i = 0; i < testingSetClassificationResult.Length; i++)
            {
                if (testingSetClasses[i] == firstClassIndex)
                {
                    testingSetClasses[i] = 0;
                }
                if (testingSetClasses[i] == secondClassIndex)
                {
                    testingSetClasses[i] = 1;
                }

                if (testingSetClassificationResult[i] == firstClassIndex)
                {
                    testingSetClassificationResult[i] = 0;
                }
                if (testingSetClassificationResult[i] == secondClassIndex)
                {
                    testingSetClassificationResult[i] = 1;
                }
            }

            confusionMatrix = new int[2][];
            for (int i = 0; i < 2; i++)
                confusionMatrix[i] = new int[2];
            for (int i = 0; i < testingSetClassificationResult.Length; i++)
            {
                confusionMatrix[testingSetClasses[i]][testingSetClassificationResult[i]]++;
            }
            ConfusionMatrixForm CFM = new ConfusionMatrixForm();
            CFM.firstClassName = this.firstClassName;
            CFM.secondClassName = this.secondClassName;
            CFM.confusionMatrix = confusionMatrix;
            CFM.setData();
            CFM.Show();
        }

        private void normalize(ref double[] X)
        {
            double[] orig = new double[X.Length];
            for (int i = 0; i < X.Length; i++)
                orig[i] = X[i];
            double maxValue = 0;
            double mean;
            int count = 0;
            double sum = 0;
            for (int i = 0; i < X.Length; i++)
            {
                sum += X[i];
                count++;
            }

            mean = sum / count;
            for (int i = 0; i < X.Length; i++)
                   X[i] -= mean;

            for (int i = 0; i < X.Length; i++)
            {
               
                    maxValue = Math.Max(maxValue, X[i]);
                
            }
            normalized = new Dictionary<double, double>();
            for (int i = 0; i < X.Length; i++)
            {
                X[i] /= maxValue;
                normalized[orig[i]] = X[i];
            }
        }

        private void x1x2Chart_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            double x = x1x2Chart.ChartAreas[0].AxisX.PixelPositionToValue(me.X);
            double y = x1x2Chart.ChartAreas[0].AxisY.PixelPositionToValue(me.Y);
            singleTestTextBox.Text = classifySinglePoint(x, y);
        }


        private void x1x3Chart_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            double x = x1x3Chart.ChartAreas[0].AxisX.PixelPositionToValue(me.X);
            double y = x1x3Chart.ChartAreas[0].AxisY.PixelPositionToValue(me.Y);
            singleTestTextBox.Text = classifySinglePoint(x, y);
       

        }

        private void x1x4Chart_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            double x = x1x4Chart.ChartAreas[0].AxisX.PixelPositionToValue(me.X);
            double y = x1x4Chart.ChartAreas[0].AxisY.PixelPositionToValue(me.Y);
            singleTestTextBox.Text = classifySinglePoint(x, y);
       

        }

        private void x2x3Chart_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            double x = x2x3Chart.ChartAreas[0].AxisX.PixelPositionToValue(me.X);
            double y = x2x3Chart.ChartAreas[0].AxisY.PixelPositionToValue(me.Y);
            singleTestTextBox.Text = classifySinglePoint(x, y);

        }

        private void x2x4Chart_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            double x = x2x4Chart.ChartAreas[0].AxisX.PixelPositionToValue(me.X);
            double y = x2x4Chart.ChartAreas[0].AxisY.PixelPositionToValue(me.Y);
            singleTestTextBox.Text = classifySinglePoint(x, y);
       

        }

        private void x3x4Chart_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            double x = x3x4Chart.ChartAreas[0].AxisX.PixelPositionToValue(me.X);
            double y = x3x4Chart.ChartAreas[0].AxisY.PixelPositionToValue(me.Y);
            singleTestTextBox.Text = classifySinglePoint(x, y);
        }

        private void ClassesToClassifyComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            assignClasses();
        }

        private string classifySinglePoint(double x, double y)
        { 
            string ret;
            double[] feature = new double[2];
            feature[0] = x;
            feature[1] = y;
            int classificationRes = perceptron.testSingle(feature);
            if (classificationRes == firstClassIndex)
                ret = firstClassName;
            else 
                ret = secondClassName;
            return ret;
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            helperClass.mainWindow.Visible = true;
        }

    }
}

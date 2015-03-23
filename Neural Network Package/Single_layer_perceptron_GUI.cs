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
        double[] allData;
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
        int firstClass, secondClass;
        string firstClassName, secondClassName;
        int trainingCount = 30, testingCount = 20;
        Single_Layer_Perceptron perceptron;

        public Single_layer_perceptron_GUI()
        {
            InitializeComponent();
            ClassesToClassifyComboBox.SelectedIndex = 2;
            firstFeatureCompoBox.SelectedIndex = 0;
            secondFeatureComboBox.SelectedIndex = 3;
            drawCharts();
        }

        public void drawCharts()
        {
            FileStream FS = new FileStream("Iris Data.txt", FileMode.Open);
            StreamReader SR = new StreamReader(FS);
            string line;
            string[] numbers;
            setosa = new double[4][];
            versicolor = new double[4][];
            virginica = new double[4][];
            for (int i = 0; i < 4; i++)
            {
                setosa[i] = new double[50];
                versicolor[i] = new double[50];
                virginica[i] = new double[50];
            }
            SR.ReadLine();
            allData = new double[600];
            int curIndex = 0;
            //read setosa data
            for (int i = 0; i < 50; i++)
            {
                line = SR.ReadLine();
                numbers = line.Split(',');
                for (int j = 0; j < 4; j++)
                {
                    setosa[j][i] = double.Parse(numbers[j]);
                    allData[curIndex++] = setosa[j][i];
                }
            }
            //read versicolor data
            for (int i = 0; i < 50; i++)
            {
                line = SR.ReadLine();
                numbers = line.Split(',');
                for (int j = 0; j < 4; j++)
                {
                    versicolor[j][i] = double.Parse(numbers[j]);
                    allData[curIndex++] = versicolor[j][i];
                }
            }

            //read virginica data

            for (int i = 0; i < 50; i++)
            {
                line = SR.ReadLine();
                numbers = line.Split(',');
                for (int j = 0; j < 4; j++)
                {
                    virginica[j][i] = double.Parse(numbers[j]);
                    allData[curIndex++] = virginica[j][i];
                }
            }

            SR.Close();
            FS.Close();

            for (int i = 0; i < 4; i++)
            { 
                
            }
            //normalize(ref allData);

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
                x1x2Chart.Series["setosa"].Points.AddXY(setosa[0][k], setosa[1][k]);
                x1x3Chart.Series["setosa"].Points.AddXY(setosa[0][k], setosa[2][k]);
                x1x4Chart.Series["setosa"].Points.AddXY(setosa[0][k], setosa[3][k]);
                x2x3Chart.Series["setosa"].Points.AddXY(setosa[1][k], setosa[2][k]);
                x2x4Chart.Series["setosa"].Points.AddXY(setosa[1][k], setosa[3][k]);
                x3x4Chart.Series["setosa"].Points.AddXY(setosa[2][k], setosa[3][k]);

                x1x2Chart.Series["versicolor"].Points.AddXY(versicolor[0][k], versicolor[1][k]);
                x1x3Chart.Series["versicolor"].Points.AddXY(versicolor[0][k], versicolor[2][k]);
                x1x4Chart.Series["versicolor"].Points.AddXY(versicolor[0][k], versicolor[3][k]);
                x2x3Chart.Series["versicolor"].Points.AddXY(versicolor[1][k], versicolor[2][k]);
                x2x4Chart.Series["versicolor"].Points.AddXY(versicolor[1][k], versicolor[3][k]);
                x3x4Chart.Series["versicolor"].Points.AddXY(versicolor[2][k], versicolor[3][k]);

                x1x2Chart.Series["virginica"].Points.AddXY(virginica[0][k], virginica[1][k]);
                x1x3Chart.Series["virginica"].Points.AddXY(virginica[0][k], virginica[2][k]);
                x1x4Chart.Series["virginica"].Points.AddXY(virginica[0][k], virginica[3][k]);
                x2x3Chart.Series["virginica"].Points.AddXY(virginica[1][k], virginica[2][k]);
                x2x4Chart.Series["virginica"].Points.AddXY(virginica[1][k], virginica[3][k]);
                x3x4Chart.Series["virginica"].Points.AddXY(virginica[2][k], virginica[3][k]);

                for (int z = 0; z < 3; z++)
                {
                    minX[z] = Math.Min(minX[z], setosa[z][k]);
                    minX[z] = Math.Min(minX[z], versicolor[z][k]);
                    minX[z] = Math.Min(minX[z], virginica[z][k]);

                    maxX[z] = Math.Max(maxX[z], setosa[z][k]);
                    maxX[z] = Math.Max(maxX[z], versicolor[z][k]);
                    maxX[z] = Math.Max(maxX[z], virginica[z][k]);
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
                firstClass = 0;
                secondClass = 1;
                firstClassName = "setosa";
                secondClassName = "versicolor";
               
            }
            else if (ClassesToClassifyComboBox.Text == "setosa and virginica")
            {
                firstClass = 0;
                secondClass = 2;
                firstClassName = "setosa";
                secondClassName = "virginica";
            }
            else
            {
                firstClass = 1;
                secondClass = 2;
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
                    classes[sample] = firstClass;
                    if (firstClass == 0)
                    {
                        X[sample][0] = setosa[firstFeatureIndex][startIndex + sample];
                        X[sample][1] = setosa[secondFeatureIndex][startIndex + sample];       
                    }
                    else if (firstClass == 1)
                    {
                        X[sample][0] = versicolor[firstFeatureIndex][startIndex + sample];
                        X[sample][1] = versicolor[secondFeatureIndex][startIndex + sample];
                
                    }
                    else
                    {
                        X[sample][0] = virginica[firstFeatureIndex][startIndex + sample];
                        X[sample][1] = virginica[secondFeatureIndex][startIndex + sample];
                   
                    }

                    //for (int feature = 0; feature < 2; feature++)
                    //{
                    //    if (firstClass == 0)
                    //    {
                    //        X[sample][feature] = normalized[setosa[startIndex + sample][feature]];
                    //    }
                    //    else if (firstClass == 1)
                    //    {
                    //        X[sample][feature] = normalized[versicolor[startIndex + sample][feature]];
                    //    }
                    //    else
                    //    {
                    //        X[sample][feature] = normalized[virginica[startIndex + sample][feature]];
                    //    }
                    //}
                }
                else
                {
                    classes[sample] = secondClass;
                   
                    if (secondClass == 0)
                    {
                        X[sample][0] = setosa[firstFeatureIndex][startIndex + sample - countToFill];
                        X[sample][1] = setosa[secondFeatureIndex][startIndex + sample - countToFill];
                    }
                    else if (secondClass == 1)
                    {
                        X[sample][0] = versicolor[firstFeatureIndex][startIndex + sample - countToFill];
                        X[sample][1] = versicolor[secondFeatureIndex][startIndex + sample - countToFill];

                    }
                    else
                    {
                        X[sample][0] = virginica[firstFeatureIndex][startIndex + sample - countToFill];
                        X[sample][1] = virginica[secondFeatureIndex][startIndex + sample - countToFill];
                    }
                    //for (int feature = 0; feature < 4; feature++)
                    //{
                    //    if (secondClass == 0)
                    //    {
                    //        X[sample][feature] = normalized[setosa[startIndex + sample - countToFill][feature]];
                    //    }
                    //    else if (secondClass == 1)
                    //    {
                    //        X[sample][feature] = normalized[versicolor[startIndex + sample - countToFill][feature]];
                    //    }
                    //    else
                    //    {
                    //        X[sample][feature] = normalized[virginica[startIndex + sample - countToFill][feature]];
                    //    }
                    //}

                }

            }

        }

        private void startTrainingTextBox_Click(object sender, EventArgs e)
        {
            assignClasses();
            perceptron = new Single_Layer_Perceptron(2, 0.47, 300, firstClass, secondClass, 0.001);
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
                if (testingSetClasses[i] == firstClass)
                {
                    testingSetClasses[i] = 0;
                }
                if (testingSetClasses[i] == secondClass)
                {
                    testingSetClasses[i] = 1;
                }

                if (testingSetClassificationResult[i] == firstClass)
                {
                    testingSetClassificationResult[i] = 0;
                }
                if (testingSetClassificationResult[i] == secondClass)
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
            if (classificationRes == firstClass)
                ret = firstClassName;
            else 
                ret = secondClassName;
            return ret;
        }

    }
}

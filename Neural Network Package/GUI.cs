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
using System.Windows.Forms.DataVisualization.Charting;

namespace Neural_Network_Package
{
    public partial class GUI : Form
    {
        int[][] confusionMatrix;
        double[][] setosa;
        double[][] versicolor;
        double[][] virginica;
        double[][] trainingSet;
        double[][] testingSet;
        int[] trainingSetClasses;
        int[] testingSetClasses;
        int[] testingSetClassificationResult;
        int firstClass, secondClass;
        string firstClassName, secondClassName;
        int trainingCount = 30, testingCount = 20;
        Single_Layer_Perceptron perceptron;

        public GUI()
        {
            InitializeComponent();
            ClassesToClassifyComboBox.SelectedIndex = 0;
            drawGraphs();
        }

        public void drawGraphs()
        {
            FileStream FS = new FileStream("Iris Data.txt", FileMode.Open);
            StreamReader SR = new StreamReader(FS);
            string line;
            string[] numbers;
            setosa = new double[50][];
            versicolor = new double[50][];
            virginica = new double[50][];
            for (int i = 0; i < 50; i++)
            {
                setosa[i] = new double[4];
                versicolor[i] = new double[4];
                virginica[i] = new double[4];
            }
            SR.ReadLine();
            //read setosa data
            for (int i = 0; i < 50; i++)
            {
                line = SR.ReadLine();
                numbers = line.Split(',');
                for (int j = 0; j < 4; j++)
                    setosa[i][j] = double.Parse(numbers[j]);
            }
            //read versicolor data
            for (int i = 0; i < 50; i++)
            {
                line = SR.ReadLine();
                numbers = line.Split(',');
                for (int j = 0; j < 4; j++)
                    versicolor[i][j] = double.Parse(numbers[j]);
            }

            //read virginica data

            for (int i = 0; i < 50; i++)
            {
                line = SR.ReadLine();
                numbers = line.Split(',');
                for (int j = 0; j < 4; j++)
                    virginica[i][j] = double.Parse(numbers[j]);
            }

            SR.Close();
            FS.Close();

            x1x2Graph.ChartAreas.Add(new ChartArea("line"));
            x1x3Graph.ChartAreas.Add(new ChartArea("line"));
            x1x4Graph.ChartAreas.Add(new ChartArea("line"));
            x2x3Graph.ChartAreas.Add(new ChartArea("line"));
            x2x4Graph.ChartAreas.Add(new ChartArea("line"));
            x3x4Graph.ChartAreas.Add(new ChartArea("line"));
            //clear the graphs
            for (int i = 0; i < 3; i++)
            {
                x1x2Graph.Series[i].Points.Clear();
                x1x3Graph.Series[i].Points.Clear();
                x1x4Graph.Series[i].Points.Clear();
                x2x3Graph.Series[i].Points.Clear();
                x2x4Graph.Series[i].Points.Clear(); 
                x3x4Graph.Series[i].Points.Clear();
            }
            //draw the graphs
            for (int k = 0; k < 50; k++)
            {
                x1x2Graph.Series["setosa"].Points.AddXY(setosa[k][0], setosa[k][1]);
                x1x3Graph.Series["setosa"].Points.AddXY(setosa[k][0], setosa[k][2]);
                x1x4Graph.Series["setosa"].Points.AddXY(setosa[k][0], setosa[k][3]);
                x2x3Graph.Series["setosa"].Points.AddXY(setosa[k][1], setosa[k][2]);
                x2x4Graph.Series["setosa"].Points.AddXY(setosa[k][1], setosa[k][3]);
                x3x4Graph.Series["setosa"].Points.AddXY(setosa[k][2], setosa[k][3]);

                x1x2Graph.Series["versicolor"].Points.AddXY(versicolor[k][0], versicolor[k][1]);
                x1x3Graph.Series["versicolor"].Points.AddXY(versicolor[k][0], versicolor[k][2]);
                x1x4Graph.Series["versicolor"].Points.AddXY(versicolor[k][0], versicolor[k][3]);
                x2x3Graph.Series["versicolor"].Points.AddXY(versicolor[k][1], versicolor[k][2]);
                x2x4Graph.Series["versicolor"].Points.AddXY(versicolor[k][1], versicolor[k][3]);
                x3x4Graph.Series["versicolor"].Points.AddXY(versicolor[k][2], versicolor[k][3]);

                x1x2Graph.Series["virginica"].Points.AddXY(virginica[k][0], virginica[k][1]);
                x1x3Graph.Series["virginica"].Points.AddXY(virginica[k][0], virginica[k][2]);
                x1x4Graph.Series["virginica"].Points.AddXY(virginica[k][0], virginica[k][3]);
                x2x3Graph.Series["virginica"].Points.AddXY(virginica[k][1], virginica[k][2]);
                x2x4Graph.Series["virginica"].Points.AddXY(virginica[k][1], virginica[k][3]);
                x3x4Graph.Series["virginica"].Points.AddXY(virginica[k][2], virginica[k][3]);
               
            }

        }

        private void assignClasses()
        {
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

                X[sample] = new double[4];
                if (sample < countToFill)
                {
                    classes[sample] = firstClass;
                    for (int feature = 0; feature < 4; feature++)
                    {
                        if (firstClass == 0)
                        {
                            X[sample][feature] = setosa[startIndex + sample][feature];
                        }
                        else if (firstClass == 1)
                        {
                            X[sample][feature] = versicolor[startIndex + sample][feature];
                        }
                        else
                        {
                            X[sample][feature] = virginica[startIndex + sample][feature];
                        }
                    }
                }
                else
                {
                    classes[sample] = secondClass;
                    for (int feature = 0; feature < 4; feature++)
                    {
                        if (secondClass == 0)
                        {
                            X[sample][feature] = setosa[startIndex + sample - countToFill][feature];
                        }
                        else if (secondClass == 1)
                        {
                            X[sample][feature] = versicolor[startIndex + sample - countToFill][feature];
                        }
                        else
                        {
                            X[sample][feature] = virginica[startIndex + sample - countToFill][feature];
                        }
                    }

                }

            }

        }

        private void startTrainingButton_Click(object sender, EventArgs e)
        {
            assignClasses();
            perceptron = new Single_Layer_Perceptron(4, 0.2, 200, firstClass, secondClass);
            trainingSetClasses = new int[trainingCount * 2];
            trainingSet = new double[trainingCount * 2][];
            for (int i = 0; i < trainingSet.Length; i++)
                trainingSet[i] = new double[4];
            fillData(ref trainingSet, ref trainingSetClasses, trainingCount, 0);
            perceptron.train(trainingSet, trainingSetClasses);
        }

        private void startTestingButton_Click(object sender, EventArgs e)
        {
            assignClasses();
            testingSetClasses = new int[testingCount * 2];
            testingSet = new double[testingCount * 2][];
            for (int i = 0; i < testingSet.Length; i++)
                testingSet[i] = new double[4];
            
            fillData(ref testingSet, ref testingSetClasses, testingCount, trainingCount);
            int[] classificationRes = perceptron.testMultiple(testingSet, testingSetClasses);
            confusionMatrix = new int[3][];
            for (int i = 0; i < 3; i++)
                confusionMatrix[i] = new int[3];
            for (int i = 0; i < classificationRes.Length; i++)
            {
                confusionMatrix[testingSetClasses[i]][classificationRes[i]]++;
            }
            ConfusionMatrixForm CFM = new ConfusionMatrixForm();
            CFM.confusionMatrix = confusionMatrix;
            CFM.setData();
            CFM.Show();
        }


    }
}

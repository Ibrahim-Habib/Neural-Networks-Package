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
    public partial class Multi_Layer_Perceptron_GUI : Form
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
        int trainingCount, testingCount;
        int num_of_features, num_of_classes;
        Multi_Layer_Perceptron perceptron;
        public Multi_Layer_Perceptron_GUI()
        {
            InitializeComponent();
            initialWeightTextBox.Text = "0.1";
            slopeTextBox.Text = "1";
            numOfIterationsTextBox.Text = "500";
            HiddenLayersCountComboBox.SelectedIndex = 0;
            num_of_classes = 3;
            num_of_features = 4;
            trainingCount = 90;
            testingCount = 60;
            helperClass.readIrisFiles(ref setosa, ref versicolor, ref virginica);
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

            trainingSet = new double[trainingCount][];
            testingSet = new double[testingCount][];
            trainingSetClasses = new int[trainingCount];
            testingSetClasses = new int[testingCount];
            for (int i = 0; i < trainingSet.Length; i++)
            {
                trainingSet[i] = new double[num_of_features];
            }

            for (int i = 0; i < testingSet.Length; i++)
            {
                testingSet[i] = new double[num_of_features];
            }

            for (int i = 0; i < trainingSet.Length; i++)
            {
                if (i % 3 == 0)
                {
                    trainingSet[i] = setosa[i / 3];
                }
                else if (i % 3 == 1)
                {
                    trainingSet[i] = versicolor[i / 3];
                }
                else
                {
                    trainingSet[i] = virginica[i / 3];
                }
                trainingSetClasses[i] = i % 3;
            }

            
            for (int i = 0; i < testingSet.Length; i++)
            {
                if (i % 3 == 0)
                {
                    testingSet[i] = setosa[(i + 90) / 3];
                }
                else if (i % 3 == 1)
                {
                    testingSet[i] = versicolor[(i + 90) / 3];
                }
                else
                {
                    testingSet[i] = virginica[(i + 90) / 3];
                }
                testingSetClasses[i] = i % 3;
            }

        }

        private void HiddenLayersCountComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int hiddeLayersCount =int.Parse(HiddenLayersCountComboBox.Text);

            while (hiddeLayersCount < numOfNeuronGridView.Rows.Count)
            {
                numOfNeuronGridView.Rows.RemoveAt(numOfNeuronGridView.Rows.Count - 1);
            }

            while (hiddeLayersCount > numOfNeuronGridView.Rows.Count)
            {
                numOfNeuronGridView.Rows.Add();
            }

            for (int i = 0; i < numOfNeuronGridView.Rows.Count; i++)
            {
                numOfNeuronGridView[0, i].Value = "Layer #" +(i + 1).ToString();
            }
            
        }

        private void trainButton_Click(object sender, EventArgs e)
        {
            int hiddenLayersCount = int.Parse(HiddenLayersCountComboBox.Text);
            int[] temp = new int[hiddenLayersCount];
            for(int i = 0; i < temp.Length; i++)
            {
                temp[i] = int.Parse(numOfNeuronGridView[1, i].Value.ToString());
            }
            perceptron = new Multi_Layer_Perceptron(num_of_classes, num_of_features, hiddenLayersCount, temp, double.Parse(slopeTextBox.Text), double.Parse(initialWeightTextBox.Text));
            double eta = double.Parse(etaTextBOx.Text);
            perceptron.train(trainingSet, trainingSetClasses, eta, 0.01, int.Parse(numOfIterationsTextBox.Text));
            MessageBox.Show("Training Done Successfully");
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            helperClass.mainWindow.Visible = true;
      
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            confusionMatrix = new int[num_of_classes][];
            for (int i = 0; i < confusionMatrix.Length; i++)
            {
                confusionMatrix[i] = new int[num_of_classes];
            }
            testingSetClassificationResult = perceptron.testMultiple(testingSet);
            for (int i = 0; i < testingSetClassificationResult.Length; i++)
            {
                confusionMatrix[testingSetClasses[i]][testingSetClassificationResult[i]]++;
            }

            ConfusionMatrixForm CFM = new ConfusionMatrixForm();
            CFM.confusionMatrix = confusionMatrix;
            string[] names = new string[3];
            names[0] = "Setosa";
            names[1] = "Versicolor";
            names[2] = "Virginica";
            CFM.setData(names);
            CFM.Show();
        }
    }
}

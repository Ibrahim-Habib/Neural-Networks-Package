using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neural_Network_Package
{
    class Multi_Layer_Perceptron
    {
        

        private int classesCount;
        private int featuresCount;
        private int hiddenLayersCount;
        private int[] neuronsCount;
        private double a;
        private double eta;
        private double[][][] weight;
        private double[][] nodeValue;
        private double[][] gradient;
        private double mseThreshold;

        Multi_Layer_Perceptron(int num_of_classes, int num_of_features, int num_of_hidden_Layers, int[] neuron_count, double learning_rate, double threshold)
        {
            mseThreshold = threshold;
            hiddenLayersCount = num_of_hidden_Layers;
            neuronsCount = neuron_count;
            a = 1;
            eta = learning_rate;
            classesCount = num_of_classes;
            featuresCount = num_of_features;
            hiddenLayersCount = num_of_hidden_Layers;
            #region initializtions
            weight = new double[hiddenLayersCount + 1][][];
            for (int layer = 0; layer < weight.Length; layer++)
            {
                if (layer == 0)
                {
                    weight[layer] = new double[num_of_features + 1][];
                    // + 1 for the bias
                }
                else
                {
                    weight[layer] = new double[neuronsCount[layer - 1] + 1][];
                    // + 1 for the bias
                }

                for (int neuron = 0; neuron < weight[layer].Length; neuron++)
                {
                    if (layer < weight.Length - 1)
                        weight[layer][neuron] = new double[neuronsCount[layer]];
                    else
                        weight[layer][neuron] = new double[num_of_classes];
                }
            }

            nodeValue = new double[hiddenLayersCount + 2][];
            gradient = new double[hiddenLayersCount + 1][];
            for (int i = 0; i < nodeValue.Length; i++)
            {
                if (i == 0)
                {
                    nodeValue[i] = new double[featuresCount];
                }
                else if (i == nodeValue.Length - 1)
                {
                    nodeValue[i] = new double[classesCount];
                    gradient[i] = new double[classesCount];
                }
                else
                {
                    nodeValue[i] = new double[neuronsCount[i - 1]];
                    gradient[i] = new double[neuron_count[i - 1]];
                }
            }

            #endregion

        }

        public void train(double[][] trainingSample, int[] sampleClass)
        {
            for (int sample = 0; sample < trainingSample.Length; sample++)
            {
                for (int feature = 0; feature < trainingSample[sample].Length; feature++)
                {
                    nodeValue[0][feature] = trainingSample[sample][feature];
                }
            }
            
            //forward phase
            for (int layer = 0; layer < hiddenLayersCount + 1; layer++)
            {
                //summation
                for (int curNode = -1; curNode < nodeValue[layer].Length + 1; curNode++)
                {
                    for (int nextNode = 0; nextNode < nodeValue[layer + 1].Length; nextNode++)
                    {
                        if (curNode == -1)
                        {
                            nodeValue[layer + 1][nextNode] += weight[layer][curNode + 1][curNode];
                        }
                        else
                        {
                            nodeValue[layer + 1][nextNode] += nodeValue[layer][curNode] * weight[layer][curNode + 1][nextNode];
                        }
                    }  
                }

                //activation function
                for (int nextNode = 0; nextNode < nodeValue[layer + 1].Length; nextNode++)
                {
                    nodeValue[layer + 1][nextNode] = activationFunction(nodeValue[layer + 1][nextNode]);
                }
            }

            //backward phase
            for (int layer = hiddenLayersCount; layer >= 0; layer--)
            { 
            
            }

            //weight updating



        }

        public int testSingle(double[] featuresVector)
        {

            return -1;
        }

        public int[] testMultiple(double[][] featuresVectors)
        {
            return null;
        }

        private double activationFunction(double sum)
        { 
            return 1 / (1 + Math.Exp(- a * sum));
        }
    }
}

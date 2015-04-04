using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neural_Network_Package
{
    class Multi_Layer_Perceptron
    {
        private Random rnd;
        private double initWeight;
        private int maxNumOfTrainingIterations;
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
        private double mseInIteration;
        private double[] desired;

        public Multi_Layer_Perceptron(int num_of_classes, int num_of_features, int num_of_hidden_Layers, int[] neuron_count, double slope, double initialWeight)
        {
            hiddenLayersCount = num_of_hidden_Layers;
            neuronsCount = neuron_count;
            initWeight = initialWeight;
            desired = new double[num_of_classes];
            a = slope;
            classesCount = num_of_classes;
            featuresCount = num_of_features;
            hiddenLayersCount = num_of_hidden_Layers;
            rnd = new Random();
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

                    for (int i = 0; i < weight[layer][neuron].Length; i++)
                    {
                        weight[layer][neuron][i] = initWeight;
                    }
                }
            }

            nodeValue = new double[hiddenLayersCount + 2][];
            for (int i = 0; i < nodeValue.Length; i++)
            {
                if (i == 0)
                {
                    nodeValue[i] = new double[featuresCount];
                }
                else if (i == nodeValue.Length - 1)
                {
                    nodeValue[i] = new double[classesCount];
                }
                else
                {
                    nodeValue[i] = new double[neuronsCount[i - 1]];
                }
            }

            gradient = new double[hiddenLayersCount + 1][];
            for (int i = 0; i < gradient.Length; i++)
            {
                if (i == gradient.Length - 1)
                {
                    gradient[i] = new double[classesCount];
                }
                else
                {
                    gradient[i] = new double[neuronsCount[i] + 1];
                    // + 1 for the bias
                }
            }

            #endregion

        }

        public void train(double[][] trainingSample, int[] sampleClass, double learning_rate, double threshold, int trainingIterationsCount)
        {
            maxNumOfTrainingIterations = trainingIterationsCount;
            mseThreshold = threshold;
            eta = learning_rate;
            mseInIteration = 0;
            int iterationNum = 0;
            do
            {
                for (int sample = 0; sample < trainingSample.Length; sample++)
                {
                    for (int feature = 0; feature < trainingSample[sample].Length; feature++)
                    {
                        nodeValue[0][feature] = trainingSample[sample][feature];
                    }

                    for (int i = 0; i < classesCount; i++)
                    {
                        if (i == sampleClass[sample])
                            desired[i] = 1;
                        else
                            desired[i] = 0;
                    }

                    //forward phase
                    for (int layer = 0; layer < hiddenLayersCount + 1; layer++)
                    {
                        //summation
                        for (int curNode = -1; curNode < nodeValue[layer].Length; curNode++)
                        {
                            for (int nextNode = 0; nextNode < nodeValue[layer + 1].Length; nextNode++)
                            {
                                if (curNode == -1)
                                {
                                    nodeValue[layer + 1][nextNode] += weight[layer][curNode + 1][nextNode];
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
                    for (int outputNode = 0; outputNode < classesCount; outputNode++)
                    {
                        double y = nodeValue[nodeValue.Length - 1][outputNode];
                        gradient[hiddenLayersCount][outputNode] = a * (desired[outputNode] - y) * (y * (1 - y));
                        mseInIteration += 0.5 * (desired[outputNode] - y) * (desired[outputNode] - y);
                    }

                    for (int layer = hiddenLayersCount - 1; layer >= 0; layer--)
                    {
                        for (int curNode = 1; curNode < neuronsCount[layer]; curNode++)
                        {
                            gradient[layer][curNode] = 0;
                            if(layer < hiddenLayersCount - 1)
                            {
                                for (int nextNode = 0; nextNode < neuronsCount[layer + 1]; nextNode++)
                                {
                                    gradient[layer][curNode] += gradient[layer + 1][nextNode] * weight[layer + 1][curNode][nextNode];
                                }
                            }
                            else
                            {
                                for (int nextNode = 0; nextNode < classesCount; nextNode++)
                                {
                                    gradient[layer][curNode] += gradient[layer + 1][nextNode] * weight[layer + 1][curNode][nextNode];
                                }
                            }
                            
                            if (curNode > 0)
                                gradient[layer][curNode] = gradient[layer][curNode] * a * (nodeValue[layer + 1][curNode - 1]) * (1 - nodeValue[layer + 1][curNode - 1]);                 
                        }
                    }

                    //for (int layer = hiddenLayersCount - 1; layer >= 0; layer--)
                    //{
                    //    for (int curNode = 0; curNode < gradient[layer].Length; curNode++)
                    //    {
                    //        for (int nextNode = 0; nextNode < weight[layer][curNode].Length; nextNode++)
                    //        {
                    //            gradient[layer][curNode] += gradient[layer + 1][nextNode] * weight[layer][curNode][nextNode];
                    //        }
                    //        if(curNode > 0)
                    //            gradient[layer][curNode] = gradient[layer][curNode] * a * (nodeValue[layer + 1][curNode - 1]) * (1 - nodeValue[layer + 1][curNode - 1]);   
                    //    }
                    //}

                    //weight updating
                    for (int layer = 0; layer < weight.Length; layer++)
                    {
                        for (int curNode = 0; curNode < weight[layer].Length; curNode++)
                        {
                            for (int nextNode = 1; nextNode < weight[layer][curNode].Length; nextNode++)
                            {
                                if (curNode > 0)
                                    weight[layer][curNode][nextNode] += eta * gradient[layer][nextNode] * nodeValue[layer][curNode - 1];
                                else
                                    weight[layer][curNode][nextNode] += eta * gradient[layer][nextNode];
                            }
                        }
                    }

                }

                mseInIteration /= trainingSample.Length;
                iterationNum++;

            } while (mseInIteration > mseThreshold && iterationNum < maxNumOfTrainingIterations);


        }

        public int testSingle(double[] featuresVector)
        {
            for (int feature = 0; feature < featuresVector.Length; feature++)
            {
                nodeValue[0][feature] = featuresVector[feature];
            }

            for (int layer = 0; layer < hiddenLayersCount + 1; layer++)
            {
                //summation
                for (int curNode = -1; curNode < nodeValue[layer].Length; curNode++)
                {
                    for (int nextNode = 0; nextNode < nodeValue[layer + 1].Length; nextNode++)
                    {
                        if (curNode == -1)
                        {
                            nodeValue[layer + 1][nextNode] += weight[layer][curNode + 1][nextNode];
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

            for (int i = 0; i < classesCount; i++)
            {
                for (int j = 0; j < classesCount; j++)
                {
                    if (i != j && nodeValue[nodeValue.Length - 1][i] < nodeValue[nodeValue.Length - 1][j])
                        break;
                    if (j == classesCount - 1)
                        return i;
                }

            }

            return -1;


        }

        public int[] testMultiple(double[][] featuresVectors)
        {
            int[] ret = new int[featuresVectors.Length];
            for (int i = 0; i < featuresVectors.Length; i++)
            {
                ret[i] = testSingle(featuresVectors[i]);
            }
            return ret;
        }

        private double activationFunction(double sum)
        {
            return 1 / (1 + Math.Exp(-a * sum));
        }
    }
}

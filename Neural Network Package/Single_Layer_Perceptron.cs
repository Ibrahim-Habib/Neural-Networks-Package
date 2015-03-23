using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Neural_Network_Package
{
    class Single_Layer_Perceptron
    {
        private double[] weight;
        private double MSE;
        private double errorThreshold;
        private double eta;
        private int features_count;
        private int epochs_count;
        private Random generator;
        private int firstClassIndex;
        private int secondClassIndex;
        public Single_Layer_Perceptron(int num_of_features, double learning_rate, int num_of_iterations, int firstClassIndex, int secondClassIndex, double threshold)
        {
            errorThreshold = threshold;
            features_count = num_of_features;
            eta = learning_rate;
            epochs_count = num_of_iterations;
            this.firstClassIndex = firstClassIndex;
            this.secondClassIndex = secondClassIndex;
            weight = new double[features_count + 1];
            //initialize the weights with random values
            generator = new Random();
            for (int i = 0; i < weight.Length; i++)
            {
                weight[i] = 0;
            }

        }

        public void train(double[][] trainingSample, int[] sampleClass)
        {
            double perceptronResult;
            for (int i = 0; i < sampleClass.Length; i++)
            {
                if (sampleClass[i] == firstClassIndex)
                    sampleClass[i] = 1;
                else
                    sampleClass[i] = -1;
            }

            //error = new double[trainingSample.Length];
            for (int epoch = 0; epoch < epochs_count; epoch++)
            {
                MSE = 0;
                for (int sample = 0; sample < trainingSample.Length; sample++)
                {
                    perceptronResult =activationFunction(adder(trainingSample[sample]));
                    updateWeights(sampleClass[sample], perceptronResult, trainingSample[sample]);
                    MSE += (sampleClass[sample] - perceptronResult) * (sampleClass[sample] - perceptronResult);
                }

                MSE /= (double)trainingSample.Length;
                
                if (MSE < errorThreshold)
                    break;
            }
        }

        public int testSingle(double[] featuresVector)
        {
            int res = activationFunction(adder(featuresVector));
            if (res == 1)
                return firstClassIndex;
            else
                return secondClassIndex;
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

        private int activationFunction(double sum)
        {
            if (sum >= 0)
                return 1;
            else
                return -1;
        }

        private double adder(double[] featuresVector)
        {
            //wieght[0] = bias
            double sum = weight[0];
            for (int i = 0; i < featuresVector.Length; i++)
            {
                sum += weight[i + 1] * featuresVector[i];
            }
            return sum;
        }

        private void updateWeights(int d, double y, double[] x)
        {
           
           for (int i = 0; i < weight.Length; i++)
            {
                if (i > 0)
                    weight[i] += ((double)d - y) * eta * x[i - 1];
                else
                    weight[i] += ((double)d - y) * eta;
            }
        }

        public double[] getWeights()
        {
            return weight;
        }
    }
}

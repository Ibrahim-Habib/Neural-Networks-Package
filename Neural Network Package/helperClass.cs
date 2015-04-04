using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Neural_Network_Package
{
    static class helperClass
    {
        public static Form mainWindow;
        public static Form SLP_GUI;
        public static Form MLP_GUI;
        public static void readIrisFiles(ref double[][] setosa, ref double[][] versicolor, ref double[][] virginica)
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
                {
                    setosa[i][j] = double.Parse(numbers[j]);
                }
            }
            //read versicolor data
            for (int i = 0; i < 50; i++)
            {
                line = SR.ReadLine();
                numbers = line.Split(',');
                for (int j = 0; j < 4; j++)
                {
                    versicolor[i][j] = double.Parse(numbers[j]);
                }
            }

            //read virginica data

            for (int i = 0; i < 50; i++)
            {
                line = SR.ReadLine();
                numbers = line.Split(',');
                for (int j = 0; j < 4; j++)
                {
                    virginica[i][j] = double.Parse(numbers[j]);
                }
            }

            SR.Close();
            FS.Close();

        }

        public static void normalize(ref double[][] data)
        {
            double[] mean = new double[data[0].Length];
            double[] maxi = new double[data[0].Length];
            for (int i = 0; i < maxi.Length; i++)
            {
                maxi[i] = double.MinValue;
                mean[i] = 0;
            }

            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    mean[j] += data[i][j];
                }
            }

            for (int i = 0; i < data[0].Length; i++)
            {
                mean[i] /= data.Length;
            }

            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    data[i][j] -= mean[j];
                    maxi[j] = Math.Max(maxi[j], Math.Abs(data[i][j]));
                }
            }


            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < data[i].Length; j++)
                {
                    data[i][j] /= maxi[j];
                }
            }


        }
    }
}

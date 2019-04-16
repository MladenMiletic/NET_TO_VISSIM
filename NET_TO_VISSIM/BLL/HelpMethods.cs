using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_TO_VISSIM.BLL
{
    /// <summary>
    /// Various help methods
    /// </summary>
    public static class HelpMethods
    {
        /// <summary>
        /// Loads a csv document into a double [][] array with a set delimiter, accepts both decimal point or commas. Returns null if loading is not succesfull
        /// </summary>
        /// <param name="path">Full file name</param>
        /// <param name="delimiter">Delimiter</param>
        /// <returns></returns>
        public static double[][] LoadTrainingSet(string path, char delimiter)
        {
            try
            {
                string[][] data = File.ReadLines(path).Where(line => line != "").Select(x => x.Split(',')).ToArray();

                double[][] doubledata = new double[data.Length][];
                int i = 0;
                foreach (string[] line in data)
                {
                    double[] doubleline = new double[line.Length];
                    int j = 0;
                    foreach (string datapoint in line)
                    {
                        doubleline[j] = Double.Parse(datapoint, CultureInfo.InvariantCulture);
                        j++;
                    }
                    doubledata[i] = doubleline;
                    i++;
                }
                return doubledata;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Implementation of Knuth shuffle algorithm
        /// </summary>
        /// <param name="inputDataSet">Set to be shuffled</param>
        /// <returns></returns>
        public static double[][] Shuffle2DArray(double[][] inputDataSet)
        {
            Random r = new Random();
            int rInt = r.Next(0, 100);
            for (int t = 0; t < inputDataSet.Length; t++)
            {
                double[] tmp = inputDataSet[t];
                rInt = r.Next(t, inputDataSet.Length);

                inputDataSet[t] = inputDataSet[rInt];
                inputDataSet[rInt] = tmp;
            }
            return inputDataSet;
        }
    }
}

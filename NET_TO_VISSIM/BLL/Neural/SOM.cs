using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge;
using AForge.Neuro;
using AForge.Neuro.Learning;
using System.Windows.Forms;

namespace NET_TO_VISSIM.BLL.Neural
{
    class SOM
    {
        private int neuronCount;
        private int networkDimensionWidth;
        private int networkDimensionHeight;
        private double learningRate;
        private double learningRadius;
        private int inputDimensions;
        private DistanceNetwork somNetwork;
        private SOMLearning somLearningTrainer;
        private bool isSOMtrained;

        public int NeuronCount { get => neuronCount; set => neuronCount = value; }
        public int NetworkDimensionWidth { get => networkDimensionWidth; set => networkDimensionWidth = value; }
        public int NetworkDimensionHeight { get => networkDimensionHeight; set => networkDimensionHeight = value; }
        public double LearningRate { get => learningRate; set => learningRate = value; }
        public double LearningRadius { get => learningRadius; set => learningRadius = value; }
        public int InputDimensions { get => inputDimensions; set => inputDimensions = value; }

        /// <summary>
        /// Constructor method that takes in all required variables to create SOM network and trainer
        /// </summary>
        /// <param name="neuronCount">Number of neurons must be equal to networkWidth * networkHeight</param>
        /// <param name="networkDimensionWidth">Number of neurons in X direction</param>
        /// <param name="networkDimensionHeight">Number of neurons in Y direction</param>
        /// <param name="learningRate">Speed of learning. Determines how much neuron weights change when presented with an input.</param>
        /// <param name="learningRadius">Determines how non wining neurons are affected by input. Large radius = More change for distant neurons</param>
        /// <param name="inputDimensions">Number of inputs</param>
        /// <param name="randomRange">Determines the range in which the neuron weights will be randomized</param>
        public SOM(int neuronCount, int networkDimensionWidth, int networkDimensionHeight, double learningRate, double learningRadius, int inputDimensions, float randomRange)
        {
            try
            {
                NeuronCount = neuronCount;
                NetworkDimensionWidth = networkDimensionWidth;
                NetworkDimensionHeight = networkDimensionHeight;
                LearningRate = learningRate;
                LearningRadius = learningRadius;
                InputDimensions = inputDimensions;
                isSOMtrained = false;

                Neuron.RandRange = new Range(0, randomRange);
                somNetwork = new DistanceNetwork(inputDimensions, neuronCount);
                somNetwork.Randomize();

                somLearningTrainer = new SOMLearning(somNetwork, networkDimensionWidth, networkDimensionHeight);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while creating network");
            }

        }
        /// <summary>
        /// SOM training, entire inputData set is fed into the trainer once every epoch, learningRate and learningRadius are recalculated every epoch ensuring convergence
        /// </summary>
        /// <param name="inputDataSet">2 dimensional array rows = data points columns = dimensions</param>
        /// <param name="numOfEpochs"></param>
        public void TrainSOM(double [][] inputDataSet, int numOfEpochs)
        {
            double fixedLearningRate = learningRate / 10;
            double driftingLearningRate = fixedLearningRate * 9;

            for (int currentEpoch = 0; currentEpoch < numOfEpochs; currentEpoch++)
            {
                somLearningTrainer.LearningRate = driftingLearningRate * (numOfEpochs - currentEpoch) / numOfEpochs + fixedLearningRate;
                somLearningTrainer.LearningRadius = (double)learningRadius * (numOfEpochs - currentEpoch) / numOfEpochs;

                double[][] shuffledDataSet = BLL.HelpMethods.Shuffle2DArray(inputDataSet);

                double epochError = somLearningTrainer.RunEpoch(shuffledDataSet);
            }
            isSOMtrained = true;
        }

        /// <summary>
        /// Returns the number of winner neuron based on given input data point, if SOM is not trained -1 is returned
        /// </summary>
        /// <param name="input">double array as input point</param>
        /// <returns></returns>
        public int GetWinningNeuronNumber(double [] input)
        {
            if (!isSOMtrained)
            {
                return -1;
            }

            somNetwork.Compute(input);
            return somNetwork.GetWinner();
        }


    }
}

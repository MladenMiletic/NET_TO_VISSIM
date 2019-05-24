using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VISSIMLIB;
using AForge.Math;

namespace NET_TO_VISSIM.BLL.Thesis
{
    /// <summary>
    /// Class handling specific actions as required in Mladen Miletić thesis
    /// </summary>
    public class Actions
    {
        private int numStates;
        private int numActions;
        public double[,] hitMatrix;

        /// <summary>
        /// Creates matrix that counts each state-action pair activated
        /// </summary>
        /// <param name="numStates">Number of possible states</param>
        /// <param name="numActions">Number of possible actions</param>
        public Actions(int numStates, int numActions)
        {
            this.numStates = numStates;
            this.numActions = numActions;
            hitMatrix = new double[numStates, numActions];
            for (int i = 0; i < numStates; i++)
            {
                for (int j = 0; j < numActions; j++)
                {
                    hitMatrix[i, j] = 0;
                }
            }
        }

        /// <summary>
        /// Performs the action by changing the signal program
        /// </summary>
        /// <param name="state">Current state</param>
        /// <param name="action">Current action</param>
        /// <param name="signalController">Signal controller</param>
        public void PerformAction(int state, int action, ISignalController signalController)
        {
            try
            {
                signalController.set_AttValue("ProgNo", action);
                hitMatrix[state-1, action]++;
            }
            catch (Exception ex)

            {
                
            }
        }
    }
}

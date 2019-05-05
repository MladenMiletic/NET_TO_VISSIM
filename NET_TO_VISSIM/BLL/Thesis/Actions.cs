using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <param name="signalProgram">Signal program that will be changed by the actions</param>
        public void PerformAction(int state, int action, SignalProgram signalProgram)
        {
            switch (action)
            {
                case 1:
                    //Do nothing :)
                    break;
                case 2:
                    signalProgram.phases[0].subPhases[0].ChangeDuration(27);
                    break;
                case 3:
                    signalProgram.phases[0].subPhases[0].ChangeDuration(29);
                    break;
                case 4:
                    signalProgram.phases[0].subPhases[0].ChangeDuration(17);
                    break;
                case 5:
                    signalProgram.phases[0].subPhases[0].ChangeDuration(15);
                    break;
                case 6:
                    signalProgram.phases[1].subPhases[1].ChangeDuration(21);
                    break;
                case 7:
                    signalProgram.phases[1].subPhases[1].ChangeDuration(22);
                    break;
                case 8:
                    signalProgram.phases[1].subPhases[1].ChangeDuration(15);
                    break;
                case 9:
                    signalProgram.phases[1].subPhases[1].ChangeDuration(13);
                    break;
                case 10:
                    signalProgram.phases[2].subPhases[1].ChangeDuration(16);
                    break;
                case 11:
                    signalProgram.phases[2].subPhases[1].ChangeDuration(10);
                    break;
                case 12:
                    signalProgram.phases[2].subPhases[4].ChangeDuration(11);
                    break;
                case 13:
                    signalProgram.phases[2].subPhases[4].ChangeDuration(5);
                    break;
                case 14:
                    signalProgram.phases[2].subPhases[4].ChangeDuration(0);
                    break;
                case 15:
                    signalProgram.phases[0].subPhases[0].ChangeDuration(17);
                    signalProgram.phases[1].subPhases[1].ChangeDuration(15);
                    signalProgram.phases[2].subPhases[1].ChangeDuration(16);
                    break;
                case 16:
                    signalProgram.phases[0].subPhases[0].ChangeDuration(17);
                    signalProgram.phases[1].subPhases[1].ChangeDuration(21);
                    signalProgram.phases[2].subPhases[1].ChangeDuration(16);
                    break;
                case 17:
                    signalProgram.phases[0].subPhases[0].ChangeDuration(17);
                    signalProgram.phases[1].subPhases[1].ChangeDuration(15);
                    signalProgram.phases[2].subPhases[1].ChangeDuration(10);
                    break;
                case 18:
                    signalProgram.phases[0].subPhases[0].ChangeDuration(17);
                    signalProgram.phases[1].subPhases[1].ChangeDuration(21);
                    signalProgram.phases[2].subPhases[1].ChangeDuration(10);
                    break;
                case 19:
                    signalProgram.phases[0].subPhases[0].ChangeDuration(27);
                    signalProgram.phases[1].subPhases[1].ChangeDuration(15);
                    signalProgram.phases[2].subPhases[1].ChangeDuration(16);
                    break;
                case 20:
                    signalProgram.phases[0].subPhases[0].ChangeDuration(27);
                    signalProgram.phases[1].subPhases[1].ChangeDuration(21);
                    signalProgram.phases[2].subPhases[1].ChangeDuration(16);
                    break;
                case 21:
                    signalProgram.phases[0].subPhases[0].ChangeDuration(27);
                    signalProgram.phases[1].subPhases[1].ChangeDuration(15);
                    signalProgram.phases[2].subPhases[1].ChangeDuration(10);
                    break;
                case 22:
                    signalProgram.phases[0].subPhases[0].ChangeDuration(27);
                    signalProgram.phases[1].subPhases[1].ChangeDuration(21);
                    signalProgram.phases[2].subPhases[1].ChangeDuration(10);
                    break;
                default:
                    //Do nothing :) This should not happen so Message warning is shown
                    System.Windows.Forms.MessageBox.Show("Error Action with number " + action + " is not defined");
                    break;
            }
            hitMatrix[state, action]++;
        }



    }
}

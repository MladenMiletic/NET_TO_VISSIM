using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge;
using AForge.MachineLearning;

namespace NET_TO_VISSIM.BLL.Neural
{
    /// <summary>
    /// Class that connects Q learning from AForge to VISSIM Network
    /// </summary>
    public class Q
    {
        /// <summary>
        /// QLearning class from AForge
        /// </summary>
        public QLearning qLearning;
        
        /// <summary>
        /// Main constructor, currently only calls AForge QLearning class constructor
        /// </summary>
        /// <param name="states">number of states</param>
        /// <param name="actions">number of actions</param>
        /// <param name="explorationRate">exploration rate in epsilon greedy exploration</param>
        public Q(int states, int actions, double explorationRate)
        {
            qLearning = new QLearning(states, actions, new EpsilonGreedyExploration(explorationRate), false);
        }

        /// <summary>
        /// Gets action determined by the current state
        /// </summary>
        /// <param name="state">state number</param>
        /// <returns>action number</returns>
        public int GetAction(int state)
        {
            return qLearning.GetAction(state);
        }

        /// <summary>
        /// Updates Q table based on the calculated reward
        /// </summary>
        /// <param name="previousState">state number of previous state</param>
        /// <param name="action">number of action that was taken</param>
        /// <param name="reward">reward given</param>
        /// <param name="nextState">number of the next state</param>
        public void UpdateQTable(int previousState, int action, double reward, int nextState)
        {
            qLearning.UpdateState(previousState, action, reward, nextState);
        }

        
    }
}

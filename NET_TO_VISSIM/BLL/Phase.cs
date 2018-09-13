using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NET_TO_VISSIM.BLL
{
    /// <summary>
    /// Contains the list of all subphases
    /// </summary>
    class Phase
    {
        public List<SubPhase> subPhases;
        public float duration;
        public float currentDuration;
        public SubPhase currentSubPhase;
        public VariableSubPhase variablePhase;
        public int currentSubPhaseIndex;
        public int phaseCount;

        /// <summary>
        /// Initializes phase, currentDuration is considered to be 0
        /// </summary>
        /// <param name="subPhases">List of subphasess in execution order</param>
        public Phase(List<SubPhase> subPhases) : this(subPhases, 0)
        {
        }

        /// <summary>
        /// Initializes phase
        /// </summary>
        /// <param name="subPhases">List of subphasess in execution order</param>
        /// <param name="currentDuration">Current duration of the phase</param>
        public Phase(List<SubPhase> subPhases, float currentDuration)
        {
            this.subPhases = subPhases;
            this.currentDuration = currentDuration;
            phaseCount = subPhases.Count();
            CalculateDuration();
            SetInitialPhase();
        }

        /// <summary>
        /// Calculates the current total duration od the phase
        /// </summary>
        private void CalculateDuration()
        {
            this.duration = 0;
            foreach (SubPhase subPhase in subPhases)
            {
                this.duration += subPhase.Duration;
            }
        }

        /// <summary>
        /// Sets the initial subphase and current time point inside the subphase
        /// </summary>
        private void SetInitialPhase()
        {
            float checkedPhasesDuration = 0;
            currentSubPhaseIndex = 0;
            foreach (SubPhase subPhase in subPhases)
            {
                if (checkedPhasesDuration + subPhase.Duration > currentDuration)
                {
                    this.currentSubPhase = subPhase;
                    subPhase.CurrentDuration = currentDuration - checkedPhasesDuration;
                }
                else
                {
                    checkedPhasesDuration += subPhase.Duration;
                    currentSubPhaseIndex++;
                }
            }
        }

        /// <summary>
        /// Activates required subphase
        /// </summary>
        /// <param name="resolution">Simulation resolution</param>
        /// <returns>True if phase is over, false if not</returns>
        public bool Step(int resolution)
        {
            if(currentSubPhase.Step(resolution))
            {
                currentSubPhaseIndex++;
                if (currentSubPhaseIndex == phaseCount)
                {
                    currentSubPhaseIndex = 0;
                    currentDuration = 0;
                    currentSubPhase = subPhases[currentSubPhaseIndex];
                    return true;
                }
                currentSubPhase = subPhases[currentSubPhaseIndex];
            }
            return false;
        }
    }
}

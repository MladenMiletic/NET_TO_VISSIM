using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VISSIMLIB;

namespace NET_TO_VISSIM.BLL
{
    /// <summary>
    /// Contains signal program for one intersection
    /// </summary>
    class SignalProgram
    {
        public List<Phase> phases;
        public float duration;
        public float currentDuration;
        public float offset;
        public int phaseCount;
        public int currentPhaseIndex;
        public Phase currentPhase;
        public int signalControllerId;

        /// <summary>
        /// Constructor, if no offset is set it is considered to be 0
        /// </summary>
        /// <param name="phases">List of phases</param>
        public SignalProgram(List<Phase> phases, int signalControllerId) : this(phases, signalControllerId, 0)
        {
        }

        /// <summary>
        /// Constructor, initialization
        /// </summary>
        /// <param name="phases">List of phases</param>
        /// <param name="offset">Offset</param>
        public SignalProgram(List<Phase> phases, int signalControllerId, float offset)
        {
            this.signalControllerId = signalControllerId;
            this.phases = phases;
            this.offset = offset;
            this.currentDuration = offset;
            phaseCount = phases.Count();
            CalculateDuration();
            SetInitialPhase();
        }

        /// <summary>
        /// Sets initial phase depending on the offset
        /// </summary>
        private void SetInitialPhase()
        {
            float checkedPhasesDuration = 0;
            currentPhaseIndex = 0;
            foreach (Phase phase in phases)
            {
                if (checkedPhasesDuration + phase.duration > offset)
                {
                    this.currentPhase = phase;
                    phase.currentDuration = offset - checkedPhasesDuration;
                }
                else
                {
                    checkedPhasesDuration += phase.duration;
                    currentPhaseIndex++;
                }
            }
        }

        /// <summary>
        /// Calculates total cycle duration
        /// </summary>
        private void CalculateDuration()
        {
            this.duration = 0;
            foreach (Phase phase in phases)
            {
                this.duration += phase.duration;
            }
        }

        /// <summary>
        /// Performs one step, activates proper phase
        /// </summary>
        /// <param name="resolution">Simulation resolution</param>
        /// <param name="vissim">Vissim instance</param>
        public void Step(int resolution, Vissim vissim)
        {
            if (currentPhase.Step(resolution, signalControllerId, vissim))
            {
                currentPhaseIndex++;
                if (currentPhaseIndex == phaseCount)
                {
                    currentPhaseIndex = 0;
                    currentDuration = 0;
                    currentPhase = phases[currentPhaseIndex];
                }
                currentPhase = phases[currentPhaseIndex];
            }
            currentDuration += 1 / resolution;
        }
    }
}

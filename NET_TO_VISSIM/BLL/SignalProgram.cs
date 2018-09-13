using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public SignalProgram(List<Phase> phases) : this(phases, 0)
        {
        }
        public SignalProgram(List<Phase> phases, float offset)
        {
            this.phases = phases;
            this.offset = offset;
            this.currentDuration = offset;
            phaseCount = phases.Count();
            CalculateDuration();
            SetInitialPhase();
        }

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

        private void CalculateDuration()
        {
            this.duration = 0;
            foreach (Phase phase in phases)
            {
                this.duration += phase.duration;
            }
        }

        public void Step(int resolution)
        {
            if (currentPhase.Step(resolution))
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

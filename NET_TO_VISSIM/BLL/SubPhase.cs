using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VISSIMLIB;
using NET_TO_VISSIM.DAL;

namespace NET_TO_VISSIM.BLL
{
    /// <summary>
    /// Base Class used to store subphase data, subphase is in this context defined as a set of signal states for various signal groups
    /// </summary>
    class SubPhase
    {
        public List<ISignalGroup> SignalGroups { get => signalGroups; set => signalGroups = value; }
        public List<int> SignalStates { get => signalStates; set => signalStates = value; }
        public float CurrentDuration { get => currentDuration; set => currentDuration = value; }
        public float Duration { get => duration; set => duration = value; }

        private List<ISignalGroup> signalGroups;
        private List<int> signalStates;
        private float duration;
        private float currentDuration;

        /// <summary>
        /// SubPhase constructor, initializes data
        /// </summary>
        /// <param name="signalGroups">List of signal groups affected by this subphase</param>
        /// <param name="signalStates">List of states as defined by vissim coresponding numbers, must match the order of signal groups</param>
        /// <param name="duration">Duration of the subphase in seconds regardless of simulation resolution</param>
        public SubPhase(List<ISignalGroup> signalGroups, List<int> signalStates, float duration)
        {
            this.signalGroups = signalGroups;
            this.signalStates = signalStates;
            this.duration = duration;
            this.currentDuration = 0;
        }

        /// <summary>
        /// Sets all signal groups to coresponding signal state, resolution is assumed to be 1
        /// </summary>
        /// <returns>True when the subphase is complete, false when not complete</returns>
        public bool Step()
        {
            return Step(1);
        }

        /// <summary>
        /// Sets all signal groups to coresponding signal state
        /// </summary>
        /// <param name="resolution">Simulation resolution for time tracking</param>
        /// <returns>True when the subphase is complete, false when not complete</returns>
        public bool Step(int resolution)
        {
            if (currentDuration == 0)
            {
                using (var enumeratorSignalGroups = signalGroups.GetEnumerator())
                using (var enumeratorSignalStates = signalStates.GetEnumerator())
                {
                    while (enumeratorSignalGroups.MoveNext() && enumeratorSignalStates.MoveNext())
                    {
                        var signalGroup = enumeratorSignalGroups.Current;
                        var signalState = enumeratorSignalStates.Current;
                        DAL.COM.SetSignalState(signalGroup, signalState);
                    }
                }
                currentDuration = currentDuration + (1/resolution);
            }
            if (currentDuration >= duration)
            {
                currentDuration = 0;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

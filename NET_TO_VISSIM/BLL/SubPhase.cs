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
        public float DefaultDuration { get => defaultDuration; set => defaultDuration = value; }
        public List<ISignalGroup> SignalGroups { get => signalGroups; set => signalGroups = value; }
        public List<int> SignalStates { get => signalStates; set => signalStates = value; }
        public float CurrentDuration { get => currentDuration; set => currentDuration = value; }
        public float Duration { get => duration; private set => duration = value; }
        public bool Editable { get => editable; set => editable = value; }

        private bool editable;
        private List<ISignalGroup> signalGroups;
        private List<int> signalStates;
        private float duration;
        private float currentDuration;
        private float defaultDuration;


        /// <summary>
        /// SubPhase constructor, initializes data
        /// </summary>
        /// <param name="signalGroups">List of signal groups affected by this subphase</param>
        /// <param name="signalStates">List of states as defined by vissim coresponding numbers, must match the order of signal groups</param>
        /// <param name="duration">Duration of the subphase in seconds regardless of simulation resolution</param>
        /// <param name="editable">Set true if duration of this subphase can be changed</param>
        public SubPhase(List<ISignalGroup> signalGroups, List<int> signalStates, float duration, bool editable)
        {
            this.signalGroups = signalGroups;
            this.signalStates = signalStates;
            this.duration = duration;
            this.currentDuration = 0;
            this.defaultDuration = duration;
            this.editable = editable;
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

        /// <summary>
        /// Resets the duration to the default one
        /// </summary>
        public void ResetDuration()
        {
            this.Duration = defaultDuration;
        }

        /// <summary>
        /// Changes the duration
        /// </summary>
        /// <param name="newDuration">new duration</param>
        public void ChangeDuration(float newDuration)
        {
            if(editable)
            {
                this.duration = newDuration;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VISSIMLIB;

namespace NET_TO_VISSIM.BLL
{
    /// <summary>
    /// VariableSubPhase is a subphase that can have its duration changed
    /// </summary>
    class VariableSubPhase : SubPhase
    {
        private float defaultDuration;

        public float DefaultDuration { get => defaultDuration; set => defaultDuration = value; }

        /// <summary>
        /// Data initialization
        /// </summary>
        /// <param name="signalGroups">List of signal groups affected by this subphase</param>
        /// <param name="signalStates">List of states as defined by vissim coresponding numbers, must match the order of signal groups</param>
        /// <param name="duration">Duration of the subphase in seconds regardless of simulation resolution</param>
        public VariableSubPhase(List<ISignalGroup> signalGroups, List<int> signalStates, float duration) : base(signalGroups, signalStates, duration)
        {
            this.defaultDuration = duration;
        }

        /// <summary>
        /// Resets the duration to the default one
        /// </summary>
        public void ResetDuration()
        {
            this.Duration = defaultDuration;
        }
        
    }
}

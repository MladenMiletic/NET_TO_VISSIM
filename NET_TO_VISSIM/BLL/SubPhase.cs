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
    public class SubPhase
    {
        public float DefaultDuration { get => defaultDuration; set => defaultDuration = value; }
        public List<int> SignalGroupIds { get => signalGroupIds; set => signalGroupIds = value; }
        public List<int> SignalStates { get => signalStates; set => signalStates = value; }
        public float CurrentDuration { get => currentDuration; set => currentDuration = value; }
        public float Duration { get => duration; private set => duration = value; }
        public bool Editable { get => editable; set => editable = value; }
        public float MaxDuration { get => maxDuration; set => maxDuration = value; }
        public float MinDuration { get => minDuration; set => minDuration = value; }

        private float minDuration;
        private float maxDuration;
        private bool editable;
        private List<int> signalGroupIds;
        private List<int> signalStates;
        private float duration;
        private float currentDuration;
        private float defaultDuration;


        /// <summary>
        /// SubPhase constructor, initializes data
        /// </summary>
        /// <param name="signalGroupIds">List of signal group ids affected by this subphase</param>
        /// <param name="signalStates">List of states as defined by vissim coresponding numbers, must match the order of signal groups</param>
        /// <param name="duration">Duration of the subphase in seconds regardless of simulation resolution</param>
        /// <param name="editable">Set true if duration of this subphase can be changed</param>
        public SubPhase(List<int> signalGroupIds, List<int> signalStates, float duration, bool editable)
        {
            this.signalGroupIds = signalGroupIds.ToList();
            this.signalStates = signalStates.ToList();
            this.duration = duration;
            this.currentDuration = 0;
            this.defaultDuration = duration;
            this.editable = editable;
        }

        /// <summary>
        /// Sets all signal groups to coresponding signal state, resolution is assumed to be 1
        /// </summary>
        /// <returns>True when the subphase is complete, false when not complete</returns>
        public bool Step(int signalControllerId, Vissim vissim)
        {
            return Step(1, signalControllerId, vissim);
        }

        /// <summary>
        /// Sets all signal groups to coresponding signal state
        /// </summary>
        /// <param name="resolution">Simulation resolution for time tracking</param>
        /// <param name="signalControllerId">Id of coresponding signal controller</param>
        /// <param name="vissim">Vissim instance</param>
        /// <returns>True when the subphase is complete, false when not complete</returns>
        public bool Step(int resolution, int signalControllerId, Vissim vissim)
        {
            if (currentDuration == 0)
            {
                using (var enumeratorSignalGroups = signalGroupIds.GetEnumerator())
                using (var enumeratorSignalStates = signalStates.GetEnumerator())
                {
                    while (enumeratorSignalGroups.MoveNext() && enumeratorSignalStates.MoveNext())
                    {
                        var signalGroupId = enumeratorSignalGroups.Current;
                        var signalState = enumeratorSignalStates.Current;
                        ISignalController SignalController = vissim.Net.SignalControllers.get_ItemByKey(signalControllerId);
                        ISignalGroup SignalGroup = SignalController.SGs.get_ItemByKey(signalGroupId);
                        COM.SetSignalState(SignalGroup, signalState);
                    }
                }
                
            }
            currentDuration = currentDuration + (1 / resolution);
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
        /// Changes the duration of this subphase
        /// </summary>
        /// <param name="newDuration">new duration</param>
        public void ChangeDuration(float newDuration)
        {
            if(editable)
            {
                if(newDuration >= minDuration && newDuration <= maxDuration)
                {
                    this.duration = newDuration;
                }
            }
        }

        /// <summary>
        /// Increases the duration of this subphase by given amount, if this increase is above max duration the subphase duration will be set to max duration
        /// </summary>
        /// <param name="increase">Number of seconds by which the duration will be increased</param>
        public void ChangeDurationIncrease(float increase)
        {
            if(editable)
            {
                float newDuration = this.duration + increase;
                if (newDuration > maxDuration)
                {
                    ChangeDuration(maxDuration);
                }
                else
                {
                    ChangeDuration(newDuration);
                }
            }
        }
 

        /// <summary>
        /// Decreases the duration of this subphase by given amount, if this decrease is below min duration the subphase duration will be set to min duration
        /// </summary>
        /// <param name="decrease">Number of seconds by which the duration will be decreased</param>
        public void ChangeDurationDecrease(float decrease)
        {
            if (editable)
            {
                float newDuration = this.duration - decrease;
                if (newDuration < minDuration)
                {
                    ChangeDuration(minDuration);
                }
                else
                {
                    ChangeDuration(newDuration);
                }
            }
        }
    }
}

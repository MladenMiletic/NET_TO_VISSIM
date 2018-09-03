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
    /// Class used to store current simulation data with methods used to manipulate the current simulation
    /// </summary>
    public class Simulation : IDisposable
    {
        /// <summary>
        /// Flag: Has Dispose already been called?
        /// </summary>
        bool disposed = false;

        /// <summary>
        /// Holds the reference to the simulation interface of VISSIM
        /// </summary>
        private ISimulation currentSimulation;

        /// <summary>
        /// Current simulation seed
        /// </summary>
        private int simulationSeed;

        private int simulationPeriod;

        /// <summary>
        /// Default constructor which gets the simulation object reference from VISSIM
        /// </summary>
        /// <param name="vissimConnection">Vissim connection from <see cref="VissimConnection"/></param>
        public Simulation(VissimConnection vissimConnection) => currentSimulation = vissimConnection.GetVissimInstance().Simulation;

        /// <summary>
        /// Sets simulation seed in VISSIM and stores it locally if successful
        /// </summary>
        /// <param name="seed">Integer value of random seed</param>
        public void SetSimulationSeed(int seed)
        {
            if(COM.SetSimulationSeed(currentSimulation, seed))
            {
                simulationSeed = seed;
            }
        }

        /// <summary>
        /// Sets simulation period in VISSIM and stores it locally if successful
        /// </summary>
        /// <param name="period"></param>
        public void SetSimulationPeriod(int period)
        {
            if (COM.SetSimulationPeriod(currentSimulation, period))
            {
                simulationPeriod = period;
            }
        }
        #region Dispose
        /// <summary>
        /// Public implementation of Dispose pattern callable by consumers.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Protected implementation of Dispose pattern.
        /// </summary>
        /// <param name="disposing">Bool for freeing other managed objects</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }
            if (disposing)
            {
            }
            disposed = true;
        }
        #endregion
    }
}

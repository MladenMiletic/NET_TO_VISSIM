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
    public class Simulation
    {
        /// <summary>
        /// Holds the reference to the simulation interface of VISSIM
        /// </summary>
        private ISimulation currentSimulation;
        private int simulationSeed;


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
    }
}

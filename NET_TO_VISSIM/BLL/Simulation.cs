using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VISSIMLIB;
using NET_TO_VISSIM.DAL;
using System.Windows.Forms;

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

        /// <summary>
        /// Current simulation period
        /// </summary>
        private int simulationPeriod;

        /// <summary>
        /// Current simulation resolution
        /// </summary>
        private int simulationResolution;

        private VissimConnection vissimConnection;

        /// <summary>
        /// Default constructor which gets the simulation object reference from VISSIM
        /// </summary>
        /// <param name="vissimConnection">Vissim connection from <see cref="VissimConnection"/></param>
        public Simulation(VissimConnection vissimConnection)
        {
            currentSimulation = vissimConnection.GetVissimInstance().Simulation;
            this.vissimConnection = vissimConnection;
        }

        /// <summary>
        /// Sets simulation seed in VISSIM and stores it locally if successful
        /// </summary>
        /// <param name="seed">Integer value of random seed. Min: 1</param>
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
        /// <param name="period">Integer value of period. Min: 1</param>
        public void SetSimulationPeriod(int period)
        {
            if (COM.SetSimulationPeriod(currentSimulation, period))
            {
                simulationPeriod = period;
            }
        }

        /// <summary>
        /// Sets simulation resolution in VISSIM and stores it locally if successful
        /// </summary>
        /// <param name="resolution">Integer value of resolution. Range: 1 - 20</param>
        public void SetSimulationResolution(int resolution)
        {
            if (COM.SetSimulationResolution(currentSimulation, resolution))
            {
                simulationResolution = resolution;
            }
        }

        /// <summary>
        /// Performs one sigle simulation step, and checks possible algorithms and implemenations
        /// </summary>
        public void SimulationStep(SignalProgram signalProgram)
        {
            try
            {
                currentSimulation.RunSingleStep();
                //AlgorithmCheck();
                signalProgram.Step(this.simulationResolution, vissimConnection.GetVissimInstance());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        /// <summary>
        /// Not implemented yet
        /// </summary>
        private void AlgorithmCheck()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Simulation continously runs until the end or until a predefined break point is reached.
        /// </summary>
        public void RunContinuos()
        {
            currentSimulation.RunContinuous();
        }

        /// <summary>
        /// Simulation continuosly runs until the set simulationBreakPoint
        /// </summary>
        /// <param name="simulationBreakPoint">Integer value of the simulation breaking point</param>
        public void RunContinuos(int simulationBreakPoint)
        {
            if(COM.SetSimulationBreakPoint(currentSimulation, simulationBreakPoint))
            {
                RunContinuos();
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

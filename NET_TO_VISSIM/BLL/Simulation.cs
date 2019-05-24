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
        public ISimulation currentSimulation;

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

        private double simulationTime;

        public double[] queueCounterResultsMax;

        public double[] queueCounterResultsAvg;

        /// <summary>
        /// Default constructor which gets the simulation object reference from VISSIM
        /// </summary>
        /// <param name="vissimConnection">Vissim connection from <see cref="VissimConnection"/></param>
        public Simulation(VissimConnection vissimConnection)
        {
            currentSimulation = vissimConnection.GetVissimInstance().Simulation;
            this.vissimConnection = vissimConnection;
            vissimConnection.GetVissimInstance().SuspendUpdateGUI();
            int numOfCounters = vissimConnection.GetVissimInstance().Net.QueueCounters.Count;
            queueCounterResultsMax = new double[numOfCounters];
            queueCounterResultsAvg = new double[numOfCounters];
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
                SensorCheck();
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

        private void SensorCheck()
        {
            if ((simulationTime + 2) % 300 == 0)
            {
                ReadQCounters();
            }

        }

        private void ReadQCounters()
        {
            int i = 0;
            foreach (IQueueCounter queueCounter in vissimConnection.GetVissimInstance().Net.QueueCounters)
            {
                queueCounterResultsMax[i] = queueCounter.get_AttValue("QLenMax(Current, Current)");
                queueCounterResultsAvg[i] = queueCounter.get_AttValue("QLen(Current, Current)");
                i++;
            }
        }

        /// <summary>
        /// Simulation continously runs until the end or until a predefined break point is reached.
        /// </summary>
        public void RunContinuos()
        {
            currentSimulation.RunContinuous();
            ReadQCounters();
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

        /// <summary>
        /// Runs entire simulation step by step
        /// </summary>
        /// <param name="signalProgram">Current signal program</param>
        public void RunStepByStep(SignalProgram signalProgram)
        {
            simulationPeriod = COM.getSimulationPeriod(currentSimulation);
            simulationTime = 0;
            for (int i = 0; i < (simulationPeriod * simulationResolution); i++)
            {
                SimulationStep(signalProgram);
                simulationTime = simulationTime + (1 / simulationResolution);
            }
            //GET SIMULATION RESULTS HERE
        }

        public void RunMultipleStepByStep(SignalProgram signalProgram, int numberOfSimulations)
        {
            for (int i = 0; i < numberOfSimulations; i++)
            {
                RunStepByStep(signalProgram);
            }
            //
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

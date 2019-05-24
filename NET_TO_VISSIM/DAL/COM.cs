using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VISSIMLIB;
using System.Windows.Forms;

namespace NET_TO_VISSIM.DAL
{
    /// <summary>
    /// Static class containing methods used for COM communication
    /// </summary>
    public static class COM
    {
        #region Vissim object

        /// <summary>
        /// Creates COM and opens VISSIM.exe
        /// </summary>
        /// <returns>Returns the Vissim object used for further communication with VISSIM</returns>
        public static Vissim InitialConnection()
        {
            try
            {
                return new Vissim();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while establishing Vissim connection!");
                return null;
            }
        }

        /// <summary>
        /// Attempts to load network into VISSIM
        /// </summary>
        /// <param name="vissimInstance">Vissim object with COM established</param>
        /// <param name="inpxPath">Full path to the .inpx VISSIM file</param>
        public static void LoadVissimNetwork(Vissim vissimInstance, string inpxPath)
        {
            try
            {
                vissimInstance.LoadNet(inpxPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while loading Vissim network!");
            }
        }

        /// <summary>
        /// Attempts to load layout into VISSIM
        /// </summary>
        /// <param name="vissimInstance">Vissim object with COM established</param>
        /// <param name="layxPath">Full path to the .layx VISSIM file</param>
        public static void LoadVissimLayout(Vissim vissimInstance, string layxPath)
        {
            try
            {
                vissimInstance.LoadLayout(layxPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while loading Vissim layout!");
            }
        }

        #endregion

        #region ISimulation

        /// <summary>
        /// Attempts to set simulation seed for the given simulation object
        /// </summary>
        /// <param name="simulation">Vissim.Simulation object</param>
        /// <param name="seed">Integer value of seed</param>
        /// <returns>True if VISSIM accepted the value</returns>
        public static bool SetSimulationSeed(ISimulation simulation, int seed)
        {
            try
            {
                simulation.set_AttValue("RandSeed", seed);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while setting simulation seed!");
                return false;
            }
        }

        /// <summary>
        /// Attempts to set simulation period for the given simulation object
        /// </summary>
        /// <param name="simulation">Vissim.Simulation object</param>
        /// <param name="period">Integer value of period</param>
        /// <returns>True if VISSIM accepted the value</returns>
        public static bool SetSimulationPeriod(ISimulation simulation, int period)
        {
            try
            {
                simulation.set_AttValue("SimPeriod", period);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while setting simulation period!");
                return false;
            }
        }

        /// <summary>
        /// Gets the simulation period from Vissim simulation
        /// </summary>
        /// <param name="simulation">Vissim.Simulation object</param>
        /// <returns></returns>
        public static int getSimulationPeriod(ISimulation simulation)
        {
            try
            {
                return simulation.get_AttValue("SimPeriod");
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Sets a breaking point used for continuos run
        /// </summary>
        /// <param name="simulation">Vissim.Simulation object</param>
        /// <param name="simulationBreakPoint">Integer value of the breakpoint</param>
        /// <returns>True if VISSIM accepted the value</returns>
        public static bool SetSimulationBreakPoint(ISimulation simulation, int simulationBreakPoint)
        {
            try
            {
                simulation.set_AttValue("SimBreakAt", simulationBreakPoint);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while setting simulation break point!");
                return false;
            }
        }

        /// <summary>
        /// Attempts to set simulation resolution for the given simulation object
        /// </summary>
        /// <param name="simulation">Vissim.Simulation object</param>
        /// <param name="resolution">Integer value of resolution</param>
        /// <returns>True if VISSIM accepted the value</returns>
        public static bool SetSimulationResolution(ISimulation simulation, int resolution)
        {
            try
            {
                simulation.set_AttValue("SimRes", resolution);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while setting simulation resolution!");
                return false;
            }
        }


        #endregion

        #region SignalControl

        /// <summary>
        /// Attempts to set signal state of a given signal group and state. If using this VISSIM will automatically set ContrByCOM to true.
        /// </summary>
        /// <param name="signalGroup">ISignalGroup object</param>
        /// <param name="state">Integer value representig signal state</param>
        /// <returns>True if VISSIM accepted the change</returns>
        public static bool SetSignalState(ISignalGroup signalGroup, int state)
        {
            try
            {
                signalGroup.set_AttValue("SigState", state);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error while setting signal state!");
                return false;
            }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VISSIMLIB;
using System.Windows.Forms;

namespace NET_TO_VISSIM.DAL
{
    public static class COM
    {
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
        /// <param name="netPath">Full path to the .inpx VISSIM file</param>
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
    }
}

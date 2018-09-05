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
    /// VissimConnection class which holds the reference to the VissimInstance initialized by <see cref="COM"/>
    /// </summary>
    public class VissimConnection
    {
        /// <summary>
        /// Holds the Vissim object that is used for COM 
        /// </summary>
        private Vissim vissimInstance;

        /// <summary>
        /// True if VISSIM is open, False if not
        /// </summary>
        private bool isVissimOpen;

        /// <summary>
        /// Default VissimConnection constructor which starts Vissim instance
        /// </summary>
        public VissimConnection()
        {
            vissimInstance = COM.InitialConnection();
            isVissimOpen = true;
        }

        /// <summary>
        /// Default deconstructor which will close the Vissim instance
        /// </summary>
        ~VissimConnection() => Close();

        /// <summary>
        /// Get method for Vissim instance in the current Vissim Connection
        /// </summary>
        /// <returns>Vissim object with established COM</returns>
        public Vissim GetVissimInstance() => vissimInstance;

        /// <summary>
        /// Closes VISSIM instance
        /// </summary>
        public void Close()
        {
            if(isVissimOpen)
            {
                try
                {
                    vissimInstance.Exit();
                    isVissimOpen = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error while closing Vissim instance");
                }
            }
        }
    }
}

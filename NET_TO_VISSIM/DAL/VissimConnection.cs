using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VISSIMLIB;

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
        private Vissim VissimInstance;

        /// <summary>
        /// Default VissimConnection constructor which starts Vissim instance
        /// </summary>
        public VissimConnection() => VissimInstance = COM.InitialConnection();

        /// <summary>
        /// Default deconstructor which will close the Vissim instance
        /// </summary>
        ~VissimConnection() => VissimInstance.Exit();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NET_TO_VISSIM.UI
{
    /// <summary>
    /// Class containing all meta information
    /// </summary>
    public static class Meta
    {
        private const string licenceInfo = "Copyright 2018 Mladen Miletić\n\n" +
                "Licensed under the GNU LESSER GENERAL PUBLIC LICENSE";
                
        /// <summary>
        /// Opens a message box showing licence information to the user
        /// </summary>
        public static void ShowLicenceInfo()
        {
            MessageBox.Show(GetLicenceInfo(), "Licence information!", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
        }
        /// <summary>
        /// Get method for licence info data
        /// </summary>
        /// <returns>String containing entire licence data</returns>
        public static string GetLicenceInfo() => licenceInfo;
    }
}

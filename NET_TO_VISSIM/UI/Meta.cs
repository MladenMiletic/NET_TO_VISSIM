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
                "Licensed under the Apache License, Version 2.0(the \"License\");\n" +
                "you may not use this file except in compliance with the License.\n" +
                "You may obtain a copy of the License at\n\n" +
                "http://www.apache.org/licenses/LICENSE-2.0 \n\n" +
                "Unless required by applicable law or agreed to in writing, software " +
                "distributed under the License is distributed on an \"AS IS\" BASIS, " +
                "WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. " +
                "See the License for the specific language governing permissions and " +
                "limitations under the License.";
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

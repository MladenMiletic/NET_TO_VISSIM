using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NET_TO_VISSIM.UI;

namespace NET_TO_VISSIM
{
    public partial class MainWindow : Form
    {
        /// <summary>
        /// Main Window constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Load method for mainWindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Load(object sender, EventArgs e)
        {
            Meta.ShowLicenceInfo();
        }
    }
}

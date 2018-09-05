using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NET_TO_VISSIM.DAL;
using NET_TO_VISSIM.BLL;


namespace NET_TO_VISSIM.UI
{
    public partial class TestingForm : Form
    {
        public VissimConnection vissim;
        public Simulation sim;

        public TestingForm()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            vissim = new VissimConnection();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sim = new Simulation(vissim);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sim.SimulationStep();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sim.RunContinuos();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                COM.LoadVissimNetwork(vissim.GetVissimInstance(), openFileDialog1.FileName);
            }
        }

        private void TestingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            vissim.Close();
        }
    }
}

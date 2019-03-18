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
using VISSIMLIB;


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
            if(vissim != null)
            {
                vissim.Close();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Vissim vis = vissim.GetVissimInstance();
            int controllercount = vis.Net.SignalControllers.Count;
            foreach (ISignalController controller in vis.Net.SignalControllers)
            {
                int numSGS = controller.SGs.Count;
                MessageBox.Show("" + numSGS, "OP OP");
                foreach (ISignalGroup signalGroup in controller.SGs)
                {
                    MessageBox.Show("My name is: " + signalGroup.get_AttValue("Name"), "OP OP");
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int numOfPhases = 2;
            int sigController = 1;
            List<Phase> listPhases = new List<Phase>();
            for (int i = 0; i < numOfPhases; i++)
            {
                //How many subphases in this phase?
                int numOfSubPhases = 3;
                List<SubPhase> listSubPhases = new List<SubPhase>();
                for (int j = 0; j < numOfSubPhases; j++)
                {
                    //how many SGs?
                    List<int> listSGids = new List<int>();
                    List<int> listStates = new List<int>();
                    int numSGS = 6;
                    for (int k = 0; k < numSGS; k++)
                    {
                        listSGids.Add(k + 1);
                        listStates.Add(3);
                    }
                    listSubPhases.Add(new SubPhase(listSGids, listStates, 20, false));

                }
                listPhases.Add(new Phase(listSubPhases));
            }
            SignalProgram signalProgram = new SignalProgram(listPhases, sigController);
            MessageBox.Show("", "");
        }
    }
}

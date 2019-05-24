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
using System.IO;
using System.Globalization;
using AForge.Neuro;
using AForge.MachineLearning;

namespace NET_TO_VISSIM.UI
{
    public partial class TestingForm : System.Windows.Forms.Form
    {
        public VissimConnection vissim;
        public Simulation sim;
        public SignalProgram signalProgram;

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
            sim.SetSimulationResolution(1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //sim.SimulationStep();
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

        private void button8_Click(object sender, EventArgs e)
        {
            BLL.Neural.SOM som;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                double[][] importedData = BLL.HelpMethods.LoadTrainingSet(openFileDialog1.FileName, ',');
                if (importedData != null)
                {
                    som = new BLL.Neural.SOM(25, 5, 5, 0.5, 1, 8, 170);
                    som.TrainSOM(importedData, 1000);
                    som.PerformAnalysis();
                }
                
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int sigController = 3;
            List<Phase> listPhases = new List<Phase>();
            
            //PHASE 1
            List<SubPhase> listSubPhases = new List<SubPhase>();
            //SUB 1
            //how many SGs?
            List<int> listSGids = new List<int>();
            List<int> listStates = new List<int>();
            int numSGS = 11;
            for (int k = 0; k < numSGS; k++)
            {
                listSGids.Add(k + 1);
            }
            listStates.AddRange(new int[] { 3, 3, 1, 1, 1, 1, 1, 1, 1, 3, 3});
            listSubPhases.Add(new SubPhase(listSGids, listStates, 22, true));

            //SUB 2
            listStates.Clear();
            listStates.AddRange(new int[] { 3, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1});
            listSubPhases.Add(new SubPhase(listSGids, listStates, 3, false));

            //SUB 3
            listStates.Clear();
            listStates.AddRange(new int[] { 4, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1 });
            listSubPhases.Add(new SubPhase(listSGids, listStates, 3, false));

            //SUB 4
            listStates.Clear();
            listStates.AddRange(new int[] { 1, 1, 2, 2, 1, 1, 1, 1, 1, 1, 1 });
            listSubPhases.Add(new SubPhase(listSGids, listStates, 2, false));

            listPhases.Add(new Phase(listSubPhases));
            listSubPhases.Clear();

            //PHASE 2
            //SUB 1
            listStates.Clear();
            listStates.AddRange(new int[] { 1, 1, 3, 3, 1, 2, 1, 1, 1, 1, 1 });
            listSubPhases.Add(new SubPhase(listSGids, listStates, 2, false));

            //SUB 2
            listStates.Clear();
            listStates.AddRange(new int[] { 1, 1, 3, 3, 1, 3, 1, 1, 3, 1, 1 });
            listSubPhases.Add(new SubPhase(listSGids, listStates, 18, true));

            //SUB 3
            listStates.Clear();
            listStates.AddRange(new int[] { 1, 1, 4, 4, 1, 3, 1, 1, 3, 1, 1 });
            listSubPhases.Add(new SubPhase(listSGids, listStates, 3, false));

            //SUB 4
            listStates.Clear();
            listStates.AddRange(new int[] { 1, 1, 1, 1, 2, 3, 1, 2, 3, 1, 1 });
            listSubPhases.Add(new SubPhase(listSGids, listStates, 2, false));

            listPhases.Add(new Phase(listSubPhases));
            listSubPhases.Clear();

            //PHASE 3
            //SUB 1
            listStates.Clear();
            listStates.AddRange(new int[] { 1, 1, 1, 1, 3, 4, 1, 3, 1, 1, 1 });
            listSubPhases.Add(new SubPhase(listSGids, listStates, 3, false));

            //SUB 2
            listStates.Clear();
            listStates.AddRange(new int[] { 1, 1, 1, 1, 3, 1, 1, 3, 1, 1, 1 });
            listSubPhases.Add(new SubPhase(listSGids, listStates, 13, true));

            //SUB 3
            listStates.Clear();
            listStates.AddRange(new int[] { 1, 1, 1, 1, 3, 1, 1, 4, 1, 1, 1 });
            listSubPhases.Add(new SubPhase(listSGids, listStates, 3, false));

            //SUB 4
            listStates.Clear();
            listStates.AddRange(new int[] { 1, 1, 1, 1, 3, 1, 1, 1, 1, 1, 1 });
            listSubPhases.Add(new SubPhase(listSGids, listStates, 1, false));

            //SUB 5
            listStates.Clear();
            listStates.AddRange(new int[] { 1, 1, 1, 1, 3, 1, 3, 1, 1, 1, 1 });
            listSubPhases.Add(new SubPhase(listSGids, listStates, 8, true));

            //SUB 6
            listStates.Clear();
            listStates.AddRange(new int[] { 1, 1, 1, 1, 4, 1, 1, 1, 1, 1, 1 });
            listSubPhases.Add(new SubPhase(listSGids, listStates, 3, false));

            //SUB 7
            listStates.Clear();
            listStates.AddRange(new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 });
            listSubPhases.Add(new SubPhase(listSGids, listStates, 2, false));

            //SUB 8
            listStates.Clear();
            listStates.AddRange(new int[] { 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1 });
            listSubPhases.Add(new SubPhase(listSGids, listStates, 2, false));

            listPhases.Add(new Phase(listSubPhases));
            listSubPhases.Clear();

            signalProgram = new SignalProgram(listPhases, sigController);
            MessageBox.Show("", "");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            sim.RunStepByStep(signalProgram);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            sim.RunMultipleStepByStep(signalProgram, 10);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            double simulationPeriod = COM.getSimulationPeriod(sim.currentSimulation);
            for (int i = 300; i < simulationPeriod; i = i + 300)
            {
                sim.RunContinuos(i);
                if (i % 600 == 0)
                {
                    ISignalController SignalController = vissim.GetVissimInstance().Net.SignalControllers.get_ItemByKey(3);
                    SignalController.set_AttValue("ProgNo", 1);
                }
                else
                {
                    vissim.GetVissimInstance().Net.SignalControllers.get_ItemByKey(3).set_AttValue("ProgNo", 2);
                }
            }
            
        }

        private void button13_Click(object sender, EventArgs e)
        {
            vissim = new VissimConnection();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                COM.LoadVissimNetwork(vissim.GetVissimInstance(), openFileDialog1.FileName);
            }
            sim = new Simulation(vissim);
            sim.SetSimulationResolution(1);


            BLL.Neural.SOM som = new BLL.Neural.SOM(25, 5, 5, 0.5, 1, 18, 100);
            OpenFileDialog openFileDialog2 = new OpenFileDialog();
            if (openFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                double[][] importedData = BLL.HelpMethods.LoadTrainingSet(openFileDialog2.FileName, ',');
                if (importedData != null)
                {
                    
                    som.TrainSOM(importedData, 1000);
                }

            }
            EpsilonGreedyExploration greedyExploration = new EpsilonGreedyExploration(1);
            double beta = 0;
            BLL.Neural.Q q = new BLL.Neural.Q(25, 7, greedyExploration);
            q.qLearning.DiscountFactor = 0.1;
            
            BLL.Thesis.Actions actions = new BLL.Thesis.Actions(25,22);

            double simulationPeriod = COM.getSimulationPeriod(sim.currentSimulation);
            ISignalController SignalController = vissim.GetVissimInstance().Net.SignalControllers.get_ItemByKey(3);
            StreamWriter writer = new StreamWriter("Results1000.csv");
            for (int j = 0; j < 1000; j++)
            {
                sim.RunContinuos(299);
                for (int i = 599; i <= simulationPeriod -1; i = i + 300)
                {

                    double[] qLenMax = sim.queueCounterResultsMax;
                    double[] qLenAvg = sim.queueCounterResultsAvg;
                    double[] qLenAll = new double[18];

                    Array.Copy(qLenAvg, qLenAll, qLenAvg.Length);
                    Array.Copy(qLenMax, 0, qLenAll, qLenAvg.Length, qLenMax.Length);
                    

                    double delayAvgBefore = vissim.GetVissimInstance().Net.VehicleNetworkPerformanceMeasurement.get_AttValue("DelayAvg(Current, Current, All)");

                    int state1 = som.GetWinningNeuronNumber(qLenAll);

                    int action = q.GetAction(state1);
                    
                    actions.PerformAction(state1, action, SignalController);

                    sim.RunContinuos(i);

                    qLenMax = sim.queueCounterResultsMax;
                    qLenAvg = sim.queueCounterResultsAvg;
                    Array.Copy(qLenAvg, qLenAll, qLenAvg.Length);
                    Array.Copy(qLenMax, 0, qLenAll, qLenAvg.Length, qLenMax.Length);

                    int state2 = som.GetWinningNeuronNumber(qLenAll);

                    double delayAvgAfter= vissim.GetVissimInstance().Net.VehicleNetworkPerformanceMeasurement.get_AttValue("DelayAvg(Current, Current, All)");

                    double reward = BLL.Neural.Q.CalculateReward(qLenAvg, delayAvgBefore, delayAvgAfter, beta);

                    q.UpdateQTable(state1, action, reward, state2);
                }
                
                double resultTTS = vissim.GetVissimInstance().Net.VehicleNetworkPerformanceMeasurement.get_AttValue("TravTmTot(Current, Total, All)");
              //  int seed = sim.currentSimulation.get_AttValue("RandSeed");
                writer.WriteLine(j + "," + resultTTS);
                q.qLearning.ExplorationPolicy = new EpsilonGreedyExploration((Math.Pow(0.995, (j+1)) * 0.99) + 0.01);
               // beta = (j + 1) * 0.001;
                writer.Flush();
                sim.currentSimulation.RunSingleStep();

            }
            
            writer.Close();


            
        }
    }
}

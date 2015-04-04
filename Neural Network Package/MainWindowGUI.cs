using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neural_Network_Package
{
    public partial class MainWindowGUI : Form
    {
        public MainWindowGUI()
        {
            InitializeComponent();
        }

        private void LMSButton_Click(object sender, EventArgs e)
        {
            //helperClass.SLP_GUI = new Single_layer_perceptron_GUI();
           // helperClass.SLP_GUI.Show();
            helperClass.SLP_GUI = new Single_layer_perceptron_GUI();
            //Single_layer_perceptron_GUI GUI = new Single_layer_perceptron_GUI();
            helperClass.SLP_GUI.Show();
            this.Visible = false;
            //Application.Run(helperClass.SLP_GUI);
            //this.Close();
        }

        private void BPButton_Click(object sender, EventArgs e)
        {
            helperClass.MLP_GUI = new Multi_Layer_Perceptron_GUI();
            helperClass.MLP_GUI.Show();
            this.Visible = false;
        }
    }
}

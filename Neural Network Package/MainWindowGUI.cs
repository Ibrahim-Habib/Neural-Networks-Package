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
            //Application.Run(new Single_layer_perceptron_GUI());
            Single_layer_perceptron_GUI GUI = new Single_layer_perceptron_GUI();
            GUI.Show();
            this.Visible = false;
        }

        private void BPButton_Click(object sender, EventArgs e)
        {

        }
    }
}

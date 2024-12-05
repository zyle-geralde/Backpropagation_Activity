using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Backprop;

namespace BackPropagation
{
    public partial class Form1 : Form
    {
        NeuralNet nn;
        int b1indic = 0;
        int b2indic = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox6.Text.Length == 0)
            {
                MessageBox.Show($"Number of hidden neurons should be identified.");
                return;
            }
            nn = new NeuralNet(4, Convert.ToInt32(textBox6.Text), 1);
            b1indic = 1;
            b2indic = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(b1indic == 0)
            {
                MessageBox.Show($"Create Backpropagation Neural Network.");
                return;
            }
            int epochs = 0;
            int maxepochs = 100000;
            bool allcorrect = false;

            while (epochs < maxepochs && !allcorrect)
            {
                epochs++;
                allcorrect = true;
                double[,] inputs = {
                    {0, 0, 0, 0},
                    {0, 0, 0, 1},
                    {0, 0, 1, 0},
                    {0, 0, 1, 1},
                    {0, 1, 0, 0},
                    {0, 1, 0, 1},
                    {0, 1, 1, 0},
                    {0, 1, 1, 1},
                    {1, 0, 0, 0},
                    {1, 0, 0, 1},
                    {1, 0, 1, 0},
                    {1, 0, 1, 1},
                    {1, 1, 0, 0},
                    {1, 1, 0, 1},
                    {1, 1, 1, 0},
                    {1, 1, 1, 1}
                };

                double[] outputs = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1};

                for (int i =0; i <16;i++)
                {
                    nn.setInputs(0, inputs[i, 0]);
                    nn.setInputs(1, inputs[i, 1]);
                    nn.setInputs(2, inputs[i, 2]);
                    nn.setInputs(3, inputs[i, 3]);
                    nn.setDesiredOutput(0, outputs[i]);

                    nn.learn();

                    nn.run();
                    double predOutput =nn.getOuputData(0);
                    double classifOutput =predOutput >= 0.5 ? 1.0 : 0.0;
                    if (classifOutput !=outputs[i])
                    {
                        allcorrect = false;
                    }
                }
                string messageDisp = "";
                if (allcorrect)
                {
                    messageDisp = "All outputs are correct";
                }
                else
                {
                    messageDisp = "Learning...";
                }
                Console.WriteLine($"Epoch {epochs}: {messageDisp}");
            }

            if (allcorrect)
            {
                MessageBox.Show($"Network with {textBox6.Text} hidden neorons learned the 4-input AND gate in {epochs} epochs.");
                label8.Text = ""+epochs;
                b2indic = 1;
            }
            else
            {
                MessageBox.Show($"Max epochs reached.");
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(b2indic == 0)
            {
                MessageBox.Show($"Train Neural Network first.");
                return;
            }
            if(textBox1.Text.Length == 0 || textBox2.Text.Length == 0 || textBox3.Text.Length == 0 || textBox4.Text.Length == 0)
            {
                MessageBox.Show($"Fill in all missing inputs");
                return;
            }
            if((textBox1.Text == "1" || textBox1.Text == "0") && (textBox2.Text == "1" || textBox2.Text == "0") && (textBox3.Text == "1" || textBox3.Text == "0") && (textBox4.Text == "1" || textBox4.Text == "0"))
            {

            }
            else
            {
                MessageBox.Show($"Input Must be 0 or 1");
                return;
            }
            nn.setInputs(0, Convert.ToDouble(textBox1.Text));
            nn.setInputs(1, Convert.ToDouble(textBox2.Text));
            nn.setInputs(2, Convert.ToDouble(textBox3.Text));
            nn.setInputs(3, Convert.ToDouble(textBox4.Text));
            nn.run();
            double outputData = nn.getOuputData(0);
            double displayData = 1;
            if (outputData >= 0.5)
            {
                displayData = 1;
            }
            else
            {
                displayData = 0;
            }
            textBox5.Text = "" + displayData;

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Hello");
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            return;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            return;
        }
    }
}

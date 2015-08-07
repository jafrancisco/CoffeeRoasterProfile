using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Threading;


namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            label2.Text = "No hay conexión";
        }

        string[] puertos = SerialPort.GetPortNames();
        string puerto = "";
        int dato = 0;
        int contador = 0;

        private void Form1_Load(object sender, EventArgs e)

        {
            foreach (string mostrar in puertos)
            {
                comboBox1.Items.Add(mostrar);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string puerto = "";
            puerto = comboBox1.Text;
            try
            {
                serialPort1.PortName = puerto;
                serialPort1.Open();
                CheckForIllegalCrossThreadCalls = false;
                if (serialPort1.IsOpen == true) ;
                {
                    timer1.Start();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString(), "Error");
                timer1.Stop();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            serialPort1.Dispose();
            label2.Text = "Tarjeta no conectada";
            timer1.Stop();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();

        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            dato = serialPort1.ReadByte();
            contador++;
            if (contador == 1)
            {
                label2.Text = "Recibiendo datos";
                int humedad = dato;
                label5.Text = humedad.ToString();
            }
            if (contador == 2)
            {
                label2.Text = "Recibiendo datos";
                int temperatura = dato;
                label6.Text = temperatura.ToString();
                contador = 0;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = "";
        }
    }
}

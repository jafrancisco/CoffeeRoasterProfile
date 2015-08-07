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
            chart1.Update();
            chart1.Legends.Clear();
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
            label2.Text = "No hay conexión";
        }

        string[] puertos = SerialPort.GetPortNames();
        string puerto = "";
        int dato = 0;
        int contador = 0;
        int contador2 = 1;
        int contador3 = 1;
        int var1 = 2;
        int rpm=0;
        int starfan = 0;
        int stopfan = 0;
        private string file00;
       // string var2 = var1.ToString();




        

        
            

        //public void OpenArduinoConnection()
        //{
         //  if(serialPort1.IsOpen)
          // {
             //  serialPort1.Write("1");

           //}
           //else
          // {
          //     throw new InvalidOperationException("Can't Communicate");
          // }
      //  }
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
                if (serialPort1.IsOpen == true) 
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
            serialPort1.Write("0");
            serialPort1.Close();
            serialPort1.Dispose();
            label2.Text = "Tarjeta no conectada";
            timer1.Stop();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            Close();

        }

        public void button3_Click(object sender, EventArgs e)
        {
           
                 
                serialPort1.Write("1");
                
           
           
            }
        private void button4_Click(object sender, EventArgs e)
        {
            serialPort1.Write("0");
            serialPort1.Write("0");
            serialPort1.Write("0");
            serialPort1.Write("0");
        }   
        
        
        
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
           
            
            dato = serialPort1.ReadByte();
            contador++;
            contador2++;
            contador3++;


            if (contador == 1)
            {
                label2.Text = "Recibiendo datos";
                int humedad = dato;
                int fan = starfan;
                label5.Text = humedad.ToString();
                chart1.Series[3].Points.AddXY(contador2++, fan);
                
            }
            if (contador == 2)
            {
                label2.Text = "Recibiendo datos";
                int temperatura = dato;
                label6.Text = temperatura.ToString();
                //contador = 0;
                chart1.Series[0].Points.AddXY(contador2++, temperatura);
                

            }
            if (contador == 3)
            {
                label2.Text = "Recibiendo datos";
                int rpm = dato;
                label7.Text = rpm.ToString();
                contador = 0;
                chart1.Series[1].Points.AddXY(contador2++, rpm);

            }
            //if (dato > 50)
           // {
            //    chart1.Series[0].Points.AddXY(contador3++, dato);
           // }
          }  

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = "";
            
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void saveFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bmp = new Bitmap(1397, 655);
            chart1.DrawToBitmap(bmp, new Rectangle(0, 0, 2095, 982));
            string number ="1";
            bmp.Save(@"C:\Temp\" + number +".png");

        }

        private void button5_Click(object sender, EventArgs e)
        {
            starfan = 1;
           // if (serialPort1.IsOpen == true) ;
               // {
                 //   chart1.Series[3].Points.AddXY(contador2++, starfan);
                //}
            
                
        }

        private void button6_Click(object sender, EventArgs e)
        {
            starfan = 0;
            // if (serialPort1.IsOpen == true) ;
              //  {
                //    chart1.Series[3].Points.AddXY(contador2++, starfan);
                //}
            
        }

        
        

        }
        }

       
    


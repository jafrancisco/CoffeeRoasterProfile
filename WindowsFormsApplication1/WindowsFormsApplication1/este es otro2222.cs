using System;
using System.Collections;
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
            //chart1.Legends.Clear();
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
            label10.Text = "Not Loaded";
            label2.Text = "Press Start Up to Start";
            label10.Text = "Not Started";
            label11.Text = "OFF";
            label12.Text = "OFF";
            label5.Text = "Not Reading";
            label6.Text = "Not Reading";
            label7.Text = "Not Reading";
            label9.Text = "Not Loaded";
            label13.Text = "Not Registered";
            label8.Text = "Not Registered";
            DateTime theDate = DateTime.Now;
            string lafecha = "";
            lafecha = theDate.ToString("MM/dd/yyyy H:mm");
            label22.Text = lafecha;

        }

        string[] puertos = SerialPort.GetPortNames();
        string puerto = "";
        float dato = 0;
        double contador = 0;
        double contador2 = 1;
        double contador3 = 1;
        int var1 = 2;
        int rpm=0;
        int starfan = 0;
        int stopfan = 0;
        private string file00;
        int heatingstart = 0;
        int beansdrop = 0;
        int beansload = 0;
        int exhaustfan= 0;
        int specialevent = 0;
        int fcracks = 0;
        int scracks = 0;
        int fcracke = 0;
        int scracke = 0;
        uint Seegundos = 0;
        uint minutes = 0;
        uint hours = 0;
        int tiempo = 0;
        double rl = 1.9;
        double adc =0;
        double rs = 31;
        double mqr = 0;
        double ratio = 0;
       double factortime = 0.291938;
        //double a1, a2, b1, b2, s;
        
        string profilename = "notdeclared";
        
        
        
       
        private void Form1_Load(object sender, EventArgs e)

        {
            //timer2.Tick += new EventHandler(this.timer2_Tick);
           // timer2.Interval = 1000;
            
            


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
            serialPort1.Write("0");
            serialPort1.Close();
            serialPort1.Dispose();
            label2.Text = "Device is not connected";
            timer1.Stop();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            Close();

        }

        public void button3_Click(object sender, EventArgs e)
        {
           
                 
                serialPort1.Write("1");
                timer2.Start();
           
           
            }
        private void button4_Click(object sender, EventArgs e)
        {
            timer2.Stop();
            serialPort1.Write("0");
            serialPort1.Write("0");
            serialPort1.Write("0");
            serialPort1.Write("0");
        }   
        
        
        
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            string dato0 = serialPort1.ReadLine();

            double dato2 = Convert.ToDouble(dato0);
           
            contador++;
            contador2++;
            contador3++;


            if (contador == 1)
            {
                label2.Text = "Receiving data";
                double humedad = dato2;
                label5.Text = humedad.ToString();
                
            }
            if (contador == 2)
            {
                label2.Text = "Receiving data";
                double temperatura = dato2;
                label6.Text = temperatura.ToString();
                //contador = 0;
                chart1.Series[0].Points.AddXY(contador2++/745, temperatura);
                
             
                

            }
            if (contador == 3)
            {
                label2.Text = "Receiving data";
                double rpm = dato2;
                label7.Text = rpm.ToString();
                //contador = 0;
                chart1.Series[1].Points.AddXY(contador2++/745 , rpm);
                chart1.Series[2].Points.AddXY(contador2++/745 , starfan);
                chart1.Series[3].Points.AddXY(contador2++/745 , exhaustfan);
               
            }
            if (contador == 4)
            {
                
                double lpg = dato2;
                double dato3 = lpg;
                mqr = rl * (1023 - dato3) / dato3;
                ratio = Math.Log(rs/mqr,10);
                double ethanolx = 1;
                double toluenelx = 1;
                double coxlx = 1;
                double chforlx = 1;
                double benzenelx = 1;
                double propanelx = 1;
                double ethanoly = -0.3768;
                double toluenely = -0.2924;
                double coxly = -0.2076;
                double chforly = -0.3565;
                double benzenely = -0.4437;
                double propanely = -0.3872;
                double ethanolp = -0.5093;
                double toluenelp = -0.4523;
                double coxlp = -0.3153;
                double chforlp = -0.4130;
                double benzenelp = -0.4771;
                double propanelp = -0.5336;

                double ethanol = Math.Pow((ratio - ethanoly / ethanolp + ethanolx), 10) * 1000;
                double toluene = Math.Pow((ratio - toluenely / toluenelp + toluenelx), 10) * 1000;
               
                double cox = Math.Pow((ratio - coxly / coxlp + coxlx), 10) * 1000;
                double chfor = Math.Pow((ratio - chforly / chforlp + chforlx), 10) * 1000;
                double benzene = Math.Pow((ratio - benzenely / benzenelp + benzenelx), 10) * 1000;
                double propane = Math.Pow((ratio - propanely / propanelp + propanelx), 10) * 1000;
                label35.Text = lpg.ToString();
                label30.Text = Convert.ToString(ethanol);
                label29.Text = Convert.ToString(toluene);
                label28.Text = Convert.ToString(cox);
                label26.Text = Convert.ToString(chfor);
                label25.Text = Convert.ToString(benzene);
                label40.Text = Convert.ToString(propane);
                

                chart2.Series[0].Points.AddXY(contador2++/745, ethanol); //LPG
                chart2.Series[1].Points.AddXY(contador2++/745, toluene); //LPG
                chart2.Series[2].Points.AddXY(contador2++/745, cox); //LPG
                chart2.Series[3].Points.AddXY(contador2++/745, chfor); //LPG
                chart2.Series[4].Points.AddXY(contador2++/745, benzene); //LPG
                chart2.Series[5].Points.AddXY(contador2++/745, propane); //LPG
                contador = 0;

            }
            
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
            //this.Hide();
         
            SendKeys.Send("{PRTSC}");
            
            
            //this.Show();

            
            
            //Bitmap bmp = new Bitmap(1397, 655);
            //chart1.DrawToBitmap(bmp, new Rectangle(0, 0, 2095, 982));
            //string number ="1";
            //bmp.Save(@"C:\Temp\" + number +".png");

        }

        private void button5_Click(object sender, EventArgs e)
        {
            starfan = 10;
            label12.Text = "ON";
          
                
        }

        private void button6_Click(object sender, EventArgs e)
        {
            starfan = 0;
            label12.Text = "OFF";
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            exhaustfan = 20;
            label11.Text = "ON";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            exhaustfan = 0;
            label11.Text = "OFF";
        }

        private void button16_Click(object sender, EventArgs e)
        {
            heatingstart = 70;
            chart1.Series[4].Points.AddXY(contador2++/745, heatingstart);
            label10.Text = "Started";
            
        }

        private void label9_Click(object sender, EventArgs e)
        {
           

        }

        private void button15_Click(object sender, EventArgs e)
        {
            beansload = 100;
            chart1.Series[5].Points.AddXY(contador2++/745, beansload);
            label9.Text = "Loaded";
            label10.Text = "Completed";
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            
            Seegundos++;   

            if (Seegundos >= 60)
            {
                
                Seegundos = 0;
                minutes++; 

            }

            if (minutes >= 60)
            {
                minutes = 0;
                hours++;

            }
            //codigo segundos
            if (Seegundos <= 9){
             Segundos.Text = "0" + Seegundos.ToString();
            }
            if (Seegundos > 9)
            {
                Segundos.Text = Seegundos.ToString();
            }

            //codigo minutos

            if (minutes <= 9)
            {
              
                Minutes.Text = "0" + minutes.ToString();
            }
            if (minutes > 9)
            {
                Minutes.Text = minutes.ToString();
            }
          //codigo horas

            if (hours <= 9)
            {
                Hours.Text = "0" + hours.ToString();
            }
            if (hours > 9)
            {
                Hours.Text = hours.ToString();
            }
            

            }

        private void button12_Click(object sender, EventArgs e)
        {
            fcracks = 85;
            chart1.Series[6].Points.AddXY(contador2++/745, fcracks);
            label8.Text = "Registered";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            fcracke = 85;
            chart1.Series[9].Points.AddXY(contador2++/745, fcracke);
            label8.Text = "Completed";
        }

        private void button14_Click(object sender, EventArgs e)
        {
            beansdrop = 100;
            label9.Text = "Dropped";
            chart1.Series[8].Points.AddXY(contador2++/745, beansdrop);

        }

        private void button10_Click(object sender, EventArgs e)
        {
            scracks = 95;
            chart1.Series[7].Points.AddXY(contador2++/745, scracks);
            label13.Text = "Registered";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            scracke = 95;
            chart1.Series[10].Points.AddXY(contador2++/745, scracke);
            label13.Text = "Completed";
        }

        private void button13_Click(object sender, EventArgs e)
        {
            specialevent = 35;
            chart1.Series[11].Points.AddXY(contador2++/745, specialevent);
            
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            profilename = textBox1.Text;

        }

        private void saveProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{PRTSC}");
            System.Threading.Thread.Sleep(1000);
            Image img = Clipboard.GetImage();
            pictureBox3.Image = img;
            img.Save("C:\\Profiles\\" + profilename + ".jpg");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string cajita = textBox2.Text;
            rs = Convert.ToDouble(cajita);
           
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            //string cajita2 = textBox3.Text;
            //rl = Convert.ToDouble(cajita2);
            //textBox3.Text = trackBar1.Value.ToString();

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {


            rs = Convert.ToDouble(trackBar1.Value) * 0.1;
            textBox2.Text = rs.ToString();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
             rl = Convert.ToDouble(trackBar2.Value) / 10;
            textBox3.Text = rl.ToString();

        }

        private void button17_Click(object sender, EventArgs e)
        {
            
            label10.Text = "Not Loaded";
            label2.Text = "Press Start Up to Start";
            label10.Text = "Not Started";
            label11.Text = "OFF";
            label12.Text = "OFF";
            label5.Text = "Not Reading";
            label6.Text = "Not Reading";
            label7.Text = "Not Reading";
            label9.Text = "Not Loaded";
            label13.Text = "Not Registered";
            label8.Text = "Not Registered";
            
        
        chart1.Series.Clear();
        chart2.Series.Clear();
        }


            


        }

        
        

        }
        


       
    


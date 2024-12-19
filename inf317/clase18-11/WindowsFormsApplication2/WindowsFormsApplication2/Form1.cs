using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Thread hilo = new Thread(mueve_etiqueta);
            hilo.IsBackground = true;
            hilo.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void mueve_etiqueta()
        {
            while (true)
            {
                Invoke((MethodInvoker)delegate
                {
                    label1.Left += 5;
                    if (label1.Left > 300)
                        label1.Left = 0;
                });

                Thread.Sleep(100);
            }
        }
    }
}

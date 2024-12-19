using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        private int suma(int a,int b)
        {
            return a+b;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int a, b, c;
            a = Convert.ToInt32(textBox1.Text);
            b = Convert.ToInt32(textBox2.Text);
            c = a + b;
            textBox3.Text = c.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int a, b, c;
            a = Convert.ToInt32(textBox1.Text);
            b = Convert.ToInt32(textBox2.Text);
            c = suma(a,b);
            textBox3.Text = c.ToString();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Class1 c1 = new Class1();
            int a, b, c;
            a = Convert.ToInt32(textBox1.Text);
            b = Convert.ToInt32(textBox2.Text);
            c = c1.suma(a, b);
            textBox3.Text = c.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClassLibrary1.Class1 cc1 = new ClassLibrary1.Class1();
            int a, b, c;
            a = Convert.ToInt32(textBox1.Text);
            b = Convert.ToInt32(textBox2.Text);
            c = cc1.suma(a, b);
            textBox3.Text = c.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ServiceReference2.WebServiceSoapClient sc1 =new ServiceReference2.WebServiceSoapClient();
            int a, b, c;
            a = Convert.ToInt32(textBox1.Text);
            b = Convert.ToInt32(textBox2.Text);
            c = sc1.suma(a,b);
            textBox3.Text = c.ToString();

        }
    }
}

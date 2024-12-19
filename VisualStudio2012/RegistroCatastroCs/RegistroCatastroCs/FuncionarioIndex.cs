using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegistroCatastroCs
{
    public partial class FuncionarioIndex : Form
    {
        public FuncionarioIndex()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form login = new Form1();
            login.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form form2 = new Form2("funcionario");
            form2.Show();
            this.Hide();
        }
    }
}

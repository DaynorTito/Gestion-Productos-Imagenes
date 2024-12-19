using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace RegistroCatastroCs
{
    public partial class DuenioIndex : Form
    {
        string ci;
        SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-FQ2GHMQ;Initial Catalog=BDDaynor;Integrated Security=True");

        public DuenioIndex(string ci)
        {
            InitializeComponent();
            this.ci = ci;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DuenioIndex_Load(object sender, EventArgs e)
        {
            CargarPropiedades();
        }

        private void CargarPropiedades()
        {
            try
            {
                conexion.Open();

                string query = "SELECT * FROM catastro WHERE ciDuenio = @ci";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@ci", ci);

                SqlDataAdapter adaptador = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adaptador.Fill(dt);

                dataGridView1.DataSource = dt;
                dataGridView1.Columns["id"].HeaderText = "ID";
                dataGridView1.Columns["zona"].HeaderText = "Zona";
                dataGridView1.Columns["distrito"].HeaderText = "Distrito";
                dataGridView1.Columns["superficie"].HeaderText = "Superficie";
                dataGridView1.Columns["xini"].HeaderText = "X Inicial";
                dataGridView1.Columns["yini"].HeaderText = "Y Inicial";
                dataGridView1.Columns["xfin"].HeaderText = "X Final";
                dataGridView1.Columns["yfin"].HeaderText = "Y Final";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conexion.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form login = new Form1();
            login.Show();
            this.Hide();
        }
    }
}

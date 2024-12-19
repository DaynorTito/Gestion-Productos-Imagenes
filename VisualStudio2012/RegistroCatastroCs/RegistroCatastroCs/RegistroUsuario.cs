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
    public partial class RegistroUsuario : Form
    {
        SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-FQ2GHMQ;Initial Catalog=BDDaynor;Integrated Security=True");

        public RegistroUsuario()
        {
            InitializeComponent();
        }

        private void RegistroUsuario_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }
        private void LimpiarTextBoxes() {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string ci = textBox1.Text;
            string nombre = textBox2.Text;
            string paterno = textBox3.Text;
            string materno = textBox4.Text;
            string direccion = textBox5.Text;
            string contrasena = textBox6.Text;
            try
            {
                conexion.Open();
                string consultaVerificar = "SELECT COUNT(*) FROM persona WHERE ci = @ci";
                SqlCommand cmdVerificar = new SqlCommand(consultaVerificar, conexion);
                cmdVerificar.Parameters.AddWithValue("@ci", ci);
                int existe = (int)cmdVerificar.ExecuteScalar();
                if (existe > 0)
                {
                    MessageBox.Show("El CI ya está registrado. Por favor, use otro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string consultaInsertar = "INSERT INTO persona (ci, nombre, paterno, materno, direccion, contrasena, rol) " +
                                               "VALUES (@ci, @nombre, @paterno, @materno, @direccion, @contrasena, 'duenio')";
                    SqlCommand cmdInsertar = new SqlCommand(consultaInsertar, conexion);
                    cmdInsertar.Parameters.AddWithValue("@ci", ci);
                    cmdInsertar.Parameters.AddWithValue("@nombre", nombre);
                    cmdInsertar.Parameters.AddWithValue("@paterno", paterno);
                    cmdInsertar.Parameters.AddWithValue("@materno", materno);
                    cmdInsertar.Parameters.AddWithValue("@direccion", direccion);
                    cmdInsertar.Parameters.AddWithValue("@contrasena", contrasena);

                    cmdInsertar.ExecuteNonQuery();

                    MessageBox.Show("Registro exitoso!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpiarTextBoxes();
                }
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

        private void button3_Click(object sender, EventArgs e)
        {
            Form login = new Form1();
            login.Show();
            this.Hide();
        }
    }
}

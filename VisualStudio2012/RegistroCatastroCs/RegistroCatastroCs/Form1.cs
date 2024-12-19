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
    public partial class Form1 : Form
    {
        SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-FQ2GHMQ;Initial Catalog=BDDaynor;Integrated Security=True");

        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ci = textBox1.Text;
            string password = textBox2.Text;

            try
            {
                conexion.Open();
                string query = "SELECT rol FROM persona WHERE ci = @ci AND contrasena = @password";
                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@ci", ci);
                cmd.Parameters.AddWithValue("@password", password);

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    string rol = result.ToString();
                    Form nuevaVentana;
                    switch (rol)
                    {
                        case "admin":
                            nuevaVentana = new AdminIndex();
                            break;
                        case "funcionario":
                            nuevaVentana = new FuncionarioIndex();
                            break;
                        case "duenio":
                            nuevaVentana = new DuenioIndex(ci);
                            break;
                        default:
                            MessageBox.Show("Rol no reconocido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                    }

                    nuevaVentana.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Credenciales incorrectas. Intente de nuevo.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("Acceso no autorizado. Verifique sus credenciales.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void button2_Click(object sender, EventArgs e)
        {
            Form registroForm = new RegistroUsuario();
            registroForm.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

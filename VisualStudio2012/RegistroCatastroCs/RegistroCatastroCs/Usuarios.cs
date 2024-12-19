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
    public partial class Usuarios : Form
    {
        SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-FQ2GHMQ;Initial Catalog=BDDaynor;Integrated Security=True");
        private bool isEditing = false;
        public Usuarios()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form registroForm = new AdminIndex();
            registroForm.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.ReadOnly = false; 
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
        }

        private void Usuarios_Load(object sender, EventArgs e)
        {
            CargarUsuarios();
        }
        private void CargarUsuarios()
        {
            try
            {
                conexion.Close();
                conexion.Open();

                string query = "SELECT ci, nombre, paterno, materno, direccion, rol FROM persona";
                SqlCommand cmd = new SqlCommand(query, conexion);

                SqlDataAdapter adaptador = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adaptador.Fill(dt);

                dataGridView1.DataSource = dt;

                dataGridView1.Columns["ci"].HeaderText = "C.I.";
                dataGridView1.Columns["nombre"].HeaderText = "Nombre";
                dataGridView1.Columns["paterno"].HeaderText = "Apellido Paterno";
                dataGridView1.Columns["materno"].HeaderText = "Apellido Materno";
                dataGridView1.Columns["direccion"].HeaderText = "Dirección";
                dataGridView1.Columns["rol"].HeaderText = "Rol";
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

        private void button4_Click(object sender, EventArgs e)
        {
            CargarUsuarios();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow filaSeleccionada = dataGridView1.SelectedRows[0];

                textBox1.Text = filaSeleccionada.Cells["ci"].Value.ToString();
                textBox2.Text = filaSeleccionada.Cells["nombre"].Value.ToString();
                textBox3.Text = filaSeleccionada.Cells["paterno"].Value.ToString();
                textBox4.Text = filaSeleccionada.Cells["materno"].Value.ToString();
                textBox5.Text = filaSeleccionada.Cells["direccion"].Value.ToString();
                comboBox1.Text = filaSeleccionada.Cells["rol"].Value.ToString();

                isEditing = true;

                textBox1.ReadOnly = true; 
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;
                textBox4.ReadOnly = false;
                textBox5.ReadOnly = false;
                textBox6.ReadOnly = false;
                comboBox1.Enabled = true;
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un usuario para editar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string ci = textBox1.Text;
            string nombre = textBox2.Text;
            string paterno = textBox3.Text;
            string materno = textBox4.Text;
            string direccion = textBox5.Text;
            string contrasena = textBox6.Text;
            string rol = comboBox1.SelectedItem != null ? comboBox1.SelectedItem.ToString() : string.Empty;

            try
            {
                if (conexion.State != ConnectionState.Open)
                {
                    conexion.Open();
                }

                if (isEditing)
                {
                    string updateQuery = "UPDATE persona SET nombre = @nombre, paterno = @paterno, materno = @materno, direccion = @direccion, rol = @rol";
                    if (!string.IsNullOrEmpty(contrasena))
                    {
                        updateQuery += ", contrasena = @contrasena";
                    }

                    updateQuery += " WHERE ci = @ci";

                    SqlCommand updateCmd = new SqlCommand(updateQuery, conexion);
                    updateCmd.Parameters.AddWithValue("@ci", ci);
                    updateCmd.Parameters.AddWithValue("@nombre", nombre);
                    updateCmd.Parameters.AddWithValue("@paterno", paterno);
                    updateCmd.Parameters.AddWithValue("@materno", materno);
                    updateCmd.Parameters.AddWithValue("@direccion", direccion);
                    updateCmd.Parameters.AddWithValue("@rol", rol);

                    if (!string.IsNullOrEmpty(contrasena))
                    {
                        updateCmd.Parameters.AddWithValue("@contrasena", contrasena);
                    }

                    int filasActualizadas = updateCmd.ExecuteNonQuery();

                    if (filasActualizadas > 0)
                    {
                        MessageBox.Show("Datos actualizados exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarUsuarios();
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron cambios para actualizar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    string checkQuery = "SELECT COUNT(*) FROM persona WHERE ci = @ci";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, conexion);
                    checkCmd.Parameters.AddWithValue("@ci", ci);
                    int existe = (int)checkCmd.ExecuteScalar();

                    if (existe > 0)
                    {
                        MessageBox.Show("El CI ya existe. No se puede registrar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        string insertQuery = "INSERT INTO persona (ci, nombre, paterno, materno, direccion, contrasena, rol) VALUES (@ci, @nombre, @paterno, @materno, @direccion, @contrasena, @rol)";
                        SqlCommand insertCmd = new SqlCommand(insertQuery, conexion);
                        insertCmd.Parameters.AddWithValue("@ci", ci);
                        insertCmd.Parameters.AddWithValue("@nombre", nombre);
                        insertCmd.Parameters.AddWithValue("@paterno", paterno);
                        insertCmd.Parameters.AddWithValue("@materno", materno);
                        insertCmd.Parameters.AddWithValue("@direccion", direccion);
                        insertCmd.Parameters.AddWithValue("@contrasena", contrasena);
                        insertCmd.Parameters.AddWithValue("@rol", rol);

                        int filasInsertadas = insertCmd.ExecuteNonQuery();

                        if (filasInsertadas > 0)
                        {
                            MessageBox.Show("Usuario registrado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarUsuarios();
                        }
                        else
                        {
                            MessageBox.Show("Error al registrar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conexion.State == ConnectionState.Open)
                {
                    conexion.Close();
                }
            }

            isEditing = false;
            LimpiarCampos();
        }

        private void LimpiarCampos()
        {
            textBox1.ReadOnly = false; 
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            comboBox1.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow filaSeleccionada = dataGridView1.SelectedRows[0];
                string ci = filaSeleccionada.Cells["ci"].Value.ToString();
                DialogResult resultado = MessageBox.Show("¿Está seguro de que desea eliminar este usuario?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        conexion.Open();
                        string deleteQuery = "DELETE FROM persona WHERE ci = @ci";
                        SqlCommand deleteCmd = new SqlCommand(deleteQuery, conexion);
                        deleteCmd.Parameters.AddWithValue("@ci", ci);

                        int filasEliminadas = deleteCmd.ExecuteNonQuery();

                        if (filasEliminadas > 0)
                        {
                            MessageBox.Show("Usuario eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarUsuarios();
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el usuario para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un usuario para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

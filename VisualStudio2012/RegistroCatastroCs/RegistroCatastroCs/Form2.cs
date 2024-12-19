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
    public partial class Form2 : Form
    {

        SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-FQ2GHMQ;Initial Catalog=BDDaynor;Integrated Security=True");
        private bool isEditing = false;
        private DataGridViewRow filaSeleccionada;
        private string rol;

        public Form2(string rol)
        {
            InitializeComponent();
            this.rol = rol;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            CargarCatastro();
        }

        private void CargarCatastro()
        {
            try
            {
                conexion.Close();
                conexion.Open();
                string query = "SELECT id, zona, distrito, superficie, xini, yini, xfin, yfin, ciDuenio FROM catastro";
                SqlCommand cmd = new SqlCommand(query, conexion);
                SqlDataAdapter adaptador = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adaptador.Fill(dt);
                dataGridView1.DataSource = dt;
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
            numericUpDown1.ReadOnly = false;
            string zona = textBox2.Text;
            string distrito = textBox1.Text;
            decimal id = numericUpDown1.Value;
            decimal superficie = numericUpDown2.Value;
            decimal xini = numericUpDown3.Value;
            decimal yini = numericUpDown4.Value;
            decimal xfin = numericUpDown5.Value;
            decimal yfin = numericUpDown6.Value;
            string ciDuenio = textBox3.Text;

            try
            {
                conexion.Open();

                string checkDuenioQuery = "SELECT COUNT(*) FROM persona WHERE ci = @ciDuenio";
                SqlCommand checkDuenioCmd = new SqlCommand(checkDuenioQuery, conexion);
                checkDuenioCmd.Parameters.AddWithValue("@ciDuenio", ciDuenio);
                int duenioCount = (int)checkDuenioCmd.ExecuteScalar();

                if (duenioCount == 0)
                {
                    MessageBox.Show("El C.I. del dueño no existe en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (isEditing)
                {
                    int num = Convert.ToInt32(filaSeleccionada.Cells["id"].Value);

                    string updateQuery = "UPDATE catastro SET zona = @zona, distrito = @distrito, superficie = @superficie, xini = @xini, yini = @yini, xfin = @xfin, yfin = @yfin, ciDuenio = @ciDuenio WHERE id = @id";

                    SqlCommand updateCmd = new SqlCommand(updateQuery, conexion);
                    updateCmd.Parameters.AddWithValue("@id", num);
                    updateCmd.Parameters.AddWithValue("@zona", zona);
                    updateCmd.Parameters.AddWithValue("@distrito", distrito);
                    updateCmd.Parameters.AddWithValue("@superficie", superficie);
                    updateCmd.Parameters.AddWithValue("@xini", xini);
                    updateCmd.Parameters.AddWithValue("@yini", yini);
                    updateCmd.Parameters.AddWithValue("@xfin", xfin);
                    updateCmd.Parameters.AddWithValue("@yfin", yfin);
                    updateCmd.Parameters.AddWithValue("@ciDuenio", ciDuenio);

                    int filasActualizadas = updateCmd.ExecuteNonQuery();

                    if (filasActualizadas > 0)
                    {
                        MessageBox.Show("Registro actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                        CargarCatastro();
                    }
                    else
                    {
                        MessageBox.Show("No se encontraron cambios para actualizar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    string checkIdQuery = "SELECT COUNT(*) FROM catastro WHERE id = @id";
                    SqlCommand checkIdCmd = new SqlCommand(checkIdQuery, conexion);
                    checkIdCmd.Parameters.AddWithValue("@id", id);
                    int idCount = (int)checkIdCmd.ExecuteScalar();

                    if (idCount > 0)
                    {
                        MessageBox.Show("Ya existe un catastro con el ID proporcionado.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string insertQuery = "INSERT INTO catastro (id, zona, distrito, superficie, xini, yini, xfin, yfin, ciDuenio) VALUES (@id, @zona, @distrito, @superficie, @xini, @yini, @xfin, @yfin, @ciDuenio)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, conexion);
                    insertCmd.Parameters.AddWithValue("@id", id);
                    insertCmd.Parameters.AddWithValue("@zona", zona);
                    insertCmd.Parameters.AddWithValue("@distrito", distrito);
                    insertCmd.Parameters.AddWithValue("@superficie", superficie);
                    insertCmd.Parameters.AddWithValue("@xini", xini);
                    insertCmd.Parameters.AddWithValue("@yini", yini);
                    insertCmd.Parameters.AddWithValue("@xfin", xfin);
                    insertCmd.Parameters.AddWithValue("@yfin", yfin);
                    insertCmd.Parameters.AddWithValue("@ciDuenio", ciDuenio);

                    int filasInsertadas = insertCmd.ExecuteNonQuery();

                    if (filasInsertadas > 0)
                    {
                        MessageBox.Show("Registro agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpiarCampos();
                        CargarCatastro();
                    }
                    else
                    {
                        MessageBox.Show("Error al agregar el registro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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

        private void LimpiarCampos()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            numericUpDown1.Value = 0;
            numericUpDown2.Value = 0;
            numericUpDown3.Value = 0;
            numericUpDown4.Value = 0;
            numericUpDown5.Value = 0;
            numericUpDown6.Value = 0;
            textBox3.Text = "";
            isEditing = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                filaSeleccionada = dataGridView1.SelectedRows[0];
                numericUpDown1.Value = Convert.ToDecimal(filaSeleccionada.Cells["id"].Value);
                numericUpDown1.ReadOnly = true;
                textBox2.Text = filaSeleccionada.Cells["zona"].Value.ToString();
                textBox1.Text = filaSeleccionada.Cells["distrito"].Value.ToString();
                numericUpDown2.Value = Convert.ToDecimal(filaSeleccionada.Cells["superficie"].Value);
                numericUpDown3.Value = Convert.ToDecimal(filaSeleccionada.Cells["xini"].Value);
                numericUpDown4.Value = Convert.ToDecimal(filaSeleccionada.Cells["yini"].Value);
                numericUpDown5.Value = Convert.ToDecimal(filaSeleccionada.Cells["xfin"].Value);
                numericUpDown6.Value = Convert.ToDecimal(filaSeleccionada.Cells["yfin"].Value);
                textBox3.Text = filaSeleccionada.Cells["ciDuenio"].Value.ToString();
                isEditing = true;
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un registro para editar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CargarCatastro();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow filaSeleccionada = dataGridView1.SelectedRows[0];
                int id = Convert.ToInt32(filaSeleccionada.Cells["id"].Value);
                DialogResult resultado = MessageBox.Show("¿Está seguro de que desea eliminar este registro?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (resultado == DialogResult.Yes)
                {
                    try
                    {
                        conexion.Open();
                        string deleteQuery = "DELETE FROM catastro WHERE id = @id";
                        SqlCommand deleteCmd = new SqlCommand(deleteQuery, conexion);
                        deleteCmd.Parameters.AddWithValue("@id", id);

                        int filasEliminadas = deleteCmd.ExecuteNonQuery();

                        if (filasEliminadas > 0)
                        {
                            MessageBox.Show("Registro eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarCatastro();
                        }
                        else
                        {
                            MessageBox.Show("No se encontró el registro para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                MessageBox.Show("Por favor, seleccione un registro para eliminar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (rol.Equals("admin"))
            {
                Form admin = new AdminIndex();
                admin.Show();
                this.Hide();
            }
            else
            {
                Form func = new FuncionarioIndex();
                func.Show();
                this.Hide();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {

                DataGridViewRow filaSeleccionada = dataGridView1.SelectedRows[0];
                string codCatastro = filaSeleccionada.Cells["id"].Value.ToString();

                string url = String.Format("http://localhost:58026/WebSite2/Default.aspx?rol={0}&codCatastro={1}", this.rol, codCatastro);
                System.Diagnostics.Process.Start(url);
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un registro para redirigir.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}

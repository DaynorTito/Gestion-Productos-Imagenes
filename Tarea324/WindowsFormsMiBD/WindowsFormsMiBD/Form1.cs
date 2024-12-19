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

namespace WindowsFormsMiBD
{
    public partial class Form1 : Form
    {
        SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-FQ2GHMQ;Initial Catalog=midb;Integrated Security=True");
      
        int iii = 0;
        public Form1()
        {
            InitializeComponent();
            LlenarTablaAlumno();
            iii = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(dataGridView1_CellEndEdit);

        }

        public void LlenarTablaAlumno()
        {
            String consulta ="SELECT * FROM ALUMNO";
            SqlDataAdapter adaptador = new SqlDataAdapter(consulta,conexion);
            DataTable dt = new DataTable();

            adaptador.Fill(dt);
            dataGridView1.DataSource=dt;
            iii = 0;
            vaciar();
        }

        public void LlenarTablaInscritos()
        {
            String consulta = "SELECT xi.id_alumno,xi.id_materia,xm.sigla,xa.ci ci_alumno,xi.nota1,xi.nota2,xi.nota3 FROM alumno xa,inscribe xi, materia xm WHERE xa.id_alumno=xi.id_alumno AND xi.id_materia=xm.id_materia;";
            SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            dataGridView1.DataSource = dt;
            iii = 2;
            vaciar();
        }
 

        public void LlenarTablaMateria()
        {
            String consulta = "SELECT * FROM MATERIA";
            SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            dataGridView1.DataSource = dt;
            iii = 1;
            vaciar();
        }

        public void vaciar() 
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox12.Clear();
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            conexion.Open();
            string consulta = "insert into alumno values ('"+textBox1.Text+"','"+textBox2.Text+"','"+textBox3.Text+"','"+textBox4.Text+"',"+textBox5.Text+");";
            SqlCommand comando = new SqlCommand(consulta,conexion);
            comando.ExecuteNonQuery();
            MessageBox.Show("Alumno Agregado corrrectamente");
            conexion.Close();
            LlenarTablaAlumno();
            vaciar();
            iii = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conexion.Open();
            String consulta = "delete from alumno where ci='"+textBox1.Text+"';";
            SqlCommand comando = new SqlCommand(consulta,conexion);
            int con =comando.ExecuteNonQuery();
            if (con > 1)
            {
                MessageBox.Show(" Alumno Eliminado Correctamente");
            }
            else {
                MessageBox.Show(" No se Elimino Ningun Alumno ");
            }
            
            LlenarTablaAlumno();
            vaciar();
            conexion.Close();
            iii = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conexion.Open();
            String consulta = "update alumno SET ci='"+textBox1.Text+"',nombre='"+textBox2.Text+"',paterno='"+textBox3.Text+"',materno='"+textBox4.Text+"',departamento='"+textBox5.Text+"'where ci='"+textBox1.Text+"';";
            SqlCommand comando = new SqlCommand(consulta,conexion);
            int con =comando.ExecuteNonQuery();
            if (con > 0)
            {
                MessageBox.Show("Alumno Modificado Correctamente");
            }
            else 
            {
                MessageBox.Show("No se encontro al Alumno");
            }
            conexion.Close();
            LlenarTablaAlumno();
            vaciar();
            iii = 0;
        }

        private void modifyAlu(String name, String pat, String mat, String dpto, String ci)
        {
            conexion.Open();
            String consulta = "update alumno SET nombre='" + name + "',paterno='" + pat + "',materno='" + mat + "',departamento='" + dpto + "'where ci='" + ci + "';";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            int con = comando.ExecuteNonQuery();
            if (con > 0)
            {
                MessageBox.Show("Alumno Modificado Correctamente");
            }
            else
            {
                MessageBox.Show("No se encontro al Alumno");
            }
            conexion.Close();
            LlenarTablaAlumno();
            vaciar();
            iii = 0;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (iii == 0) 
            {
                var cellValue1 = dataGridView1.Rows[e.RowIndex].Cells[1].Value;
                var cellValue2 = dataGridView1.Rows[e.RowIndex].Cells[2].Value;
                var cellValue3 = dataGridView1.Rows[e.RowIndex].Cells[3].Value;
                var cellValue4 = dataGridView1.Rows[e.RowIndex].Cells[4].Value;
                var cellValue5 = dataGridView1.Rows[e.RowIndex].Cells[5].Value;

                textBox1.Text = cellValue1 != null ? cellValue1.ToString() : "";
                textBox2.Text = cellValue2 != null ? cellValue2.ToString() : "";
                textBox3.Text = cellValue3 != null ? cellValue3.ToString() : "";
                textBox4.Text = cellValue4 != null ? cellValue4.ToString() : "";
                textBox5.Text = cellValue5 != null ? cellValue5.ToString() : "";
            }
            if (iii == 1) 
            {
                var cellValue7 = dataGridView1.Rows[e.RowIndex].Cells[1].Value;
                var cellValue8 = dataGridView1.Rows[e.RowIndex].Cells[2].Value;
                textBox7.Text = cellValue7 != null ? cellValue7.ToString() : "";
                textBox8.Text = cellValue8 != null ? cellValue8.ToString() : "";
            }
            if(iii==2){
                var cellValue6 = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                var cellValue12 = dataGridView1.Rows[e.RowIndex].Cells[1].Value;
                var cellValue11 = dataGridView1.Rows[e.RowIndex].Cells[4].Value;
                var cellValue10 = dataGridView1.Rows[e.RowIndex].Cells[5].Value;
                var cellValue9 = dataGridView1.Rows[e.RowIndex].Cells[6].Value;

                textBox6.Text = cellValue6 != null ? cellValue6.ToString() : "";
                textBox12.Text = cellValue12 != null ? cellValue12.ToString() : "";
                textBox11.Text = cellValue11 != null ? cellValue11.ToString() : "";
                textBox10.Text = cellValue10 != null ? cellValue10.ToString() : "";
                textBox9.Text = cellValue9 != null ? cellValue9.ToString() : "";

            }
            
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_BackColorChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            FormMateria mat = new FormMateria();
            mat.Show();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            conexion.Open();
            String consulta = "insert into materia values('"+textBox7.Text+"','"+textBox8.Text+"');";
            SqlCommand comando = new SqlCommand(consulta,conexion);
            comando.ExecuteNonQuery();
            MessageBox.Show("Materia Agregada");
            conexion.Close();
            iii = 1;
            LlenarTablaMateria();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            conexion.Open();
            String consulta = "delete from materia where sigla = '"+textBox7.Text+"';";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            int con =comando.ExecuteNonQuery();
            if (con >= 1)
            {
                MessageBox.Show("Materia Eliminada");
            }else {
                MessageBox.Show("Materia no Encontrada");
            }
            conexion.Close();
            iii = 1;
            LlenarTablaMateria();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            conexion.Open();
            String consulta = "update materia set sigla='"+textBox7.Text+"',descripcion='"+textBox8.Text+"' where sigla ='"+textBox7.Text+"';";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            int con = comando.ExecuteNonQuery();
            if (con >= 1)
            {
                MessageBox.Show("Materia Modificada correctamente");
            }
            else
            {
                MessageBox.Show("Materia no Encontrada");
            }
            conexion.Close();
            iii = 1;
            LlenarTablaMateria();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            LlenarTablaMateria();
            vaciar();
            iii = 1;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            LlenarTablaAlumno();
            vaciar();
            iii = 0;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            LlenarTablaInscritos();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            conexion.Open();
            String id_alumno = textBox6.Text;
            String id_materia = textBox12.Text;
            String nota1 = textBox11.Text;
            String nota2 = textBox10.Text;
            String nota3 = textBox9.Text;

            if (nota1.Length == 0)
            {
                nota1 = "0";
            }
            if (nota2.Length == 0)
            {
                nota2 = "0";
            }
            if (nota3.Length == 0)
            {
                nota3 = "0";
            }
            String consulta = "INSERT INTO inscribe (id_alumno, id_materia, nota1, nota2, nota3) VALUES (" + id_alumno + "," + id_materia + ", " + nota1 + ", " + nota2 + ", " + nota3 + ");";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            comando.ExecuteNonQuery();
            MessageBox.Show("Inscripcion Correcta");
            conexion.Close();
            iii = 2;
            LlenarTablaInscritos();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            conexion.Open();
            String consulta = "delete from inscribe where id_alumno="+textBox6.Text+" AND id_materia="+textBox12.Text+";";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            int con = comando.ExecuteNonQuery();
            if (con >= 1)
            {
                MessageBox.Show("Inscripcion Eliminada");
            }
            else
            {
                MessageBox.Show("Inscripcion no Encontrada");
            }
            conexion.Close();
            iii = 2;
            LlenarTablaInscritos();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            conexion.Open();
            String id_alumno = textBox6.Text;
            String id_materia = textBox12.Text;
            String nota1 = textBox11.Text;
            String nota2 = textBox10.Text;
            String nota3 = textBox9.Text;

            if (nota1.Length == 0)
            {
                nota1 = "0";
            }
            if (nota2.Length == 0)
            {
                nota2 = "0";
            }
            if (nota3.Length == 0)
            {
                nota3 = "0";
            }
            String consulta = "update inscribe set id_alumno="+id_alumno+",id_materia="+id_materia+",nota1="+nota1+",nota2="+nota2+",nota3="+nota3+" where id_alumno="+id_alumno+" AND id_materia="+id_materia+";";
            SqlCommand comando = new SqlCommand(consulta, conexion);
            comando.ExecuteNonQuery();
            MessageBox.Show("Inscripcion Correcta");
            conexion.Close();
            iii = 2;
            LlenarTablaInscritos();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            vaciar();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            vaciar();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            vaciar();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            String consulta = @"WITH Calculos AS (
                                    SELECT 
                                        a.departamento,
                                        (i.nota1 + i.nota2 + i.nota3)/3 AS suma_notas
                                    FROM alumno a
                                    JOIN inscribe i ON a.id_alumno = i.id_alumno
                                )
                                SELECT 
                                    [01] AS CHQ,
                                    [02] AS LPZ,
                                    [03] AS CBA,
                                    [04] AS ORU,
                                    [05] AS PTS
                                FROM (
                                    SELECT departamento, suma_notas
                                    FROM Calculos
                                ) AS SourceTable
                                PIVOT (
                                    AVG(suma_notas)
                                    FOR departamento IN ([01], [02], [03], [04], [05])
                                ) AS PivotTable;
                                ";
            SqlDataAdapter adaptador = new SqlDataAdapter(consulta, conexion);
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            dataGridView1.DataSource = dt;
            iii = 4;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["nombre"].Index || e.ColumnIndex == dataGridView1.Columns["paterno"].Index || e.ColumnIndex == dataGridView1.Columns["materno"].Index || e.ColumnIndex == dataGridView1.Columns["departamento"].Index)
            {
                string nueNom = dataGridView1.Rows[e.RowIndex].Cells["nombre"].Value.ToString();
                string pat = dataGridView1.Rows[e.RowIndex].Cells["paterno"].Value.ToString();
                string mat = dataGridView1.Rows[e.RowIndex].Cells["materno"].Value.ToString();
                string dpto = dataGridView1.Rows[e.RowIndex].Cells["departamento"].Value.ToString();
                string ci = dataGridView1.Rows[e.RowIndex].Cells["ci"].Value.ToString();
                modifyAlu(nueNom, pat, mat, dpto, ci);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
 
        }

        

        

        
    }
}

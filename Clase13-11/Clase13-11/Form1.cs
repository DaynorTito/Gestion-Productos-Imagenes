using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clase13_11
{
public partial class Form1 : Form
{
    private Bitmap originalImage;
    private int selectedR, selectedG, selectedB;
    private const int TOLERANCE = 80;
    private bool isProcessing = false;

    public Form1()
    {
        InitializeComponent();
        ConfigurarFormulario();
        InicializarComponentesAdicionales();
    }

    private void InicializarComponentesAdicionales()
    {
        openFileDialog1 = new OpenFileDialog();
        openFileDialog1.Filter = "Archivos de imagen|*.png;*.jpg;*.jpeg;*.bmp|Todos los archivos|*.*";
        openFileDialog1.Title = "Seleccionar Muestra Geológica";
    }

    private bool EsColorSimilar(Color color)
    {
        int umbralBrillo = 120;
        int umbralNegro = 50;

        if (color.R < umbralNegro && color.G < umbralNegro && color.B < umbralNegro)
        {
            return false;
        }

        bool esDorado = (color.R > 180 && color.G > 150 && color.B < 100 &&
                         (color.R + color.G + color.B) / 3 > umbralBrillo) ||
                        (color.R > 220 && color.G > 200 && color.B < 180 &&
                         (color.R + color.G + color.B) / 3 > umbralBrillo) ||
                        (color.R > 200 && color.G > 180 && color.B < 140 &&
                         (color.R + color.G + color.B) / 3 > umbralBrillo) ||
                        (color.R > 150 && color.G > 100 && color.B < 80 &&
                         (color.R + color.G + color.B) / 3 > umbralBrillo);

        bool esPlata = (color.R > 200 && color.G > 200 && color.B > 200 &&
                        (color.R + color.G + color.B) / 3 > umbralBrillo) ||
                       (color.R > 180 && color.G > 180 && color.B > 180 &&
                        (color.R + color.G + color.B) / 3 > umbralBrillo);

        bool esBrillante = (color.R > 200 && color.G < 100 && color.B < 100) ||
                           (color.R < 100 && color.G > 200 && color.B < 100) ||
                           (color.R < 100 && color.G < 100 && color.B > 200) ||
                           (color.R > 200 && color.G > 200 && color.B < 100) ||
                           (color.R > 200 && color.G > 200 && color.B > 0 &&
                            color.B < 100);

        bool esColorSeleccionado = Math.Abs(color.R - selectedR) <= TOLERANCE &&
                                   Math.Abs(color.G - selectedG) <= TOLERANCE &&
                                   Math.Abs(color.B - selectedB) <= TOLERANCE;

        return esDorado || esPlata || esBrillante || esColorSeleccionado;
    }
    
    private void ConfigurarFormulario()
    {
        button2.Enabled = false;
        this.Text = "Detector de Minerales";

        pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

        Label lblInstrucciones = new Label();
        lblInstrucciones.Text = "Haga clic en el mineral que desea detectar";
        lblInstrucciones.AutoSize = true;
        lblInstrucciones.Location = new Point(10, 10);
        this.Controls.Add(lblInstrucciones);
    }

    private void button1_Click(object sender, EventArgs e)
    {
        if (openFileDialog1.ShowDialog() == DialogResult.OK)
        {
            try
            {
                if (originalImage != null)
                {
                    originalImage.Dispose();
                }
                originalImage = new Bitmap(openFileDialog1.FileName);
                pictureBox1.Image = originalImage;
                button2.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar la imagen: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }

    private Color ObtenerColorPromedio(int x, int y, int radio)
    {
        if (originalImage == null) return Color.Black;

        int startX = Math.Max(0, x - radio);
        int startY = Math.Max(0, y - radio);
        int endX = Math.Min(originalImage.Width, x + radio);
        int endY = Math.Min(originalImage.Height, y + radio);

        int sumR = 0, sumG = 0, sumB = 0;
        int count = 0;

        for (int i = startX; i < endX; i++)
        {
            for (int j = startY; j < endY; j++)
            {
                Color pixel = originalImage.GetPixel(i, j);
                sumR += pixel.R;
                sumG += pixel.G;
                sumB += pixel.B;
                count++;
            }
        }

        if (count == 0) return Color.Black;

        return Color.FromArgb(sumR / count, sumG / count, sumB / count);
    }

    private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
    {
        if (originalImage == null || isProcessing) return;

        try
        {
            Point ubicacionClick = ConvertirCoordenadas(e.Location);

            Color colorMuestra = ObtenerColorPromedio(
                ubicacionClick.X,
                ubicacionClick.Y,
                5
            );

            selectedR = colorMuestra.R;
            selectedG = colorMuestra.G;
            selectedB = colorMuestra.B;

            textBox1.Text = selectedR.ToString();
            textBox2.Text = selectedG.ToString();
            textBox3.Text = selectedB.ToString();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error al muestrear el color: " + ex.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private Point ConvertirCoordenadas(Point ubicacionClick)
    {
        float ratioX = (float)originalImage.Width / pictureBox1.ClientSize.Width;
        float ratioY = (float)originalImage.Height / pictureBox1.ClientSize.Height;

        int imageX = (int)(ubicacionClick.X * ratioX);
        int imageY = (int)(ubicacionClick.Y * ratioY);

        return new Point(
            Math.Min(Math.Max(imageX, 0), originalImage.Width - 1),
            Math.Min(Math.Max(imageY, 0), originalImage.Height - 1)
        );
    }

    private void button2_Click(object sender, EventArgs e)
    {
        if (originalImage == null || isProcessing) return;

        isProcessing = true;
        Cursor = Cursors.WaitCursor;

        try
        {
            Bitmap resultImage = new Bitmap(originalImage.Width, originalImage.Height);

            for (int x = 0; x < originalImage.Width; x++)
            {
                for (int y = 0; y < originalImage.Height; y++)
                {
                    Color pixelColor = originalImage.GetPixel(x, y);

                    if (EsColorSimilar(pixelColor))
                    {
                        // Resaltamos en un color más visible las áreas detectadas
                        resultImage.SetPixel(x, y, Color.FromArgb(255, 0, 0)); // Rojo brillante
                    }
                    else
                    {
                        // Mantenemos el color original para el resto
                        resultImage.SetPixel(x, y, pixelColor);
                    }
                }
            }

            if (pictureBox2.Image != null)
            {
                pictureBox2.Image.Dispose();
            }
            pictureBox2.Image = resultImage;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error al procesar la imagen: " + ex.Message, "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            isProcessing = false;
            Cursor = Cursors.Default;
        }
    }


    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);
        if (originalImage != null)
            originalImage.Dispose();
        if (pictureBox1.Image != null)
            pictureBox1.Image.Dispose();
        if (pictureBox2.Image != null)
            pictureBox2.Image.Dispose();
    }

    // Añadir este método para resolver el error
    private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
    {
        // Este método puede estar vacío, solo está para satisfacer el Designer
    }
}
}


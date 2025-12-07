using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proyecto_final
{

    public partial class Form1 : Form
    {
        List<Estudiante> listaEstudiantes = new List<Estudiante>();
        int contadorID = 1;

        public Form1()
        {
            InitializeComponent();
          
        }
        

        public void txtNombre_TextChanged(object sender, EventArgs e)
        {

        }

        public void txtTelefono_TextChanged(object sender, EventArgs e)
        {

        }

        public void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public void DTFecha_ingreso_ValueChanged(object sender, EventArgs e)
        {

        }

        public void txtCarrera_TextChanged(object sender, EventArgs e)
        {

        }

        public void txtCuota_mensual_TextChanged(object sender, EventArgs e)
        {

        }

        public void txtDireccion_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void label2_Click(object sender, EventArgs e)
        {

        }

        public void label3_Click(object sender, EventArgs e)
        {

        }

        public void label4_Click(object sender, EventArgs e)
        {

        }

        public void label5_Click(object sender, EventArgs e)
        {

        }

        public void label6_Click(object sender, EventArgs e)
        {

        }

        //Clase Estudiante y Dbcontext
        public class Estudiante
        {
            public int ID { get; set; }
            public string Nombre { get; set; }
            public string Telefono { get; set; }
            public DateTime FechaIngreso { get; set; }
            public string Carrera { get; set; }
            public int Cuota { get; set; }
            public string Direccion { get; set; }
        }

        public class AppDbContext : DbContext
        {
            public DbSet<Estudiante> Estudiantes { get; set; }

            public AppDbContext() : base("ConexionSQL")
            {
            }
        }



        //Boton Registrar
        public void btnRegistrar_Click(object sender, EventArgs e)
        {
            try { 
            int cuota;

            if (!int.TryParse(txtCuota_mensual.Text, out cuota))
            {
                MessageBox.Show("La cuota mensual debe ser un valor numérico.", "Error");
                return;
            }

            using (var db = new AppDbContext())
            {
                Estudiante est = new Estudiante()
                {
                    Nombre = txtNombre.Text,
                    Telefono = txtTelefono.Text,
                    FechaIngreso = DTFecha_ingreso.Value,
                    Carrera = txtCarrera.Text,
                    Cuota = cuota,
                    Direccion = txtDireccion.Text
                };

                db.Estudiantes.Add(est);
                db.SaveChanges(); // GUARDA EN SQL
            }

            MessageBox.Show("Estudiante registrado.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

            txtNombre.Clear();
            txtTelefono.Clear();
            txtCarrera.Clear();
            txtCuota_mensual.Clear();
            txtDireccion.Clear();

            } catch (Exception ex)
            {
                MessageBox.Show("Error al Registrar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Form1_Load(object sender, EventArgs e) 
        {
           
        }



        public void estudiantesRegistradosToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        //Metodo para llamar al formulario 2
        public void mostrarEstudiantesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2(listaEstudiantes);
            frm.ShowDialog();
        }
    }
}

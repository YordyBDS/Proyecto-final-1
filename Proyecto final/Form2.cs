using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Proyecto_final.Form1;
using System.Data.Entity;

namespace Proyecto_final
{
    public partial class Form2 : Form
    {
        public List<Estudiante> estudiantes;

        string path = "C:\\Users\\yordy\\OneDrive\\Escritorio\\UFHEC\\Universidad trabajos\\Lenguaje de programacion I\\Proyecto final\\txt";


        public Form2(List<Estudiante> lista)
        {
            InitializeComponent();
            estudiantes_registrado.ColumnCount = 7;
            estudiantes_registrado.Columns[0].Name = "ID";
            estudiantes_registrado.Columns[1].Name = "Nombre completo";
            estudiantes_registrado.Columns[2].Name = "Telefono";
            estudiantes_registrado.Columns[3].Name = "Fecha de ingreso";
            estudiantes_registrado.Columns[4].Name = "Carrera";
            estudiantes_registrado.Columns[5].Name = "Cuota mensual";
            estudiantes_registrado.Columns[6].Name = "Direccion";
        }

        
        // metodo para agregar estudiante al datagridview
        public void AgregarEstudiante(
        int id,
        string nombre,
        string telefono,
        DateTime fecha_ingreso,
        string carrera,
        int cuota,
        string direccion)
        {
            estudiantes_registrado.Rows.Add(
                id,
                nombre,
                telefono,
                fecha_ingreso.ToShortDateString(),
                carrera,
                cuota,
                direccion
            );
        }


        // metodo cargar datos desde la base de datos al datagridview
        private void CargarDato()
        {
            try
            {
                using (var db = new AppDbContext())
                {
                    var lista = db.Estudiantes.ToList();

                    estudiantes_registrado.Rows.Clear();

                    foreach (var est in lista)
                    {
                        estudiantes_registrado.Rows.Add(
                            est.ID,
                            est.Nombre,
                            est.Telefono,
                            est.FechaIngreso.ToShortDateString(),
                            est.Carrera,
                            est.Cuota,
                            est.Direccion
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void Form2_Load(object sender, EventArgs e)
        {
            
        }

        public void estudiantes_registrado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

        }
        
        
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (estudiantes_registrado.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione una fila.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var row = estudiantes_registrado.SelectedRows[0];

                int id = Convert.ToInt32(row.Cells[0].Value);

                using (var db = new AppDbContext())
                {
                    var est = db.Estudiantes.FirstOrDefault(x => x.ID == id);

                    if (est != null)
                    {
                        est.Nombre = row.Cells[1].Value.ToString();
                        est.Telefono = row.Cells[2].Value.ToString();
                        est.FechaIngreso = DateTime.Parse(row.Cells[3].Value.ToString());
                        est.Carrera = row.Cells[4].Value.ToString();
                        est.Cuota = Convert.ToInt32(row.Cells[5].Value);
                        est.Direccion = row.Cells[6].Value.ToString();

                        db.SaveChanges();
                    }
                }

                MessageBox.Show("Registro actualizado.", "Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            CargarDato();
        }

        private void Eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (estudiantes_registrado.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Seleccione una fila.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int id = Convert.ToInt32(estudiantes_registrado.SelectedRows[0].Cells[0].Value);

                using (var db = new AppDbContext())
                {
                    var est = db.Estudiantes.FirstOrDefault(x => x.ID == id);

                    if (est != null)
                    {
                        db.Estudiantes.Remove(est);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            CargarDato();
        }

        private void exportar()
        {

        }
    }
}

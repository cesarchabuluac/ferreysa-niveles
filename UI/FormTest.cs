using FirebirdSql.Data.FirebirdClient;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Niveles.Infraestructura;

namespace Niveles.UI
{
    public partial class FormTest : FormBaseConSesion
    {
        public FormTest()
        {
            InitializeComponent();

           // this.Text = $"NIVELES - {Empresa.Nombre} ({Empresa.Alias}) - {Usuario.NombreCompleto}";
        }

        protected override void OnSesionInicializada()
        {
            //Sesión garantizada aquí
            this.Text = "Niveles - " + Empresa.Nombre + " (" + Empresa.Alias + ") - " + Usuario.NombreUsuario;
            //lblEmpresa.Text = "Empresa: " + Empresa.Nombre + " (" + Empresa.Alias + ")";
            //lblUsuario.Text = "Usuario: " + Usuario.NombreUsuario + " - " + Usuario.NombreCompleto;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = new FbConnection(AppSession.PeripheralConnectionString))
                {
                    conn.Open();
                    using (var cmd = new FbCommand("SELECT CURRENT_TIMESTAMP FROM RDB$DATABASE", conn))
                    {
                        var ts = (DateTime)cmd.ExecuteScalar();
                        MessageBox.Show("Conexión OK.\nAhora: " + ts.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexión: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

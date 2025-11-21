using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Niveles.Domain;
using Niveles.Infraestructura;

namespace Niveles
{
    public partial class FormAccess : Form
    {
        private List<Empresa> _empresas = new List<Empresa>();

        public FormAccess()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                // Si el foco está en el botón → ejecutar el click
                if (this.ActiveControl is Button btn)
                {
                    btn.PerformClick();
                    return true;
                }

                // Sino, avanzar al siguiente control como TAB
                this.SelectNextControl(this.ActiveControl, true, true, true, true);
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }


        private async void FormAccess_Load(object sender, EventArgs e)
        {
            try
            {
                var central = new ConexionCentral();
                _empresas = await central.GetEmpresasAsync();
                cboCompny.DataSource = _empresas;
                cboCompny.DisplayMember = "Nombre";
                cboCompny.ValueMember = "IdEmpresa";

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando empresas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private async void btnConectar_Click(object sender, EventArgs e)
        {
            var emp = cboCompny.SelectedItem as Empresa;
            var user = txtUserName.Text?.Trim() ?? "";
            var pass = txtPassword.Text ?? "";

            if (emp == null)
            {
                MessageBox.Show("Seleccione una empresa.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Usuario y contraseña son requeridos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1) Autenticar contra AspNetUsers en CENTRAL.FDB (Firebird)
                // Si tu tabla tiene NORMALIZEDUSERNAME usa true; si es Identity v2 clásico (no la tiene), usa false.
                var authAsp = new AuthServiceIdentityFirebird();
                var usuario = await authAsp.AuthenticateAsync(user, pass);
                if (usuario == null)
                {
                    MessageBox.Show("Credenciales inválidas o usuario bloqueado.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 2) Construir conexión a base periférica seleccionada
                var perConnStr = ConexionPerifericaFactory.Build(emp);

                // 3) Guardar sesión global
                AppSession.SetSession(emp, usuario, perConnStr);

                //Seteamos la Cadena de conexión en ClaseConn para compatibilidad con código existente
                ClaseConn.cadena = perConnStr;               


                // 4) Cerrar login
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("No fue posible autenticar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

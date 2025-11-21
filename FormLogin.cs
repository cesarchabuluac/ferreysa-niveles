using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Niveles.Clases;
namespace Niveles
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            
            if (ClaseUsuario.Validacion_Usuario(txt_login.Text.ToString()))
            {
                ClaseUsuario.Set_Vendedor_Nombre();
                this.Hide();
                Form1 forma = new Form1();
                forma.ShowDialog();
            }
            else
            {
                txt_login.Clear();
            }
        }

        private void txt_login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {               
                btn_login_Click(null, null);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ClaseUsuario.Validacion_Usuario(txt_login.Text.ToString()))
            {
                ClaseUsuario.Set_Vendedor_Nombre();
                this.Hide();
                FormNiveles formani = new FormNiveles();
                formani.ShowDialog();
            }
            else
            {
                txt_login.Clear();
            }
        }
    }
}

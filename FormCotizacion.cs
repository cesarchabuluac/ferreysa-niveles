using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Vales.Clases;
using Microsoft.Reporting.WinForms;



namespace Vales
{
    public partial class FormCotizacion : Form
    {
        public FormCotizacion()
        {
            InitializeComponent();
        }

        private void btn_buscar_Click(object sender, EventArgs e)
        {
            dgv_busca_clientes.Hide();

            dgv_busca_arts.Rows.Clear();

            dgv_busca_arts.Show();

            if (ClaseCotizacion.Buscar_Arts(txt_nombre_art.Text) != null)
            {

                foreach (DataRow row in ClaseCotizacion.Buscar_Arts(txt_nombre_art.Text).Rows)
                {
                    dgv_busca_arts.Rows.Add(row["nombre"], row["ud"], row["articulo_id"]);
                }

                dgv_busca_arts.Focus();

            }
            else
            {
                MessageBox.Show("NO HAY ARTICULOS");
                dgv_busca_arts.Hide();
                txt_nombre_art.Clear();
            }
                     

        }       

        private void FormCotizacion_Load(object sender, EventArgs e)
        {
            dgv_busca_arts.Hide();
            dgv_busca_clientes.Hide();
        }

        private void txt_nombre_art_KeyDown(object sender, KeyEventArgs e)
        {           
            dgv_busca_clientes.Hide();
            
            if(e.KeyCode == Keys.Enter)
            {
                btn_buscar_Click(null, null);
            }
        }

        private void dgv_busca_arts_KeyDown(object sender, KeyEventArgs e)
        {
           
            if(e.KeyCode == Keys.Enter)
            {
                foreach(DataGridViewRow row in dgv_busca_arts.SelectedRows)
                {
                    dgv_articulos_cotizar.Rows.Add(row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value);
                   
                    dgv_busca_arts.Hide();
                    
                    txt_nombre_art.Clear();
                }

            }
        }

        private void dgv_articulos_cotizar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txt_nombre_cliente_KeyDown(object sender, KeyEventArgs e)
        {
            dgv_busca_arts.Hide();

            if (e.KeyCode == Keys.Enter)
            {
                dgv_busca_clientes.Rows.Clear();
                dgv_busca_clientes.Show();


                if (ClaseCotizacion.Buscar_Clientes(txt_nombre_cliente.Text) != null)
                {
                    foreach (DataRow row in ClaseCotizacion.Buscar_Clientes(txt_nombre_cliente.Text).Rows)
                    {
                        dgv_busca_clientes.Rows.Add(row["nombre"], row["cliente_id"]);
                    }

                    dgv_busca_clientes.Focus();

                }
                else
                {
                    MessageBox.Show("NO HAY CLIENTES");
                    dgv_busca_clientes.Rows.Clear();
                    dgv_busca_clientes.Hide();
                }
            }
        }
    }
}

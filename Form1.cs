using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Vales.Clases;

namespace Vales
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //constructor, solo se hace una vez incluso si abres mas veces la forma, mas rapido            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //form load cada que se abre la forma al parecer es mas lento segun stackoverflow

            //Timer tmr = new Timer();
            //tmr.Tick += delegate 
            //{
            //    this.Close();
            //};
            //tmr.Interval = (int)TimeSpan.FromMinutes(1).TotalMilliseconds;
            //tmr.Start();
            
            lbl_vendedor_nombre.Text = ClaseUsuario.vendedor_nombre.ToString();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            //tenia Environment.Exit(1); pero marca error cual es la differencia?

        }

        private void btn_nuevo_vale_Click(object sender, EventArgs e)
        {
            if (ClaseVale.Validar_Factura(txt_factura.Text))
            {
                ClaseVale.Set_Datos_Factura();
                FormValeNuevoDF forma_valenuevo_df = new FormValeNuevoDF();
                forma_valenuevo_df.ShowDialog();
                txt_factura.Clear();
            }
        }

        private void btn_afectar_vale_Click(object sender, EventArgs e)
        {

        }

        private void btn_nueva_cot_Click(object sender, EventArgs e)
        {
            FormCotizacion nueva_cot = new FormCotizacion();
            nueva_cot.Show();

        }

     
    }
}

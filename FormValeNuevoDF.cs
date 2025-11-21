using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Niveles.Clases;
using Microsoft.Reporting.WinForms;

namespace Niveles
{
    public partial class FormValeNuevoDF : Form
    {
        public FormValeNuevoDF()
        {
            InitializeComponent();
        }

        private void FormValeNuevoDF_Load(object sender, EventArgs e)
        {
            txt_folio_factura.Text = ClaseVale.folio_factura;
            txt_cliente.Text = ClaseVale.nombre_cliente;
            txt_direccion.Text = ClaseVale.calle + " " + ClaseVale.num_exterior + " \r\n\" " + ClaseVale.colonia;
            txt_fecha.Text = ClaseVale.fecha_fact;
            txt_telefono.Text = ClaseVale.telefono1;
            txt_devolucion.Text = ClaseVale.folio_devolucion;
            txt_vendedor.Text = ClaseVale.vendedor;
            txt_importe.Text = ClaseVale.importe;

            cbo_cobradores.DataSource = ClaseVale.Cobradores();
            cbo_cobradores.ValueMember = "cobrador_id";
            cbo_cobradores.DisplayMember = "nombre";
            
            foreach(DataRow row in ClaseVale.Articulos_Factura().Rows)
            {
                dgv_articulos.Rows.Add(row["Articulo"], row["Um"], row["UmTotal"], "", row["Entregado"], row["Pendiente"], row["articulo_id"], row["docto_pv_det_id"]);
            }
        }

        private void btc_vale_Click(object sender, EventArgs e)
        {
            FormPrintPreview form = new FormPrintPreview();

            ReportParameter[] p = new ReportParameter[]
            {
                new ReportParameter("foliofact","parameter1asdf"),
                new ReportParameter("doctopvid","46056596"),

            };

            form.reportViewer1.LocalReport.SetParameters(p);


            DataTable dt = new DataTable();
            dt.Columns.Add("folio", typeof(string));
            dt.Columns.Add("nombre", typeof(string));

            dt.Rows.Add("prueba", "asdf");
            dt.Rows.Add("prueba2");


            ReportDataSource dataSource = new ReportDataSource("DataSet1", dt);

            form.reportViewer1.LocalReport.DataSources.Clear();
            form.reportViewer1.LocalReport.DataSources.Add(dataSource);
            form.reportViewer1.LocalReport.Refresh();

            form.ShowDialog();
        }
    }
}

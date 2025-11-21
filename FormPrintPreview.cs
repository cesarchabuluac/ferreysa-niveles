using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;


namespace Vales
{
    public partial class FormPrintPreview : Form 
    {
        public FormPrintPreview()
        {
            InitializeComponent();
        }

        public void FormPrintPreview_Load(object sender, EventArgs e) //era private
        {

            //ReportParameter[] p = new ReportParameter[]
            //{
            //    new ReportParameter("pfolio","parameter1"),
            //    new ReportParameter("pfolio", "parameter2")
            //};

            //this.reportViewer1.LocalReport.SetParameters(p);


            //DataTable dt = new DataTable();
            //dt.Columns.Add("folio", typeof(string));
            //dt.Columns.Add("nombre", typeof(string));

            //dt.Rows.Add("prueba","asdf");
            //dt.Rows.Add("prueba2");


            //ReportDataSource dataSource = new ReportDataSource("DataSet1", dt);

            //reportViewer1.LocalReport.DataSources.Clear();
            //reportViewer1.LocalReport.DataSources.Add(dataSource);
            //reportViewer1.LocalReport.Refresh();

            this.reportViewer1.RefreshReport();
        }

        public void reportViewer1_Load(object sender, EventArgs e) //era private
        {

        }
    }
}

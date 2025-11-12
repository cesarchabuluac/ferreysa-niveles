using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FirebirdSql.Data.FirebirdClient;

namespace Vales
{
    public partial class FormNiveles : Form
    {

        int i = 0;

        public FormNiveles()
        {
            InitializeComponent();
            
        }

        private void FormNiveles_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit(); 
        }

        private void LIMPIA()
        {
            dgv_exis.Rows.Clear();
            dgv_arts.Rows.Clear();

            label1.Text = "Sobreinventario: ";
            label2.Text = "Criticos: ";
            label3.Text = "Optimos: ";
            label4.Text = "Pedir: ";
            tbx_sobreinv.Text = "0";
            tbx_subinv.Text = "0";
            tbx_normal.Text = "0";
            tbx_pedir.Text = "0";
            tbx_art.Text = "";
        }

        private void Llenar_DGV(string almacen, string grupo)
        {
            dgv_arts.Rows.Clear();
            int sobreinv = 0;
            int subinv = 0;
            int normal = 0;
            int pedir = 0;
            int sinreord = 0;
            int suma = 0;


            if (!string.IsNullOrEmpty(almacen)) //todo este pedo esta mal no ocupas pasar parametro y checar
            {
                almacen = cbo_almacenes.SelectedValue.ToString();


                if (string.IsNullOrEmpty(grupo))
                {
                    
                    grupo = "";                                           
                }
                else
                {
                     grupo = $" and d.valor_clasif_id = {grupo} ";
                }

                

                string almacenes = "select A.ARTICULO_ID, e.clave_articulo, a.nombre, a.unidad_compra, " +

                    $"(SELECT EXISTENCIA FROM GET_EXIS_ART_SUCURSAL({almacen}, A.ARTICULO_ID, 0)) AS EXIS_MAT, " +
                        "G.INVENTARIO_MAXIMO AS MAX_MAT, G.PUNTO_REORDEN AS REORD_MAT, G.INVENTARIO_MINIMO AS MIN_MAT, " +

                    "r.valor, f.reorden_automatico, w.clasificador_id " +

                    "from elementos_cat_clasif d " +
                    "left join articulos a on d.elemento_id = a.articulo_id " +
                    "left join claves_articulos e on a.articulo_id = e.articulo_id and e.rol_clave_art_id = 17 " +
                    "left join libres_articulos f on a.articulo_id = f.articulo_id " +
                    
                    
                    "left join clasificadores_cat_valores r on d.valor_clasif_id = r.valor_clasif_id " +
                    $"LEFT JOIN NIVELES_ARTICULOS G ON A.ARTICULO_ID = G.ARTICULO_ID AND G.almacen_id = {almacen} " +
                    "left join clasificadores_cat w on r.clasificador_id = w.clasificador_id " +
                    $"where w.clasificador_id = {cbo_clasificaciones.SelectedValue} {grupo}";
               

                FbDataAdapter ada = new FbDataAdapter(almacenes, ClaseConn.cadena);
                DataTable dtt = new DataTable();
                ada.Fill(dtt);

                if(dtt.Rows.Count > 0)
                {
                    foreach (DataRow fila in dtt.Rows)
                    {
                        dgv_arts.Rows.Add(fila["ARTICULO_ID"], fila["clave_articulo"], fila["nombre"], fila["unidad_compra"],
                            fila["valor"], string.IsNullOrEmpty(fila["EXIS_MAT"].ToString())? 0 : fila["EXIS_MAT"],
                            string.IsNullOrEmpty(fila["MIN_MAT"].ToString()) ? 0 : fila["MIN_MAT"], 
                            string.IsNullOrEmpty(fila["REORD_MAT"].ToString()) ? 0 : fila["REORD_MAT"],
                            string.IsNullOrEmpty(fila["MAX_MAT"].ToString()) ? 0 : fila["MAX_MAT"],
                            "accion", fila["reorden_automatico"]);                        
                    }

                    foreach(DataGridViewRow dr in dgv_arts.Rows)
                    {
                        if(dr.Cells[10].Value!=null && dr.Cells[10].Value.ToString() == "N")
                        {
                            dr.DefaultCellStyle.BackColor = Color.LightCyan;
                            dr.Cells[9].Value = "No tiene R.A.";
                            sinreord++;
                        }
                        else
                        {
                            if(Convert.ToDouble( dr.Cells[5].Value) > Convert.ToDouble(dr.Cells[8].Value))
                            {
                                dr.DefaultCellStyle.BackColor = Color.Pink;
                                dr.Cells[9].Value = "SOBREINVENTARIO";
                                sobreinv++;
                            }
                            else
                            {
                                if (Convert.ToDouble(dr.Cells[5].Value) <= Convert.ToDouble(dr.Cells[6].Value))
                                    
                                    
                                {
                                    dr.DefaultCellStyle.BackColor = Color.LightCoral;
                                    dr.Cells[9].Value = "CRITICO";
                                    subinv++;
                                }
                                else
                                {
                                    if (Convert.ToDouble(dr.Cells[5].Value) <= Convert.ToDouble(dr.Cells[8].Value) &&
                                        Convert.ToDouble(dr.Cells[5].Value) > Convert.ToDouble(dr.Cells[7].Value))
                                    {
                                        dr.DefaultCellStyle.BackColor = Color.LightGreen;
                                        dr.Cells[9].Value = "OPTIMO";
                                        normal++;
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(dr.Cells[5].Value) <= Convert.ToDouble(dr.Cells[7].Value) &&
                                            Convert.ToDouble(dr.Cells[5].Value) > Convert.ToDouble(dr.Cells[6].Value))
                                        {
                                            dr.DefaultCellStyle.BackColor = Color.Yellow;
                                            dr.Cells[9].Value = "PEDIR";
                                            pedir++;
                                        }
                                    }

                                }

                            }

                            

                        }

                        
                    }

                   

                }
                else
                {
                    MessageBox.Show("no hay arts");
                }

            }
            else
            {
                MessageBox.Show("debe haber almacen seleccionado");
            }


            tbx_sobreinv.Text = sobreinv.ToString();
            tbx_subinv.Text = subinv.ToString();
            tbx_normal.Text = normal.ToString();
            tbx_pedir.Text = pedir.ToString();
            tbx_sinreord.Text = sinreord.ToString();
            
            suma = sobreinv + subinv + normal + pedir + sinreord;
            
            if(suma != 0)
            {
                label1.Text = "Sobreinventario: " + (sobreinv * 100.0 / suma).ToString("F0") + "%";
                label2.Text = "Criticos: " + (subinv * 100.0 / suma).ToString("F0") + "%";
                label3.Text = "Optimos: " + (normal * 100.0 / suma).ToString("F0") + "%";
                label4.Text = "Pedir: " + (pedir * 100.0 / suma).ToString("F0") + "%";
                label5.Text = "Sin Reorden Autom: " + (sinreord * 100.0 / suma).ToString("F0") + "%";
            }
            else
            {
                label1.Text = "Sobreinventario: ";
                label2.Text = "Criticos: " ;
                label3.Text = "Optimos: " ;
                label4.Text = "Pedir: " ;
                label5.Text = "Sin Reorden Autom: ";
            }
            

        }

        private void FormNiveles_Load(object sender, EventArgs e)
        {
            //string grupos = "select a.valor_clasif_id, a.valor from clasificadores_cat_valores a where a.clasificador_id = 24934362";
            //FbDataAdapter adapter = new FbDataAdapter(grupos, ClaseConn.cadena);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);

            //cbo_grupos.DataSource = dt;
            //cbo_grupos.DisplayMember = "valor";
            //cbo_grupos.ValueMember = "valor_clasif_id";
            //cbo_grupos.SelectedIndex = 0;


            string grupos = "select a.clasificador_id, a.nombre from clasificadores_cat a";
            FbDataAdapter adapter = new FbDataAdapter(grupos, ClaseConn.cadena);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            cbo_clasificaciones.DataSource = dt;
            cbo_clasificaciones.DisplayMember = "nombre";
            cbo_clasificaciones.ValueMember = "clasificador_id";
            cbo_clasificaciones.SelectedIndex = -1;



            string almacenes = "select a.almacen_id, a.nombre from almacenes a ";
            FbDataAdapter ada = new FbDataAdapter(almacenes, ClaseConn.cadena);
            DataTable dtt = new DataTable();
            ada.Fill(dtt);

            cbo_almacenes.DataSource = dtt;
            cbo_almacenes.DisplayMember = "nombre";
            cbo_almacenes.ValueMember = "almacen_id";
            cbo_almacenes.SelectedIndex = 0;

            i++;

        }

        private void btn_general_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(cbo_grupos.SelectedValue.ToString());

            Llenar_DGV(cbo_almacenes.SelectedValue.ToString(), "");
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Llenar_DGV(cbo_almacenes.SelectedValue.ToString(), cbo_grupos.SelectedValue.ToString());
        }

        private void btn_ocultar_sobreinv_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> rowsToRemove = new List<DataGridViewRow>();

            foreach (DataGridViewRow rr in dgv_arts.Rows)
            {
                if (rr.DefaultCellStyle.BackColor == Color.Pink)
                {
                    rowsToRemove.Add(rr);
                }
            }

            foreach (DataGridViewRow rr in rowsToRemove)
            {
                dgv_arts.Rows.Remove(rr);
            }
        }

        private void tb_ocultar_subinv_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> rowsToRemove = new List<DataGridViewRow>();

            foreach (DataGridViewRow rr in dgv_arts.Rows)
            {
                if (rr.DefaultCellStyle.BackColor == Color.LightCoral)
                {
                    rowsToRemove.Add(rr);
                }
            }

            foreach (DataGridViewRow rr in rowsToRemove)
            {
                dgv_arts.Rows.Remove(rr);
            }
        }

        private void btn_ocultar_normal_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> rowsToRemove = new List<DataGridViewRow>();

            foreach (DataGridViewRow rr in dgv_arts.Rows)
            {
                if (rr.DefaultCellStyle.BackColor == Color.LightGreen)
                {
                    rowsToRemove.Add(rr);
                }
            }

            foreach (DataGridViewRow rr in rowsToRemove)
            {
                dgv_arts.Rows.Remove(rr);
            }
        }

        private void btn_ocultar_pedir_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> rowsToRemove = new List<DataGridViewRow>();

            foreach (DataGridViewRow rr in dgv_arts.Rows)
            {
                if (rr.DefaultCellStyle.BackColor == Color.Yellow)
                {
                    rowsToRemove.Add(rr);
                }
            }

            foreach (DataGridViewRow rr in rowsToRemove)
            {
                dgv_arts.Rows.Remove(rr);
            }
        }

        private void btn_copiar_portapapeles_Click(object sender, EventArgs e)
        {
            if (dgv_arts.Rows.Count > 0)
            {
                StringBuilder ClipboardBuilder = new StringBuilder();

                // Add the column headers to the clipboard
                foreach (DataGridViewColumn column in dgv_arts.Columns)
                {
                    ClipboardBuilder.Append(column.HeaderText + "\t");
                }
                ClipboardBuilder.AppendLine();

                // Add the data rows to the clipboard
                foreach (DataGridViewRow row in dgv_arts.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value == null || cell.Value == DBNull.Value || string.IsNullOrWhiteSpace(cell.Value.ToString()))
                        {
                            ClipboardBuilder.Append("\t");
                        }
                        else
                        {
                            ClipboardBuilder.Append(cell.Value.ToString() + "\t");
                        }
                    }
                    ClipboardBuilder.AppendLine();
                }

                Clipboard.SetText(ClipboardBuilder.ToString());

                MessageBox.Show("EXITO");

            }
            else
            {
                MessageBox.Show("NO HAY NADA QUE COPIAR AL PORTAPELES");
            }
        }

        private void dgv_arts_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            dgv_exis.Rows.Clear();

            if (!string.IsNullOrEmpty(dgv_arts.Rows[e.RowIndex].Cells[0].Value.ToString()))

            {
                string almacenes = $"select {dgv_arts.Rows[e.RowIndex].Cells[0].Value} artid, b.almacen_id, b.nombre from almacenes b";

                FbDataAdapter adapter = new FbDataAdapter(almacenes, ClaseConn.cadena);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                foreach(DataRow row in dataTable.Rows)
                {
                    dgv_exis.Rows.Add(row["artid"], row["almacen_id"], row["nombre"] );
                }
              
                foreach(DataGridViewRow row in dgv_exis.Rows)
                {
                    string niveles = $"select A.ARTICULO_ID, "+
                                       $"(select EXISTENCIA " +
                                       $"from GET_EXIS_ART_SUCURSAL({row.Cells[1].Value}, {dgv_arts.Rows[e.RowIndex].Cells[0].Value}, 0)) " +
                                       $"as EXIS_MAT, "+
                                        "G.INVENTARIO_MAXIMO as MAX_MAT,  G.PUNTO_REORDEN as REORD_MAT, " +
                                        "G.INVENTARIO_MINIMO as MIN_MAT,  f.reorden_automatico " +
                                "from ARTICULOS A "+
                                $"LEFT JOIN NIVELES_ARTICULOS G ON A.ARTICULO_ID = G.ARTICULO_ID and g.almacen_id = {row.Cells[1].Value}" +
                                $"left join libres_articulos f on a.articulo_id = f.articulo_id " +                              
                                $"where A.ARTICULO_ID = {dgv_arts.Rows[e.RowIndex].Cells[0].Value}";
                        

                    FbDataAdapter adapter2 = new FbDataAdapter(niveles, ClaseConn.cadena);
                    DataTable dataTable2 = new DataTable();
                    adapter2.Fill(dataTable2);
                    if (dataTable2.Rows.Count == 1)
                    {
                        row.Cells[3].Value = dataTable2.Rows[0]["EXIS_MAT"].ToString();
                        row.Cells[4].Value = dataTable2.Rows[0]["MIN_MAT"].ToString();
                        row.Cells[5].Value = dataTable2.Rows[0]["REORD_MAT"].ToString();
                        row.Cells[6].Value = dataTable2.Rows[0]["MAX_MAT"].ToString();
                        row.Cells[7].Value = dataTable2.Rows[0]["reorden_automatico"].ToString();
                    }
                    else
                    {
                        if (dataTable2.Rows.Count > 1)
                        {
                            MessageBox.Show("ERROR REPORTAR POR FAVOR CLAVE 5582");
                        }
                        else
                        {
                            row.Cells[3].Value = dataTable2.Rows[0]["EXIS_MAT"].ToString();
                            row.Cells[4].Value = "N.A.";
                            row.Cells[5].Value = "N.A.";
                            row.Cells[6].Value = "N.A.";
                        }
                            
                    }

                    //ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff

                    //if (row.Cells[3].Value != null && row.Cells[4].Value != null && row.Cells[5].Value != null && row.Cells[6].Value != null && row.Cells[7].Value != null)
                    if(!string.IsNullOrEmpty(row.Cells[3].Value.ToString()) && 
                            !string.IsNullOrEmpty(row.Cells[4].Value.ToString()) && 
                            !string.IsNullOrEmpty(row.Cells[5].Value.ToString()) && 
                            !string.IsNullOrEmpty(row.Cells[6].Value.ToString()) && 
                            !string.IsNullOrEmpty(row.Cells[7].Value.ToString()))
                    {


                        //VER ANOTACION DE COLORES EN METODO DE LLENAR DGV!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        if (row.Cells[7].Value != null && row.Cells[7].Value.ToString() == "N")
                        {
                            row.DefaultCellStyle.BackColor = Color.LightCyan;

                        }
                        else
                        {
                            if (Convert.ToDouble(row.Cells[3].Value) >  Convert.ToDouble(row.Cells[6].Value))
                            {
                                row.DefaultCellStyle.BackColor = Color.Pink;

                            }
                            else
                            {
                                if (Convert.ToDouble(row.Cells[3].Value) <= Convert.ToDouble(row.Cells[4].Value))
                                {
                                    row.DefaultCellStyle.BackColor = Color.LightCoral;

                                }
                                else
                                {
                                    if (Convert.ToDouble(row.Cells[3].Value) <= Convert.ToDouble(row.Cells[6].Value) &&
                                        Convert.ToDouble(row.Cells[3].Value) > Convert.ToDouble(row.Cells[5].Value))
                                    {
                                        row.DefaultCellStyle.BackColor = Color.LightGreen;

                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(row.Cells[3].Value) <= Convert.ToDouble(row.Cells[5].Value) &&
                                            Convert.ToDouble(row.Cells[3].Value) > Convert.ToDouble(row.Cells[4].Value))
                                        {
                                            row.DefaultCellStyle.BackColor = Color.Yellow;

                                        }
                                    }

                                }

                            }


                        }
                    }
                    

                }

                tbx_art.Text = $"Informacion de: {dgv_arts.Rows[e.RowIndex].Cells[2].Value}";

            }
        }

        private void cbo_grupos_SelectedIndexChanged(object sender, EventArgs e)
        {
            LIMPIA();


        }

        private void cbo_almacenes_SelectedIndexChanged(object sender, EventArgs e)
        {
            LIMPIA();
        }

        private void btn_ocultar_sin_reord_Click(object sender, EventArgs e)
        {
            List<DataGridViewRow> rowsToRemove = new List<DataGridViewRow>();

            foreach (DataGridViewRow rr in dgv_arts.Rows)
            {
                if (rr.DefaultCellStyle.BackColor == Color.LightCyan)
                {
                    rowsToRemove.Add(rr);
                }
            }

            foreach (DataGridViewRow rr in rowsToRemove)
            {
                dgv_arts.Rows.Remove(rr);
            }
        }

        private void dgv_exis_SelectionChanged(object sender, EventArgs e)
        {
            dgv_exis.ClearSelection();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LIMPIA();

            if (i > 0)
            {
                string grupos = $"select a.valor_clasif_id, a.valor from clasificadores_cat_valores a where a.clasificador_id = {cbo_clasificaciones.SelectedValue}";
                FbDataAdapter adapter = new FbDataAdapter(grupos, ClaseConn.cadena);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                cbo_grupos.DataSource = dt;
                cbo_grupos.DisplayMember = "valor";
                cbo_grupos.ValueMember = "valor_clasif_id";
                cbo_grupos.SelectedIndex = 0;
            }

            

        }

        private void dgv_arts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex != -1)
            {
                FormVerArt ver_art = new FormVerArt();
                ver_art.id_art = Convert.ToInt32(dgv_arts.Rows[e.RowIndex].Cells[0].Value);
                ver_art.id_almacen = Convert.ToInt32(cbo_almacenes.SelectedValue.ToString());
                ver_art.id_almacen_orden = Convert.ToInt32(cbo_almacenes.SelectedIndex.ToString());
                ver_art.ShowDialog();
            }
        }

        private void dgv_arts_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex != -1)
            //{
            //    FormVerArt ver_art = new FormVerArt();
            //    ver_art.id_art = Convert.ToInt32(dgv_arts.Rows[e.RowIndex].Cells[0].Value);
            //    ver_art.id_almacen = Convert.ToInt32(cbo_almacenes.SelectedValue.ToString());
            //    ver_art.id_almacen_orden = Convert.ToInt32(cbo_almacenes.SelectedIndex.ToString());
            //    ver_art.ShowDialog();
            //}
        }
    }
}

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
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Vales.Helpers;
using Vales.Infraestructura;
using Vales.UI;

namespace Vales
{
    public partial class FormNiveles : FormBaseConSesion
    {
        private readonly RepositoryBase _repository = new RepositoryBase();

        private bool panelExpandido = false;
        private int anchoMinimoPanel2 = 200; // el mínimo para mostrar el botón

        int i = 0;

        public FormNiveles()
        {
            InitializeComponent();
            this.Text = $"NIVELES - {Empresa.Nombre} ({Empresa.Alias}) - {Usuario.NombreUsuario}";
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

        #region OLD_LLENAR_DGV_REFACTORIZADO
        private void Llenar_DGV(string almacen, string grupo)
        {
            dgv_arts.SuspendLayout();
            try
            {
                dgv_arts.Rows.Clear();

                // 1) Validaciones & capturar valores de UI una sola vez
                if (cbo_almacenes.SelectedValue == null)
                {
                    MessageBox.Show("Debe haber almacén seleccionado");
                    return;
                }
                // Usar el seleccionado real (mantiene tu comportamiento actual)
                var almacenId = cbo_almacenes.SelectedValue.ToString();

                if (cbo_clasificaciones.SelectedValue == null)
                {
                    MessageBox.Show("Seleccione una clasificación.");
                    return;
                }
                var clasifId = cbo_clasificaciones.SelectedValue.ToString();

                int? grupoId = null;
                if (!string.IsNullOrWhiteSpace(grupo))
                {
                    int gtmp;
                    if (int.TryParse(grupo, out gtmp)) grupoId = gtmp;
                }

                // 2) SQL PARAMETRIZADA (idéntica lógica que tu query original)
                var sql =
                    "SELECT A.ARTICULO_ID, e.clave_articulo AS clave_articulo, a.nombre, a.unidad_compra, " +
                    "(SELECT EXISTENCIA FROM GET_EXIS_ART_SUCURSAL(@alm, A.ARTICULO_ID, 0)) AS EXIS_MAT, " +
                    "G.INVENTARIO_MAXIMO AS MAX_MAT, G.PUNTO_REORDEN AS REORD_MAT, G.INVENTARIO_MINIMO AS MIN_MAT, " +
                    "r.valor, f.reorden_automatico, w.clasificador_id " +
                    "FROM elementos_cat_clasif d " +
                    "LEFT JOIN articulos a ON d.elemento_id = a.articulo_id " +
                    "LEFT JOIN claves_articulos e ON a.articulo_id = e.articulo_id AND e.rol_clave_art_id = 17 " +
                    "LEFT JOIN libres_articulos f ON a.articulo_id = f.articulo_id " +
                    "LEFT JOIN clasificadores_cat_valores r ON d.valor_clasif_id = r.valor_clasif_id " +
                    "LEFT JOIN NIVELES_ARTICULOS G ON A.ARTICULO_ID = G.ARTICULO_ID AND G.almacen_id = @alm " +
                    "LEFT JOIN clasificadores_cat w ON r.clasificador_id = w.clasificador_id " +
                    "WHERE w.clasificador_id = @clasif " +
                    (grupoId.HasValue ? "AND d.valor_clasif_id = @grupo " : "");

                var dt = new DataTable();
                using (var conn = new FbConnection(AppSession.PeripheralConnectionString))
                using (var cmd = new FbCommand(sql, conn))
                using (var da = new FbDataAdapter(cmd))
                {
                    cmd.Parameters.Add(new FbParameter("@alm", FbDbType.Integer) { Value = int.Parse(almacenId) });
                    cmd.Parameters.Add(new FbParameter("@clasif", FbDbType.Integer) { Value = int.Parse(clasifId) });
                    if (grupoId.HasValue)
                        cmd.Parameters.Add(new FbParameter("@grupo", FbDbType.Integer) { Value = grupoId.Value });

                    da.Fill(dt);
                }

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("No hay artículos");
                    // Limpia totales y etiquetas
                    tbx_sobreinv.Text = "0";
                    tbx_subinv.Text = "0";
                    tbx_normal.Text = "0";
                    tbx_pedir.Text = "0";
                    tbx_sinreord.Text = "0";
                    label1.Text = "Sobreinventario: ";
                    label2.Text = "Criticos: ";
                    label3.Text = "Optimos: ";
                    label4.Text = "Pedir: ";
                    label5.Text = "Sin Reorden Autom: ";
                    return;
                }

                // 3) Llenado del grid: cachea índices y parsea una sola vez
                const int IDX_EXIS = 5;
                const int IDX_MIN = 6;
                const int IDX_REOR = 7;
                const int IDX_MAX = 8;
                const int IDX_MSG = 9;
                const int IDX_RA = 10;

                int sobreinv = 0, subinv = 0, normal = 0, pedir = 0, sinreord = 0;

                foreach (DataRow r in dt.Rows)
                {
                    // Obtén números de forma segura
                    double exis = Global.ToDouble(r["EXIS_MAT"]);
                    double min = Global.ToDouble(r["MIN_MAT"]);
                    double reor = Global.ToDouble(r["REORD_MAT"]);
                    double max = Global.ToDouble(r["MAX_MAT"]);
                    string ra = Convert.ToString(r["reorden_automatico"]); // puede ser "N" u otro

                    // Agrega fila
                    var clave = r["clave_articulo"]?.ToString() ?? "";
                    int rowIndex = dgv_arts.Rows.Add(
                        r["ARTICULO_ID"], clave, r["nombre"], r["unidad_compra"],
                        r["valor"], exis, min, reor, max, "accion", ra
                    );

                    var row = dgv_arts.Rows[rowIndex];

                    // 4) Clasificación visual (mismo flujo condicional, sin conversions repetidas)
                    if (!string.IsNullOrEmpty(ra) && ra == "N")
                    {
                        row.DefaultCellStyle.BackColor = Color.LightCyan;
                        row.Cells[IDX_MSG].Value = "No tiene R.A.";
                        sinreord++;
                    }
                    else if (exis > max)
                    {
                        row.DefaultCellStyle.BackColor = Color.Pink;
                        row.Cells[IDX_MSG].Value = "SOBREINVENTARIO";
                        sobreinv++;
                    }
                    else if (exis <= min)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightCoral;
                        row.Cells[IDX_MSG].Value = "CRITICO";
                        subinv++;
                    }
                    else if (exis <= max && exis > reor)
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                        row.Cells[IDX_MSG].Value = "OPTIMO";
                        normal++;
                    }
                    else if (exis <= reor && exis > min)
                    {
                        row.DefaultCellStyle.BackColor = Color.Yellow;
                        row.Cells[IDX_MSG].Value = "PEDIR";
                        pedir++;
                    }
                }

                // 5) Totales y porcentajes
                tbx_sobreinv.Text = sobreinv.ToString();
                tbx_subinv.Text = subinv.ToString();
                tbx_normal.Text = normal.ToString();
                tbx_pedir.Text = pedir.ToString();
                tbx_sinreord.Text = sinreord.ToString();

                int suma = sobreinv + subinv + normal + pedir + sinreord;
                if (suma != 0)
                {
                    label1.Text = "Sobreinventario: " +  Global.Perc(sobreinv, suma);
                    label2.Text = "Criticos: " + Global.Perc(subinv, suma);
                    label3.Text = "Optimos: " + Global.Perc(normal, suma);
                    label4.Text = "Pedir: " + Global.Perc(pedir, suma);
                    label5.Text = "Sin Reorden Autom: " + Global.Perc(sinreord, suma);
                }
                else
                {
                    label1.Text = "Sobreinventario: ";
                    label2.Text = "Criticos: ";
                    label3.Text = "Optimos: ";
                    label4.Text = "Pedir: ";
                    label5.Text = "Sin Reorden Autom: ";
                }
            }
            finally
            {
                dgv_arts.ResumeLayout();
            }
        }

        private void OLDLlenar_DGV(string almacen, string grupo)
        {
            dgv_arts.SuspendLayout();

            try
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


                    FbDataAdapter ada = new FbDataAdapter(almacenes, AppSession.PeripheralConnectionString);
                    DataTable dtt = new DataTable();
                    ada.Fill(dtt);

                    if (dtt.Rows.Count > 0)
                    {
                        foreach (DataRow fila in dtt.Rows)
                        {
                            dgv_arts.Rows.Add(fila["ARTICULO_ID"], fila["clave_articulo"], fila["nombre"], fila["unidad_compra"],
                                fila["valor"], string.IsNullOrEmpty(fila["EXIS_MAT"].ToString()) ? 0 : fila["EXIS_MAT"],
                                string.IsNullOrEmpty(fila["MIN_MAT"].ToString()) ? 0 : fila["MIN_MAT"],
                                string.IsNullOrEmpty(fila["REORD_MAT"].ToString()) ? 0 : fila["REORD_MAT"],
                                string.IsNullOrEmpty(fila["MAX_MAT"].ToString()) ? 0 : fila["MAX_MAT"],
                                "accion", fila["reorden_automatico"]);
                        }

                        foreach (DataGridViewRow dr in dgv_arts.Rows)
                        {
                            if (dr.Cells[10].Value != null && dr.Cells[10].Value.ToString() == "N")
                            {
                                dr.DefaultCellStyle.BackColor = Color.LightCyan;
                                dr.Cells[9].Value = "No tiene R.A.";
                                sinreord++;
                            }
                            else
                            {
                                if (Convert.ToDouble(dr.Cells[5].Value) > Convert.ToDouble(dr.Cells[8].Value))
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

                if (suma != 0)
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
                    label2.Text = "Criticos: ";
                    label3.Text = "Optimos: ";
                    label4.Text = "Pedir: ";
                    label5.Text = "Sin Reorden Autom: ";
                }
            }
            finally
            {
                dgv_arts.ResumeLayout();
            }
        }
        #endregion

        #region LLENA GRID CON REFACTORIZACION

        #endregion

        private async void FormNiveles_Load(object sender, EventArgs e)
        {
            try
            {
                string grupos = "select a.clasificador_id, a.nombre from clasificadores_cat a";
                string almacenes = "select a.almacen_id, a.nombre from almacenes a";

                // 1) Corre ambas consultas en paralelo dentro del overlay (sin tocar UI)
                var result = await RunWithOverlayAsync<Tuple<DataTable, DataTable>>(() =>
                {
                    var table1 = _repository.QueryTable(grupos);
                    var table2 = _repository.QueryTable(almacenes);
                    return Tuple.Create(table1, table2);
                }, "Cargando catálogos...");
              
                var dt = result.Item1;
                var dt2 = result.Item2;

                // 2) Ya en el hilo de UI: bindea a los controles
                cbo_clasificaciones.DataSource = dt;
                cbo_clasificaciones.DisplayMember = "nombre";
                cbo_clasificaciones.ValueMember = "clasificador_id";
                cbo_clasificaciones.SelectedIndex = -1;

                cbo_almacenes.DataSource = dt2;
                cbo_almacenes.DisplayMember = "nombre";
                cbo_almacenes.ValueMember = "almacen_id";
                cbo_almacenes.SelectedIndex = 0;

                i++;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btn_general_Click(object sender, EventArgs e)
        {
            //Llenar_DGV(cbo_almacenes.SelectedValue.ToString(), "");

            try
            {
                var alm = cbo_almacenes.SelectedValue != null ? cbo_almacenes.SelectedValue.ToString() : "";
                await RunUIWorkWithOverlayAsync(
                    () => Llenar_DGV(alm, ""),         // no se modifica tu método
                    title: "Cargando datos...",
                    subtitle: "Calculando niveles y pintando filas",
                    false
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private async void btnVistaValor_Click(object sender, EventArgs e)
        {
            //Llenar_DGV(cbo_almacenes.SelectedValue.ToString(), cbo_grupos.SelectedValue.ToString());
            try
            {
                var alm = cbo_almacenes.SelectedValue != null ? cbo_almacenes.SelectedValue.ToString() : "";
                var grp = cbo_grupos.SelectedValue != null ? cbo_grupos.SelectedValue.ToString() : "";
                await RunUIWorkWithOverlayAsync(
                    () => Llenar_DGV(alm, grp),         // no se modifica tu método
                    title: "Cargando datos...",
                    subtitle: "Calculando niveles y pintando filas",
                    false
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void btnShowPanelValues_Click(object sender, EventArgs e)
        {
            if (!panelExpandido)
            {
                // EXPANDIR → mitad y mitad
                int totalAncho = splitContainer1.Width;
                splitContainer1.SplitterDistance = totalAncho / 2;
                panelExpandido = true;
                btnShowPanelValues.Text = "Ocultar valores";
            }
            else
            {
                // COLAPSAR → Panel2 tamaño mínimo
                int totalAncho = splitContainer1.Width;
                splitContainer1.SplitterDistance = totalAncho - anchoMinimoPanel2;
                panelExpandido = false;
                btnShowPanelValues.Text = "Ver valores";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
          
        }
    }
}

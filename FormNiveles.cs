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
        private int anchoMinimoPanel2 = 100; // el mínimo para mostrar el botón
        private DataTable DTValores = new DataTable();

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
            //dataGridViewValores.Rows.Clear();

            lblSobreinventario.Text = "Sobreinventario: ";
            lblCriticos.Text = "Criticos: ";
            lblOptimos.Text = "Optimos: ";
            lblPedir.Text = "Pedir: ";
            tbx_sobreinv.Text = "0";
            tbx_subinv.Text = "0";
            tbx_normal.Text = "0";
            tbx_pedir.Text = "0";
            tbx_art.Text = "";
        }


        #region LLENA GRID CON REFACTORIZACION
        
        private (int sobreinv, int subinv, int normal, int pedir, int sinreord) CalcularTotalesPorValor(int almacenId, int valorId)
        {
            string sql =
                "SELECT " +
                "  (SELECT EXISTENCIA FROM GET_EXIS_ART_SUCURSAL(@alm, A.ARTICULO_ID, 0)) AS EXIS_MAT," +
                "  G.INVENTARIO_MAXIMO AS MAX_MAT," +
                "  G.PUNTO_REORDEN AS REORD_MAT," +
                "  G.INVENTARIO_MINIMO AS MIN_MAT," +
                "  f.reorden_automatico " +
                "FROM elementos_cat_clasif d " +
                "LEFT JOIN articulos a ON d.elemento_id = a.articulo_id " +
                "LEFT JOIN libres_articulos f ON a.articulo_id = f.articulo_id " +
                "LEFT JOIN NIVELES_ARTICULOS G ON A.ARTICULO_ID = G.ARTICULO_ID AND G.almacen_id = @alm " +
                "WHERE d.valor_clasif_id = @valor";

            int sobreinv = 0, subinv = 0, normal = 0, pedir = 0, sinreord = 0;

            using (var conn = new FbConnection(AppSession.PeripheralConnectionString))
            using (var cmd = new FbCommand(sql, conn))
            {
                cmd.Parameters.Add("@alm", FbDbType.Integer).Value = almacenId;
                cmd.Parameters.Add("@valor", FbDbType.Integer).Value = valorId;

                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        double exis = Global.ToDouble(rdr["EXIS_MAT"]);
                        double min = Global.ToDouble(rdr["MIN_MAT"]);
                        double reor = Global.ToDouble(rdr["REORD_MAT"]);
                        double max = Global.ToDouble(rdr["MAX_MAT"]);
                        string ra = rdr["reorden_automatico"]?.ToString() ?? "";

                        if (ra == "N")
                            sinreord++;
                        else if (exis > max)
                            sobreinv++;
                        else if (exis <= min)
                            subinv++;
                        else if (exis <= max && exis > reor)
                            normal++;
                        else if (exis <= reor && exis > min)
                            pedir++;
                    }
                }
            }

            return (sobreinv, subinv, normal, pedir, sinreord);
        }

        private void ActualizarHeadersValores()
        {
            if (dataGridViewValores.Tag == null)
                return;

            dynamic h = dataGridViewValores.Tag;

            dataGridViewValores.Columns[2].HeaderText = $"Sobreinventario ({h.pSobre})";
            dataGridViewValores.Columns[3].HeaderText = $"Óptimos ({h.pNorm})";
            dataGridViewValores.Columns[4].HeaderText = $"Pedir ({h.pPed})";
            dataGridViewValores.Columns[5].HeaderText = $"Críticos ({h.pSub})";            
            dataGridViewValores.Columns[6].HeaderText = $"Sin R.A. ({h.pSin})";
        }

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
                    lblSobreinventario.Text = "Sobreinventario: ";
                    lblCriticos.Text = "Criticos: ";
                    lblOptimos.Text = "Optimos: ";
                    lblPedir.Text = "Pedir: ";
                    lblSinReorden.Text = "Sin Reorden Autom: ";
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
                    lblSobreinventario.Text = "Sobreinventario: " + Global.Perc(sobreinv, suma);
                    lblCriticos.Text = "Criticos: " + Global.Perc(subinv, suma);
                    lblOptimos.Text = "Optimos: " + Global.Perc(normal, suma);
                    lblPedir.Text = "Pedir: " + Global.Perc(pedir, suma);
                    lblSinReorden.Text = "Sin Reorden Autom: " + Global.Perc(sinreord, suma);
                }
                else
                {
                    lblSobreinventario.Text = "Sobreinventario: ";
                    lblCriticos.Text = "Criticos: ";
                    lblOptimos.Text = "Optimos: ";
                    lblPedir.Text = "Pedir: ";
                    lblSinReorden.Text = "Sin Reorden Autom: ";
                }
            }
            finally
            {
                dgv_arts.ResumeLayout();
            }
        }
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

                cboAlmacen.DataSource = dt2.Copy();
                cboAlmacen.DisplayMember = "nombre";
                cboAlmacen.ValueMember = "almacen_id";
                cboAlmacen.SelectedIndex = 0;

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
                // Mostrar loading solo en el panel izquierdo donde están los artículos
                await RunUIWorkWithOverlayAsync(
                    () => Llenar_DGV(alm, ""),
                    title: "Cargando artículos...",
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
                // Mostrar loading solo en el panel izquierdo donde están los artículos
                await RunUIWorkWithOverlayAsync(
                    () => Llenar_DGV(alm, grp),
                    title: "Cargando por valor...",
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

        /// <summary>
        /// Combobox para cargar valores por el grupo seleccionado
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LIMPIA();

            dataGridViewValores.Rows.Clear();

            if (i == 0)
                return;

            if (cbo_clasificaciones.SelectedValue == null)
                return;

            int clasifId = Convert.ToInt32(cbo_clasificaciones.SelectedValue);

            string sql =
               $"SELECT valor_clasif_id, valor " +
                "FROM clasificadores_cat_valores " +
               $"WHERE clasificador_id = {clasifId}";

            //DataTable dt = new DataTable();
            DTValores = new DataTable();
            FbDataAdapter da = new FbDataAdapter(sql, AppSession.PeripheralConnectionString);
            da.Fill(DTValores);

            cbo_grupos.DataSource = DTValores;
            cbo_grupos.DisplayMember = "valor";
            cbo_grupos.ValueMember = "valor_clasif_id";
            cbo_grupos.SelectedIndex = 0;

            if (panelExpandido)
            {
                try
                {
                    // Mostrar loading solo en el panel derecho
                    await RunUIWorkWithPanelOverlayAsync(
                        splitContainer1.Panel2,  // Panel específico
                        () => TogglePanelValues(),  // Método que modifica controles
                        title: panelExpandido ? "Ocultando panel de valores..." : "Mostrando panel de valores...",
                        subtitle: panelExpandido ? "Cerrando vista de estadísticas" : "Cargando estadísticas por grupo",
                        cancellable: false
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CargarValoresPorGrupoCompras()
        {
            groupBoxValores.Text = $"Valores de {cbo_clasificaciones.Text}";
            dataGridViewValores.Rows.Clear();

            // ---- Necesitamos el almacén seleccionado ----
            if (cboAlmacen.SelectedValue == null)
                return;

            int almacenId = Convert.ToInt32(cboAlmacen.SelectedValue);

            // ---- Ahora recorrer los valores, calcular totales y agregar al grid ----
            foreach (DataRow fila in DTValores.Rows)
            {
                int valorId = Convert.ToInt32(fila["valor_clasif_id"]);
                string valorTexto = fila["valor"].ToString();

                var tot = CalcularTotalesPorValor(almacenId, valorId);

                int suma = tot.sobreinv + tot.subinv + tot.normal + tot.pedir + tot.sinreord;

                string pSobre = (suma == 0 ? "0%" : Global.Perc(tot.sobreinv, suma));
                string pSub = (suma == 0 ? "0%" : Global.Perc(tot.subinv, suma));
                string pNorm = (suma == 0 ? "0%" : Global.Perc(tot.normal, suma));
                string pPed = (suma == 0 ? "0%" : Global.Perc(tot.pedir, suma));
                string pSin = (suma == 0 ? "0%" : Global.Perc(tot.sinreord, suma));

                dataGridViewValores.Rows.Add(
                    valorId,
                    valorTexto,
                    pSobre,
                    pNorm,
                    pPed,
                    pSub,                    
                    pSin
                //tot.sobreinv,  // num
                //tot.subinv,    // num
                //tot.normal,    // num
                //tot.pedir,     // num
                //tot.sinreord   // num
                );

                // Guardar estos porcentajes para actualizar encabezados después
                dataGridViewValores.Tag = new
                {
                    pSobre,
                    pSub,
                    pNorm,
                    pPed,
                    pSin
                };
            }

            //ActualizarHeadersValores();
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

        private async void btnShowPanelValues_Click(object sender, EventArgs e)
        {
            if (cbo_clasificaciones.SelectedValue == null)
            {
                MessageBox.Show("Seleccione una clasificación.");
                return;
            }
                

            try
            {
                // Mostrar loading solo en el panel derecho
                await RunUIWorkWithPanelOverlayAsync(
                    splitContainer1.Panel2,  // Panel específico
                    () => TogglePanelValues(),  // Método que modifica controles
                    title: panelExpandido ? "Ocultando panel de valores..." : "Mostrando panel de valores...",
                    subtitle: panelExpandido ? "Cerrando vista de estadísticas" : "Cargando estadísticas por grupo",
                    cancellable: false
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TogglePanelValues()
        {
            // ---- LIMPIAR GRID ----
            dataGridViewValores.Rows.Clear();

            if (!panelExpandido)
            {
                groupBoxValores.Visible = true;
                groupBoxFiltroAlmacen.Visible = true;

                // Activar scrollbar en Panel1 para evitar que se corten los controles
                splitContainer1.Panel1.AutoScroll = true;

                // 1. Cargar datos primero para que el grid calcule los anchos de columna
                CargarValoresPorGrupoCompras();

                // 2. Calcular el ancho requerido basado en las columnas visibles
                int requiredWidth = 0;
                foreach (DataGridViewColumn col in dataGridViewValores.Columns)
                {
                    if (col.Visible) requiredWidth += col.Width;
                }

                // 3. Agregar padding (scrollbar vertical + bordes + márgenes)
                // Un scrollbar típico es ~17px, más márgenes del GroupBox y padding
                requiredWidth += 50; 

                // 4. Definir límites (Mínimo razonable y Máximo para no tapar todo)
                int maxWidth = (int)(splitContainer1.Width * 0.6); // Máximo 60% de la pantalla
                int minWidth = 300; // Mínimo 300px

                if (requiredWidth > maxWidth) requiredWidth = maxWidth;
                if (requiredWidth < minWidth) requiredWidth = minWidth;

                // 5. Aplicar el nuevo SplitterDistance
                // Nota: SplitterDistance es la distancia desde la izquierda (Panel1)
                int nuevoSplitterDistance = splitContainer1.Width - requiredWidth;

                // Asegurar que Panel1 tenga al menos su tamaño mínimo
                if (nuevoSplitterDistance < splitContainer1.Panel1MinSize)
                {
                    nuevoSplitterDistance = splitContainer1.Panel1MinSize;
                }

                splitContainer1.SplitterDistance = nuevoSplitterDistance;
                panelExpandido = true;
                btnShowPanelValues.Text = "Ocultar Panel";
            }
            else
            {
                groupBoxValores.Visible = false;
                groupBoxFiltroAlmacen.Visible = false;

                // Desactivar scrollbar en Panel1 cuando se colapsa
                splitContainer1.Panel1.AutoScroll = false;
                
                // Forzar el reset de la posición del scroll para que se oculte completamente
                splitContainer1.Panel1.AutoScrollPosition = new System.Drawing.Point(0, 0);

                // COLAPSAR → Panel2 tamaño mínimo
                int totalAncho = splitContainer1.Width;
                int anchoMinimoPanel2 = 310; // Definir un ancho mínimo para el Panel2
                splitContainer1.SplitterDistance = totalAncho - anchoMinimoPanel2;
                panelExpandido = false;
                btnShowPanelValues.Text = "Ver Panel Valores";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
          
        }

        private async void btnCargarValores_Click(object sender, EventArgs e)
        {
            // Mostrar loading solo en el panel derecho donde están los valores
            await RunUIWorkWithPanelOverlayAsync(
                splitContainer1.Panel2,  // Panel específico donde están los valores
                () => CargarValoresPorGrupoCompras(),
                title: "Cargando valores...",
                subtitle: "Calculando estadísticas por grupo",
                false
            );
        }

        private async void cboAlmacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ejemplo de loading en panel específico - solo en el panel derecho
            await RunUIWorkWithPanelOverlayAsync(
                splitContainer1.Panel2,  // Panel específico donde mostrar el loading
                () => CargarValoresPorGrupoCompras(),
                title: "Cargando valores...",
                subtitle: "Actualizando datos del almacén",
                false
            );
        }

        // Método de ejemplo para mostrar loading en el panel izquierdo
        private async void CargarDatosEnPanelIzquierdo()
        {
            try
            {
                var alm = cbo_almacenes.SelectedValue != null ? cbo_almacenes.SelectedValue.ToString() : "";
                await RunUIWorkWithPanelOverlayAsync(
                    splitContainer1.Panel1,  // Panel izquierdo
                    () => Llenar_DGV(alm, ""),
                    title: "Cargando artículos...",
                    subtitle: "Calculando niveles de inventario",
                    false
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Método de ejemplo para mostrar loading en todo el formulario
        private async void CargarDatosCompletos()
        {
            try
            {
                // Usar el loading completo del formulario
                await RunUIWorkWithOverlayAsync(
                    () => {
                        // Simular carga de datos completos
                        System.Threading.Thread.Sleep(2000);
                        CargarValoresPorGrupoCompras();
                    },
                    title: "Cargando datos completos...",
                    subtitle: "Esto puede tardar unos momentos",
                    false
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Ejemplo de uso del método estático para casos simples
        private void EjemploLoadingEstatico()
        {
            // Crear y mostrar loading usando el método estático
            var loading = UI.Controls.PanelLoadingOverlay.Create(
                splitContainer1.Panel1, 
                "Procesando...", 
                "Operación en progreso"
            );

            try
            {
                // Simular trabajo
                System.Threading.Thread.Sleep(1000);
                
                // Actualizar mensaje durante el proceso
                loading.UpdateMessage("Finalizando...", "Casi terminado");
                System.Threading.Thread.Sleep(500);
            }
            finally
            {
                // Ocultar loading
                loading.HideFromPanel();
            }
        }
    }
}

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
using System.IO;

namespace Niveles
{
    public partial class FormVerArt : Form
    {

        public int id_art;
        public int id_almacen;
        public int id_almacen_orden;

        public FormVerArt()
        {
            InitializeComponent();
        }

        private void Cargar_Art(int art_id, int almacen_id)
        {
            string fijos = "select a.nombre, b.nombre as linea, c.nombre as grupo, a.unidad_venta, a.unidad_compra, " +
                            "d.unidades_min_uven as compra_minima, "+
                                "case " +
                                    "when a.estatus = 'V' then 'Suspension de Ventas' " +
                                    "when a.estatus = 'C' then 'Suspension de Compras' " +
                                    "when a.estatus = 'A' then 'Activo' " +
                                    "when a.estatus = 'B' then 'Baja' " +
                                    "when a.estatus = 'S' then 'Suspension de Compras y Ventas' " +
                                    "else 'Reportar a Lic'  end " +
                                    "as estatus " +
                                "from articulos a " +
                                "left join lineas_articulos b on a.linea_articulo_id = b.linea_articulo_id " +
                                "left join grupos_lineas c on b.grupo_linea_id = c.grupo_linea_id " +
                                "left join precios_compra d on a.articulo_id = d.articulo_id and d.es_prov_predet = true " +
                                $"where a.articulo_id = {art_id}";

            FbDataAdapter ada = new FbDataAdapter(fijos, ClaseConn.cadena);
            DataTable dtt = new DataTable();
            ada.Fill(dtt);

            tbx_estatus.Text = dtt.Rows[0]["estatus"].ToString();
            tbx_nombre.Text = dtt.Rows[0]["nombre"].ToString();
            tbx_linea.Text = dtt.Rows[0]["linea"].ToString();
            tbx_grupo.Text = dtt.Rows[0]["grupo"].ToString();
            tbx_orden_minima.Text = dtt.Rows[0]["compra_minima"].ToString();
            tbx_uc.Text = dtt.Rows[0]["unidad_compra"].ToString();
            tbx_um.Text = dtt.Rows[0]["unidad_venta"].ToString();


            string claves = "select b.clave_articulo_id as clave_id, a.nombre as rol, b.clave_articulo as clave "+
                                    "from claves_articulos b " +
                                    "left join roles_claves_articulos a on b.rol_clave_art_id = a.rol_clave_art_id " +
                                    $"where b.articulo_id = {art_id}";

            FbDataAdapter clave_ada = new FbDataAdapter(claves, ClaseConn.cadena);
            DataTable clave_dtt = new DataTable();
            clave_ada.Fill(clave_dtt);

            foreach(DataRow dr in clave_dtt.Rows)
            {
                dgv_claves.Rows.Add(dr["clave_id"], dr["clave"], dr["rol"]);
            }


            string clasifs = "select a.valor, b.nombre "+
                                "from elementos_cat_clasif c "+
                                "left join clasificadores_cat_valores a on c.valor_clasif_id = a.valor_clasif_id "+
                                "left join clasificadores_cat b on a.clasificador_id = b.clasificador_id "+
                                $"where c.elemento_id = {art_id}";

            FbDataAdapter clasif_ada = new FbDataAdapter(clasifs, ClaseConn.cadena);
            DataTable clasifs_dtt = new DataTable();
            clasif_ada.Fill(clasifs_dtt);

            foreach (DataRow cdr in clasifs_dtt.Rows)
            {
                dgv_clasificadores.Rows.Add(cdr["nombre"], cdr["valor"]);
            }



            Cargar_art_almacen(art_id, almacen_id);

            


            

        }

        private void Limpiar_dependientes_almacen()
        {
            dgv_kardex.Rows.Clear();            
            tbx_ubicacion.Clear();
        }

        private void Cargar_art_almacen(int art_id, int almacen_id)
        {

            Limpiar_dependientes_almacen();

            string kardex = "select first 200 fecha as Fecha, nombre_concep as Concepto, folio as Folio, descripcion as Desc, unidades as Ud, inv_fin_unidades as Inv_Final from " +
                    $"auxiliar_artalm({art_id}, {almacen_id}, '" + DateTime.Now.AddYears(-3).ToString("dd.MM.yyyy") + "', " +
                    "'" + DateTime.Now.ToString("dd.MM.yyyy") + "',1, " +
                    $"(select Existencia from exival_art({art_id}, {almacen_id}, '{DateTime.Now.AddYears(-3).AddDays(-1).ToString("dd.MM.yyyy")}','N',343918))) a " +
                    "left join articulos b on a.articulo_id = b.articulo_id "+
                    "where b.es_almacenable = 'S' " +
                    "order by docto_in_id desc";

            FbDataAdapter kardex_a = new FbDataAdapter(kardex, ClaseConn.cadena);
            DataTable kardex_dt = new DataTable();
            kardex_a.Fill(kardex_dt);

            foreach (DataRow kdr in kardex_dt.Rows)
            {
                dgv_kardex.Rows.Add(Convert.ToDateTime(kdr["Fecha"]).ToShortDateString(), kdr["Concepto"], kdr["Folio"], kdr["Desc"], kdr["Ud"], kdr["Inv_Final"]);
            }


            string ubicacion = $"select a.localizacion from niveles_articulos a where a.articulo_id = {art_id} and a.almacen_id = {almacen_id}";
            FbDataAdapter ubi_ada = new FbDataAdapter(ubicacion, ClaseConn.cadena);
            DataTable ubi_dtt = new DataTable();
            ubi_ada.Fill(ubi_dtt);
            if(ubi_dtt.Rows.Count > 0)
            {
                tbx_ubicacion.Text = ubi_dtt.Rows[0]["localizacion"].ToString();
            }
            else
            {
                tbx_ubicacion.Text = "";
            }
            
        }

        private void FormVerArt_Load(object sender, EventArgs e)
        {
            string almacenes = "select a.almacen_id, a.nombre from almacenes a ";
            FbDataAdapter ada = new FbDataAdapter(almacenes, ClaseConn.cadena);
            DataTable dtt = new DataTable();
            ada.Fill(dtt);

            cbo_almacen.DataSource = dtt;
            cbo_almacen.DisplayMember = "nombre";
            cbo_almacen.ValueMember = "almacen_id";
            cbo_almacen.SelectedIndex = 0;

            cbo_almacen.SelectedIndex = id_almacen_orden;

            Cargar_Art(id_art, id_almacen);

        }

        private void dgv_claves_SelectionChanged(object sender, EventArgs e)
        {
            dgv_claves.ClearSelection();
        }

        private void dgv_clasificadores_SelectionChanged(object sender, EventArgs e)
        {
            dgv_clasificadores.ClearSelection();
        }

        private void dgv_kardex_SelectionChanged(object sender, EventArgs e)
        {
            dgv_kardex.ClearSelection();
        }

        private void cbo_almacen_SelectedIndexChanged(object sender, EventArgs e)
        {
            Limpiar_dependientes_almacen();
        }

        private void btn_cargar_Click(object sender, EventArgs e)
        {
            Cargar_art_almacen(id_art, Convert.ToInt32(cbo_almacen.SelectedValue.ToString()));
        }

        private void btn_ver_imagen_Click(object sender, EventArgs e)
        {
            string q = $"select imagen from imagenes_articulos where articulo_id = {id_art} and rol_imagen_art_id = 64";
            FbConnection con = new FbConnection(ClaseConn.cadena);
            con.Open();
            FbCommand cmd = new FbCommand(q, con);
            byte[] imageData = (byte[])cmd.ExecuteScalar();

            // Convert the byte array to an Image

            Image image;
            if (imageData != null)
            {


                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    image = Image.FromStream(ms);
                }

                FormImagen formImagen = new FormImagen();
                formImagen.art_id = id_art;
                formImagen.imagen = image;
                formImagen.ShowDialog();
            }
            else
            {
                MessageBox.Show("ARTICULO NO TIENE IMAGEN GUARDADA");
            }
        }
    }
}

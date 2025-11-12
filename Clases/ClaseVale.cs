using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FirebirdSql.Data.FirebirdClient;
using System.Windows.Forms;


namespace Vales.Clases
{
    class ClaseVale
    {

        public static int docto_pv_id;

        public static string folio_factura;

        public static string ticket_id;

        public static string importe;

        public static string nombre_cliente;

        public static int cliente_id;

        public static string fecha_fact;

        public static string tipo_docto;

        public static string calle;

        public static string colonia;

        public static string num_exterior;

        public static string telefono1;

        public static string estatus;

        public static string vendedor;

        public static string folio_devolucion;



        public static bool Validar_Factura(string codigo_barras_fact)
        {
            if(int.TryParse(codigo_barras_fact, out int docto_id))
            {
                    string validacion_factura = "select * from doctos_pv where tipo_docto = 'F' and es_fac_global != 'S' and docto_pv_id = " + docto_id;
                    FbDataAdapter adapter_validacion_factura = new FbDataAdapter(validacion_factura, ClaseConn.cadena);
                    DataTable tabla_validacion_factura = new DataTable();
                    adapter_validacion_factura.Fill(tabla_validacion_factura);

                    if (tabla_validacion_factura.Rows.Count == 1)
                    {
                       docto_pv_id = Convert.ToInt32(tabla_validacion_factura.Rows[0]["docto_pv_id"].ToString());                       
                       return true;
                    }
                    else
                    {
                        MessageBox.Show("Clave Incorrecta", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
            }
            else
            {
                MessageBox.Show("Clave Incorrecta", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            
        }

        public static void Set_Datos_Factura()
        {

            if (docto_pv_id != 0)
            {
                string query_datos = "select clientes.cliente_id, clientes.nombre, doctos_pv.docto_pv_id, doctos_pv.folio, " +
                             "(doctos_pv.importe_neto + doctos_pv.total_impuestos)as importe_neto, doctos_pv.almacen_id, doctos_pv.fecha, doctos_pv.tipo_docto, " +
                             "dirs_clientes.calle, dirs_clientes.colonia, dirs_clientes.num_exterior, dirs_clientes.telefono1, " +
                             "doctos_pv_ligas.docto_pv_fte_id, doctos_pv.estatus, " +

                              "(select vendedores.nombre from doctos_pv left join vendedores on doctos_pv.vendedor_id = vendedores.vendedor_id " +
                              "where doctos_pv.docto_pv_id = doctos_pv_ligas.docto_pv_fte_id ) as vendedor " +

                             "from doctos_pv " +
                             "left join clientes on doctos_pv.cliente_id = clientes.cliente_id " +
                             "left join  dirs_clientes on clientes.cliente_id = dirs_clientes.cliente_id " +
                             "left join doctos_pv_ligas on doctos_pv.docto_pv_id = doctos_pv_ligas.docto_pv_dest_id " +
                             "where doctos_pv.docto_pv_id = " + docto_pv_id + " and dirs_clientes.es_dir_ppal = 'S'";

                FbDataAdapter adapter_datos_fact = new FbDataAdapter(query_datos, ClaseConn.cadena);
                DataTable tabla_datos_fact = new DataTable();
                adapter_datos_fact.Fill(tabla_datos_fact);

                if(tabla_datos_fact.Rows.Count == 1)
                {
                    folio_factura = tabla_datos_fact.Rows[0]["folio"].ToString();
                    cliente_id = Convert.ToInt32(tabla_datos_fact.Rows[0]["cliente_id"].ToString());
                    nombre_cliente = tabla_datos_fact.Rows[0]["nombre"].ToString();
                    ticket_id = tabla_datos_fact.Rows[0]["docto_pv_fte_id"].ToString();
                    importe = "$" + Convert.ToInt32(tabla_datos_fact.Rows[0]["importe_neto"]).ToString("N0");
                    fecha_fact = Convert.ToDateTime(tabla_datos_fact.Rows[0]["fecha"].ToString()).ToShortDateString();
                    tipo_docto = tabla_datos_fact.Rows[0]["tipo_docto"].ToString();
                    calle = tabla_datos_fact.Rows[0]["calle"].ToString();
                    colonia = tabla_datos_fact.Rows[0]["colonia"].ToString();
                    num_exterior = tabla_datos_fact.Rows[0]["num_exterior"].ToString();
                    telefono1 = tabla_datos_fact.Rows[0]["telefono1"].ToString();
                    estatus = tabla_datos_fact.Rows[0]["estatus"].ToString();
                    vendedor = tabla_datos_fact.Rows[0]["vendedor"].ToString();

                }
                else
                {
                    MessageBox.Show("ERROR SE CARGO MAS DE 1 DOCTO FAVOR DE REPORTAR");
                }
            }
            else
            {
                MessageBox.Show("ERROR FAVOR DE REPORTAR");
            }

        }

        public static DataTable Articulos_Factura()
        {

            //tambien puedes regresar una List<ClaseArticulos>

            string query_articulos = "select doctos_pv_det.docto_pv_det_id, articulos.articulo_id, articulos.nombre as Articulo, " +
                    "articulos.unidad_venta as Um, doctos_pv_det.unidades as UmTotal, " +
                    "case when sum(soft_entregasdet.unidades) is null then 0 " +
                    "else sum(soft_entregasdet.unidades) end as Entregado, " +
                    "doctos_pv_det.unidades - B.unidades_dev - (case when sum(soft_entregasdet.unidades) is null then 0 " +
                    "else sum(soft_entregasdet.unidades) end) as Pendiente " +
                    "from doctos_pv " +
                    "join doctos_pv_det on doctos_pv.docto_pv_id = doctos_pv_det.docto_pv_id " +
                    "left join soft_entregasdet on doctos_pv.docto_pv_id = soft_entregasdet.doctopvid " +
                    "and doctos_pv_det.docto_pv_det_id = soft_entregasdet.doctopvdetid " +
                    "join articulos on doctos_pv_det.articulo_id = articulos.articulo_id " +
                    "left join doctos_pv_ligas_det on doctos_pv_det.docto_pv_det_id = doctos_pv_ligas_det.docto_pv_det_dest_id " +
                    "left join doctos_pv_det B on doctos_pv_ligas_det.docto_pv_det_fte_id = B.docto_pv_det_id " +
                    "where doctos_pv.docto_pv_id = " + docto_pv_id +" "+
                    "group by articulos.nombre, docto_pv_det_id, Um, UmTotal, articulos.articulo_id, B.unidades_dev " +
                    "order by doctos_pv_det.docto_pv_det_id";

            FbDataAdapter adapter_articulos = new FbDataAdapter(query_articulos, ClaseConn.cadena);
            DataTable tabla_articulos = new DataTable();
            adapter_articulos.Fill(tabla_articulos);

            if(tabla_articulos.Rows.Count >= 1)
            {
                return tabla_articulos;
            }
            else
            {
                return null;
            }
        }

        public static DataTable Cobradores()
        {

            string query_cobradores = "select cobradores.cobrador_id, cobradores.nombre from cobradores " +
                 "where cobradores.politica_comis_cob_id = 339258 order by nombre";
            FbDataAdapter adapter_cobradores = new FbDataAdapter(query_cobradores, ClaseConn.cadena);
            DataTable tabla_cobradores = new DataTable();
            adapter_cobradores.Fill(tabla_cobradores);

            if(tabla_cobradores.Rows.Count >= 1)
            {
                return tabla_cobradores;
            }
            else
            {
                return null;
            }
            
        }

    }
}

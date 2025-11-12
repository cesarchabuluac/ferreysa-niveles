using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using System.Data;

namespace Vales.Clases
{
    class ClaseCotizacion
    {

        public static DataTable Buscar_Arts(string text)
        {

            if (!string.IsNullOrWhiteSpace(text))
            {



                string[] palabras = text.Split(' ');

                string cadena = "";

                foreach (var palabra in palabras)
                {
                    cadena = cadena + " and Upper(Nombre) Like '%" + palabra.ToUpper() + "%'";
                }
                cadena = " Where 0 = 0 " + cadena;

                string query_arts = "select articulos.nombre, articulos.articulo_id, articulos.unidad_venta ud " +
                    "from articulos " + cadena;

                FbDataAdapter adapter_arts = new FbDataAdapter(query_arts, ClaseConn.cadena);
                DataTable tabla_arts = new DataTable();
                adapter_arts.Fill(tabla_arts);

                if (tabla_arts.Rows.Count >= 1)
                {
                    return tabla_arts;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static DataTable Buscar_Clientes(string texto)
        {

            if (!string.IsNullOrWhiteSpace(texto))
            {

                string[] palabras = texto.Split(' ');

                string cadena = "";

                foreach (var palabra in palabras)
                {
                    cadena = cadena + " and Upper(Nombre) like '%" + palabra.ToUpper() + "%'";
                }
                cadena = " where 0=0 " + cadena;

                string query_clientes = "select clientes.cliente_id, clientes.nombre from clientes " + cadena;

                FbDataAdapter adapter_clientes = new FbDataAdapter(query_clientes, ClaseConn.cadena);
                DataTable tabla_clientes = new DataTable();
                adapter_clientes.Fill(tabla_clientes);

                if (tabla_clientes.Rows.Count >= 1)
                {
                    return tabla_clientes;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        

      

    }
}

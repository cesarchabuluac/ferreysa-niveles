using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using System.Data;
using System.Windows.Forms;

namespace Vales.Clases
{
    class ClaseUsuario
    {

        public static int vendedor_id;

        public static string vendedor_nombre;

        public static int vendedor_politica_comis_id;

        public static bool Validacion_Usuario(string codigo_barras)
        {
            int int_codigo_barras;

            if (int.TryParse(codigo_barras, out int_codigo_barras)) 
            {
                string query_validacion = "select * from vendedores where vendedor_id = "+ int_codigo_barras;

                FbDataAdapter adapter_validacion_usuario = new FbDataAdapter(query_validacion, ClaseConn.cadena);
                DataTable tabla_validacion_usuario = new DataTable();
                adapter_validacion_usuario.Fill(tabla_validacion_usuario);

                if(tabla_validacion_usuario.Rows.Count == 1)
                {
                    vendedor_id = Convert.ToInt32(tabla_validacion_usuario.Rows[0]["vendedor_id"].ToString());
                               
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
            };
            
           

        }

        public static void Set_Vendedor_Nombre()
        {
            if (vendedor_id != 0)
            {
                string query_validacion = "select * from vendedores where vendedor_id = " + vendedor_id;

                FbDataAdapter adapter_validacion_usuario = new FbDataAdapter(query_validacion, ClaseConn.cadena);
                DataTable tabla_validacion_usuario = new DataTable();
                adapter_validacion_usuario.Fill(tabla_validacion_usuario);

                if (tabla_validacion_usuario.Rows.Count == 1)
                {
                    vendedor_nombre = tabla_validacion_usuario.Rows[0]["nombre"].ToString();
                    vendedor_politica_comis_id = Convert.ToInt32(tabla_validacion_usuario.Rows[0]["politica_comis_ven_id"].ToString());
                }
            }
            else
            {
                MessageBox.Show("ERROR FAVOR DE REPORTAR");
            }
        }



    }


}

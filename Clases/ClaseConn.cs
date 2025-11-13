using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;


namespace Vales
{
    class ClaseConn
    {
        public FbConnection fbconn = null;
        public static string cadena = null;
        public static bool es_papalote = false;

        public void MetodoConn(string servidor, string base_datos, string usuario, string password)
        {
            cadena = $"DataSource={servidor};Database={base_datos};User={usuario};Password={password};Dialect=3;Charset=UTF8;Port=3050";
        }

        public bool Validacion()
        {
            try
            {
                fbconn = new FbConnection(cadena);
                fbconn.Open();
                fbconn.Close();
                return true;
            }
            catch (FbException)
            {
                return false;
            }
        }
    }
}

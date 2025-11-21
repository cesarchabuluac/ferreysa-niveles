using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vales.Clases
{
    public class ConexionCentral
    {
        public static string cadena_central = null;

        public void MetodoConnCentral(string servidor, string base_datos, string usuario, string password)
        {
            cadena_central = $"DataSource={servidor};Database={base_datos};User={usuario};Password={password};Dialect=3;Charset=UTF8;Port=3050";
        }
    }
}

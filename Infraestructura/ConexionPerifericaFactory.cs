using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Niveles.Domain;

namespace Niveles.Infraestructura
{
    /// <summary>
    /// Centraliza la construcción de la connection string periférica.
    /// </summary>
    public static class ConexionPerifericaFactory
    {
        public static string Build(Empresa e)
        {
            var csb = new FbConnectionStringBuilder
            {
                DataSource = string.IsNullOrWhiteSpace(e.DataSource) ? "127.0.0.1" : e.DataSource,
                Database = e.Database,
                Port = e.Port == 0 ? 3050 : e.Port,
                UserID = string.IsNullOrWhiteSpace(e.UserId) ? "SYSDBA" : e.UserId,
                Password = e.Password ?? "",
                Dialect = e.Dialect == 0 ? 3 : e.Dialect,
                Charset = "UTF8",
                Pooling = true
            };
            return csb.ToString();
        }
    }
}

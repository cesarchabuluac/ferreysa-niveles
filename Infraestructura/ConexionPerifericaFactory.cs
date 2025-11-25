using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Niveles.Domain;
using Niveles.Helpers;

namespace Niveles.Infraestructura
{
    /// <summary>
    /// Centraliza la construcción de la connection string periférica.
    /// </summary>
    public static class ConexionPerifericaFactory
    {
        public static string Build(Empresa e)
        {
            try
            {
                FileLogger.Separator("CONEXIÓN PERIFÉRICA");
                FileLogger.Info($"Construyendo conexión para empresa: {e.Nombre} (ID: {e.IdEmpresa})");
                
                var dataSource = string.IsNullOrWhiteSpace(e.DataSource) ? "127.0.0.1" : e.DataSource;
                var port = e.Port == 0 ? 3050 : e.Port;
                var userId = string.IsNullOrWhiteSpace(e.UserId) ? "SYSDBA" : e.UserId;
                var password = e.Password ?? "";
                var dialect = e.Dialect == 0 ? 3 : e.Dialect;
                
                FileLogger.Connection($"Parámetros de conexión periférica:");
                FileLogger.Connection($"  - DataSource: {dataSource}");
                FileLogger.Connection($"  - Database: {e.Database}");
                FileLogger.Connection($"  - Port: {port}");
                FileLogger.Connection($"  - UserID: {userId}");
                FileLogger.Connection($"  - Password: {(string.IsNullOrEmpty(password) ? "[VACÍA]" : "[CONFIGURADA]")}");
                FileLogger.Connection($"  - Dialect: {dialect}");
                
                var csb = new FbConnectionStringBuilder
                {
                    DataSource = dataSource,
                    Database = e.Database,
                    Port = port,
                    UserID = userId,
                    Password = password,
                    Dialect = dialect,
                    Charset = "UTF8",
                    Pooling = true
                };
                
                var connectionString = csb.ToString();
                FileLogger.Info("Cadena de conexión periférica construida exitosamente");
                
                return connectionString;
            }
            catch (Exception ex)
            {
                FileLogger.Error($"Error construyendo conexión periférica para empresa {e?.Nombre ?? "NULL"}", ex);
                throw;
            }
        }
    }
}

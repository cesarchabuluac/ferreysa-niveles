using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Niveles.Infraestructura
{
    /// <summary>
    /// Centraliza acceso a datos a la base periférica (usa pooling, abre/cierra por operación).
    /// </summary>
    public sealed class RepositoryBase
    {
        public string ConnStr
        {
            get
            {
                if (string.IsNullOrEmpty(AppSession.PeripheralConnectionString))
                    throw new InvalidOperationException("Sesión no activa.");
                return AppSession.PeripheralConnectionString;
            }
        }

        public FbConnection CreateConn() => new FbConnection(ConnStr);

        public FbCommand CreateCmd(FbConnection conn, string sql, params FbParameter[] parameters)
        {
            var cmd = new FbCommand(sql, conn);
            if (parameters != null && parameters.Length > 0) cmd.Parameters.AddRange(parameters);
            return cmd;
        }

        // SELECT -> DataTable
        public DataTable QueryTable(string sql, params FbParameter[] parameters)
        {
            using (var conn = CreateConn())
            using (var cmd = CreateCmd(conn, sql, parameters))
            using (var da = new FbDataAdapter(cmd))
            {
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // SELECT escalar -> T
        public T QueryScalar<T>(string sql, params FbParameter[] parameters)
        {
            using (var conn = CreateConn())
            {
                conn.Open();
                using (var cmd = CreateCmd(conn, sql, parameters))
                {
                    var obj = cmd.ExecuteScalar();
                    if (obj == null || obj == DBNull.Value) return default(T);
                    return (T)Convert.ChangeType(obj, typeof(T));
                }
            }
        }

        // INSERT/UPDATE/DELETE
        public int Execute(string sql, params FbParameter[] parameters)
        {
            using (var conn = CreateConn())
            {
                conn.Open();
                using (var cmd = CreateCmd(conn, sql, parameters))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // Transacciones
        public void ExecuteInTransaction(Action<FbConnection, FbTransaction> work)
        {
            if (work == null) throw new ArgumentNullException("work");
            using (var conn = CreateConn())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        work(conn, tx);
                        tx.Commit();
                    }
                    catch
                    {
                        try { tx.Rollback(); } catch { }
                        throw;
                    }
                }
            }
        }

        // Helper de parámetros
        public FbParameter Param(string name, FbDbType type, object value)
        {
            var p = new FbParameter(name, type) { Value = value ?? DBNull.Value };
            return p;
        }
    }
}


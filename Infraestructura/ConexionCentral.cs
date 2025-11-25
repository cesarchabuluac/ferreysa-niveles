// File: /Vales.Infraestructura/ConexionCentral.cs
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;
using Niveles.Domain;
using Niveles.Helpers;

namespace Niveles.Infraestructura
{
    public sealed class ConexionCentral : IDisposable
    {
        private readonly FbConnection _conn;

        public ConexionCentral()
        {
            try
            {
                FileLogger.Separator("CONEXIÓN CENTRAL");
                FileLogger.Info("Iniciando conexión a base de datos central");
                
                // Obtener cadena de conexión ajustada según el entorno
                var cs = NetworkHelper.GetCentralConnectionString();

                if (string.IsNullOrEmpty(cs))
                {
                    FileLogger.Error("No se pudo obtener cadena de conexión válida");
                    throw new InvalidOperationException("No se pudo obtener cadena de conexión válida.");
                }

                FileLogger.Connection("Cadena de conexión obtenida, intentando conectar...");
                _conn = new FbConnection(cs);
                _conn.Open();
                FileLogger.Info("Conexión a base central establecida exitosamente");
            }
            catch (Exception ex)
            {
                FileLogger.Error("Error estableciendo conexión central", ex);
                throw;
            }
        }

        public async Task<List<Empresa>> GetEmpresasAsync()
        {
            const string sql = @"
                SELECT 
                    IDEMPRESA, NOMBRE, DATASOURCE, DATABASE, PORT, USERID, PASSWORD, DIALECT,
                    IDCIA, ALIAS, DIRECCION, TELEFONO, RUTALOGO, STATUS, ID_PADRE, CONTROL_TOTAL,
                    ES_REMOTO, LOGIN, SYNC
                FROM WEB_EMPRESAS
                WHERE STATUS = 1 AND LOGIN = 1
                ORDER BY NOMBRE";

            var list = new List<Empresa>();

            using (var cmd = new FbCommand(sql, _conn))
            using (DbDataReader rdr = await cmd.ExecuteReaderAsync())
            {
                while (await rdr.ReadAsync())
                {
                    list.Add(new Empresa
                    {
                        IdEmpresa = rdr.GetInt32(0),
                        Nombre = rdr.IsDBNull(1) ? "" : rdr.GetString(1),
                        DataSource = rdr.IsDBNull(2) ? "" : rdr.GetString(2),
                        Database = rdr.IsDBNull(3) ? "" : rdr.GetString(3),
                        Port = rdr.IsDBNull(4) ? (short)3050 : rdr.GetInt16(4),
                        UserId = rdr.IsDBNull(5) ? "" : rdr.GetString(5),
                        Password = rdr.IsDBNull(6) ? "" : rdr.GetString(6),
                        Dialect = rdr.IsDBNull(7) ? 3 : rdr.GetInt32(7),
                        IdCia = rdr.IsDBNull(8) ? (short?)null : rdr.GetInt16(8),
                        Alias = rdr.IsDBNull(9) ? "" : rdr.GetString(9),
                        Direccion = rdr.IsDBNull(10) ? "" : rdr.GetString(10),
                        Telefono = rdr.IsDBNull(11) ? "" : rdr.GetString(11),
                        RutaLogo = rdr.IsDBNull(12) ? "" : rdr.GetString(12),
                        Status = rdr.IsDBNull(13) ? (short)1 : rdr.GetInt16(13),
                        IdPadre = rdr.IsDBNull(14) ? (int?)null : rdr.GetInt32(14),
                        ControlTotal = !rdr.IsDBNull(15) && rdr.GetBoolean(15),
                        EsRemoto = !rdr.IsDBNull(16) && rdr.GetBoolean(16),
                        Login = rdr.IsDBNull(17) ? (short)1 : rdr.GetInt16(17),
                        Sync = rdr.IsDBNull(18) ? (short)0 : rdr.GetInt16(18),
                    });
                }
            }

            return list;
        }

        // Opción sincrónica (por si tu proveedor no maneja bien async en .NET Framework)
        public List<Empresa> GetEmpresas()
        {
            const string sql = @"
                SELECT 
                    IDEMPRESA, NOMBRE, DATASOURCE, DATABASE, PORT, USERID, PASSWORD, DIALECT,
                    IDCIA, ALIAS, DIRECCION, TELEFONO, RUTALOGO, STATUS, ID_PADRE, CONTROL_TOTAL,
                    ES_REMOTO, LOGIN, SYNC
                FROM WEB_EMPRESAS
                WHERE STATUS = 1
                ORDER BY NOMBRE";

            var list = new List<Empresa>();

            using (var cmd = new FbCommand(sql, _conn))
            using (var rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    list.Add(new Empresa
                    {
                        IdEmpresa = rdr.GetInt32(0),
                        Nombre = rdr.IsDBNull(1) ? "" : rdr.GetString(1),
                        DataSource = rdr.IsDBNull(2) ? "" : rdr.GetString(2),
                        Database = rdr.IsDBNull(3) ? "" : rdr.GetString(3),
                        Port = rdr.IsDBNull(4) ? (short)3050 : rdr.GetInt16(4),
                        UserId = rdr.IsDBNull(5) ? "" : rdr.GetString(5),
                        Password = rdr.IsDBNull(6) ? "" : rdr.GetString(6),
                        Dialect = rdr.IsDBNull(7) ? 3 : rdr.GetInt32(7),
                        IdCia = rdr.IsDBNull(8) ? (short?)null : rdr.GetInt16(8),
                        Alias = rdr.IsDBNull(9) ? "" : rdr.GetString(9),
                        Direccion = rdr.IsDBNull(10) ? "" : rdr.GetString(10),
                        Telefono = rdr.IsDBNull(11) ? "" : rdr.GetString(11),
                        RutaLogo = rdr.IsDBNull(12) ? "" : rdr.GetString(12),
                        Status = rdr.IsDBNull(13) ? (short)1 : rdr.GetInt16(13),
                        IdPadre = rdr.IsDBNull(14) ? (int?)null : rdr.GetInt32(14),
                        ControlTotal = !rdr.IsDBNull(15) && rdr.GetBoolean(15),
                        EsRemoto = !rdr.IsDBNull(16) && rdr.GetBoolean(16),
                        Login = rdr.IsDBNull(17) ? (short)1 : rdr.GetInt16(17),
                        Sync = rdr.IsDBNull(18) ? (short)0 : rdr.GetInt16(18),
                    });
                }
            }

            return list;
        }

        public void Dispose()
        {
            // Por qué: asegurar liberación de recursos nativos del provider.
            try { if (_conn != null) _conn.Dispose(); } catch { }
        }
    }
}

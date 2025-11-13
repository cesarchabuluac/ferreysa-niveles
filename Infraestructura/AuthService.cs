// File: /Vales.Infraestructura/AuthService.cs  (C# 7 compatible)
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;
using Vales.Domain;

namespace Vales.Infraestructura
{
    public sealed class AuthService
    {
        // Ajusta tabla/columnas según tu esquema periférico.
        private const string SqlLogin = @"
            SELECT ID, NOMBRE_USUARIO, NOMBRE_COMPLETO, ACTIVO
            FROM USUARIOS
            WHERE UPPER(NOMBRE_USUARIO) = UPPER(@user)
              AND CLAVE = @pass
              AND (ACTIVO IS NULL OR ACTIVO <> 0)
            ROWS 1";

        /// <summary>
        /// Retorna un Usuario si las credenciales son válidas; de lo contrario null.
        /// </summary>
        public async Task<Usuario> AuthenticateAsync(string peripheralConnStr, string user, string pass)
        {
            using (var conn = new FbConnection(peripheralConnStr))
            {
                await conn.OpenAsync();

                using (var cmd = new FbCommand(SqlLogin, conn))
                {
                    cmd.Parameters.Add(new FbParameter("@user", FbDbType.VarChar) { Value = user });
                    cmd.Parameters.Add(new FbParameter("@pass", FbDbType.VarChar) { Value = pass });

                    using (DbDataReader rdr = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                    {
                        if (await rdr.ReadAsync())
                        {
                            var u = new Usuario();
                            u.Id = rdr.GetInt32(0);
                            u.NombreUsuario = rdr.IsDBNull(1) ? user : rdr.GetString(1);
                            u.NombreCompleto = rdr.IsDBNull(2) ? "" : rdr.GetString(2);
                            u.Activo = rdr.IsDBNull(3) || rdr.GetBoolean(3);
                            return u;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Versión sincrónica (útil si no deseas async en .NET Framework).
        /// </summary>
        public Usuario Authenticate(string peripheralConnStr, string user, string pass)
        {
            using (var conn = new FbConnection(peripheralConnStr))
            {
                conn.Open();

                using (var cmd = new FbCommand(SqlLogin, conn))
                {
                    cmd.Parameters.Add(new FbParameter("@user", FbDbType.VarChar) { Value = user });
                    cmd.Parameters.Add(new FbParameter("@pass", FbDbType.VarChar) { Value = pass });

                    using (var rdr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (rdr.Read())
                        {
                            var u = new Usuario();
                            u.Id = rdr.GetInt32(0);
                            u.NombreUsuario = rdr.IsDBNull(1) ? user : rdr.GetString(1);
                            u.NombreCompleto = rdr.IsDBNull(2) ? "" : rdr.GetString(2);
                            u.Activo = rdr.IsDBNull(3) || rdr.GetBoolean(3);
                            return u;
                        }
                    }
                }
            }

            return null;
        }
    }
}

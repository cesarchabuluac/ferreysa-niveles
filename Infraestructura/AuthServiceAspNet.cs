// File: /Vales.Infraestructura/AuthServiceAspNet.cs   (C# 7 & .NET Framework)
// Autenticación contra AspNetUsers (Identity v2) en SQL Server.
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Niveles.Domain;

namespace Niveles.Infraestructura
{
    public sealed class AuthServiceAspNet
    {
        private const string SqlFindUser = @"
            SELECT TOP(1) Id, UserName, NormalizedUserName, PasswordHash, Email, PhoneNumber, LockoutEndDateUtc, AccessFailedCount, LockoutEnabled
            FROM AspNetUsers
            WHERE NormalizedUserName = UPPER(@user)";

        private readonly string _identitySqlServerConnStr;

        public AuthServiceAspNet(string identitySqlServerConnStr)
        {
            if (string.IsNullOrEmpty(identitySqlServerConnStr))
                throw new ArgumentException("ConnectionString de Identity vacío.", nameof(identitySqlServerConnStr));
            _identitySqlServerConnStr = identitySqlServerConnStr;
        }

        /// <summary>
        /// Valida usuario/clave contra AspNetUsers. Retorna Usuario (propio del dominio) o null.
        /// </summary>
        public async Task<Usuario> AuthenticateAsync(string user, string password)
        {
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrEmpty(password))
                return null;

            using (var conn = new SqlConnection(_identitySqlServerConnStr))
            {
                await conn.OpenAsync();

                using (var cmd = new SqlCommand(SqlFindUser, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@user", SqlDbType.NVarChar, 256) { Value = user });

                    using (var rdr = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                    {
                        if (!await rdr.ReadAsync())
                            return null;

                        // Campos mínimos
                        var userName = rdr.IsDBNull(1) ? user : rdr.GetString(1);
                        var passwordHash = rdr.IsDBNull(3) ? null : rdr.GetString(3);
                        var lockoutEnabled = !rdr.IsDBNull(8) && rdr.GetBoolean(8);

                        if (string.IsNullOrEmpty(passwordHash))
                            return null;

                        // Verificación con Identity v2
                        var hasher = new PasswordHasher(); // de Microsoft.AspNet.Identity.Core
                        var result = hasher.VerifyHashedPassword(passwordHash, password);

                        if (result == PasswordVerificationResult.Failed)
                            return null;

                        // Si LockoutEnabled y tienes LockoutEndDateUtc, puedes bloquear aquí (opcional)
                        // var lockoutEndUtc = rdr.IsDBNull(6) ? (DateTime?)null : rdr.GetDateTime(6);
                        // if (lockoutEnabled && lockoutEndUtc != null && lockoutEndUtc > DateTime.UtcNow) return null;

                        var u = new Usuario();
                        u.Id = 0; // si necesitas mapear a tu Id local, ajusta aquí
                        u.NombreUsuario = userName;
                        u.NombreCompleto = userName; // o mapea de otra tabla si la tienes
                        u.Activo = true;
                        return u;
                    }
                }
            }
        }

        /// <summary>
        /// Versión sincrónica.
        /// </summary>
        public Usuario Authenticate(string user, string password)
        {
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrEmpty(password))
                return null;

            using (var conn = new SqlConnection(_identitySqlServerConnStr))
            {
                conn.Open();

                using (var cmd = new SqlCommand(SqlFindUser, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("@user", SqlDbType.NVarChar, 256) { Value = user });

                    using (var rdr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (!rdr.Read())
                            return null;

                        var userName = rdr.IsDBNull(1) ? user : rdr.GetString(1);
                        var passwordHash = rdr.IsDBNull(3) ? null : rdr.GetString(3);
                        var lockoutEnabled = !rdr.IsDBNull(8) && rdr.GetBoolean(8);

                        if (string.IsNullOrEmpty(passwordHash))
                            return null;

                        var hasher = new PasswordHasher();
                        var result = hasher.VerifyHashedPassword(passwordHash, password);

                        if (result == PasswordVerificationResult.Failed)
                            return null;

                        var u = new Usuario();
                        u.Id = 0;
                        u.NombreUsuario = userName;
                        u.NombreCompleto = userName;
                        u.Activo = true;
                        return u;
                    }
                }
            }
        }
    }
}

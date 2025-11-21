// File: /Vales.Infraestructura/AuthServiceIdentityFirebird.cs  (.NET Framework 4.8, C# 7)
using System;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.AspNetCore.Identity; // Paquete NuGet: Microsoft.AspNetCore.Identity
using Vales.Domain;

namespace Vales.Infraestructura
{
    /// <summary>
    /// Autentica contra "AspNetUsers" en CENTRAL.FDB (Firebird) usando ASP.NET Core Identity (hash "AQAAAA...").
    /// Requiere: Install-Package Microsoft.AspNetCore.Identity -Version 2.2.0
    /// </summary>
    public sealed class AuthServiceIdentityFirebird
    {
        private const string SqlFindUser = @"
            SELECT FIRST 1 
                   ""Id"", ""UserName"", ""PasswordHash"", ""LockoutEnabled"", ""LockoutEnd"", ""ESTATUS"", ""FullName""
            FROM ""AspNetUsers""
            WHERE ""NormalizedUserName"" = UPPER(@user)";

        private readonly string _centralConnStr;
        private readonly PasswordHasher<object> _hasher = new PasswordHasher<object>();

        public AuthServiceIdentityFirebird()
        {
            var cs = ConfigurationManager.ConnectionStrings["CentralDb"] != null
                ? ConfigurationManager.ConnectionStrings["CentralDb"].ConnectionString
                : null;
            if (string.IsNullOrEmpty(cs))
                throw new InvalidOperationException("Falta 'CentralDb' en App.config.");
            _centralConnStr = cs;
        }

        public async Task<Usuario> AuthenticateAsync(string user, string password)
        {
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrEmpty(password))
                return null;

            using (var conn = new FbConnection(_centralConnStr))
            {
                await conn.OpenAsync();

                using (var cmd = new FbCommand(SqlFindUser, conn))
                {
                    cmd.Parameters.Add(new FbParameter("@user", FbDbType.VarChar) { Value = user });

                    using (var rdr = await cmd.ExecuteReaderAsync(CommandBehavior.SingleRow))
                    {
                        if (!await rdr.ReadAsync())
                            return null;

                        var userName = rdr.IsDBNull(1) ? user : rdr.GetString(1);
                        var passwordHash = rdr.IsDBNull(2) ? null : rdr.GetString(2);
                        var lockoutEnabled = !rdr.IsDBNull(3) && rdr.GetBoolean(3);
                        var lockoutEndStr = rdr.IsDBNull(4) ? null : rdr.GetString(4);
                        var estatus = rdr.IsDBNull(5) ? true : rdr.GetBoolean(5);
                        var fullName = rdr.IsDBNull(6) ? userName : rdr.GetString(6);

                        if (!estatus) return null;

                        if (lockoutEnabled && !string.IsNullOrEmpty(lockoutEndStr))
                        {
                            DateTimeOffset dt;
                            if (DateTimeOffset.TryParse(lockoutEndStr, out dt) && dt > DateTimeOffset.UtcNow)
                                return null;
                        }

                        if (string.IsNullOrEmpty(passwordHash))
                            return null;

                        var res = _hasher.VerifyHashedPassword(null, passwordHash, password);
                        if (res == PasswordVerificationResult.Failed)
                            return null;

                        var u = new Usuario();
                        u.Id = 0; // ajusta si mapeas a otro Id
                        u.NombreUsuario = userName;
                        u.NombreCompleto = fullName;
                        u.Activo = true;
                        return u;
                    }
                }
            }
        }

        // Opción sincrónica si prefieres evitar async en WinForms
        public Usuario Authenticate(string user, string password)
        {
            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrEmpty(password))
                return null;

            using (var conn = new FbConnection(_centralConnStr))
            {
                conn.Open();

                using (var cmd = new FbCommand(SqlFindUser, conn))
                {
                    cmd.Parameters.Add(new FbParameter("@user", FbDbType.VarChar) { Value = user });

                    using (var rdr = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        if (!rdr.Read())
                            return null;

                        var userName = rdr.IsDBNull(1) ? user : rdr.GetString(1);
                        var passwordHash = rdr.IsDBNull(2) ? null : rdr.GetString(2);
                        var lockoutEnabled = !rdr.IsDBNull(3) && rdr.GetBoolean(3);
                        var lockoutEndStr = rdr.IsDBNull(4) ? null : rdr.GetString(4);
                        var estatus = rdr.IsDBNull(5) ? true : rdr.GetBoolean(5);
                        var fullName = rdr.IsDBNull(6) ? userName : rdr.GetString(6);

                        if (!estatus) return null;

                        if (lockoutEnabled && !string.IsNullOrEmpty(lockoutEndStr))
                        {
                            DateTimeOffset dt;
                            if (DateTimeOffset.TryParse(lockoutEndStr, out dt) && dt > DateTimeOffset.UtcNow)
                                return null;
                        }

                        if (string.IsNullOrEmpty(passwordHash))
                            return null;

                        var res = _hasher.VerifyHashedPassword(null, passwordHash, password);
                        if (res == PasswordVerificationResult.Failed)
                            return null;

                        var u = new Usuario();
                        u.Id = 0;
                        u.NombreUsuario = userName;
                        u.NombreCompleto = fullName;
                        u.Activo = true;
                        return u;
                    }
                }
            }
        }
    }
}

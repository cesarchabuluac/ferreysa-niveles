// File: /Vales.Infraestructura/AuthServiceIdentityFirebird.cs  (.NET Framework 4.8, C# 7)
using System;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.AspNetCore.Identity; // Paquete NuGet: Microsoft.AspNetCore.Identity
using Niveles.Domain;
using Niveles.Helpers;

namespace Niveles.Infraestructura
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
            try
            {
                FileLogger.Info("Inicializando AuthServiceIdentityFirebird");
                
                // Usar NetworkHelper para obtener la cadena correcta según el entorno
                _centralConnStr = NetworkHelper.GetCentralConnectionString();
                
                if (string.IsNullOrEmpty(_centralConnStr))
                {
                    FileLogger.Error("No se pudo obtener cadena de conexión central válida");
                    throw new InvalidOperationException("No se pudo obtener cadena de conexión central válida.");
                }
                
                FileLogger.Connection("Cadena de conexión central configurada para autenticación");
            }
            catch (Exception ex)
            {
                FileLogger.Error("Error inicializando AuthServiceIdentityFirebird", ex);
                throw;
            }
        }

        public async Task<Usuario> AuthenticateAsync(string user, string password)
        {
            try
            {
                FileLogger.Separator("AUTENTICACIÓN");
                FileLogger.Info($"Iniciando autenticación para usuario: {user}");
                
                if (string.IsNullOrWhiteSpace(user) || string.IsNullOrEmpty(password))
                {
                    FileLogger.Warning("Usuario o contraseña vacíos en autenticación");
                    return null;
                }

                FileLogger.Connection("Abriendo conexión para autenticación...");
                using (var conn = new FbConnection(_centralConnStr))
                {
                    await conn.OpenAsync();
                    FileLogger.Info("Conexión de autenticación establecida exitosamente");

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
                        
                        FileLogger.Info($"Autenticación exitosa para usuario: {userName}");
                        return u;
                    }
                }
                }
            }
            catch (Exception ex)
            {
                FileLogger.Error($"Error durante autenticación para usuario: {user}", ex);
                throw;
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

using System;
using System.Configuration;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Niveles.Helpers
{
    /// <summary>
    /// Helper para detectar entorno de red y ajustar cadenas de conexión
    /// </summary>
    public static class NetworkHelper
    {
        private const string INTRANET_IP = "192.168.1.2";
        private const string INTRANET_PASSWORD = "masterkey";
        private const string REMOTE_PASSWORD = "masterkey190652";

        /// <summary>
        /// Obtiene la cadena de conexión ajustada según el entorno
        /// </summary>
        /// <returns>Cadena de conexión configurada para el entorno actual</returns>
        public static async Task<string> GetCentralConnectionStringAsync()
        {
            try
            {
                // Obtener cadena base del app.config
                var baseConnectionString = ConfigurationManager.ConnectionStrings["CentralDb"]?.ConnectionString;
                
                if (string.IsNullOrEmpty(baseConnectionString))
                {
                    throw new InvalidOperationException("Falta 'CentralDb' en App.config.");
                }

                // Detectar si estamos en intranet
                bool isIntranet = await IsIntranetAsync().ConfigureAwait(false);
                
                if (isIntranet)
                {
                    Debug.WriteLine("ENTORNO DETECTADO: INTRANET - Usando contraseña local");
                    // En intranet, usar contraseña local (por defecto)
                    return ReplacePassword(baseConnectionString, INTRANET_PASSWORD);
                }
                else
                {
                    Debug.WriteLine("ENTORNO DETECTADO: REMOTO - Cambiando a contraseña remota");
                    // En remoto, cambiar a contraseña remota
                    return ReplacePassword(baseConnectionString, REMOTE_PASSWORD);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error detectando entorno: {ex.Message}");
                // En caso de error, usar la cadena base
                return ConfigurationManager.ConnectionStrings["CentralDb"]?.ConnectionString ?? "";
            }
        }

        /// <summary>
        /// Detecta si estamos en la red intranet
        /// </summary>
        /// <returns>True si estamos en intranet</returns>
        private static async Task<bool> IsIntranetAsync()
        {
            try
            {
                Debug.WriteLine($"Verificando conectividad a intranet: {INTRANET_IP}");
                
                using (var ping = new Ping())
                {
                    var reply = await ping.SendPingAsync(INTRANET_IP, 3000).ConfigureAwait(false);
                    bool isIntranet = reply.Status == IPStatus.Success;
                    
                    Debug.WriteLine($"Ping a {INTRANET_IP}: {reply.Status} - Intranet: {isIntranet}");
                    return isIntranet;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error verificando intranet: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Reemplaza la contraseña en una cadena de conexión
        /// </summary>
        /// <param name="connectionString">Cadena de conexión original</param>
        /// <param name="newPassword">Nueva contraseña</param>
        /// <returns>Cadena de conexión con contraseña actualizada</returns>
        private static string ReplacePassword(string connectionString, string newPassword)
        {
            try
            {
                FileLogger.Debug($"Reemplazando contraseña en cadena de conexión");
                FileLogger.Debug($"Cadena original: {connectionString}");
                FileLogger.Debug($"Nueva contraseña: {newPassword}");
                
                // Buscar y reemplazar el parámetro password
                var result = connectionString;
                
                // Patrones posibles para password
                var patterns = new[]
                {
                    "password=masterkey190652",
                    "password=masterkey",
                    "Password=masterkey190652", 
                    "Password=masterkey"
                };

                bool found = false;
                foreach (var pattern in patterns)
                {
                    if (result.Contains(pattern))
                    {
                        result = result.Replace(pattern, $"password={newPassword}");
                        FileLogger.Info($"Contraseña cambiada de '{pattern}' a 'password={newPassword}'");
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    FileLogger.Warning("No se encontró patrón de contraseña para reemplazar");
                }

                FileLogger.Debug($"Cadena final: {result}");
                return result;
            }
            catch (Exception ex)
            {
                FileLogger.Error("Error reemplazando contraseña", ex);
                return connectionString;
            }
        }

        /// <summary>
        /// Versión completamente sincrónica (sin async/await)
        /// </summary>
        /// <returns>Cadena de conexión configurada</returns>
        public static string GetCentralConnectionString()
        {
            try
            {
                FileLogger.Separator("DETECCIÓN DE ENTORNO");
                FileLogger.Info("Iniciando detección de entorno de red");
                
                // Obtener cadena base del app.config
                var baseConnectionString = ConfigurationManager.ConnectionStrings["CentralDb"]?.ConnectionString;
                
                if (string.IsNullOrEmpty(baseConnectionString))
                {
                    throw new InvalidOperationException("Falta 'CentralDb' en App.config.");
                }

                // Detectar si estamos en intranet (versión sincrónica)
                bool isIntranet = IsIntranetSync();
                
                if (isIntranet)
                {
                    FileLogger.Info("ENTORNO DETECTADO: INTRANET - Usando contraseña local");
                    var result = ReplacePassword(baseConnectionString, INTRANET_PASSWORD);
                    FileLogger.Connection($"Cadena de conexión configurada para INTRANET");
                    return result;
                }
                else
                {
                    FileLogger.Info("ENTORNO DETECTADO: REMOTO - Cambiando a contraseña remota");
                    var result = ReplacePassword(baseConnectionString, REMOTE_PASSWORD);
                    FileLogger.Connection($"Cadena de conexión configurada para REMOTO");
                    return result;
                }
            }
            catch (Exception ex)
            {
                FileLogger.Error("Error en GetCentralConnectionString", ex);
                
                // Fallback: usar cadena base del config
                var fallback = ConfigurationManager.ConnectionStrings["CentralDb"]?.ConnectionString ?? "";
                FileLogger.Warning($"Usando fallback: {(!string.IsNullOrEmpty(fallback) ? "Cadena encontrada" : "Cadena vacía")}");
                return fallback;
            }
        }

        /// <summary>
        /// Versión sincrónica de detección de intranet (sin async/await)
        /// </summary>
        /// <returns>True si estamos en intranet</returns>
        private static bool IsIntranetSync()
        {
            try
            {
                FileLogger.Debug($"Verificando conectividad a intranet: {INTRANET_IP}");
                
                using (var ping = new Ping())
                {
                    FileLogger.Debug($"Enviando ping sincrónico a {INTRANET_IP}...");
                    
                    // Usar método sincrónico Send en lugar de SendAsync
                    var reply = ping.Send(INTRANET_IP, 3000);
                    
                    FileLogger.Debug($"Respuesta recibida - Estado: {reply.Status}, Tiempo: {reply.RoundtripTime}ms");
                    
                    bool isIntranet = reply.Status == IPStatus.Success;
                    
                    FileLogger.Info($"Resultado ping: {(isIntranet ? "INTRANET DISPONIBLE" : "INTRANET NO DISPONIBLE")}");
                    
                    return isIntranet;
                }
            }
            catch (PingException pex)
            {
                FileLogger.Warning($"Error de ping: {pex.Message} - Asumiendo REMOTO");
                return false;
            }
            catch (Exception ex)
            {
                FileLogger.Error("Error general en verificación de intranet - Asumiendo REMOTO", ex);
                return false;
            }
        }
    }
}

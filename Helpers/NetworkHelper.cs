using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

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
        /// Lee el entorno desde UpdateConfig.json
        /// </summary>
        /// <returns>El entorno configurado ("local", "produccion", etc.)</returns>
        private static string GetEnvironmentFromConfig()
        {
            try
            {
                FileLogger.Debug("Leyendo entorno desde UpdateConfig.json");
                
                // Buscar UpdateConfig.json en múltiples ubicaciones
                string[] possiblePaths = {
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Niveles", "UpdateConfig.json"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UpdateConfig.json"),
                    Path.Combine(Directory.GetCurrentDirectory(), "UpdateConfig.json")
                };

                string configPath = null;
                foreach (string path in possiblePaths)
                {
                    if (File.Exists(path))
                    {
                        configPath = path;
                        FileLogger.Debug($"UpdateConfig.json encontrado en: {path}");
                        break;
                    }
                }

                if (configPath == null)
                {
                    FileLogger.Warning("UpdateConfig.json no encontrado, usando 'local' por defecto");
                    return "local";
                }

                // Leer y parsear el JSON
                string jsonContent = File.ReadAllText(configPath);
                var config = JObject.Parse(jsonContent);
                
                string environment = config["AppSettings"]?["Environment"]?.ToString() ?? "local";
                FileLogger.Info($"Entorno leído desde config: '{environment}'");
                
                return environment.ToLower();
            }
            catch (Exception ex)
            {
                FileLogger.Error("Error leyendo entorno desde config, usando 'local' por defecto", ex);
                return "local";
            }
        }

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

                // Leer entorno desde configuración
                string environment = GetEnvironmentFromConfig();
                
                // Configurar cadena completa según el entorno
                return ConfigureConnectionString(baseConnectionString, environment);
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
        /// Configura la cadena de conexión completa según el entorno
        /// </summary>
        /// <param name="connectionString">Cadena de conexión original</param>
        /// <param name="environment">Entorno desde configuración</param>
        /// <returns>Cadena de conexión configurada para el entorno</returns>
        private static string ConfigureConnectionString(string connectionString, string environment)
        {
            try
            {
                FileLogger.Debug($"Configurando cadena de conexión para entorno: '{environment}'");
                FileLogger.Debug($"Cadena original: {connectionString}");
                
                var result = connectionString;
                
                if (environment == "local")
                {
                    // DESARROLLO: localhost + contraseña de desarrollo
                    result = ReplaceDataSource(result, "localhost");
                    result = ReplacePassword(result, REMOTE_PASSWORD);
                    FileLogger.Info($"Configurado para DESARROLLO: IP=localhost, Password={REMOTE_PASSWORD}");
                }
                else if (environment == "produccion")
                {
                    // PRODUCCIÓN: Detectar si estamos en red local o en nube
                    bool isLocalNetwork = IsIntranetSync();
                    
                    if (isLocalNetwork)
                    {
                        // En red local: usar IP específica
                        result = ReplaceDataSource(result, INTRANET_IP);
                        result = ReplacePassword(result, INTRANET_PASSWORD);
                        FileLogger.Info($"Configurado para PRODUCCIÓN LOCAL: IP={INTRANET_IP}, Password={INTRANET_PASSWORD}");
                    }
                    else
                    {
                        // En nube: usar localhost pero con contraseña de nube
                        result = ReplaceDataSource(result, "localhost");
                        result = ReplacePassword(result, REMOTE_PASSWORD);
                        FileLogger.Info($"Configurado para PRODUCCIÓN NUBE: IP=localhost, Password={REMOTE_PASSWORD}");
                    }
                }
                else
                {
                    FileLogger.Warning($"Entorno desconocido '{environment}', usando configuración por defecto");
                    // Por defecto, usar desarrollo
                    result = ReplaceDataSource(result, "localhost");
                    result = ReplacePassword(result, REMOTE_PASSWORD);
                }

                FileLogger.Debug($"Cadena final: {result}");
                return result;
            }
            catch (Exception ex)
            {
                FileLogger.Error("Error configurando cadena de conexión", ex);
                return connectionString;
            }
        }

        /// <summary>
        /// Reemplaza el data source en una cadena de conexión
        /// </summary>
        /// <param name="connectionString">Cadena de conexión original</param>
        /// <param name="newDataSource">Nuevo data source (IP o hostname)</param>
        /// <returns>Cadena de conexión con data source actualizado</returns>
        private static string ReplaceDataSource(string connectionString, string newDataSource)
        {
            try
            {
                var result = connectionString;
                
                // Patrones posibles para data source
                var patterns = new[]
                {
                    "data source=localhost",
                    "data source=192.168.1.2",
                    "Data Source=localhost",
                    "Data Source=192.168.1.2",
                    "server=localhost",
                    "server=192.168.1.2",
                    "Server=localhost",
                    "Server=192.168.1.2"
                };

                bool found = false;
                foreach (var pattern in patterns)
                {
                    if (result.Contains(pattern))
                    {
                        result = result.Replace(pattern, $"data source={newDataSource}");
                        FileLogger.Debug($"Data source cambiado de '{pattern}' a 'data source={newDataSource}'");
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    FileLogger.Warning("No se encontró patrón de data source para reemplazar");
                }

                return result;
            }
            catch (Exception ex)
            {
                FileLogger.Error("Error reemplazando data source", ex);
                return connectionString;
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

                // Leer entorno desde configuración
                string environment = GetEnvironmentFromConfig();
                
                // Configurar cadena completa según el entorno
                var result = ConfigureConnectionString(baseConnectionString, environment);
                FileLogger.Connection($"Cadena de conexión configurada para entorno '{environment}'");
                return result;
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

        /// <summary>
        /// Método de prueba para verificar la detección de entorno
        /// </summary>
        /// <returns>Información del entorno detectado</returns>
        public static string TestEnvironmentDetection()
        {
            try
            {
                FileLogger.Separator("PRUEBA DE DETECCIÓN DE ENTORNO");
                
                // Obtener cadena base
                var baseConnectionString = ConfigurationManager.ConnectionStrings["CentralDb"]?.ConnectionString;
                FileLogger.Info($"Cadena base del app.config: {baseConnectionString}");
                
                // Leer entorno desde configuración
                string environment = GetEnvironmentFromConfig();
                FileLogger.Info($"Entorno configurado: '{environment}'");
                
                // Si es producción, detectar red
                string networkInfo = "";
                if (environment == "produccion")
                {
                    bool isLocalNetwork = IsIntranetSync();
                    networkInfo = $"• Red detectada: {(isLocalNetwork ? "LOCAL" : "NUBE")}\n";
                    FileLogger.Info($"Detección de red para producción: {(isLocalNetwork ? "LOCAL" : "NUBE")}");
                }
                
                // Obtener cadena configurada
                var configuredString = GetCentralConnectionString();
                FileLogger.Info($"Cadena configurada: {configuredString}");
                
                // Crear resumen
                var summary = $"CONFIGURACIÓN DE ENTORNO:\n" +
                             $"• Environment en config: '{environment}'\n" +
                             networkInfo +
                             $"• Cadena configurada: {configuredString}";
                
                FileLogger.Info("Prueba de detección completada");
                return summary;
            }
            catch (Exception ex)
            {
                FileLogger.Error("Error en prueba de detección", ex);
                return $"ERROR: {ex.Message}";
            }
        }
    }
}

using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace Niveles.Helpers
{
    /// <summary>
    /// Helper para obtener información de versión de la aplicación
    /// </summary>
    public static class VersionHelper
    {
        /// <summary>
        /// Obtiene la versión actual de la aplicación desde el UpdateManager
        /// Si falla, usa Application.ProductVersion como fallback
        /// </summary>
        /// <returns>Versión actual de la aplicación</returns>
        public static string GetCurrentVersion()
        {
            try
            {
                // Intentar obtener versión desde UpdateManager (más precisa)
                var updateManager = new UpdateManager();
                string updateManagerVersion = updateManager.GetCurrentVersion();
                
                if (!string.IsNullOrEmpty(updateManagerVersion))
                {
                    Debug.WriteLine($"Versión obtenida desde UpdateManager: {updateManagerVersion}");
                    return updateManagerVersion;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al obtener versión desde UpdateManager: {ex.Message}");
            }

            // Fallback: usar Application.ProductVersion
            try
            {
                string productVersion = Application.ProductVersion;
                Debug.WriteLine($"Versión obtenida desde Application.ProductVersion: {productVersion}");
                return productVersion;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al obtener Application.ProductVersion: {ex.Message}");
            }

            // Último fallback: versión del ensamblado
            try
            {
                var assembly = Assembly.GetExecutingAssembly();
                var version = assembly.GetName().Version;
                string assemblyVersion = version?.ToString() ?? "1.0.0.0";
                Debug.WriteLine($"Versión obtenida desde Assembly: {assemblyVersion}");
                return assemblyVersion;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al obtener versión del Assembly: {ex.Message}");
                return "Desconocida";
            }
        }

        /// <summary>
        /// Obtiene información detallada de versión para soporte técnico
        /// </summary>
        /// <returns>Información completa de versión</returns>
        public static string GetDetailedVersionInfo()
        {
            try
            {
                var info = new System.Text.StringBuilder();
                
                // Versión principal
                info.AppendLine($"Versión: {GetCurrentVersion()}");
                
                // Información adicional para soporte
                try
                {
                    var updateManager = new UpdateManager();
                    info.AppendLine($"Versión UpdateManager: {updateManager.GetCurrentVersion()}");
                }
                catch { /* Ignorar errores */ }
                
                try
                {
                    info.AppendLine($"Versión Assembly: {Application.ProductVersion}");
                }
                catch { /* Ignorar errores */ }
                
                try
                {
                    var assembly = Assembly.GetExecutingAssembly();
                    info.AppendLine($"Fecha compilación: {GetBuildDate(assembly):yyyy-MM-dd HH:mm:ss}");
                }
                catch { /* Ignorar errores */ }
                
                return info.ToString().Trim();
            }
            catch (Exception ex)
            {
                return $"Error obteniendo información de versión: {ex.Message}";
            }
        }

        /// <summary>
        /// Obtiene la fecha de compilación del ensamblado
        /// </summary>
        /// <param name="assembly">Ensamblado</param>
        /// <returns>Fecha de compilación</returns>
        private static DateTime GetBuildDate(Assembly assembly)
        {
            try
            {
                var attribute = assembly.GetCustomAttribute<System.Reflection.AssemblyMetadataAttribute>();
                if (attribute != null && attribute.Key == "BuildDate")
                {
                    if (DateTime.TryParse(attribute.Value, out DateTime buildDate))
                        return buildDate;
                }
                
                // Fallback: usar fecha de modificación del archivo
                string location = assembly.Location;
                if (!string.IsNullOrEmpty(location) && System.IO.File.Exists(location))
                {
                    return System.IO.File.GetLastWriteTime(location);
                }
            }
            catch { /* Ignorar errores */ }
            
            return DateTime.Now;
        }

        /// <summary>
        /// Formatea la versión para mostrar al usuario
        /// </summary>
        /// <param name="includePrefix">Si incluir el prefijo "Versión"</param>
        /// <returns>Versión formateada</returns>
        public static string GetFormattedVersion(bool includePrefix = true)
        {
            string version = GetCurrentVersion();
            return includePrefix ? $"Versión {version}" : version;
        }

        public static string GetCurrentEnvironment()
        {
            // Intentar obtener versión desde UpdateManager (más precisa)
            var updateManager = new UpdateManager();
            return $"Entorno: {updateManager.GetCurrentEnvironment()}";
        }
    }
}

using System;
using System.IO;
using System.Threading;

namespace Niveles.Helpers
{
    /// <summary>
    /// Logger que escribe a archivo con thread-safety
    /// </summary>
    public static class FileLogger
    {
        private static readonly object _lockObject = new object();
        private static string _logFilePath;

        static FileLogger()
        {
            // Crear archivo de log en la carpeta de la aplicación
            var appPath = AppDomain.CurrentDomain.BaseDirectory;
            var logFileName = $"Niveles_Log_{DateTime.Now:yyyyMMdd}.txt";
            _logFilePath = Path.Combine(appPath, logFileName);
            
            // Escribir header inicial
            WriteToFile($"=== INICIO DE SESIÓN - {DateTime.Now:yyyy-MM-dd HH:mm:ss} ===");
        }

        /// <summary>
        /// Escribe un mensaje de información
        /// </summary>
        public static void Info(string message)
        {
            WriteLog("INFO", message);
        }

        /// <summary>
        /// Escribe un mensaje de error
        /// </summary>
        public static void Error(string message, Exception ex = null)
        {
            var fullMessage = message;
            if (ex != null)
            {
                fullMessage += $"\nExcepción: {ex.Message}\nStack Trace: {ex.StackTrace}";
            }
            WriteLog("ERROR", fullMessage);
        }

        /// <summary>
        /// Escribe un mensaje de debug
        /// </summary>
        public static void Debug(string message)
        {
            WriteLog("DEBUG", message);
        }

        /// <summary>
        /// Escribe un mensaje de warning
        /// </summary>
        public static void Warning(string message)
        {
            WriteLog("WARN", message);
        }

        /// <summary>
        /// Escribe información de conexión
        /// </summary>
        public static void Connection(string message)
        {
            WriteLog("CONN", message);
        }

        /// <summary>
        /// Método interno para escribir al log
        /// </summary>
        private static void WriteLog(string level, string message)
        {
            var logMessage = $"[{DateTime.Now:HH:mm:ss.fff}] [{level}] {message}";
            
            // También escribir a Debug para Visual Studio
            System.Diagnostics.Debug.WriteLine(logMessage);
            
            // Escribir a archivo
            WriteToFile(logMessage);
        }

        /// <summary>
        /// Escribe al archivo con thread-safety
        /// </summary>
        private static void WriteToFile(string message)
        {
            try
            {
                lock (_lockObject)
                {
                    File.AppendAllText(_logFilePath, message + Environment.NewLine);
                }
            }
            catch
            {
                // Si no puede escribir al archivo, al menos que aparezca en Debug
                System.Diagnostics.Debug.WriteLine($"[LOG ERROR] No se pudo escribir: {message}");
            }
        }

        /// <summary>
        /// Obtiene la ruta del archivo de log actual
        /// </summary>
        public static string GetLogFilePath()
        {
            return _logFilePath;
        }

        /// <summary>
        /// Escribe un separador para marcar secciones
        /// </summary>
        public static void Separator(string sectionName)
        {
            WriteToFile($"\n=== {sectionName} - {DateTime.Now:HH:mm:ss} ===");
        }
    }
}

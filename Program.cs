using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Niveles.Helpers;
using Niveles.UI;


namespace Niveles
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ═══════════════════════════════════════════════════════════
            // VERIFICAR ACTUALIZACIONES AL INICIAR
            // ═══════════════════════════════════════════════════════════
            try
            {
                FileLogger.Separator("=== INICIANDO VERIFICACIÓN DE ACTUALIZACIONES ===");
                FileLogger.Info("Iniciando verificación de actualizaciones al iniciar la aplicación.");
                FileLogger.Info($"Timestamp: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                
                var updateManager = new UpdateManager();
                FileLogger.Info("UpdateManager creado exitosamente.");

                // Ejecutar verificación en background thread para no bloquear
                var updateTask = Task.Run(() => 
                {
                    FileLogger.Separator("=== EJECUTANDO CheckAndUpdate() EN BACKGROUND ===");
                    try
                    {
                        updateManager.CheckAndUpdate();
                        FileLogger.Info("CheckAndUpdate() completado exitosamente.");
                    }
                    catch (Exception taskEx)
                    {
                        FileLogger.Error($"Error durante CheckAndUpdate() {taskEx}");                        
                        FileLogger.Info("Continuando ejecución normal tras error en actualización.");
                    }
                });
                
                FileLogger.Info("Task.Run iniciado para CheckAndUpdate()");
            }
            catch (Exception ex)
            {
                // Si falla la verificación de actualizaciones, continuar normalmente
                FileLogger.Error($"Error al verificar actualizaciones: {ex.Message}");
                FileLogger.Error($"StackTrace: {ex.StackTrace}");
            }

            using (var login = new FormAccess())
            {
                var result = login.ShowDialog();
                if (result == DialogResult.OK)
                {
                    Application.Run(new FormNiveles());
                }
            }
        }
    }
}

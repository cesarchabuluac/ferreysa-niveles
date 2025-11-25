using System;
using System.Windows.Forms;
using Niveles.Helpers;
using Niveles.UI;

namespace Niveles
{
    /// <summary>
    /// Clase para probar el sistema de actualización
    /// </summary>
    public static class TestUpdater
    {
        /// <summary>
        /// Prueba solo la conectividad FTP
        /// </summary>
        public static void TestFtpOnly()
        {
            try
            {
                var updateManager = new UpdateManager();
                bool ftpOk = updateManager.TestFtpConnection();
                
                MessageBox.Show(
                    ftpOk ? "✅ FTP funciona correctamente" : "❌ Problema con FTP - revisa el log",
                    "Prueba FTP",
                    MessageBoxButtons.OK,
                    ftpOk ? MessageBoxIcon.Information : MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en prueba FTP: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Prueba solo el FormUpdater (sin descarga real)
        /// </summary>
        public static void TestFormUpdaterOnly()
        {
            try
            {
                using (var form = new FormUpdater())
                {
                    form.Show();
                    
                    // Simular progreso
                    for (int i = 0; i <= 100; i += 10)
                    {
                        form.UpdateProgress(i, $"Simulando descarga... {i}%");
                        System.Threading.Thread.Sleep(500);
                        Application.DoEvents();
                    }
                    
                    form.SetCompleted(true, "Prueba completada exitosamente");
                    form.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en prueba FormUpdater: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Prueba completa del sistema de actualización
        /// </summary>
        public static void TestFullUpdate()
        {
            try
            {
                var updateManager = new UpdateManager();
                updateManager.CheckAndUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en prueba completa: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

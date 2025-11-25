using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpdaterModerno
{
    /// <summary>
    /// Programa principal del Updater moderno
    /// Reemplaza el Updater.exe de consola con UI gráfica
    /// </summary>
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal de la aplicación
        /// </summary>
        [STAThread]
        static async Task Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            try
            {
                // Crear y mostrar el formulario de instalación
                var installerForm = new FormUpdaterInstaller(args);
                
                // Mostrar el formulario
                installerForm.Show();
                
                // Iniciar la instalación de forma asíncrona
                var installTask = installerForm.StartInstallationAsync();
                
                // Ejecutar el formulario
                Application.Run(installerForm);
                
                // Esperar a que termine la instalación si aún no ha terminado
                await installTask;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error crítico en el instalador: {ex.Message}\n\nDetalles: {ex.StackTrace}",
                    "Error del Instalador",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}

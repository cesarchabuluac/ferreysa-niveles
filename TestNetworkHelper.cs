using System;
using System.Windows.Forms;
using Niveles.Helpers;

namespace Niveles
{
    /// <summary>
    /// Clase para probar el NetworkHelper y la detección de entorno
    /// </summary>
    public static class TestNetworkHelper
    {
        /// <summary>
        /// Prueba la detección de entorno y muestra el resultado
        /// </summary>
        public static void TestEnvironment()
        {
            try
            {
                string result = NetworkHelper.TestEnvironmentDetection();
                
                MessageBox.Show(
                    result,
                    "Prueba de Detección de Entorno",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error en prueba: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Obtiene y muestra la cadena de conexión configurada
        /// </summary>
        public static void ShowConnectionString()
        {
            try
            {
                string connectionString = NetworkHelper.GetCentralConnectionString();
                
                MessageBox.Show(
                    $"Cadena de conexión configurada:\n\n{connectionString}",
                    "Cadena de Conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error obteniendo cadena: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}

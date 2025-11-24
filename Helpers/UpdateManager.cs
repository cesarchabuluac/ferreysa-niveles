using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Deployment.Application;
using System.Reflection;
using FluentFTP;
using Newtonsoft.Json.Linq;

namespace Niveles.Helpers
{
    /// <summary>
    /// Administrador de actualizaciones automáticas con soporte FTP
    /// </summary>
    public class UpdateManager
    {
        private readonly string _configPath;
        private JObject _config;
        private string _ftpHost;
        private string _ftpUsername;
        private string _ftpPassword;
        private string _updatesPath;
        private string _versionFile;
        private string _updatePackage;
        private string _currentVersion;
        private string _updaterExeName;

        public UpdateManager()
        {
            // Buscar UpdateConfig.json en múltiples ubicaciones posibles
            _configPath = FindConfigFile();
            LoadConfiguration();
        }

        /// <summary>
        /// Busca el archivo UpdateConfig.json en diferentes ubicaciones
        /// </summary>
        private string FindConfigFile()
        {
            string[] possiblePaths;
            
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                // Para ClickOnce, buscar en múltiples ubicaciones
                possiblePaths = new string[]
                {
                    Path.Combine(ApplicationDeployment.CurrentDeployment.DataDirectory, "UpdateConfig.json"),
                    Path.Combine(Application.StartupPath, "UpdateConfig.json"),
                    Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Niveles", "UpdateConfig.json"),
                    Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "UpdateConfig.json")
                };
            }
            else
            {
                // Para instalación normal
                possiblePaths = new string[]
                {
                    Path.Combine(Application.StartupPath, "UpdateConfig.json"),
                    Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "UpdateConfig.json")
                };
            }

            foreach (string path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    return path;
                }
            }

            // Si no se encuentra, usar la ruta por defecto
            return Path.Combine(Application.StartupPath, "UpdateConfig.json");
        }

        /// <summary>
        /// Carga la configuración desde recursos embebidos
        /// </summary>
        private string LoadConfigFromEmbeddedResource()
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string resourceName = "Niveles.UpdateConfig.json";
                
                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    if (stream == null)
                        return null;
                        
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al cargar configuración desde recursos: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Carga la configuración desde UpdateConfig.json
        /// </summary>
        private void LoadConfiguration()
        {
            try
            {
                string json = null;
                
                // Intentar leer desde archivo físico primero
                if (File.Exists(_configPath))
                {
                    json = File.ReadAllText(_configPath);
                }
                else
                {
                    // Si no existe el archivo, intentar leer desde recursos embebidos
                    json = LoadConfigFromEmbeddedResource();
                    
                    if (string.IsNullOrEmpty(json))
                    {
                        string diagnosticInfo = $"No se encontró UpdateConfig.json en: {_configPath}\n\n";
                        diagnosticInfo += "Ubicaciones buscadas:\n";
                        
                        if (ApplicationDeployment.IsNetworkDeployed)
                        {
                            diagnosticInfo += $"- DataDirectory: {ApplicationDeployment.CurrentDeployment.DataDirectory}\n";
                            diagnosticInfo += $"- StartupPath: {Application.StartupPath}\n";
                            diagnosticInfo += $"- AppData: {Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Niveles")}\n";
                            diagnosticInfo += $"- ExecutablePath: {Path.GetDirectoryName(Application.ExecutablePath)}\n";
                        }
                        else
                        {
                            diagnosticInfo += $"- StartupPath: {Application.StartupPath}\n";
                            diagnosticInfo += $"- ExecutablePath: {Path.GetDirectoryName(Application.ExecutablePath)}\n";
                        }
                        diagnosticInfo += "- Recursos embebidos: No encontrado\n";
                        
                        throw new FileNotFoundException(diagnosticInfo);
                    }
                }

                _config = JObject.Parse(json);

                // FTP Settings
                _ftpHost = _config["FtpSettings"]["Host"].ToString();
                _ftpUsername = _config["FtpSettings"]["Username"].ToString();
                _ftpPassword = _config["FtpSettings"]["Password"].ToString();
                _updatesPath = _config["FtpSettings"]["UpdatesPath"].ToString();
                _versionFile = _config["FtpSettings"]["VersionFile"].ToString();
                _updatePackage = _config["FtpSettings"]["UpdatePackage"].ToString();

                // App Settings
                _currentVersion = _config["AppSettings"]["CurrentVersion"].ToString();
                _updaterExeName = _config["AppSettings"]["UpdaterExeName"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar configuración de actualizaciones: {ex.Message}",
                    "Error de Configuración", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Verifica si hay actualizaciones disponibles
        /// </summary>
        /// <returns>True si hay una actualización disponible</returns>
        public bool CheckForUpdates()
        {
            try
            {
                using (var client = new FtpClient(_ftpHost, _ftpUsername, _ftpPassword))
                {
                    client.Connect();

                    string remotePath = _updatesPath + _versionFile;
                    
                    // Descargar contenido del archivo de versión
                    using (var stream = client.OpenRead(remotePath))
                    using (var reader = new StreamReader(stream))
                    {
                        string remoteVersion = reader.ReadToEnd().Trim();

                        if (string.IsNullOrWhiteSpace(remoteVersion))
                        {
                            client.Disconnect();
                            return false;
                        }

                        Version current = new Version(_currentVersion);
                        Version remote = new Version(remoteVersion);

                        client.Disconnect();

                        return remote > current;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al verificar actualizaciones: {ex.Message}");
                return false;
            }
        }



        /// <summary>
        /// Descarga la actualización desde el servidor FTP
        /// </summary>
        /// <param name="progress">Callback para reportar progreso (0-100)</param>
        /// <returns>Ruta del archivo descargado o null si falla</returns>
        public string DownloadUpdate(IProgress<int> progress = null)
        {
            try
            {
                string tempPath = Path.Combine(Path.GetTempPath(), "NivelesUpdate");
                if (!Directory.Exists(tempPath))
                    Directory.CreateDirectory(tempPath);

                string localUpdatePath = Path.Combine(tempPath, _updatePackage);

                using (var client = new FtpClient(_ftpHost, _ftpUsername, _ftpPassword))
                {
                    client.Connect();

                    string remotePath = _updatesPath + _updatePackage;

                    // Reportar inicio de descarga
                    if (progress != null)
                        progress.Report(0);

                    // Descargar archivo usando el método básico de v53
                    // DownloadFile en v53 retorna void y lanza excepción si falla
                    client.DownloadFile(localUpdatePath, remotePath, FtpLocalExists.Overwrite);

                    // Reportar finalización
                    if (progress != null)
                        progress.Report(100);

                    client.Disconnect();

                    // Verificar que el archivo se descargó correctamente
                    if (File.Exists(localUpdatePath) && new FileInfo(localUpdatePath).Length > 0)
                    {
                        return localUpdatePath;
                    }
                    else
                    {
                        MessageBox.Show("Error al descargar la actualización.",
                            "Error de Descarga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al descargar actualización: {ex.Message}",
                    "Error de Descarga", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        /// <summary>
        /// Inicia el proceso de actualización
        /// </summary>
        /// <param name="updateZipPath">Ruta del archivo ZIP de actualización</param>
        public void StartUpdate(string updateZipPath)
        {
            try
            {
                // Determinar la ruta correcta del updater
                string basePath;
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    basePath = ApplicationDeployment.CurrentDeployment.DataDirectory;
                }
                else
                {
                    basePath = Application.StartupPath;
                }
                
                string updaterPath = Path.Combine(basePath, _updaterExeName);

                if (!File.Exists(updaterPath))
                {
                    MessageBox.Show($"No se encontró el actualizador: {_updaterExeName}\nBuscado en: {updaterPath}",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Argumentos para el updater:
                // 1. Ruta del ZIP
                // 2. Ruta de instalación
                // 3. Nombre del ejecutable principal
                string installPath = ApplicationDeployment.IsNetworkDeployed ? 
                    ApplicationDeployment.CurrentDeployment.DataDirectory : 
                    Application.StartupPath;
                    
                string args = $"\"{updateZipPath}\" \"{installPath}\" \"Niveles.exe\"";

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = updaterPath,
                    Arguments = args,
                    UseShellExecute = true,
                    Verb = "runas" // Ejecutar como administrador
                };

                Process.Start(startInfo);

                // Cerrar la aplicación principal
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al iniciar actualización: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Flujo completo de actualización con UI
        /// </summary>
        public void CheckAndUpdate()
        {
            try
            {
                // Si es ClickOnce, usar el sistema de actualización nativo
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    CheckAndUpdateClickOnce();
                    return;
                }

                bool updateAvailable = CheckForUpdates();

                if (!updateAvailable)
                    return;

                var result = MessageBox.Show(
                    "Hay una nueva versión disponible. ¿Desea descargar e instalar la actualización?",
                    "Actualización Disponible",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information
                );

                if (result != DialogResult.Yes)
                    return;

                // Crear formulario de progreso
                Form progressForm = new Form
                {
                    Text = "Descargando actualización...",
                    Width = 400,
                    Height = 150,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    StartPosition = FormStartPosition.CenterScreen,
                    MaximizeBox = false,
                    MinimizeBox = false
                };

                ProgressBar progressBar = new ProgressBar
                {
                    Minimum = 0,
                    Maximum = 100,
                    Value = 0,
                    Dock = DockStyle.Bottom,
                    Height = 30
                };

                Label label = new Label
                {
                    Text = "Descargando actualización, por favor espere...",
                    Dock = DockStyle.Fill,
                    TextAlign = System.Drawing.ContentAlignment.MiddleCenter
                };

                progressForm.Controls.Add(label);
                progressForm.Controls.Add(progressBar);
                progressForm.Show();

                var progress = new Progress<int>(value =>
                {
                    if (progressBar.InvokeRequired)
                    {
                        progressBar.Invoke(new Action(() =>
                        {
                            progressBar.Value = value;
                            label.Text = $"Descargando actualización... {value}%";
                            progressForm.Refresh();
                        }));
                    }
                    else
                    {
                        progressBar.Value = value;
                        label.Text = $"Descargando actualización... {value}%";
                        progressForm.Refresh();
                    }
                });

                // Ejecutar descarga en background thread para no bloquear UI
                string updatePath = null;
                Task.Run(() =>
                {
                    updatePath = DownloadUpdate(progress);
                }).Wait();

                progressForm.Close();

                if (!string.IsNullOrEmpty(updatePath))
                {
                    MessageBox.Show(
                        "Actualización descargada. La aplicación se cerrará para aplicar los cambios.",
                        "Actualización Lista",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );

                    StartUpdate(updatePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en el proceso de actualización: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja actualizaciones para aplicaciones ClickOnce
        /// </summary>
        private void CheckAndUpdateClickOnce()
        {
            try
            {
                ApplicationDeployment deployment = ApplicationDeployment.CurrentDeployment;
                
                if (deployment.CheckForUpdate())
                {
                    var result = MessageBox.Show(
                        "Hay una nueva versión disponible. ¿Desea descargar e instalar la actualización?",
                        "Actualización Disponible",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information
                    );

                    if (result == DialogResult.Yes)
                    {
                        deployment.Update();
                        MessageBox.Show(
                            "La aplicación se ha actualizado. Se reiniciará para aplicar los cambios.",
                            "Actualización Completada",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                        Application.Restart();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al verificar actualizaciones ClickOnce: {ex.Message}",
                    "Error de Actualización", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

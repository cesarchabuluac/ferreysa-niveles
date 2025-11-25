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
using Niveles.UI;

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
            Debug.WriteLine("=== CONSTRUCTOR UpdateManager INICIADO ===");
            try
            {
                // Buscar UpdateConfig.json en múltiples ubicaciones posibles
                Debug.WriteLine("Buscando archivo de configuración...");
                _configPath = FindConfigFile();
                Debug.WriteLine($"Archivo de configuración encontrado en: {_configPath}");
                
                Debug.WriteLine("Cargando configuración...");
                LoadConfiguration();
                Debug.WriteLine("Configuración cargada exitosamente");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error en constructor UpdateManager: {ex.Message}");
                Debug.WriteLine($"StackTrace: {ex.StackTrace}");
                throw;
            }
            Debug.WriteLine("=== CONSTRUCTOR UpdateManager COMPLETADO ===");
        }

        /// <summary>
        /// Busca el archivo UpdateConfig.json en diferentes ubicaciones
        /// </summary>
        private string FindConfigFile()
        {
            // PRIORIDAD: Usar ubicación con permisos de escritura
            string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Niveles", "UpdateConfig.json");
            
            string[] possiblePaths;
            
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                // Para ClickOnce, buscar en múltiples ubicaciones
                possiblePaths = new string[]
                {
                    appDataPath, // PRIORIDAD: AppData (siempre escribible)
                    Path.Combine(ApplicationDeployment.CurrentDeployment.DataDirectory, "UpdateConfig.json"),
                    Path.Combine(Application.StartupPath, "UpdateConfig.json"),
                    Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "UpdateConfig.json")
                };
            }
            else
            {
                // Para instalación normal
                possiblePaths = new string[]
                {
                    appDataPath, // PRIORIDAD: AppData (siempre escribible)
                    Path.Combine(Application.StartupPath, "UpdateConfig.json"),
                    Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "UpdateConfig.json")
                };
            }

            // Buscar archivo existente
            foreach (string path in possiblePaths)
            {
                if (File.Exists(path))
                {
                    Debug.WriteLine($"Archivo de configuración encontrado en: {path}");
                    return path;
                }
            }

            // Si no existe, crear en AppData (ubicación con permisos)
            Debug.WriteLine($"No se encontró archivo existente, creando en AppData: {appDataPath}");
            return appDataPath;
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
                    Debug.WriteLine($"Configuración cargada desde: {_configPath}");
                }
                else
                {
                    Debug.WriteLine($"Archivo no existe en: {_configPath}");
                    
                    // Si no existe el archivo, intentar leer desde recursos embebidos
                    json = LoadConfigFromEmbeddedResource();
                    
                    if (string.IsNullOrEmpty(json))
                    {
                        // Buscar en ubicación original del proyecto
                        string originalPath = Path.Combine(Application.StartupPath, "UpdateConfig.json");
                        if (File.Exists(originalPath))
                        {
                            json = File.ReadAllText(originalPath);
                            Debug.WriteLine($"Configuración encontrada en ubicación original: {originalPath}");
                        }
                    }
                    
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
                    else
                    {
                        // Crear el archivo en la ubicación de AppData para futuras escrituras
                        try
                        {
                            string directory = Path.GetDirectoryName(_configPath);
                            if (!Directory.Exists(directory))
                            {
                                Directory.CreateDirectory(directory);
                                Debug.WriteLine($"Directorio creado: {directory}");
                            }
                            
                            File.WriteAllText(_configPath, json);
                            Debug.WriteLine($"Archivo de configuración copiado a: {_configPath}");
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"Error al crear archivo en AppData: {ex.Message}");
                        }
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
                FileLogger.Info("Verificando actualizaciones en servidor FTP");
                FileLogger.Debug($"Servidor: {_ftpHost}, Usuario: {_ftpUsername}");
                
                using (var client = new FtpClient(_ftpHost, _ftpUsername, _ftpPassword))
                {
                    FileLogger.Debug("Conectando al servidor FTP...");
                    // Para FluentFTP v53, configurar timeouts
                    client.Config.ConnectTimeout = 30000; // 30 segundos
                    client.Config.ReadTimeout = 30000;    // 30 segundos
                    client.Config.DataConnectionConnectTimeout = 30000;
                    client.Connect();
                    FileLogger.Info("Conexión FTP establecida");

                    string remotePath = _updatesPath + _versionFile;
                    FileLogger.Debug($"Leyendo archivo de versión: {remotePath}");
                    
                    // Verificar que el archivo existe
                    if (!client.FileExists(remotePath))
                    {
                        FileLogger.Warning($"Archivo de versión no encontrado: {remotePath}");
                        client.Disconnect();
                        return false;
                    }
                    
                    // Descargar contenido del archivo de versión
                    using (var stream = client.OpenRead(remotePath))
                    using (var reader = new StreamReader(stream))
                    {
                        string remoteVersion = reader.ReadToEnd().Trim();
                        FileLogger.Info($"Versión remota encontrada: {remoteVersion}");
                        FileLogger.Info($"Versión local actual: {_currentVersion}");

                        if (string.IsNullOrWhiteSpace(remoteVersion))
                        {
                            FileLogger.Warning("Archivo de versión remoto está vacío");
                            client.Disconnect();
                            return false;
                        }

                        Version current = new Version(_currentVersion);
                        Version remote = new Version(remoteVersion);

                        client.Disconnect();
                        FileLogger.Info("Conexión FTP cerrada");

                        bool updateAvailable = remote > current;
                        FileLogger.Info($"¿Actualización disponible?: {updateAvailable}");
                        
                        return updateAvailable;
                    }
                }
            }
            catch (Exception ex)
            {
                FileLogger.Error("Error al verificar actualizaciones", ex);
                return false;
            }
        }



        /// <summary>
        /// Descarga la actualización desde el servidor FTP con progreso real
        /// </summary>
        /// <param name="progress">Callback para reportar progreso (0-100)</param>
        /// <returns>Ruta del archivo descargado o null si falla</returns>
        public string DownloadUpdate(IProgress<int> progress = null)
        {
            try
            {
                FileLogger.Info("Iniciando descarga de actualización");
                
                string tempPath = Path.Combine(Path.GetTempPath(), "NivelesUpdate");
                if (!Directory.Exists(tempPath))
                {
                    Directory.CreateDirectory(tempPath);
                    FileLogger.Debug($"Directorio temporal creado: {tempPath}");
                }

                string localUpdatePath = Path.Combine(tempPath, _updatePackage);
                FileLogger.Info($"Descargando a: {localUpdatePath}");

                using (var client = new FtpClient(_ftpHost, _ftpUsername, _ftpPassword))
                {
                    FileLogger.Debug($"Conectando a FTP: {_ftpHost}");
                    // Configurar timeouts para FluentFTP v53
                    client.Config.ConnectTimeout = 30000;
                    client.Config.ReadTimeout = 30000;
                    client.Config.DataConnectionConnectTimeout = 30000;
                    client.Connect();

                    string remotePath = _updatesPath + _updatePackage;
                    FileLogger.Debug($"Archivo remoto: {remotePath}");

                    // Reportar inicio de descarga
                    progress?.Report(0);

                    // Obtener tamaño del archivo para calcular progreso real
                    long fileSize = 0;
                    try
                    {
                        fileSize = client.GetFileSize(remotePath);
                        FileLogger.Debug($"Tamaño del archivo: {fileSize} bytes");
                    }
                    catch (Exception ex)
                    {
                        FileLogger.Warning($"No se pudo obtener tamaño del archivo: {ex.Message}");
                    }

                    // Configurar callback de progreso si tenemos el tamaño
                    if (fileSize > 0 && progress != null)
                    {
                        Action<FtpProgress> progressCallback = (ftpProgress) =>
                        {
                            if (ftpProgress.Progress >= 0 && ftpProgress.Progress <= 100)
                            {
                                progress.Report((int)ftpProgress.Progress);
                                FileLogger.Debug($"Progreso descarga: {ftpProgress.Progress:F1}%");
                            }
                        };

                        // Descargar con progreso
                        client.DownloadFile(localUpdatePath, remotePath, FtpLocalExists.Overwrite, FtpVerify.None, progressCallback);
                    }
                    else
                    {
                        // Descarga sin progreso detallado
                        FileLogger.Debug("Descargando sin progreso detallado");
                        client.DownloadFile(localUpdatePath, remotePath, FtpLocalExists.Overwrite);
                        progress?.Report(100);
                    }

                    client.Disconnect();
                    FileLogger.Info("Descarga FTP completada");

                    // Verificar que el archivo se descargó correctamente
                    if (File.Exists(localUpdatePath) && new FileInfo(localUpdatePath).Length > 0)
                    {
                        var fileInfo = new FileInfo(localUpdatePath);
                        FileLogger.Info($"Archivo descargado exitosamente: {fileInfo.Length} bytes");
                        return localUpdatePath;
                    }
                    else
                    {
                        FileLogger.Error("El archivo descargado está vacío o no existe");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                FileLogger.Error("Error durante la descarga de actualización", ex);
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
        /// Flujo completo de actualización con UI moderna
        /// </summary>
        public async void CheckAndUpdate()
        {
            FileLogger.Separator("SISTEMA DE ACTUALIZACIÓN");
            FileLogger.Info("Iniciando verificación de actualizaciones");
            
            try
            {
                FileLogger.Debug($"ApplicationDeployment.IsNetworkDeployed: {ApplicationDeployment.IsNetworkDeployed}");
                
                // Si es ClickOnce, usar el sistema de actualización nativo
                if (ApplicationDeployment.IsNetworkDeployed)
                {
                    FileLogger.Info("Aplicación detectada como ClickOnce, usando sistema nativo");
                    CheckAndUpdateClickOnce();
                    return;
                }

                FileLogger.Info("Aplicación independiente, verificando actualizaciones FTP");
                
                // PREVENIR LOOP: Verificar si acabamos de actualizar
                if (JustUpdated())
                {
                    FileLogger.Info("Actualización reciente detectada - saltando verificación");
                    return;
                }
                
                FileLogger.Info("Verificando disponibilidad de actualizaciones...");
                bool updateAvailable = CheckForUpdates();

                if (!updateAvailable)
                {
                    FileLogger.Info("No hay actualizaciones disponibles");
                    return;
                }

                FileLogger.Info("Actualización disponible - solicitando confirmación del usuario");
                var result = MessageBox.Show(
                    "Hay una nueva versión disponible. ¿Desea descargar e instalar la actualización?",
                    "Actualización Disponible",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Information
                );

                if (result != DialogResult.Yes)
                {
                    FileLogger.Info("Usuario canceló la actualización");
                    return;
                }

                // Usar el nuevo FormUpdater moderno
                using (var updaterForm = new FormUpdater())
                {
                    FileLogger.Info("Iniciando descarga con UI moderna");
                    
                    // Mostrar el formulario
                    updaterForm.Show();
                    updaterForm.UpdateProgress(0, "Conectando al servidor...");
                    
                    // Ejecutar descarga en background
                    string updatePath = null;
                    bool downloadSuccess = false;
                    
                    try
                    {
                        // Agregar timeout para evitar que se quede pegado
                        var downloadTask = Task.Run(() =>
                        {
                            var progress = new Progress<int>(percentage =>
                            {
                                string status = percentage < 100 ? 
                                    $"Descargando actualización... ({percentage}%)" : 
                                    "Descarga completada";
                                updaterForm.UpdateProgress(percentage, status);
                            });
                            
                            updatePath = DownloadUpdate(progress);
                            downloadSuccess = !string.IsNullOrEmpty(updatePath);
                        });
                        
                        // Esperar máximo 5 minutos
                        var timeoutTask = Task.Delay(TimeSpan.FromMinutes(5));
                        var completedTask = await Task.WhenAny(downloadTask, timeoutTask);
                        
                        if (completedTask == timeoutTask)
                        {
                            FileLogger.Error("Timeout en descarga de actualización (5 minutos)");
                            throw new TimeoutException("La descarga tardó demasiado tiempo (5 minutos)");
                        }
                        
                        await downloadTask; // Esperar a que termine la descarga
                    }
                    catch (Exception ex)
                    {
                        FileLogger.Error("Error durante la descarga", ex);
                        updaterForm.SetCompleted(false, $"Error en descarga: {ex.Message}");
                        updaterForm.ShowDialog(); // Esperar a que el usuario cierre
                        return;
                    }

                    if (downloadSuccess)
                    {
                        FileLogger.Info("Descarga completada exitosamente");
                        updaterForm.UpdateProgress(100, "Preparando instalación...");
                        updaterForm.DisableCancel();
                        
                        // ACTUALIZAR VERSIÓN LOCAL ANTES DE APLICAR LA ACTUALIZACIÓN
                        string remoteVersion = GetRemoteVersion();
                        if (!string.IsNullOrEmpty(remoteVersion))
                        {
                            FileLogger.Info($"Actualizando versión local a {remoteVersion}");
                            UpdateLocalVersion(remoteVersion);
                        }

                        updaterForm.SetCompleted(true, "Actualización lista para instalar. La aplicación se cerrará.");
                        
                        // Esperar a que el usuario cierre el diálogo
                        if (updaterForm.ShowDialog() == DialogResult.OK)
                        {
                            FileLogger.Info("Iniciando proceso de instalación");
                            StartUpdate(updatePath);
                        }
                    }
                    else
                    {
                        FileLogger.Error("Fallo en la descarga de actualización");
                        updaterForm.SetCompleted(false, "Error al descargar la actualización");
                        updaterForm.ShowDialog(); // Esperar a que el usuario cierre
                    }
                }
            }
            catch (Exception ex)
            {
                FileLogger.Error("Error en proceso de actualización", ex);
                MessageBox.Show($"Error en el proceso de actualización: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            FileLogger.Info("Proceso de actualización completado");
        }

        /// <summary>
        /// Método público para forzar actualización de versión (útil para testing)
        /// </summary>
        /// <param name="newVersion">Nueva versión a establecer</param>
        public void ForceUpdateLocalVersion(string newVersion)
        {
            UpdateLocalVersion(newVersion);
        }

        /// <summary>
        /// Prueba la conectividad FTP (útil para debugging)
        /// </summary>
        /// <returns>True si la conexión es exitosa</returns>
        public bool TestFtpConnection()
        {
            try
            {
                FileLogger.Separator("PRUEBA DE CONECTIVIDAD FTP");
                FileLogger.Info($"Probando conexión a: {_ftpHost}");
                FileLogger.Info($"Usuario: {_ftpUsername}");
                
                using (var client = new FtpClient(_ftpHost, _ftpUsername, _ftpPassword))
                {
                    // Configurar timeouts
                    client.Config.ConnectTimeout = 15000; // 15 segundos para prueba
                    client.Config.ReadTimeout = 15000;
                    client.Config.DataConnectionConnectTimeout = 15000;
                    
                    FileLogger.Info("Intentando conectar...");
                    client.Connect();
                    FileLogger.Info("✅ Conexión FTP exitosa");
                    
                    // Probar listar directorio
                    FileLogger.Info($"Listando directorio: {_updatesPath}");
                    var files = client.GetListing(_updatesPath);
                    FileLogger.Info($"Archivos encontrados: {files.Length}");
                    
                    foreach (var file in files)
                    {
                        FileLogger.Debug($"  - {file.Name} ({file.Size} bytes)");
                    }
                    
                    client.Disconnect();
                    FileLogger.Info("✅ Prueba FTP completada exitosamente");
                    return true;
                }
            }
            catch (Exception ex)
            {
                FileLogger.Error("❌ Error en prueba FTP", ex);
                return false;
            }
        }

        /// <summary>
        /// Obtiene la versión local actual
        /// </summary>
        /// <returns>Versión local actual</returns>
        public string GetCurrentVersion()
        {
            return _currentVersion;
        }

        /// <summary>
        /// Actualiza la versión local en UpdateConfig.json después de una actualización exitosa
        /// </summary>
        /// <param name="newVersion">Nueva versión a establecer</param>
        private void UpdateLocalVersion(string newVersion)
        {
            try
            {
                Debug.WriteLine($"=== ACTUALIZANDO VERSIÓN LOCAL A: {newVersion} ===");
                
                // Actualizar el objeto de configuración en memoria
                _config["AppSettings"]["CurrentVersion"] = newVersion;
                _currentVersion = newVersion;
                
                // Escribir el archivo actualizado
                string updatedJson = _config.ToString();
                File.WriteAllText(_configPath, updatedJson);
                
                Debug.WriteLine($"Versión local actualizada exitosamente a: {newVersion}");
                Debug.WriteLine($"Archivo actualizado en: {_configPath}");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al actualizar versión local: {ex.Message}");
                Debug.WriteLine($"StackTrace: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Obtiene la versión remota desde el FTP
        /// </summary>
        /// <returns>Versión remota o null si hay error</returns>
        private string GetRemoteVersion()
        {
            try
            {
                using (var client = new FtpClient(_ftpHost, _ftpUsername, _ftpPassword))
                {
                    // Configurar timeouts para FluentFTP v53
                    client.Config.ConnectTimeout = 30000;
                    client.Config.ReadTimeout = 30000;
                    client.Connect();
                    string remotePath = _updatesPath + _versionFile;
                    
                    using (var stream = client.OpenRead(remotePath))
                    using (var reader = new StreamReader(stream))
                    {
                        string remoteVersion = reader.ReadToEnd().Trim();
                        client.Disconnect();
                        return string.IsNullOrWhiteSpace(remoteVersion) ? null : remoteVersion;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al obtener versión remota: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Verifica si acabamos de actualizar comparando versiones
        /// </summary>
        /// <returns>True si acabamos de actualizar</returns>
        private bool JustUpdated()
        {
            try
            {
                string remoteVersion = GetRemoteVersion();
                if (string.IsNullOrEmpty(remoteVersion))
                    return false;

                Version current = new Version(_currentVersion);
                Version remote = new Version(remoteVersion);
                
                // Si las versiones son iguales, significa que ya actualizamos
                bool justUpdated = current >= remote;
                
                Debug.WriteLine($"Verificación post-actualización:");
                Debug.WriteLine($"  Versión local: {_currentVersion}");
                Debug.WriteLine($"  Versión remota: {remoteVersion}");
                Debug.WriteLine($"  ¿Acabamos de actualizar?: {justUpdated}");
                
                return justUpdated;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error al verificar si acabamos de actualizar: {ex.Message}");
                return false;
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

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UpdaterModerno
{
    /// <summary>
    /// Formulario moderno para instalación de actualizaciones
    /// Reemplaza el Updater.exe de consola con UI gráfica
    /// </summary>
    public partial class FormUpdaterInstaller : Form
    {
        private ProgressBar progressBar;
        private Label lblStatus;
        private Label lblProgress;
        private Label lblTitle;
        private Button btnClose;
        private PictureBox pictureBox;
        
        private string _zipPath;
        private string _installPath;
        private string _exeName;
        private bool _installCompleted = false;

        public FormUpdaterInstaller(string[] args)
        {
            InitializeComponent();
            SetupModernUI();
            ParseArguments(args);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblTitle.Location = new System.Drawing.Point(80, 25);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(560, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Instalando Actualización";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(20, 18);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(43, 39);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 1;
            this.pictureBox.TabStop = false;
            // 
            // lblStatus
            // 
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.lblStatus.Location = new System.Drawing.Point(27, 86);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(613, 49);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Preparando instalación...";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(27, 148);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(613, 31);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 3;
            // 
            // lblProgress
            // 
            this.lblProgress.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblProgress.Location = new System.Drawing.Point(27, 185);
            this.lblProgress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(613, 18);
            this.lblProgress.TabIndex = 4;
            this.lblProgress.Text = "0%";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(547, 246);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(93, 37);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Finalizar";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Visible = false;
            // 
            // FormUpdaterInstaller
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(667, 324);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormUpdaterInstaller";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Instalador de Actualización NIVELES UPDATER";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FormUpdaterInstaller_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        private void SetupModernUI()
        {
            this.Font = new Font("Segoe UI", 9F);
            
            // Prevenir cierre durante instalación
            this.FormClosing += FormUpdaterInstaller_FormClosing;
        }

        private void FormUpdaterInstaller_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_installCompleted)
            {
                // No permitir cerrar durante la instalación
                e.Cancel = true;
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// Parsea los argumentos de línea de comandos
        /// </summary>
        private void ParseArguments(string[] args)
        {
            if (args.Length >= 3)
            {
                _zipPath = args[0].Trim('"');
                _installPath = args[1].Trim('"');
                _exeName = args[2].Trim('"');
            }
            else
            {
                MessageBox.Show(
                    "Uso: UpdaterModerno.exe \"ruta_zip\" \"ruta_instalacion\" \"nombre_exe\"",
                    "Error de Argumentos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        /// <summary>
        /// Actualiza el progreso de la instalación
        /// </summary>
        public void UpdateProgress(int percentage, string status)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<int, string>(UpdateProgress), percentage, status);
                return;
            }

            progressBar.Value = Math.Max(0, Math.Min(100, percentage));
            lblProgress.Text = $"{percentage}%";
            lblStatus.Text = status;
            
            Application.DoEvents(); // Actualizar UI inmediatamente
        }

        /// <summary>
        /// Marca la instalación como completada
        /// </summary>
        public void SetCompleted(bool success, string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<bool, string>(SetCompleted), success, message);
                return;
            }

            _installCompleted = true;
            
            if (success)
            {
                lblTitle.Text = "Actualización Completada";
                lblTitle.ForeColor = Color.FromArgb(0, 150, 0);
                progressBar.Value = 100;
                lblProgress.Text = "100%";
                lblStatus.Text = message ?? "Actualización instalada exitosamente";
            }
            else
            {
                lblTitle.Text = "Error en Instalación";
                lblTitle.ForeColor = Color.FromArgb(200, 0, 0);
                lblStatus.Text = message ?? "Error durante la instalación";
            }
            
            btnClose.Visible = true;
            btnClose.Focus();
        }

        /// <summary>
        /// Inicia el proceso de instalación
        /// </summary>
        public async Task StartInstallationAsync()
        {
            try
            {
                UpdateProgress(0, "Validando archivos...");
                
                // Validar que el ZIP existe
                if (!File.Exists(_zipPath))
                {
                    SetCompleted(false, $"No se encontró el archivo: {_zipPath}");
                    return;
                }

                // Validar que el directorio de instalación existe
                if (!Directory.Exists(_installPath))
                {
                    SetCompleted(false, $"No se encontró el directorio: {_installPath}");
                    return;
                }

                UpdateProgress(10, "Cerrando aplicación principal...");
                
                // Esperar a que se cierre la aplicación principal
                await WaitForApplicationToClose();

                UpdateProgress(20, "Extrayendo archivos...");
                
                // Extraer el ZIP
                await ExtractUpdateAsync();

                UpdateProgress(90, "Finalizando instalación...");
                
                // Pequeña pausa para mostrar progreso
                await Task.Delay(500);

                UpdateProgress(100, "Iniciando aplicación actualizada...");
                
                // Reiniciar la aplicación
                RestartApplication();

                SetCompleted(true, "Actualización completada. La aplicación se reiniciará.");
                
                // Esperar un momento antes de cerrar
                await Task.Delay(2000);
                Application.Exit();
            }
            catch (Exception ex)
            {
                SetCompleted(false, $"Error durante la instalación: {ex.Message}");
            }
        }

        /// <summary>
        /// Espera a que se cierre la aplicación principal
        /// </summary>
        private async Task WaitForApplicationToClose()
        {
            string processName = Path.GetFileNameWithoutExtension(_exeName);
            int attempts = 0;
            const int maxAttempts = 30; // 30 segundos máximo
            
            while (attempts < maxAttempts)
            {
                var processes = Process.GetProcessesByName(processName);
                if (processes.Length == 0)
                {
                    break; // La aplicación se cerró
                }
                
                UpdateProgress(10 + (attempts * 2), $"Esperando cierre de {_exeName}... ({maxAttempts - attempts}s)");
                await Task.Delay(1000);
                attempts++;
            }
        }

        /// <summary>
        /// Extrae la actualización
        /// </summary>
        private async Task ExtractUpdateAsync()
        {
            await Task.Run(() =>
            {
                using (var archive = ZipFile.OpenRead(_zipPath))
                {
                    int totalEntries = archive.Entries.Count;
                    int currentEntry = 0;
                    
                    foreach (var entry in archive.Entries)
                    {
                        currentEntry++;
                        int progress = 20 + (int)((currentEntry / (double)totalEntries) * 60);
                        
                        UpdateProgress(progress, $"Extrayendo: {entry.Name}");
                        
                        string destinationPath = Path.Combine(_installPath, entry.FullName);
                        
                        // Crear directorio si no existe
                        string directory = Path.GetDirectoryName(destinationPath);
                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }
                        
                        // Extraer archivo (solo si no es directorio)
                        if (!string.IsNullOrEmpty(entry.Name))
                        {
                            entry.ExtractToFile(destinationPath, true);
                        }
                    }
                }
            });
        }

        /// <summary>
        /// Reinicia la aplicación principal
        /// </summary>
        private void RestartApplication()
        {
            try
            {
                string exePath = Path.Combine(_installPath, _exeName);
                if (File.Exists(exePath))
                {
                    Process.Start(exePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al reiniciar la aplicación: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void FormUpdaterInstaller_Load(object sender, EventArgs e)
        {

        }
    }
}

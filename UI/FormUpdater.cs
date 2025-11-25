using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using FluentFTP;
using Niveles.Helpers;

namespace Niveles.UI
{
    /// <summary>
    /// Formulario moderno para actualizaciones con UI amigable
    /// </summary>
    public partial class FormUpdater : Form
    {
        private ProgressBar progressBar;
        private Label lblStatus;
        private Label lblProgress;
        private Label lblTitle;
        private Button btnCancel;
        private Button btnClose;
        private PictureBox pictureBox;
        
        private bool _canCancel = true;
        private bool _updateCompleted = false;

        public FormUpdater()
        {
            InitializeComponent();
            SetupModernUI();
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.lblProgress = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
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
            this.lblTitle.Text = "Actualizando NIVELES";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(20, 18);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.lblStatus.Size = new System.Drawing.Size(613, 25);
            this.lblStatus.TabIndex = 2;
            this.lblStatus.Text = "Preparando actualización...";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(27, 123);
            this.progressBar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(613, 31);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 3;
            // 
            // lblProgress
            // 
            this.lblProgress.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblProgress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.lblProgress.Location = new System.Drawing.Point(27, 160);
            this.lblProgress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProgress.Name = "lblProgress";
            this.lblProgress.Size = new System.Drawing.Size(613, 18);
            this.lblProgress.TabIndex = 4;
            this.lblProgress.Text = "0%";
            this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.btnCancel.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnCancel.Location = new System.Drawing.Point(417, 199);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(107, 37);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancelar";
            this.btnCancel.UseVisualStyleBackColor = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(537, 199);
            this.btnClose.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(93, 37);
            this.btnClose.TabIndex = 6;
            this.btnClose.Text = "Cerrar";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Visible = false;
            // 
            // FormUpdater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(667, 253);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.lblProgress);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormUpdater";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Validando Actualización NIVELES";
            this.Load += new System.EventHandler(this.FormUpdater_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);

        }

        private void SetupModernUI()
        {
            // Configuración adicional para UI moderna
            this.Font = new Font("Segoe UI", 9F);
            
            // Conectar event handlers que se perdieron en el designer
            this.btnCancel.Click += BtnCancel_Click;
            this.btnClose.Click += BtnClose_Click;
            
            // Prevenir cierre con X mientras actualiza
            this.FormClosing += FormUpdater_FormClosing;
        }

        private void FormUpdater_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!_updateCompleted && _canCancel)
            {
                var result = MessageBox.Show(
                    "¿Está seguro que desea cancelar la actualización?", 
                    "Confirmar Cancelación", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question);
                
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            if (_canCancel)
            {
                var result = MessageBox.Show(
                    "¿Está seguro que desea cancelar la actualización?", 
                    "Confirmar Cancelación", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Actualiza el estado del progreso
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
            
            FileLogger.Debug($"Progress: {percentage}% - {status}");
        }

        /// <summary>
        /// Marca la actualización como completada
        /// </summary>
        public void SetCompleted(bool success, string message)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<bool, string>(SetCompleted), success, message);
                return;
            }

            _updateCompleted = true;
            _canCancel = false;
            
            if (success)
            {
                lblTitle.Text = "Actualización Completada";
                lblTitle.ForeColor = Color.FromArgb(0, 150, 0);
                progressBar.Value = 100;
                lblProgress.Text = "100%";
                lblStatus.Text = message ?? "Actualización completada exitosamente";
            }
            else
            {
                lblTitle.Text = "Error en Actualización";
                lblTitle.ForeColor = Color.FromArgb(200, 0, 0);
                lblStatus.Text = message ?? "Error durante la actualización";
            }
            
            btnCancel.Visible = false;
            btnClose.Visible = true;
            btnClose.Focus();
        }

        /// <summary>
        /// Deshabilita la cancelación
        /// </summary>
        public void DisableCancel()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(DisableCancel));
                return;
            }

            _canCancel = false;
            btnCancel.Enabled = false;
            btnCancel.Text = "Procesando...";
        }

        private void FormUpdater_Load(object sender, EventArgs e)
        {
            FileLogger.Info("FormUpdater cargado y listo");
            this.BackColor = Color.White; // Asegurar fondo blanco
        }
    }
}

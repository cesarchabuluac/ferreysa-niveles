namespace Vales
{
    partial class FormImagen
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormImagen));
            this.pbx_imagen = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbx_imagen)).BeginInit();
            this.SuspendLayout();
            // 
            // pbx_imagen
            // 
            this.pbx_imagen.Location = new System.Drawing.Point(12, 12);
            this.pbx_imagen.Name = "pbx_imagen";
            this.pbx_imagen.Size = new System.Drawing.Size(420, 420);
            this.pbx_imagen.TabIndex = 0;
            this.pbx_imagen.TabStop = false;
            // 
            // FormImagen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pbx_imagen);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormImagen";
            this.Text = "Imagen de Articulo";
            this.Load += new System.EventHandler(this.FormImagen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbx_imagen)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbx_imagen;
    }
}
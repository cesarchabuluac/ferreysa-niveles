namespace Vales
{
    partial class FormVerArt
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormVerArt));
            this.cbo_almacen = new System.Windows.Forms.ComboBox();
            this.dgv_kardex = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.tbx_nombre = new System.Windows.Forms.TextBox();
            this.tbx_ubicacion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tbx_estatus = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dgv_claves = new System.Windows.Forms.DataGridView();
            this.clave_art_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Clave = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_clasificadores = new System.Windows.Forms.DataGridView();
            this.Clasificador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbx_linea = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbx_grupo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbx_uc = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.tbx_um = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tbx_orden_minima = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.conepto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Folio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripcion = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ud = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Inv_fin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_cargar = new System.Windows.Forms.Button();
            this.tbx_rotacion = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btn_ver_imagen = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_kardex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_claves)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_clasificadores)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbo_almacen
            // 
            this.cbo_almacen.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_almacen.FormattingEnabled = true;
            this.cbo_almacen.Location = new System.Drawing.Point(9, 22);
            this.cbo_almacen.Name = "cbo_almacen";
            this.cbo_almacen.Size = new System.Drawing.Size(121, 21);
            this.cbo_almacen.TabIndex = 0;
            this.cbo_almacen.SelectedIndexChanged += new System.EventHandler(this.cbo_almacen_SelectedIndexChanged);
            // 
            // dgv_kardex
            // 
            this.dgv_kardex.AllowUserToAddRows = false;
            this.dgv_kardex.AllowUserToDeleteRows = false;
            this.dgv_kardex.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_kardex.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Fecha,
            this.conepto,
            this.Folio,
            this.Descripcion,
            this.ud,
            this.Inv_fin});
            this.dgv_kardex.Location = new System.Drawing.Point(12, 383);
            this.dgv_kardex.Name = "dgv_kardex";
            this.dgv_kardex.ReadOnly = true;
            this.dgv_kardex.RowHeadersVisible = false;
            this.dgv_kardex.Size = new System.Drawing.Size(592, 281);
            this.dgv_kardex.TabIndex = 1;
            this.dgv_kardex.SelectionChanged += new System.EventHandler(this.dgv_kardex_SelectionChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Descripcion:";
            // 
            // tbx_nombre
            // 
            this.tbx_nombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_nombre.Location = new System.Drawing.Point(83, 10);
            this.tbx_nombre.Name = "tbx_nombre";
            this.tbx_nombre.ReadOnly = true;
            this.tbx_nombre.Size = new System.Drawing.Size(498, 22);
            this.tbx_nombre.TabIndex = 3;
            // 
            // tbx_ubicacion
            // 
            this.tbx_ubicacion.Location = new System.Drawing.Point(83, 38);
            this.tbx_ubicacion.Name = "tbx_ubicacion";
            this.tbx_ubicacion.ReadOnly = true;
            this.tbx_ubicacion.Size = new System.Drawing.Size(71, 20);
            this.tbx_ubicacion.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ubicacion alm:";
            // 
            // tbx_estatus
            // 
            this.tbx_estatus.Location = new System.Drawing.Point(434, 22);
            this.tbx_estatus.Name = "tbx_estatus";
            this.tbx_estatus.ReadOnly = true;
            this.tbx_estatus.Size = new System.Drawing.Size(152, 20);
            this.tbx_estatus.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(482, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Estatus del Articulo:";
            // 
            // dgv_claves
            // 
            this.dgv_claves.AllowUserToAddRows = false;
            this.dgv_claves.AllowUserToDeleteRows = false;
            this.dgv_claves.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_claves.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clave_art_id,
            this.Clave,
            this.rol});
            this.dgv_claves.Location = new System.Drawing.Point(6, 120);
            this.dgv_claves.Name = "dgv_claves";
            this.dgv_claves.ReadOnly = true;
            this.dgv_claves.RowHeadersVisible = false;
            this.dgv_claves.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgv_claves.Size = new System.Drawing.Size(257, 129);
            this.dgv_claves.TabIndex = 8;
            this.dgv_claves.SelectionChanged += new System.EventHandler(this.dgv_claves_SelectionChanged);
            // 
            // clave_art_id
            // 
            this.clave_art_id.HeaderText = "Column1";
            this.clave_art_id.Name = "clave_art_id";
            this.clave_art_id.ReadOnly = true;
            this.clave_art_id.Visible = false;
            // 
            // Clave
            // 
            this.Clave.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Clave.HeaderText = "Clave";
            this.Clave.Name = "Clave";
            this.Clave.ReadOnly = true;
            // 
            // rol
            // 
            this.rol.HeaderText = "Rol";
            this.rol.Name = "rol";
            this.rol.ReadOnly = true;
            this.rol.Width = 110;
            // 
            // dgv_clasificadores
            // 
            this.dgv_clasificadores.AllowUserToAddRows = false;
            this.dgv_clasificadores.AllowUserToDeleteRows = false;
            this.dgv_clasificadores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_clasificadores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Clasificador,
            this.valor});
            this.dgv_clasificadores.Location = new System.Drawing.Point(296, 120);
            this.dgv_clasificadores.Name = "dgv_clasificadores";
            this.dgv_clasificadores.ReadOnly = true;
            this.dgv_clasificadores.RowHeadersVisible = false;
            this.dgv_clasificadores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgv_clasificadores.Size = new System.Drawing.Size(285, 129);
            this.dgv_clasificadores.TabIndex = 9;
            this.dgv_clasificadores.SelectionChanged += new System.EventHandler(this.dgv_clasificadores_SelectionChanged);
            // 
            // Clasificador
            // 
            this.Clasificador.HeaderText = "Clasificador";
            this.Clasificador.Name = "Clasificador";
            this.Clasificador.ReadOnly = true;
            this.Clasificador.Width = 120;
            // 
            // valor
            // 
            this.valor.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.valor.HeaderText = "Valor";
            this.valor.Name = "valor";
            this.valor.ReadOnly = true;
            // 
            // tbx_linea
            // 
            this.tbx_linea.Location = new System.Drawing.Point(218, 36);
            this.tbx_linea.Name = "tbx_linea";
            this.tbx_linea.ReadOnly = true;
            this.tbx_linea.Size = new System.Drawing.Size(228, 20);
            this.tbx_linea.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(175, 41);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Linea: ";
            // 
            // tbx_grupo
            // 
            this.tbx_grupo.Location = new System.Drawing.Point(264, 64);
            this.tbx_grupo.Name = "tbx_grupo";
            this.tbx_grupo.ReadOnly = true;
            this.tbx_grupo.Size = new System.Drawing.Size(182, 20);
            this.tbx_grupo.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(175, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Grupo de Lineas:";
            // 
            // tbx_uc
            // 
            this.tbx_uc.Location = new System.Drawing.Point(535, 64);
            this.tbx_uc.Name = "tbx_uc";
            this.tbx_uc.ReadOnly = true;
            this.tbx_uc.Size = new System.Drawing.Size(45, 20);
            this.tbx_uc.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label6.Location = new System.Drawing.Point(518, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(20, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "uC";
            // 
            // tbx_um
            // 
            this.tbx_um.Location = new System.Drawing.Point(471, 64);
            this.tbx_um.Name = "tbx_um";
            this.tbx_um.ReadOnly = true;
            this.tbx_um.Size = new System.Drawing.Size(46, 20);
            this.tbx_um.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label7.Location = new System.Drawing.Point(452, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(22, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "uM";
            // 
            // tbx_orden_minima
            // 
            this.tbx_orden_minima.Location = new System.Drawing.Point(83, 64);
            this.tbx_orden_minima.Name = "tbx_orden_minima";
            this.tbx_orden_minima.ReadOnly = true;
            this.tbx_orden_minima.Size = new System.Drawing.Size(71, 20);
            this.tbx_orden_minima.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(7, 67);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(75, 13);
            this.label8.TabIndex = 18;
            this.label8.Text = "Orden Minima:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.tbx_rotacion);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.tbx_grupo);
            this.panel1.Controls.Add(this.tbx_um);
            this.panel1.Controls.Add(this.tbx_nombre);
            this.panel1.Controls.Add(this.tbx_orden_minima);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.tbx_uc);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.dgv_clasificadores);
            this.panel1.Controls.Add(this.tbx_linea);
            this.panel1.Controls.Add(this.dgv_claves);
            this.panel1.Controls.Add(this.tbx_ubicacion);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Location = new System.Drawing.Point(12, 64);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(592, 275);
            this.panel1.TabIndex = 20;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.groupBox1.Location = new System.Drawing.Point(7, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 94);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Claves del Articulo:";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.groupBox2.Location = new System.Drawing.Point(296, 102);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(284, 95);
            this.groupBox2.TabIndex = 25;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Clasificadores del Articulo:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 13);
            this.label9.TabIndex = 21;
            this.label9.Text = "Almacen";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.btn_ver_imagen);
            this.panel2.Controls.Add(this.btn_cargar);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.tbx_estatus);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.cbo_almacen);
            this.panel2.Location = new System.Drawing.Point(11, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(593, 59);
            this.panel2.TabIndex = 22;
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.groupBox3.Location = new System.Drawing.Point(11, 365);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(593, 210);
            this.groupBox3.TabIndex = 23;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Kardex del Articulo alm: ";
            // 
            // Fecha
            // 
            this.Fecha.HeaderText = "Fecha";
            this.Fecha.Name = "Fecha";
            this.Fecha.ReadOnly = true;
            this.Fecha.Width = 80;
            // 
            // conepto
            // 
            this.conepto.HeaderText = "Concepto";
            this.conepto.Name = "conepto";
            this.conepto.ReadOnly = true;
            this.conepto.Width = 150;
            // 
            // Folio
            // 
            this.Folio.HeaderText = "Folio";
            this.Folio.Name = "Folio";
            this.Folio.ReadOnly = true;
            this.Folio.Width = 80;
            // 
            // Descripcion
            // 
            this.Descripcion.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Descripcion.HeaderText = "Descripcion";
            this.Descripcion.Name = "Descripcion";
            this.Descripcion.ReadOnly = true;
            // 
            // ud
            // 
            this.ud.HeaderText = "Unidades";
            this.ud.Name = "ud";
            this.ud.ReadOnly = true;
            this.ud.Width = 60;
            // 
            // Inv_fin
            // 
            this.Inv_fin.HeaderText = "Inventario";
            this.Inv_fin.Name = "Inv_fin";
            this.Inv_fin.ReadOnly = true;
            this.Inv_fin.Width = 60;
            // 
            // btn_cargar
            // 
            this.btn_cargar.Location = new System.Drawing.Point(136, 22);
            this.btn_cargar.Name = "btn_cargar";
            this.btn_cargar.Size = new System.Drawing.Size(79, 21);
            this.btn_cargar.TabIndex = 22;
            this.btn_cargar.Text = "Cargar";
            this.btn_cargar.UseVisualStyleBackColor = true;
            this.btn_cargar.Click += new System.EventHandler(this.btn_cargar_Click);
            // 
            // tbx_rotacion
            // 
            this.tbx_rotacion.Location = new System.Drawing.Point(533, 34);
            this.tbx_rotacion.Name = "tbx_rotacion";
            this.tbx_rotacion.ReadOnly = true;
            this.tbx_rotacion.Size = new System.Drawing.Size(47, 20);
            this.tbx_rotacion.TabIndex = 27;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label10.Location = new System.Drawing.Point(452, 38);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(72, 13);
            this.label10.TabIndex = 26;
            this.label10.Text = "Rotacion alm:";
            // 
            // btn_ver_imagen
            // 
            this.btn_ver_imagen.Location = new System.Drawing.Point(290, 19);
            this.btn_ver_imagen.Name = "btn_ver_imagen";
            this.btn_ver_imagen.Size = new System.Drawing.Size(138, 23);
            this.btn_ver_imagen.TabIndex = 23;
            this.btn_ver_imagen.Text = "Ver Imagen de Articulo";
            this.btn_ver_imagen.UseVisualStyleBackColor = true;
            this.btn_ver_imagen.Click += new System.EventHandler(this.btn_ver_imagen_Click);
            // 
            // FormVerArt
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(616, 676);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dgv_kardex);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox3);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormVerArt";
            this.Text = "Detalle de Articulo";
            this.Load += new System.EventHandler(this.FormVerArt_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_kardex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_claves)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_clasificadores)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbo_almacen;
        private System.Windows.Forms.DataGridView dgv_kardex;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbx_nombre;
        private System.Windows.Forms.TextBox tbx_ubicacion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbx_estatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgv_claves;
        private System.Windows.Forms.DataGridView dgv_clasificadores;
        private System.Windows.Forms.TextBox tbx_linea;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbx_grupo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbx_uc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbx_um;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tbx_orden_minima;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridViewTextBoxColumn clave_art_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Clave;
        private System.Windows.Forms.DataGridViewTextBoxColumn rol;
        private System.Windows.Forms.DataGridViewTextBoxColumn Clasificador;
        private System.Windows.Forms.DataGridViewTextBoxColumn valor;
        private System.Windows.Forms.DataGridViewTextBoxColumn Fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn conepto;
        private System.Windows.Forms.DataGridViewTextBoxColumn Folio;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripcion;
        private System.Windows.Forms.DataGridViewTextBoxColumn ud;
        private System.Windows.Forms.DataGridViewTextBoxColumn Inv_fin;
        private System.Windows.Forms.Button btn_cargar;
        private System.Windows.Forms.TextBox tbx_rotacion;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btn_ver_imagen;
    }
}
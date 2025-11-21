namespace Vales
{
    partial class Form1
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
            this.dgv_valeentresus = new System.Windows.Forms.DataGridView();
            this.es_folio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.es_fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.es_origen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.es_solicita = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.es_chofer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.es_estatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_valefact = new System.Windows.Forms.DataGridView();
            this.df_folio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.df_fecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.df_origen = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.df_solicita = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.df_chofer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.df_factura = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_nuevo_vale = new System.Windows.Forms.Button();
            this.btn_nueva_cot = new System.Windows.Forms.Button();
            this.btn_afectar_vale = new System.Windows.Forms.Button();
            this.lbl_vendedor_nombre = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.clientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.misFacturasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.estadosDeCuentaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.articulosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.articulosNuevosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambiosDePreciosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.articulosAImpulsarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.entregasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.entregasToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.lbl_vales_entresus = new System.Windows.Forms.Label();
            this.lbl_vales_defacts = new System.Windows.Forms.Label();
            this.txt_pedido = new System.Windows.Forms.TextBox();
            this.txt_factura = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_cargar_cot = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_valeentresus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_valefact)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_valeentresus
            // 
            this.dgv_valeentresus.AllowUserToAddRows = false;
            this.dgv_valeentresus.AllowUserToDeleteRows = false;
            this.dgv_valeentresus.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_valeentresus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_valeentresus.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.es_folio,
            this.es_fecha,
            this.es_origen,
            this.es_solicita,
            this.es_chofer,
            this.es_estatus});
            this.dgv_valeentresus.Location = new System.Drawing.Point(270, 114);
            this.dgv_valeentresus.Margin = new System.Windows.Forms.Padding(2);
            this.dgv_valeentresus.Name = "dgv_valeentresus";
            this.dgv_valeentresus.ReadOnly = true;
            this.dgv_valeentresus.RowHeadersVisible = false;
            this.dgv_valeentresus.RowHeadersWidth = 82;
            this.dgv_valeentresus.RowTemplate.Height = 33;
            this.dgv_valeentresus.Size = new System.Drawing.Size(376, 405);
            this.dgv_valeentresus.TabIndex = 0;
            // 
            // es_folio
            // 
            this.es_folio.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.es_folio.HeaderText = "Folio";
            this.es_folio.Name = "es_folio";
            this.es_folio.ReadOnly = true;
            // 
            // es_fecha
            // 
            this.es_fecha.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.es_fecha.HeaderText = "Fecha";
            this.es_fecha.Name = "es_fecha";
            this.es_fecha.ReadOnly = true;
            // 
            // es_origen
            // 
            this.es_origen.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.es_origen.HeaderText = "Origen";
            this.es_origen.Name = "es_origen";
            this.es_origen.ReadOnly = true;
            // 
            // es_solicita
            // 
            this.es_solicita.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.es_solicita.HeaderText = "Solicita";
            this.es_solicita.Name = "es_solicita";
            this.es_solicita.ReadOnly = true;
            // 
            // es_chofer
            // 
            this.es_chofer.HeaderText = "Chofer";
            this.es_chofer.Name = "es_chofer";
            this.es_chofer.ReadOnly = true;
            // 
            // es_estatus
            // 
            this.es_estatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.es_estatus.HeaderText = "Estatus";
            this.es_estatus.Name = "es_estatus";
            this.es_estatus.ReadOnly = true;
            // 
            // dgv_valefact
            // 
            this.dgv_valefact.AllowUserToAddRows = false;
            this.dgv_valefact.AllowUserToDeleteRows = false;
            this.dgv_valefact.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_valefact.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_valefact.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.df_folio,
            this.df_fecha,
            this.df_origen,
            this.df_solicita,
            this.df_chofer,
            this.df_factura});
            this.dgv_valefact.Location = new System.Drawing.Point(660, 114);
            this.dgv_valefact.Margin = new System.Windows.Forms.Padding(2);
            this.dgv_valefact.Name = "dgv_valefact";
            this.dgv_valefact.ReadOnly = true;
            this.dgv_valefact.RowHeadersVisible = false;
            this.dgv_valefact.RowHeadersWidth = 82;
            this.dgv_valefact.RowTemplate.Height = 33;
            this.dgv_valefact.Size = new System.Drawing.Size(376, 405);
            this.dgv_valefact.TabIndex = 1;
            // 
            // df_folio
            // 
            this.df_folio.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.df_folio.HeaderText = "Folio";
            this.df_folio.Name = "df_folio";
            this.df_folio.ReadOnly = true;
            // 
            // df_fecha
            // 
            this.df_fecha.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.df_fecha.HeaderText = "Fecha";
            this.df_fecha.Name = "df_fecha";
            this.df_fecha.ReadOnly = true;
            // 
            // df_origen
            // 
            this.df_origen.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.df_origen.HeaderText = "Origen";
            this.df_origen.Name = "df_origen";
            this.df_origen.ReadOnly = true;
            // 
            // df_solicita
            // 
            this.df_solicita.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.df_solicita.HeaderText = "Solicita";
            this.df_solicita.Name = "df_solicita";
            this.df_solicita.ReadOnly = true;
            // 
            // df_chofer
            // 
            this.df_chofer.HeaderText = "Chofer";
            this.df_chofer.Name = "df_chofer";
            this.df_chofer.ReadOnly = true;
            // 
            // df_factura
            // 
            this.df_factura.HeaderText = "Factura";
            this.df_factura.Name = "df_factura";
            this.df_factura.ReadOnly = true;
            // 
            // btn_nuevo_vale
            // 
            this.btn_nuevo_vale.Location = new System.Drawing.Point(20, 69);
            this.btn_nuevo_vale.Margin = new System.Windows.Forms.Padding(2);
            this.btn_nuevo_vale.Name = "btn_nuevo_vale";
            this.btn_nuevo_vale.Size = new System.Drawing.Size(129, 42);
            this.btn_nuevo_vale.TabIndex = 2;
            this.btn_nuevo_vale.Text = "Nuevo Vale (Pedido)";
            this.btn_nuevo_vale.UseVisualStyleBackColor = true;
            this.btn_nuevo_vale.Click += new System.EventHandler(this.btn_nuevo_vale_Click);
            // 
            // btn_nueva_cot
            // 
            this.btn_nueva_cot.Location = new System.Drawing.Point(21, 75);
            this.btn_nueva_cot.Margin = new System.Windows.Forms.Padding(2);
            this.btn_nueva_cot.Name = "btn_nueva_cot";
            this.btn_nueva_cot.Size = new System.Drawing.Size(128, 42);
            this.btn_nueva_cot.TabIndex = 3;
            this.btn_nueva_cot.Text = "Nueva Cotizacion";
            this.btn_nueva_cot.UseVisualStyleBackColor = true;
            this.btn_nueva_cot.Click += new System.EventHandler(this.btn_nueva_cot_Click);
            // 
            // btn_afectar_vale
            // 
            this.btn_afectar_vale.Location = new System.Drawing.Point(21, 186);
            this.btn_afectar_vale.Margin = new System.Windows.Forms.Padding(2);
            this.btn_afectar_vale.Name = "btn_afectar_vale";
            this.btn_afectar_vale.Size = new System.Drawing.Size(127, 42);
            this.btn_afectar_vale.TabIndex = 4;
            this.btn_afectar_vale.Text = "Afectar Vale (Envio)";
            this.btn_afectar_vale.UseVisualStyleBackColor = true;
            this.btn_afectar_vale.Click += new System.EventHandler(this.btn_afectar_vale_Click);
            // 
            // lbl_vendedor_nombre
            // 
            this.lbl_vendedor_nombre.AutoSize = true;
            this.lbl_vendedor_nombre.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_vendedor_nombre.Location = new System.Drawing.Point(42, 33);
            this.lbl_vendedor_nombre.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_vendedor_nombre.Name = "lbl_vendedor_nombre";
            this.lbl_vendedor_nombre.Size = new System.Drawing.Size(117, 24);
            this.lbl_vendedor_nombre.TabIndex = 9;
            this.lbl_vendedor_nombre.Text = "VENDEDOR";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Enabled = false;
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(938, 37);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(98, 20);
            this.dateTimePicker1.TabIndex = 10;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clientesToolStripMenuItem,
            this.articulosToolStripMenuItem,
            this.entregasToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1088, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // clientesToolStripMenuItem
            // 
            this.clientesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.misFacturasToolStripMenuItem,
            this.estadosDeCuentaToolStripMenuItem});
            this.clientesToolStripMenuItem.Name = "clientesToolStripMenuItem";
            this.clientesToolStripMenuItem.Size = new System.Drawing.Size(61, 22);
            this.clientesToolStripMenuItem.Text = "Clientes";
            // 
            // misFacturasToolStripMenuItem
            // 
            this.misFacturasToolStripMenuItem.Name = "misFacturasToolStripMenuItem";
            this.misFacturasToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.misFacturasToolStripMenuItem.Text = "Mis Facturas";
            // 
            // estadosDeCuentaToolStripMenuItem
            // 
            this.estadosDeCuentaToolStripMenuItem.Name = "estadosDeCuentaToolStripMenuItem";
            this.estadosDeCuentaToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.estadosDeCuentaToolStripMenuItem.Text = "Estados de Cuenta";
            // 
            // articulosToolStripMenuItem
            // 
            this.articulosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.articulosNuevosToolStripMenuItem,
            this.cambiosDePreciosToolStripMenuItem,
            this.articulosAImpulsarToolStripMenuItem});
            this.articulosToolStripMenuItem.Name = "articulosToolStripMenuItem";
            this.articulosToolStripMenuItem.Size = new System.Drawing.Size(66, 22);
            this.articulosToolStripMenuItem.Text = "Articulos";
            // 
            // articulosNuevosToolStripMenuItem
            // 
            this.articulosNuevosToolStripMenuItem.Name = "articulosNuevosToolStripMenuItem";
            this.articulosNuevosToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.articulosNuevosToolStripMenuItem.Text = "Articulos Nuevos";
            // 
            // cambiosDePreciosToolStripMenuItem
            // 
            this.cambiosDePreciosToolStripMenuItem.Name = "cambiosDePreciosToolStripMenuItem";
            this.cambiosDePreciosToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.cambiosDePreciosToolStripMenuItem.Text = "Cambios de Precios";
            // 
            // articulosAImpulsarToolStripMenuItem
            // 
            this.articulosAImpulsarToolStripMenuItem.Name = "articulosAImpulsarToolStripMenuItem";
            this.articulosAImpulsarToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.articulosAImpulsarToolStripMenuItem.Text = "Articulos a Impulsar";
            // 
            // entregasToolStripMenuItem
            // 
            this.entregasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.entregasToolStripMenuItem1});
            this.entregasToolStripMenuItem.Name = "entregasToolStripMenuItem";
            this.entregasToolStripMenuItem.Size = new System.Drawing.Size(64, 22);
            this.entregasToolStripMenuItem.Text = "Entregas";
            // 
            // entregasToolStripMenuItem1
            // 
            this.entregasToolStripMenuItem1.Name = "entregasToolStripMenuItem1";
            this.entregasToolStripMenuItem1.Size = new System.Drawing.Size(119, 22);
            this.entregasToolStripMenuItem1.Text = "Entregas";
            // 
            // lbl_vales_entresus
            // 
            this.lbl_vales_entresus.AutoSize = true;
            this.lbl_vales_entresus.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_vales_entresus.Location = new System.Drawing.Point(382, 88);
            this.lbl_vales_entresus.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_vales_entresus.Name = "lbl_vales_entresus";
            this.lbl_vales_entresus.Size = new System.Drawing.Size(153, 24);
            this.lbl_vales_entresus.TabIndex = 12;
            this.lbl_vales_entresus.Text = "Entre Sucursales";
            // 
            // lbl_vales_defacts
            // 
            this.lbl_vales_defacts.AutoSize = true;
            this.lbl_vales_defacts.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_vales_defacts.Location = new System.Drawing.Point(793, 88);
            this.lbl_vales_defacts.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbl_vales_defacts.Name = "lbl_vales_defacts";
            this.lbl_vales_defacts.Size = new System.Drawing.Size(111, 24);
            this.lbl_vales_defacts.TabIndex = 13;
            this.lbl_vales_defacts.Text = "De Facturas";
            // 
            // txt_pedido
            // 
            this.txt_pedido.Location = new System.Drawing.Point(22, 161);
            this.txt_pedido.Name = "txt_pedido";
            this.txt_pedido.Size = new System.Drawing.Size(125, 20);
            this.txt_pedido.TabIndex = 14;
            this.txt_pedido.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_pedido.UseSystemPasswordChar = true;
            // 
            // txt_factura
            // 
            this.txt_factura.Location = new System.Drawing.Point(21, 44);
            this.txt_factura.Name = "txt_factura";
            this.txt_factura.Size = new System.Drawing.Size(127, 20);
            this.txt_factura.TabIndex = 15;
            this.txt_factura.Text = "46056596";
            this.txt_factura.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 18);
            this.label2.TabIndex = 16;
            this.label2.Text = "Escanear Pedido";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(22, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 18);
            this.label3.TabIndex = 17;
            this.label3.Text = "Escanear Factura";
            // 
            // btn_cargar_cot
            // 
            this.btn_cargar_cot.Location = new System.Drawing.Point(21, 29);
            this.btn_cargar_cot.Margin = new System.Windows.Forms.Padding(2);
            this.btn_cargar_cot.Name = "btn_cargar_cot";
            this.btn_cargar_cot.Size = new System.Drawing.Size(128, 42);
            this.btn_cargar_cot.TabIndex = 18;
            this.btn_cargar_cot.Text = "Cargar Cotizacion";
            this.btn_cargar_cot.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox1.Controls.Add(this.txt_factura);
            this.groupBox1.Controls.Add(this.btn_nuevo_vale);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btn_afectar_vale);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_pedido);
            this.groupBox1.Location = new System.Drawing.Point(46, 114);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(171, 252);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Vales";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox2.Controls.Add(this.btn_cargar_cot);
            this.groupBox2.Controls.Add(this.btn_nueva_cot);
            this.groupBox2.Location = new System.Drawing.Point(46, 381);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(171, 138);
            this.groupBox2.TabIndex = 20;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cotizaciones";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1088, 563);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbl_vales_defacts);
            this.Controls.Add(this.lbl_vales_entresus);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.lbl_vendedor_nombre);
            this.Controls.Add(this.dgv_valefact);
            this.Controls.Add(this.dgv_valeentresus);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Vales JMRS";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_valeentresus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_valefact)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_valeentresus;
        private System.Windows.Forms.DataGridView dgv_valefact;
        private System.Windows.Forms.Button btn_nuevo_vale;
        private System.Windows.Forms.Button btn_nueva_cot;
        private System.Windows.Forms.Button btn_afectar_vale;
        private System.Windows.Forms.Label lbl_vendedor_nombre;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem clientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem misFacturasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem estadosDeCuentaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem articulosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem articulosNuevosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cambiosDePreciosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem articulosAImpulsarToolStripMenuItem;
        private System.Windows.Forms.Label lbl_vales_entresus;
        private System.Windows.Forms.Label lbl_vales_defacts;
        private System.Windows.Forms.TextBox txt_pedido;
        private System.Windows.Forms.TextBox txt_factura;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn es_folio;
        private System.Windows.Forms.DataGridViewTextBoxColumn es_fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn es_origen;
        private System.Windows.Forms.DataGridViewTextBoxColumn es_solicita;
        private System.Windows.Forms.DataGridViewTextBoxColumn es_chofer;
        private System.Windows.Forms.DataGridViewTextBoxColumn es_estatus;
        private System.Windows.Forms.ToolStripMenuItem entregasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem entregasToolStripMenuItem1;
        private System.Windows.Forms.Button btn_cargar_cot;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridViewTextBoxColumn df_folio;
        private System.Windows.Forms.DataGridViewTextBoxColumn df_fecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn df_origen;
        private System.Windows.Forms.DataGridViewTextBoxColumn df_solicita;
        private System.Windows.Forms.DataGridViewTextBoxColumn df_chofer;
        private System.Windows.Forms.DataGridViewTextBoxColumn df_factura;
    }
}


namespace Niveles
{
    partial class FormValeNuevoDF
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
            this.dgv_articulos = new System.Windows.Forms.DataGridView();
            this.arts_nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.arts_um = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.arts_unidades = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.arts_apedir = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.arts_entregadas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.arts_pendientes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.arts_art_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.arts_detid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_vale_completo = new System.Windows.Forms.Button();
            this.btc_vale = new System.Windows.Forms.Button();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txt_telefono = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_devolucion = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_direccion = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_vendedor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_cliente = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_importe = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_fecha = new System.Windows.Forms.TextBox();
            this.lbl_factura = new System.Windows.Forms.Label();
            this.txt_folio_factura = new System.Windows.Forms.TextBox();
            this.cbo_cobradores = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_articulos)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgv_articulos
            // 
            this.dgv_articulos.AllowUserToAddRows = false;
            this.dgv_articulos.AllowUserToDeleteRows = false;
            this.dgv_articulos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_articulos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_articulos.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.arts_nombre,
            this.arts_um,
            this.arts_unidades,
            this.arts_apedir,
            this.arts_entregadas,
            this.arts_pendientes,
            this.arts_art_id,
            this.arts_detid});
            this.dgv_articulos.Location = new System.Drawing.Point(21, 142);
            this.dgv_articulos.Name = "dgv_articulos";
            this.dgv_articulos.RowHeadersVisible = false;
            this.dgv_articulos.Size = new System.Drawing.Size(917, 468);
            this.dgv_articulos.TabIndex = 0;
            // 
            // arts_nombre
            // 
            this.arts_nombre.HeaderText = "Articulo";
            this.arts_nombre.Name = "arts_nombre";
            // 
            // arts_um
            // 
            this.arts_um.HeaderText = "Ud";
            this.arts_um.Name = "arts_um";
            // 
            // arts_unidades
            // 
            this.arts_unidades.HeaderText = "Unidades";
            this.arts_unidades.Name = "arts_unidades";
            // 
            // arts_apedir
            // 
            this.arts_apedir.HeaderText = "Solicitar";
            this.arts_apedir.Name = "arts_apedir";
            // 
            // arts_entregadas
            // 
            this.arts_entregadas.HeaderText = "Entregadas";
            this.arts_entregadas.Name = "arts_entregadas";
            // 
            // arts_pendientes
            // 
            this.arts_pendientes.HeaderText = "Pendientes";
            this.arts_pendientes.Name = "arts_pendientes";
            // 
            // arts_art_id
            // 
            this.arts_art_id.HeaderText = "art_id";
            this.arts_art_id.Name = "arts_art_id";
            // 
            // arts_detid
            // 
            this.arts_detid.HeaderText = "art_det_id";
            this.arts_detid.Name = "arts_detid";
            // 
            // btn_vale_completo
            // 
            this.btn_vale_completo.Location = new System.Drawing.Point(815, 52);
            this.btn_vale_completo.Name = "btn_vale_completo";
            this.btn_vale_completo.Size = new System.Drawing.Size(123, 50);
            this.btn_vale_completo.TabIndex = 1;
            this.btn_vale_completo.Text = "SOLICITAR TODO EN FACTURA";
            this.btn_vale_completo.UseVisualStyleBackColor = true;
            // 
            // btc_vale
            // 
            this.btc_vale.Location = new System.Drawing.Point(853, 616);
            this.btc_vale.Name = "btc_vale";
            this.btc_vale.Size = new System.Drawing.Size(84, 31);
            this.btc_vale.TabIndex = 2;
            this.btc_vale.Text = "Solicitar Vale";
            this.btc_vale.UseVisualStyleBackColor = true;
            this.btc_vale.Click += new System.EventHandler(this.btc_vale_Click);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(836, 12);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(102, 20);
            this.dateTimePicker1.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txt_telefono);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txt_devolucion);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txt_direccion);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_vendedor);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_cliente);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txt_importe);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_fecha);
            this.groupBox1.Controls.Add(this.lbl_factura);
            this.groupBox1.Controls.Add(this.txt_folio_factura);
            this.groupBox1.Location = new System.Drawing.Point(21, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(747, 124);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Datos de Factura";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(560, 91);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "Telefono:";
            // 
            // txt_telefono
            // 
            this.txt_telefono.Location = new System.Drawing.Point(620, 87);
            this.txt_telefono.Name = "txt_telefono";
            this.txt_telefono.ReadOnly = true;
            this.txt_telefono.Size = new System.Drawing.Size(100, 20);
            this.txt_telefono.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(550, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Devolucion:";
            // 
            // txt_devolucion
            // 
            this.txt_devolucion.Location = new System.Drawing.Point(620, 56);
            this.txt_devolucion.Name = "txt_devolucion";
            this.txt_devolucion.ReadOnly = true;
            this.txt_devolucion.Size = new System.Drawing.Size(100, 20);
            this.txt_devolucion.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(158, 60);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(58, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Direccion: ";
            // 
            // txt_direccion
            // 
            this.txt_direccion.Location = new System.Drawing.Point(220, 56);
            this.txt_direccion.Multiline = true;
            this.txt_direccion.Name = "txt_direccion";
            this.txt_direccion.ReadOnly = true;
            this.txt_direccion.Size = new System.Drawing.Size(316, 51);
            this.txt_direccion.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(555, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Vendedor: ";
            // 
            // txt_vendedor
            // 
            this.txt_vendedor.Location = new System.Drawing.Point(620, 25);
            this.txt_vendedor.Name = "txt_vendedor";
            this.txt_vendedor.ReadOnly = true;
            this.txt_vendedor.Size = new System.Drawing.Size(100, 20);
            this.txt_vendedor.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(172, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Cliente:";
            // 
            // txt_cliente
            // 
            this.txt_cliente.Location = new System.Drawing.Point(220, 25);
            this.txt_cliente.Name = "txt_cliente";
            this.txt_cliente.ReadOnly = true;
            this.txt_cliente.Size = new System.Drawing.Size(316, 20);
            this.txt_cliente.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Importe:";
            // 
            // txt_importe
            // 
            this.txt_importe.Location = new System.Drawing.Point(52, 87);
            this.txt_importe.Name = "txt_importe";
            this.txt_importe.ReadOnly = true;
            this.txt_importe.Size = new System.Drawing.Size(100, 20);
            this.txt_importe.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Fecha:";
            // 
            // txt_fecha
            // 
            this.txt_fecha.Location = new System.Drawing.Point(52, 25);
            this.txt_fecha.Name = "txt_fecha";
            this.txt_fecha.ReadOnly = true;
            this.txt_fecha.Size = new System.Drawing.Size(100, 20);
            this.txt_fecha.TabIndex = 2;
            // 
            // lbl_factura
            // 
            this.lbl_factura.AutoSize = true;
            this.lbl_factura.Location = new System.Drawing.Point(19, 60);
            this.lbl_factura.Name = "lbl_factura";
            this.lbl_factura.Size = new System.Drawing.Size(32, 13);
            this.lbl_factura.TabIndex = 1;
            this.lbl_factura.Text = "Folio:";
            // 
            // txt_folio_factura
            // 
            this.txt_folio_factura.Location = new System.Drawing.Point(52, 56);
            this.txt_folio_factura.Name = "txt_folio_factura";
            this.txt_folio_factura.ReadOnly = true;
            this.txt_folio_factura.Size = new System.Drawing.Size(100, 20);
            this.txt_folio_factura.TabIndex = 0;
            // 
            // cbo_cobradores
            // 
            this.cbo_cobradores.FormattingEnabled = true;
            this.cbo_cobradores.Location = new System.Drawing.Point(815, 115);
            this.cbo_cobradores.Name = "cbo_cobradores";
            this.cbo_cobradores.Size = new System.Drawing.Size(122, 21);
            this.cbo_cobradores.TabIndex = 5;
            // 
            // FormValeNuevoDF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1138, 701);
            this.Controls.Add(this.cbo_cobradores);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.btc_vale);
            this.Controls.Add(this.btn_vale_completo);
            this.Controls.Add(this.dgv_articulos);
            this.Name = "FormValeNuevoDF";
            this.Text = "FormValeNuevoDF";
            this.Load += new System.EventHandler(this.FormValeNuevoDF_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_articulos)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_articulos;
        private System.Windows.Forms.Button btn_vale_completo;
        private System.Windows.Forms.Button btc_vale;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_factura;
        private System.Windows.Forms.TextBox txt_folio_factura;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_direccion;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_vendedor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_cliente;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_importe;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_fecha;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txt_telefono;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_devolucion;
        private System.Windows.Forms.DataGridViewTextBoxColumn arts_nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn arts_um;
        private System.Windows.Forms.DataGridViewTextBoxColumn arts_unidades;
        private System.Windows.Forms.DataGridViewTextBoxColumn arts_apedir;
        private System.Windows.Forms.DataGridViewTextBoxColumn arts_entregadas;
        private System.Windows.Forms.DataGridViewTextBoxColumn arts_pendientes;
        private System.Windows.Forms.DataGridViewTextBoxColumn arts_art_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn arts_detid;
        private System.Windows.Forms.ComboBox cbo_cobradores;
    }
}
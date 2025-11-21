namespace Niveles
{
    partial class FormCotizacion
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
            this.dgv_articulos_cotizar = new System.Windows.Forms.DataGridView();
            this.Articulo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ud = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precio_cot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SubTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.art_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_nombre_art = new System.Windows.Forms.TextBox();
            this.btn_buscar = new System.Windows.Forms.Button();
            this.dgv_busca_arts = new System.Windows.Forms.DataGridView();
            this.art_nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.art_ud = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precio_lsta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.artid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gb_datos_cliente = new System.Windows.Forms.GroupBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_nombre_cliente = new System.Windows.Forms.TextBox();
            this.dgv_busca_clientes = new System.Windows.Forms.DataGridView();
            this.c_cte_nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c_cte_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_articulos_cotizar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_busca_arts)).BeginInit();
            this.gb_datos_cliente.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_busca_clientes)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv_articulos_cotizar
            // 
            this.dgv_articulos_cotizar.AllowUserToAddRows = false;
            this.dgv_articulos_cotizar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_articulos_cotizar.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Articulo,
            this.ud,
            this.precio_cot,
            this.cantidad,
            this.SubTotal,
            this.total,
            this.art_id});
            this.dgv_articulos_cotizar.Location = new System.Drawing.Point(12, 221);
            this.dgv_articulos_cotizar.Name = "dgv_articulos_cotizar";
            this.dgv_articulos_cotizar.ReadOnly = true;
            this.dgv_articulos_cotizar.RowHeadersVisible = false;
            this.dgv_articulos_cotizar.Size = new System.Drawing.Size(869, 339);
            this.dgv_articulos_cotizar.TabIndex = 1;
            this.dgv_articulos_cotizar.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_articulos_cotizar_CellContentClick);
            // 
            // Articulo
            // 
            this.Articulo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Articulo.HeaderText = "Articulo";
            this.Articulo.Name = "Articulo";
            this.Articulo.ReadOnly = true;
            // 
            // ud
            // 
            this.ud.HeaderText = "Unidad";
            this.ud.Name = "ud";
            this.ud.ReadOnly = true;
            // 
            // precio_cot
            // 
            this.precio_cot.HeaderText = "Precio";
            this.precio_cot.Name = "precio_cot";
            this.precio_cot.ReadOnly = true;
            // 
            // cantidad
            // 
            this.cantidad.HeaderText = "Unidades";
            this.cantidad.Name = "cantidad";
            this.cantidad.ReadOnly = true;
            // 
            // SubTotal
            // 
            this.SubTotal.HeaderText = "Sub-Total";
            this.SubTotal.Name = "SubTotal";
            this.SubTotal.ReadOnly = true;
            // 
            // total
            // 
            this.total.HeaderText = "Total";
            this.total.Name = "total";
            this.total.ReadOnly = true;
            // 
            // art_id
            // 
            this.art_id.HeaderText = "ART_ID";
            this.art_id.Name = "art_id";
            this.art_id.ReadOnly = true;
            // 
            // txt_nombre_art
            // 
            this.txt_nombre_art.Location = new System.Drawing.Point(12, 170);
            this.txt_nombre_art.Name = "txt_nombre_art";
            this.txt_nombre_art.Size = new System.Drawing.Size(482, 20);
            this.txt_nombre_art.TabIndex = 1;
            this.txt_nombre_art.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_nombre_art_KeyDown);
            // 
            // btn_buscar
            // 
            this.btn_buscar.Location = new System.Drawing.Point(806, 170);
            this.btn_buscar.Name = "btn_buscar";
            this.btn_buscar.Size = new System.Drawing.Size(75, 23);
            this.btn_buscar.TabIndex = 2;
            this.btn_buscar.Text = "button1";
            this.btn_buscar.UseVisualStyleBackColor = true;
            this.btn_buscar.Click += new System.EventHandler(this.btn_buscar_Click);
            // 
            // dgv_busca_arts
            // 
            this.dgv_busca_arts.AllowUserToAddRows = false;
            this.dgv_busca_arts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_busca_arts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.art_nombre,
            this.art_ud,
            this.precio_lsta,
            this.artid});
            this.dgv_busca_arts.Location = new System.Drawing.Point(12, 196);
            this.dgv_busca_arts.MultiSelect = false;
            this.dgv_busca_arts.Name = "dgv_busca_arts";
            this.dgv_busca_arts.ReadOnly = true;
            this.dgv_busca_arts.RowHeadersVisible = false;
            this.dgv_busca_arts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_busca_arts.Size = new System.Drawing.Size(482, 330);
            this.dgv_busca_arts.TabIndex = 0;
            this.dgv_busca_arts.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dgv_busca_arts_KeyDown);
            // 
            // art_nombre
            // 
            this.art_nombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.art_nombre.HeaderText = "Articulo";
            this.art_nombre.Name = "art_nombre";
            this.art_nombre.ReadOnly = true;
            // 
            // art_ud
            // 
            this.art_ud.HeaderText = "Ud";
            this.art_ud.Name = "art_ud";
            this.art_ud.ReadOnly = true;
            this.art_ud.Width = 60;
            // 
            // precio_lsta
            // 
            this.precio_lsta.HeaderText = "Precio";
            this.precio_lsta.Name = "precio_lsta";
            this.precio_lsta.ReadOnly = true;
            // 
            // artid
            // 
            this.artid.HeaderText = "ART_ID";
            this.artid.Name = "artid";
            this.artid.ReadOnly = true;
            // 
            // gb_datos_cliente
            // 
            this.gb_datos_cliente.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.gb_datos_cliente.Controls.Add(this.textBox9);
            this.gb_datos_cliente.Controls.Add(this.label10);
            this.gb_datos_cliente.Controls.Add(this.textBox8);
            this.gb_datos_cliente.Controls.Add(this.label9);
            this.gb_datos_cliente.Controls.Add(this.textBox7);
            this.gb_datos_cliente.Controls.Add(this.label8);
            this.gb_datos_cliente.Controls.Add(this.textBox5);
            this.gb_datos_cliente.Controls.Add(this.label6);
            this.gb_datos_cliente.Controls.Add(this.textBox4);
            this.gb_datos_cliente.Controls.Add(this.label5);
            this.gb_datos_cliente.Controls.Add(this.textBox3);
            this.gb_datos_cliente.Controls.Add(this.label4);
            this.gb_datos_cliente.Controls.Add(this.textBox6);
            this.gb_datos_cliente.Controls.Add(this.label7);
            this.gb_datos_cliente.Controls.Add(this.textBox2);
            this.gb_datos_cliente.Controls.Add(this.label3);
            this.gb_datos_cliente.Controls.Add(this.label2);
            this.gb_datos_cliente.Controls.Add(this.textBox1);
            this.gb_datos_cliente.Controls.Add(this.label1);
            this.gb_datos_cliente.Controls.Add(this.txt_nombre_cliente);
            this.gb_datos_cliente.Location = new System.Drawing.Point(12, 12);
            this.gb_datos_cliente.Name = "gb_datos_cliente";
            this.gb_datos_cliente.Size = new System.Drawing.Size(868, 152);
            this.gb_datos_cliente.TabIndex = 3;
            this.gb_datos_cliente.TabStop = false;
            this.gb_datos_cliente.Text = "Cliente";
            // 
            // textBox9
            // 
            this.textBox9.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox9.Location = new System.Drawing.Point(708, 19);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(154, 20);
            this.textBox9.TabIndex = 29;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(636, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(71, 13);
            this.label10.TabIndex = 28;
            this.label10.Text = "Condiciones: ";
            // 
            // textBox8
            // 
            this.textBox8.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox8.Location = new System.Drawing.Point(513, 75);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(135, 20);
            this.textBox8.TabIndex = 27;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(463, 78);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(40, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "Precio:";
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox7.Location = new System.Drawing.Point(513, 49);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(135, 20);
            this.textBox7.TabIndex = 25;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(463, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(40, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "R.F.C.:";
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox5.Location = new System.Drawing.Point(513, 101);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(135, 20);
            this.textBox5.TabIndex = 23;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(463, 104);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Estatus";
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox4.Location = new System.Drawing.Point(708, 101);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(154, 20);
            this.textBox4.TabIndex = 21;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(654, 104);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Tipo:";
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox3.Location = new System.Drawing.Point(708, 75);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(154, 20);
            this.textBox3.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(654, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 18;
            this.label4.Text = "Vendedor:";
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox6.Location = new System.Drawing.Point(708, 49);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(154, 20);
            this.textBox6.TabIndex = 17;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(654, 52);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Cobrador:";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox2.Location = new System.Drawing.Point(74, 75);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(377, 46);
            this.textBox2.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Direccion:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Buscar x Nombre: ";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox1.Location = new System.Drawing.Point(74, 49);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(377, 20);
            this.textBox1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Nombre:";
            // 
            // txt_nombre_cliente
            // 
            this.txt_nombre_cliente.Location = new System.Drawing.Point(120, 19);
            this.txt_nombre_cliente.Name = "txt_nombre_cliente";
            this.txt_nombre_cliente.Size = new System.Drawing.Size(510, 20);
            this.txt_nombre_cliente.TabIndex = 4;
            this.txt_nombre_cliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_nombre_cliente_KeyDown);
            // 
            // dgv_busca_clientes
            // 
            this.dgv_busca_clientes.AllowUserToAddRows = false;
            this.dgv_busca_clientes.AllowUserToDeleteRows = false;
            this.dgv_busca_clientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_busca_clientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.c_cte_nombre,
            this.c_cte_id});
            this.dgv_busca_clientes.Location = new System.Drawing.Point(132, 57);
            this.dgv_busca_clientes.MultiSelect = false;
            this.dgv_busca_clientes.Name = "dgv_busca_clientes";
            this.dgv_busca_clientes.ReadOnly = true;
            this.dgv_busca_clientes.RowHeadersVisible = false;
            this.dgv_busca_clientes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_busca_clientes.Size = new System.Drawing.Size(510, 330);
            this.dgv_busca_clientes.TabIndex = 4;
            // 
            // c_cte_nombre
            // 
            this.c_cte_nombre.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.c_cte_nombre.HeaderText = "Nombre";
            this.c_cte_nombre.Name = "c_cte_nombre";
            this.c_cte_nombre.ReadOnly = true;
            // 
            // c_cte_id
            // 
            this.c_cte_id.HeaderText = "cliente_id";
            this.c_cte_id.Name = "c_cte_id";
            this.c_cte_id.ReadOnly = true;
            this.c_cte_id.Width = 65;
            // 
            // FormCotizacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(944, 588);
            this.Controls.Add(this.dgv_busca_clientes);
            this.Controls.Add(this.gb_datos_cliente);
            this.Controls.Add(this.dgv_busca_arts);
            this.Controls.Add(this.btn_buscar);
            this.Controls.Add(this.txt_nombre_art);
            this.Controls.Add(this.dgv_articulos_cotizar);
            this.Name = "FormCotizacion";
            this.Text = "FormCotizacion";
            this.Load += new System.EventHandler(this.FormCotizacion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_articulos_cotizar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_busca_arts)).EndInit();
            this.gb_datos_cliente.ResumeLayout(false);
            this.gb_datos_cliente.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_busca_clientes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_articulos_cotizar;
        private System.Windows.Forms.TextBox txt_nombre_art;
        private System.Windows.Forms.Button btn_buscar;
        private System.Windows.Forms.DataGridView dgv_busca_arts;
        private System.Windows.Forms.DataGridViewTextBoxColumn Articulo;
        private System.Windows.Forms.DataGridViewTextBoxColumn ud;
        private System.Windows.Forms.DataGridViewTextBoxColumn precio_cot;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn SubTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn total;
        private System.Windows.Forms.DataGridViewTextBoxColumn art_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn art_nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn art_ud;
        private System.Windows.Forms.DataGridViewTextBoxColumn precio_lsta;
        private System.Windows.Forms.DataGridViewTextBoxColumn artid;
        private System.Windows.Forms.GroupBox gb_datos_cliente;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_nombre_cliente;
        private System.Windows.Forms.DataGridView dgv_busca_clientes;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_cte_nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn c_cte_id;
    }
}
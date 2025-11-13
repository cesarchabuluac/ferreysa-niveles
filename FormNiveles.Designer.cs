namespace Vales
{
    partial class FormNiveles
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNiveles));
            this.cbo_grupos = new System.Windows.Forms.ComboBox();
            this.dgv_arts = new System.Windows.Forms.DataGridView();
            this.artid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clave = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.desc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.um = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gpo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.inv = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.min = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reord = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.max = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sugerencia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reordauto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btn_general = new System.Windows.Forms.Button();
            this.btnVistaValor = new System.Windows.Forms.Button();
            this.cbo_almacenes = new System.Windows.Forms.ComboBox();
            this.tbx_sobreinv = new System.Windows.Forms.TextBox();
            this.tbx_subinv = new System.Windows.Forms.TextBox();
            this.tbx_normal = new System.Windows.Forms.TextBox();
            this.tbx_pedir = new System.Windows.Forms.TextBox();
            this.dgv_exis = new System.Windows.Forms.DataGridView();
            this.artid2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.almacennnnid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.almacena = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.exxis = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.miin = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reeeord = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.maax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_ocultar_sobreinv = new System.Windows.Forms.Button();
            this.tb_ocultar_subinv = new System.Windows.Forms.Button();
            this.btn_ocultar_normal = new System.Windows.Forms.Button();
            this.btn_ocultar_pedir = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_ocultar_sin_reord = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tbx_sinreord = new System.Windows.Forms.TextBox();
            this.btn_copiar_portapapeles = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.cbo_clasificaciones = new System.Windows.Forms.ComboBox();
            this.tbx_art = new System.Windows.Forms.TextBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnShowPanelValues = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_arts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_exis)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbo_grupos
            // 
            this.cbo_grupos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_grupos.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_grupos.FormattingEnabled = true;
            this.cbo_grupos.Location = new System.Drawing.Point(515, 5);
            this.cbo_grupos.Margin = new System.Windows.Forms.Padding(4);
            this.cbo_grupos.Name = "cbo_grupos";
            this.cbo_grupos.Size = new System.Drawing.Size(304, 33);
            this.cbo_grupos.TabIndex = 0;
            this.cbo_grupos.SelectedIndexChanged += new System.EventHandler(this.cbo_grupos_SelectedIndexChanged);
            // 
            // dgv_arts
            // 
            this.dgv_arts.AllowUserToAddRows = false;
            this.dgv_arts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_arts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.artid,
            this.clave,
            this.desc,
            this.um,
            this.gpo,
            this.inv,
            this.min,
            this.reord,
            this.max,
            this.sugerencia,
            this.reordauto,
            this.rot});
            this.dgv_arts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_arts.Location = new System.Drawing.Point(4, 124);
            this.dgv_arts.Margin = new System.Windows.Forms.Padding(4);
            this.dgv_arts.MultiSelect = false;
            this.dgv_arts.Name = "dgv_arts";
            this.dgv_arts.ReadOnly = true;
            this.dgv_arts.RowHeadersVisible = false;
            this.dgv_arts.RowHeadersWidth = 51;
            this.dgv_arts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_arts.Size = new System.Drawing.Size(1240, 374);
            this.dgv_arts.TabIndex = 1;
            this.dgv_arts.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_arts_CellContentDoubleClick);
            this.dgv_arts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_arts_CellDoubleClick);
            this.dgv_arts.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_arts_CellEnter);
            // 
            // artid
            // 
            this.artid.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.artid.HeaderText = "artid";
            this.artid.MinimumWidth = 6;
            this.artid.Name = "artid";
            this.artid.ReadOnly = true;
            this.artid.Visible = false;
            this.artid.Width = 125;
            // 
            // clave
            // 
            this.clave.HeaderText = "Clave";
            this.clave.MinimumWidth = 6;
            this.clave.Name = "clave";
            this.clave.ReadOnly = true;
            this.clave.Width = 125;
            // 
            // desc
            // 
            this.desc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.desc.HeaderText = "Descripcion";
            this.desc.MinimumWidth = 6;
            this.desc.Name = "desc";
            this.desc.ReadOnly = true;
            // 
            // um
            // 
            this.um.HeaderText = "Um";
            this.um.MinimumWidth = 6;
            this.um.Name = "um";
            this.um.ReadOnly = true;
            this.um.Width = 40;
            // 
            // gpo
            // 
            this.gpo.HeaderText = "Valor";
            this.gpo.MinimumWidth = 6;
            this.gpo.Name = "gpo";
            this.gpo.ReadOnly = true;
            this.gpo.Width = 150;
            // 
            // inv
            // 
            this.inv.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.inv.HeaderText = "Existencia";
            this.inv.MinimumWidth = 6;
            this.inv.Name = "inv";
            this.inv.ReadOnly = true;
            this.inv.Width = 92;
            // 
            // min
            // 
            this.min.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.min.HeaderText = "Minimo";
            this.min.MinimumWidth = 6;
            this.min.Name = "min";
            this.min.ReadOnly = true;
            this.min.Width = 77;
            // 
            // reord
            // 
            this.reord.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.reord.HeaderText = "Reorden";
            this.reord.MinimumWidth = 6;
            this.reord.Name = "reord";
            this.reord.ReadOnly = true;
            this.reord.Width = 84;
            // 
            // max
            // 
            this.max.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.max.HeaderText = "Maximo";
            this.max.MinimumWidth = 6;
            this.max.Name = "max";
            this.max.ReadOnly = true;
            this.max.Width = 80;
            // 
            // sugerencia
            // 
            this.sugerencia.HeaderText = "Accion Sugerida";
            this.sugerencia.MinimumWidth = 6;
            this.sugerencia.Name = "sugerencia";
            this.sugerencia.ReadOnly = true;
            this.sugerencia.Width = 120;
            // 
            // reordauto
            // 
            this.reordauto.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.reordauto.HeaderText = "ReordAuto";
            this.reordauto.MinimumWidth = 6;
            this.reordauto.Name = "reordauto";
            this.reordauto.ReadOnly = true;
            this.reordauto.Width = 96;
            // 
            // rot
            // 
            this.rot.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.rot.HeaderText = "Vta Mensual";
            this.rot.MinimumWidth = 6;
            this.rot.Name = "rot";
            this.rot.ReadOnly = true;
            this.rot.Width = 98;
            // 
            // btn_general
            // 
            this.btn_general.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_general.Location = new System.Drawing.Point(8, 50);
            this.btn_general.Margin = new System.Windows.Forms.Padding(4);
            this.btn_general.Name = "btn_general";
            this.btn_general.Size = new System.Drawing.Size(570, 46);
            this.btn_general.TabIndex = 3;
            this.btn_general.Text = "Ver Todo lo Clasificado en la Clasificacion Seleccionada";
            this.btn_general.UseVisualStyleBackColor = true;
            this.btn_general.Click += new System.EventHandler(this.btn_general_Click);
            // 
            // btnVistaValor
            // 
            this.btnVistaValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVistaValor.Location = new System.Drawing.Point(611, 50);
            this.btnVistaValor.Margin = new System.Windows.Forms.Padding(4);
            this.btnVistaValor.Name = "btnVistaValor";
            this.btnVistaValor.Size = new System.Drawing.Size(305, 46);
            this.btnVistaValor.TabIndex = 4;
            this.btnVistaValor.Text = "Ver Lista Por Valor";
            this.btnVistaValor.UseVisualStyleBackColor = true;
            this.btnVistaValor.Click += new System.EventHandler(this.btnVistaValor_Click);
            // 
            // cbo_almacenes
            // 
            this.cbo_almacenes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_almacenes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_almacenes.FormattingEnabled = true;
            this.cbo_almacenes.Location = new System.Drawing.Point(981, 7);
            this.cbo_almacenes.Margin = new System.Windows.Forms.Padding(4);
            this.cbo_almacenes.Name = "cbo_almacenes";
            this.cbo_almacenes.Size = new System.Drawing.Size(304, 33);
            this.cbo_almacenes.TabIndex = 5;
            this.cbo_almacenes.SelectedIndexChanged += new System.EventHandler(this.cbo_almacenes_SelectedIndexChanged);
            // 
            // tbx_sobreinv
            // 
            this.tbx_sobreinv.Location = new System.Drawing.Point(21, 31);
            this.tbx_sobreinv.Margin = new System.Windows.Forms.Padding(4);
            this.tbx_sobreinv.Name = "tbx_sobreinv";
            this.tbx_sobreinv.Size = new System.Drawing.Size(104, 23);
            this.tbx_sobreinv.TabIndex = 6;
            // 
            // tbx_subinv
            // 
            this.tbx_subinv.Location = new System.Drawing.Point(397, 31);
            this.tbx_subinv.Margin = new System.Windows.Forms.Padding(4);
            this.tbx_subinv.Name = "tbx_subinv";
            this.tbx_subinv.Size = new System.Drawing.Size(104, 23);
            this.tbx_subinv.TabIndex = 7;
            // 
            // tbx_normal
            // 
            this.tbx_normal.Location = new System.Drawing.Point(151, 31);
            this.tbx_normal.Margin = new System.Windows.Forms.Padding(4);
            this.tbx_normal.Name = "tbx_normal";
            this.tbx_normal.Size = new System.Drawing.Size(104, 23);
            this.tbx_normal.TabIndex = 8;
            // 
            // tbx_pedir
            // 
            this.tbx_pedir.Location = new System.Drawing.Point(272, 31);
            this.tbx_pedir.Margin = new System.Windows.Forms.Padding(4);
            this.tbx_pedir.Name = "tbx_pedir";
            this.tbx_pedir.Size = new System.Drawing.Size(104, 23);
            this.tbx_pedir.TabIndex = 9;
            // 
            // dgv_exis
            // 
            this.dgv_exis.AllowUserToAddRows = false;
            this.dgv_exis.AllowUserToDeleteRows = false;
            this.dgv_exis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_exis.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.artid2,
            this.almacennnnid,
            this.almacena,
            this.exxis,
            this.miin,
            this.reeeord,
            this.maax,
            this.reee});
            this.dgv_exis.Location = new System.Drawing.Point(4, 32);
            this.dgv_exis.Margin = new System.Windows.Forms.Padding(4);
            this.dgv_exis.Name = "dgv_exis";
            this.dgv_exis.ReadOnly = true;
            this.dgv_exis.RowHeadersVisible = false;
            this.dgv_exis.RowHeadersWidth = 51;
            this.dgv_exis.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgv_exis.Size = new System.Drawing.Size(562, 180);
            this.dgv_exis.TabIndex = 10;
            this.dgv_exis.SelectionChanged += new System.EventHandler(this.dgv_exis_SelectionChanged);
            // 
            // artid2
            // 
            this.artid2.HeaderText = "artid";
            this.artid2.MinimumWidth = 6;
            this.artid2.Name = "artid2";
            this.artid2.ReadOnly = true;
            this.artid2.Visible = false;
            this.artid2.Width = 125;
            // 
            // almacennnnid
            // 
            this.almacennnnid.HeaderText = "almacenid";
            this.almacennnnid.MinimumWidth = 6;
            this.almacennnnid.Name = "almacennnnid";
            this.almacennnnid.ReadOnly = true;
            this.almacennnnid.Visible = false;
            this.almacennnnid.Width = 125;
            // 
            // almacena
            // 
            this.almacena.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.almacena.HeaderText = "Almacen";
            this.almacena.MinimumWidth = 6;
            this.almacena.Name = "almacena";
            this.almacena.ReadOnly = true;
            // 
            // exxis
            // 
            this.exxis.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.exxis.HeaderText = "Existencia";
            this.exxis.MinimumWidth = 6;
            this.exxis.Name = "exxis";
            this.exxis.ReadOnly = true;
            this.exxis.Width = 92;
            // 
            // miin
            // 
            this.miin.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.miin.HeaderText = "Minimo";
            this.miin.MinimumWidth = 6;
            this.miin.Name = "miin";
            this.miin.ReadOnly = true;
            this.miin.Width = 77;
            // 
            // reeeord
            // 
            this.reeeord.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.reeeord.HeaderText = "Reorden";
            this.reeeord.MinimumWidth = 6;
            this.reeeord.Name = "reeeord";
            this.reeeord.ReadOnly = true;
            this.reeeord.Width = 84;
            // 
            // maax
            // 
            this.maax.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.maax.HeaderText = "Maximo";
            this.maax.MinimumWidth = 6;
            this.maax.Name = "maax";
            this.maax.ReadOnly = true;
            this.maax.Width = 80;
            // 
            // reee
            // 
            this.reee.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.reee.HeaderText = "ReordAuto";
            this.reee.MinimumWidth = 6;
            this.reee.Name = "reee";
            this.reee.ReadOnly = true;
            this.reee.Width = 96;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Pink;
            this.label1.Location = new System.Drawing.Point(18, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "Sobreinventario:";
            // 
            // btn_ocultar_sobreinv
            // 
            this.btn_ocultar_sobreinv.Location = new System.Drawing.Point(21, 62);
            this.btn_ocultar_sobreinv.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ocultar_sobreinv.Name = "btn_ocultar_sobreinv";
            this.btn_ocultar_sobreinv.Size = new System.Drawing.Size(104, 31);
            this.btn_ocultar_sobreinv.TabIndex = 12;
            this.btn_ocultar_sobreinv.Text = "Ocultar ";
            this.btn_ocultar_sobreinv.UseVisualStyleBackColor = true;
            this.btn_ocultar_sobreinv.Click += new System.EventHandler(this.btn_ocultar_sobreinv_Click);
            // 
            // tb_ocultar_subinv
            // 
            this.tb_ocultar_subinv.Location = new System.Drawing.Point(397, 64);
            this.tb_ocultar_subinv.Margin = new System.Windows.Forms.Padding(4);
            this.tb_ocultar_subinv.Name = "tb_ocultar_subinv";
            this.tb_ocultar_subinv.Size = new System.Drawing.Size(104, 31);
            this.tb_ocultar_subinv.TabIndex = 13;
            this.tb_ocultar_subinv.Text = "Ocultar";
            this.tb_ocultar_subinv.UseVisualStyleBackColor = true;
            this.tb_ocultar_subinv.Click += new System.EventHandler(this.tb_ocultar_subinv_Click);
            // 
            // btn_ocultar_normal
            // 
            this.btn_ocultar_normal.Location = new System.Drawing.Point(151, 62);
            this.btn_ocultar_normal.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ocultar_normal.Name = "btn_ocultar_normal";
            this.btn_ocultar_normal.Size = new System.Drawing.Size(104, 31);
            this.btn_ocultar_normal.TabIndex = 14;
            this.btn_ocultar_normal.Text = "Ocultar";
            this.btn_ocultar_normal.UseVisualStyleBackColor = true;
            this.btn_ocultar_normal.Click += new System.EventHandler(this.btn_ocultar_normal_Click);
            // 
            // btn_ocultar_pedir
            // 
            this.btn_ocultar_pedir.Location = new System.Drawing.Point(272, 62);
            this.btn_ocultar_pedir.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ocultar_pedir.Name = "btn_ocultar_pedir";
            this.btn_ocultar_pedir.Size = new System.Drawing.Size(104, 31);
            this.btn_ocultar_pedir.TabIndex = 15;
            this.btn_ocultar_pedir.Text = "Ocultar";
            this.btn_ocultar_pedir.UseVisualStyleBackColor = true;
            this.btn_ocultar_pedir.Click += new System.EventHandler(this.btn_ocultar_pedir_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.LightCoral;
            this.label2.Location = new System.Drawing.Point(396, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 16);
            this.label2.TabIndex = 16;
            this.label2.Text = "Criticos";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.LightGreen;
            this.label3.Location = new System.Drawing.Point(148, 6);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 16);
            this.label3.TabIndex = 17;
            this.label3.Text = "Optimos";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Yellow;
            this.label4.Location = new System.Drawing.Point(269, 6);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 16);
            this.label4.TabIndex = 18;
            this.label4.Text = "Pedir";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btn_ocultar_sin_reord);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.tbx_sinreord);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.tbx_sobreinv);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tbx_subinv);
            this.panel1.Controls.Add(this.btn_ocultar_pedir);
            this.panel1.Controls.Add(this.tbx_normal);
            this.panel1.Controls.Add(this.btn_ocultar_normal);
            this.panel1.Controls.Add(this.tbx_pedir);
            this.panel1.Controls.Add(this.tb_ocultar_subinv);
            this.panel1.Controls.Add(this.btn_ocultar_sobreinv);
            this.panel1.Location = new System.Drawing.Point(574, 4);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(664, 103);
            this.panel1.TabIndex = 19;
            // 
            // btn_ocultar_sin_reord
            // 
            this.btn_ocultar_sin_reord.Location = new System.Drawing.Point(522, 62);
            this.btn_ocultar_sin_reord.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ocultar_sin_reord.Name = "btn_ocultar_sin_reord";
            this.btn_ocultar_sin_reord.Size = new System.Drawing.Size(104, 31);
            this.btn_ocultar_sin_reord.TabIndex = 22;
            this.btn_ocultar_sin_reord.Text = "Ocultar";
            this.btn_ocultar_sin_reord.UseVisualStyleBackColor = true;
            this.btn_ocultar_sin_reord.Click += new System.EventHandler(this.btn_ocultar_sin_reord_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.LightCyan;
            this.label5.Location = new System.Drawing.Point(519, 8);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 16);
            this.label5.TabIndex = 22;
            this.label5.Text = "Sin Reorden Autom:";
            // 
            // tbx_sinreord
            // 
            this.tbx_sinreord.Location = new System.Drawing.Point(522, 31);
            this.tbx_sinreord.Margin = new System.Windows.Forms.Padding(4);
            this.tbx_sinreord.Name = "tbx_sinreord";
            this.tbx_sinreord.Size = new System.Drawing.Size(104, 23);
            this.tbx_sinreord.TabIndex = 22;
            // 
            // btn_copiar_portapapeles
            // 
            this.btn_copiar_portapapeles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_copiar_portapapeles.Location = new System.Drawing.Point(945, 50);
            this.btn_copiar_portapapeles.Margin = new System.Windows.Forms.Padding(4);
            this.btn_copiar_portapapeles.Name = "btn_copiar_portapapeles";
            this.btn_copiar_portapapeles.Size = new System.Drawing.Size(340, 46);
            this.btn_copiar_portapapeles.TabIndex = 20;
            this.btn_copiar_portapapeles.Text = "Copiar a Portapapeles";
            this.btn_copiar_portapapeles.UseVisualStyleBackColor = true;
            this.btn_copiar_portapapeles.Click += new System.EventHandler(this.btn_copiar_portapapeles_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(448, 12);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(59, 20);
            this.label6.TabIndex = 21;
            this.label6.Text = "Valor:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(864, 10);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(109, 25);
            this.label7.TabIndex = 22;
            this.label7.Text = "Almacen: ";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.MediumAquamarine;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.cbo_clasificaciones);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.btn_copiar_portapapeles);
            this.panel2.Controls.Add(this.btn_general);
            this.panel2.Controls.Add(this.cbo_almacenes);
            this.panel2.Controls.Add(this.btnVistaValor);
            this.panel2.Controls.Add(this.cbo_grupos);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(4, 4);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1240, 112);
            this.panel2.TabIndex = 23;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(4, 12);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(124, 20);
            this.label8.TabIndex = 24;
            this.label8.Text = "Clasificacion:";
            // 
            // cbo_clasificaciones
            // 
            this.cbo_clasificaciones.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbo_clasificaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbo_clasificaciones.FormattingEnabled = true;
            this.cbo_clasificaciones.Location = new System.Drawing.Point(136, 5);
            this.cbo_clasificaciones.Margin = new System.Windows.Forms.Padding(4);
            this.cbo_clasificaciones.Name = "cbo_clasificaciones";
            this.cbo_clasificaciones.Size = new System.Drawing.Size(304, 33);
            this.cbo_clasificaciones.TabIndex = 23;
            this.cbo_clasificaciones.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // tbx_art
            // 
            this.tbx_art.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbx_art.Location = new System.Drawing.Point(4, 0);
            this.tbx_art.Margin = new System.Windows.Forms.Padding(4);
            this.tbx_art.Name = "tbx_art";
            this.tbx_art.ReadOnly = true;
            this.tbx_art.Size = new System.Drawing.Size(562, 30);
            this.tbx_art.TabIndex = 24;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.btnShowPanelValues);
            this.splitContainer1.Size = new System.Drawing.Size(1456, 765);
            this.splitContainer1.SplitterDistance = 1248;
            this.splitContainer1.TabIndex = 25;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgv_arts, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 382F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1248, 765);
            this.tableLayoutPanel1.TabIndex = 25;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tbx_art);
            this.panel3.Controls.Add(this.dgv_exis);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(3, 505);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1242, 257);
            this.panel3.TabIndex = 24;
            // 
            // btnShowPanelValues
            // 
            this.btnShowPanelValues.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.btnShowPanelValues.Location = new System.Drawing.Point(17, 18);
            this.btnShowPanelValues.Name = "btnShowPanelValues";
            this.btnShowPanelValues.Size = new System.Drawing.Size(175, 63);
            this.btnShowPanelValues.TabIndex = 0;
            this.btnShowPanelValues.Text = "Ver Panel Valores";
            this.btnShowPanelValues.UseVisualStyleBackColor = true;
            this.btnShowPanelValues.Click += new System.EventHandler(this.btnShowPanelValues_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FormNiveles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SteelBlue;
            this.ClientSize = new System.Drawing.Size(1456, 765);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormNiveles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Niveles de Inventario";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormNiveles_FormClosed);
            this.Load += new System.EventHandler(this.FormNiveles_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_arts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_exis)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbo_grupos;
        private System.Windows.Forms.DataGridView dgv_arts;
        private System.Windows.Forms.Button btn_general;
        private System.Windows.Forms.Button btnVistaValor;
        private System.Windows.Forms.ComboBox cbo_almacenes;
        private System.Windows.Forms.TextBox tbx_sobreinv;
        private System.Windows.Forms.TextBox tbx_subinv;
        private System.Windows.Forms.TextBox tbx_normal;
        private System.Windows.Forms.TextBox tbx_pedir;
        private System.Windows.Forms.DataGridView dgv_exis;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_ocultar_sobreinv;
        private System.Windows.Forms.Button tb_ocultar_subinv;
        private System.Windows.Forms.Button btn_ocultar_normal;
        private System.Windows.Forms.Button btn_ocultar_pedir;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_copiar_portapapeles;
        private System.Windows.Forms.Button btn_ocultar_sin_reord;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbx_sinreord;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridViewTextBoxColumn artid2;
        private System.Windows.Forms.DataGridViewTextBoxColumn almacennnnid;
        private System.Windows.Forms.DataGridViewTextBoxColumn almacena;
        private System.Windows.Forms.DataGridViewTextBoxColumn exxis;
        private System.Windows.Forms.DataGridViewTextBoxColumn miin;
        private System.Windows.Forms.DataGridViewTextBoxColumn reeeord;
        private System.Windows.Forms.DataGridViewTextBoxColumn maax;
        private System.Windows.Forms.DataGridViewTextBoxColumn reee;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox tbx_art;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cbo_clasificaciones;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridViewTextBoxColumn artid;
        private System.Windows.Forms.DataGridViewTextBoxColumn clave;
        private System.Windows.Forms.DataGridViewTextBoxColumn desc;
        private System.Windows.Forms.DataGridViewTextBoxColumn um;
        private System.Windows.Forms.DataGridViewTextBoxColumn gpo;
        private System.Windows.Forms.DataGridViewTextBoxColumn inv;
        private System.Windows.Forms.DataGridViewTextBoxColumn min;
        private System.Windows.Forms.DataGridViewTextBoxColumn reord;
        private System.Windows.Forms.DataGridViewTextBoxColumn max;
        private System.Windows.Forms.DataGridViewTextBoxColumn sugerencia;
        private System.Windows.Forms.DataGridViewTextBoxColumn reordauto;
        private System.Windows.Forms.DataGridViewTextBoxColumn rot;
        private System.Windows.Forms.Button btnShowPanelValues;
        private System.Windows.Forms.Timer timer1;
    }
}
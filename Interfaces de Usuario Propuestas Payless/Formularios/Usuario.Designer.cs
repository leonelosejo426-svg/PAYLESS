namespace Interfaces_de_Usuario_Propuestas_Payless
{
    partial class Usuario
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Usuario));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbBuscar = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvUsuarios = new System.Windows.Forms.DataGridView();
            this.btnBuscarUsuarios = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblMenu = new System.Windows.Forms.Label();
            this.lblCaja = new System.Windows.Forms.Label();
            this.lblUsuarios = new System.Windows.Forms.Label();
            this.lblCliente = new System.Windows.Forms.Label();
            this.lblProveedores = new System.Windows.Forms.Label();
            this.lblProductos = new System.Windows.Forms.Label();
            this.lblCompras = new System.Windows.Forms.Label();
            this.lblVenta = new System.Windows.Forms.Label();
            this.lblCredito = new System.Windows.Forms.Label();
            this.lblInventario = new System.Windows.Forms.Label();
            this.lblMantenimiento = new System.Windows.Forms.Label();
            this.lblReportes = new System.Windows.Forms.Label();
            this.lblAyuda = new System.Windows.Forms.Label();
            this.Nombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvUsuario = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Rol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Estado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label8.Location = new System.Drawing.Point(182, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 19);
            this.label8.TabIndex = 37;
            this.label8.Text = "Buscar";
            // 
            // cmbBuscar
            // 
            this.cmbBuscar.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbBuscar.FormattingEnabled = true;
            this.cmbBuscar.Items.AddRange(new object[] {
            "Activo",
            "Inactivo"});
            this.cmbBuscar.Location = new System.Drawing.Point(185, 115);
            this.cmbBuscar.Name = "cmbBuscar";
            this.cmbBuscar.Size = new System.Drawing.Size(202, 27);
            this.cmbBuscar.TabIndex = 28;
            this.cmbBuscar.SelectedIndexChanged += new System.EventHandler(this.cmbBuscar_SelectedIndexChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.pictureBox4);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Location = new System.Drawing.Point(164, -9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(809, 81);
            this.groupBox2.TabIndex = 26;
            this.groupBox2.TabStop = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // pictureBox4
            // 
            this.pictureBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(674, 20);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(72, 50);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 85;
            this.pictureBox4.TabStop = false;
            this.pictureBox4.Click += new System.EventHandler(this.pictureBox4_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.label2.Location = new System.Drawing.Point(553, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Acerca de...";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(18, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(135, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Usuarios";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // dgvUsuarios
            // 
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUsuarios.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvUsuarios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUsuarios.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Nombre,
            this.dgvUsuario,
            this.Rol,
            this.Estado});
            this.dgvUsuarios.Cursor = System.Windows.Forms.Cursors.No;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvUsuarios.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvUsuarios.EnableHeadersVisualStyles = false;
            this.dgvUsuarios.GridColor = System.Drawing.Color.Silver;
            this.dgvUsuarios.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.dgvUsuarios.Location = new System.Drawing.Point(201, 209);
            this.dgvUsuarios.Name = "dgvUsuarios";
            this.dgvUsuarios.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvUsuarios.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvUsuarios.RowHeadersWidth = 62;
            this.dgvUsuarios.ShowCellToolTips = false;
            this.dgvUsuarios.Size = new System.Drawing.Size(687, 348);
            this.dgvUsuarios.TabIndex = 50;
            this.dgvUsuarios.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvUsuarios_CellFormatting);
            // 
            // btnBuscarUsuarios
            // 
            this.btnBuscarUsuarios.BackColor = System.Drawing.Color.Black;
            this.btnBuscarUsuarios.FlatAppearance.BorderSize = 0;
            this.btnBuscarUsuarios.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnBuscarUsuarios.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarUsuarios.ForeColor = System.Drawing.SystemColors.Control;
            this.btnBuscarUsuarios.Location = new System.Drawing.Point(567, 107);
            this.btnBuscarUsuarios.Name = "btnBuscarUsuarios";
            this.btnBuscarUsuarios.Size = new System.Drawing.Size(82, 29);
            this.btnBuscarUsuarios.TabIndex = 51;
            this.btnBuscarUsuarios.Text = "Buscar";
            this.btnBuscarUsuarios.UseVisualStyleBackColor = false;
            this.btnBuscarUsuarios.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.Black;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAgregar.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.ForeColor = System.Drawing.SystemColors.Control;
            this.btnAgregar.Location = new System.Drawing.Point(709, 107);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(80, 32);
            this.btnAgregar.TabIndex = 52;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEditar.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.ForeColor = System.Drawing.SystemColors.Control;
            this.btnEditar.Location = new System.Drawing.Point(824, 107);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(75, 32);
            this.btnEditar.TabIndex = 55;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.Location = new System.Drawing.Point(404, 114);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(2);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(143, 26);
            this.txtBuscar.TabIndex = 56;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel2.Controls.Add(this.pictureBox1);
            this.flowLayoutPanel2.Controls.Add(this.lblMenu);
            this.flowLayoutPanel2.Controls.Add(this.lblCaja);
            this.flowLayoutPanel2.Controls.Add(this.lblUsuarios);
            this.flowLayoutPanel2.Controls.Add(this.lblCliente);
            this.flowLayoutPanel2.Controls.Add(this.lblProveedores);
            this.flowLayoutPanel2.Controls.Add(this.lblProductos);
            this.flowLayoutPanel2.Controls.Add(this.lblCompras);
            this.flowLayoutPanel2.Controls.Add(this.lblVenta);
            this.flowLayoutPanel2.Controls.Add(this.lblCredito);
            this.flowLayoutPanel2.Controls.Add(this.lblInventario);
            this.flowLayoutPanel2.Controls.Add(this.lblMantenimiento);
            this.flowLayoutPanel2.Controls.Add(this.lblReportes);
            this.flowLayoutPanel2.Controls.Add(this.lblAyuda);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(-12, -9);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(189, 703);
            this.flowLayoutPanel2.TabIndex = 57;
            this.flowLayoutPanel2.WrapContents = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(33, 26);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(33, 26, 3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(133, 88);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 132;
            this.pictureBox1.TabStop = false;
            // 
            // lblMenu
            // 
            this.lblMenu.AutoSize = true;
            this.lblMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenu.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenu.ForeColor = System.Drawing.Color.Black;
            this.lblMenu.Location = new System.Drawing.Point(23, 124);
            this.lblMenu.Margin = new System.Windows.Forms.Padding(23, 7, 4, 16);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(132, 19);
            this.lblMenu.TabIndex = 158;
            this.lblMenu.Text = "≡   Menú Principal";
            this.lblMenu.Click += new System.EventHandler(this.lblMenu_Click);
            // 
            // lblCaja
            // 
            this.lblCaja.AutoSize = true;
            this.lblCaja.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCaja.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaja.ForeColor = System.Drawing.Color.Black;
            this.lblCaja.Location = new System.Drawing.Point(23, 166);
            this.lblCaja.Margin = new System.Windows.Forms.Padding(23, 7, 4, 16);
            this.lblCaja.Name = "lblCaja";
            this.lblCaja.Size = new System.Drawing.Size(69, 19);
            this.lblCaja.TabIndex = 128;
            this.lblCaja.Text = "💰   Caja";
            this.lblCaja.Click += new System.EventHandler(this.lblCaja_Click);
            // 
            // lblUsuarios
            // 
            this.lblUsuarios.AutoSize = true;
            this.lblUsuarios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblUsuarios.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuarios.ForeColor = System.Drawing.Color.Black;
            this.lblUsuarios.Location = new System.Drawing.Point(23, 208);
            this.lblUsuarios.Margin = new System.Windows.Forms.Padding(23, 7, 4, 16);
            this.lblUsuarios.Name = "lblUsuarios";
            this.lblUsuarios.Size = new System.Drawing.Size(97, 19);
            this.lblUsuarios.TabIndex = 123;
            this.lblUsuarios.Text = "👥   Usuarios";
            this.lblUsuarios.Click += new System.EventHandler(this.lblUsuarios_Click);
            // 
            // lblCliente
            // 
            this.lblCliente.AutoSize = true;
            this.lblCliente.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCliente.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCliente.ForeColor = System.Drawing.Color.Black;
            this.lblCliente.Location = new System.Drawing.Point(23, 250);
            this.lblCliente.Margin = new System.Windows.Forms.Padding(23, 7, 4, 16);
            this.lblCliente.Name = "lblCliente";
            this.lblCliente.Size = new System.Drawing.Size(94, 19);
            this.lblCliente.TabIndex = 122;
            this.lblCliente.Text = "🧑‍🤝‍🧑   Clientes";
            this.lblCliente.Click += new System.EventHandler(this.lblCliente_Click);
            // 
            // lblProveedores
            // 
            this.lblProveedores.AutoSize = true;
            this.lblProveedores.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblProveedores.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProveedores.ForeColor = System.Drawing.Color.Black;
            this.lblProveedores.Location = new System.Drawing.Point(23, 292);
            this.lblProveedores.Margin = new System.Windows.Forms.Padding(23, 7, 4, 16);
            this.lblProveedores.Name = "lblProveedores";
            this.lblProveedores.Size = new System.Drawing.Size(122, 19);
            this.lblProveedores.TabIndex = 121;
            this.lblProveedores.Text = "🚚   Proveedores";
            this.lblProveedores.Click += new System.EventHandler(this.lblProveedores_Click);
            // 
            // lblProductos
            // 
            this.lblProductos.AutoSize = true;
            this.lblProductos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblProductos.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductos.ForeColor = System.Drawing.Color.Black;
            this.lblProductos.Location = new System.Drawing.Point(23, 334);
            this.lblProductos.Margin = new System.Windows.Forms.Padding(23, 7, 4, 16);
            this.lblProductos.Name = "lblProductos";
            this.lblProductos.Size = new System.Drawing.Size(104, 19);
            this.lblProductos.TabIndex = 120;
            this.lblProductos.Text = "🛍️   Productos";
            this.lblProductos.Click += new System.EventHandler(this.lblProductos_Click);
            // 
            // lblCompras
            // 
            this.lblCompras.AutoSize = true;
            this.lblCompras.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCompras.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompras.ForeColor = System.Drawing.Color.Black;
            this.lblCompras.Location = new System.Drawing.Point(23, 376);
            this.lblCompras.Margin = new System.Windows.Forms.Padding(23, 7, 4, 16);
            this.lblCompras.Name = "lblCompras";
            this.lblCompras.Size = new System.Drawing.Size(98, 19);
            this.lblCompras.TabIndex = 124;
            this.lblCompras.Text = "💳   Compras";
            this.lblCompras.Click += new System.EventHandler(this.lblCompras_Click);
            // 
            // lblVenta
            // 
            this.lblVenta.AutoSize = true;
            this.lblVenta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblVenta.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVenta.ForeColor = System.Drawing.Color.Black;
            this.lblVenta.Location = new System.Drawing.Point(23, 418);
            this.lblVenta.Margin = new System.Windows.Forms.Padding(23, 7, 4, 16);
            this.lblVenta.Name = "lblVenta";
            this.lblVenta.Size = new System.Drawing.Size(84, 19);
            this.lblVenta.TabIndex = 125;
            this.lblVenta.Text = "🛒   Ventas";
            this.lblVenta.Click += new System.EventHandler(this.lblVenta_Click);
            // 
            // lblCredito
            // 
            this.lblCredito.AutoSize = true;
            this.lblCredito.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCredito.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCredito.ForeColor = System.Drawing.Color.Black;
            this.lblCredito.Location = new System.Drawing.Point(23, 460);
            this.lblCredito.Margin = new System.Windows.Forms.Padding(23, 7, 4, 16);
            this.lblCredito.Name = "lblCredito";
            this.lblCredito.Size = new System.Drawing.Size(88, 19);
            this.lblCredito.TabIndex = 127;
            this.lblCredito.Text = "🧾   Credito";
            this.lblCredito.Click += new System.EventHandler(this.lblCredito_Click);
            // 
            // lblInventario
            // 
            this.lblInventario.AutoSize = true;
            this.lblInventario.BackColor = System.Drawing.Color.Transparent;
            this.lblInventario.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblInventario.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInventario.ForeColor = System.Drawing.Color.Black;
            this.lblInventario.Location = new System.Drawing.Point(23, 502);
            this.lblInventario.Margin = new System.Windows.Forms.Padding(23, 7, 4, 16);
            this.lblInventario.Name = "lblInventario";
            this.lblInventario.Size = new System.Drawing.Size(107, 19);
            this.lblInventario.TabIndex = 129;
            this.lblInventario.Text = "🏢   Inventario";
            this.lblInventario.Click += new System.EventHandler(this.lblInventario_Click);
            // 
            // lblMantenimiento
            // 
            this.lblMantenimiento.AutoSize = true;
            this.lblMantenimiento.BackColor = System.Drawing.Color.Transparent;
            this.lblMantenimiento.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMantenimiento.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMantenimiento.ForeColor = System.Drawing.Color.Black;
            this.lblMantenimiento.Location = new System.Drawing.Point(23, 544);
            this.lblMantenimiento.Margin = new System.Windows.Forms.Padding(23, 7, 4, 16);
            this.lblMantenimiento.Name = "lblMantenimiento";
            this.lblMantenimiento.Size = new System.Drawing.Size(140, 19);
            this.lblMantenimiento.TabIndex = 130;
            this.lblMantenimiento.Text = "🛠️   Mantenimiento";
            this.lblMantenimiento.Click += new System.EventHandler(this.lblMantenimiento_Click);
            // 
            // lblReportes
            // 
            this.lblReportes.AutoSize = true;
            this.lblReportes.BackColor = System.Drawing.Color.White;
            this.lblReportes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblReportes.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReportes.ForeColor = System.Drawing.Color.Black;
            this.lblReportes.Location = new System.Drawing.Point(23, 586);
            this.lblReportes.Margin = new System.Windows.Forms.Padding(23, 7, 4, 16);
            this.lblReportes.Name = "lblReportes";
            this.lblReportes.Size = new System.Drawing.Size(100, 19);
            this.lblReportes.TabIndex = 133;
            this.lblReportes.Text = "📋   Reportes";
            this.lblReportes.Click += new System.EventHandler(this.lblReportes_Click_1);
            // 
            // lblAyuda
            // 
            this.lblAyuda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblAyuda.AutoSize = true;
            this.lblAyuda.BackColor = System.Drawing.Color.Transparent;
            this.lblAyuda.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblAyuda.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAyuda.ForeColor = System.Drawing.Color.Black;
            this.lblAyuda.Location = new System.Drawing.Point(23, 628);
            this.lblAyuda.Margin = new System.Windows.Forms.Padding(23, 7, 4, 16);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(80, 19);
            this.lblAyuda.TabIndex = 131;
            this.lblAyuda.Text = "🆘   Ayuda";
            this.lblAyuda.Click += new System.EventHandler(this.lblAyuda_Click);
            // 
            // Nombre
            // 
            this.Nombre.DataPropertyName = "nombre_completo";
            this.Nombre.HeaderText = "Nombre";
            this.Nombre.MinimumWidth = 8;
            this.Nombre.Name = "Nombre";
            this.Nombre.ReadOnly = true;
            this.Nombre.Width = 200;
            // 
            // dgvUsuario
            // 
            this.dgvUsuario.DataPropertyName = "nombre_usuario";
            this.dgvUsuario.HeaderText = "Usuario";
            this.dgvUsuario.MinimumWidth = 8;
            this.dgvUsuario.Name = "dgvUsuario";
            this.dgvUsuario.ReadOnly = true;
            this.dgvUsuario.Width = 150;
            // 
            // Rol
            // 
            this.Rol.DataPropertyName = "nombre_rol";
            this.Rol.HeaderText = "Rol";
            this.Rol.MinimumWidth = 6;
            this.Rol.Name = "Rol";
            this.Rol.ReadOnly = true;
            this.Rol.Width = 125;
            // 
            // Estado
            // 
            this.Estado.DataPropertyName = "estado";
            this.Estado.HeaderText = "Estado";
            this.Estado.MinimumWidth = 8;
            this.Estado.Name = "Estado";
            this.Estado.ReadOnly = true;
            this.Estado.Width = 150;
            // 
            // Usuario
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(930, 654);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnBuscarUsuarios);
            this.Controls.Add(this.dgvUsuarios);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbBuscar);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Usuario";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Usuario";
            this.Load += new System.EventHandler(this.Usuario_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUsuarios)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion  
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbBuscar;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.DataGridView dgvUsuarios;
        private System.Windows.Forms.Button btnBuscarUsuarios;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblCaja;
        private System.Windows.Forms.Label lblUsuarios;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblProveedores;
        private System.Windows.Forms.Label lblProductos;
        private System.Windows.Forms.Label lblCompras;
        private System.Windows.Forms.Label lblVenta;
        private System.Windows.Forms.Label lblCredito;
        private System.Windows.Forms.Label lblInventario;
        private System.Windows.Forms.Label lblMantenimiento;
        private System.Windows.Forms.Label lblReportes;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.Label lblMenu;
        private System.Windows.Forms.DataGridViewTextBoxColumn Nombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn Rol;
        private System.Windows.Forms.DataGridViewTextBoxColumn Estado;
    }
}
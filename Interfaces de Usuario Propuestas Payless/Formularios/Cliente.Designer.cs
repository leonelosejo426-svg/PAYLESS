namespace Interfaces_de_Usuario_Propuestas_Payless
{
    partial class Cliente
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Cliente));
            this.CmbBusqueda = new System.Windows.Forms.ComboBox();
            this.DGVtabla1 = new System.Windows.Forms.DataGridView();
            this.LblBusqueda = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label18 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnbuscar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
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
            this.lblMenu = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DGVtabla1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // CmbBusqueda
            // 
            this.CmbBusqueda.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbBusqueda.FormattingEnabled = true;
            this.CmbBusqueda.Items.AddRange(new object[] {
            "Nombre",
            "Numero de cédula",
            "Código"});
            this.CmbBusqueda.Location = new System.Drawing.Point(196, 109);
            this.CmbBusqueda.Name = "CmbBusqueda";
            this.CmbBusqueda.Size = new System.Drawing.Size(174, 27);
            this.CmbBusqueda.TabIndex = 27;
            this.CmbBusqueda.SelectedIndexChanged += new System.EventHandler(this.CBbusqueda_SelectedIndexChanged);
            // 
            // DGVtabla1
            // 
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(106)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle27.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle27.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle27.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle27.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DGVtabla1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle27;
            this.DGVtabla1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle28.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle28.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle28.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle28.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle28.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle28.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DGVtabla1.DefaultCellStyle = dataGridViewCellStyle28;
            this.DGVtabla1.EnableHeadersVisualStyles = false;
            this.DGVtabla1.Location = new System.Drawing.Point(207, 201);
            this.DGVtabla1.Name = "DGVtabla1";
            this.DGVtabla1.RowHeadersWidth = 62;
            this.DGVtabla1.Size = new System.Drawing.Size(803, 403);
            this.DGVtabla1.TabIndex = 26;
            this.DGVtabla1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVtabla1_CellContentClick);
            this.DGVtabla1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DGVtabla1_CellFormatting);
            // 
            // LblBusqueda
            // 
            this.LblBusqueda.AutoSize = true;
            this.LblBusqueda.BackColor = System.Drawing.Color.White;
            this.LblBusqueda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LblBusqueda.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblBusqueda.ForeColor = System.Drawing.Color.Black;
            this.LblBusqueda.Location = new System.Drawing.Point(203, 81);
            this.LblBusqueda.Name = "LblBusqueda";
            this.LblBusqueda.Size = new System.Drawing.Size(76, 19);
            this.LblBusqueda.TabIndex = 15;
            this.LblBusqueda.Text = "Buscar por";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label18);
            this.panel2.Controls.Add(this.pictureBox4);
            this.panel2.Controls.Add(this.label1);
            this.panel2.ForeColor = System.Drawing.Color.White;
            this.panel2.Location = new System.Drawing.Point(152, -10);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(930, 70);
            this.panel2.TabIndex = 60;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(106)))), ((int)(((byte)(0)))));
            this.label18.Location = new System.Drawing.Point(678, 42);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(92, 19);
            this.label18.TabIndex = 11;
            this.label18.Text = "Acerca De...";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(801, 18);
            this.pictureBox4.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(59, 46);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 85;
            this.pictureBox4.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(106)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(48, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Clientes";
            // 
            // btnbuscar
            // 
            this.btnbuscar.BackColor = System.Drawing.Color.Black;
            this.btnbuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnbuscar.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnbuscar.ForeColor = System.Drawing.Color.White;
            this.btnbuscar.Location = new System.Drawing.Point(574, 101);
            this.btnbuscar.Name = "btnbuscar";
            this.btnbuscar.Size = new System.Drawing.Size(90, 40);
            this.btnbuscar.TabIndex = 89;
            this.btnbuscar.Text = "Buscar";
            this.btnbuscar.UseVisualStyleBackColor = false;
            this.btnbuscar.Click += new System.EventHandler(this.btnbuscar_Click);
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.Black;
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.ForeColor = System.Drawing.Color.White;
            this.btnAgregar.Location = new System.Drawing.Point(707, 101);
            this.btnAgregar.Margin = new System.Windows.Forms.Padding(2);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(93, 40);
            this.btnAgregar.TabIndex = 90;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click_3);
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.Black;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEditar.ForeColor = System.Drawing.Color.White;
            this.btnEditar.Location = new System.Drawing.Point(835, 100);
            this.btnEditar.Margin = new System.Windows.Forms.Padding(2);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(89, 40);
            this.btnEditar.TabIndex = 91;
            this.btnEditar.Text = " Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.Location = new System.Drawing.Point(386, 109);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(2);
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(153, 26);
            this.txtBuscar.TabIndex = 93;
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
            this.flowLayoutPanel2.Location = new System.Drawing.Point(-15, -7);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(194, 699);
            this.flowLayoutPanel2.TabIndex = 94;
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
            // lblCaja
            // 
            this.lblCaja.AutoSize = true;
            this.lblCaja.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCaja.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaja.ForeColor = System.Drawing.Color.Black;
            this.lblCaja.Location = new System.Drawing.Point(23, 164);
            this.lblCaja.Margin = new System.Windows.Forms.Padding(23, 6, 4, 16);
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
            this.lblUsuarios.Location = new System.Drawing.Point(23, 205);
            this.lblUsuarios.Margin = new System.Windows.Forms.Padding(23, 6, 4, 16);
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
            this.lblCliente.Location = new System.Drawing.Point(23, 246);
            this.lblCliente.Margin = new System.Windows.Forms.Padding(23, 6, 4, 16);
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
            this.lblProveedores.Location = new System.Drawing.Point(23, 287);
            this.lblProveedores.Margin = new System.Windows.Forms.Padding(23, 6, 4, 16);
            this.lblProveedores.Name = "lblProveedores";
            this.lblProveedores.Size = new System.Drawing.Size(122, 19);
            this.lblProveedores.TabIndex = 121;
            this.lblProveedores.Text = "🚚   Proveedores";
            this.lblProveedores.Click += new System.EventHandler(this.lblProveedores_Click_1);
            // 
            // lblProductos
            // 
            this.lblProductos.AutoSize = true;
            this.lblProductos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblProductos.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductos.ForeColor = System.Drawing.Color.Black;
            this.lblProductos.Location = new System.Drawing.Point(23, 328);
            this.lblProductos.Margin = new System.Windows.Forms.Padding(23, 6, 4, 16);
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
            this.lblCompras.Location = new System.Drawing.Point(23, 369);
            this.lblCompras.Margin = new System.Windows.Forms.Padding(23, 6, 4, 16);
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
            this.lblVenta.Location = new System.Drawing.Point(23, 410);
            this.lblVenta.Margin = new System.Windows.Forms.Padding(23, 6, 4, 16);
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
            this.lblCredito.Location = new System.Drawing.Point(23, 451);
            this.lblCredito.Margin = new System.Windows.Forms.Padding(23, 6, 4, 16);
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
            this.lblInventario.Location = new System.Drawing.Point(23, 492);
            this.lblInventario.Margin = new System.Windows.Forms.Padding(23, 6, 4, 16);
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
            this.lblMantenimiento.Location = new System.Drawing.Point(23, 533);
            this.lblMantenimiento.Margin = new System.Windows.Forms.Padding(23, 6, 4, 16);
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
            this.lblReportes.Location = new System.Drawing.Point(23, 574);
            this.lblReportes.Margin = new System.Windows.Forms.Padding(23, 6, 4, 16);
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
            this.lblAyuda.Location = new System.Drawing.Point(23, 615);
            this.lblAyuda.Margin = new System.Windows.Forms.Padding(23, 6, 4, 16);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(80, 19);
            this.lblAyuda.TabIndex = 131;
            this.lblAyuda.Text = "🆘   Ayuda";
            this.lblAyuda.Click += new System.EventHandler(this.lblAyuda_Click);
            // 
            // lblMenu
            // 
            this.lblMenu.AutoSize = true;
            this.lblMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblMenu.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMenu.ForeColor = System.Drawing.Color.Black;
            this.lblMenu.Location = new System.Drawing.Point(23, 123);
            this.lblMenu.Margin = new System.Windows.Forms.Padding(23, 6, 4, 16);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(132, 19);
            this.lblMenu.TabIndex = 150;
            this.lblMenu.Text = "≡   Menú Principal";
            this.lblMenu.Click += new System.EventHandler(this.lblMenu_Click);
            // 
            // Cliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1030, 649);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.btnbuscar);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.CmbBusqueda);
            this.Controls.Add(this.DGVtabla1);
            this.Controls.Add(this.LblBusqueda);
            this.Name = "Cliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cliente";
            this.Load += new System.EventHandler(this.Cliente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGVtabla1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox CmbBusqueda;
        private System.Windows.Forms.DataGridView DGVtabla1;
        private System.Windows.Forms.Label LblBusqueda;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Button btnbuscar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblCaja;
        private System.Windows.Forms.Label lblProveedores;
        private System.Windows.Forms.Label lblCliente;
        private System.Windows.Forms.Label lblUsuarios;
        private System.Windows.Forms.Label lblProductos;
        private System.Windows.Forms.Label lblCompras;
        private System.Windows.Forms.Label lblVenta;
        private System.Windows.Forms.Label lblCredito;
        private System.Windows.Forms.Label lblInventario;
        private System.Windows.Forms.Label lblMantenimiento;
        private System.Windows.Forms.Label lblReportes;
        private System.Windows.Forms.Label lblAyuda;
        private System.Windows.Forms.Label lblMenu;
    }
}
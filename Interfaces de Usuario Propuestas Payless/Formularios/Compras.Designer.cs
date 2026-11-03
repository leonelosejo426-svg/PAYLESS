namespace Interfaces_de_Usuario_Propuestas_Payless
{
    partial class Compras_nuevo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Compras_nuevo));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label31 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label12 = new System.Windows.Forms.Label();
            this.dgvCompras = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnBuscar = new System.Windows.Forms.Button();
            this.cbBuscarPor = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.btnImprimir = new System.Windows.Forms.Button();
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
            this.colFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProveedor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colEstado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompras)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.label31);
            this.groupBox1.Controls.Add(this.pictureBox4);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Location = new System.Drawing.Point(-2, -10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1989, 89);
            this.groupBox1.TabIndex = 25;
            this.groupBox1.TabStop = false;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.BackColor = System.Drawing.Color.Transparent;
            this.label31.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label31.Location = new System.Drawing.Point(773, 51);
            this.label31.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(92, 19);
            this.label31.TabIndex = 73;
            this.label31.Text = "Acerca De...";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(902, 22);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(69, 52);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 72;
            this.pictureBox4.TabStop = false;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label12.Location = new System.Drawing.Point(199, 37);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(141, 36);
            this.label12.TabIndex = 5;
            this.label12.Text = "Compras";
            // 
            // dgvCompras
            // 
            this.dgvCompras.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCompras.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCompras.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCompras.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFecha,
            this.colProveedor,
            this.colTotal,
            this.colEstado});
            this.dgvCompras.EnableHeadersVisualStyles = false;
            this.dgvCompras.Location = new System.Drawing.Point(221, 207);
            this.dgvCompras.Name = "dgvCompras";
            this.dgvCompras.RowHeadersWidth = 62;
            this.dgvCompras.Size = new System.Drawing.Size(713, 370);
            this.dgvCompras.TabIndex = 26;
            this.dgvCompras.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.Transparent;
            this.groupBox3.Controls.Add(this.btnBuscar);
            this.groupBox3.Controls.Add(this.cbBuscarPor);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.txtBuscar);
            this.groupBox3.Controls.Add(this.btnAgregar);
            this.groupBox3.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(203, 100);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(742, 92);
            this.groupBox3.TabIndex = 112;
            this.groupBox3.TabStop = false;
            // 
            // btnBuscar
            // 
            this.btnBuscar.BackColor = System.Drawing.Color.Black;
            this.btnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBuscar.FlatAppearance.BorderSize = 0;
            this.btnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuscar.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscar.ForeColor = System.Drawing.Color.White;
            this.btnBuscar.Location = new System.Drawing.Point(363, 26);
            this.btnBuscar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnBuscar.Name = "btnBuscar";
            this.btnBuscar.Size = new System.Drawing.Size(112, 32);
            this.btnBuscar.TabIndex = 122;
            this.btnBuscar.Text = "Buscar";
            this.btnBuscar.UseVisualStyleBackColor = false;
            this.btnBuscar.Click += new System.EventHandler(this.btnBuscar_Click);
            // 
            // cbBuscarPor
            // 
            this.cbBuscarPor.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbBuscarPor.FormattingEnabled = true;
            this.cbBuscarPor.Location = new System.Drawing.Point(93, 30);
            this.cbBuscarPor.Name = "cbBuscarPor";
            this.cbBuscarPor.Size = new System.Drawing.Size(121, 27);
            this.cbBuscarPor.TabIndex = 121;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(24, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 19);
            this.label3.TabIndex = 120;
            this.label3.Text = "Buscar";
            // 
            // txtBuscar
            // 
            this.txtBuscar.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.Location = new System.Drawing.Point(231, 30);
            this.txtBuscar.Multiline = true;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(106, 22);
            this.txtBuscar.TabIndex = 119;
            // 
            // btnAgregar
            // 
            this.btnAgregar.BackColor = System.Drawing.Color.Black;
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.FlatAppearance.BorderSize = 0;
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAgregar.ForeColor = System.Drawing.Color.White;
            this.btnAgregar.Location = new System.Drawing.Point(556, 26);
            this.btnAgregar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(112, 32);
            this.btnAgregar.TabIndex = 104;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // btnImprimir
            // 
            this.btnImprimir.BackColor = System.Drawing.Color.Black;
            this.btnImprimir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnImprimir.FlatAppearance.BorderSize = 0;
            this.btnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnImprimir.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImprimir.ForeColor = System.Drawing.Color.White;
            this.btnImprimir.Location = new System.Drawing.Point(204, 618);
            this.btnImprimir.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnImprimir.Name = "btnImprimir";
            this.btnImprimir.Size = new System.Drawing.Size(177, 38);
            this.btnImprimir.TabIndex = 115;
            this.btnImprimir.Text = "🖨️Imprrimir Factura";
            this.btnImprimir.UseVisualStyleBackColor = false;
            this.btnImprimir.Click += new System.EventHandler(this.btnImprimir_Click);
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
            this.flowLayoutPanel2.Location = new System.Drawing.Point(-11, -10);
            this.flowLayoutPanel2.Margin = new System.Windows.Forms.Padding(2);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(185, 700);
            this.flowLayoutPanel2.TabIndex = 116;
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
            this.lblMenu.Location = new System.Drawing.Point(23, 123);
            this.lblMenu.Margin = new System.Windows.Forms.Padding(23, 6, 4, 16);
            this.lblMenu.Name = "lblMenu";
            this.lblMenu.Size = new System.Drawing.Size(132, 19);
            this.lblMenu.TabIndex = 151;
            this.lblMenu.Text = "≡   Menú Principal";
            this.lblMenu.Click += new System.EventHandler(this.lblMenu_Click);
            // 
            // lblCaja
            // 
            this.lblCaja.AutoSize = true;
            this.lblCaja.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCaja.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaja.ForeColor = System.Drawing.Color.Black;
            this.lblCaja.Location = new System.Drawing.Point(23, 168);
            this.lblCaja.Margin = new System.Windows.Forms.Padding(23, 10, 4, 16);
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
            this.lblUsuarios.Location = new System.Drawing.Point(23, 213);
            this.lblUsuarios.Margin = new System.Windows.Forms.Padding(23, 10, 4, 16);
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
            this.lblCliente.Location = new System.Drawing.Point(23, 258);
            this.lblCliente.Margin = new System.Windows.Forms.Padding(23, 10, 4, 16);
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
            this.lblProveedores.Location = new System.Drawing.Point(23, 303);
            this.lblProveedores.Margin = new System.Windows.Forms.Padding(23, 10, 4, 16);
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
            this.lblProductos.Location = new System.Drawing.Point(23, 348);
            this.lblProductos.Margin = new System.Windows.Forms.Padding(23, 10, 4, 16);
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
            this.lblCompras.Location = new System.Drawing.Point(23, 393);
            this.lblCompras.Margin = new System.Windows.Forms.Padding(23, 10, 4, 16);
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
            this.lblVenta.Location = new System.Drawing.Point(23, 438);
            this.lblVenta.Margin = new System.Windows.Forms.Padding(23, 10, 4, 16);
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
            this.lblCredito.Location = new System.Drawing.Point(23, 483);
            this.lblCredito.Margin = new System.Windows.Forms.Padding(23, 10, 4, 16);
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
            this.lblInventario.Location = new System.Drawing.Point(23, 528);
            this.lblInventario.Margin = new System.Windows.Forms.Padding(23, 10, 4, 16);
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
            this.lblMantenimiento.Location = new System.Drawing.Point(23, 573);
            this.lblMantenimiento.Margin = new System.Windows.Forms.Padding(23, 10, 4, 16);
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
            this.lblReportes.Location = new System.Drawing.Point(23, 618);
            this.lblReportes.Margin = new System.Windows.Forms.Padding(23, 10, 4, 16);
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
            this.lblAyuda.Location = new System.Drawing.Point(23, 663);
            this.lblAyuda.Margin = new System.Windows.Forms.Padding(23, 10, 4, 16);
            this.lblAyuda.Name = "lblAyuda";
            this.lblAyuda.Size = new System.Drawing.Size(80, 19);
            this.lblAyuda.TabIndex = 131;
            this.lblAyuda.Text = "🆘   Ayuda";
            this.lblAyuda.Click += new System.EventHandler(this.lblAyuda_Click);
            // 
            // colFecha
            // 
            this.colFecha.HeaderText = "Fecha";
            this.colFecha.Name = "colFecha";
            // 
            // colProveedor
            // 
            this.colProveedor.HeaderText = "Proveedor";
            this.colProveedor.Name = "colProveedor";
            this.colProveedor.Width = 300;
            // 
            // colTotal
            // 
            this.colTotal.HeaderText = "Total";
            this.colTotal.Name = "colTotal";
            this.colTotal.Width = 150;
            // 
            // colEstado
            // 
            this.colEstado.HeaderText = "Estado";
            this.colEstado.Name = "colEstado";
            // 
            // Compras_nuevo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(988, 682);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.btnImprimir);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.dgvCompras);
            this.Controls.Add(this.groupBox1);
            this.Name = "Compras_nuevo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Compras_nuevo";
            this.Load += new System.EventHandler(this.Compras_nuevo_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompras)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DataGridView dgvCompras;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnBuscar;
        private System.Windows.Forms.ComboBox cbBuscarPor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnImprimir;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn colFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProveedor;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colEstado;
    }
}
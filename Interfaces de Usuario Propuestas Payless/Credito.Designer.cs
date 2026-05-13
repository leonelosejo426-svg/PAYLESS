namespace Interfaces_de_Usuario_Propuestas_Payless
{
    partial class Credito
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Credito));
            this.label1 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.codigo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.producto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cantidad = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.precio = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.iva = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subtotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.lblProductos = new System.Windows.Forms.Label();
            this.lblProveedores = new System.Windows.Forms.Label();
            this.lblClientes = new System.Windows.Forms.Label();
            this.lblUsuarios = new System.Windows.Forms.Label();
            this.lblCompras = new System.Windows.Forms.Label();
            this.lblVentas = new System.Windows.Forms.Label();
            this.lblDevoluciones = new System.Windows.Forms.Label();
            this.lblCredito = new System.Windows.Forms.Label();
            this.lblCaja = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12.25F);
            this.label1.Location = new System.Drawing.Point(209, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "No.Factura";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.dataGridView1);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(201, 46);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(671, 227);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codigo,
            this.producto,
            this.cantidad,
            this.precio,
            this.iva,
            this.subtotal});
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(661, 198);
            this.dataGridView1.TabIndex = 0;
            // 
            // codigo
            // 
            this.codigo.HeaderText = "Código";
            this.codigo.Name = "codigo";
            // 
            // producto
            // 
            this.producto.HeaderText = "Producto";
            this.producto.Name = "producto";
            this.producto.Width = 120;
            // 
            // cantidad
            // 
            this.cantidad.HeaderText = "Cantidad";
            this.cantidad.Name = "cantidad";
            // 
            // precio
            // 
            this.precio.HeaderText = "Precio";
            this.precio.Name = "precio";
            // 
            // iva
            // 
            this.iva.HeaderText = "IVA";
            this.iva.Name = "iva";
            // 
            // subtotal
            // 
            this.subtotal.HeaderText = "Subtotal";
            this.subtotal.Name = "subtotal";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(656, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "Cliente";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12.25F);
            this.label3.Location = new System.Drawing.Point(460, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 19);
            this.label3.TabIndex = 5;
            this.label3.Text = "Fecha";
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(534, 9);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 29);
            this.textBox3.TabIndex = 7;
            // 
            // textBox4
            // 
            this.textBox4.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.Location = new System.Drawing.Point(319, 9);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(117, 29);
            this.textBox4.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox5);
            this.panel1.Controls.Add(this.textBox6);
            this.panel1.Controls.Add(this.textBox7);
            this.panel1.Controls.Add(this.textBox8);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Location = new System.Drawing.Point(884, 42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(278, 183);
            this.panel1.TabIndex = 9;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(72, 49);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(136, 20);
            this.textBox5.TabIndex = 10;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(96, 84);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(121, 20);
            this.textBox6.TabIndex = 11;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(104, 118);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(113, 20);
            this.textBox7.TabIndex = 12;
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(76, 150);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(132, 20);
            this.textBox8.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 12.25F);
            this.label8.Location = new System.Drawing.Point(8, 150);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 19);
            this.label8.TabIndex = 10;
            this.label8.Text = "Estado";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12.25F);
            this.label4.Location = new System.Drawing.Point(8, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 19);
            this.label4.TabIndex = 10;
            this.label4.Text = "Disponible";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12.25F);
            this.label5.Location = new System.Drawing.Point(8, 83);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 19);
            this.label5.TabIndex = 11;
            this.label5.Text = "Pendiente";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 12.25F);
            this.label6.Location = new System.Drawing.Point(8, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(55, 19);
            this.label6.TabIndex = 12;
            this.label6.Text = "Límite";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(8, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(203, 19);
            this.label7.TabIndex = 13;
            this.label7.Text = "Información crediticia del cliente";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.textBox9);
            this.panel2.Controls.Add(this.textBox10);
            this.panel2.Controls.Add(this.textBox11);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Location = new System.Drawing.Point(884, 244);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(332, 169);
            this.panel2.TabIndex = 14;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(72, 49);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(136, 20);
            this.textBox9.TabIndex = 10;
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(96, 84);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(121, 20);
            this.textBox10.TabIndex = 11;
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(110, 118);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(113, 20);
            this.textBox11.TabIndex = 12;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 12.25F);
            this.label10.Location = new System.Drawing.Point(8, 117);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 19);
            this.label10.TabIndex = 10;
            this.label10.Text = "Vencimiento";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Times New Roman", 12.25F);
            this.label11.Location = new System.Drawing.Point(8, 83);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(56, 19);
            this.label11.TabIndex = 11;
            this.label11.Text = "Cuotas";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 12.25F);
            this.label12.Location = new System.Drawing.Point(8, 49);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(47, 19);
            this.label12.TabIndex = 12;
            this.label12.Text = "Plazo";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Times New Roman", 12.25F);
            this.label13.Location = new System.Drawing.Point(8, 9);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(173, 19);
            this.label13.TabIndex = 13;
            this.label13.Text = "Condiciones del crédito";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Times New Roman", 12.25F);
            this.label14.Location = new System.Drawing.Point(207, 379);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 19);
            this.label14.TabIndex = 14;
            this.label14.Text = "Total";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Times New Roman", 12.25F);
            this.label15.Location = new System.Drawing.Point(207, 337);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(34, 19);
            this.label15.TabIndex = 15;
            this.label15.Text = "IVA";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Times New Roman", 12.25F);
            this.label16.Location = new System.Drawing.Point(207, 298);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 19);
            this.label16.TabIndex = 16;
            this.label16.Text = "Subtotal";
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(263, 381);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(121, 20);
            this.textBox13.TabIndex = 14;
            // 
            // textBox14
            // 
            this.textBox14.Location = new System.Drawing.Point(263, 337);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(121, 20);
            this.textBox14.TabIndex = 15;
            // 
            // textBox15
            // 
            this.textBox15.Location = new System.Drawing.Point(295, 295);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(121, 20);
            this.textBox15.TabIndex = 16;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Red;
            this.button2.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Location = new System.Drawing.Point(709, 393);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(143, 23);
            this.button2.TabIndex = 17;
            this.button2.Text = "Registar Monto";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Red;
            this.button4.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Location = new System.Drawing.Point(899, 10);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(143, 23);
            this.button4.TabIndex = 19;
            this.button4.Text = "Buscar cliente";
            this.button4.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.Black;
            this.button5.ForeColor = System.Drawing.Color.Transparent;
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button5.Location = new System.Drawing.Point(168, -2);
            this.button5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(10, 466);
            this.button5.TabIndex = 110;
            this.button5.UseVisualStyleBackColor = false;
            // 
            // lblProductos
            // 
            this.lblProductos.AutoSize = true;
            this.lblProductos.BackColor = System.Drawing.Color.Black;
            this.lblProductos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblProductos.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblProductos.ForeColor = System.Drawing.Color.White;
            this.lblProductos.Location = new System.Drawing.Point(12, 140);
            this.lblProductos.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProductos.Name = "lblProductos";
            this.lblProductos.Size = new System.Drawing.Size(104, 19);
            this.lblProductos.TabIndex = 111;
            this.lblProductos.Text = "🛍️   Productos";
            this.lblProductos.Click += new System.EventHandler(this.label17_Click);
            // 
            // lblProveedores
            // 
            this.lblProveedores.AutoSize = true;
            this.lblProveedores.BackColor = System.Drawing.Color.Black;
            this.lblProveedores.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblProveedores.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblProveedores.ForeColor = System.Drawing.Color.White;
            this.lblProveedores.Location = new System.Drawing.Point(12, 186);
            this.lblProveedores.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblProveedores.Name = "lblProveedores";
            this.lblProveedores.Size = new System.Drawing.Size(122, 19);
            this.lblProveedores.TabIndex = 112;
            this.lblProveedores.Text = "🚚   Proveedores";
            this.lblProveedores.Click += new System.EventHandler(this.label18_Click);
            // 
            // lblClientes
            // 
            this.lblClientes.AutoSize = true;
            this.lblClientes.BackColor = System.Drawing.Color.Black;
            this.lblClientes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblClientes.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblClientes.ForeColor = System.Drawing.Color.White;
            this.lblClientes.Location = new System.Drawing.Point(14, 232);
            this.lblClientes.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClientes.Name = "lblClientes";
            this.lblClientes.Size = new System.Drawing.Size(94, 19);
            this.lblClientes.TabIndex = 113;
            this.lblClientes.Text = "🧑‍🤝‍🧑   Clientes";
            this.lblClientes.Click += new System.EventHandler(this.label19_Click);
            // 
            // lblUsuarios
            // 
            this.lblUsuarios.AutoSize = true;
            this.lblUsuarios.BackColor = System.Drawing.Color.Black;
            this.lblUsuarios.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblUsuarios.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblUsuarios.ForeColor = System.Drawing.Color.White;
            this.lblUsuarios.Location = new System.Drawing.Point(14, 274);
            this.lblUsuarios.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblUsuarios.Name = "lblUsuarios";
            this.lblUsuarios.Size = new System.Drawing.Size(97, 19);
            this.lblUsuarios.TabIndex = 114;
            this.lblUsuarios.Text = "👥   Usuarios";
            this.lblUsuarios.Click += new System.EventHandler(this.label20_Click);
            // 
            // lblCompras
            // 
            this.lblCompras.AutoSize = true;
            this.lblCompras.BackColor = System.Drawing.Color.Black;
            this.lblCompras.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCompras.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblCompras.ForeColor = System.Drawing.Color.White;
            this.lblCompras.Location = new System.Drawing.Point(14, 316);
            this.lblCompras.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCompras.Name = "lblCompras";
            this.lblCompras.Size = new System.Drawing.Size(98, 19);
            this.lblCompras.TabIndex = 115;
            this.lblCompras.Text = "💳   Compras";
            this.lblCompras.Click += new System.EventHandler(this.label21_Click);
            // 
            // lblVentas
            // 
            this.lblVentas.AutoSize = true;
            this.lblVentas.BackColor = System.Drawing.Color.Black;
            this.lblVentas.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblVentas.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblVentas.ForeColor = System.Drawing.Color.White;
            this.lblVentas.Location = new System.Drawing.Point(14, 356);
            this.lblVentas.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblVentas.Name = "lblVentas";
            this.lblVentas.Size = new System.Drawing.Size(84, 19);
            this.lblVentas.TabIndex = 116;
            this.lblVentas.Text = "🛒   Ventas";
            this.lblVentas.Click += new System.EventHandler(this.label22_Click);
            // 
            // lblDevoluciones
            // 
            this.lblDevoluciones.AutoSize = true;
            this.lblDevoluciones.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblDevoluciones.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblDevoluciones.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblDevoluciones.ForeColor = System.Drawing.Color.White;
            this.lblDevoluciones.Location = new System.Drawing.Point(13, 397);
            this.lblDevoluciones.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDevoluciones.Name = "lblDevoluciones";
            this.lblDevoluciones.Size = new System.Drawing.Size(128, 19);
            this.lblDevoluciones.TabIndex = 117;
            this.lblDevoluciones.Text = "🔄   Devoluciones";
            this.lblDevoluciones.Click += new System.EventHandler(this.label23_Click);
            // 
            // lblCredito
            // 
            this.lblCredito.AutoSize = true;
            this.lblCredito.BackColor = System.Drawing.Color.Black;
            this.lblCredito.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCredito.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.lblCredito.ForeColor = System.Drawing.Color.White;
            this.lblCredito.Location = new System.Drawing.Point(14, 439);
            this.lblCredito.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCredito.Name = "lblCredito";
            this.lblCredito.Size = new System.Drawing.Size(88, 19);
            this.lblCredito.TabIndex = 118;
            this.lblCredito.Text = "🧾   Credito";
            this.lblCredito.Click += new System.EventHandler(this.label24_Click);
            // 
            // lblCaja
            // 
            this.lblCaja.AutoSize = true;
            this.lblCaja.BackColor = System.Drawing.Color.Black;
            this.lblCaja.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblCaja.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaja.ForeColor = System.Drawing.Color.White;
            this.lblCaja.Location = new System.Drawing.Point(12, 474);
            this.lblCaja.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCaja.Name = "lblCaja";
            this.lblCaja.Size = new System.Drawing.Size(87, 22);
            this.lblCaja.TabIndex = 119;
            this.lblCaja.Text = "💰   Caja";
            this.lblCaja.Click += new System.EventHandler(this.lblCaja_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(26, 22);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 89);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(725, 12);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(160, 21);
            this.comboBox1.TabIndex = 120;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(0, -2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(169, 512);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 23.25F);
            this.label9.Location = new System.Drawing.Point(1050, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 35);
            this.label9.TabIndex = 121;
            this.label9.Text = "☰";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.Location = new System.Drawing.Point(1079, 9);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(44, 19);
            this.label26.TabIndex = 122;
            this.label26.Text = "Menú";
            // 
            // Credito
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1227, 505);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.lblCaja);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblCredito);
            this.Controls.Add(this.lblDevoluciones);
            this.Controls.Add(this.lblVentas);
            this.Controls.Add(this.lblCompras);
            this.Controls.Add(this.lblUsuarios);
            this.Controls.Add(this.lblClientes);
            this.Controls.Add(this.lblProveedores);
            this.Controls.Add(this.lblProductos);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox13);
            this.Controls.Add(this.textBox14);
            this.Controls.Add(this.textBox15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Credito";
            this.Text = "Crédito";
            this.Load += new System.EventHandler(this.Credito_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn codigo;
        private System.Windows.Forms.DataGridViewTextBoxColumn producto;
        private System.Windows.Forms.DataGridViewTextBoxColumn cantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn precio;
        private System.Windows.Forms.DataGridViewTextBoxColumn iva;
        private System.Windows.Forms.DataGridViewTextBoxColumn subtotal;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label lblProductos;
        private System.Windows.Forms.Label lblProveedores;
        private System.Windows.Forms.Label lblClientes;
        private System.Windows.Forms.Label lblUsuarios;
        private System.Windows.Forms.Label lblCompras;
        private System.Windows.Forms.Label lblVentas;
        private System.Windows.Forms.Label lblDevoluciones;
        private System.Windows.Forms.Label lblCredito;
        private System.Windows.Forms.Label lblCaja;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label26;
    }
}
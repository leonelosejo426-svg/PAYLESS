namespace Interfaces_de_Usuario_Propuestas_Payless
{
    partial class Caja
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnCierredecaja = new System.Windows.Forms.Button();
            this.txtAperturadecaja = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Descripción = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Monto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblAperturadecaja = new System.Windows.Forms.Label();
            this.txtIngresos = new System.Windows.Forms.TextBox();
            this.lblIngresos = new System.Windows.Forms.Label();
            this.txtSaldoactual = new System.Windows.Forms.TextBox();
            this.lblSaldoactual = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnMenu = new System.Windows.Forms.Button();
            this.btnRegistrodeegresos = new System.Windows.Forms.Button();
            this.btnAperturadecaja = new System.Windows.Forms.Button();
            this.txtegresos = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            // this.pictureBox1.Image = global::Interfaces_de_Usuario_Propuestas_Payless.Properties.Resources.Logo_Payless;
            this.pictureBox1.Location = new System.Drawing.Point(62, 43);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(228, 158);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 59;
            this.pictureBox1.TabStop = false;
            // 
            // btnCierredecaja
            // 
            this.btnCierredecaja.BackColor = System.Drawing.Color.Transparent;
            this.btnCierredecaja.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCierredecaja.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCierredecaja.ForeColor = System.Drawing.Color.Black;
            this.btnCierredecaja.Location = new System.Drawing.Point(601, 655);
            this.btnCierredecaja.Name = "btnCierredecaja";
            this.btnCierredecaja.Size = new System.Drawing.Size(204, 34);
            this.btnCierredecaja.TabIndex = 72;
            this.btnCierredecaja.Text = "Cierre de Caja";
            this.btnCierredecaja.UseVisualStyleBackColor = false;
            // 
            // txtAperturadecaja
            // 
            this.txtAperturadecaja.BackColor = System.Drawing.Color.White;
            this.txtAperturadecaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAperturadecaja.ForeColor = System.Drawing.Color.Black;
            this.txtAperturadecaja.Location = new System.Drawing.Point(268, 279);
            this.txtAperturadecaja.Multiline = true;
            this.txtAperturadecaja.Name = "txtAperturadecaja";
            this.txtAperturadecaja.Size = new System.Drawing.Size(170, 40);
            this.txtAperturadecaja.TabIndex = 71;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(378, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 55);
            this.label1.TabIndex = 70;
            this.label1.Text = "Caja";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(57, 246);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 26);
            this.label2.TabIndex = 73;
            this.label2.Text = "Usuario:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Tipo,
            this.Descripción,
            this.Monto});
            this.dataGridView1.Location = new System.Drawing.Point(42, 13);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(645, 172);
            this.dataGridView1.TabIndex = 74;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Fecha";
            this.Column1.MinimumWidth = 8;
            this.Column1.Name = "Column1";
            this.Column1.Width = 150;
            // 
            // Tipo
            // 
            this.Tipo.HeaderText = "Tipo";
            this.Tipo.MinimumWidth = 8;
            this.Tipo.Name = "Tipo";
            this.Tipo.Width = 150;
            // 
            // Descripción
            // 
            this.Descripción.HeaderText = "Descripción";
            this.Descripción.MinimumWidth = 8;
            this.Descripción.Name = "Descripción";
            this.Descripción.Width = 150;
            // 
            // Monto
            // 
            this.Monto.HeaderText = "Monto";
            this.Monto.MinimumWidth = 8;
            this.Monto.Name = "Monto";
            this.Monto.Width = 150;
            // 
            // lblAperturadecaja
            // 
            this.lblAperturadecaja.AutoSize = true;
            this.lblAperturadecaja.BackColor = System.Drawing.Color.Transparent;
            this.lblAperturadecaja.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAperturadecaja.ForeColor = System.Drawing.Color.Black;
            this.lblAperturadecaja.Location = new System.Drawing.Point(57, 287);
            this.lblAperturadecaja.Name = "lblAperturadecaja";
            this.lblAperturadecaja.Size = new System.Drawing.Size(195, 26);
            this.lblAperturadecaja.TabIndex = 76;
            this.lblAperturadecaja.Text = "Apertura de caja:";
            // 
            // txtIngresos
            // 
            this.txtIngresos.BackColor = System.Drawing.Color.White;
            this.txtIngresos.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIngresos.ForeColor = System.Drawing.Color.Black;
            this.txtIngresos.Location = new System.Drawing.Point(268, 335);
            this.txtIngresos.Multiline = true;
            this.txtIngresos.Name = "txtIngresos";
            this.txtIngresos.Size = new System.Drawing.Size(170, 36);
            this.txtIngresos.TabIndex = 75;
            // 
            // lblIngresos
            // 
            this.lblIngresos.AutoSize = true;
            this.lblIngresos.BackColor = System.Drawing.Color.Transparent;
            this.lblIngresos.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblIngresos.ForeColor = System.Drawing.Color.Black;
            this.lblIngresos.Location = new System.Drawing.Point(56, 335);
            this.lblIngresos.Name = "lblIngresos";
            this.lblIngresos.Size = new System.Drawing.Size(106, 26);
            this.lblIngresos.TabIndex = 78;
            this.lblIngresos.Text = "Ingresos:";
            // 
            // txtSaldoactual
            // 
            this.txtSaldoactual.BackColor = System.Drawing.Color.White;
            this.txtSaldoactual.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaldoactual.ForeColor = System.Drawing.Color.Black;
            this.txtSaldoactual.Location = new System.Drawing.Point(268, 384);
            this.txtSaldoactual.Multiline = true;
            this.txtSaldoactual.Name = "txtSaldoactual";
            this.txtSaldoactual.Size = new System.Drawing.Size(170, 37);
            this.txtSaldoactual.TabIndex = 77;
            // 
            // lblSaldoactual
            // 
            this.lblSaldoactual.AutoSize = true;
            this.lblSaldoactual.BackColor = System.Drawing.Color.Transparent;
            this.lblSaldoactual.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaldoactual.ForeColor = System.Drawing.Color.Black;
            this.lblSaldoactual.Location = new System.Drawing.Point(56, 384);
            this.lblSaldoactual.Name = "lblSaldoactual";
            this.lblSaldoactual.Size = new System.Drawing.Size(150, 26);
            this.lblSaldoactual.TabIndex = 80;
            this.lblSaldoactual.Text = "Saldo Actual:";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Bisque;
            this.groupBox1.Controls.Add(this.btnMenu);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnRegistrodeegresos);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.btnCierredecaja);
            this.groupBox1.Controls.Add(this.btnAperturadecaja);
            this.groupBox1.Controls.Add(this.txtegresos);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lblUsuario);
            this.groupBox1.Controls.Add(this.txtIngresos);
            this.groupBox1.Controls.Add(this.txtSaldoactual);
            this.groupBox1.Controls.Add(this.txtAperturadecaja);
            this.groupBox1.Controls.Add(this.lblSaldoactual);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.lblIngresos);
            this.groupBox1.Controls.Add(this.lblAperturadecaja);
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(35, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(840, 707);
            this.groupBox1.TabIndex = 81;
            this.groupBox1.TabStop = false;
            // 
            // btnMenu
            // 
            this.btnMenu.BackColor = System.Drawing.Color.Transparent;
            this.btnMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenu.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMenu.ForeColor = System.Drawing.Color.Black;
            this.btnMenu.Location = new System.Drawing.Point(677, 43);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(128, 34);
            this.btnMenu.TabIndex = 87;
            this.btnMenu.Text = "Menú";
            this.btnMenu.UseVisualStyleBackColor = false;
            this.btnMenu.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnRegistrodeegresos
            // 
            this.btnRegistrodeegresos.BackColor = System.Drawing.Color.Transparent;
            this.btnRegistrodeegresos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRegistrodeegresos.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRegistrodeegresos.ForeColor = System.Drawing.Color.Black;
            this.btnRegistrodeegresos.Location = new System.Drawing.Point(311, 655);
            this.btnRegistrodeegresos.Name = "btnRegistrodeegresos";
            this.btnRegistrodeegresos.Size = new System.Drawing.Size(224, 34);
            this.btnRegistrodeegresos.TabIndex = 86;
            this.btnRegistrodeegresos.Text = "Registro de egresos";
            this.btnRegistrodeegresos.UseVisualStyleBackColor = false;
            // 
            // btnAperturadecaja
            // 
            this.btnAperturadecaja.BackColor = System.Drawing.Color.Transparent;
            this.btnAperturadecaja.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAperturadecaja.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAperturadecaja.ForeColor = System.Drawing.Color.Black;
            this.btnAperturadecaja.Location = new System.Drawing.Point(36, 655);
            this.btnAperturadecaja.Name = "btnAperturadecaja";
            this.btnAperturadecaja.Size = new System.Drawing.Size(204, 34);
            this.btnAperturadecaja.TabIndex = 85;
            this.btnAperturadecaja.Text = "Apertura de caja:";
            this.btnAperturadecaja.UseVisualStyleBackColor = false;
            // 
            // txtegresos
            // 
            this.txtegresos.BackColor = System.Drawing.Color.White;
            this.txtegresos.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtegresos.ForeColor = System.Drawing.Color.Black;
            this.txtegresos.Location = new System.Drawing.Point(588, 327);
            this.txtegresos.Multiline = true;
            this.txtegresos.Name = "txtegresos";
            this.txtegresos.Size = new System.Drawing.Size(170, 36);
            this.txtegresos.TabIndex = 83;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(464, 335);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 26);
            this.label7.TabIndex = 82;
            this.label7.Text = "Egresos:";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.ForeColor = System.Drawing.Color.Black;
            this.lblUsuario.Location = new System.Drawing.Point(166, 246);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(86, 27);
            this.lblUsuario.TabIndex = 81;
            this.lblUsuario.Text = "Usuario";
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.DarkOrange;
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Location = new System.Drawing.Point(36, 439);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(714, 201);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // Caja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(927, 765);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Times New Roman", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Caja";
            this.Text = "Caja";
            this.Load += new System.EventHandler(this.Caja_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnCierredecaja;
        private System.Windows.Forms.TextBox txtAperturadecaja;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblAperturadecaja;
        private System.Windows.Forms.TextBox txtIngresos;
        private System.Windows.Forms.Label lblIngresos;
        private System.Windows.Forms.TextBox txtSaldoactual;
        private System.Windows.Forms.Label lblSaldoactual;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnRegistrodeegresos;
        private System.Windows.Forms.Button btnAperturadecaja;
        private System.Windows.Forms.TextBox txtegresos;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblUsuario;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descripción;
        private System.Windows.Forms.DataGridViewTextBoxColumn Monto;
        private System.Windows.Forms.Button btnMenu;
    }
}
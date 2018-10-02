namespace SharpNodeSettings.Forms
{
    partial class FormModbusTcpAlien
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if (disposing && (components != null))
            {
                components.Dispose( );
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( )
        {
            this.userButton1 = new HslCommunication.Controls.UserButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // userButton1
            // 
            this.userButton1.BackColor = System.Drawing.Color.Transparent;
            this.userButton1.CustomerInformation = "";
            this.userButton1.EnableColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(190)))), ((int)(((byte)(190)))));
            this.userButton1.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.userButton1.Location = new System.Drawing.Point(171, 178);
            this.userButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.userButton1.Name = "userButton1";
            this.userButton1.Size = new System.Drawing.Size(95, 28);
            this.userButton1.TabIndex = 26;
            this.userButton1.UIText = "确认";
            this.userButton1.Click += new System.EventHandler(this.userButton1_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(241, 111);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(94, 21);
            this.checkBox1.TabIndex = 25;
            this.checkBox1.Text = "起始地址为1";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(103, 109);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(105, 23);
            this.textBox5.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 17);
            this.label5.TabIndex = 23;
            this.label5.Text = "站号：";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(103, 77);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(212, 23);
            this.textBox3.TabIndex = 22;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 17);
            this.label3.TabIndex = 21;
            this.label3.Text = "DTU：";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(103, 46);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(317, 23);
            this.textBox2.TabIndex = 20;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 19;
            this.label2.Text = "节点备注：";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(103, 14);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(317, 23);
            this.textBox1.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 17;
            this.label1.Text = "节点名称：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Gray;
            this.label4.Location = new System.Drawing.Point(331, 80);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 17);
            this.label4.TabIndex = 27;
            this.label4.Text = "( 11 位字符 )";
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(241, 143);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(87, 21);
            this.checkBox3.TabIndex = 29;
            this.checkBox3.Text = "字符串倒转";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(102, 141);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(105, 25);
            this.comboBox1.TabIndex = 31;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(26, 144);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(68, 17);
            this.label8.TabIndex = 30;
            this.label8.Text = "数据格式：";
            // 
            // FormModbusTcpAlien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(453, 222);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.userButton1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "FormModbusTcpAlien";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "异形ModbusTcp的客户端";
            this.Load += new System.EventHandler(this.FormModbusTcpAlien_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private HslCommunication.Controls.UserButton userButton1;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label8;
    }
}
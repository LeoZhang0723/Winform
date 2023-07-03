namespace CH341App
{
    partial class I2C_W_R
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
            this.btnSavReDat = new System.Windows.Forms.Button();
            this.btnLodWrDat = new System.Windows.Forms.Button();
            this.chkNoStp = new System.Windows.Forms.CheckBox();
            this.txtWrLen = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtReadData = new System.Windows.Forms.TextBox();
            this.txtWriteData = new System.Windows.Forms.TextBox();
            this.nudReadLen = new System.Windows.Forms.NumericUpDown();
            this.btnI2CWrite = new System.Windows.Forms.Button();
            this.btnI2CStm = new System.Windows.Forms.Button();
            this.btnI2CRead = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.nudSlvAddr = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudReadLen)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlvAddr)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSavReDat
            // 
            this.btnSavReDat.Location = new System.Drawing.Point(250, 197);
            this.btnSavReDat.Name = "btnSavReDat";
            this.btnSavReDat.Size = new System.Drawing.Size(95, 23);
            this.btnSavReDat.TabIndex = 93;
            this.btnSavReDat.Text = "Save Date";
            this.btnSavReDat.UseVisualStyleBackColor = true;
            this.btnSavReDat.Click += new System.EventHandler(this.btnSavReDat_Click);
            // 
            // btnLodWrDat
            // 
            this.btnLodWrDat.Location = new System.Drawing.Point(250, 46);
            this.btnLodWrDat.Name = "btnLodWrDat";
            this.btnLodWrDat.Size = new System.Drawing.Size(95, 23);
            this.btnLodWrDat.TabIndex = 92;
            this.btnLodWrDat.Text = "Load Date";
            this.btnLodWrDat.UseVisualStyleBackColor = true;
            this.btnLodWrDat.Click += new System.EventHandler(this.btnLodWrDat_Click);
            // 
            // chkNoStp
            // 
            this.chkNoStp.AutoSize = true;
            this.chkNoStp.Location = new System.Drawing.Point(409, 163);
            this.chkNoStp.Name = "chkNoStp";
            this.chkNoStp.Size = new System.Drawing.Size(66, 16);
            this.chkNoStp.TabIndex = 91;
            this.chkNoStp.Text = "No Stop";
            this.chkNoStp.UseVisualStyleBackColor = true;
            // 
            // txtWrLen
            // 
            this.txtWrLen.Location = new System.Drawing.Point(409, 89);
            this.txtWrLen.Name = "txtWrLen";
            this.txtWrLen.ReadOnly = true;
            this.txtWrLen.Size = new System.Drawing.Size(110, 21);
            this.txtWrLen.TabIndex = 90;
            this.txtWrLen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(409, 68);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(113, 12);
            this.label9.TabIndex = 89;
            this.label9.Text = "Write Length(byte)";
            // 
            // txtReadData
            // 
            this.txtReadData.BackColor = System.Drawing.SystemColors.Info;
            this.txtReadData.Location = new System.Drawing.Point(12, 229);
            this.txtReadData.Multiline = true;
            this.txtReadData.Name = "txtReadData";
            this.txtReadData.ReadOnly = true;
            this.txtReadData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtReadData.Size = new System.Drawing.Size(391, 107);
            this.txtReadData.TabIndex = 84;
            this.txtReadData.TabStop = false;
            this.txtReadData.TextChanged += new System.EventHandler(this.txtReadData_TextChanged);
            // 
            // txtWriteData
            // 
            this.txtWriteData.Location = new System.Drawing.Point(12, 74);
            this.txtWriteData.Multiline = true;
            this.txtWriteData.Name = "txtWriteData";
            this.txtWriteData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtWriteData.Size = new System.Drawing.Size(391, 105);
            this.txtWriteData.TabIndex = 85;
            this.txtWriteData.TabStop = false;
            this.txtWriteData.TextChanged += new System.EventHandler(this.txtWriteData_TextChanged);
            // 
            // nudReadLen
            // 
            this.nudReadLen.Font = new System.Drawing.Font("宋体", 10F);
            this.nudReadLen.Location = new System.Drawing.Point(409, 257);
            this.nudReadLen.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.nudReadLen.Name = "nudReadLen";
            this.nudReadLen.Size = new System.Drawing.Size(110, 23);
            this.nudReadLen.TabIndex = 80;
            this.nudReadLen.TabStop = false;
            this.nudReadLen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudReadLen.ValueChanged += new System.EventHandler(this.nudReadLen_ValueChanged);
            // 
            // btnI2CWrite
            // 
            this.btnI2CWrite.Location = new System.Drawing.Point(409, 116);
            this.btnI2CWrite.Name = "btnI2CWrite";
            this.btnI2CWrite.Size = new System.Drawing.Size(110, 35);
            this.btnI2CWrite.TabIndex = 86;
            this.btnI2CWrite.TabStop = false;
            this.btnI2CWrite.Text = "I2C Write";
            this.btnI2CWrite.Click += new System.EventHandler(this.btnI2CWrite_Click);
            // 
            // btnI2CStm
            // 
            this.btnI2CStm.Location = new System.Drawing.Point(541, 175);
            this.btnI2CStm.Name = "btnI2CStm";
            this.btnI2CStm.Size = new System.Drawing.Size(120, 45);
            this.btnI2CStm.TabIndex = 87;
            this.btnI2CStm.TabStop = false;
            this.btnI2CStm.Text = "I2C Stream\r\n(Random Read)";
            this.btnI2CStm.Click += new System.EventHandler(this.btnI2CStm_Click);
            // 
            // btnI2CRead
            // 
            this.btnI2CRead.Location = new System.Drawing.Point(409, 301);
            this.btnI2CRead.Name = "btnI2CRead";
            this.btnI2CRead.Size = new System.Drawing.Size(110, 35);
            this.btnI2CRead.TabIndex = 88;
            this.btnI2CRead.TabStop = false;
            this.btnI2CRead.Text = "I2C Read";
            this.btnI2CRead.Click += new System.EventHandler(this.btnI2CRead_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(-179, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 12);
            this.label5.TabIndex = 81;
            this.label5.Text = "Read Data(hex)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(-179, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 12);
            this.label2.TabIndex = 82;
            this.label2.Text = "Write Data(hex)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(407, 230);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 12);
            this.label7.TabIndex = 83;
            this.label7.Text = "Read Length(byte)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudSlvAddr);
            this.groupBox1.Location = new System.Drawing.Point(409, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(158, 56);
            this.groupBox1.TabIndex = 94;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Slave Address_7bit(hex)";
            // 
            // nudSlvAddr
            // 
            this.nudSlvAddr.Font = new System.Drawing.Font("宋体", 9F);
            this.nudSlvAddr.Hexadecimal = true;
            this.nudSlvAddr.Location = new System.Drawing.Point(11, 19);
            this.nudSlvAddr.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.nudSlvAddr.Name = "nudSlvAddr";
            this.nudSlvAddr.Size = new System.Drawing.Size(100, 21);
            this.nudSlvAddr.TabIndex = 40;
            this.nudSlvAddr.TabStop = false;
            this.nudSlvAddr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudSlvAddr.ValueChanged += new System.EventHandler(this.nudSlvAddr_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 16F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(38, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(307, 22);
            this.label1.TabIndex = 95;
            this.label1.Text = "I2C General Debugger(CH341)";
            // 
            // I2C_W_R
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(673, 354);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSavReDat);
            this.Controls.Add(this.btnLodWrDat);
            this.Controls.Add(this.chkNoStp);
            this.Controls.Add(this.txtWrLen);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtReadData);
            this.Controls.Add(this.txtWriteData);
            this.Controls.Add(this.nudReadLen);
            this.Controls.Add(this.btnI2CWrite);
            this.Controls.Add(this.btnI2CStm);
            this.Controls.Add(this.btnI2CRead);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Name = "I2C_W_R";
            this.Text = "I2C_W_R";
            ((System.ComponentModel.ISupportInitialize)(this.nudReadLen)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudSlvAddr)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSavReDat;
        private System.Windows.Forms.Button btnLodWrDat;
        private System.Windows.Forms.CheckBox chkNoStp;
        private System.Windows.Forms.TextBox txtWrLen;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtReadData;
        private System.Windows.Forms.TextBox txtWriteData;
        private System.Windows.Forms.NumericUpDown nudReadLen;
        private System.Windows.Forms.Button btnI2CWrite;
        private System.Windows.Forms.Button btnI2CStm;
        private System.Windows.Forms.Button btnI2CRead;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown nudSlvAddr;
        private System.Windows.Forms.Label label1;
    }
}
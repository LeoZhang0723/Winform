namespace CH341App
{
    partial class frmCH341_I2C
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCH341_I2C));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.nudSlvAddr = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnScanAddr = new System.Windows.Forms.Button();
            this.cmbBitRat = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbI2CPort = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblIsCall = new System.Windows.Forms.Label();
            this.Update_Display_button = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Cell_Name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ADC_rd_vol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer_auto_Update = new System.Windows.Forms.Timer(this.components);
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.CC_disabled = new System.Windows.Forms.RadioButton();
            this.CC_one_shot = new System.Windows.Forms.RadioButton();
            this.CC_continous = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ADC_DIS = new System.Windows.Forms.RadioButton();
            this.ADC_EN = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.EXT_TEMP = new System.Windows.Forms.RadioButton();
            this.INT_TEMP = new System.Windows.Forms.RadioButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.Reset = new System.Windows.Forms.Button();
            this.ClearFault = new System.Windows.Forms.Button();
            this.CELLBAL3_SHOW = new System.Windows.Forms.TextBox();
            this.CELLBAL2_SHOW = new System.Windows.Forms.TextBox();
            this.CELLBAL1_SHOW = new System.Windows.Forms.TextBox();
            this.UV_TRIP_SHOW = new System.Windows.Forms.TextBox();
            this.OV_TRIP_SHOW = new System.Windows.Forms.TextBox();
            this.PROTECT3_SHOW = new System.Windows.Forms.TextBox();
            this.PROTECT2_SHOW = new System.Windows.Forms.TextBox();
            this.PROTECT1_SHOW = new System.Windows.Forms.TextBox();
            this.SYS_CTRL2_SHOW = new System.Windows.Forms.TextBox();
            this.SYS_CTRL1_SHOW = new System.Windows.Forms.TextBox();
            this.SYS_STAT_SHOW = new System.Windows.Forms.TextBox();
            this.Bit_Name = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.CB13 = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.CB7 = new System.Windows.Forms.ComboBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.UV_T0 = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.OV_T0 = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.RSVD10 = new System.Windows.Forms.ComboBox();
            this.System_Status = new System.Windows.Forms.Label();
            this.OCD_T0 = new System.Windows.Forms.ComboBox();
            this.SCD_T0 = new System.Windows.Forms.ComboBox();
            this.CB14 = new System.Windows.Forms.ComboBox();
            this.CHG_ON = new System.Windows.Forms.ComboBox();
            this.CB1 = new System.Windows.Forms.ComboBox();
            this.UV_T1 = new System.Windows.Forms.ComboBox();
            this.SHUTB = new System.Windows.Forms.ComboBox();
            this.OV_T1 = new System.Windows.Forms.ComboBox();
            this.CB8 = new System.Windows.Forms.ComboBox();
            this.RSVD9 = new System.Windows.Forms.ComboBox();
            this.OCD = new System.Windows.Forms.ComboBox();
            this.OCD_T1 = new System.Windows.Forms.ComboBox();
            this.SCD_T1 = new System.Windows.Forms.ComboBox();
            this.CB15 = new System.Windows.Forms.ComboBox();
            this.DSG_ON = new System.Windows.Forms.ComboBox();
            this.CB2 = new System.Windows.Forms.ComboBox();
            this.UV_T2 = new System.Windows.Forms.ComboBox();
            this.SHUTA = new System.Windows.Forms.ComboBox();
            this.OV_T2 = new System.Windows.Forms.ComboBox();
            this.CB9 = new System.Windows.Forms.ComboBox();
            this.RSVD8 = new System.Windows.Forms.ComboBox();
            this.SCD = new System.Windows.Forms.ComboBox();
            this.OCD_T2 = new System.Windows.Forms.ComboBox();
            this.SCD_T2 = new System.Windows.Forms.ComboBox();
            this.CB16 = new System.Windows.Forms.ComboBox();
            this.RSVD3 = new System.Windows.Forms.ComboBox();
            this.CB3 = new System.Windows.Forms.ComboBox();
            this.UV_T3 = new System.Windows.Forms.ComboBox();
            this.CB_HOST = new System.Windows.Forms.ComboBox();
            this.OV_T3 = new System.Windows.Forms.ComboBox();
            this.CB10 = new System.Windows.Forms.ComboBox();
            this.RSVD7 = new System.Windows.Forms.ComboBox();
            this.OV = new System.Windows.Forms.ComboBox();
            this.OCD_T3 = new System.Windows.Forms.ComboBox();
            this.SCD_D0 = new System.Windows.Forms.ComboBox();
            this.CB17 = new System.Windows.Forms.ComboBox();
            this.RSVD2 = new System.Windows.Forms.ComboBox();
            this.CB4 = new System.Windows.Forms.ComboBox();
            this.UV_T4 = new System.Windows.Forms.ComboBox();
            this.TEMP_S = new System.Windows.Forms.ComboBox();
            this.OV_T4 = new System.Windows.Forms.ComboBox();
            this.CB11 = new System.Windows.Forms.ComboBox();
            this.OV_D0 = new System.Windows.Forms.ComboBox();
            this.UV = new System.Windows.Forms.ComboBox();
            this.OCD_D0 = new System.Windows.Forms.ComboBox();
            this.SCD_D1 = new System.Windows.Forms.ComboBox();
            this.CB18 = new System.Windows.Forms.ComboBox();
            this.UV_T5 = new System.Windows.Forms.ComboBox();
            this.RSVD1 = new System.Windows.Forms.ComboBox();
            this.OV_T5 = new System.Windows.Forms.ComboBox();
            this.CB5 = new System.Windows.Forms.ComboBox();
            this.OV_D1 = new System.Windows.Forms.ComboBox();
            this.ADC_EN2 = new System.Windows.Forms.ComboBox();
            this.OCD_D1 = new System.Windows.Forms.ComboBox();
            this.RSVD5 = new System.Windows.Forms.ComboBox();
            this.UV_T6 = new System.Windows.Forms.ComboBox();
            this.CB12 = new System.Windows.Forms.ComboBox();
            this.OV_T6 = new System.Windows.Forms.ComboBox();
            this.CC_ONE = new System.Windows.Forms.ComboBox();
            this.UV_D0 = new System.Windows.Forms.ComboBox();
            this.OVRDAL = new System.Windows.Forms.ComboBox();
            this.OCD_D2 = new System.Windows.Forms.ComboBox();
            this.RSVD4 = new System.Windows.Forms.ComboBox();
            this.UV_T7 = new System.Windows.Forms.ComboBox();
            this.CB_ST0 = new System.Windows.Forms.ComboBox();
            this.OV_T7 = new System.Windows.Forms.ComboBox();
            this.CC_EN2 = new System.Windows.Forms.ComboBox();
            this.UV_D1 = new System.Windows.Forms.ComboBox();
            this.CB6 = new System.Windows.Forms.ComboBox();
            this.RSVD6 = new System.Windows.Forms.ComboBox();
            this.RSNS = new System.Windows.Forms.ComboBox();
            this.CB_ST1 = new System.Windows.Forms.ComboBox();
            this.DLY_DIS = new System.Windows.Forms.ComboBox();
            this.DEVXD = new System.Windows.Forms.ComboBox();
            this.LOAD_P = new System.Windows.Forms.ComboBox();
            this.INTCBDONE = new System.Windows.Forms.ComboBox();
            this.CCREADY_CHOS = new System.Windows.Forms.ComboBox();
            this.I2C_WR = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.ADCOffset_Val = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ADCGain_Val = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.donotSupportCRC = new System.Windows.Forms.RadioButton();
            this.SupportCRC = new System.Windows.Forms.RadioButton();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.Cell18_TEST = new System.Windows.Forms.RadioButton();
            this.Cell17_TEST = new System.Windows.Forms.RadioButton();
            this.Cell16_TEST = new System.Windows.Forms.RadioButton();
            this.Cell15_TEST = new System.Windows.Forms.RadioButton();
            this.Cell14_TEST = new System.Windows.Forms.RadioButton();
            this.Cell13_TEST = new System.Windows.Forms.RadioButton();
            this.Cell12_TEST = new System.Windows.Forms.RadioButton();
            this.Cell11_TEST = new System.Windows.Forms.RadioButton();
            this.Cell10_TEST = new System.Windows.Forms.RadioButton();
            this.Cell9_TEST = new System.Windows.Forms.RadioButton();
            this.Cell8_TEST = new System.Windows.Forms.RadioButton();
            this.Cell7_TEST = new System.Windows.Forms.RadioButton();
            this.Current_TEST = new System.Windows.Forms.RadioButton();
            this.Cell6_TEST = new System.Windows.Forms.RadioButton();
            this.Cell5_TEST = new System.Windows.Forms.RadioButton();
            this.Cell4_TEST = new System.Windows.Forms.RadioButton();
            this.Cell3_TEST = new System.Windows.Forms.RadioButton();
            this.Cell2_TEST = new System.Windows.Forms.RadioButton();
            this.Cell1_TEST = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlvAddr)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(855, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(61, 70);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 38;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 12);
            this.label3.TabIndex = 50;
            this.label3.Text = "USB-I2C Port";
            // 
            // nudSlvAddr
            // 
            this.nudSlvAddr.Font = new System.Drawing.Font("宋体", 9F);
            this.nudSlvAddr.Hexadecimal = true;
            this.nudSlvAddr.Location = new System.Drawing.Point(12, 19);
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
            this.label1.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(51, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(505, 21);
            this.label1.TabIndex = 56;
            this.label1.Text = "        BMS03 Evaluation Software            ";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(450, 10);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(113, 12);
            this.lblInfo.TabIndex = 57;
            this.lblInfo.Text = "Rev X.X XXXXXXXXXX";
            // 
            // btnScanAddr
            // 
            this.btnScanAddr.Location = new System.Drawing.Point(124, 16);
            this.btnScanAddr.Name = "btnScanAddr";
            this.btnScanAddr.Size = new System.Drawing.Size(106, 31);
            this.btnScanAddr.TabIndex = 55;
            this.btnScanAddr.TabStop = false;
            this.btnScanAddr.Text = "Scan";
            this.btnScanAddr.Click += new System.EventHandler(this.btnScanAddr_Click);
            // 
            // cmbBitRat
            // 
            this.cmbBitRat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBitRat.FormattingEnabled = true;
            this.cmbBitRat.Items.AddRange(new object[] {
            "  20kHz ",
            " 100kHz "});
            this.cmbBitRat.Location = new System.Drawing.Point(121, 52);
            this.cmbBitRat.Name = "cmbBitRat";
            this.cmbBitRat.Size = new System.Drawing.Size(81, 20);
            this.cmbBitRat.TabIndex = 58;
            this.cmbBitRat.TabStop = false;
            this.cmbBitRat.SelectedIndexChanged += new System.EventHandler(this.cmbBitRat_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(119, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 59;
            this.label8.Text = "Bit Rate";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // cmbI2CPort
            // 
            this.cmbI2CPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbI2CPort.FormattingEnabled = true;
            this.cmbI2CPort.Items.AddRange(new object[] {
            "Driver 1",
            "Driver 2",
            "Driver 3",
            "Driver 4"});
            this.cmbI2CPort.Location = new System.Drawing.Point(12, 52);
            this.cmbI2CPort.Name = "cmbI2CPort";
            this.cmbI2CPort.Size = new System.Drawing.Size(91, 20);
            this.cmbI2CPort.TabIndex = 66;
            this.cmbI2CPort.TabStop = false;
            this.cmbI2CPort.SelectedIndexChanged += new System.EventHandler(this.cmbI2CPort_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.nudSlvAddr);
            this.groupBox1.Controls.Add(this.btnScanAddr);
            this.groupBox1.Location = new System.Drawing.Point(231, 30);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(248, 56);
            this.groupBox1.TabIndex = 67;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Slave Address_7bit(hex)";
            // 
            // lblIsCall
            // 
            this.lblIsCall.AutoSize = true;
            this.lblIsCall.Location = new System.Drawing.Point(5, 5);
            this.lblIsCall.Name = "lblIsCall";
            this.lblIsCall.Size = new System.Drawing.Size(0, 12);
            this.lblIsCall.TabIndex = 83;
            // 
            // Update_Display_button
            // 
            this.Update_Display_button.Location = new System.Drawing.Point(606, 314);
            this.Update_Display_button.Name = "Update_Display_button";
            this.Update_Display_button.Size = new System.Drawing.Size(78, 50);
            this.Update_Display_button.TabIndex = 84;
            this.Update_Display_button.TabStop = false;
            this.Update_Display_button.Text = "Update Display";
            this.Update_Display_button.Click += new System.EventHandler(this.Update_Display_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Cell_Name,
            this.ADC_rd_vol});
            this.dataGridView1.Location = new System.Drawing.Point(718, 73);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(201, 564);
            this.dataGridView1.TabIndex = 86;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Cell_Name
            // 
            this.Cell_Name.HeaderText = "Cell";
            this.Cell_Name.Name = "Cell_Name";
            this.Cell_Name.ReadOnly = true;
            this.Cell_Name.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // ADC_rd_vol
            // 
            this.ADC_rd_vol.HeaderText = "V/V";
            this.ADC_rd_vol.Name = "ADC_rd_vol";
            this.ADC_rd_vol.ReadOnly = true;
            this.ADC_rd_vol.Width = 90;
            // 
            // timer_auto_Update
            // 
            this.timer_auto_Update.Interval = 250;
            this.timer_auto_Update.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.BackColor = System.Drawing.SystemColors.Control;
            this.checkBox1.Location = new System.Drawing.Point(120, 8);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(48, 16);
            this.checkBox1.TabIndex = 87;
            this.checkBox1.Text = "Scan";
            this.checkBox1.UseVisualStyleBackColor = false;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(108, 27);
            this.textBox1.MaxLength = 6;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(59, 21);
            this.textBox1.TabIndex = 88;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(20, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(167, 12);
            this.label4.TabIndex = 89;
            this.label4.Text = "can change to            ms";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.Control;
            this.label6.Location = new System.Drawing.Point(19, 51);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(149, 12);
            this.label6.TabIndex = 90;
            this.label6.Text = "limited to 250ms minimum";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.checkBox1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(396, 92);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 66);
            this.groupBox2.TabIndex = 92;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Data Scanning";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.CC_disabled);
            this.groupBox3.Controls.Add(this.CC_one_shot);
            this.groupBox3.Controls.Add(this.CC_continous);
            this.groupBox3.Font = new System.Drawing.Font("新宋体", 9F);
            this.groupBox3.Location = new System.Drawing.Point(21, 78);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(122, 84);
            this.groupBox3.TabIndex = 93;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Coulomb Counter";
            this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
            // 
            // CC_disabled
            // 
            this.CC_disabled.AutoSize = true;
            this.CC_disabled.Checked = true;
            this.CC_disabled.Location = new System.Drawing.Point(6, 64);
            this.CC_disabled.Name = "CC_disabled";
            this.CC_disabled.Size = new System.Drawing.Size(65, 16);
            this.CC_disabled.TabIndex = 94;
            this.CC_disabled.TabStop = true;
            this.CC_disabled.Text = "Disable";
            this.CC_disabled.UseVisualStyleBackColor = true;
            this.CC_disabled.CheckedChanged += new System.EventHandler(this.CC_disabled_CheckedChanged);
            // 
            // CC_one_shot
            // 
            this.CC_one_shot.AutoSize = true;
            this.CC_one_shot.Location = new System.Drawing.Point(6, 42);
            this.CC_one_shot.Name = "CC_one_shot";
            this.CC_one_shot.Size = new System.Drawing.Size(71, 16);
            this.CC_one_shot.TabIndex = 94;
            this.CC_one_shot.TabStop = true;
            this.CC_one_shot.Text = "One-Shot";
            this.CC_one_shot.UseVisualStyleBackColor = true;
            this.CC_one_shot.CheckedChanged += new System.EventHandler(this.CC_one_shot_CheckedChanged);
            // 
            // CC_continous
            // 
            this.CC_continous.AutoSize = true;
            this.CC_continous.Location = new System.Drawing.Point(6, 20);
            this.CC_continous.Name = "CC_continous";
            this.CC_continous.Size = new System.Drawing.Size(77, 16);
            this.CC_continous.TabIndex = 94;
            this.CC_continous.TabStop = true;
            this.CC_continous.Text = "Continous";
            this.CC_continous.UseVisualStyleBackColor = true;
            this.CC_continous.CheckedChanged += new System.EventHandler(this.CC_continous_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ADC_DIS);
            this.groupBox4.Controls.Add(this.ADC_EN);
            this.groupBox4.Location = new System.Drawing.Point(149, 92);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(91, 66);
            this.groupBox4.TabIndex = 94;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "ADC_EN";
            this.groupBox4.Enter += new System.EventHandler(this.groupBox4_Enter);
            // 
            // ADC_DIS
            // 
            this.ADC_DIS.AutoSize = true;
            this.ADC_DIS.Location = new System.Drawing.Point(7, 41);
            this.ADC_DIS.Name = "ADC_DIS";
            this.ADC_DIS.Size = new System.Drawing.Size(65, 16);
            this.ADC_DIS.TabIndex = 1;
            this.ADC_DIS.Text = "Disable";
            this.ADC_DIS.UseVisualStyleBackColor = true;
            this.ADC_DIS.CheckedChanged += new System.EventHandler(this.ADC_DIS_CheckedChanged);
            // 
            // ADC_EN
            // 
            this.ADC_EN.AutoSize = true;
            this.ADC_EN.Checked = true;
            this.ADC_EN.Location = new System.Drawing.Point(7, 19);
            this.ADC_EN.Name = "ADC_EN";
            this.ADC_EN.Size = new System.Drawing.Size(59, 16);
            this.ADC_EN.TabIndex = 0;
            this.ADC_EN.TabStop = true;
            this.ADC_EN.Text = "Enable";
            this.ADC_EN.UseVisualStyleBackColor = true;
            this.ADC_EN.CheckedChanged += new System.EventHandler(this.ADC_EN_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.EXT_TEMP);
            this.groupBox5.Controls.Add(this.INT_TEMP);
            this.groupBox5.Location = new System.Drawing.Point(253, 92);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(129, 66);
            this.groupBox5.TabIndex = 95;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Temperature Sensor";
            // 
            // EXT_TEMP
            // 
            this.EXT_TEMP.AutoSize = true;
            this.EXT_TEMP.Location = new System.Drawing.Point(7, 41);
            this.EXT_TEMP.Name = "EXT_TEMP";
            this.EXT_TEMP.Size = new System.Drawing.Size(71, 16);
            this.EXT_TEMP.TabIndex = 1;
            this.EXT_TEMP.Text = "External";
            this.EXT_TEMP.UseVisualStyleBackColor = true;
            this.EXT_TEMP.CheckedChanged += new System.EventHandler(this.EXT_TEMP_CheckedChanged);
            // 
            // INT_TEMP
            // 
            this.INT_TEMP.AutoSize = true;
            this.INT_TEMP.Checked = true;
            this.INT_TEMP.Location = new System.Drawing.Point(7, 18);
            this.INT_TEMP.Name = "INT_TEMP";
            this.INT_TEMP.Size = new System.Drawing.Size(71, 16);
            this.INT_TEMP.TabIndex = 0;
            this.INT_TEMP.TabStop = true;
            this.INT_TEMP.Text = "Internal";
            this.INT_TEMP.UseVisualStyleBackColor = true;
            this.INT_TEMP.CheckedChanged += new System.EventHandler(this.INT_TEMP_CheckedChanged);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.Reset);
            this.groupBox6.Controls.Add(this.ClearFault);
            this.groupBox6.Controls.Add(this.CELLBAL3_SHOW);
            this.groupBox6.Controls.Add(this.CELLBAL2_SHOW);
            this.groupBox6.Controls.Add(this.CELLBAL1_SHOW);
            this.groupBox6.Controls.Add(this.UV_TRIP_SHOW);
            this.groupBox6.Controls.Add(this.OV_TRIP_SHOW);
            this.groupBox6.Controls.Add(this.PROTECT3_SHOW);
            this.groupBox6.Controls.Add(this.PROTECT2_SHOW);
            this.groupBox6.Controls.Add(this.Update_Display_button);
            this.groupBox6.Controls.Add(this.PROTECT1_SHOW);
            this.groupBox6.Controls.Add(this.SYS_CTRL2_SHOW);
            this.groupBox6.Controls.Add(this.SYS_CTRL1_SHOW);
            this.groupBox6.Controls.Add(this.SYS_STAT_SHOW);
            this.groupBox6.Controls.Add(this.Bit_Name);
            this.groupBox6.Controls.Add(this.label13);
            this.groupBox6.Controls.Add(this.label12);
            this.groupBox6.Controls.Add(this.CB13);
            this.groupBox6.Controls.Add(this.label11);
            this.groupBox6.Controls.Add(this.CB7);
            this.groupBox6.Controls.Add(this.label20);
            this.groupBox6.Controls.Add(this.label19);
            this.groupBox6.Controls.Add(this.label18);
            this.groupBox6.Controls.Add(this.label17);
            this.groupBox6.Controls.Add(this.label16);
            this.groupBox6.Controls.Add(this.UV_T0);
            this.groupBox6.Controls.Add(this.label15);
            this.groupBox6.Controls.Add(this.OV_T0);
            this.groupBox6.Controls.Add(this.label14);
            this.groupBox6.Controls.Add(this.RSVD10);
            this.groupBox6.Controls.Add(this.System_Status);
            this.groupBox6.Controls.Add(this.OCD_T0);
            this.groupBox6.Controls.Add(this.SCD_T0);
            this.groupBox6.Controls.Add(this.CB14);
            this.groupBox6.Controls.Add(this.CHG_ON);
            this.groupBox6.Controls.Add(this.CB1);
            this.groupBox6.Controls.Add(this.UV_T1);
            this.groupBox6.Controls.Add(this.SHUTB);
            this.groupBox6.Controls.Add(this.OV_T1);
            this.groupBox6.Controls.Add(this.CB8);
            this.groupBox6.Controls.Add(this.RSVD9);
            this.groupBox6.Controls.Add(this.OCD);
            this.groupBox6.Controls.Add(this.OCD_T1);
            this.groupBox6.Controls.Add(this.SCD_T1);
            this.groupBox6.Controls.Add(this.CB15);
            this.groupBox6.Controls.Add(this.DSG_ON);
            this.groupBox6.Controls.Add(this.CB2);
            this.groupBox6.Controls.Add(this.UV_T2);
            this.groupBox6.Controls.Add(this.SHUTA);
            this.groupBox6.Controls.Add(this.OV_T2);
            this.groupBox6.Controls.Add(this.CB9);
            this.groupBox6.Controls.Add(this.RSVD8);
            this.groupBox6.Controls.Add(this.SCD);
            this.groupBox6.Controls.Add(this.OCD_T2);
            this.groupBox6.Controls.Add(this.SCD_T2);
            this.groupBox6.Controls.Add(this.CB16);
            this.groupBox6.Controls.Add(this.RSVD3);
            this.groupBox6.Controls.Add(this.CB3);
            this.groupBox6.Controls.Add(this.UV_T3);
            this.groupBox6.Controls.Add(this.CB_HOST);
            this.groupBox6.Controls.Add(this.OV_T3);
            this.groupBox6.Controls.Add(this.CB10);
            this.groupBox6.Controls.Add(this.RSVD7);
            this.groupBox6.Controls.Add(this.OV);
            this.groupBox6.Controls.Add(this.OCD_T3);
            this.groupBox6.Controls.Add(this.SCD_D0);
            this.groupBox6.Controls.Add(this.CB17);
            this.groupBox6.Controls.Add(this.RSVD2);
            this.groupBox6.Controls.Add(this.CB4);
            this.groupBox6.Controls.Add(this.UV_T4);
            this.groupBox6.Controls.Add(this.TEMP_S);
            this.groupBox6.Controls.Add(this.OV_T4);
            this.groupBox6.Controls.Add(this.CB11);
            this.groupBox6.Controls.Add(this.OV_D0);
            this.groupBox6.Controls.Add(this.UV);
            this.groupBox6.Controls.Add(this.OCD_D0);
            this.groupBox6.Controls.Add(this.SCD_D1);
            this.groupBox6.Controls.Add(this.CB18);
            this.groupBox6.Controls.Add(this.UV_T5);
            this.groupBox6.Controls.Add(this.RSVD1);
            this.groupBox6.Controls.Add(this.OV_T5);
            this.groupBox6.Controls.Add(this.CB5);
            this.groupBox6.Controls.Add(this.OV_D1);
            this.groupBox6.Controls.Add(this.ADC_EN2);
            this.groupBox6.Controls.Add(this.OCD_D1);
            this.groupBox6.Controls.Add(this.RSVD5);
            this.groupBox6.Controls.Add(this.UV_T6);
            this.groupBox6.Controls.Add(this.CB12);
            this.groupBox6.Controls.Add(this.OV_T6);
            this.groupBox6.Controls.Add(this.CC_ONE);
            this.groupBox6.Controls.Add(this.UV_D0);
            this.groupBox6.Controls.Add(this.OVRDAL);
            this.groupBox6.Controls.Add(this.OCD_D2);
            this.groupBox6.Controls.Add(this.RSVD4);
            this.groupBox6.Controls.Add(this.UV_T7);
            this.groupBox6.Controls.Add(this.CB_ST0);
            this.groupBox6.Controls.Add(this.OV_T7);
            this.groupBox6.Controls.Add(this.CC_EN2);
            this.groupBox6.Controls.Add(this.UV_D1);
            this.groupBox6.Controls.Add(this.CB6);
            this.groupBox6.Controls.Add(this.RSVD6);
            this.groupBox6.Controls.Add(this.RSNS);
            this.groupBox6.Controls.Add(this.CB_ST1);
            this.groupBox6.Controls.Add(this.DLY_DIS);
            this.groupBox6.Controls.Add(this.DEVXD);
            this.groupBox6.Controls.Add(this.LOAD_P);
            this.groupBox6.Controls.Add(this.INTCBDONE);
            this.groupBox6.Controls.Add(this.CCREADY_CHOS);
            this.groupBox6.Font = new System.Drawing.Font("新宋体", 9F);
            this.groupBox6.Location = new System.Drawing.Point(12, 168);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(694, 469);
            this.groupBox6.TabIndex = 97;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "All read/write registers";
            // 
            // Reset
            // 
            this.Reset.Location = new System.Drawing.Point(606, 191);
            this.Reset.Name = "Reset";
            this.Reset.Size = new System.Drawing.Size(75, 37);
            this.Reset.TabIndex = 99;
            this.Reset.Text = "Reset to Default";
            this.Reset.UseVisualStyleBackColor = true;
            this.Reset.Click += new System.EventHandler(this.Reset_Click);
            // 
            // ClearFault
            // 
            this.ClearFault.Location = new System.Drawing.Point(600, 94);
            this.ClearFault.Name = "ClearFault";
            this.ClearFault.Size = new System.Drawing.Size(88, 34);
            this.ClearFault.TabIndex = 98;
            this.ClearFault.Text = "Clear Faults";
            this.ClearFault.UseVisualStyleBackColor = true;
            this.ClearFault.Click += new System.EventHandler(this.ClearFault_Click);
            // 
            // CELLBAL3_SHOW
            // 
            this.CELLBAL3_SHOW.Location = new System.Drawing.Point(37, 166);
            this.CELLBAL3_SHOW.MaxLength = 8;
            this.CELLBAL3_SHOW.Name = "CELLBAL3_SHOW";
            this.CELLBAL3_SHOW.ReadOnly = true;
            this.CELLBAL3_SHOW.Size = new System.Drawing.Size(66, 21);
            this.CELLBAL3_SHOW.TabIndex = 3;
            // 
            // CELLBAL2_SHOW
            // 
            this.CELLBAL2_SHOW.Location = new System.Drawing.Point(37, 129);
            this.CELLBAL2_SHOW.MaxLength = 8;
            this.CELLBAL2_SHOW.Name = "CELLBAL2_SHOW";
            this.CELLBAL2_SHOW.ReadOnly = true;
            this.CELLBAL2_SHOW.Size = new System.Drawing.Size(66, 21);
            this.CELLBAL2_SHOW.TabIndex = 3;
            // 
            // CELLBAL1_SHOW
            // 
            this.CELLBAL1_SHOW.Location = new System.Drawing.Point(37, 91);
            this.CELLBAL1_SHOW.MaxLength = 8;
            this.CELLBAL1_SHOW.Name = "CELLBAL1_SHOW";
            this.CELLBAL1_SHOW.ReadOnly = true;
            this.CELLBAL1_SHOW.Size = new System.Drawing.Size(66, 21);
            this.CELLBAL1_SHOW.TabIndex = 3;
            // 
            // UV_TRIP_SHOW
            // 
            this.UV_TRIP_SHOW.Location = new System.Drawing.Point(37, 439);
            this.UV_TRIP_SHOW.MaxLength = 9;
            this.UV_TRIP_SHOW.Name = "UV_TRIP_SHOW";
            this.UV_TRIP_SHOW.ReadOnly = true;
            this.UV_TRIP_SHOW.Size = new System.Drawing.Size(66, 21);
            this.UV_TRIP_SHOW.TabIndex = 3;
            // 
            // OV_TRIP_SHOW
            // 
            this.OV_TRIP_SHOW.Location = new System.Drawing.Point(37, 399);
            this.OV_TRIP_SHOW.MaxLength = 9;
            this.OV_TRIP_SHOW.Name = "OV_TRIP_SHOW";
            this.OV_TRIP_SHOW.ReadOnly = true;
            this.OV_TRIP_SHOW.Size = new System.Drawing.Size(66, 21);
            this.OV_TRIP_SHOW.TabIndex = 3;
            // 
            // PROTECT3_SHOW
            // 
            this.PROTECT3_SHOW.Location = new System.Drawing.Point(37, 361);
            this.PROTECT3_SHOW.MaxLength = 9;
            this.PROTECT3_SHOW.Name = "PROTECT3_SHOW";
            this.PROTECT3_SHOW.ReadOnly = true;
            this.PROTECT3_SHOW.Size = new System.Drawing.Size(66, 21);
            this.PROTECT3_SHOW.TabIndex = 3;
            // 
            // PROTECT2_SHOW
            // 
            this.PROTECT2_SHOW.Location = new System.Drawing.Point(37, 323);
            this.PROTECT2_SHOW.MaxLength = 9;
            this.PROTECT2_SHOW.Name = "PROTECT2_SHOW";
            this.PROTECT2_SHOW.ReadOnly = true;
            this.PROTECT2_SHOW.Size = new System.Drawing.Size(66, 21);
            this.PROTECT2_SHOW.TabIndex = 3;
            // 
            // PROTECT1_SHOW
            // 
            this.PROTECT1_SHOW.Location = new System.Drawing.Point(37, 284);
            this.PROTECT1_SHOW.MaxLength = 9;
            this.PROTECT1_SHOW.Name = "PROTECT1_SHOW";
            this.PROTECT1_SHOW.ReadOnly = true;
            this.PROTECT1_SHOW.Size = new System.Drawing.Size(66, 21);
            this.PROTECT1_SHOW.TabIndex = 3;
            // 
            // SYS_CTRL2_SHOW
            // 
            this.SYS_CTRL2_SHOW.Location = new System.Drawing.Point(37, 245);
            this.SYS_CTRL2_SHOW.MaxLength = 9;
            this.SYS_CTRL2_SHOW.Name = "SYS_CTRL2_SHOW";
            this.SYS_CTRL2_SHOW.ReadOnly = true;
            this.SYS_CTRL2_SHOW.Size = new System.Drawing.Size(66, 21);
            this.SYS_CTRL2_SHOW.TabIndex = 3;
            // 
            // SYS_CTRL1_SHOW
            // 
            this.SYS_CTRL1_SHOW.Location = new System.Drawing.Point(37, 207);
            this.SYS_CTRL1_SHOW.MaxLength = 9;
            this.SYS_CTRL1_SHOW.Name = "SYS_CTRL1_SHOW";
            this.SYS_CTRL1_SHOW.ReadOnly = true;
            this.SYS_CTRL1_SHOW.Size = new System.Drawing.Size(66, 21);
            this.SYS_CTRL1_SHOW.TabIndex = 3;
            // 
            // SYS_STAT_SHOW
            // 
            this.SYS_STAT_SHOW.Location = new System.Drawing.Point(37, 57);
            this.SYS_STAT_SHOW.MaxLength = 9;
            this.SYS_STAT_SHOW.Name = "SYS_STAT_SHOW";
            this.SYS_STAT_SHOW.ReadOnly = true;
            this.SYS_STAT_SHOW.Size = new System.Drawing.Size(66, 21);
            this.SYS_STAT_SHOW.TabIndex = 3;
            // 
            // Bit_Name
            // 
            this.Bit_Name.AutoSize = true;
            this.Bit_Name.Font = new System.Drawing.Font("新宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Bit_Name.Location = new System.Drawing.Point(124, 26);
            this.Bit_Name.Name = "Bit_Name";
            this.Bit_Name.Size = new System.Drawing.Size(449, 12);
            this.Bit_Name.TabIndex = 2;
            this.Bit_Name.Text = "Bit7      Bit6      Bit5      Bit4      Bit3      Bit2      Bit1      Bit0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("新宋体", 9F);
            this.label13.Location = new System.Drawing.Point(7, 153);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(563, 12);
            this.label13.TabIndex = 1;
            this.label13.Text = "CELLBAL3 (0x03)                         CB18      CB17      CB16      CB15     CB" +
    "14      CB13";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("新宋体", 9F);
            this.label12.Location = new System.Drawing.Point(7, 116);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(563, 12);
            this.label12.TabIndex = 1;
            this.label12.Text = "CELLBAL2 (0x02)                         CB12      CB11      CB10      CB9       C" +
    "B8       CB7";
            // 
            // CB13
            // 
            this.CB13.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB13.FormattingEnabled = true;
            this.CB13.Items.AddRange(new object[] {
            "0",
            "1"});
            this.CB13.Location = new System.Drawing.Point(538, 168);
            this.CB13.MaxDropDownItems = 2;
            this.CB13.Name = "CB13";
            this.CB13.Size = new System.Drawing.Size(42, 20);
            this.CB13.TabIndex = 0;
            this.CB13.SelectedIndexChanged += new System.EventHandler(this.CB13_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("新宋体", 9F);
            this.label11.Location = new System.Drawing.Point(7, 78);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(563, 12);
            this.label11.TabIndex = 1;
            this.label11.Text = "CELLBAL1 (0x01)                         CB6       CB5        CB4      CB3       C" +
    "B2       CB1";
            // 
            // CB7
            // 
            this.CB7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB7.FormattingEnabled = true;
            this.CB7.Items.AddRange(new object[] {
            "0",
            "1"});
            this.CB7.Location = new System.Drawing.Point(538, 131);
            this.CB7.MaxDropDownItems = 2;
            this.CB7.Name = "CB7";
            this.CB7.Size = new System.Drawing.Size(42, 20);
            this.CB7.TabIndex = 0;
            this.CB7.SelectedIndexChanged += new System.EventHandler(this.CB7_SelectedIndexChanged);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("新宋体", 9F);
            this.label20.Location = new System.Drawing.Point(7, 423);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(569, 12);
            this.label20.TabIndex = 1;
            this.label20.Text = "UV_TRIP (0x0A)     UV_T7     UV_T6     UV_T5     UV_T4     UV_T3     UV_T2     UV" +
    "_T1     UV_T0";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("新宋体", 9F);
            this.label19.Location = new System.Drawing.Point(7, 383);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(569, 12);
            this.label19.TabIndex = 1;
            this.label19.Text = "OV_TRIP (0x09)     OV_T7     OV_T6     OV_T5     OV_T4     OV_T3     OV_T2     OV" +
    "_T1     OV_T0";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("新宋体", 9F);
            this.label18.Location = new System.Drawing.Point(7, 345);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(569, 12);
            this.label18.TabIndex = 1;
            this.label18.Text = "PROTECT3 (0x08)    UV_D1     UV_D0     OV_D1     OV_D0      RSVD     RSVD      RS" +
    "VD       RSVD";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("新宋体", 9F);
            this.label17.Location = new System.Drawing.Point(7, 307);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(575, 12);
            this.label17.TabIndex = 1;
            this.label17.Text = "PROTECT2 (0x07)     RSVD     OCD_D2    OCD_D1    OCD_D0    OCD_T3    OCD_T2    OC" +
    "D_T1    OCD_T0";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("新宋体", 9F);
            this.label16.Location = new System.Drawing.Point(7, 268);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(575, 12);
            this.label16.TabIndex = 1;
            this.label16.Text = "PROTECT1 (0x06)     RSNS     INTTO1    INTTO0    SCD_D1    SCD_D0    SCD_T2    SC" +
    "D_T1    SCD_T0";
            this.label16.Click += new System.EventHandler(this.label16_Click);
            // 
            // UV_T0
            // 
            this.UV_T0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UV_T0.FormattingEnabled = true;
            this.UV_T0.Items.AddRange(new object[] {
            "0",
            "1"});
            this.UV_T0.Location = new System.Drawing.Point(538, 439);
            this.UV_T0.MaxDropDownItems = 2;
            this.UV_T0.Name = "UV_T0";
            this.UV_T0.Size = new System.Drawing.Size(42, 20);
            this.UV_T0.TabIndex = 0;
            this.UV_T0.SelectedIndexChanged += new System.EventHandler(this.UV_T0_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("新宋体", 9F);
            this.label15.Location = new System.Drawing.Point(7, 229);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(575, 12);
            this.label15.TabIndex = 1;
            this.label15.Text = "SYS_CTRL2 (0x05)  DLY_DIS    CC_EN     CC_ONE     RSVD      RSVD     RSVD      DS" +
    "G_ON    CHG_ON";
            // 
            // OV_T0
            // 
            this.OV_T0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OV_T0.FormattingEnabled = true;
            this.OV_T0.Items.AddRange(new object[] {
            "0",
            "1"});
            this.OV_T0.Location = new System.Drawing.Point(538, 399);
            this.OV_T0.MaxDropDownItems = 2;
            this.OV_T0.Name = "OV_T0";
            this.OV_T0.Size = new System.Drawing.Size(42, 20);
            this.OV_T0.TabIndex = 0;
            this.OV_T0.SelectedIndexChanged += new System.EventHandler(this.OV_T0_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("新宋体", 9F);
            this.label14.Location = new System.Drawing.Point(7, 191);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(569, 12);
            this.label14.TabIndex = 1;
            this.label14.Text = "SYS_CTRL1 (0x04)  LOAD_P     CB_ST1    CB_ST0    ADC_EN    TEMP_S   CB_HOST    SH" +
    "UTA     SHUTB";
            // 
            // RSVD10
            // 
            this.RSVD10.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RSVD10.FormattingEnabled = true;
            this.RSVD10.Items.AddRange(new object[] {
            "0",
            "1"});
            this.RSVD10.Location = new System.Drawing.Point(538, 361);
            this.RSVD10.MaxDropDownItems = 2;
            this.RSVD10.Name = "RSVD10";
            this.RSVD10.Size = new System.Drawing.Size(42, 20);
            this.RSVD10.TabIndex = 0;
            this.RSVD10.SelectedIndexChanged += new System.EventHandler(this.RSVD10_SelectedIndexChanged);
            // 
            // System_Status
            // 
            this.System_Status.AutoSize = true;
            this.System_Status.Font = new System.Drawing.Font("新宋体", 9F);
            this.System_Status.Location = new System.Drawing.Point(7, 41);
            this.System_Status.Name = "System_Status";
            this.System_Status.Size = new System.Drawing.Size(563, 12);
            this.System_Status.TabIndex = 1;
            this.System_Status.Text = "SYS_STAT (0x00)  CC_Ready   INTCBDONE  DEV_XD    OVRDAL      UV       OV        S" +
    "CD       OCD";
            // 
            // OCD_T0
            // 
            this.OCD_T0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OCD_T0.FormattingEnabled = true;
            this.OCD_T0.Items.AddRange(new object[] {
            "0",
            "1"});
            this.OCD_T0.Location = new System.Drawing.Point(538, 323);
            this.OCD_T0.MaxDropDownItems = 2;
            this.OCD_T0.Name = "OCD_T0";
            this.OCD_T0.Size = new System.Drawing.Size(42, 20);
            this.OCD_T0.TabIndex = 0;
            this.OCD_T0.SelectedIndexChanged += new System.EventHandler(this.OCD_T0_SelectedIndexChanged);
            // 
            // SCD_T0
            // 
            this.SCD_T0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SCD_T0.FormattingEnabled = true;
            this.SCD_T0.Items.AddRange(new object[] {
            "0",
            "1"});
            this.SCD_T0.Location = new System.Drawing.Point(538, 284);
            this.SCD_T0.MaxDropDownItems = 2;
            this.SCD_T0.Name = "SCD_T0";
            this.SCD_T0.Size = new System.Drawing.Size(42, 20);
            this.SCD_T0.TabIndex = 0;
            this.SCD_T0.SelectedIndexChanged += new System.EventHandler(this.SCD_T0_SelectedIndexChanged);
            // 
            // CB14
            // 
            this.CB14.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB14.FormattingEnabled = true;
            this.CB14.Items.AddRange(new object[] {
            "0",
            "1"});
            this.CB14.Location = new System.Drawing.Point(477, 168);
            this.CB14.MaxDropDownItems = 2;
            this.CB14.Name = "CB14";
            this.CB14.Size = new System.Drawing.Size(42, 20);
            this.CB14.TabIndex = 0;
            this.CB14.SelectedIndexChanged += new System.EventHandler(this.CB14_SelectedIndexChanged);
            // 
            // CHG_ON
            // 
            this.CHG_ON.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CHG_ON.FormattingEnabled = true;
            this.CHG_ON.Items.AddRange(new object[] {
            "0",
            "1"});
            this.CHG_ON.Location = new System.Drawing.Point(538, 245);
            this.CHG_ON.MaxDropDownItems = 2;
            this.CHG_ON.Name = "CHG_ON";
            this.CHG_ON.Size = new System.Drawing.Size(42, 20);
            this.CHG_ON.TabIndex = 0;
            this.CHG_ON.SelectedIndexChanged += new System.EventHandler(this.CHG_ON_SelectedIndexChanged);
            // 
            // CB1
            // 
            this.CB1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB1.FormattingEnabled = true;
            this.CB1.Items.AddRange(new object[] {
            "0",
            "1"});
            this.CB1.Location = new System.Drawing.Point(538, 93);
            this.CB1.MaxDropDownItems = 2;
            this.CB1.Name = "CB1";
            this.CB1.Size = new System.Drawing.Size(42, 20);
            this.CB1.TabIndex = 0;
            this.CB1.SelectedIndexChanged += new System.EventHandler(this.CB1_SelectedIndexChanged);
            // 
            // UV_T1
            // 
            this.UV_T1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UV_T1.FormattingEnabled = true;
            this.UV_T1.Items.AddRange(new object[] {
            "0",
            "1"});
            this.UV_T1.Location = new System.Drawing.Point(477, 439);
            this.UV_T1.MaxDropDownItems = 2;
            this.UV_T1.Name = "UV_T1";
            this.UV_T1.Size = new System.Drawing.Size(42, 20);
            this.UV_T1.TabIndex = 0;
            this.UV_T1.SelectedIndexChanged += new System.EventHandler(this.UV_T1_SelectedIndexChanged);
            // 
            // SHUTB
            // 
            this.SHUTB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SHUTB.FormattingEnabled = true;
            this.SHUTB.Items.AddRange(new object[] {
            "0",
            "1"});
            this.SHUTB.Location = new System.Drawing.Point(538, 207);
            this.SHUTB.MaxDropDownItems = 2;
            this.SHUTB.Name = "SHUTB";
            this.SHUTB.Size = new System.Drawing.Size(42, 20);
            this.SHUTB.TabIndex = 0;
            this.SHUTB.SelectedIndexChanged += new System.EventHandler(this.SHUTB_SelectedIndexChanged);
            // 
            // OV_T1
            // 
            this.OV_T1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OV_T1.FormattingEnabled = true;
            this.OV_T1.Items.AddRange(new object[] {
            "0",
            "1"});
            this.OV_T1.Location = new System.Drawing.Point(477, 399);
            this.OV_T1.MaxDropDownItems = 2;
            this.OV_T1.Name = "OV_T1";
            this.OV_T1.Size = new System.Drawing.Size(42, 20);
            this.OV_T1.TabIndex = 0;
            this.OV_T1.SelectedIndexChanged += new System.EventHandler(this.OV_T1_SelectedIndexChanged);
            // 
            // CB8
            // 
            this.CB8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB8.FormattingEnabled = true;
            this.CB8.Items.AddRange(new object[] {
            "0",
            "1"});
            this.CB8.Location = new System.Drawing.Point(477, 131);
            this.CB8.MaxDropDownItems = 2;
            this.CB8.Name = "CB8";
            this.CB8.Size = new System.Drawing.Size(42, 20);
            this.CB8.TabIndex = 0;
            this.CB8.SelectedIndexChanged += new System.EventHandler(this.CB8_SelectedIndexChanged);
            // 
            // RSVD9
            // 
            this.RSVD9.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RSVD9.FormattingEnabled = true;
            this.RSVD9.Items.AddRange(new object[] {
            "0",
            "1"});
            this.RSVD9.Location = new System.Drawing.Point(477, 361);
            this.RSVD9.MaxDropDownItems = 2;
            this.RSVD9.Name = "RSVD9";
            this.RSVD9.Size = new System.Drawing.Size(42, 20);
            this.RSVD9.TabIndex = 0;
            this.RSVD9.SelectedIndexChanged += new System.EventHandler(this.RSVD9_SelectedIndexChanged);
            // 
            // OCD
            // 
            this.OCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OCD.FormattingEnabled = true;
            this.OCD.Items.AddRange(new object[] {
            "0",
            "1"});
            this.OCD.Location = new System.Drawing.Point(538, 57);
            this.OCD.MaxDropDownItems = 2;
            this.OCD.Name = "OCD";
            this.OCD.Size = new System.Drawing.Size(42, 20);
            this.OCD.TabIndex = 0;
            this.OCD.SelectedIndexChanged += new System.EventHandler(this.OCD_SelectedIndexChanged);
            // 
            // OCD_T1
            // 
            this.OCD_T1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OCD_T1.FormattingEnabled = true;
            this.OCD_T1.Items.AddRange(new object[] {
            "0",
            "1"});
            this.OCD_T1.Location = new System.Drawing.Point(477, 323);
            this.OCD_T1.MaxDropDownItems = 2;
            this.OCD_T1.Name = "OCD_T1";
            this.OCD_T1.Size = new System.Drawing.Size(42, 20);
            this.OCD_T1.TabIndex = 0;
            this.OCD_T1.SelectedIndexChanged += new System.EventHandler(this.OCD_T1_SelectedIndexChanged);
            // 
            // SCD_T1
            // 
            this.SCD_T1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SCD_T1.FormattingEnabled = true;
            this.SCD_T1.Items.AddRange(new object[] {
            "0",
            "1"});
            this.SCD_T1.Location = new System.Drawing.Point(477, 284);
            this.SCD_T1.MaxDropDownItems = 2;
            this.SCD_T1.Name = "SCD_T1";
            this.SCD_T1.Size = new System.Drawing.Size(42, 20);
            this.SCD_T1.TabIndex = 0;
            this.SCD_T1.SelectedIndexChanged += new System.EventHandler(this.SCD_T1_SelectedIndexChanged);
            // 
            // CB15
            // 
            this.CB15.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB15.FormattingEnabled = true;
            this.CB15.Items.AddRange(new object[] {
            "0",
            "1"});
            this.CB15.Location = new System.Drawing.Point(416, 168);
            this.CB15.MaxDropDownItems = 2;
            this.CB15.Name = "CB15";
            this.CB15.Size = new System.Drawing.Size(42, 20);
            this.CB15.TabIndex = 0;
            this.CB15.SelectedIndexChanged += new System.EventHandler(this.CB15_SelectedIndexChanged);
            // 
            // DSG_ON
            // 
            this.DSG_ON.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DSG_ON.FormattingEnabled = true;
            this.DSG_ON.Items.AddRange(new object[] {
            "0",
            "1"});
            this.DSG_ON.Location = new System.Drawing.Point(477, 245);
            this.DSG_ON.MaxDropDownItems = 2;
            this.DSG_ON.Name = "DSG_ON";
            this.DSG_ON.Size = new System.Drawing.Size(42, 20);
            this.DSG_ON.TabIndex = 0;
            this.DSG_ON.SelectedIndexChanged += new System.EventHandler(this.DSG_ON_SelectedIndexChanged);
            // 
            // CB2
            // 
            this.CB2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB2.FormattingEnabled = true;
            this.CB2.Items.AddRange(new object[] {
            "0",
            "1"});
            this.CB2.Location = new System.Drawing.Point(477, 93);
            this.CB2.MaxDropDownItems = 2;
            this.CB2.Name = "CB2";
            this.CB2.Size = new System.Drawing.Size(42, 20);
            this.CB2.TabIndex = 0;
            this.CB2.SelectedIndexChanged += new System.EventHandler(this.CB2_SelectedIndexChanged);
            // 
            // UV_T2
            // 
            this.UV_T2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UV_T2.FormattingEnabled = true;
            this.UV_T2.Items.AddRange(new object[] {
            "0",
            "1"});
            this.UV_T2.Location = new System.Drawing.Point(416, 439);
            this.UV_T2.MaxDropDownItems = 2;
            this.UV_T2.Name = "UV_T2";
            this.UV_T2.Size = new System.Drawing.Size(42, 20);
            this.UV_T2.TabIndex = 0;
            this.UV_T2.SelectedIndexChanged += new System.EventHandler(this.UV_T2_SelectedIndexChanged);
            // 
            // SHUTA
            // 
            this.SHUTA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SHUTA.FormattingEnabled = true;
            this.SHUTA.Items.AddRange(new object[] {
            "0",
            "1"});
            this.SHUTA.Location = new System.Drawing.Point(477, 207);
            this.SHUTA.MaxDropDownItems = 2;
            this.SHUTA.Name = "SHUTA";
            this.SHUTA.Size = new System.Drawing.Size(42, 20);
            this.SHUTA.TabIndex = 0;
            this.SHUTA.SelectedIndexChanged += new System.EventHandler(this.SHUTA_SelectedIndexChanged);
            // 
            // OV_T2
            // 
            this.OV_T2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OV_T2.FormattingEnabled = true;
            this.OV_T2.Items.AddRange(new object[] {
            "0",
            "1"});
            this.OV_T2.Location = new System.Drawing.Point(416, 399);
            this.OV_T2.MaxDropDownItems = 2;
            this.OV_T2.Name = "OV_T2";
            this.OV_T2.Size = new System.Drawing.Size(42, 20);
            this.OV_T2.TabIndex = 0;
            this.OV_T2.SelectedIndexChanged += new System.EventHandler(this.OV_T2_SelectedIndexChanged);
            // 
            // CB9
            // 
            this.CB9.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB9.FormattingEnabled = true;
            this.CB9.Items.AddRange(new object[] {
            "0",
            "1"});
            this.CB9.Location = new System.Drawing.Point(416, 131);
            this.CB9.MaxDropDownItems = 2;
            this.CB9.Name = "CB9";
            this.CB9.Size = new System.Drawing.Size(42, 20);
            this.CB9.TabIndex = 0;
            this.CB9.SelectedIndexChanged += new System.EventHandler(this.CB9_SelectedIndexChanged);
            // 
            // RSVD8
            // 
            this.RSVD8.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RSVD8.FormattingEnabled = true;
            this.RSVD8.Items.AddRange(new object[] {
            "0",
            "1"});
            this.RSVD8.Location = new System.Drawing.Point(416, 361);
            this.RSVD8.MaxDropDownItems = 2;
            this.RSVD8.Name = "RSVD8";
            this.RSVD8.Size = new System.Drawing.Size(42, 20);
            this.RSVD8.TabIndex = 0;
            this.RSVD8.SelectedIndexChanged += new System.EventHandler(this.RSVD8_SelectedIndexChanged);
            // 
            // SCD
            // 
            this.SCD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SCD.FormattingEnabled = true;
            this.SCD.Items.AddRange(new object[] {
            "0",
            "1"});
            this.SCD.Location = new System.Drawing.Point(477, 57);
            this.SCD.MaxDropDownItems = 2;
            this.SCD.Name = "SCD";
            this.SCD.Size = new System.Drawing.Size(42, 20);
            this.SCD.TabIndex = 0;
            this.SCD.SelectedIndexChanged += new System.EventHandler(this.SCD_SelectedIndexChanged);
            // 
            // OCD_T2
            // 
            this.OCD_T2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OCD_T2.FormattingEnabled = true;
            this.OCD_T2.Items.AddRange(new object[] {
            "0",
            "1"});
            this.OCD_T2.Location = new System.Drawing.Point(416, 323);
            this.OCD_T2.MaxDropDownItems = 2;
            this.OCD_T2.Name = "OCD_T2";
            this.OCD_T2.Size = new System.Drawing.Size(42, 20);
            this.OCD_T2.TabIndex = 0;
            this.OCD_T2.SelectedIndexChanged += new System.EventHandler(this.OCD_T2_SelectedIndexChanged);
            // 
            // SCD_T2
            // 
            this.SCD_T2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SCD_T2.FormattingEnabled = true;
            this.SCD_T2.Items.AddRange(new object[] {
            "0",
            "1"});
            this.SCD_T2.Location = new System.Drawing.Point(416, 284);
            this.SCD_T2.MaxDropDownItems = 2;
            this.SCD_T2.Name = "SCD_T2";
            this.SCD_T2.Size = new System.Drawing.Size(42, 20);
            this.SCD_T2.TabIndex = 0;
            this.SCD_T2.SelectedIndexChanged += new System.EventHandler(this.SCD_T2_SelectedIndexChanged);
            // 
            // CB16
            // 
            this.CB16.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB16.FormattingEnabled = true;
            this.CB16.Items.AddRange(new object[] {
            "0",
            "1"});
            this.CB16.Location = new System.Drawing.Point(361, 168);
            this.CB16.MaxDropDownItems = 2;
            this.CB16.Name = "CB16";
            this.CB16.Size = new System.Drawing.Size(42, 20);
            this.CB16.TabIndex = 0;
            this.CB16.SelectedIndexChanged += new System.EventHandler(this.CB16_SelectedIndexChanged);
            // 
            // RSVD3
            // 
            this.RSVD3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RSVD3.FormattingEnabled = true;
            this.RSVD3.Items.AddRange(new object[] {
            "0",
            "1"});
            this.RSVD3.Location = new System.Drawing.Point(416, 245);
            this.RSVD3.MaxDropDownItems = 2;
            this.RSVD3.Name = "RSVD3";
            this.RSVD3.Size = new System.Drawing.Size(42, 20);
            this.RSVD3.TabIndex = 0;
            this.RSVD3.SelectedIndexChanged += new System.EventHandler(this.RSVD3_SelectedIndexChanged);
            // 
            // CB3
            // 
            this.CB3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB3.FormattingEnabled = true;
            this.CB3.Items.AddRange(new object[] {
            "0",
            "1"});
            this.CB3.Location = new System.Drawing.Point(416, 93);
            this.CB3.MaxDropDownItems = 2;
            this.CB3.Name = "CB3";
            this.CB3.Size = new System.Drawing.Size(42, 20);
            this.CB3.TabIndex = 0;
            this.CB3.SelectedIndexChanged += new System.EventHandler(this.CB3_SelectedIndexChanged);
            // 
            // UV_T3
            // 
            this.UV_T3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UV_T3.FormattingEnabled = true;
            this.UV_T3.Items.AddRange(new object[] {
            "0",
            "1"});
            this.UV_T3.Location = new System.Drawing.Point(361, 439);
            this.UV_T3.MaxDropDownItems = 2;
            this.UV_T3.Name = "UV_T3";
            this.UV_T3.Size = new System.Drawing.Size(42, 20);
            this.UV_T3.TabIndex = 0;
            this.UV_T3.SelectedIndexChanged += new System.EventHandler(this.UV_T3_SelectedIndexChanged);
            // 
            // CB_HOST
            // 
            this.CB_HOST.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_HOST.FormattingEnabled = true;
            this.CB_HOST.Items.AddRange(new object[] {
            "0",
            "1"});
            this.CB_HOST.Location = new System.Drawing.Point(416, 207);
            this.CB_HOST.MaxDropDownItems = 2;
            this.CB_HOST.Name = "CB_HOST";
            this.CB_HOST.Size = new System.Drawing.Size(42, 20);
            this.CB_HOST.TabIndex = 0;
            this.CB_HOST.SelectedIndexChanged += new System.EventHandler(this.CB_HOST_SelectedIndexChanged);
            // 
            // OV_T3
            // 
            this.OV_T3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OV_T3.FormattingEnabled = true;
            this.OV_T3.Items.AddRange(new object[] {
            "0",
            "1"});
            this.OV_T3.Location = new System.Drawing.Point(361, 399);
            this.OV_T3.MaxDropDownItems = 2;
            this.OV_T3.Name = "OV_T3";
            this.OV_T3.Size = new System.Drawing.Size(42, 20);
            this.OV_T3.TabIndex = 0;
            this.OV_T3.SelectedIndexChanged += new System.EventHandler(this.OV_T3_SelectedIndexChanged);
            // 
            // CB10
            // 
            this.CB10.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB10.FormattingEnabled = true;
            this.CB10.Items.AddRange(new object[] {
            "0",
            "1"});
            this.CB10.Location = new System.Drawing.Point(361, 131);
            this.CB10.MaxDropDownItems = 2;
            this.CB10.Name = "CB10";
            this.CB10.Size = new System.Drawing.Size(42, 20);
            this.CB10.TabIndex = 0;
            this.CB10.SelectedIndexChanged += new System.EventHandler(this.CB10_SelectedIndexChanged);
            // 
            // RSVD7
            // 
            this.RSVD7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RSVD7.FormattingEnabled = true;
            this.RSVD7.Items.AddRange(new object[] {
            "0",
            "1"});
            this.RSVD7.Location = new System.Drawing.Point(361, 361);
            this.RSVD7.MaxDropDownItems = 2;
            this.RSVD7.Name = "RSVD7";
            this.RSVD7.Size = new System.Drawing.Size(42, 20);
            this.RSVD7.TabIndex = 0;
            this.RSVD7.SelectedIndexChanged += new System.EventHandler(this.RSVD7_SelectedIndexChanged);
            // 
            // OV
            // 
            this.OV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OV.FormattingEnabled = true;
            this.OV.Items.AddRange(new object[] {
            "0",
            "1"});
            this.OV.Location = new System.Drawing.Point(416, 57);
            this.OV.MaxDropDownItems = 2;
            this.OV.Name = "OV";
            this.OV.Size = new System.Drawing.Size(42, 20);
            this.OV.TabIndex = 0;
            this.OV.SelectedIndexChanged += new System.EventHandler(this.OV_SelectedIndexChanged);
            // 
            // OCD_T3
            // 
            this.OCD_T3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OCD_T3.FormattingEnabled = true;
            this.OCD_T3.Items.AddRange(new object[] {
            "0",
            "1"});
            this.OCD_T3.Location = new System.Drawing.Point(361, 323);
            this.OCD_T3.MaxDropDownItems = 2;
            this.OCD_T3.Name = "OCD_T3";
            this.OCD_T3.Size = new System.Drawing.Size(42, 20);
            this.OCD_T3.TabIndex = 0;
            this.OCD_T3.SelectedIndexChanged += new System.EventHandler(this.OCD_T3_SelectedIndexChanged);
            // 
            // SCD_D0
            // 
            this.SCD_D0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SCD_D0.FormattingEnabled = true;
            this.SCD_D0.Items.AddRange(new object[] {
            "0",
            "1"});
            this.SCD_D0.Location = new System.Drawing.Point(361, 284);
            this.SCD_D0.MaxDropDownItems = 2;
            this.SCD_D0.Name = "SCD_D0";
            this.SCD_D0.Size = new System.Drawing.Size(42, 20);
            this.SCD_D0.TabIndex = 0;
            this.SCD_D0.SelectedIndexChanged += new System.EventHandler(this.SCD_D0_SelectedIndexChanged);
            // 
            // CB17
            // 
            this.CB17.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB17.FormattingEnabled = true;
            this.CB17.Items.AddRange(new object[] {
            "0",
            "1"});
            this.CB17.Location = new System.Drawing.Point(301, 167);
            this.CB17.MaxDropDownItems = 2;
            this.CB17.Name = "CB17";
            this.CB17.Size = new System.Drawing.Size(42, 20);
            this.CB17.TabIndex = 0;
            this.CB17.SelectedIndexChanged += new System.EventHandler(this.CB17_SelectedIndexChanged);
            // 
            // RSVD2
            // 
            this.RSVD2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RSVD2.FormattingEnabled = true;
            this.RSVD2.Items.AddRange(new object[] {
            "0",
            "1"});
            this.RSVD2.Location = new System.Drawing.Point(361, 245);
            this.RSVD2.MaxDropDownItems = 2;
            this.RSVD2.Name = "RSVD2";
            this.RSVD2.Size = new System.Drawing.Size(42, 20);
            this.RSVD2.TabIndex = 0;
            this.RSVD2.SelectedIndexChanged += new System.EventHandler(this.RSVD2_SelectedIndexChanged);
            // 
            // CB4
            // 
            this.CB4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB4.FormattingEnabled = true;
            this.CB4.Items.AddRange(new object[] {
            "0",
            "1"});
            this.CB4.Location = new System.Drawing.Point(361, 93);
            this.CB4.MaxDropDownItems = 2;
            this.CB4.Name = "CB4";
            this.CB4.Size = new System.Drawing.Size(42, 20);
            this.CB4.TabIndex = 0;
            this.CB4.SelectedIndexChanged += new System.EventHandler(this.CB4_SelectedIndexChanged);
            // 
            // UV_T4
            // 
            this.UV_T4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UV_T4.FormattingEnabled = true;
            this.UV_T4.Items.AddRange(new object[] {
            "0",
            "1"});
            this.UV_T4.Location = new System.Drawing.Point(301, 438);
            this.UV_T4.MaxDropDownItems = 2;
            this.UV_T4.Name = "UV_T4";
            this.UV_T4.Size = new System.Drawing.Size(42, 20);
            this.UV_T4.TabIndex = 0;
            this.UV_T4.SelectedIndexChanged += new System.EventHandler(this.UV_T4_SelectedIndexChanged);
            // 
            // TEMP_S
            // 
            this.TEMP_S.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TEMP_S.FormattingEnabled = true;
            this.TEMP_S.Items.AddRange(new object[] {
            "0",
            "1"});
            this.TEMP_S.Location = new System.Drawing.Point(361, 207);
            this.TEMP_S.MaxDropDownItems = 2;
            this.TEMP_S.Name = "TEMP_S";
            this.TEMP_S.Size = new System.Drawing.Size(42, 20);
            this.TEMP_S.TabIndex = 0;
            this.TEMP_S.SelectedIndexChanged += new System.EventHandler(this.TEMP_S_SelectedIndexChanged);
            // 
            // OV_T4
            // 
            this.OV_T4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OV_T4.FormattingEnabled = true;
            this.OV_T4.Items.AddRange(new object[] {
            "0",
            "1"});
            this.OV_T4.Location = new System.Drawing.Point(301, 398);
            this.OV_T4.MaxDropDownItems = 2;
            this.OV_T4.Name = "OV_T4";
            this.OV_T4.Size = new System.Drawing.Size(42, 20);
            this.OV_T4.TabIndex = 0;
            this.OV_T4.SelectedIndexChanged += new System.EventHandler(this.OV_T4_SelectedIndexChanged);
            // 
            // CB11
            // 
            this.CB11.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB11.FormattingEnabled = true;
            this.CB11.Items.AddRange(new object[] {
            "0",
            "1"});
            this.CB11.Location = new System.Drawing.Point(301, 130);
            this.CB11.MaxDropDownItems = 2;
            this.CB11.Name = "CB11";
            this.CB11.Size = new System.Drawing.Size(42, 20);
            this.CB11.TabIndex = 0;
            this.CB11.SelectedIndexChanged += new System.EventHandler(this.CB11_SelectedIndexChanged);
            // 
            // OV_D0
            // 
            this.OV_D0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OV_D0.FormattingEnabled = true;
            this.OV_D0.Items.AddRange(new object[] {
            "0",
            "1"});
            this.OV_D0.Location = new System.Drawing.Point(301, 360);
            this.OV_D0.MaxDropDownItems = 2;
            this.OV_D0.Name = "OV_D0";
            this.OV_D0.Size = new System.Drawing.Size(42, 20);
            this.OV_D0.TabIndex = 0;
            this.OV_D0.SelectedIndexChanged += new System.EventHandler(this.OV_D0_SelectedIndexChanged);
            // 
            // UV
            // 
            this.UV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UV.FormattingEnabled = true;
            this.UV.Items.AddRange(new object[] {
            "0",
            "1"});
            this.UV.Location = new System.Drawing.Point(361, 57);
            this.UV.MaxDropDownItems = 2;
            this.UV.Name = "UV";
            this.UV.Size = new System.Drawing.Size(42, 20);
            this.UV.TabIndex = 0;
            this.UV.SelectedIndexChanged += new System.EventHandler(this.UV_SelectedIndexChanged);
            // 
            // OCD_D0
            // 
            this.OCD_D0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OCD_D0.FormattingEnabled = true;
            this.OCD_D0.Items.AddRange(new object[] {
            "0",
            "1"});
            this.OCD_D0.Location = new System.Drawing.Point(301, 322);
            this.OCD_D0.MaxDropDownItems = 2;
            this.OCD_D0.Name = "OCD_D0";
            this.OCD_D0.Size = new System.Drawing.Size(42, 20);
            this.OCD_D0.TabIndex = 0;
            this.OCD_D0.SelectedIndexChanged += new System.EventHandler(this.OCD_D0_SelectedIndexChanged);
            // 
            // SCD_D1
            // 
            this.SCD_D1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SCD_D1.FormattingEnabled = true;
            this.SCD_D1.Items.AddRange(new object[] {
            "0",
            "1"});
            this.SCD_D1.Location = new System.Drawing.Point(301, 283);
            this.SCD_D1.MaxDropDownItems = 2;
            this.SCD_D1.Name = "SCD_D1";
            this.SCD_D1.Size = new System.Drawing.Size(42, 20);
            this.SCD_D1.TabIndex = 0;
            this.SCD_D1.SelectedIndexChanged += new System.EventHandler(this.SCD_D1_SelectedIndexChanged);
            // 
            // CB18
            // 
            this.CB18.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB18.FormattingEnabled = true;
            this.CB18.Items.AddRange(new object[] {
            "0",
            "1"});
            this.CB18.Location = new System.Drawing.Point(240, 167);
            this.CB18.MaxDropDownItems = 2;
            this.CB18.Name = "CB18";
            this.CB18.Size = new System.Drawing.Size(42, 20);
            this.CB18.TabIndex = 0;
            this.CB18.SelectedIndexChanged += new System.EventHandler(this.CB18_SelectedIndexChanged);
            // 
            // UV_T5
            // 
            this.UV_T5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UV_T5.FormattingEnabled = true;
            this.UV_T5.Items.AddRange(new object[] {
            "0",
            "1"});
            this.UV_T5.Location = new System.Drawing.Point(240, 438);
            this.UV_T5.MaxDropDownItems = 2;
            this.UV_T5.Name = "UV_T5";
            this.UV_T5.Size = new System.Drawing.Size(42, 20);
            this.UV_T5.TabIndex = 0;
            this.UV_T5.SelectedIndexChanged += new System.EventHandler(this.UV_T5_SelectedIndexChanged);
            // 
            // RSVD1
            // 
            this.RSVD1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RSVD1.FormattingEnabled = true;
            this.RSVD1.Items.AddRange(new object[] {
            "0",
            "1"});
            this.RSVD1.Location = new System.Drawing.Point(301, 244);
            this.RSVD1.MaxDropDownItems = 2;
            this.RSVD1.Name = "RSVD1";
            this.RSVD1.Size = new System.Drawing.Size(42, 20);
            this.RSVD1.TabIndex = 0;
            this.RSVD1.SelectedIndexChanged += new System.EventHandler(this.RSVD1_SelectedIndexChanged);
            // 
            // OV_T5
            // 
            this.OV_T5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OV_T5.FormattingEnabled = true;
            this.OV_T5.Items.AddRange(new object[] {
            "0",
            "1"});
            this.OV_T5.Location = new System.Drawing.Point(240, 398);
            this.OV_T5.MaxDropDownItems = 2;
            this.OV_T5.Name = "OV_T5";
            this.OV_T5.Size = new System.Drawing.Size(42, 20);
            this.OV_T5.TabIndex = 0;
            this.OV_T5.SelectedIndexChanged += new System.EventHandler(this.OV_T5_SelectedIndexChanged);
            // 
            // CB5
            // 
            this.CB5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB5.FormattingEnabled = true;
            this.CB5.Items.AddRange(new object[] {
            "0",
            "1"});
            this.CB5.Location = new System.Drawing.Point(301, 92);
            this.CB5.MaxDropDownItems = 2;
            this.CB5.Name = "CB5";
            this.CB5.Size = new System.Drawing.Size(42, 20);
            this.CB5.TabIndex = 0;
            this.CB5.SelectedIndexChanged += new System.EventHandler(this.CB5_SelectedIndexChanged);
            // 
            // OV_D1
            // 
            this.OV_D1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OV_D1.FormattingEnabled = true;
            this.OV_D1.Items.AddRange(new object[] {
            "0",
            "1"});
            this.OV_D1.Location = new System.Drawing.Point(240, 360);
            this.OV_D1.MaxDropDownItems = 2;
            this.OV_D1.Name = "OV_D1";
            this.OV_D1.Size = new System.Drawing.Size(42, 20);
            this.OV_D1.TabIndex = 0;
            this.OV_D1.SelectedIndexChanged += new System.EventHandler(this.OV_D1_SelectedIndexChanged);
            // 
            // ADC_EN2
            // 
            this.ADC_EN2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ADC_EN2.FormattingEnabled = true;
            this.ADC_EN2.Items.AddRange(new object[] {
            "0",
            "1"});
            this.ADC_EN2.Location = new System.Drawing.Point(301, 206);
            this.ADC_EN2.MaxDropDownItems = 2;
            this.ADC_EN2.Name = "ADC_EN2";
            this.ADC_EN2.Size = new System.Drawing.Size(42, 20);
            this.ADC_EN2.TabIndex = 0;
            this.ADC_EN2.SelectedIndexChanged += new System.EventHandler(this.ADC_EN2_SelectedIndexChanged);
            // 
            // OCD_D1
            // 
            this.OCD_D1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OCD_D1.FormattingEnabled = true;
            this.OCD_D1.Items.AddRange(new object[] {
            "0",
            "1"});
            this.OCD_D1.Location = new System.Drawing.Point(240, 322);
            this.OCD_D1.MaxDropDownItems = 2;
            this.OCD_D1.Name = "OCD_D1";
            this.OCD_D1.Size = new System.Drawing.Size(42, 20);
            this.OCD_D1.TabIndex = 0;
            this.OCD_D1.SelectedIndexChanged += new System.EventHandler(this.OCD_D1_SelectedIndexChanged);
            // 
            // RSVD5
            // 
            this.RSVD5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RSVD5.FormattingEnabled = true;
            this.RSVD5.Items.AddRange(new object[] {
            "0",
            "1"});
            this.RSVD5.Location = new System.Drawing.Point(240, 283);
            this.RSVD5.MaxDropDownItems = 2;
            this.RSVD5.Name = "RSVD5";
            this.RSVD5.Size = new System.Drawing.Size(42, 20);
            this.RSVD5.TabIndex = 0;
            this.RSVD5.SelectedIndexChanged += new System.EventHandler(this.RSVD5_SelectedIndexChanged);
            // 
            // UV_T6
            // 
            this.UV_T6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UV_T6.FormattingEnabled = true;
            this.UV_T6.Items.AddRange(new object[] {
            "0",
            "1"});
            this.UV_T6.Location = new System.Drawing.Point(178, 439);
            this.UV_T6.MaxDropDownItems = 2;
            this.UV_T6.Name = "UV_T6";
            this.UV_T6.Size = new System.Drawing.Size(42, 20);
            this.UV_T6.TabIndex = 0;
            this.UV_T6.SelectedIndexChanged += new System.EventHandler(this.UV_T6_SelectedIndexChanged);
            // 
            // CB12
            // 
            this.CB12.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB12.FormattingEnabled = true;
            this.CB12.Items.AddRange(new object[] {
            "0",
            "1"});
            this.CB12.Location = new System.Drawing.Point(240, 130);
            this.CB12.MaxDropDownItems = 2;
            this.CB12.Name = "CB12";
            this.CB12.Size = new System.Drawing.Size(42, 20);
            this.CB12.TabIndex = 0;
            this.CB12.SelectedIndexChanged += new System.EventHandler(this.CB12_SelectedIndexChanged);
            // 
            // OV_T6
            // 
            this.OV_T6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OV_T6.FormattingEnabled = true;
            this.OV_T6.Items.AddRange(new object[] {
            "0",
            "1"});
            this.OV_T6.Location = new System.Drawing.Point(178, 399);
            this.OV_T6.MaxDropDownItems = 2;
            this.OV_T6.Name = "OV_T6";
            this.OV_T6.Size = new System.Drawing.Size(42, 20);
            this.OV_T6.TabIndex = 0;
            this.OV_T6.SelectedIndexChanged += new System.EventHandler(this.OV_T6_SelectedIndexChanged);
            // 
            // CC_ONE
            // 
            this.CC_ONE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CC_ONE.FormattingEnabled = true;
            this.CC_ONE.Items.AddRange(new object[] {
            "0",
            "1"});
            this.CC_ONE.Location = new System.Drawing.Point(240, 244);
            this.CC_ONE.MaxDropDownItems = 2;
            this.CC_ONE.Name = "CC_ONE";
            this.CC_ONE.Size = new System.Drawing.Size(42, 20);
            this.CC_ONE.TabIndex = 0;
            this.CC_ONE.SelectedIndexChanged += new System.EventHandler(this.CC_ONE_SelectedIndexChanged);
            // 
            // UV_D0
            // 
            this.UV_D0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UV_D0.FormattingEnabled = true;
            this.UV_D0.Items.AddRange(new object[] {
            "0",
            "1"});
            this.UV_D0.Location = new System.Drawing.Point(178, 361);
            this.UV_D0.MaxDropDownItems = 2;
            this.UV_D0.Name = "UV_D0";
            this.UV_D0.Size = new System.Drawing.Size(42, 20);
            this.UV_D0.TabIndex = 0;
            this.UV_D0.SelectedIndexChanged += new System.EventHandler(this.UV_D0_SelectedIndexChanged);
            // 
            // OVRDAL
            // 
            this.OVRDAL.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OVRDAL.FormattingEnabled = true;
            this.OVRDAL.Items.AddRange(new object[] {
            "0",
            "1"});
            this.OVRDAL.Location = new System.Drawing.Point(301, 56);
            this.OVRDAL.MaxDropDownItems = 2;
            this.OVRDAL.Name = "OVRDAL";
            this.OVRDAL.Size = new System.Drawing.Size(42, 20);
            this.OVRDAL.TabIndex = 0;
            this.OVRDAL.SelectedIndexChanged += new System.EventHandler(this.OVRDAL_SelectedIndexChanged);
            // 
            // OCD_D2
            // 
            this.OCD_D2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OCD_D2.FormattingEnabled = true;
            this.OCD_D2.Items.AddRange(new object[] {
            "0",
            "1"});
            this.OCD_D2.Location = new System.Drawing.Point(178, 323);
            this.OCD_D2.MaxDropDownItems = 2;
            this.OCD_D2.Name = "OCD_D2";
            this.OCD_D2.Size = new System.Drawing.Size(42, 20);
            this.OCD_D2.TabIndex = 0;
            this.OCD_D2.SelectedIndexChanged += new System.EventHandler(this.OCD_D2_SelectedIndexChanged);
            // 
            // RSVD4
            // 
            this.RSVD4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RSVD4.FormattingEnabled = true;
            this.RSVD4.Items.AddRange(new object[] {
            "0",
            "1"});
            this.RSVD4.Location = new System.Drawing.Point(178, 284);
            this.RSVD4.MaxDropDownItems = 2;
            this.RSVD4.Name = "RSVD4";
            this.RSVD4.Size = new System.Drawing.Size(42, 20);
            this.RSVD4.TabIndex = 0;
            this.RSVD4.SelectedIndexChanged += new System.EventHandler(this.RSVD4_SelectedIndexChanged);
            // 
            // UV_T7
            // 
            this.UV_T7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UV_T7.FormattingEnabled = true;
            this.UV_T7.Items.AddRange(new object[] {
            "0",
            "1"});
            this.UV_T7.Location = new System.Drawing.Point(117, 439);
            this.UV_T7.MaxDropDownItems = 2;
            this.UV_T7.Name = "UV_T7";
            this.UV_T7.Size = new System.Drawing.Size(42, 20);
            this.UV_T7.TabIndex = 0;
            this.UV_T7.SelectedIndexChanged += new System.EventHandler(this.UV_T7_SelectedIndexChanged);
            // 
            // CB_ST0
            // 
            this.CB_ST0.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_ST0.FormattingEnabled = true;
            this.CB_ST0.Items.AddRange(new object[] {
            "0",
            "1"});
            this.CB_ST0.Location = new System.Drawing.Point(240, 206);
            this.CB_ST0.MaxDropDownItems = 2;
            this.CB_ST0.Name = "CB_ST0";
            this.CB_ST0.Size = new System.Drawing.Size(42, 20);
            this.CB_ST0.TabIndex = 0;
            this.CB_ST0.SelectedIndexChanged += new System.EventHandler(this.CB_ST0_SelectedIndexChanged);
            // 
            // OV_T7
            // 
            this.OV_T7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.OV_T7.FormattingEnabled = true;
            this.OV_T7.Items.AddRange(new object[] {
            "0",
            "1"});
            this.OV_T7.Location = new System.Drawing.Point(117, 399);
            this.OV_T7.MaxDropDownItems = 2;
            this.OV_T7.Name = "OV_T7";
            this.OV_T7.Size = new System.Drawing.Size(42, 20);
            this.OV_T7.TabIndex = 0;
            this.OV_T7.SelectedIndexChanged += new System.EventHandler(this.OV_T7_SelectedIndexChanged);
            // 
            // CC_EN2
            // 
            this.CC_EN2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CC_EN2.FormattingEnabled = true;
            this.CC_EN2.Items.AddRange(new object[] {
            "0",
            "1"});
            this.CC_EN2.Location = new System.Drawing.Point(178, 245);
            this.CC_EN2.MaxDropDownItems = 2;
            this.CC_EN2.Name = "CC_EN2";
            this.CC_EN2.Size = new System.Drawing.Size(42, 20);
            this.CC_EN2.TabIndex = 0;
            this.CC_EN2.SelectedIndexChanged += new System.EventHandler(this.CC_EN2_SelectedIndexChanged);
            // 
            // UV_D1
            // 
            this.UV_D1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UV_D1.FormattingEnabled = true;
            this.UV_D1.Items.AddRange(new object[] {
            "0",
            "1"});
            this.UV_D1.Location = new System.Drawing.Point(117, 361);
            this.UV_D1.MaxDropDownItems = 2;
            this.UV_D1.Name = "UV_D1";
            this.UV_D1.Size = new System.Drawing.Size(42, 20);
            this.UV_D1.TabIndex = 0;
            this.UV_D1.SelectedIndexChanged += new System.EventHandler(this.UV_D1_SelectedIndexChanged);
            // 
            // CB6
            // 
            this.CB6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB6.FormattingEnabled = true;
            this.CB6.Items.AddRange(new object[] {
            "0",
            "1"});
            this.CB6.Location = new System.Drawing.Point(240, 92);
            this.CB6.MaxDropDownItems = 2;
            this.CB6.Name = "CB6";
            this.CB6.Size = new System.Drawing.Size(42, 20);
            this.CB6.TabIndex = 0;
            this.CB6.SelectedIndexChanged += new System.EventHandler(this.CB6_SelectedIndexChanged);
            // 
            // RSVD6
            // 
            this.RSVD6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RSVD6.FormattingEnabled = true;
            this.RSVD6.Items.AddRange(new object[] {
            "0",
            "1"});
            this.RSVD6.Location = new System.Drawing.Point(117, 323);
            this.RSVD6.MaxDropDownItems = 2;
            this.RSVD6.Name = "RSVD6";
            this.RSVD6.Size = new System.Drawing.Size(42, 20);
            this.RSVD6.TabIndex = 0;
            this.RSVD6.SelectedIndexChanged += new System.EventHandler(this.RSVD6_SelectedIndexChanged);
            // 
            // RSNS
            // 
            this.RSNS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RSNS.FormattingEnabled = true;
            this.RSNS.Items.AddRange(new object[] {
            "0",
            "1"});
            this.RSNS.Location = new System.Drawing.Point(117, 284);
            this.RSNS.MaxDropDownItems = 2;
            this.RSNS.Name = "RSNS";
            this.RSNS.Size = new System.Drawing.Size(42, 20);
            this.RSNS.TabIndex = 0;
            this.RSNS.SelectedIndexChanged += new System.EventHandler(this.RSNS_SelectedIndexChanged);
            // 
            // CB_ST1
            // 
            this.CB_ST1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_ST1.FormattingEnabled = true;
            this.CB_ST1.Items.AddRange(new object[] {
            "0",
            "1"});
            this.CB_ST1.Location = new System.Drawing.Point(178, 207);
            this.CB_ST1.MaxDropDownItems = 2;
            this.CB_ST1.Name = "CB_ST1";
            this.CB_ST1.Size = new System.Drawing.Size(42, 20);
            this.CB_ST1.TabIndex = 0;
            this.CB_ST1.SelectedIndexChanged += new System.EventHandler(this.CB_ST1_SelectedIndexChanged);
            // 
            // DLY_DIS
            // 
            this.DLY_DIS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DLY_DIS.FormattingEnabled = true;
            this.DLY_DIS.Items.AddRange(new object[] {
            "0",
            "1"});
            this.DLY_DIS.Location = new System.Drawing.Point(117, 245);
            this.DLY_DIS.MaxDropDownItems = 2;
            this.DLY_DIS.Name = "DLY_DIS";
            this.DLY_DIS.Size = new System.Drawing.Size(42, 20);
            this.DLY_DIS.TabIndex = 0;
            this.DLY_DIS.SelectedIndexChanged += new System.EventHandler(this.DLY_DIS_SelectedIndexChanged);
            // 
            // DEVXD
            // 
            this.DEVXD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DEVXD.FormattingEnabled = true;
            this.DEVXD.Items.AddRange(new object[] {
            "0",
            "1"});
            this.DEVXD.Location = new System.Drawing.Point(240, 56);
            this.DEVXD.MaxDropDownItems = 2;
            this.DEVXD.Name = "DEVXD";
            this.DEVXD.Size = new System.Drawing.Size(42, 20);
            this.DEVXD.TabIndex = 0;
            this.DEVXD.SelectedIndexChanged += new System.EventHandler(this.DEVXD_SelectedIndexChanged);
            // 
            // LOAD_P
            // 
            this.LOAD_P.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.LOAD_P.FormattingEnabled = true;
            this.LOAD_P.Items.AddRange(new object[] {
            "0",
            "1"});
            this.LOAD_P.Location = new System.Drawing.Point(117, 207);
            this.LOAD_P.MaxDropDownItems = 2;
            this.LOAD_P.Name = "LOAD_P";
            this.LOAD_P.Size = new System.Drawing.Size(42, 20);
            this.LOAD_P.TabIndex = 0;
            this.LOAD_P.SelectedIndexChanged += new System.EventHandler(this.LOAD_P_SelectedIndexChanged);
            // 
            // INTCBDONE
            // 
            this.INTCBDONE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.INTCBDONE.FormattingEnabled = true;
            this.INTCBDONE.Items.AddRange(new object[] {
            "0",
            "1"});
            this.INTCBDONE.Location = new System.Drawing.Point(178, 57);
            this.INTCBDONE.MaxDropDownItems = 2;
            this.INTCBDONE.Name = "INTCBDONE";
            this.INTCBDONE.Size = new System.Drawing.Size(42, 20);
            this.INTCBDONE.TabIndex = 0;
            this.INTCBDONE.SelectedIndexChanged += new System.EventHandler(this.INTCBDONE_SelectedIndexChanged);
            // 
            // CCREADY_CHOS
            // 
            this.CCREADY_CHOS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CCREADY_CHOS.FormattingEnabled = true;
            this.CCREADY_CHOS.Items.AddRange(new object[] {
            "0",
            "1"});
            this.CCREADY_CHOS.Location = new System.Drawing.Point(117, 57);
            this.CCREADY_CHOS.MaxDropDownItems = 2;
            this.CCREADY_CHOS.Name = "CCREADY_CHOS";
            this.CCREADY_CHOS.Size = new System.Drawing.Size(42, 20);
            this.CCREADY_CHOS.TabIndex = 0;
            this.CCREADY_CHOS.SelectedIndexChanged += new System.EventHandler(this.CCREADY_CHOS_SelectedIndexChanged);
            // 
            // I2C_WR
            // 
            this.I2C_WR.Location = new System.Drawing.Point(618, 102);
            this.I2C_WR.Name = "I2C_WR";
            this.I2C_WR.Size = new System.Drawing.Size(88, 34);
            this.I2C_WR.TabIndex = 96;
            this.I2C_WR.Text = "I2C_WR";
            this.I2C_WR.UseVisualStyleBackColor = true;
            this.I2C_WR.Click += new System.EventHandler(this.I2C_WR_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.ADCOffset_Val);
            this.groupBox7.Controls.Add(this.label5);
            this.groupBox7.Controls.Add(this.ADCGain_Val);
            this.groupBox7.Controls.Add(this.label2);
            this.groupBox7.Location = new System.Drawing.Point(713, 5);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(125, 62);
            this.groupBox7.TabIndex = 98;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "ADC GAIN/OFFSET ";
            // 
            // ADCOffset_Val
            // 
            this.ADCOffset_Val.Location = new System.Drawing.Point(59, 36);
            this.ADCOffset_Val.MaxLength = 6;
            this.ADCOffset_Val.Name = "ADCOffset_Val";
            this.ADCOffset_Val.ReadOnly = true;
            this.ADCOffset_Val.Size = new System.Drawing.Size(40, 21);
            this.ADCOffset_Val.TabIndex = 88;
            this.ADCOffset_Val.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.Control;
            this.label5.Location = new System.Drawing.Point(13, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 12);
            this.label5.TabIndex = 89;
            this.label5.Text = "Offset:        mV";
            // 
            // ADCGain_Val
            // 
            this.ADCGain_Val.Location = new System.Drawing.Point(59, 14);
            this.ADCGain_Val.MaxLength = 6;
            this.ADCGain_Val.Name = "ADCGain_Val";
            this.ADCGain_Val.ReadOnly = true;
            this.ADCGain_Val.Size = new System.Drawing.Size(40, 21);
            this.ADCGain_Val.TabIndex = 88;
            this.ADCGain_Val.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(13, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 12);
            this.label2.TabIndex = 89;
            this.label2.Text = "Gain:          uV";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.label7);
            this.groupBox8.Controls.Add(this.donotSupportCRC);
            this.groupBox8.Controls.Add(this.SupportCRC);
            this.groupBox8.Location = new System.Drawing.Point(515, 30);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(151, 64);
            this.groupBox8.TabIndex = 99;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "CRC";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(72, 41);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "Support";
            // 
            // donotSupportCRC
            // 
            this.donotSupportCRC.AutoSize = true;
            this.donotSupportCRC.Checked = true;
            this.donotSupportCRC.Location = new System.Drawing.Point(13, 40);
            this.donotSupportCRC.Name = "donotSupportCRC";
            this.donotSupportCRC.Size = new System.Drawing.Size(59, 16);
            this.donotSupportCRC.TabIndex = 1;
            this.donotSupportCRC.TabStop = true;
            this.donotSupportCRC.Text = "do not";
            this.donotSupportCRC.UseVisualStyleBackColor = true;
            this.donotSupportCRC.CheckedChanged += new System.EventHandler(this.donotSupportCRC_CheckedChanged);
            // 
            // SupportCRC
            // 
            this.SupportCRC.AutoSize = true;
            this.SupportCRC.Location = new System.Drawing.Point(13, 17);
            this.SupportCRC.Name = "SupportCRC";
            this.SupportCRC.Size = new System.Drawing.Size(65, 16);
            this.SupportCRC.TabIndex = 0;
            this.SupportCRC.Text = "Support";
            this.SupportCRC.UseVisualStyleBackColor = true;
            this.SupportCRC.CheckedChanged += new System.EventHandler(this.SupportCRC_CheckedChanged);
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4",
            "COM5",
            "COM6",
            "COM7",
            "COM8",
            "COM9",
            "COM10"});
            this.comboBox1.Location = new System.Drawing.Point(452, 664);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(67, 20);
            this.comboBox1.TabIndex = 101;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(462, 649);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 102;
            this.label9.Text = "通道选择";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(550, 664);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(78, 16);
            this.checkBox2.TabIndex = 103;
            this.checkBox2.Text = "测电压ADC";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(550, 686);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(78, 16);
            this.checkBox3.TabIndex = 104;
            this.checkBox3.Text = "测电流ADC";
            this.checkBox3.UseVisualStyleBackColor = true;
            this.checkBox3.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(664, 664);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 105;
            this.button1.Text = "读数据";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(550, 720);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(116, 21);
            this.textBox2.TabIndex = 106;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(758, 662);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 107;
            this.button2.Text = "关闭通道";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.Cell18_TEST);
            this.groupBox9.Controls.Add(this.Cell17_TEST);
            this.groupBox9.Controls.Add(this.Cell16_TEST);
            this.groupBox9.Controls.Add(this.Cell15_TEST);
            this.groupBox9.Controls.Add(this.Cell14_TEST);
            this.groupBox9.Controls.Add(this.Cell13_TEST);
            this.groupBox9.Controls.Add(this.Cell12_TEST);
            this.groupBox9.Controls.Add(this.Cell11_TEST);
            this.groupBox9.Controls.Add(this.Cell10_TEST);
            this.groupBox9.Controls.Add(this.Cell9_TEST);
            this.groupBox9.Controls.Add(this.Cell8_TEST);
            this.groupBox9.Controls.Add(this.Cell7_TEST);
            this.groupBox9.Controls.Add(this.Current_TEST);
            this.groupBox9.Controls.Add(this.Cell6_TEST);
            this.groupBox9.Controls.Add(this.Cell5_TEST);
            this.groupBox9.Controls.Add(this.Cell4_TEST);
            this.groupBox9.Controls.Add(this.Cell3_TEST);
            this.groupBox9.Controls.Add(this.Cell2_TEST);
            this.groupBox9.Controls.Add(this.Cell1_TEST);
            this.groupBox9.Location = new System.Drawing.Point(21, 649);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(410, 122);
            this.groupBox9.TabIndex = 108;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "选择电压通道或者电流";
            // 
            // Cell18_TEST
            // 
            this.Cell18_TEST.AutoSize = true;
            this.Cell18_TEST.Location = new System.Drawing.Point(292, 86);
            this.Cell18_TEST.Name = "Cell18_TEST";
            this.Cell18_TEST.Size = new System.Drawing.Size(59, 16);
            this.Cell18_TEST.TabIndex = 18;
            this.Cell18_TEST.TabStop = true;
            this.Cell18_TEST.Text = "Cell18";
            this.Cell18_TEST.UseVisualStyleBackColor = true;
            // 
            // Cell17_TEST
            // 
            this.Cell17_TEST.AutoSize = true;
            this.Cell17_TEST.Location = new System.Drawing.Point(293, 64);
            this.Cell17_TEST.Name = "Cell17_TEST";
            this.Cell17_TEST.Size = new System.Drawing.Size(59, 16);
            this.Cell17_TEST.TabIndex = 17;
            this.Cell17_TEST.TabStop = true;
            this.Cell17_TEST.Text = "Cell17";
            this.Cell17_TEST.UseVisualStyleBackColor = true;
            // 
            // Cell16_TEST
            // 
            this.Cell16_TEST.AutoSize = true;
            this.Cell16_TEST.Location = new System.Drawing.Point(293, 42);
            this.Cell16_TEST.Name = "Cell16_TEST";
            this.Cell16_TEST.Size = new System.Drawing.Size(59, 16);
            this.Cell16_TEST.TabIndex = 16;
            this.Cell16_TEST.TabStop = true;
            this.Cell16_TEST.Text = "Cell16";
            this.Cell16_TEST.UseVisualStyleBackColor = true;
            // 
            // Cell15_TEST
            // 
            this.Cell15_TEST.AutoSize = true;
            this.Cell15_TEST.Location = new System.Drawing.Point(293, 20);
            this.Cell15_TEST.Name = "Cell15_TEST";
            this.Cell15_TEST.Size = new System.Drawing.Size(59, 16);
            this.Cell15_TEST.TabIndex = 15;
            this.Cell15_TEST.TabStop = true;
            this.Cell15_TEST.Text = "Cell15";
            this.Cell15_TEST.UseVisualStyleBackColor = true;
            // 
            // Cell14_TEST
            // 
            this.Cell14_TEST.AutoSize = true;
            this.Cell14_TEST.Location = new System.Drawing.Point(232, 87);
            this.Cell14_TEST.Name = "Cell14_TEST";
            this.Cell14_TEST.Size = new System.Drawing.Size(59, 16);
            this.Cell14_TEST.TabIndex = 14;
            this.Cell14_TEST.TabStop = true;
            this.Cell14_TEST.Text = "Cell14";
            this.Cell14_TEST.UseVisualStyleBackColor = true;
            this.Cell14_TEST.CheckedChanged += new System.EventHandler(this.radioButton10_CheckedChanged);
            // 
            // Cell13_TEST
            // 
            this.Cell13_TEST.AutoSize = true;
            this.Cell13_TEST.Location = new System.Drawing.Point(232, 65);
            this.Cell13_TEST.Name = "Cell13_TEST";
            this.Cell13_TEST.Size = new System.Drawing.Size(59, 16);
            this.Cell13_TEST.TabIndex = 13;
            this.Cell13_TEST.TabStop = true;
            this.Cell13_TEST.Text = "Cell13";
            this.Cell13_TEST.UseVisualStyleBackColor = true;
            // 
            // Cell12_TEST
            // 
            this.Cell12_TEST.AutoSize = true;
            this.Cell12_TEST.Location = new System.Drawing.Point(232, 43);
            this.Cell12_TEST.Name = "Cell12_TEST";
            this.Cell12_TEST.Size = new System.Drawing.Size(59, 16);
            this.Cell12_TEST.TabIndex = 12;
            this.Cell12_TEST.TabStop = true;
            this.Cell12_TEST.Text = "Cell12";
            this.Cell12_TEST.UseVisualStyleBackColor = true;
            // 
            // Cell11_TEST
            // 
            this.Cell11_TEST.AutoSize = true;
            this.Cell11_TEST.Location = new System.Drawing.Point(232, 21);
            this.Cell11_TEST.Name = "Cell11_TEST";
            this.Cell11_TEST.Size = new System.Drawing.Size(59, 16);
            this.Cell11_TEST.TabIndex = 11;
            this.Cell11_TEST.TabStop = true;
            this.Cell11_TEST.Text = "Cell11";
            this.Cell11_TEST.UseVisualStyleBackColor = true;
            // 
            // Cell10_TEST
            // 
            this.Cell10_TEST.AutoSize = true;
            this.Cell10_TEST.Location = new System.Drawing.Point(170, 87);
            this.Cell10_TEST.Name = "Cell10_TEST";
            this.Cell10_TEST.Size = new System.Drawing.Size(59, 16);
            this.Cell10_TEST.TabIndex = 10;
            this.Cell10_TEST.TabStop = true;
            this.Cell10_TEST.Text = "Cell10";
            this.Cell10_TEST.UseVisualStyleBackColor = true;
            // 
            // Cell9_TEST
            // 
            this.Cell9_TEST.AutoSize = true;
            this.Cell9_TEST.Location = new System.Drawing.Point(170, 65);
            this.Cell9_TEST.Name = "Cell9_TEST";
            this.Cell9_TEST.Size = new System.Drawing.Size(53, 16);
            this.Cell9_TEST.TabIndex = 9;
            this.Cell9_TEST.TabStop = true;
            this.Cell9_TEST.Text = "Cell9";
            this.Cell9_TEST.UseVisualStyleBackColor = true;
            // 
            // Cell8_TEST
            // 
            this.Cell8_TEST.AutoSize = true;
            this.Cell8_TEST.Location = new System.Drawing.Point(170, 43);
            this.Cell8_TEST.Name = "Cell8_TEST";
            this.Cell8_TEST.Size = new System.Drawing.Size(53, 16);
            this.Cell8_TEST.TabIndex = 8;
            this.Cell8_TEST.TabStop = true;
            this.Cell8_TEST.Text = "Cell8";
            this.Cell8_TEST.UseVisualStyleBackColor = true;
            // 
            // Cell7_TEST
            // 
            this.Cell7_TEST.AutoSize = true;
            this.Cell7_TEST.Location = new System.Drawing.Point(170, 21);
            this.Cell7_TEST.Name = "Cell7_TEST";
            this.Cell7_TEST.Size = new System.Drawing.Size(53, 16);
            this.Cell7_TEST.TabIndex = 7;
            this.Cell7_TEST.TabStop = true;
            this.Cell7_TEST.Text = "Cell7";
            this.Cell7_TEST.UseVisualStyleBackColor = true;
            // 
            // Current_TEST
            // 
            this.Current_TEST.AutoSize = true;
            this.Current_TEST.Location = new System.Drawing.Point(97, 65);
            this.Current_TEST.Name = "Current_TEST";
            this.Current_TEST.Size = new System.Drawing.Size(65, 16);
            this.Current_TEST.TabIndex = 6;
            this.Current_TEST.TabStop = true;
            this.Current_TEST.Text = "Current";
            this.Current_TEST.UseVisualStyleBackColor = true;
            // 
            // Cell6_TEST
            // 
            this.Cell6_TEST.AutoSize = true;
            this.Cell6_TEST.Location = new System.Drawing.Point(97, 43);
            this.Cell6_TEST.Name = "Cell6_TEST";
            this.Cell6_TEST.Size = new System.Drawing.Size(53, 16);
            this.Cell6_TEST.TabIndex = 5;
            this.Cell6_TEST.TabStop = true;
            this.Cell6_TEST.Text = "Cell6";
            this.Cell6_TEST.UseVisualStyleBackColor = true;
            // 
            // Cell5_TEST
            // 
            this.Cell5_TEST.AutoSize = true;
            this.Cell5_TEST.Location = new System.Drawing.Point(97, 21);
            this.Cell5_TEST.Name = "Cell5_TEST";
            this.Cell5_TEST.Size = new System.Drawing.Size(53, 16);
            this.Cell5_TEST.TabIndex = 4;
            this.Cell5_TEST.TabStop = true;
            this.Cell5_TEST.Text = "Cell5";
            this.Cell5_TEST.UseVisualStyleBackColor = true;
            // 
            // Cell4_TEST
            // 
            this.Cell4_TEST.AutoSize = true;
            this.Cell4_TEST.Location = new System.Drawing.Point(18, 87);
            this.Cell4_TEST.Name = "Cell4_TEST";
            this.Cell4_TEST.Size = new System.Drawing.Size(53, 16);
            this.Cell4_TEST.TabIndex = 3;
            this.Cell4_TEST.TabStop = true;
            this.Cell4_TEST.Text = "Cell4";
            this.Cell4_TEST.UseVisualStyleBackColor = true;
            // 
            // Cell3_TEST
            // 
            this.Cell3_TEST.AutoSize = true;
            this.Cell3_TEST.Location = new System.Drawing.Point(18, 65);
            this.Cell3_TEST.Name = "Cell3_TEST";
            this.Cell3_TEST.Size = new System.Drawing.Size(53, 16);
            this.Cell3_TEST.TabIndex = 2;
            this.Cell3_TEST.TabStop = true;
            this.Cell3_TEST.Text = "Cell3";
            this.Cell3_TEST.UseVisualStyleBackColor = true;
            // 
            // Cell2_TEST
            // 
            this.Cell2_TEST.AutoSize = true;
            this.Cell2_TEST.Location = new System.Drawing.Point(18, 43);
            this.Cell2_TEST.Name = "Cell2_TEST";
            this.Cell2_TEST.Size = new System.Drawing.Size(53, 16);
            this.Cell2_TEST.TabIndex = 1;
            this.Cell2_TEST.TabStop = true;
            this.Cell2_TEST.Text = "Cell2";
            this.Cell2_TEST.UseVisualStyleBackColor = true;
            // 
            // Cell1_TEST
            // 
            this.Cell1_TEST.AutoSize = true;
            this.Cell1_TEST.Location = new System.Drawing.Point(18, 21);
            this.Cell1_TEST.Name = "Cell1_TEST";
            this.Cell1_TEST.Size = new System.Drawing.Size(53, 16);
            this.Cell1_TEST.TabIndex = 0;
            this.Cell1_TEST.TabStop = true;
            this.Cell1_TEST.Text = "Cell1";
            this.Cell1_TEST.UseVisualStyleBackColor = true;
            this.Cell1_TEST.CheckedChanged += new System.EventHandler(this.Cell1_TEST_CheckedChanged);
            // 
            // frmCH341_I2C
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 783);
            this.Controls.Add(this.groupBox9);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.groupBox8);
            this.Controls.Add(this.I2C_WR);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lblIsCall);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cmbI2CPort);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cmbBitRat);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("新宋体", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmCH341_I2C";
            this.Text = "BMS03 Evaluation Software";
            this.Load += new System.EventHandler(this.frmMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSlvAddr)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudSlvAddr;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.Button btnScanAddr;
        private System.Windows.Forms.ComboBox cmbBitRat;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbI2CPort;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblIsCall;
        private System.Windows.Forms.Button Update_Display_button;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Cell_Name;
        private System.Windows.Forms.DataGridViewTextBoxColumn ADC_rd_vol;
        private System.Windows.Forms.Timer timer_auto_Update;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton CC_disabled;
        private System.Windows.Forms.RadioButton CC_one_shot;
        private System.Windows.Forms.RadioButton CC_continous;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton ADC_DIS;
        private System.Windows.Forms.RadioButton ADC_EN;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton EXT_TEMP;
        private System.Windows.Forms.RadioButton INT_TEMP;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.ComboBox CCREADY_CHOS;
        private System.Windows.Forms.Label Bit_Name;
        private System.Windows.Forms.Label System_Status;
        private System.Windows.Forms.TextBox SYS_STAT_SHOW;
        private System.Windows.Forms.ComboBox INTCBDONE;
        private System.Windows.Forms.ComboBox DEVXD;
        private System.Windows.Forms.ComboBox OVRDAL;
        private System.Windows.Forms.ComboBox UV;
        private System.Windows.Forms.ComboBox OCD;
        private System.Windows.Forms.ComboBox SCD;
        private System.Windows.Forms.ComboBox OV;
        private System.Windows.Forms.Button ClearFault;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox CELLBAL1_SHOW;
        private System.Windows.Forms.ComboBox CB1;
        private System.Windows.Forms.ComboBox CB2;
        private System.Windows.Forms.ComboBox CB3;
        private System.Windows.Forms.ComboBox CB4;
        private System.Windows.Forms.ComboBox CB5;
        private System.Windows.Forms.ComboBox CB6;
        private System.Windows.Forms.TextBox CELLBAL2_SHOW;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox CB7;
        private System.Windows.Forms.ComboBox CB8;
        private System.Windows.Forms.ComboBox CB9;
        private System.Windows.Forms.ComboBox CB10;
        private System.Windows.Forms.ComboBox CB11;
        private System.Windows.Forms.ComboBox CB12;
        private System.Windows.Forms.TextBox CELLBAL3_SHOW;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox CB13;
        private System.Windows.Forms.ComboBox CB14;
        private System.Windows.Forms.ComboBox CB15;
        private System.Windows.Forms.ComboBox CB16;
        private System.Windows.Forms.ComboBox CB17;
        private System.Windows.Forms.ComboBox CB18;
        private System.Windows.Forms.TextBox SYS_CTRL1_SHOW;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox SHUTB;
        private System.Windows.Forms.ComboBox SHUTA;
        private System.Windows.Forms.ComboBox CB_HOST;
        private System.Windows.Forms.ComboBox TEMP_S;
        private System.Windows.Forms.ComboBox ADC_EN2;
        private System.Windows.Forms.ComboBox CB_ST0;
        private System.Windows.Forms.ComboBox CB_ST1;
        private System.Windows.Forms.ComboBox LOAD_P;
        private System.Windows.Forms.TextBox SYS_CTRL2_SHOW;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox CHG_ON;
        private System.Windows.Forms.ComboBox DSG_ON;
        private System.Windows.Forms.ComboBox RSVD3;
        private System.Windows.Forms.ComboBox RSVD2;
        private System.Windows.Forms.ComboBox RSVD1;
        private System.Windows.Forms.ComboBox CC_ONE;
        private System.Windows.Forms.ComboBox CC_EN2;
        private System.Windows.Forms.ComboBox DLY_DIS;
        private System.Windows.Forms.TextBox PROTECT1_SHOW;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox SCD_T0;
        private System.Windows.Forms.ComboBox SCD_T1;
        private System.Windows.Forms.ComboBox SCD_T2;
        private System.Windows.Forms.ComboBox SCD_D0;
        private System.Windows.Forms.ComboBox SCD_D1;
        private System.Windows.Forms.ComboBox RSVD5;
        private System.Windows.Forms.ComboBox RSVD4;
        private System.Windows.Forms.ComboBox RSNS;
        private System.Windows.Forms.TextBox PROTECT2_SHOW;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox OCD_T0;
        private System.Windows.Forms.ComboBox OCD_T1;
        private System.Windows.Forms.ComboBox OCD_T2;
        private System.Windows.Forms.ComboBox OCD_T3;
        private System.Windows.Forms.ComboBox OCD_D0;
        private System.Windows.Forms.ComboBox OCD_D1;
        private System.Windows.Forms.ComboBox OCD_D2;
        private System.Windows.Forms.ComboBox RSVD6;
        private System.Windows.Forms.TextBox PROTECT3_SHOW;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.ComboBox RSVD10;
        private System.Windows.Forms.ComboBox RSVD9;
        private System.Windows.Forms.ComboBox RSVD8;
        private System.Windows.Forms.ComboBox RSVD7;
        private System.Windows.Forms.ComboBox OV_D0;
        private System.Windows.Forms.ComboBox OV_D1;
        private System.Windows.Forms.ComboBox UV_D0;
        private System.Windows.Forms.ComboBox UV_D1;
        private System.Windows.Forms.TextBox OV_TRIP_SHOW;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox OV_T0;
        private System.Windows.Forms.ComboBox OV_T1;
        private System.Windows.Forms.ComboBox OV_T2;
        private System.Windows.Forms.ComboBox OV_T3;
        private System.Windows.Forms.ComboBox OV_T4;
        private System.Windows.Forms.ComboBox OV_T5;
        private System.Windows.Forms.ComboBox OV_T6;
        private System.Windows.Forms.ComboBox OV_T7;
        private System.Windows.Forms.TextBox UV_TRIP_SHOW;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox UV_T0;
        private System.Windows.Forms.ComboBox UV_T1;
        private System.Windows.Forms.ComboBox UV_T2;
        private System.Windows.Forms.ComboBox UV_T3;
        private System.Windows.Forms.ComboBox UV_T4;
        private System.Windows.Forms.ComboBox UV_T5;
        private System.Windows.Forms.ComboBox UV_T6;
        private System.Windows.Forms.ComboBox UV_T7;
        private System.Windows.Forms.Button I2C_WR;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.TextBox ADCOffset_Val;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox ADCGain_Val;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Reset;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.RadioButton donotSupportCRC;
        private System.Windows.Forms.RadioButton SupportCRC;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.RadioButton Current_TEST;
        private System.Windows.Forms.RadioButton Cell6_TEST;
        private System.Windows.Forms.RadioButton Cell5_TEST;
        private System.Windows.Forms.RadioButton Cell4_TEST;
        private System.Windows.Forms.RadioButton Cell3_TEST;
        private System.Windows.Forms.RadioButton Cell2_TEST;
        private System.Windows.Forms.RadioButton Cell1_TEST;
        private System.Windows.Forms.RadioButton Cell17_TEST;
        private System.Windows.Forms.RadioButton Cell16_TEST;
        private System.Windows.Forms.RadioButton Cell15_TEST;
        private System.Windows.Forms.RadioButton Cell14_TEST;
        private System.Windows.Forms.RadioButton Cell13_TEST;
        private System.Windows.Forms.RadioButton Cell12_TEST;
        private System.Windows.Forms.RadioButton Cell11_TEST;
        private System.Windows.Forms.RadioButton Cell10_TEST;
        private System.Windows.Forms.RadioButton Cell9_TEST;
        private System.Windows.Forms.RadioButton Cell8_TEST;
        private System.Windows.Forms.RadioButton Cell7_TEST;
        private System.Windows.Forms.RadioButton Cell18_TEST;
    }
}
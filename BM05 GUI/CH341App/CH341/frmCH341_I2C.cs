using System; //using关键字用于在程序中包含System命名空间 。C#文件的后缀是.cs
using System.Collections.Generic; 
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Windows.Forms;
using ClsLib;

namespace CH341App   //namespace声明，CH341APP命名空间中可以包含一系列的类。该命名空间内只有一个类。类包含了多个方法。
{
    public partial class frmCH341_I2C : Form  //新建立的windows form的类会自动生成部分类。编译时会把局部类合成完成类。可以在一个命名空间里写多次public partial class A，最后合成完整的A。
                                              //处理大型项目时，使一个类分布于多个独立文件中可以让多位程序员同时对该类进行处理。
                                              //使用自动生成的源时，无需重新创建源文件便可将代码添加到类中。Visual Studio 在创建 Windows 窗体、Web 服务包装代码等时都使用此方法。无需编辑 Visual Studio 所创建的文件，便可创建使用这些类的代码。
    {
        // 辅助介面
        // frmCH341_GPIO fGPIOCtl;
        I2C_W_R f1;    //指的是I2C_WR整个窗口       
//        Timer tmrPcs = new Timer();
        public string isNull = null;
        // 定义局部变量
        public static uint slvAdr;
        public double ADCgain = 6.4/65535*1000000;
        public uint ADCoffset =  0;
        public bool isCall = false;  
        public bool IsCall { set { isCall = value; } }  //???
        private string comboload = null;
        public static bool CRC_enable = false;

        // 主程序
        public frmCH341_I2C()
        {
            // 初始化控制设备
            if (!I2CLib.IsOpened)
            {
                if (!I2CLib.OpenDevice(I2CLib.eDvcType.CH341, 0))
                {
                    ErrHdl.ErrorHandle(ErrHdl.ErrEnum.ExitApp, null);
                    Environment.Exit(0);
                }
                if (!I2CLib.SetBitRate(I2CLib.eBitRate._20K)) { return; }
                if (!I2CLib.SetTranMode(I2CLib.eTranMode.StepMsg)) { return; }
            }

            // 初始化辅助介面

            // fGPIOCtl = new frmCH341_GPIO();
            f1 = new I2C_W_R();                     //？？ 
            { 
            InitializeComponent();
            }
            /* new关键字
            用于创建对象和调用构造函数；
            用于隐藏基类成员的继承成员；
            用于在泛型声明中约束可能用作类型参数的参数的类型
            */

            // 初始化应用程序
            string revInf = "Rev 2.0";  //字符串创建和赋值
            string dataInf = System.IO.File.GetLastWriteTime(this.GetType().Assembly.Location).ToString("yyyyMMdd");//yyyy/MM/dd HH:mm:ss         生成的是程序修改日期
            //this指的是当前对象；ToString是强制类型转换
            lblInfo.Text = revInf + " " + dataInf;                        // +指的是字符串连接    

            cmbI2CPort.SelectedIndex = (int)I2CLib.NumPort;
            //cmbBitRat.SelectedIndex = (int)I2CLib.BitRate;
            cmbBitRat.SelectedIndex = 2;
            //            cmbTranMod.SelectedIndex = (int)I2CLib.TranMode;

            PROTECTION1_SHOW_BM05.Text = "0000 0000";   //自动属性设置   string TextBox.Text { get; set; }
            FLAG_SHOW_BM05.Text = "0000 0000";
            PROTECTION2_SHOW_BM05.Text = "0000 0000";
            PROTECTION3_SHOW_BM05.Text = "0000 0000";
            PROTECTION4_SHOW_BM05.Text= "0000 0000";
            PROTECTION5_SHOW_BM05.Text = "0000 0000";
            STATUS_SHOW_BM05.Text = "0000 0000";
            STATUS_MSK_SHOW_BM05.Text = "0000 0111";
            CELLBAL_SHOW_BM05.Text = "0001 0000";
            CTRL1_SHOW_BM05.Text = "0000 1011";
            CTRL2_SHOW_BM05.Text = "1000 0000";
            FET_CTRL_SHOW_BM05.Text = "0110 0000";
            MUX_STATUS_SHOW_BM05.Text = "0000 0000";
            WAKE_COMP_SHOW_BM05.Text = "0000 0000";


            nudSlvAddr.Value = 0x00;
//            nudReadLen.Value = 2;
//            txtWriteData.Text = "00";
//            txtReadData.Text = "00";

            InitTipMsg();

//            tmrPcs.Interval = 1000;
//            tmrPcs.Tick += tmrPcs_Tick;
//            tmrPcs.Start();
        }
        // 初始化显示
        private void frmMain_Load(object sender, EventArgs e)     // sender和e在下面的函数中怎么体现出来????一定要这么写吗       一打开窗体就执行这个程序？？？？
        {

           
  
            RSNS_BM05.Text = "0"; SCDDx2_BM05.Text = "0";

            OCD_BM05.Text = "0"; SCC_BM05.Text = "0"; SCD_BM05.Text = "0"; SDSG_ALERT_BM05.Text = "0"; 

            OCD_T0_BM05.Text = "0"; OCD_T1_BM05.Text = "0"; OCD_T2_BM05.Text = "0"; OCD_T3_BM05.Text = "0"; OCD_D0_BM05.Text = "0"; OCD_D1_BM05.Text = "0"; OCD_D2_BM05.Text = "0"; OCD_D3_BM05.Text = "0";

            SCC_T0_BM05.Text = "0"; SCC_T1_BM05.Text = "0"; SCC_T2_BM05.Text = "0"; SCC_D0_BM05.Text = "0"; SCC_D1_BM05.Text = "0"; SCC_D2_BM05.Text = "0"; SCC_D3_BM05.Text = "0";

            SCD1_T0_BM05.Text = "0"; SCD1_T1_BM05.Text = "0"; SCD1_T2_BM05.Text = "0"; SCD1_D0_BM05.Text = "0"; SCD1_D1_BM05.Text = "0"; SCD1_D2_BM05.Text = "0"; SCD1_D3_BM05.Text = "0";

            SCD2_T0_BM05.Text = "0"; SCD2_T1_BM05.Text = "0"; SCD2_T2_BM05.Text = "0"; SCD2_D0_BM05.Text = "0"; SCD2_D1_BM05.Text = "0"; SCD2_D2_BM05.Text = "0"; SCD2_D3_BM05.Text = "0";

            CHG_BM05.Text = "0";DSG_BM05.Text = "0";BMC_BM05.Text = "0"; LD_SHORT_BM05.Text = "0";

            OCD_DRI_MSK_BM05.Text = "1"; SCC_DRI_MSK_BM05.Text = "1"; SCD_DRI_MSK_BM05.Text = "1"; OCD_MSK_BM05.Text = "0"; SCC_MSK_BM05.Text = "0"; SCD_MSK_BM05.Text = "0"; SDSG_MSK_BM05.Text = "0";
                                   
            CB1_BM05.Text = "0"; CB2_BM05.Text = "0"; CB3_BM05.Text = "0"; CB4_BM05.Text = "0"; CB_EN_BM05.Text = "1";

            WAKE_HIGH_BM05.Text = "1"; VDD_EN_BM05.Text = "1"; LD_SRC_BM05.Text = "0"; POWER_MCU_BM05.Text = "1";

            SHUTB_BM05.Text = "0"; SHUTA_BM05.Text = "0"; GO_SLP_BM05.Text = "0"; DRST_BM05.Text = "1";

            CHG_ON_BM05.Text = "0"; DSG_ON_BM05.Text = "0"; BMC_ON_BM05.Text = "0"; PCHG_ON_BM05.Text = "0"; SDSG_ON_BM05.Text = "0"; FET_EN_BM05.Text = "1"; CP_EN_BM05.Text = "1"; VBUS_ON_BM05.Text = "0";

            MUX0_BM05.Text = "0"; MUX1_BM05.Text = "0"; MUX2_BM05.Text = "0"; MUX3_BM05.Text = "0";


            WAKE_CMP0_BM05.Text = "0"; WAKE_CMP1_BM05.Text = "0";


            comboload = "1"; 

        }
        // 退出应用或隐藏介面
        /*
        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isCall)
            { // 当前应用作为主程序被打开

                fGPIOCtl.Hide();
                this.Hide(); // 基于设置的包含关系，在隐藏当前介面之前先隐藏子介面
                e.Cancel = true; // 屏蔽接下来的Close功能调用。
            }
            else
            { // 当前应用作为子介面被调用

                fGPIOCtl.Close();
                I2CLib.CloseDevice(); // 关闭I2C硬件资源
            }
        }*/
        // I2C速率
        private void cmbBitRat_SelectedIndexChanged(object sender, EventArgs e)
        {
            I2CLib.SetBitRate((I2CLib.eBitRate)cmbBitRat.SelectedIndex);                          // ?????
        }
        // 驱动器序号
        private void cmbI2CPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            uint n = (uint)cmbI2CPort.SelectedIndex;
            if ((I2CLib.DvcTyp != I2CLib.eDvcType.CH341) | (n != I2CLib.NumPort))
            {
                I2CLib.CloseDevice();

                if (!I2CLib.OpenDevice(I2CLib.eDvcType.CH341, n))
                {
                    ErrHdl.ErrorHandle(ErrHdl.ErrEnum.ExitApp, null);
                    Environment.Exit(0);
                }
            }
        }
        // I2C通信传输模式，有中断和连续两种。前者方便调试，后者方便显波器抓图
/*        private void cmbTranMod_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = cmbTranMod.SelectedIndex;
//            chkNoStp.Visible = i > 0;
            if (!I2CLib.SetTranMode((I2CLib.eTranMode)i)) { return; }
        }
*/        // 扫描地址
        private void btnScanAddr_Click(object sender, EventArgs e)                
        {
            string str = ""; List<byte> ackList;                                      //定义str，和ackList的一个list，ackList里面装ACK信号的位置
            try
            {
                btnScanAddr.Enabled = false;
                I2CLib.ScanAddrEvent = UpdtScanAddr;                                //将UpdtScanAddr方法定义给ScanAddrEvent，好让每次ScanAddrEvent触发的时候都能调用UpdtScanAddr函数
                                                        // 赋值，右边是一个方法，左边是一个变量？？？？？？

//                txtReadData.Text = "";
//                txtReadData.Update(); // 
                if (!I2CLib.ScanSlvAddr(out ackList)) { return; }
                if (ackList.Count <= 0) { /*txtReadData.Text = "Slave driver not found.";*/ return; }
/*                for (int i = 0; i < ackList.Count; i++)
                {
                    str = str + "<0x" + string.Format("{0:X2}", ackList[i]) + "> "; // 
                }
*/                nudSlvAddr.Value = ackList[0];                                      // 第一个有效从机地址赋值给当前操作地址,nudslvaddr指的是那个框
//                txtReadData.Text = "List of ack slave address(7bit): " + str;
            }
            finally
            {
                //      I2CLib.ScanAddrEvent -= UpdtScanAddr;                                //这一行并非必须，可删
                btnScanAddr.Enabled = true;
            }
        }
                                                         //为什么要用try，finally的结构？？？？？？

        // I2C写数据
        private void txtWrite_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)0x08) //删除键                                        //e.Handled是属性
            {
                e.Handled = false;
            }
            else if (e.KeyChar == (char)0x16) //Ctrl+V
            {
                e.Handled = false;
            }
            else if (e.KeyChar == (char)0x18) //Ctrl+X
            {
                e.Handled = false;
            }
            else if (e.KeyChar == (char)0x03) //Ctrl+C
            {
                e.Handled = false;
            }
            else if (e.KeyChar == (char)0x20) //空格
            {
                e.Handled = false;
            }
            else if ("0123456789ABCDEF".IndexOf(e.KeyChar) >= 0)
            {
                e.Handled = false;
            }
            else if ("abcdef".IndexOf(e.KeyChar) >= 0)
            {
                e.Handled = false;
                e.KeyChar = (char)(e.KeyChar - 32);//转为大写
            }
            else
            {
                e.Handled = true;
            }
        }
        // 写字符更改，需要格式化字符以方便示别
/*        private void txtWriteData_TextChanged(object sender, EventArgs e)
        {
            string str = txtWriteData.Text.Replace(" ", "");
            txtWrLen.Text = Convert.ToString((double)str.Length / 2);
        }*/
        // 从地址变更
        private void nudSlvAddr_ValueChanged(object sender, EventArgs e)
        {
            slvAdr = (uint)nudSlvAddr.Value;
        }

        //载入文件数据到写字符串
/*        private void btnLodWrDat_Click(object sender, EventArgs e)
        {
            string[] strArr = new string[1];
            if (!CfgFilLib.ReCfgArr((uint)strArr.Length, ref strArr)) { return; }
            txtWriteData.Text = strArr[0];
        }
        //存储读字符串数据到文件
        private void btnSavReDat_Click(object sender, EventArgs e)
        {
            string[] strArr = new string[1];
            strArr[0] = txtReadData.Text;
            if (!CfgFilLib.WrCfgArr((uint)strArr.Length, strArr)) { return; }
        }
        // I2C写
        private void btnI2CWrite_Click(object sender, EventArgs e)
        {
            byte[] wArr;
            try
            {
                btnI2CWrite.Enabled = false;

                string str = txtWriteData.Text;
                if (!FmtStr.FmtStr2Hex(ref str, out wArr)) { return; }
                txtWriteData.Text = str;
                uint wLn = (uint)wArr.Length;
                bool NoStop = chkNoStp.Checked;
                if (!I2CLib.WriteI2C(slvAdr, wLn, wArr, NoStop)) { return; }
            }
            finally
            {
                btnI2CWrite.Enabled = true;
            }
        }
        // I2C读
        private void btnI2CRead_Click(object sender, EventArgs e)
        {
            try
            {
                btnI2CRead.Enabled = false;

                txtReadData.Text = "";
                txtReadData.Update();
                uint rLn = (uint)nudReadLen.Value;
                if (rLn == 0)
                {
                    MessageBox.Show("读数据长度为0错误，请检查后重试。");
                    return;
                }
                byte[] rDat = new byte[rLn];
                if (!I2CLib.ReadI2C(slvAdr, rLn, ref rDat)) { return; }
                string str = "";
                for (int i = 0; i < rLn; i++)
                {
                    str = str + string.Format("{0:X2}", rDat[i]);
                    if (i > 0 & i % 2 != 0) str = str + " ";
                }
                txtReadData.Text = str;
            }
            finally
            {
                btnI2CRead.Enabled = true;
            }
        }
        // I2C流读,先写后读。
        private void btnI2CStm_Click(object sender, EventArgs e)
        {
            byte[] wDat;
            try
            {
                btnI2CStm.Enabled = false;

                txtReadData.Text = "";
                txtReadData.Update();
                uint rLn = (uint)nudReadLen.Value;
                string str = txtWriteData.Text;

                if (!FmtStr.FmtStr2Hex(ref str, out wDat)) { return; }
                txtWriteData.Text = str;
                if (rLn == 0) { return; }
                uint wLn = (uint)wDat.Length;
                byte[] rBuff = new byte[rLn];
                if (!I2CLib.StreamI2C(slvAdr, wLn, wDat, rLn, ref rBuff)) { return; }
                str = "";
                for (int i = 0; i < rLn; i++)
                {
                    str = str + string.Format("{0:X2}", rBuff[i]);
                    if (i > 0 & i % 2 != 0) str = str + " ";
                }
                txtReadData.Text = str;
            }
            finally
            {
                btnI2CStm.Enabled = true;
            }
        }


         






        private void btnIOCtl_Click(object sender, EventArgs e)
        {
  //          fGPIOCtl.Show();
        }
*/
        private void InitTipMsg()
        {
            // 提示信息
            ToolTip tipMsg = new ToolTip();
            tipMsg.AutoPopDelay = 10000;
            tipMsg.InitialDelay = 1000;
            tipMsg.ReshowDelay = 500;
            tipMsg.ShowAlways = true;
            tipMsg.SetToolTip(this.cmbI2CPort,             //this代表当前窗体的实例
                "USB-I2C Port选项：" +
                "电脑中同时插有多个CH341（USB-I2C）通信端口，通过此选项你可以在多个\r\n" +
                "端口中进行切换。驱动器序号和插入次序没关系，只会对应插入的USB端口序列。");
            
            tipMsg.SetToolTip(this.cmbBitRat,
                "Bit Rate选项: " +
                "I2C通信速率，默认为100K。通信速率不是越慢越好，如果电路中有干扰，并\r\n" +
                "且接入了滤波电容，请选择较慢的通信速率。");
/*           tipMsg.SetToolTip(this.cmbTranMod,
                "Transmission Mode选项:\r\n" +
                "I2C通信传送模式有两个选择，默认为Step by Step Msg，即通信过程中任\r\n" +
                "何环节异常都会有提示，如写到第n个数据时没有ACK信号。\r\n" +
                "1.No Exception Tips，用示波器查看这种传送波形将会比较连续，对保存\r\n" +
                "波形比较完整易读，缺点就是发生通信故障没有提示，如从机没有ACK，但通\r\n" +
                "信照样成功完成。\r\n" +
                "2.Step by Step Msg，用示波器查看这种传送波形时会特别难于分析。原因\r\n" +
                "是在通信过程中，设备需要把当前状态不断传送给计算机。优点就是发生任何\r\n" +
                "通信异常时有提示，在响应提示后会立即中断当前通信。");
*/            tipMsg.SetToolTip(this.I2C_WR,
                "启动CH341的I2C读写工具\r\n" );
            tipMsg.SetToolTip(this.ClearFault,
                 "通过向SYS_STAT所有位写1，清除Faults\r\n");
            tipMsg.SetToolTip(this.Reset,
                 "SYS_STAT所有位写1，其他所有寄存器复位到初始值\r\n");
            /*            tipMsg.SetToolTip(this.btnI2CWrite,
                            "此功能为I2C写。\r\n");
                        tipMsg.SetToolTip(this.btnI2CRead,
                            "此功能为Current Address Read。\r\n" +
                            "多次点击此按钮实现连续读下一地址的数据，如果从设备支持连续读功能。");
                        tipMsg.SetToolTip(this.btnI2CStm,
                            "Random Read功能：\r\n" +
                            "Random Read也称为I2C Stream或Stream Read，它是把I2C写和I2C读两个操作\r\n" +
                            "整合在一起形成了Random Read这么一种通信方式，可以加快I2C读取寄存器数\r\n" +
                            "据的速度。注意，有些从设备只支持Random Read，相反，有些则只支持写和读\r\n" +
                            "分开操作，不异而同。下面是读寄存器实例。\r\n" +
                            "1.在写字节中输入1Byte数据，表示寄存器地址。\r\n" +
                            "2.设置读入字节数。\r\n" +
                            "3.点击I2C Stream，读取相关内容。");
            */
            tipMsg.SetToolTip(this.nudSlvAddr,
               "Slave Address输入控件：\r\n" +
               "也就是当前操作地址输入控件。7位，不包括读写位。你可以输入0x00到0x7F\r\n" +
               "中任一个数字作为I2C地址。");
            tipMsg.SetToolTip(this.btnScanAddr,
               "Slave Address Scan功能：\r\n" +
               "从地址0x00到0x7F总共128个地址，搜索到(从机有应答ACK信号)的地址列表将会\r\n" +
               "在读数据框中显示。");
        }
        // 扫描地址时实时更新当前地址,起显示的作用
        private void UpdtScanAddr(byte CurrAddr)
        {
            nudSlvAddr.Value = CurrAddr;
            nudSlvAddr.Update();
        }
/*        void tmrPcs_Tick(object sender, EventArgs e)
        {
            if (isCall)
            {
                lblIsCall.Text = "C";
            }
        }
        */
        private void label8_Click(object sender, EventArgs e)
        {

        }
        private void Update_Display_Click(object sender, EventArgs e)
        {
            try
            {
                Update_Display_button.Enabled = false;
                Update_table();
                if (!ErrHdl.scanfalse)
                {
                    ErrHdl.scanfalse = true;

                    return;
                }


            }
            finally
            {
                Update_Display_button.Enabled = true;                                  // try故障捕获
            }


        }
        private void Update_table()
        {
            uint rLn = 58; //rLn/2表示一共有多少寄存器
            byte[] rAll = new byte[rLn+1];  //创建一个具有59个字节长度的数组rALL
            if (!ADC_all(rLn, rAll)) {return; }    //从00起始地址开始读，读59个byte 

            int[] StrOut = new int[rLn];

           for(int i=0;i<24;i++)
                int.TryParse(Convert.ToString(rAll[i], 2),out StrOut[i]);   
           
           //注意 BM05的register map 从01开始，前面并不是连续的
            PROTECTION1_SHOW_BM05.Text = string.Format("{0:0000 0000}", StrOut[1]);//数据类型转换后赋值给SHOW BOX
            FLAG_SHOW_BM05.Text = string.Format("{0:0000 0000}", StrOut[9]);
            PROTECTION2_SHOW_BM05.Text = string.Format("{0:0000 0000}", StrOut[12]);
            PROTECTION3_SHOW_BM05.Text = string.Format("{0:0000 0000}", StrOut[13]);
            PROTECTION4_SHOW_BM05.Text = string.Format("{0:0000 0000}", StrOut[14]);
            PROTECTION5_SHOW_BM05.Text = string.Format("{0:0000 0000}", StrOut[15]);
            STATUS_SHOW_BM05.Text = string.Format("{0:0000 0000}", StrOut[16]);
            STATUS_MSK_SHOW_BM05.Text = string.Format("{0:0000 0000}", StrOut[17]);
            CELLBAL_SHOW_BM05.Text = string.Format("{0:0000 0000}", StrOut[18]);
            CTRL1_SHOW_BM05.Text = string.Format("{0:0000 0000}", StrOut[19]);
            CTRL2_SHOW_BM05.Text = string.Format("{0:0000 0000}", StrOut[20]);

            //增加新的寄存器【Leo Zhang】
            FET_CTRL_SHOW_BM05.Text = string.Format("{0:0000 0000}", StrOut[21]);
            MUX_STATUS_SHOW_BM05.Text = string.Format("{0:0000 0000}", StrOut[22]);
            WAKE_COMP_SHOW_BM05.Text = string.Format("{0:0000 0000}", StrOut[23]);




            //通过I2C连续读了59个byte回来，起始地址是00

            //0x01寄存器

            SCDDx2_BM05.SelectedIndex = (rAll[1] >> 1) % 2;
            RSNS_BM05.SelectedIndex = rAll[1] % 2;

            //0x09寄存器

            SDSG_ALERT_BM05.SelectedIndex = (rAll[9] >> 3) % 2;
            SCD_BM05.SelectedIndex = (rAll[9] >> 2) % 2;
            SCC_BM05.SelectedIndex = (rAll[9] >> 1) % 2;
            OCD_BM05.SelectedIndex = rAll[9] % 2;

            //0x0C寄存器
            OCD_D3_BM05.SelectedIndex = rAll[12] >> 7;
            OCD_D2_BM05.SelectedIndex = (rAll[12] >> 6) % 2;
            OCD_D1_BM05.SelectedIndex = (rAll[12] >> 5) % 2;
            OCD_D0_BM05.SelectedIndex = (rAll[12] >> 4) % 2;
            OCD_T3_BM05.SelectedIndex = (rAll[12] >> 3) % 2;
            OCD_T2_BM05.SelectedIndex = (rAll[12] >> 2) % 2;
            OCD_T1_BM05.SelectedIndex = (rAll[12] >> 1) % 2;
            OCD_T0_BM05.SelectedIndex = rAll[12] % 2;

            //0x0D寄存器
            SCC_D3_BM05.SelectedIndex = rAll[13] >> 7;
            SCC_D2_BM05.SelectedIndex = (rAll[13] >> 6) % 2;
            SCC_D1_BM05.SelectedIndex = (rAll[13] >> 5) % 2;
            SCC_D0_BM05.SelectedIndex = (rAll[13] >> 4) % 2;

            SCC_T2_BM05.SelectedIndex = (rAll[13] >> 2) % 2;
            SCC_T1_BM05.SelectedIndex = (rAll[13] >> 1) % 2;
            SCC_T0_BM05.SelectedIndex = rAll[13] % 2;

            //0x0E寄存器
            SCD1_D3_BM05.SelectedIndex = rAll[14] >> 7;
            SCD1_D2_BM05.SelectedIndex = (rAll[14] >> 6) % 2;
            SCD1_D1_BM05.SelectedIndex = (rAll[14] >> 5) % 2;
            SCD1_D0_BM05.SelectedIndex = (rAll[14] >> 4) % 2;

            SCD1_T2_BM05.SelectedIndex = (rAll[14] >> 2) % 2;
            SCD1_T1_BM05.SelectedIndex = (rAll[14] >> 1) % 2;
            SCD1_T0_BM05.SelectedIndex = rAll[14] % 2;

            //0x0F寄存器
            SCD2_D3_BM05.SelectedIndex = rAll[15] >> 7;
            SCD2_D2_BM05.SelectedIndex = (rAll[15] >> 6) % 2;
            SCD2_D1_BM05.SelectedIndex = (rAll[15] >> 5) % 2;
            SCD2_D0_BM05.SelectedIndex = (rAll[15] >> 4) % 2;

            SCD2_T2_BM05.SelectedIndex = (rAll[15] >> 2) % 2;
            SCD2_T1_BM05.SelectedIndex = (rAll[15] >> 1) % 2;
            SCD2_T0_BM05.SelectedIndex = rAll[15] % 2;

            //0x10寄存器




            LD_SHORT_BM05.SelectedIndex = (rAll[16] >> 3) % 2;
            BMC_BM05.SelectedIndex = (rAll[16] >> 2) % 2;
            DSG_BM05.SelectedIndex = (rAll[16] >> 1) % 2;
            CHG_BM05.SelectedIndex = rAll[16] % 2;

            //0x11寄存器
            SDSG_MSK_BM05.SelectedIndex = rAll[17] >> 7;
            SCD_MSK_BM05.SelectedIndex = (rAll[17] >> 6) % 2;
            SCC_MSK_BM05.SelectedIndex = (rAll[17] >> 5) % 2;
            OCD_MSK_BM05.SelectedIndex = (rAll[17] >> 4) % 2;

            SCD_DRI_MSK_BM05.SelectedIndex = (rAll[17] >> 2) % 2;
            SCC_DRI_MSK_BM05.SelectedIndex = (rAll[17] >> 1) % 2;
            OCD_DRI_MSK_BM05.SelectedIndex = rAll[17] % 2;

            //0x12寄存器



            CB_EN_BM05.SelectedIndex = (rAll[18] >> 4) % 2;
            CB4_BM05.SelectedIndex = (rAll[18] >> 3) % 2;
            CB3_BM05.SelectedIndex = (rAll[18] >> 2) % 2;
            CB2_BM05.SelectedIndex = (rAll[18] >> 1) % 2;
            CB1_BM05.SelectedIndex = rAll[18] % 2;

            //0x13寄存器




            POWER_MCU_BM05.SelectedIndex = (rAll[19] >> 3) % 2;
            LD_SRC_BM05.SelectedIndex = (rAll[19] >> 2) % 2;
            VDD_EN_BM05.SelectedIndex = (rAll[19] >> 1) % 2;
            WAKE_HIGH_BM05.SelectedIndex = rAll[19] % 2;

            //0x14寄存器
            DRST_BM05.SelectedIndex = rAll[20] >> 7;
            GO_SLP_BM05.SelectedIndex = (rAll[20] >> 6) % 2;




            SHUTA_BM05.SelectedIndex = (rAll[20] >> 1) % 2;
            SHUTB_BM05.SelectedIndex = rAll[20] % 2;

            //0x15寄存器
            VBUS_ON_BM05.SelectedIndex = rAll[21] >> 7;
            CP_EN_BM05.SelectedIndex = (rAll[21] >> 6) % 2;
            FET_EN_BM05.SelectedIndex = (rAll[21] >> 5) % 2;
            SDSG_ON_BM05.SelectedIndex = (rAll[21] >> 4) % 2;
            PCHG_ON_BM05.SelectedIndex = (rAll[21] >> 3) % 2;
            BMC_ON_BM05.SelectedIndex = (rAll[21] >> 2) % 2;
            DSG_ON_BM05.SelectedIndex = (rAll[21] >> 1) % 2;
            CHG_ON_BM05.SelectedIndex = rAll[21] % 2;

            //0x16寄存器




            MUX3_BM05.SelectedIndex = (rAll[22] >> 3) % 2;
            MUX2_BM05.SelectedIndex = (rAll[22] >> 2) % 2;
            MUX1_BM05.SelectedIndex = (rAll[22] >> 1) % 2;
            MUX0_BM05.SelectedIndex = rAll[22] % 2;

            //0x17寄存器






            WAKE_CMP1_BM05.SelectedIndex = (rAll[23] >> 1) % 2;
            WAKE_CMP0_BM05.SelectedIndex = rAll[23] % 2;





        }
/*       private void ADCgainconv()
        {
            byte rBuff;
            byte RegAddr = 0x50;  //ADC gain1地址0x50
            byte[] Buff = new byte[2];
            if (!I2CLib.ReadReg1Byt(slvAdr, RegAddr, out rBuff, CRC_enable)) { return; }
            rBuff /= 4;  //右移2位
            Buff[1] = rBuff;
            Buff[1] &= 0x03;
            RegAddr = 0x59;  //ADC gain2地址0x59
            if (!I2CLib.ReadReg1Byt(slvAdr, RegAddr, out rBuff, CRC_enable)) { return; }
            Buff[0] = rBuff;
            Buff[0] &= 0xE0;
            ADCgain = BitConverter.ToUInt16(Buff, 0);
            ADCgain = ADCgain >> 5;
            if (!(ADCgain <= 0x1F & ADCgain >= 0)) { return; }//加入一个报错信息
            ADCgain = ADCgain + 365;
            return;

        }
*/
        private bool ADC_all(uint rLn, byte[] rBuff)
        {

            byte CellRegAddr = 0x00;   //Cell1的高8位地址 0x0C            

            if (!I2CLib.ReadRegister3(slvAdr, CellRegAddr, rLn, ref rBuff, CRC_enable))  //什么含义？？？
            {
                return false;
            }
            return true;

        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!ErrHdl.scanfalse)
            {
                ErrHdl.scanfalse = true;
                timer_auto_Update.Stop();

                return;
            }
            Update_table();

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                timer_auto_Update.Enabled = true;
                timer_auto_Update.Start();
            }
            else
            {
                timer_auto_Update.Enabled = false;
                timer_auto_Update.Stop();
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int tLn = textBox1.TextLength;
            string str = textBox1.Text.Replace(" ", "");
            int time_of_timer;
            int.TryParse(str, out time_of_timer);
            if (tLn == 0)
                timer_auto_Update.Interval = 600;
            else
            {
                if (time_of_timer <= 300)
                {
                    timer_auto_Update.Interval = 300;
                }
                else
                    timer_auto_Update.Interval = time_of_timer;
            }
                

        }


        private void I2C_WR_Click(object sender, EventArgs e)
        {
            I2C_W_R f1 = new I2C_W_R();                   //创建一个新窗体
            //在主界面点击I2C_WR后出现了I2C读写窗口？？                    闪电符号 load  是打开窗体后执行的内容？
            f1.Show();      //打开窗体
            
        }


        private void RSNS_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)RSNS_BM05.SelectedIndex;
                byte RegAdr = 0x01, RegVal = 0x01;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    RSNS_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    RSNS_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void SCDDx2_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCDDx2_BM05.SelectedIndex;
                byte RegAdr = 0x01, RegVal = 0x02;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCDDx2_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCDDx2_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void OCD_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OCD_BM05.SelectedIndex;
                byte RegAdr = 0x09, RegVal = 0x01;
                if (!bit_chosen(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                /*                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                                    OCD_BM05.SelectedIndex = 1;
                                else if ((rBuff &= RegVal) == 0x00)
                                    OCD_BM05.SelectedIndex = 0;
                                else
                                    return;
                */
            }
        }

        private void SCC_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCC_BM05.SelectedIndex;
                byte RegAdr = 0x09, RegVal = 0x02;
                if (!bit_chosen(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                /*                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                                    SCC_BM05.SelectedIndex = 1;
                                else if ((rBuff &= RegVal) == 0x00)
                                    SCC_BM05.SelectedIndex = 0;
                                else
                                    return;
                */
            }
        }

        private void SCD_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCD_BM05.SelectedIndex;
                byte RegAdr = 0x09, RegVal = 0x04;
                if (!bit_chosen(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                /*                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                                    SCD_BM05.SelectedIndex = 1;
                                else if ((rBuff &= RegVal) == 0x00)
                                    SCD_BM05.SelectedIndex = 0;
                                else
                                    return;
                */
            }
        }

        private void SDSG_ALERT_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SDSG_ALERT_BM05.SelectedIndex;
                byte RegAdr = 0x09, RegVal = 0x08;
                if (!bit_chosen(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                /*                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                                    SDSG_ALERT_BM05.SelectedIndex = 1;
                                else if ((rBuff &= RegVal) == 0x00)
                                    SDSG_ALERT_BM05.SelectedIndex = 0;
                                else
                                    return;
                */
            }
        }

        private void OCD_T0_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OCD_T0_BM05.SelectedIndex;
                byte RegAdr = 0x0C, RegVal = 0x01;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OCD_T0_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OCD_T0_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void OCD_T1_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OCD_T1_BM05.SelectedIndex;
                byte RegAdr = 0x0C, RegVal = 0x02;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OCD_T1_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OCD_T1_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void OCD_T2_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OCD_T2_BM05.SelectedIndex;
                byte RegAdr = 0x0C, RegVal = 0x04;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OCD_T2_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OCD_T2_BM05.SelectedIndex = 0;
                else
                    return;
            }

        }

        private void OCD_T3_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OCD_T3_BM05.SelectedIndex;
                byte RegAdr = 0x0C, RegVal = 0x08;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OCD_T3_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OCD_T3_BM05.SelectedIndex = 0;
                else
                    return;
            }

        }

        private void OCD_D0_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OCD_D0_BM05.SelectedIndex;
                byte RegAdr = 0x0C, RegVal = 0x10;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OCD_D0_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OCD_D0_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void OCD_D1_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OCD_D1_BM05.SelectedIndex;
                byte RegAdr = 0x0C, RegVal = 0x20;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OCD_D1_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OCD_D1_BM05.SelectedIndex = 0;
                else
                    return;
            }

        }

        private void OCD_D2_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OCD_D2_BM05.SelectedIndex;
                byte RegAdr = 0x0C, RegVal = 0x40;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OCD_D2_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OCD_D2_BM05.SelectedIndex = 0;
                else
                    return;
            }

        }

        private void OCD_D3_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OCD_D3_BM05.SelectedIndex;
                byte RegAdr = 0x0C, RegVal = 0x80;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OCD_D3_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OCD_D3_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void SCC_T0_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCC_T0_BM05.SelectedIndex;
                byte RegAdr = 0x0D, RegVal = 0x01;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCC_T0_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCC_T0_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void SCC_T1_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCC_T1_BM05.SelectedIndex;
                byte RegAdr = 0x0D, RegVal = 0x02;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCC_T1_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCC_T1_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void SCC_T2_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCC_T2_BM05.SelectedIndex;
                byte RegAdr = 0x0D, RegVal = 0x04;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCC_T2_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCC_T2_BM05.SelectedIndex = 0;
                else
                    return;
            }

        }

        private void SCC_D0_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCC_D0_BM05.SelectedIndex;
                byte RegAdr = 0x0D, RegVal = 0x10;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCC_D0_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCC_D0_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void SCC_D1_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCC_D1_BM05.SelectedIndex;
                byte RegAdr = 0x0D, RegVal = 0x20;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCC_D1_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCC_D1_BM05.SelectedIndex = 0;
                else
                    return;
            }

        }

        private void SCC_D2_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCC_D2_BM05.SelectedIndex;
                byte RegAdr = 0x0D, RegVal = 0x40;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCC_D2_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCC_D2_BM05.SelectedIndex = 0;
                else
                    return;
            }

        }

        private void SCC_D3_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCC_D3_BM05.SelectedIndex;
                byte RegAdr = 0x0D, RegVal = 0x80;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCC_D3_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCC_D3_BM05.SelectedIndex = 0;
                else
                    return;
            }

        }

        private void SCD1_T0_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCD1_T0_BM05.SelectedIndex;
                byte RegAdr = 0x0E, RegVal = 0x01;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCD1_T0_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCD1_T0_BM05.SelectedIndex = 0;
                else
                    return;
            }

        }

        private void SCD1_T1_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCD1_T1_BM05.SelectedIndex;
                byte RegAdr = 0x0E, RegVal = 0x02;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCD1_T1_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCD1_T1_BM05.SelectedIndex = 0;
                else
                    return;
            }

        }

        private void SCD1_T2_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCD1_T2_BM05.SelectedIndex;
                byte RegAdr = 0x0E, RegVal = 0x04;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCD1_T2_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCD1_T2_BM05.SelectedIndex = 0;
                else
                    return;
            }

        }

        private void SCD1_D0_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCD1_D0_BM05.SelectedIndex;
                byte RegAdr = 0x0E, RegVal = 0x10;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCD1_D0_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCD1_D0_BM05.SelectedIndex = 0;
                else
                    return;
            }

        }

        private void SCD1_D1_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCD1_D1_BM05.SelectedIndex;
                byte RegAdr = 0x0E, RegVal = 0x20;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCD1_D1_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCD1_D1_BM05.SelectedIndex = 0;
                else
                    return;
            }

        }

        private void SCD1_D2_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCD1_D2_BM05.SelectedIndex;
                byte RegAdr = 0x0E, RegVal = 0x40;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCD1_D2_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCD1_D2_BM05.SelectedIndex = 0;
                else
                    return;
            }

        }

        private void SCD1_D3_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCD1_D3_BM05.SelectedIndex;
                byte RegAdr = 0x0E, RegVal = 0x80;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCD1_D3_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCD1_D3_BM05.SelectedIndex = 0;
                else
                    return;
            }

        }

        private void SCD2_T0_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCD2_T0_BM05.SelectedIndex;
                byte RegAdr = 0x0F, RegVal = 0x01;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCD2_T0_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCD2_T0_BM05.SelectedIndex = 0;
                else
                    return;
            }

        }

        private void SCD2_T1_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCD2_T1_BM05.SelectedIndex;
                byte RegAdr = 0x0F, RegVal = 0x02;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCD2_T1_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCD2_T1_BM05.SelectedIndex = 0;
                else
                    return;
            }

        }

        private void SCD2_T2_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCD2_T2_BM05.SelectedIndex;
                byte RegAdr = 0x0F, RegVal = 0x04;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCD2_T2_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCD2_T2_BM05.SelectedIndex = 0;
                else
                    return;
            }

        }

        private void SCD2_D0_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCD2_D0_BM05.SelectedIndex;
                byte RegAdr = 0x0F, RegVal = 0x10;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCD2_D0_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCD2_D0_BM05.SelectedIndex = 0;
                else
                    return;
            }

        }

        private void SCD2_D1_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCD2_D1_BM05.SelectedIndex;
                byte RegAdr = 0x0F, RegVal = 0x20;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCD2_D1_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCD2_D1_BM05.SelectedIndex = 0;
                else
                    return;
            }

        }

        private void SCD2_D2_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCD2_D2_BM05.SelectedIndex;
                byte RegAdr = 0x0F, RegVal = 0x40;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCD2_D2_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCD2_D2_BM05.SelectedIndex = 0;
                else
                    return;
            }

        }

        private void SCD2_D3_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCD2_D3_BM05.SelectedIndex;
                byte RegAdr = 0x0F, RegVal = 0x80;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCD2_D3_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCD2_D3_BM05.SelectedIndex = 0;
                else
                    return;
            }

        }

        private void CHG_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CHG_BM05.SelectedIndex;
                byte RegAdr = 0x10, RegVal = 0x01;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CHG_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CHG_BM05.SelectedIndex = 0;
                else
                    return;
            }

        }

        private void DSG_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)DSG_BM05.SelectedIndex;
                byte RegAdr = 0x10, RegVal = 0x02;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    DSG_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    DSG_BM05.SelectedIndex = 0;
                else
                    return;
            }

        }

        private void BMC_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)BMC_BM05.SelectedIndex;
                byte RegAdr = 0x10, RegVal = 0x04;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    BMC_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    BMC_BM05.SelectedIndex = 0;
                else
                    return;
            }

        }

        private void LD_SHORT_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)LD_SHORT_BM05.SelectedIndex;
                byte RegAdr = 0x10, RegVal = 0x08;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    LD_SHORT_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    LD_SHORT_BM05.SelectedIndex = 0;
                else
                    return;
            }

        }

        private void OCD_DRI_MSK_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OCD_DRI_MSK_BM05.SelectedIndex;
                byte RegAdr = 0x11, RegVal = 0x01;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OCD_DRI_MSK_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OCD_DRI_MSK_BM05.SelectedIndex = 0;
                else
                    return;
            }

        }

        private void SCC_DRI_MSK_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCC_DRI_MSK_BM05.SelectedIndex;
                byte RegAdr = 0x11, RegVal = 0x02;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCC_DRI_MSK_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCC_DRI_MSK_BM05.SelectedIndex = 0;
                else
                    return;
            }

        }

        private void SCD_DRI_MSK_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCD_DRI_MSK_BM05.SelectedIndex;
                byte RegAdr = 0x11, RegVal = 0x04;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCD_DRI_MSK_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCD_DRI_MSK_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void OCD_MSK_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OCD_MSK_BM05.SelectedIndex;
                byte RegAdr = 0x11, RegVal = 0x10;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OCD_MSK_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OCD_MSK_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void SCC_MSK_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCC_MSK_BM05.SelectedIndex;
                byte RegAdr = 0x11, RegVal = 0x20;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCC_MSK_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCC_MSK_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void SCD_MSK_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCD_MSK_BM05.SelectedIndex;
                byte RegAdr = 0x11, RegVal = 0x40;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCD_MSK_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCD_MSK_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void SDSG_MSK_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SDSG_MSK_BM05.SelectedIndex;
                byte RegAdr = 0x11, RegVal = 0x80;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SDSG_MSK_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SDSG_MSK_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void CB1_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CB1_BM05.SelectedIndex;
                byte RegAdr = 0x12, RegVal = 0x01;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CB1_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CB1_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void CB2_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CB2_BM05.SelectedIndex;
                byte RegAdr = 0x12, RegVal = 0x02;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CB2_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CB2_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void CB3_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CB3_BM05.SelectedIndex;
                byte RegAdr = 0x12, RegVal = 0x04;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CB3_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CB3_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void CB4_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CB4_BM05.SelectedIndex;
                byte RegAdr = 0x12, RegVal = 0x08;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CB4_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CB4_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void CB_EN_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CB_EN_BM05.SelectedIndex;
                byte RegAdr = 0x12, RegVal = 0x10;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CB_EN_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CB_EN_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void WAKE_HIGH_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)WAKE_HIGH_BM05.SelectedIndex;
                byte RegAdr = 0x13, RegVal = 0x01;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    WAKE_HIGH_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    WAKE_HIGH_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void VDD_EN_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)VDD_EN_BM05.SelectedIndex;
                byte RegAdr = 0x13, RegVal = 0x02;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    VDD_EN_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    VDD_EN_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void LD_SRC_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)LD_SRC_BM05.SelectedIndex;
                byte RegAdr = 0x13, RegVal = 0x04;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    LD_SRC_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    LD_SRC_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void POWER_MCU_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)POWER_MCU_BM05.SelectedIndex;
                byte RegAdr = 0x13, RegVal = 0x08;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    POWER_MCU_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    POWER_MCU_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void SHUTB_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SHUTB_BM05.SelectedIndex;
                byte RegAdr = 0x14, RegVal = 0x01;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SHUTB_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SHUTB_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void SHUTA_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SHUTA_BM05.SelectedIndex;
                byte RegAdr = 0x14, RegVal = 0x02;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SHUTA_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SHUTA_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void GO_SLP_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)GO_SLP_BM05.SelectedIndex;
                byte RegAdr = 0x14, RegVal = 0x40;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    GO_SLP_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    GO_SLP_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void DRST_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)DRST_BM05.SelectedIndex;
                byte RegAdr = 0x14, RegVal = 0x80;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    DRST_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    DRST_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void CHG_ON_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CHG_ON_BM05.SelectedIndex;
                byte RegAdr = 0x15, RegVal = 0x01;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CHG_ON_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CHG_ON_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void DSG_ON_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)DSG_ON_BM05.SelectedIndex;
                byte RegAdr = 0x15, RegVal = 0x02;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    DSG_ON_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    DSG_ON_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void BMC_ON_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)BMC_ON_BM05.SelectedIndex;
                byte RegAdr = 0x15, RegVal = 0x04;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    BMC_ON_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    BMC_ON_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void PCHG_ON_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)PCHG_ON_BM05.SelectedIndex;
                byte RegAdr = 0x15, RegVal = 0x08;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    PCHG_ON_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    PCHG_ON_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void SDSG_ON_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SDSG_ON_BM05.SelectedIndex;
                byte RegAdr = 0x15, RegVal = 0x10;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SDSG_ON_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SDSG_ON_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void FET_EN_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)FET_EN_BM05.SelectedIndex;
                byte RegAdr = 0x15, RegVal = 0x20;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    FET_EN_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    FET_EN_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void CP_EN_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CP_EN_BM05.SelectedIndex;
                byte RegAdr = 0x15, RegVal = 0x40;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CP_EN_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CP_EN_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void VBUS_ON_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)VBUS_ON_BM05.SelectedIndex;
                byte RegAdr = 0x15, RegVal = 0x80;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    VBUS_ON_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    VBUS_ON_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void MUX0_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)MUX0_BM05.SelectedIndex;
                byte RegAdr = 0x16, RegVal = 0x01;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    MUX0_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    MUX0_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void MUX1_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)MUX1_BM05.SelectedIndex;
                byte RegAdr = 0x16, RegVal = 0x02;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    MUX1_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    MUX1_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void MUX2_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)MUX2_BM05.SelectedIndex;
                byte RegAdr = 0x16, RegVal = 0x04;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    MUX2_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    MUX2_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void MUX3_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)MUX3_BM05.SelectedIndex;
                byte RegAdr = 0x16, RegVal = 0x08;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    MUX3_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    MUX3_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void WAKE_CMP0_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)WAKE_CMP0_BM05.SelectedIndex;
                byte RegAdr = 0x17, RegVal = 0x01;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    WAKE_CMP0_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    WAKE_CMP0_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void WAKE_CMP1_BM05_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)WAKE_CMP1_BM05.SelectedIndex;
                byte RegAdr = 0x17, RegVal = 0x02;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    WAKE_CMP1_BM05.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    WAKE_CMP1_BM05.SelectedIndex = 0;
                else
                    return;
            }
        }



        private bool bit_chosen2(uint n, byte RegAdr, byte judge)    //bit chosen  是干嘛的？？？
        {
            int DAT = 0x00;byte rBuff = 0x00;
            switch (n)
            {
                case 0:
                    {
                        DAT = ~judge;
                        if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return false; }
                        rBuff &= (byte) DAT;
                        break;
                    }
                case 1:
                    {
                        DAT = judge;
                        if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return false; }
                        rBuff |= (byte) DAT;
                        break;
                    }
            }
            if (!I2CLib.WriteReg1Byt(slvAdr, RegAdr, rBuff, CRC_enable)) { return false; }
            return true;
        }
        private bool bit_chosen(uint n, byte RegAdr, byte judge)
        {
            int DAT = 0x00; byte rBuff = 0x00;
            switch (n)
            {
                case 0:
                    {
                        DAT &= judge;
                        if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return false; }
                        rBuff = (byte)DAT;
                        break;
                    }
                case 1:
                    {
                        DAT = judge;
                        if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return false; }
                        rBuff = (byte)DAT;
                        break;
                    }
            }
            if (!I2CLib.WriteReg1Byt(slvAdr, RegAdr, rBuff, CRC_enable)) { return false; }
            return true;
        }

        private void ClearFault_Click(object sender, EventArgs e)
        {
            if (!I2CLib.WriteReg1Byt(slvAdr, 0x09, 0xFF, CRC_enable)) { return; }
        }

   

        private void Reset_Click(object sender, EventArgs e)
        {
            RSNS_BM05.Text = "0"; SCDDx2_BM05.Text = "0";

            OCD_BM05.Text = "0"; SCC_BM05.Text = "0"; SCD_BM05.Text = "0"; SDSG_ALERT_BM05.Text = "0";

            OCD_T0_BM05.Text = "0"; OCD_T1_BM05.Text = "0"; OCD_T2_BM05.Text = "0"; OCD_T3_BM05.Text = "0"; OCD_D0_BM05.Text = "0"; OCD_D1_BM05.Text = "0"; OCD_D2_BM05.Text = "0"; OCD_D3_BM05.Text = "0";

            SCC_T0_BM05.Text = "0"; SCC_T1_BM05.Text = "0"; SCC_T2_BM05.Text = "0"; SCC_D0_BM05.Text = "0"; SCC_D1_BM05.Text = "0"; SCC_D2_BM05.Text = "0"; SCC_D3_BM05.Text = "0";

            SCD1_T0_BM05.Text = "0"; SCD1_T1_BM05.Text = "0"; SCD1_T2_BM05.Text = "0"; SCD1_D0_BM05.Text = "0"; SCD1_D1_BM05.Text = "0"; SCD1_D2_BM05.Text = "0"; SCD1_D3_BM05.Text = "0";

            SCD2_T0_BM05.Text = "0"; SCD2_T1_BM05.Text = "0"; SCD2_T2_BM05.Text = "0"; SCD2_D0_BM05.Text = "0"; SCD2_D1_BM05.Text = "0"; SCD2_D2_BM05.Text = "0"; SCD2_D3_BM05.Text = "0";

            CHG_BM05.Text = "0"; DSG_BM05.Text = "0"; BMC_BM05.Text = "0"; LD_SHORT_BM05.Text = "0";

            OCD_DRI_MSK_BM05.Text = "1"; SCC_DRI_MSK_BM05.Text = "1"; SCD_DRI_MSK_BM05.Text = "1"; OCD_MSK_BM05.Text = "0"; SCC_MSK_BM05.Text = "0"; SCD_MSK_BM05.Text = "0"; SDSG_MSK_BM05.Text = "0";

            CB1_BM05.Text = "0"; CB2_BM05.Text = "0"; CB3_BM05.Text = "0"; CB4_BM05.Text = "0"; CB_EN_BM05.Text = "1";

            WAKE_HIGH_BM05.Text = "1"; VDD_EN_BM05.Text = "1"; LD_SRC_BM05.Text = "0"; POWER_MCU_BM05.Text = "1";

            SHUTB_BM05.Text = "0"; SHUTA_BM05.Text = "0"; GO_SLP_BM05.Text = "0"; DRST_BM05.Text = "1";

            CHG_ON_BM05.Text = "0"; DSG_ON_BM05.Text = "0"; BMC_ON_BM05.Text = "0"; PCHG_ON_BM05.Text = "0"; SDSG_ON_BM05.Text = "0"; FET_EN_BM05.Text = "1"; CP_EN_BM05.Text = "1"; VBUS_ON_BM05.Text = "0";

            MUX0_BM05.Text = "0"; MUX1_BM05.Text = "0"; MUX2_BM05.Text = "0"; MUX3_BM05.Text = "0";


            WAKE_CMP0_BM05.Text = "0"; WAKE_CMP1_BM05.Text = "0";
        }

        private void SupportCRC_CheckedChanged(object sender, EventArgs e)
        {
            CRC_enable = true;
        }

        private void donotSupportCRC_CheckedChanged(object sender, EventArgs e)
        {
            CRC_enable = false;
        }


        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void SYS_STAT_SHOW_TextChanged(object sender, EventArgs e)
        {

        }

    

    

        private void groupBox8_Enter(object sender, EventArgs e)
        {

        }

   


        private void CELLBAL2_SHOW_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void SD1_Click(object sender, EventArgs e)
        {
            if (!I2CLib.WriteReg1Byt(slvAdr, 0x14, 0x81, CRC_enable)) { return; }
        }

        private void SD2_Click(object sender, EventArgs e)
        {

            if (!I2CLib.WriteReg1Byt(slvAdr, 0x14, 0x82, CRC_enable)) { return; }
        }
    }
}


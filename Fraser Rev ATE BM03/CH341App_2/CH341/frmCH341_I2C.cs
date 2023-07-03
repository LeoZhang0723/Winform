using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;                                                      //灰色的有什么含义，是没有被使用吗？【LEO】
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClsLib;
using Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;                           //using里面的和lib里面有什么区别？   【LEO】
using System.IO.Ports;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;

namespace CH341App
{
    public partial class frmCH341_I2C : Form
    {
        // 辅助介面
        // frmCH341_GPIO fGPIOCtl;
        I2C_W_R f1;
//        Timer tmrPcs = new Timer();
        public string isNull = null;
        // 定义局部变量
        public static uint slvAdr;
        public double ADCgain = 97.66;
        public uint ADCoffset =  0;
        public bool isCall = false; // 
        public bool IsCall { set { isCall = value; } }
        private string comboload = null;
        public static bool CRC_enable = false;

        //*******excel相关的变量定义**************//
        Excel.Application excel_export;
        Excel.Workbook book_export;

        public int circle_time = 1;
        public SerialPort SpCom1 = new SerialPort();
        public string strrrrr = "";
        public string[,] data2 = new string[10000, 2];
 //       public string[,] data3 = new string[100, 1];
        //       public int Readbuffer_origin = 0;
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
            f1 = new I2C_W_R();
            { 
            InitializeComponent();
                this.SpCom1.DataReceived += new SerialDataReceivedEventHandler(SpCom1_DataReceived);
            }
            // 初始化应用程序
            string revInf = "Rev 1.0";
            string dataInf = System.IO.File.GetLastWriteTime(this.GetType().Assembly.Location).ToString("yyyyMMdd");//yyyy/MM/dd HH:mm:ss
            lblInfo.Text = revInf + " " + dataInf;
//            SpCom1.ReadBufferSize = 1024;
            cmbI2CPort.SelectedIndex = (int)I2CLib.NumPort;
            cmbBitRat.SelectedIndex = (int)I2CLib.BitRate;
//            cmbTranMod.SelectedIndex = (int)I2CLib.TranMode;
            SYS_STAT_SHOW.Text = "0000 0000";
            CELLBAL1_SHOW.Text = "0000 0000";
            CELLBAL2_SHOW.Text = "0000 0000";
            CELLBAL3_SHOW.Text = "0000 0000";
            SYS_CTRL1_SHOW.Text= "0011 0100";
            SYS_CTRL2_SHOW.Text = "0000 0000";
            PROTECT1_SHOW.Text = "0010 0000";
            PROTECT2_SHOW.Text = "0000 0000";
            PROTECT3_SHOW.Text = "0000 0000";
            OV_TRIP_SHOW.Text = "1010 0111";
            UV_TRIP_SHOW.Text = "0110 0011";

            ADCGain_Val.Text = "97.66";
            ADCOffset_Val.Text = "0";

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
        private void frmMain_Load(object sender, EventArgs e)
        {
            //           DataGridViewRow Row = new DataGridViewRow();
            this.dataGridView1.Rows.Add(22);
            for (int i = 0; i < 18; i++)
                this.dataGridView1.Rows[i].Cells[0].Value = "Cell Volatge" + (i + 1).ToString();
            this.dataGridView1.Rows[18].Cells[0].Value = "BAT Voltage";
            this.dataGridView1.Rows[19].Cells[0].Value = "Temp Sensor1";
            this.dataGridView1.Rows[20].Cells[0].Value = "Temp Sensor2";
            this.dataGridView1.Rows[21].Cells[0].Value = "Temp Sensor3";
            this.dataGridView1.Rows[22].Cells[0].Value = "Coulomb Counter";
            CCREADY_CHOS.Text = "0";
            INTCBDONE.Text = "0";
            DEVXD.Text = "0";
            OVRDAL.Text = "0";
            OV.Text = "0";
            UV.Text = "0";
            OCD.Text = "0";
            SCD.Text = "0";
            CB1.Text = "0"; CB2.Text = "0"; CB3.Text = "0"; CB4.Text = "0"; CB5.Text = "0"; CB6.Text = "0";
            CB7.Text = "0"; CB8.Text = "0"; CB9.Text = "0"; CB10.Text = "0"; CB11.Text = "0"; CB12.Text = "0";
            CB13.Text = "0"; CB14.Text = "0"; CB15.Text = "0"; CB16.Text = "0"; CB17.Text = "0"; CB18.Text = "0";
            LOAD_P.Text = "0"; CB_ST1.Text = "0"; CB_ST0.Text = "1"; ADC_EN2.Text = "1"; TEMP_S.Text = "0"; CB_HOST.Text = "1"; SHUTB.Text = "0"; SHUTA.Text = "0";
            DLY_DIS.Text = "0";CC_EN2.Text = "0";CC_ONE.Text = "0";RSVD1.Text = "0";RSVD2.Text = "0";RSVD3.Text = "0";DSG_ON.Text = "0";CHG_ON.Text = "0";
            RSNS.Text = "0";RSVD4.Text = "0";RSVD5.Text = "1";SCD_D0.Text = "0";SCD_D1.Text = "0";SCD_T0.Text = "0";SCD_T1.Text = "0";SCD_T2.Text = "0";
            RSVD6.Text = "0";OCD_D2.Text = "0";OCD_D1.Text = "0";OCD_D0.Text = "0";OCD_T0.Text = "0";OCD_T1.Text = "0";OCD_T2.Text = "0";OCD_T3.Text = "0";
            UV_D1.Text = "0";UV_D0.Text = "0";OV_D1.Text = "0";OV_D0.Text = "0"; RSVD7.Text = "0"; RSVD8.Text = "0"; RSVD9.Text = "0"; RSVD10.Text = "0";
            OV_T7.Text = "1"; OV_T6.Text = "0"; OV_T5.Text = "1"; OV_T4.Text = "0"; OV_T3.Text = "0"; OV_T2.Text = "1"; OV_T1.Text = "1"; OV_T0.Text = "1";
            UV_T7.Text = "0"; UV_T6.Text = "1"; UV_T5.Text = "1"; UV_T4.Text = "0"; UV_T3.Text = "0"; UV_T2.Text = "0"; UV_T1.Text = "1"; UV_T0.Text = "1";
            comboload = "1";


            string export_path = @"E:\test.xlsx";    //*******导出的excel路径**************//
            excel_export = new Excel.Application();    //*******新建一个excel**************//
            if (excel_export != null)
            {
                if (File.Exists(export_path))
                {
                    book_export = excel_export.Workbooks.Open(export_path, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                }
                else
                {
                    book_export = excel_export.Workbooks.Add(true);
                }

            }
            else
            {
                throw new Exception("未找到Excel文件");
            }



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
            I2CLib.SetBitRate((I2CLib.eBitRate)cmbBitRat.SelectedIndex);
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
        // I2C写数据
        private void txtWrite_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)0x08) //删除键
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
            tipMsg.SetToolTip(this.cmbI2CPort,
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
                Update_Display_button.Enabled = true;
            }


        }
        /*       public void WriteData(string[,] data, int startRow, int startColumn)
               {
                   int rowNumber = data.GetLength(0);
                   int columnNumber = data.GetLength(1);

                   for (int i = 0; i < rowNumber; i++)
                   {
                       for (int j = 0; j < columnNumber; j++)
                       {
                           //在Excel中，如果某单元格以单引号“'”开头，表示该单元格为纯文本，因此，我们在每个单元格前面加单引号。 
                           excel_export.Cells[startRow + i, startColumn] = "'" + data[i, j];
                       }
                   }
               }
               private void ExcelAction(string[,] data4,string[,] data5)
               {

                   Excel.Worksheet sheet_export = book_export.ActiveSheet as Excel.Worksheet;
                   //           Excel.Range range = sheet_export.Range[excel_export.Cells[1, 1], excel_export.Cells[1, 256]];
                   for (int i = 0; i < 100; i++)
                   {
                       WriteData(data4, 1 + i + circle_time * 100, 1);
                       WriteData(data5, 1 + i + circle_time * 100, 3);
                   }



                   GC.Collect();
                   //            book_export.SaveAs(export_path);
                   book_export.Save();
       //            excel_export.Quit();

               }
       */

        private void ExcelAction()
        {

            Excel.Worksheet sheet_export = book_export.ActiveSheet as Excel.Worksheet;
            //            sheet_export = book_export.Worksheets[1];
            Excel.Range r = sheet_export.Range[sheet_export.Cells[1 + circle_time, 1], sheet_export.Cells[100 + circle_time, 2]];
            r.Value = data2;
            book_export.Save();



        }


   private void Update_table()
                    {
                        SpCom1.WriteLine("READ?\r");
                       uint rLn = 46; //rLn/2表示一共有多少寄存器
                        byte[] rAll = new byte[rLn];
                        if (!ADC_all(rLn, rAll)) {return; }
                        //           byte[] currenttest = new byte[2];

                        /*            string str = "";
                                                for (int i = 0; i < rLn; i++)
                                                {
                                                    str = str + string.Format("{0:X2}", rAll[i]);
                                                    if (i > 0 & i % 2 != 0) str = str + " ";
                                                }

                                    txtReadData2.Text = str;
                        */

            


            string[,] data = new string[19, 1];

            for (int i=0;i<18;i++)
                 data[i, 0] = BitConverter.ToString(rAll,2*i,2).Replace("-", "");
            data[18, 0] = BitConverter.ToString(rAll, 44, 2).Replace("-", "");
/*
            string export_path = @"F:\test.xlsx";
            excel_export = new Excel.Application();
            if (excel_export != null)
            { 
                if (File.Exists(export_path))
                {
                book_export = excel_export.Workbooks.Open(export_path, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
                }
                else
                {
                book_export = excel_export.Workbooks.Add(true);
                }

            }
            else
            {
                    throw new Exception("未找到Excel文件");
            }
*/ 
            if (Cell1_TEST.Checked)
                data2[(circle_time - 1) % 1, 0] = data[0, 0];
            else if(Cell2_TEST.Checked)
                data2[(circle_time - 1) % 1, 0] = data[1, 0];
            else if (Cell3_TEST.Checked)
                data2[(circle_time - 1) % 1, 0] = data[2, 0];
            else if (Cell4_TEST.Checked)
                data2[(circle_time - 1) % 1, 0] = data[3, 0];
            else if (Cell5_TEST.Checked) 
                data2[(circle_time - 1) % 1, 0] = data[4, 0];
            else if (Cell6_TEST.Checked)
                data2[(circle_time - 1) % 1, 0] = data[5, 0];
            else if (Current_TEST.Checked)
                data2[(circle_time - 1) % 1, 0] = data[18, 0];
            else if (Cell7_TEST.Checked)
                data2[(circle_time - 1) % 1, 0] = data[6, 0];
            else if (Cell8_TEST.Checked)
                data2[(circle_time - 1) % 1, 0] = data[7, 0];
            else if (Cell9_TEST.Checked)
                data2[(circle_time - 1) % 1, 0] = data[8, 0];
            else if (Cell10_TEST.Checked)
                data2[(circle_time - 1) % 1, 0] = data[9, 0];
            else if (Cell11_TEST.Checked)
                data2[(circle_time - 1) % 1, 0] = data[10, 0];
            else if (Cell12_TEST.Checked)
                data2[(circle_time - 1) % 1, 0] = data[11, 0];
            else if (Cell13_TEST.Checked)
                data2[(circle_time - 1) % 1, 0] = data[12, 0];
            else if (Cell14_TEST.Checked)
                data2[(circle_time - 1) % 1, 0] = data[13, 0];
            else if (Cell15_TEST.Checked)
                data2[(circle_time - 1) % 1, 0] = data[14, 0];
            else if (Cell16_TEST.Checked)
                data2[(circle_time - 1) % 1, 0] = data[15, 0];
            else if (Cell17_TEST.Checked)
                data2[(circle_time - 1) % 1, 0] = data[16, 0];
            else if (Cell18_TEST.Checked)
                data2[(circle_time - 1) % 1, 0] = data[17, 0];

            data2[(circle_time - 1) % 1, 1] = strrrrr;

/*
            Excel.Worksheet sheet_export = book_export.ActiveSheet as Excel.Worksheet;
            //           Excel.Range range = sheet_export.Range[excel_export.Cells[1, 1], excel_export.Cells[1, 256]];
            WriteData(data3, 1 + circle_time, 1);
            WriteData(data2, 1 + circle_time, 3);

            


            GC.Collect();
//            book_export.SaveAs(export_path);
            book_export.Save();
            excel_export.Quit();
*/
            if (circle_time % 1 == 0)
                ExcelAction();

            circle_time = circle_time + 1;
            /* 
                        int[] StrOut = new int[rLn];
                       for(int i=0;i<11;i++)
                            int.TryParse(Convert.ToString(rAll[i], 2),out StrOut[i]);
                       SYS_STAT_SHOW.Text = string.Format("{0:0000 0000}", StrOut[0]);
                        CELLBAL1_SHOW.Text = string.Format("{0:0000 0000}", StrOut[1]);
                        CELLBAL2_SHOW.Text = string.Format("{0:0000 0000}", StrOut[2]);
                        CELLBAL3_SHOW.Text = string.Format("{0:0000 0000}", StrOut[3]);
                        SYS_CTRL1_SHOW.Text = string.Format("{0:0000 0000}", StrOut[4]);
                        SYS_CTRL2_SHOW.Text = string.Format("{0:0000 0000}", StrOut[5]);
                        PROTECT1_SHOW.Text = string.Format("{0:0000 0000}", StrOut[6]);
                        PROTECT2_SHOW.Text = string.Format("{0:0000 0000}", StrOut[7]);
                        PROTECT3_SHOW.Text = string.Format("{0:0000 0000}", StrOut[8]);
                        OV_TRIP_SHOW.Text = string.Format("{0:0000 0000}", StrOut[9]);
                        UV_TRIP_SHOW.Text = string.Format("{0:0000 0000}", StrOut[10]);
            */
            /*          
                     for (int i = 1; i < 19; i++)
                      {
                          if (i < 7)
                              (this.groupBox6.Controls["CB" + i.ToString()] as ComboBox).SelectedIndex = (CBn[0] >> (i - 1)) % 2;
                          else if (i > 12)
                              (this.groupBox6.Controls["CB" + i.ToString()] as ComboBox).SelectedIndex = (CBn[2] >> (i - 13)) % 2;
                          else
                              (this.groupBox6.Controls["CB" + i.ToString()] as ComboBox).SelectedIndex = (CBn[1] >> (i - 7)) % 2;
                      }
           *//*            for (int i = 0; i < 8; i++)
                      {
                          (this.groupBox6.Controls["OV_T" + i.ToString()] as ComboBox).SelectedIndex = (rAll[9] >> i) % 2;
                          (this.groupBox6.Controls["UV_T" + i.ToString()] as ComboBox).SelectedIndex = (rAll[10] >> i) % 2;
                      }
                      UV_D1.SelectedIndex = rAll[8] >> 7;
                      UV_D0.SelectedIndex = (rAll[8] >> 6) % 2;
                      OV_D1.SelectedIndex = (rAll[8] >> 5) % 2;
                      OV_D0.SelectedIndex = (rAll[8] >> 4) % 2;
                      RSVD7.SelectedIndex = (rAll[8] >> 3) % 2;
                      RSVD8.SelectedIndex = (rAll[8] >> 2) % 2;
                      RSVD9.SelectedIndex = (rAll[8] >> 1) % 2;
                      RSVD10.SelectedIndex = rAll[8]  % 2;

                      RSVD6.SelectedIndex = rAll[7] >> 7;
                      OCD_D2.SelectedIndex = (rAll[7] >> 6) % 2;
                      OCD_D1.SelectedIndex = (rAll[7] >> 5) % 2;
                      OCD_D0.SelectedIndex = (rAll[7] >> 4) % 2;
                      OCD_T3.SelectedIndex = (rAll[7] >> 3) % 2;
                      OCD_T2.SelectedIndex = (rAll[7] >> 2) % 2;
                      OCD_T1.SelectedIndex = (rAll[7] >> 1) % 2;
                      OCD_T0.SelectedIndex = rAll[7] % 2;

                      RSNS.SelectedIndex = rAll[6] >> 7;
                      RSVD4.SelectedIndex = (rAll[6] >> 6) % 2;
                      RSVD5.SelectedIndex = (rAll[6] >> 5) % 2;
                      SCD_D1.SelectedIndex = (rAll[6] >> 4) % 2;
                      SCD_D0.SelectedIndex = (rAll[6] >> 3) % 2;
                      SCD_T2.SelectedIndex = (rAll[6] >> 2) % 2;
                      SCD_T1.SelectedIndex = (rAll[6] >> 1) % 2;
                      SCD_T0.SelectedIndex = rAll[6] % 2;

                      DLY_DIS.SelectedIndex = rAll[5] >> 7;
                      CC_EN2.SelectedIndex = (rAll[5] >> 6) % 2;
                      CC_ONE.SelectedIndex = (rAll[5] >> 5) % 2;
                      RSVD1.SelectedIndex = (rAll[5] >> 4) % 2;
                      RSVD2.SelectedIndex = (rAll[5] >> 3) % 2;
                      RSVD3.SelectedIndex = (rAll[5] >> 2) % 2;
                      DSG_ON.SelectedIndex = (rAll[5] >> 1) % 2;
                      CHG_ON.SelectedIndex = rAll[5] % 2;

                      LOAD_P.SelectedIndex = rAll[4] >> 7;
                      CB_ST1.SelectedIndex = (rAll[4] >> 6) % 2;
                      CB_ST0.SelectedIndex = (rAll[4] >> 5) % 2;
                      ADC_EN2.SelectedIndex = (rAll[4] >> 4) % 2;
                      TEMP_S.SelectedIndex = (rAll[4] >> 3) % 2;
                      CB_HOST.SelectedIndex = (rAll[4] >> 2) % 2;
                      SHUTA.SelectedIndex = (rAll[4] >> 1) % 2;
                      SHUTB.SelectedIndex = rAll[4] % 2;
               */       /*
                                  CCREADY_CHOS.SelectedIndex = rAll[0] >> 7;
                                  INTCBDONE.SelectedIndex = (rAll[0] >> 6) % 2;
                                  DEVXD.SelectedIndex = (rAll[0] >> 5) % 2;
                                  OVRDAL.SelectedIndex = (rAll[0] >> 4) % 2;
                                  UV.SelectedIndex = (rAll[0] >> 3) % 2;
                                  OV.SelectedIndex = (rAll[0] >> 2) % 2;
                                  SCD.SelectedIndex = (rAll[0] >> 1) % 2;
                                  OCD.SelectedIndex = rAll[0] % 2;

                                  byte RegAddr = 0x51; //ADCoffset地址0x51
                                  byte temp;
                                  if (!I2CLib.ReadReg1Byt(slvAdr, RegAddr, out temp, CRC_enable)) { return; }
                                  ADCoffset = (uint)temp;

                                  ADCgainconv();
                                  ADCGain_Val.Text = Convert.ToString(ADCgain, 10);
                                  ADCOffset_Val.Text = Convert.ToString(ADCoffset, 10);
                      */
                        /*      
                             byte[] Cell = new byte[36];
                             for (int i = 0; i < 36; i += 2)//Cell1的高8位地址 0x0C,对应第12个寄存器存储的是Cell数据
                             {
                 //                rAll[i + 0x0C] &= 0x3F;
                                 Cell[i + 1] = rAll[i + 0x0C];
                                 Cell[i] = rAll[i + 0x0C + 1];
                                 int Cella = BitConverter.ToUInt16(Cell, i);
                                 double Cell_val = (Cella * ADCgain) / 1000000.0;
                                 this.dataGridView1.Rows[i / 2].Cells[1].Value = Cell_val.ToString("f3");
                             }

                             byte[] BAT = new byte[2];
                             BAT[1] = rAll[0x30];
                             BAT[0] = rAll[0x31];
                             int BATa = BitConverter.ToUInt16(BAT, 0);
                             double BAT_val = (BATa * ADCgain * 32) / 1000000.0;       //offset数量有关,这个offset后面的10需要后续做一个判定，去判断到底一共有几节电池在线上       
                             this.dataGridView1.Rows[18].Cells[1].Value = BAT_val.ToString("f3");

                            byte[] TEMP = new byte[6];
                             for (int i = 0; i < 6; i += 2)//Temp的数据
                             {
                  //               rAll[i + 0x32] &= 0x3F;
                                 TEMP[i + 1] = rAll[i + 0x32];
                                 TEMP[i] = rAll[i + 0x32 + 1];
                                 int TEMPa = BitConverter.ToUInt16(TEMP, i);
                                 double TEMP_val = (TEMPa * ADCgain) / 1000000.0;
                                 this.dataGridView1.Rows[i / 2 + 19].Cells[1].Value = TEMP_val.ToString("f3");
                             }

                             byte[] CC = new byte[2];
                             CC[0] = rAll[0x39];
                             CC[1] = rAll[0x38];
                             Int16 CCa = BitConverter.ToInt16(CC, 0);
                             double CC_val = (CCa * 8.44) / 1000000.0;
                             this.dataGridView1.Rows[22].Cells[1].Value = CC_val.ToString("f6");
                   */
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

            byte CellRegAddr = 0x0C;   //Cell1的高8位地址 0x0C            

            if (!I2CLib.ReadRegister3(slvAdr, CellRegAddr, rLn, ref rBuff, CRC_enable))
            {
                return false;
            }
            return true;

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void nudReadLen_ValueChanged(object sender, EventArgs e)
        {

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
                if (time_of_timer <= 250)
                {
                    timer_auto_Update.Interval = 250;
                }
                else
                    timer_auto_Update.Interval = time_of_timer;
            }
                

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CC_continous_CheckedChanged(object sender, EventArgs e)
        {
            byte rBuff;
            if (!ErrHdl.scanfalse)
            {
                ErrHdl.scanfalse = true;

                return;
            }
            if (!I2CLib.ReadReg1Byt(slvAdr, 0x05, out rBuff, CRC_enable)) { return; }
            rBuff |= 0x40;
            if (!I2CLib.WriteReg1Byt(slvAdr, 0x05, rBuff, CRC_enable)) { return; }
        }

        private void CC_one_shot_CheckedChanged(object sender, EventArgs e)
        {
            byte rBuff;
            if (!ErrHdl.scanfalse)
            {
                ErrHdl.scanfalse = true;

                return;
            }
            if (!I2CLib.ReadReg1Byt(slvAdr, 0x05, out rBuff, CRC_enable)) { return; }
            rBuff |= 0x20;
            if (!I2CLib.WriteReg1Byt(slvAdr, 0x05, rBuff, CRC_enable)) { return; }

            byte rBuff2;
            while (true)
            { if (!I2CLib.ReadReg1Byt(slvAdr, 0x00, out rBuff2, CRC_enable)) { return; }
                if ((rBuff2 &= 0x80) == 0x80)
                {
 //                   if (!I2CLib.WriteReg1Byt(slvAdr, 0x00, 0x80, CRC_enable)) { return; }
                    CC_disabled.Select();
                    break;
                }
            }
        }

        private void CC_disabled_CheckedChanged(object sender, EventArgs e)
        {
            byte rBuff;
            if (!ErrHdl.scanfalse)
            {
                ErrHdl.scanfalse = true;

                return;
            }
            if (!I2CLib.ReadReg1Byt(slvAdr, 0x05, out rBuff, CRC_enable)) { return; }
            rBuff &= 0xBF;
            if (!I2CLib.WriteReg1Byt(slvAdr, 0x05, rBuff, CRC_enable)) { return; }
        }

        private void ADC_EN_CheckedChanged(object sender, EventArgs e)
        {
            byte rBuff;
            if (!ErrHdl.scanfalse)
            {
                ErrHdl.scanfalse = true;

                return;
            }
            if (!I2CLib.ReadReg1Byt(slvAdr, 0x04, out rBuff, CRC_enable)) { return; }
            rBuff |= 0x10;
            if (!I2CLib.WriteReg1Byt(slvAdr, 0x04, rBuff, CRC_enable)) { return; }
        }

        private void ADC_DIS_CheckedChanged(object sender, EventArgs e)
        {
            byte rBuff;
            if (!ErrHdl.scanfalse)
            {
                ErrHdl.scanfalse = true;

                return;
            }
            if (!I2CLib.ReadReg1Byt(slvAdr, 0x04, out rBuff, CRC_enable)) { return; }
            rBuff &= 0xEF;
            if (!I2CLib.WriteReg1Byt(slvAdr, 0x04, rBuff, CRC_enable)) { return; }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void INT_TEMP_CheckedChanged(object sender, EventArgs e)
        {
            byte rBuff;
            if (!ErrHdl.scanfalse)
            {
                ErrHdl.scanfalse = true;

                return;
            }
            if (!I2CLib.ReadReg1Byt(slvAdr, 0x04, out rBuff, CRC_enable)) { return; }
            rBuff &= 0xF7;
            if (!I2CLib.WriteReg1Byt(slvAdr, 0x04, rBuff, CRC_enable)) { return; }
        }

        private void EXT_TEMP_CheckedChanged(object sender, EventArgs e)
        {
            byte rBuff;
            if (!ErrHdl.scanfalse)
            {
                ErrHdl.scanfalse = true;

                return;
            }
            if (!I2CLib.ReadReg1Byt(slvAdr, 0x04, out rBuff, CRC_enable)) { return; }
            rBuff |= 0x08;
            if (!I2CLib.WriteReg1Byt(slvAdr, 0x04, rBuff, CRC_enable)) { return; }
        }



        private void I2C_WR_Click(object sender, EventArgs e)
        {
            I2C_W_R f1 = new I2C_W_R();
            f1.Show();
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtWrLen_TextChanged(object sender, EventArgs e)
        {

        }

        private void chkNoStp_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void CCREADY_CHOS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboload!=null)
            { 
            uint n = (uint)CCREADY_CHOS.SelectedIndex;
            byte RegAdr = 0x00, RegVal = 0x80;
            if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
            byte rBuff;
            if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

            if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                CCREADY_CHOS.SelectedIndex = 1;
            else if ((rBuff &= RegVal) == 0x00)
                CCREADY_CHOS.SelectedIndex = 0;
            else
                return;
            }
        }
        private void INTCBDONE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)INTCBDONE.SelectedIndex;
                byte RegAdr = 0x00, RegVal = 0x40;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    INTCBDONE.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    INTCBDONE.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void DEVXD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)DEVXD.SelectedIndex;
                byte RegAdr = 0x00, RegVal = 0x20;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    DEVXD.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    DEVXD.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void OVRDAL_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OVRDAL.SelectedIndex;
                byte RegAdr = 0x00, RegVal = 0x10;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OVRDAL.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OVRDAL.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void UV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)UV.SelectedIndex;
                byte RegAdr = 0x00, RegVal = 0x08;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    UV.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    UV.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void OV_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OV.SelectedIndex;
                byte RegAdr = 0x00, RegVal = 0x04;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OV.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OV.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void OCD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OCD.SelectedIndex;
                byte RegAdr = 0x00, RegVal = 0x02;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OCD.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OCD.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void SCD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCD.SelectedIndex;
                byte RegAdr = 0x00, RegVal = 0x01;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCD.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCD.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void CB6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CB6.SelectedIndex;
                byte RegAdr = 0x01, RegVal = 0x20;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CB6.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CB6.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void CB5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CB6.SelectedIndex;
                byte RegAdr = 0x01, RegVal = 0x10;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CB6.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CB6.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void CB4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CB4.SelectedIndex;
                byte RegAdr = 0x01, RegVal = 0x08;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CB4.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CB4.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void CB3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CB3.SelectedIndex;
                byte RegAdr = 0x01, RegVal = 0x04;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CB3.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CB3.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void CB2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CB2.SelectedIndex;
                byte RegAdr = 0x01, RegVal = 0x02;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CB2.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CB2.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void CB1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CB1.SelectedIndex;
                byte RegAdr = 0x01, RegVal = 0x01;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CB1.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CB1.SelectedIndex = 0;
                else
                    return;
            }
        }


        private void CB12_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CB12.SelectedIndex;
                byte RegAdr = 0x02, RegVal = 0x20;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CB12.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CB12.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void CB11_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CB11.SelectedIndex;
                byte RegAdr = 0x02, RegVal = 0x10;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CB11.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CB11.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void CB10_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CB10.SelectedIndex;
                byte RegAdr = 0x02, RegVal = 0x08;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CB10.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CB10.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void CB9_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CB9.SelectedIndex;
                byte RegAdr = 0x02, RegVal = 0x04;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CB9.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CB9.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void CB8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CB8.SelectedIndex;
                byte RegAdr = 0x02, RegVal = 0x02;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CB8.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CB8.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void CB7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CB7.SelectedIndex;
                byte RegAdr = 0x02, RegVal = 0x01;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CB7.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CB7.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void CB18_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CB18.SelectedIndex;
                byte RegAdr = 0x03, RegVal = 0x20;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CB18.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CB18.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void CB17_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CB17.SelectedIndex;
                byte RegAdr = 0x03, RegVal = 0x10;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CB17.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CB17.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void CB16_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CB16.SelectedIndex;
                byte RegAdr = 0x03, RegVal = 0x08;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CB16.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CB16.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void CB15_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CB15.SelectedIndex;
                byte RegAdr = 0x03, RegVal = 0x04;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CB15.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CB15.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void CB14_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CB14.SelectedIndex;
                byte RegAdr = 0x03, RegVal = 0x02;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CB14.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CB14.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void CB13_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CB13.SelectedIndex;
                byte RegAdr = 0x03, RegVal = 0x01;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CB13.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CB13.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void LOAD_P_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)LOAD_P.SelectedIndex;
                byte RegAdr = 0x04, RegVal = 0x80;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    LOAD_P.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    LOAD_P.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void CB_ST1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CB_ST1.SelectedIndex;
                byte RegAdr = 0x04, RegVal = 0x40;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CB_ST1.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CB_ST1.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void CB_ST0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CB_ST0.SelectedIndex;
                byte RegAdr = 0x04, RegVal = 0x20;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CB_ST0.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CB_ST0.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void ADC_EN2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)ADC_EN2.SelectedIndex;
                byte RegAdr = 0x04, RegVal = 0x10;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    ADC_EN2.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    ADC_EN2.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void TEMP_S_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)TEMP_S.SelectedIndex;
                byte RegAdr = 0x04, RegVal = 0x08;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    TEMP_S.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    TEMP_S.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void CB_HOST_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CB_HOST.SelectedIndex;
                byte RegAdr = 0x04, RegVal = 0x04;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CB_HOST.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CB_HOST.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void SHUTA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SHUTA.SelectedIndex;
                byte RegAdr = 0x04, RegVal = 0x02;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SHUTA.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SHUTA.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void SHUTB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SHUTB.SelectedIndex;
                byte RegAdr = 0x04, RegVal = 0x01;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SHUTB.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SHUTB.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void DLY_DIS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)DLY_DIS.SelectedIndex;
                byte RegAdr = 0x05, RegVal = 0x80;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    DLY_DIS.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    DLY_DIS.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void CC_EN2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CC_EN2.SelectedIndex;
                byte RegAdr = 0x05, RegVal = 0x40;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CC_EN2.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CC_EN2.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void CC_ONE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CC_ONE.SelectedIndex;
                byte RegAdr = 0x05, RegVal = 0x20;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CC_ONE.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CC_ONE.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void RSVD1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)RSVD1.SelectedIndex;
                byte RegAdr = 0x05, RegVal = 0x10;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    RSVD1.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    RSVD1.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void RSVD2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)RSVD2.SelectedIndex;
                byte RegAdr = 0x05, RegVal = 0x08;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    RSVD2.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    RSVD2.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void RSVD3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)RSVD3.SelectedIndex;
                byte RegAdr = 0x05, RegVal = 0x04;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    RSVD3.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    RSVD3.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void DSG_ON_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)DSG_ON.SelectedIndex;
                byte RegAdr = 0x05, RegVal = 0x02;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    DSG_ON.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    DSG_ON.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void CHG_ON_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)CHG_ON.SelectedIndex;
                byte RegAdr = 0x05, RegVal = 0x01;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    CHG_ON.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    CHG_ON.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void RSNS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)RSNS.SelectedIndex;
                byte RegAdr = 0x06, RegVal = 0x80;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    RSNS.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    RSNS.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void RSVD4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)RSVD4.SelectedIndex;
                byte RegAdr = 0x06, RegVal = 0x40;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    RSVD4.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    RSVD4.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void RSVD5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)RSVD5.SelectedIndex;
                byte RegAdr = 0x06, RegVal = 0x20;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    RSVD5.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    RSVD5.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void SCD_D1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCD_D1.SelectedIndex;
                byte RegAdr = 0x06, RegVal = 0x10;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCD_D1.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCD_D1.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void SCD_D0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCD_D0.SelectedIndex;
                byte RegAdr = 0x06, RegVal = 0x08;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCD_D0.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCD_D0.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void SCD_T2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCD_T2.SelectedIndex;
                byte RegAdr = 0x06, RegVal = 0x04;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCD_T2.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCD_T2.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void SCD_T1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCD_T1.SelectedIndex;
                byte RegAdr = 0x06, RegVal = 0x02;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCD_T1.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCD_T1.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void SCD_T0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)SCD_T0.SelectedIndex;
                byte RegAdr = 0x06, RegVal = 0x01;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    SCD_T0.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    SCD_T0.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void RSVD6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)RSVD6.SelectedIndex;
                byte RegAdr = 0x07, RegVal = 0x80;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    RSVD6.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    RSVD6.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void OCD_D2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OCD_D2.SelectedIndex;
                byte RegAdr = 0x07, RegVal = 0x40;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OCD_D2.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OCD_D2.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void OCD_D1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OCD_D1.SelectedIndex;
                byte RegAdr = 0x07, RegVal = 0x20;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OCD_D1.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OCD_D1.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void OCD_D0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OCD_D0.SelectedIndex;
                byte RegAdr = 0x07, RegVal = 0x10;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OCD_D0.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OCD_D0.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void OCD_T3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OCD_T3.SelectedIndex;
                byte RegAdr = 0x07, RegVal = 0x08;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OCD_T3.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OCD_T3.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void OCD_T2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OCD_T2.SelectedIndex;
                byte RegAdr = 0x07, RegVal = 0x04;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OCD_T2.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OCD_T2.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void OCD_T1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OCD_T1.SelectedIndex;
                byte RegAdr = 0x07, RegVal = 0x02;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OCD_T1.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OCD_T1.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void OCD_T0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OCD_T0.SelectedIndex;
                byte RegAdr = 0x07, RegVal = 0x01;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OCD_T0.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OCD_T0.SelectedIndex = 0;
                else
                    return;
            }
        }

        private void UV_D1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)UV_D1.SelectedIndex;
                byte RegAdr = 0x08, RegVal = 0x80;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    UV_D1.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    UV_D1.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void UV_D0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)UV_D0.SelectedIndex;
                byte RegAdr = 0x08, RegVal = 0x40;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    UV_D0.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    UV_D0.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void OV_D1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OV_D1.SelectedIndex;
                byte RegAdr = 0x08, RegVal = 0x20;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OV_D1.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OV_D1.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void OV_D0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OV_D0.SelectedIndex;
                byte RegAdr = 0x08, RegVal = 0x10;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OV_D0.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OV_D0.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void RSVD7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)RSVD7.SelectedIndex;
                byte RegAdr = 0x08, RegVal = 0x08;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    RSVD7.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    RSVD7.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void RSVD8_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)RSVD8.SelectedIndex;
                byte RegAdr = 0x08, RegVal = 0x04;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    RSVD8.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    RSVD8.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void RSVD9_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)RSVD9.SelectedIndex;
                byte RegAdr = 0x08, RegVal = 0x02;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    RSVD9.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    RSVD9.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void RSVD10_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)RSVD10.SelectedIndex;
                byte RegAdr = 0x08, RegVal = 0x01;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    RSVD10.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    RSVD10.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void OV_T7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OV_T7.SelectedIndex;
                byte RegAdr = 0x09, RegVal = 0x80;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OV_T7.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OV_T7.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void OV_T6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OV_T6.SelectedIndex;
                byte RegAdr = 0x09, RegVal = 0x40;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OV_T6.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OV_T6.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void OV_T5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OV_T5.SelectedIndex;
                byte RegAdr = 0x09, RegVal = 0x20;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OV_T5.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OV_T5.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void OV_T4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OV_T4.SelectedIndex;
                byte RegAdr = 0x09, RegVal = 0x10;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OV_T4.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OV_T4.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void OV_T3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OV_T3.SelectedIndex;
                byte RegAdr = 0x09, RegVal = 0x08;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OV_T3.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OV_T3.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void OV_T2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OV_T2.SelectedIndex;
                byte RegAdr = 0x09, RegVal = 0x04;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OV_T2.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OV_T2.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void OV_T1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OV_T1.SelectedIndex;
                byte RegAdr = 0x09, RegVal = 0x02;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OV_T1.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OV_T1.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void OV_T0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)OV_T0.SelectedIndex;
                byte RegAdr = 0x09, RegVal = 0x01;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    OV_T0.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    OV_T0.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void UV_T7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)UV_T7.SelectedIndex;
                byte RegAdr = 0x0A, RegVal = 0x80;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    UV_T7.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    UV_T7.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void UV_T6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)UV_T6.SelectedIndex;
                byte RegAdr = 0x0A, RegVal = 0x40;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    UV_T6.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    UV_T6.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void UV_T5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)UV_T5.SelectedIndex;
                byte RegAdr = 0x0A, RegVal = 0x20;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    UV_T5.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    UV_T5.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void UV_T4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)UV_T4.SelectedIndex;
                byte RegAdr = 0x0A, RegVal = 0x10;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    UV_T4.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    UV_T4.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void UV_T3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)UV_T3.SelectedIndex;
                byte RegAdr = 0x0A, RegVal = 0x08;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    UV_T3.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    UV_T3.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void UV_T2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)UV_T2.SelectedIndex;
                byte RegAdr = 0x0A, RegVal = 0x04;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    UV_T2.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    UV_T2.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void UV_T1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)UV_T1.SelectedIndex;
                byte RegAdr = 0x0A, RegVal = 0x02;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    UV_T1.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    UV_T1.SelectedIndex = 0;
                else
                    return;
            }
        }
        private void UV_T0_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboload != null)
            {
                uint n = (uint)UV_T0.SelectedIndex;
                byte RegAdr = 0x0A, RegVal = 0x01;
                if (!bit_chosen2(n, RegAdr, RegVal)) { return; };
                byte rBuff;
                if (!I2CLib.ReadReg1Byt(slvAdr, RegAdr, out rBuff, CRC_enable)) { return; }

                if ((rBuff &= RegVal) == RegVal)                                   //写完再读，读完根据真实值自动跳转
                    UV_T0.SelectedIndex = 1;
                else if ((rBuff &= RegVal) == 0x00)
                    UV_T0.SelectedIndex = 0;
                else
                    return;
            }
        }

        private bool bit_chosen2(uint n, byte RegAdr, byte judge)
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
        private void ClearFault_Click(object sender, EventArgs e)
        {
            if (!I2CLib.WriteReg1Byt(slvAdr, 0x00, 0xFF, CRC_enable)) { return; }
        }

        private void chkNoStp_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void Reset_Click(object sender, EventArgs e)
        {
            CCREADY_CHOS.Text = "1";
            INTCBDONE.Text = "1";
            DEVXD.Text = "1";
            OVRDAL.Text = "1";
            OV.Text = "1";
            UV.Text = "1";
            OCD.Text = "1";
            SCD.Text = "1";
            CB1.Text = "0"; CB2.Text = "0"; CB3.Text = "0"; CB4.Text = "0"; CB5.Text = "0"; CB6.Text = "0";
            CB7.Text = "0"; CB8.Text = "0"; CB9.Text = "0"; CB10.Text = "0"; CB11.Text = "0"; CB12.Text = "0";
            CB13.Text = "0"; CB14.Text = "0"; CB15.Text = "0"; CB16.Text = "0"; CB17.Text = "0"; CB18.Text = "0";
            LOAD_P.Text = "0"; CB_ST1.Text = "0"; CB_ST0.Text = "1"; ADC_EN2.Text = "1"; TEMP_S.Text = "0"; CB_HOST.Text = "1"; SHUTB.Text = "0"; SHUTA.Text = "0";
            DLY_DIS.Text = "0"; CC_EN2.Text = "0"; CC_ONE.Text = "0"; RSVD1.Text = "0"; RSVD2.Text = "0"; RSVD3.Text = "0"; DSG_ON.Text = "0"; CHG_ON.Text = "0";
            RSNS.Text = "0"; RSVD4.Text = "0"; RSVD5.Text = "1"; SCD_D0.Text = "0"; SCD_D1.Text = "0"; SCD_T0.Text = "0"; SCD_T1.Text = "0"; SCD_T2.Text = "0";
            RSVD6.Text = "0"; OCD_D2.Text = "0"; OCD_D1.Text = "0"; OCD_D0.Text = "0"; OCD_T0.Text = "0"; OCD_T1.Text = "0"; OCD_T2.Text = "0"; OCD_T3.Text = "0";
            UV_D1.Text = "0"; UV_D0.Text = "0"; OV_D1.Text = "0"; OV_D0.Text = "0"; RSVD7.Text = "0"; RSVD8.Text = "0"; RSVD9.Text = "0"; RSVD10.Text = "0";
            OV_T7.Text = "1"; OV_T6.Text = "0"; OV_T5.Text = "1"; OV_T4.Text = "0"; OV_T3.Text = "0"; OV_T2.Text = "1"; OV_T1.Text = "1"; OV_T0.Text = "1";
            UV_T7.Text = "0"; UV_T6.Text = "1"; UV_T5.Text = "1"; UV_T4.Text = "0"; UV_T3.Text = "0"; UV_T2.Text = "0"; UV_T1.Text = "1"; UV_T0.Text = "1";
        }

        private void SupportCRC_CheckedChanged(object sender, EventArgs e)
        {
            CRC_enable = true;
        }

        private void donotSupportCRC_CheckedChanged(object sender, EventArgs e)
        {
            CRC_enable = false;
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void Printout_Click(object sender, EventArgs e)
        {

        }

        private void txtReadData2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SpCom1.PortName = comboBox1.Text;
            SpCom1.BaudRate = 9600;
            SpCom1.StopBits = StopBits.One;
            SpCom1.Parity = Parity.None;
            SpCom1.DataBits = 8;
            SpCom1.Open();
            SpCom1.DtrEnable = true;
            SpCom1.RtsEnable = Enabled;
            
            SpCom1.DataReceived += new SerialDataReceivedEventHandler(SpCom1_DataReceived);
            System.Threading.Thread.Sleep(50);
            SpCom1.WriteLine("*RST\r\n");
            System.Threading.Thread.Sleep(50);
            SpCom1.WriteLine("*CLS\r\n");
            System.Threading.Thread.Sleep(50);
            SpCom1.WriteLine("SYST:REM\r\n");

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(50);
            SpCom1.WriteLine("CONF:VOLT:DC 10,0.00001\r\n");
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(50);
            SpCom1.WriteLine("CONF:VOLT:DC 1,0.000001\r\n");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string str = "1";
            System.Threading.Thread.Sleep(50);
            SpCom1.WriteLine("READ?\r");
//            readBuffer = 0;

        }
        private void BackReadBuff (byte []BUFF)
        {
            string str = System.Text.Encoding.Default.GetString(BUFF);
            strrrrr = str.Replace("\r\n","");

        }


        private void SpCom1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            System.Threading.Thread.Sleep(50);
            //接收数据



            int len = SpCom1.BytesToRead;
            byte[] readBuffer = new byte[len];
            if (len > 0)
            {
                SpCom1.Read(readBuffer, 0, len);
                BackReadBuff(readBuffer);
            }
//            textBox2.Text = System.Text.Encoding.Default.GetString(readBuffer);
            
        }
        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            SpCom1.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Cell1_TEST_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton10_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}


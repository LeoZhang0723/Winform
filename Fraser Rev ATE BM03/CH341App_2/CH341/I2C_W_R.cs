using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ClsLib;

namespace CH341App
{
    public partial class I2C_W_R : Form
    {
        public static uint slvAdr;
        public bool isCall = false; 
        public bool IsCall { set { isCall = value; } }
        public static uint CRCnum = 0x107;

        public I2C_W_R()
        {
        InitializeComponent();
        
        nudSlvAddr.Value = 0x00;
        nudReadLen.Value = 1;
        txtWriteData.Text = "00";
        txtReadData.Text = "00";

        InitTipMsg();

        this.Load += new EventHandler(I2C_W_R_Load);
        }

        private void I2C_W_R_Load(object sender, EventArgs e)
        {
            UpdtScanAddr((byte)frmCH341_I2C.slvAdr);

        }
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
        private void InitTipMsg()
        {
            // 提示信息
            ToolTip tipMsg = new ToolTip();
            tipMsg.AutoPopDelay = 10000;
            tipMsg.InitialDelay = 1000;
            tipMsg.ReshowDelay = 500;
            tipMsg.ShowAlways = true;
           
         
    
            tipMsg.SetToolTip(this.btnI2CWrite,
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

            tipMsg.SetToolTip(this.nudSlvAddr,
               "Slave Address输入控件：\r\n" +
               "也就是当前操作地址输入控件。7位，不包括读写位。你可以输入0x00到0x7F\r\n" +
               "中任一个数字作为I2C地址。");

        }
        //载入文件数据到写字符串
        private void btnLodWrDat_Click(object sender, EventArgs e)
        {
            string[] strArr = new string[1];
            if (!CfgFilLib.ReCfgArr((uint)strArr.Length, ref strArr)) { return; }
            txtWriteData.Text = strArr[0];
        }
        // 写字符更改，需要格式化字符以方便示别
        private void txtWriteData_TextChanged(object sender, EventArgs e)
        {
            string str = txtWriteData.Text.Replace(" ", "");
            txtWrLen.Text = Convert.ToString((double)str.Length / 2);
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

                if (frmCH341_I2C.CRC_enable)
                {
                    if (wLn <= 1)
                    {
                        if (!I2CLib.WriteI2C(slvAdr, wLn, wArr, NoStop)) { return; }
                    }
                    else
                    {                 
                    byte[] wrArr2 = new byte[4];
                    byte[] wrArr3 = new byte[wLn * 2];
                    wrArr2[0] = (byte)(slvAdr << 1);
                    wrArr2[1] = wArr[0];
                    wrArr2[2] = wArr[1];
                    wrArr3[0] = wArr[0];
                    wrArr3[1] = wArr[1];
                    wrArr3[2] = (byte)CRCcalculate(wrArr2, 3);
                    for (int i = 2; i < 2 * wLn - 2; i = i + 2)
                    {
                        wrArr2[0] = wArr[(i + 2) / 2];
                        wrArr3[i + 2] = (byte)CRCcalculate(wrArr2, 1);
                        wrArr3[i + 1] = wArr[(i + 2) / 2];
                    }
                    if (!I2CLib.WriteI2C(slvAdr, (2 * wLn) - 1, wrArr3, NoStop)) { return; }
                    }
                }
                else
                {
                if (!I2CLib.WriteI2C(slvAdr, wLn, wArr, NoStop)) { return; }
                }
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
                byte[] rDat = new byte[2*rLn];
                byte[] rDat2 = new byte[rLn];
                if (frmCH341_I2C.CRC_enable)
                {
                    if (!I2CLib.ReadI2C(slvAdr, 2*rLn, ref rDat)) { return; }

                    byte[] reRegplusDat = new byte[2];
                    reRegplusDat[0] = (byte)((slvAdr << 1) | 0x01);
                    reRegplusDat[1] = rDat[0];
                    if (rDat[1] == (byte)CRCcalculate(reRegplusDat, 2))
                        rDat2[0] = rDat[0];
                    else
                    {
                        if (!ErrHdl.ErrorHandle(ErrHdl.ErrEnum.CRCWrong, null)) { rDat2[0] = 0; return; }
                        return;
                    }
                    for (uint i = 0; i < 2 * rLn - 2; i = i + 2)
                    {
                        reRegplusDat[0] = rDat[i + 2];
                        reRegplusDat[1] = 0;
                        if (rDat[i + 3] == (byte)CRCcalculate(reRegplusDat, 1))
                            rDat2[(i + 2) / 2] = rDat[i + 2];
                        else
                        {
                            if (!ErrHdl.ErrorHandle(ErrHdl.ErrEnum.CRCWrong, null)) { rDat2[(i + 2) / 2] = 0; return; }                          
                            return;
                        }
                    }
                }


                else
                {
                    if (!I2CLib.ReadI2C(slvAdr, rLn, ref rDat2)) { return; }
                }

                string str = "";
                for (int i = 0; i < rLn; i++)
                {
                    str = str + string.Format("{0:X2}", rDat2[i]);
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

                byte[] rBuff = new byte[2 * rLn];
                byte[] rDat2 = new byte[rLn]; 
                if (frmCH341_I2C.CRC_enable)
                {
                    if (wLn <= 1)
                    {
                        if (!I2CLib.StreamI2C(slvAdr, wLn, wDat, 2 * rLn, ref rBuff)) { return; }
                    }
                    else
                    { 
                          byte[] wrArr2 = new byte[4];
                          byte[] wrArr3 = new byte[wLn * 2];
                          wrArr2[0] = (byte)(slvAdr << 1);
                          wrArr2[1] = wDat[0];
                          wrArr2[2] = wDat[1];
                          wrArr3[0] = wDat[0];
                          wrArr3[1] = wDat[1];
                          wrArr3[2] = (byte)CRCcalculate(wrArr2, 3);
                          for (int i = 2; i < 2 * wLn - 2; i = i + 2)
                       {
                             wrArr2[0] = wDat[(i + 2) / 2];
                             wrArr3[i + 2] = (byte)CRCcalculate(wrArr2, 1);
                             wrArr3[i + 1] = wDat[(i + 2) / 2];
                        }
                        if (!I2CLib.StreamI2C(slvAdr, (2 * wLn) - 1, wrArr3, 2 * rLn, ref rBuff)) { return; }
                    }
                    

                    byte[] reRegplusDat = new byte[2];
                    reRegplusDat[0] = (byte)((slvAdr << 1) | 0x01);
                    reRegplusDat[1] = rBuff[0];
                    if (rBuff[1] == (byte)CRCcalculate(reRegplusDat, 2))
                        rDat2[0] = rBuff[0];
                    else
                    {
                        if (!ErrHdl.ErrorHandle(ErrHdl.ErrEnum.CRCWrong, null)) { rDat2[0] = 0; return; }
                        return;
                    }
                    for (uint i = 0; i < 2 * rLn - 2; i = i + 2)
                    {
                        reRegplusDat[0] = rBuff[i + 2];
                        reRegplusDat[1] = 0;
                        if (rBuff[i + 3] == (byte)CRCcalculate(reRegplusDat, 1))
                            rDat2[(i + 2) / 2] = rBuff[i + 2];
                        else
                        {
                            if (!ErrHdl.ErrorHandle(ErrHdl.ErrEnum.CRCWrong, null)) { rDat2[(i + 2) / 2] = 0; return; }
                            return;
                        }
                    }
                }
                else
                {
                    if (!I2CLib.StreamI2C(slvAdr, wLn, wDat, rLn, ref rDat2)) { return; }
                }

                str = "";
                for (int i = 0; i < rLn; i++)
                {
                    str = str + string.Format("{0:X2}", rDat2[i]);
                    if (i > 0 & i % 2 != 0) str = str + " ";
                }
                txtReadData.Text = str;
            }
            finally
            {
                btnI2CStm.Enabled = true;
            }
        }
        //存储读字符串数据到文件
        private void btnSavReDat_Click(object sender, EventArgs e)
        {
            string[] strArr = new string[1];
            strArr[0] = txtReadData.Text;
            if (!CfgFilLib.WrCfgArr((uint)strArr.Length, strArr)) { return; }
        }


        private void txtReadData_TextChanged(object sender, EventArgs e)
        {

        }

        private void nudReadLen_ValueChanged(object sender, EventArgs e)
        {

        }
        // 扫描地址时实时更新当前地址,起显示的作用
        private void UpdtScanAddr(byte CurrAddr)
        {
            nudSlvAddr.Value = CurrAddr;
            nudSlvAddr.Update();
        }
        // 从地址变更
        private void nudSlvAddr_ValueChanged(object sender, EventArgs e)
        {
            slvAdr = (uint)nudSlvAddr.Value;
        }


        private uint CRCcalculate(byte[] WrCRCSingle, uint Len)
        {
            byte[] WrCRCSingle2=new byte[4];
            for (uint i = 0; i < Len; i++)
            {
                WrCRCSingle2[Len - i] = WrCRCSingle[i];
            }

            WrCRCSingle2[0] = 0;

            switch (Len)
            {
                case 1:
                    {
                        WrCRCSingle2[2] = 0;
                        WrCRCSingle2[3] = 0;
                        break;
                    }
                case 2:
                    {
                        WrCRCSingle2[3] = 0;
                        break;
                    }
                default:
                    break;
            }

                    uint CRCWrSingle = BitConverter.ToUInt32(WrCRCSingle2, 0);

            uint CRCtemp = 0;

            if ((CRCWrSingle >> 31) == 1) { CRCtemp = (CRCWrSingle >> 23) ^ CRCnum; CRCWrSingle &= 0x7FFFFF; CRCWrSingle = ((CRCtemp << 23) | CRCWrSingle); }

            if (CRCWrSingle >> 30 == 1) { CRCtemp = ((CRCWrSingle >> 22) & 0x1FF) ^ CRCnum; CRCWrSingle &= 0x3FFFFF; CRCWrSingle = ((CRCtemp << 22) | CRCWrSingle); }

            if (CRCWrSingle >> 29 == 1) { CRCtemp = ((CRCWrSingle >> 21) & 0x1FF) ^ CRCnum; CRCWrSingle &= 0x1FFFFF; CRCWrSingle = ((CRCtemp << 21) | CRCWrSingle); }

            if (CRCWrSingle >> 28 == 1) { CRCtemp = ((CRCWrSingle >> 20) & 0x1FF) ^ CRCnum; CRCWrSingle &= 0xFFFFF; CRCWrSingle = ((CRCtemp << 20) | CRCWrSingle); }

            if (CRCWrSingle >> 27 == 1) { CRCtemp = ((CRCWrSingle >> 19) & 0x1FF) ^ CRCnum; CRCWrSingle &= 0x7FFFF; CRCWrSingle = ((CRCtemp << 19) | CRCWrSingle); }

            if (CRCWrSingle >> 26 == 1) { CRCtemp = ((CRCWrSingle >> 18) & 0x1FF) ^ CRCnum; CRCWrSingle &= 0x3FFFF; CRCWrSingle = ((CRCtemp << 18) | CRCWrSingle); }

            if (CRCWrSingle >> 25 == 1) { CRCtemp = ((CRCWrSingle >> 17) & 0x1FF) ^ CRCnum; CRCWrSingle &= 0x1FFFF; CRCWrSingle = ((CRCtemp << 17) | CRCWrSingle); }

            if (CRCWrSingle >> 24 == 1) { CRCtemp = ((CRCWrSingle >> 16) & 0x1FF) ^ CRCnum; CRCWrSingle &= 0xFFFF; CRCWrSingle = ((CRCtemp << 16) | CRCWrSingle); }

            if (CRCWrSingle >> 23 == 1) { CRCtemp = ((CRCWrSingle >> 15) & 0x1FF) ^ CRCnum; CRCWrSingle &= 0x7FFF; CRCWrSingle = ((CRCtemp << 15) | CRCWrSingle); }

            if (CRCWrSingle >> 22 == 1) { CRCtemp = ((CRCWrSingle >> 14) & 0x1FF) ^ CRCnum; CRCWrSingle &= 0x3FFF; CRCWrSingle = ((CRCtemp << 14) | CRCWrSingle); }

            if (CRCWrSingle >> 21 == 1) { CRCtemp = ((CRCWrSingle >> 13) & 0x1FF) ^ CRCnum; CRCWrSingle &= 0x1FFF; CRCWrSingle = ((CRCtemp << 13) | CRCWrSingle); }

            if (CRCWrSingle >> 20 == 1) { CRCtemp = ((CRCWrSingle >> 12) & 0x1FF) ^ CRCnum; CRCWrSingle &= 0xFFF; CRCWrSingle = ((CRCtemp << 12) | CRCWrSingle); }

            if (CRCWrSingle >> 19 == 1) { CRCtemp = ((CRCWrSingle >> 11) & 0x1FF) ^ CRCnum; CRCWrSingle &= 0x7FF; CRCWrSingle = ((CRCtemp << 11) | CRCWrSingle); }

            if (CRCWrSingle >> 18 == 1) { CRCtemp = ((CRCWrSingle >> 10) & 0x1FF) ^ CRCnum; CRCWrSingle &= 0x3FF; CRCWrSingle = ((CRCtemp << 10) | CRCWrSingle); }

            if (CRCWrSingle >> 17 == 1) { CRCtemp = ((CRCWrSingle >> 9) & 0x1FF) ^ CRCnum; CRCWrSingle &= 0x1FF; CRCWrSingle = ((CRCtemp << 9) | CRCWrSingle); }

            if (CRCWrSingle >> 16 == 1) { CRCtemp = ((CRCWrSingle >> 8) & 0x1FF) ^ CRCnum; CRCWrSingle &= 0xFF; CRCWrSingle = ((CRCtemp << 8) | CRCWrSingle); }

            if (CRCWrSingle >> 15 == 1) { CRCtemp = ((CRCWrSingle >> 7) & 0x1FF) ^ CRCnum; CRCWrSingle &= 0x7F; CRCWrSingle = ((CRCtemp << 7) | CRCWrSingle); }

            if (CRCWrSingle >> 14 == 1) { CRCtemp = ((CRCWrSingle >> 6) & 0x1FF) ^ CRCnum; CRCWrSingle &= 0x3F; CRCWrSingle = ((CRCtemp << 6) | CRCWrSingle); }

            if (CRCWrSingle >> 13 == 1) { CRCtemp = ((CRCWrSingle >> 5) & 0x1FF) ^ CRCnum; CRCWrSingle &= 0x1F; CRCWrSingle = ((CRCtemp << 5) | CRCWrSingle); }

            if (CRCWrSingle >> 12 == 1) { CRCtemp = ((CRCWrSingle >> 4) & 0x1FF) ^ CRCnum; CRCWrSingle &= 0xF; CRCWrSingle = ((CRCtemp << 4) | CRCWrSingle); }

            if (CRCWrSingle >> 11 == 1) { CRCtemp = ((CRCWrSingle >> 3) & 0x1FF) ^ CRCnum; CRCWrSingle &= 0x7; CRCWrSingle = ((CRCtemp << 3) | CRCWrSingle); }

            if (CRCWrSingle >> 10 == 1) { CRCtemp = ((CRCWrSingle >> 2) & 0x1FF) ^ CRCnum; CRCWrSingle &= 0x3; CRCWrSingle = ((CRCtemp << 2) | CRCWrSingle); }

            if (CRCWrSingle >> 9 == 1) { CRCtemp = ((CRCWrSingle >> 1) & 0x1FF) ^ CRCnum; CRCWrSingle &= 0x1; CRCWrSingle = ((CRCtemp << 1) | CRCWrSingle); }

            if (CRCWrSingle >> 8 == 1) { CRCtemp = ((CRCWrSingle >> 0) & 0x1FF) ^ CRCnum; CRCWrSingle &= 0x0; CRCWrSingle = ((CRCtemp << 0) | CRCWrSingle); }

            return CRCWrSingle;
        }
    }
}

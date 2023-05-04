using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ClsLib;
//******************************************************************************************************
//  异步串口		            打印口		        并口		        同步串口			
//  引脚号		    引脚名称	引脚号	引脚名称	引脚号	引脚名称	引脚号		    	
//  341A    341T		        341A		        341A		        341A	341H	引脚名称   位定义
//  3	    无	    IN7	        3	    SIN#	    3	    AS#				                        D15
//  4	    2	    ROV#	    4	    AFD#	    4	    DS#				                        D14
//  5	    3	    TXD 	    5	    ERR#	    5	    ERR#	    5	    无	    GPIO	    D8
//  6	    4	    RXD 	    6	    PEMP	    6	    PEMP	    6	    无	    GPIO	    D9
//  7	    5	    INT#	    7	    ACK#	    7	    INT#	    7	    3	    INT#	    D10
//  8	    无	    IN3	        8	    SLCT	    8	    SLCT	    8	    无	    GPIO	    D11
//  15	    无	    CTS#	    15	    D0  	    15	    D0	        15	    10	    CS0	        D0
//  16	    无	    DSR#	    16	    D1  	    16	    D1	        16	    11	    CS1	        D1
//  17	    无	    RI#	        17	    D2  	    17	    D2	        17	    12	    CS2	        D2
//  18	    无	    DCD#	    18	    D3  	    18	    D3	        18	    13	    DCK(SCK)	D3
//  19	    无	    OUT#	    19	    D4  	    19	    D4	        19	    14	    DOUT2	    D4
//  20	    无	    DTR#	    20	    D5  	    20	    D5	        20	    15	    DOUT(MOSI)	D5
//  21	    无	    RTS#	    21	    D6  	    21	    D6	        21	    16	    DIN2	    D6
//  22	    14	    SLP#	    22	    D7  	    22	    D7	        22	    17	    DIN(MISO)	D7
//                                                                      23	    无	    SDA	        D19
//                                                                      24	    无	    SCL	        D18
//  25  	17	    RDY#	    25	    STB#	    25	    WR#				                        D17
//  26	    18	    TNOW	    26	    INI#	    26	    RST#	    26	    19	    RST#	    D16
//  27	    19	    TEN#	    27	    BUSY	    27	    WAIT#				                    D13
//******************************************************************************************************
//标准的公共引脚			
//引脚号			    引脚名称
//341A	341T	341H	
//1	    1	    1	    ACT#
//2	    无	    2	    RSTI
//9	    6	    4	    V3
//10	7	    5	    UD+
//11	8	    6	    UD-
//12	11，12	7，18	GND
//13	9	    8	    XI
//14	10	    9	    XO
//23	15	    无	    SDA
//24	16	    无	    SCL
//28	13，20	20	    VCC
//******************************************************************************************************
namespace CH341App
{
    public partial class frmCH341_GPIO : Form
    {
        Timer tmrHdl = new Timer();

        // 主程序
        public frmCH341_GPIO()
        {
            InitializeComponent();
            // 初始化应用程序

            string revInf = "Rev 3.0";
            string dataInf = System.IO.File.GetLastWriteTime(this.GetType().Assembly.Location).ToString("yyyyMMdd");//yyyy/MM/dd HH:mm:ss
            lblInfo.Text = revInf + " " + dataInf;

            CH341EX.SetByte0Direction(0, 0x00);
            CH341EX.SetByte1Direction(0, 0x00000000);
            CH341EX.SetByte0Output(0, 0x00);
            CH341EX.SetByte1Output(0, 0x00000000);
            CH341EX.SetByte2Output(0, 0x00FF0000);

            chkOutD23.Checked = true;
            chkOutD22.Checked = true;
            chkOutD21.Checked = true;
            chkOutD20.Checked = true;
            chkOutD19.Checked = true;
            chkOutD18.Checked = true;
            chkOutD17.Checked = true;

            chkOutD16.Checked = true;
            chkOutD15.Checked = true;
            chkOutD14.Checked = true;
            chkOutD13.Checked = true;
            chkOutD12.Checked = true;
            chkOutD11.Checked = true;
            chkOutD10.Checked = true;
            chkOutD9.Checked = true;
            chkOutD8.Checked = true;

            chkOutD7.Checked = true;
            chkOutD6.Checked = true;
            chkOutD5.Checked = true;
            chkOutD4.Checked = true;
            chkOutD3.Checked = true;
            chkOutD2.Checked = true;
            chkOutD1.Checked = true;
            chkOutD0.Checked = true;

            chkDirD23.Checked = true;
            chkDirD22.Checked = true;
            chkDirD21.Checked = true;
            chkDirD20.Checked = true;
            chkDirD19.Checked = true;
            chkDirD18.Checked = true;
            chkDirD17.Checked = true;
            chkDirD16.Checked = true;

            chkDirD23.Enabled = false;
            chkDirD22.Enabled = false;
            chkDirD21.Enabled = false;
            chkDirD20.Enabled = false;
            chkDirD19.Enabled = false;
            chkDirD18.Enabled = false;
            chkDirD17.Enabled = false;
            chkDirD16.Enabled = false;

            chkOutD15.Enabled = false;
            chkOutD14.Enabled = false;
            chkOutD13.Enabled = false;
            chkOutD12.Enabled = false;
            chkOutD11.Enabled = false;
            chkOutD10.Enabled = false;
            chkOutD9.Enabled = false;
            chkOutD8.Enabled = false;

            chkOutD7.Enabled = false;
            chkOutD6.Enabled = false;
            chkOutD5.Enabled = false;
            chkOutD4.Enabled = false;
            chkOutD3.Enabled = false;
            chkOutD2.Enabled = false;
            chkOutD1.Enabled = false;
            chkOutD0.Enabled = false;

            tmrHdl.Interval = 100;
            tmrHdl.Tick += tmrHdl_Tick;
            tmrHdl.Start();
        }

        // 初始化显示
        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        // 隐藏介面，
        private void frmGPIOCtl_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!Modal)
            {
                this.Hide();
                e.Cancel = true; // 屏蔽接下来的Close功能调用。
            }
        }

        private void chkOutB7B0_CheckedChanged(object sender, EventArgs e)
        {
            byte b = 0;
            if (chkOutD7.Checked) { b |= 0x80; }
            if (chkOutD6.Checked) { b |= 0x40; }
            if (chkOutD5.Checked) { b |= 0x20; }
            if (chkOutD4.Checked) { b |= 0x10; }
            if (chkOutD3.Checked) { b |= 0x08; }
            if (chkOutD2.Checked) { b |= 0x04; }
            if (chkOutD1.Checked) { b |= 0x02; }
            if (chkOutD0.Checked) { b |= 0x01; }
            CH341EX.SetByte0Output(0, b);
        }

        private void chkOutB15B8_CheckedChanged(object sender, EventArgs e)
        {
            uint p = 0;
            if (chkOutD15.Checked) { p |= 0x00008000; }
            if (chkOutD14.Checked) { p |= 0x00004000; }
            if (chkOutD13.Checked) { p |= 0x00002000; }
            if (chkOutD12.Checked) { p |= 0x00001000; }
            if (chkOutD11.Checked) { p |= 0x00000800; }
            if (chkOutD10.Checked) { p |= 0x00000400; }
            if (chkOutD9.Checked) { p |= 0x00000200; }
            if (chkOutD8.Checked) { p |= 0x00000100; }
            CH341EX.SetByte1Output(0, p);
        }

        private void chkOutB23B16_CheckedChanged(object sender, EventArgs e)
        {
            uint p = 0;
            if (chkOutD23.Checked) { p |= 0x00800000; }
            if (chkOutD22.Checked) { p |= 0x00400000; }
            if (chkOutD21.Checked) { p |= 0x00200000; }
            if (chkOutD20.Checked) { p |= 0x00100000; }
            if (chkOutD19.Checked) { p |= 0x00080000; }
            if (chkOutD18.Checked) { p |= 0x00040000; }
            if (chkOutD17.Checked) { p |= 0x00020000; }
            if (chkOutD16.Checked) { p |= 0x00010000; }
            CH341EX.SetByte2Output(0, p);
        }

        private void chkDirB7B0_CheckedChanged(object sender, EventArgs e)
        {
            byte b = 0;
            if (chkDirD7.Checked) { b |= 0x00000080; }
            if (chkDirD6.Checked) { b |= 0x00000040; }
            if (chkDirD5.Checked) { b |= 0x00000020; }
            if (chkDirD4.Checked) { b |= 0x00000010; }
            if (chkDirD3.Checked) { b |= 0x00000008; }
            if (chkDirD2.Checked) { b |= 0x00000004; }
            if (chkDirD1.Checked) { b |= 0x00000002; }
            if (chkDirD0.Checked) { b |= 0x00000001; }
            CH341EX.SetByte0Direction(0, b);
            chkOutD7.Enabled = chkDirD7.Checked;
            chkOutD6.Enabled = chkDirD6.Checked;
            chkOutD5.Enabled = chkDirD5.Checked;
            chkOutD4.Enabled = chkDirD4.Checked;
            chkOutD3.Enabled = chkDirD3.Checked;
            chkOutD2.Enabled = chkDirD2.Checked;
            chkOutD1.Enabled = chkDirD1.Checked;
            chkOutD0.Enabled = chkDirD0.Checked;
        }

        private void chkDirB15B8_CheckedChanged(object sender, EventArgs e)
        {
            uint p = 0;
            if (chkDirD15.Checked) { p |= 0x00008000; }
            if (chkDirD14.Checked) { p |= 0x00004000; }
            if (chkDirD13.Checked) { p |= 0x00002000; }
            if (chkDirD12.Checked) { p |= 0x00001000; }
            if (chkDirD11.Checked) { p |= 0x00000800; }
            if (chkDirD10.Checked) { p |= 0x00000400; }
            if (chkDirD9.Checked) { p |= 0x00000200; }
            if (chkDirD8.Checked) { p |= 0x00000100; }
            CH341EX.SetByte1Direction(0, p);
            chkOutD15.Enabled = chkDirD15.Checked;
            chkOutD14.Enabled = chkDirD14.Checked;
            chkOutD13.Enabled = chkDirD13.Checked;
            chkOutD12.Enabled = chkDirD12.Checked;
            chkOutD11.Enabled = chkDirD11.Checked;
            chkOutD10.Enabled = chkDirD10.Checked;
            chkOutD9.Enabled = chkDirD9.Checked;
            chkOutD8.Enabled = chkDirD8.Checked;
        }
        private void chkDirB23B16_CheckedChanged(object sender, EventArgs e)
        {
            // 不支持此特性
        }
        void tmrHdl_Tick(object sender, EventArgs e)
        {
            uint p;
            //CH341DLL.CH341GetInput(0, out p);
            //CH341DLL.CH341GetStatus(0, out p);
            CH341EX.CH341GetLevel(0, out p);
            chkInD23.Checked = (p & 0x00800000) > 0;
            chkInD22.Checked = (p & 0x00400000) > 0;
            chkInD21.Checked = (p & 0x00200000) > 0;
            chkInD20.Checked = (p & 0x00100000) > 0;
            chkInD19.Checked = (p & 0x00080000) > 0;
            chkInD18.Checked = (p & 0x00040000) > 0;
            chkInD17.Checked = (p & 0x00020000) > 0;
            chkInD16.Checked = (p & 0x00010000) > 0;
            chkInD15.Checked = (p & 0x00008000) > 0;
            chkInD14.Checked = (p & 0x00004000) > 0;
            chkInD13.Checked = (p & 0x00002000) > 0;
            chkInD12.Checked = (p & 0x00001000) > 0;
            chkInD11.Checked = (p & 0x00000800) > 0;
            chkInD10.Checked = (p & 0x00000400) > 0;
            chkInD9.Checked = (p & 0x00000200) > 0;
            chkInD8.Checked = (p & 0x00000100) > 0;
            chkInD7.Checked = (p & 0x00000080) > 0;
            chkInD6.Checked = (p & 0x00000040) > 0;
            chkInD5.Checked = (p & 0x00000020) > 0;
            chkInD4.Checked = (p & 0x00000010) > 0;
            chkInD3.Checked = (p & 0x00000008) > 0;
            chkInD2.Checked = (p & 0x00000004) > 0;
            chkInD1.Checked = (p & 0x00000002) > 0;
            chkInD0.Checked = (p & 0x00000001) > 0;
        }




    }
}


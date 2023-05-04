//***********************************************************************************************
// 串口公共应用子程序
//***********************************************************************************************
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using System.IO.Ports;

namespace ClsLib
{
    public static class COMLib
    {
        public enum eBaudRate { _10, _300, _600, _1200, _2400, _4800, _9600, _14400, _19200, _38400, _56000, _57600, _115200 };
        public enum eDataBits { _5, _6, _7, _8 };
        private static SerialPort com = new SerialPort();
        public static bool IsOpen { get { return com.IsOpen; } }
        private static bool ri=false ;
        public static bool RI { get { return ri; } }
        //
        public static bool GetPortNames(out string[] spList)
        {
            try
            {
                spList = System.IO.Ports.SerialPort.GetPortNames();
                return true;
            }
            catch (Exception ex)
            {
                spList = null;
                MessageBox.Show("操作端口出现错误：" + ex.Message);
                return false;
            }
        }
        //
        public static bool Open(string iPortName, eBaudRate iBaudRate, eDataBits iDataBits, StopBits iStopBits, Parity iParity, int iReadTimeout, int iWriteTimeout)
        {
            try
            {
                if (com.IsOpen)
                {
                    com.Close();
                }
                com.PortName = iPortName;
                com.BaudRate = GetBaudRate(iBaudRate);
                com.DataBits = GetDataBits(iDataBits);
                com.StopBits = iStopBits;
                com.Parity = iParity;
                com.ReadTimeout = iReadTimeout;
                com.WriteTimeout = iWriteTimeout;
                com.Open();//打开串口，如果出错则不会执行下面的代码。
                com.PinChanged += new SerialPinChangedEventHandler(SerialPort_PinChanged);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("打开端口不存在或被其它应用程序占用：" + ex.Message);
                return false;
            }
        }
        //
        public static void Close()
        {
            try
            {
                if (com.IsOpen)
                {
                    com.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作端口出现错误：" + ex.Message);
                return;
            }
        }
        public static void SerialPort_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            if (e.EventType == SerialPinChange.Ring)
            {
                ri = !ri;
            }
        }
        //
        public static bool SetBaudRate(eBaudRate iBaudRate)
        {
            try
            {
                if (!com.IsOpen)
                {
                    return false;
                }
                com.BaudRate = GetBaudRate(iBaudRate);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作端口出现错误：" + ex.Message);
                return false;
            }
        }
        //
        public static bool SetDataBits(eDataBits iDataBits)
        {
            try
            {
                if (!com.IsOpen)
                {
                    return false;
                }
                com.DataBits = GetDataBits(iDataBits);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作端口出现错误：" + ex.Message);
                return false;
            }
        }
        //
        public static bool SetStopBits(StopBits iStopBits)
        {
            try
            {
                if (!com.IsOpen)
                {
                    return false;
                }
                com.StopBits = iStopBits;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作端口出现错误：" + ex.Message);
                return false;
            }
        }
        //
        public static bool SetParity(Parity iParity)
        {
            try
            {
                if (!com.IsOpen)
                {
                    return false;
                }
                com.Parity = iParity;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作端口出现错误：" + ex.Message);
                return false;
            }
        }
        //sets the number of milliseconds before a time-out occurs when a write operation does not finish.
        public static bool SetWriteTimeout(int iTimeout)
        {
            try
            {
                if (!com.IsOpen)
                {
                    return false;
                }
                com.WriteTimeout = iTimeout;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作端口出现错误：" + ex.Message);
                return false;
            }
        }
        //sets the number of milliseconds before a time-out occurs when a read operation does not finish.
        public static bool SetReadTimeout(int iTimeout)
        {
            try
            {
                if (!com.IsOpen)
                {
                    return false;
                }
                com.ReadTimeout = iTimeout;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作端口出现错误：" + ex.Message);
                return false;
            }
        }
        // Write Text with 0x0A end
        public static bool WriteText(string iText)
        {
            try
            {
                if (!com.IsOpen)
                {
                    return false;
                }
                com.Write(iText);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作端口出现错误：" + ex.Message);
                return false;
            }
        }
        // Write Byte Array
        public static bool WriteByteArray(byte[] iBuffer, int iCount)
        {
            try
            {
                if (!com.IsOpen)
                {
                    return false;
                }
                com.Write(iBuffer, 0, iCount);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作端口出现错误：" + ex.Message);
                return false;
            }
        }
        //
        public static int GetBytesToRead()
        {
            try
            {
                if (!com.IsOpen)
                {
                    return 0;
                }
                return com.BytesToRead;
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作端口出现错误：" + ex.Message);
                return 0;
            }
        }
        //
        public static bool ClearReadBuffer()
        {
            try
            {
                if (!com.IsOpen)
                {
                    return false;
                }
                if (com.BytesToRead <= 0)
                {
                    return false;
                }
                com.ReadExisting();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作端口出现错误：" + ex.Message);
                return false;
            }
        }
        // Read Text with 0x0A end
        public static bool ReadText(ref string oText)
        {
            try
            {
                if (!com.IsOpen)
                {
                    return false;
                }
                if (com.BytesToRead <= 0)
                {
                    return false;
                }
                oText = com.ReadExisting();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作端口出现错误：" + ex.Message);
                return false;
            }
        }
        // Read Byte Array
        public static bool ReadByteArray(ref byte[] iBuffer, ref int iCount)
        {
            try
            {
                if (!com.IsOpen)
                {
                    return false;
                }
                if (com.BytesToRead <= 0)
                {
                    return false;
                }
                iCount = com.BytesToRead;
                iBuffer = new byte[iCount];
                int c = com.Read(iBuffer, 0, iCount);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作端口出现错误：" + ex.Message);
                return false;
            }
        }
        // 
        public static bool SetDtrEnable(bool Enable)
        {
            try
            {
                if (!com.IsOpen)
                {
                    return false;
                }
                com.DtrEnable = Enable;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作端口出现错误：" + ex.Message);
                return false;
            }
        }
        // 
        public static bool SetRtsEnable(bool Enable)
        {
            try
            {
                if (!com.IsOpen)
                {
                    return false;
                }
                com.RtsEnable = Enable;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作端口出现错误：" + ex.Message);
                return false;
            }
        }
        // 
        public static bool SetBreakState(bool Enable)
        {
            try
            {
                if (!com.IsOpen)
                {
                    return false;
                }
                com.BreakState = Enable;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作端口出现错误：" + ex.Message);
                return false;
            }
        }
        //
        public static bool GetCtsHolding(ref bool State)
        {
            try
            {
                if (!com.IsOpen)
                {
                    return false;
                }
                State = com.CtsHolding;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作端口出现错误：" + ex.Message);
                return false;
            }
        }
        //
        public static bool GetCDHolding(ref bool State)
        {
            try
            {
                if (!com.IsOpen)
                {
                    return false;
                }
                State = com.CDHolding;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作端口出现错误：" + ex.Message);
                return false;
            }
        }
        //
        public static bool GetDsrHolding(ref bool State)
        {
            try
            {
                if (!com.IsOpen)
                {
                    return false;
                }
                State = com.DsrHolding;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("操作端口出现错误：" + ex.Message);
                return false;
            }
        }
        //
        private static int GetBaudRate(eBaudRate iBaudRate)
        {
            switch (iBaudRate)
            {
                case eBaudRate._10: return 10;
                case eBaudRate._300: return 300;
                case eBaudRate._600: return 600;
                case eBaudRate._1200: return 1200;
                case eBaudRate._2400: return 2400;
                case eBaudRate._4800: return 4800;
                case eBaudRate._9600: return 9600;
                case eBaudRate._14400: return 14400;
                case eBaudRate._19200: return 19200;
                case eBaudRate._38400: return 38400;
                case eBaudRate._56000: return 56000;
                case eBaudRate._57600: return 57600;
                case eBaudRate._115200: return 115200;
                default: return 9600;
            }
        }
        //
        private static int GetDataBits(eDataBits iDataBits)
        {
            switch (iDataBits)
            {
                case eDataBits._5: return 5;
                case eDataBits._6: return 6;
                case eDataBits._7: return 7;
                case eDataBits._8: return 8;
                default: return 8;
            }
        }
    }
}

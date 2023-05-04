//***********************************************************************************************
// I2C公共应用子程序
//***********************************************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ClsLib
{
    public static class I2CLib
    {
        public enum eDvcType { LPT = 0, CH341 = 1, CY7C68013A = 2, }
        public enum eTranMode { NoTips = 0, StepMsg = 1, }
        public enum eBitRate { _20K = 0, _100K = 1, _400K = 2/*, _750K = 3,*/ }
        public enum eEepromType { ID_24C01, ID_24C02, ID_24C04, ID_24C08, ID_24C16, ID_24C32, ID_24C64, ID_24C128, ID_24C256, ID_24C512, ID_24C1024, ID_24C2048, ID_24C4096, };

        public delegate void UptScanAddr(byte CurrAddr);
        public static UptScanAddr ScanAddrEvent;

        private static eDvcType dvcTyp; // 驱动器硬件类型。
        private static uint numPort; // 端口号，初始化寄存器，简化I2C服务函数参数，多介面共享。
        private static eBitRate bitRate; // 定义比特率全局变量，为相关控件提供初始化值。
        private static eTranMode tranMode; // 传送模式寄存器，一般只需要初始化一次。 
        private static bool opened;

        private static uint streamMode = 0;
        public static eDvcType DvcTyp { get { return dvcTyp; } /*set { dvcTyp = value; }*/ }
        public static uint NumPort { get { return numPort; } /*set { numPort = value; }*/ }
        public static eBitRate BitRate { get { return bitRate; } /*set { numPort = bitRate; }*/ }
        public static eTranMode TranMode { get { return tranMode; } /*set { tranMode = value; }*/ }
        public static bool IsOpened { get { return opened; } /*set { iniStat = value; }*/ }

        // 驱动器拔插消息
        private static void USBIO_NOTIFY_ROUTINE(uint iEventStatus)
        {
            if (iEventStatus == CH341DLL.CH341_DEVICE_REMOVE)
            {
                CloseDevice();
                ErrHdl.ErrorHandle(ErrHdl.ErrEnum.RmvEqu, null);
                Environment.Exit(0);
            }
            if (iEventStatus == CH341DLL.CH341_DEVICE_ARRIVAL)
            {
                //MessageBox.Show("Detecting the communication equipment.");
            }
        }
        // 初始化设备
        public static bool OpenDevice(eDvcType iDvcTyp, uint iNumPort) // 初始化驱动设备
        {
            CH341EX.usbio_notify += USBIO_NOTIFY_ROUTINE;
            dvcTyp = iDvcTyp;
            numPort = iNumPort;
            if (!CH341EX.InitialDevice(iNumPort)) { return false; }
            opened = true; // 初始化成功
            return true;
        }
        // 关闭I2C设备
        public static bool CloseDevice() // 初始化驱动设备     
        {
            CH341EX.usbio_notify -= USBIO_NOTIFY_ROUTINE;
            CH341DLL.CH341CloseDevice(numPort);
            opened = false; // 关闭成功
            return true;
        }
        // 设置I2C通信速率（Hz）                                                            根据下拉表选择00/01/10/11，将其赋值给streammode，然后给出最后的工作频率
        public static bool SetBitRate(uint iBitRate)
        {
            eBitRate BitRate;
//            if (iBitRate > 400 * 1000) { BitRate = eBitRate._750K; }
            if (iBitRate > 100 * 1000) { BitRate = eBitRate._400K; }
            else if (iBitRate > 20 * 1000) { BitRate = eBitRate._100K; }
            else /*if (iBitRate > 0)*/ { BitRate = eBitRate._20K; }
            return SetBitRate(BitRate);
        }
        // 设置I2C通信速率，传入参数为枚举量
        public static bool SetBitRate(eBitRate iBitRate)
        {
            bitRate = iBitRate;
          //streamMode = streamMode & 0xFEU;
            streamMode = streamMode | (uint) iBitRate;                          
            if (!CH341DLL.CH341SetStream(numPort, streamMode)) return false;
            return true;
        }
        // 设置转送模式
        public static bool SetTranMode(eTranMode iTranMode)
        {
            tranMode = iTranMode;
            return true;
        }
        // 扫描从机地址
        public static bool ScanSlvAddr(out List<byte> oAckList)
        {
            bool ackStat;
            oAckList = new List<byte>();
            for (int i = 0; i < 128; i++) // 7位地址，从0到127，不管有没有收到回复，完成128位寻址
            {
                ScanAddrEvent((byte)i);
                if (!CH341EX.CheckAddr(numPort, (uint)i, out ackStat)) { return false; }// 检查ACK／NACK
                if (ackStat)
                {
                    oAckList.Add((byte)i);
                }
                System.Threading.Thread.Sleep(1);
            }
            return true;
        }
        public static bool CheckAddr(uint iChkAddr, out bool oAckStat)
        {
            CH341EX.CheckAddr(numPort, iChkAddr, out oAckStat);
            return true;
        }
        // 写I2C，简化参数
        public static bool WriteI2C(uint iSlvAdr, uint iWrLen, byte[] iWrArr, bool iNoStop)
        {
            switch (tranMode)
            {
                case eTranMode.NoTips:
                    uint wrLen = iWrLen + 1;
                    byte[] wrBuf = new byte[wrLen];
                    wrBuf[0] = (byte)(iSlvAdr << 1);
                    for (int i = 0; i < iWrLen; i++)
                    {
                        wrBuf[i + 1] = iWrArr[i];
                    }
                    uint reLen = 0;
                    byte[] reArr = new byte[reLen];
                    if (!CH341DLL.CH341StreamI2C(numPort, wrLen, wrBuf, reLen, reArr)) { return false; }
                    break;
                case eTranMode.StepMsg:
                    if (!CH341EX.CH341WriteI2C(numPort, iSlvAdr, iWrLen, iWrArr, iNoStop)) { return false; }
                    break;
                default:
                    break;
            }
            return true;
        }
        // 读I2C简化参数
        public static bool ReadI2C(uint iSlvAdr, uint iReLen, ref byte[] oReArr)
        {
            switch (tranMode)
            {
                case eTranMode.NoTips:
                    uint wrLen = 1;
                    byte[] wrBuf = new byte[wrLen];
                    wrBuf[0] = (byte)(iSlvAdr << 1);
                    if (!CH341DLL.CH341StreamI2C(numPort, wrLen, wrBuf, iReLen, oReArr)) { return false; }
                    break;
                case eTranMode.StepMsg:
                    if (!CH341EX.CH341ReadI2C(numPort, iSlvAdr, iReLen, ref oReArr)) { return false; }
                    break;
                default:
                    break;
            }
            return true;
        }
        // I2C流，简化参数
        public static bool StreamI2C(uint iSlvAdr, uint iWrLen, byte[] iWrArr, uint iReLen, ref byte[] oReArr)
        {
            switch (tranMode)
            {
                case eTranMode.NoTips:
                    uint wrLen = iWrLen + 1;
                    byte[] wrBuf = new byte[wrLen];
                    wrBuf[0] = (byte)(iSlvAdr << 1);
                    for (int i = 0; i < iWrLen; i++)
                    {
                        wrBuf[i + 1] = iWrArr[i];
                    }
                    if (!CH341DLL.CH341StreamI2C(numPort, wrLen, wrBuf, iReLen, oReArr)) return false;
                    break;
                case eTranMode.StepMsg:
                    if (!CH341EX.CH341StreamI2C(numPort, iSlvAdr, iWrLen, iWrArr, iReLen, ref oReArr)) return false;
                    break;
                default:
                    break;
            }
            return true;
        }
        // 写寄存器，简化参数
        public static bool WriteRegister(uint iSlvAdr, byte iStrAdr, uint iWrLen, byte[] iWrReg)
        {
            uint wrLen; byte[] wrArr; uint reLen; byte[] reArr;
            switch (tranMode)
            {
                case eTranMode.NoTips:
                    wrLen = iWrLen + 2;
                    wrArr = new byte[wrLen];
                    wrArr[0] = (byte)(iSlvAdr << 1);
                    wrArr[1] = iStrAdr;
                    for (int i = 0; i < iWrLen; i++)
                    {
                        wrArr[i + 2] = iWrReg[i];
                    }
                    reLen = 0;
                    reArr = new byte[reLen];
                    if (!CH341DLL.CH341StreamI2C(numPort, wrLen, wrArr, reLen, reArr)) return false;
                    break;
                case eTranMode.StepMsg:
                    wrLen = iWrLen + 1;
                    wrArr = new byte[wrLen];
                    wrArr[0] = iStrAdr;
                    for (int i = 0; i < iWrLen; i++)
                    {
                        wrArr[i + 1] = iWrReg[i];
                    }
                    reLen = 0;
                    reArr = new byte[reLen];
                    if (!CH341EX.CH341StreamI2C(numPort, iSlvAdr, wrLen, wrArr, reLen, ref reArr)) return false;
                    break;
                default:
                    break;
            }
            return true;
        }
        // 读寄存器，简化参数
        public static bool ReadRegister(uint iSlvAdr, byte iStrAdr, uint iReLen, ref byte[] oReArr)
        {
            uint wrLen; byte[] wrArr;
            switch (tranMode)
            {
                case eTranMode.NoTips:
                    wrLen = 2;
                    wrArr = new byte[wrLen];
                    wrArr[0] = (byte)(iSlvAdr << 1);
                    wrArr[1] = iStrAdr;
                    if (!CH341DLL.CH341StreamI2C(numPort, wrLen, wrArr, iReLen, oReArr)) return false;
                    break;
                case eTranMode.StepMsg:
                    wrLen = 1;
                    wrArr = new byte[wrLen];
                    wrArr[0] = iStrAdr;
                    if (!CH341EX.CH341StreamI2C(numPort, iSlvAdr, wrLen, wrArr, iReLen, ref oReArr)) return false;
                    break;
                default:
                    break;
            }
            return true;
        }        // 读寄存器，简化参数,删除mode选择
        public static bool ReadRegister3(uint iSlvAdr, byte iStrAdr, uint iReLen, ref byte[] oReArr, bool CRC_ena)
        {
            uint wrLen; byte[] wrArr;

                    wrLen = 1;
                    wrArr = new byte[wrLen];
                    wrArr[0] = iStrAdr;

            byte[] reArr = new byte[iReLen*2+2];
            if (CRC_ena)
            {
                if (!CH341EX.CH341StreamI2C(numPort, iSlvAdr, wrLen, wrArr, 2*iReLen, ref reArr)) { return false; }
                byte[] reRegplusDat = new byte[2];
                reRegplusDat[0] = (byte)((iSlvAdr << 1) | 0x01); 
                reRegplusDat[1] = reArr[0];
                if (reArr[1] == (byte)CRCcalculate(reRegplusDat, 2))
                    oReArr[0] = reArr[0];
                else
                {
                    if (!ErrHdl.ErrorHandle(ErrHdl.ErrEnum.CRCWrong, null)) { oReArr[0] = 0; return false; }
                    oReArr[0] = 0;
                    return false;
                }
                for (uint i = 0; i < 2 * iReLen; i = i + 2) 
                {
                    reRegplusDat[0] = reArr[i + 2];
                    reRegplusDat[1] = 0;
                    if (reArr[i + 3] == (byte)CRCcalculate(reRegplusDat, 1))
                        oReArr[(i + 2) / 2] = reArr[i + 2];
                    else
                    {
                        if (!ErrHdl.ErrorHandle(ErrHdl.ErrEnum.CRCWrong, null)) { oReArr[(i + 2) / 2] = 0; return false; }
                        oReArr[(i + 2) / 2] = 0;
                        return false;
                    }
                }
            }
            else
            {
                if (!CH341EX.CH341StreamI2C(numPort, iSlvAdr, wrLen, wrArr, iReLen, ref oReArr)) { return false; }
            }
            return true;
        }
        public static bool ReadRegister2(uint iSlvAdr, byte iStrAdr, uint iReLen, ref byte[] oReArr)
        {
            uint wrLen; byte[] wrArr;

            wrLen = 1;
            wrArr = new byte[wrLen];
            wrArr[0] = iStrAdr;
            if (!CH341EX.CH341StreamI2C(numPort, iSlvAdr, wrLen, wrArr, iReLen, ref oReArr)) { return false; }

            return true;
        }




        // 写寄存器，1Byte数据
        public static bool WriteReg1Byt(uint iSlvAdr, byte iRegAddr, byte iWrVal, bool CRC_ena)
        {
            uint wrLen = 2;
            byte[] wrArr = new byte[wrLen];
            byte[] wrArr2 = new byte[wrLen + 1];

            if (CRC_ena)
            {
                wrArr2[0] = (byte)(iSlvAdr << 1);
                wrArr2[1] = iRegAddr;
                wrArr2[2] = iWrVal;
                wrArr[1] = (byte)CRCcalculate(wrArr2, 3);
                wrArr[0] = iWrVal;
            }
            else
            {
                wrArr[0] = iWrVal;
                wrLen = 1;
            }
            return WriteRegister(iSlvAdr, iRegAddr, wrLen, wrArr);
        }
        // 读寄存器，1 Byte数据
        public static bool ReadReg1Byt(uint iSlvAdr, byte iRegAddr, out byte oReVal, bool CRC_ena)//选择用流读多了写地址这一个周期，而且写单Byte的时候，SDA会下拉不停，需要后期debug
        {
            uint reLen = 2;

            byte[] reArr = new byte[reLen];
            byte[] reArr2 = new byte[reLen];
    
            if(CRC_ena)
            {
                  if (!ReadRegister2(iSlvAdr, iRegAddr, reLen, ref  reArr))
                    {
                    oReVal = 0;
                    return false;
                    }
                reArr2[0] = (byte)((iSlvAdr << 1) | 0x01);
                reArr2[1] = reArr[0];
                if (reArr[1] == (byte)CRCcalculate(reArr2, 2))
                    oReVal = reArr[0];
                else
                {
                    if (!ErrHdl.ErrorHandle(ErrHdl.ErrEnum.CRCWrong, null)) { oReVal = 0; return false; }// CRC数据错误
                    oReVal = 0;
                    return false;
                }
            }
            else
            {
                reLen = 1;
                if (!ReadRegister2(iSlvAdr, iRegAddr, reLen, ref reArr))
                {
                    oReVal = 0;
                    return false;
                }
                oReVal = reArr[0];
            }
                

            return true;
        } 
        // 设置PIN值，只有低24位
        public static bool SetPinStatus(int iNumBit, bool iStat)
        {
            return CH341EX.SetPinStatus(numPort, iNumBit, iStat);
        }
        // 设置PIN方向，只有低16位
        public static bool SetPinDirection(int iNumBit, bool iDir)
        {
            return CH341EX.SetPinDirection(numPort, iNumBit, iDir);
        }
        // 获取PIN值，只有低24位
        public static bool GetPinStatus(int iNumBit, out bool oStatus)
        {
            return CH341EX.GetPinStatus(numPort, iNumBit, out  oStatus);
        }
        //
        public static bool SetI2CPort(bool bSCL, bool bSDA)
        {
            if (!CH341EX.SetPinStatus(numPort, 18, bSCL)) { return false; }//SCL设置为位18
            if (!CH341EX.SetPinStatus(numPort, 19, bSDA)) { return false; }//SDA设置为位19，状态为位23
            return true;
        }
        // 写EEPROM
        public static bool WriteEEPROM(eEepromType iEepromID, uint iAddr, uint iLength, Byte[] iBuffer)
        {
            return CH341DLL.CH341WriteEEPROM(numPort, (uint)iEepromID, iAddr, iLength, iBuffer);
        }
        // 读EEPROM
        public static bool ReadEEPROM(eEepromType iEepromID, uint iAddr, uint iLength, Byte[] oBuffer)
        {
            return CH341DLL.CH341ReadEEPROM(numPort, (uint)iEepromID, iAddr, iLength, oBuffer);
        }

        public static uint CRCcalculate(byte[] CRCDAT, uint Len)                                            //8位CRC计算，可以通过
        {
            uint CRCnum = 0x107;
            byte[] WrCRCSingle2 = new byte[4];
            for (uint i = 0; i < Len; i++)
            {
                WrCRCSingle2[Len - i] = CRCDAT[i];
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

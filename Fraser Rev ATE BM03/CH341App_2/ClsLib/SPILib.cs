//***********************************************************************************************
// I2C����Ӧ���ӳ���
//***********************************************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ClsLib
{
    public static class SPILib
    {
        public enum eDvcType { LPT = 0, CH341 = 1, CY7C68013A = 2, }
        public enum eTranMode { NoTips = 0, StepMsg = 1, }
        public enum eBitRate { _20K = 0, _100K = 1, _400K = 2, _750K = 3, }
        public enum eEepromType { ID_24C01, ID_24C02, ID_24C04, ID_24C08, ID_24C16, ID_24C32, ID_24C64, ID_24C128, ID_24C256, ID_24C512, ID_24C1024, ID_24C2048, ID_24C4096, };

        public delegate void UptScanAddr(byte CurrAddr);
        public static UptScanAddr ScanAddrEvent = null;

        private static eDvcType dvcTyp; // ������Ӳ�����͡�
        private static uint numPort; // �˿ںţ���ʼ���Ĵ�������I2C����������������湲��
        private static eBitRate bitRate; // ���������ȫ�ֱ�����Ϊ��ؿؼ��ṩ��ʼ��ֵ��
        private static eTranMode tranMode; // ����ģʽ�Ĵ�����һ��ֻ��Ҫ��ʼ��һ�Ρ�
        private static bool iniStat;

        private static uint streamMode = 0;
        public static eDvcType DvcTyp { get { return dvcTyp; } /*set { dvcTyp = value; }*/ }
        public static uint NumPort { get { return numPort; } /*set { numPort = value; }*/ }
        public static eBitRate BitRate { get { return bitRate; } /*set { numPort = bitRate; }*/ }
        public static eTranMode TranMode { get { return tranMode; } /*set { tranMode = value; }*/ }
        public static bool IniStat { get { return iniStat; } /*set { iniStat = value; }*/ }

        // ����I2Cͨ�����ʣ�Hz��
        public static bool SetBitRate(uint iBitRate)
        {
            eBitRate BitRate;
            if (iBitRate > 400 * 1000) { BitRate = eBitRate._750K; }
            else if (iBitRate > 100 * 1000) { BitRate = eBitRate._400K; }
            else if (iBitRate > 20 * 1000) { BitRate = eBitRate._100K; }
            else /*if (iBitRate > 0)*/ { BitRate = eBitRate._20K; }
            return SetBitRate(BitRate);
        }
        // ����I2Cͨ�����ʣ��������Ϊö����
        public static bool SetBitRate(eBitRate iBitRate)
        {
            bitRate = iBitRate;
            streamMode = streamMode & ~0x03U | (uint)iBitRate;
            if (!CH341DLL.CH341SetStream(numPort, streamMode)) return false;
            return true;
        }
        // ����ת��ģʽ
        public static bool SetTranMode(eTranMode iTranMode)
        {
            tranMode = iTranMode;
            return true;
        }
        // ɨ��ӻ���ַ
        public static bool ScanSlvAddr(out byte[] oAckList, out uint oListSize)
        {
            oListSize = 0;
            oAckList = new byte[128];
            bool ackStat;
            for (int i = 0; i < 128; i++) // 7λ��ַ����0��127
            {
                ScanAddrEvent((byte)i);
                if (!CH341EX.CheckAddr(numPort, (uint)i, out ackStat)) { return false; }// ���ACK��NACK
                if (ackStat)
                {
                    oAckList[oListSize] = (byte)i;
                    oListSize += 1;
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
        // дI2C���򻯲���
        public static bool WriteI2C(uint iSlvAddr, uint iWrLen, byte[] iWrArr, bool iNoStop)
        {
            switch (tranMode)
            {
                case eTranMode.NoTips:
                    uint wrLen = iWrLen + 1;
                    byte[] wrBuf = new byte[wrLen];
                    wrBuf[0] = (byte)(iSlvAddr << 1);
                    for (int i = 0; i < iWrLen; i++)
                    {
                        wrBuf[i + 1] = iWrArr[i];
                    }
                    uint reLen = 0;
                    byte[] reArr = new byte[reLen];
                    if (!CH341DLL.CH341StreamI2C(numPort, wrLen, wrBuf, reLen, reArr)) { return false; }
                    break;
                case eTranMode.StepMsg:
                    if (!CH341EX.CH341WriteI2C(numPort, iSlvAddr, iWrLen, iWrArr, iNoStop)) { return false; }
                    break;
                default:
                    break;
            }
            return true;
        }
        // ��I2C�򻯲���
        public static bool ReadI2C(uint iSlvAddr, uint iReLen, ref byte[] oReArr)
        {
            switch (tranMode)
            {
                case eTranMode.NoTips:
                    uint wrLen = 1;
                    byte[] wrBuf = new byte[wrLen];
                    wrBuf[0] = (byte)(iSlvAddr << 1);
                    if (!CH341DLL.CH341StreamI2C(numPort, wrLen, wrBuf, iReLen, oReArr)) { return false; }
                    break;
                case eTranMode.StepMsg:
                    if (!CH341EX.CH341ReadI2C(numPort, iSlvAddr, iReLen, ref oReArr)) { return false; }
                    break;
                default:
                    break;
            }
            return true;
        }
        // I2C�����򻯲���
        public static bool StreamI2C(uint iSlvAddr, uint iWrLen, byte[] iWrArr, uint iReLen, ref byte[] oReArr)
        {
            switch (tranMode)
            {
                case eTranMode.NoTips:
                    uint wrLen = iWrLen + 1;
                    byte[] wrBuf = new byte[wrLen];
                    wrBuf[0] = (byte)(iSlvAddr << 1);
                    for (int i = 0; i < iWrLen; i++)
                    {
                        wrBuf[i + 1] = iWrArr[i];
                    }
                    if (!CH341DLL.CH341StreamI2C(numPort, wrLen, wrBuf, iReLen, oReArr)) return false;
                    break;
                case eTranMode.StepMsg:
                    if (!CH341EX.CH341StreamI2C(numPort, iSlvAddr, iWrLen, iWrArr, iReLen, ref oReArr)) return false;
                    break;
                default:
                    break;
            }
            return true;
        }
        // д�Ĵ������򻯲���
        public static bool WriteRegister(uint iSlvAddr, byte iStrAdr, uint iWrLen, byte[] iWrReg)
        {
            uint wrLen; byte[] wrArr; uint reLen; byte[] reArr;
            switch (tranMode)
            {
                case eTranMode.NoTips:
                    wrLen = iWrLen + 2;
                    wrArr = new byte[wrLen];
                    wrArr[0] = (byte)(iSlvAddr << 1);
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
                    if (!CH341EX.CH341StreamI2C(numPort, iSlvAddr, wrLen, wrArr, reLen, ref reArr)) return false;
                    break;
                default:
                    break;
            }
            return true;
        }
        // ���Ĵ������򻯲���
        public static bool ReadRegister(uint iSlvAddr, byte iStrAdr, uint iReLen, ref byte[] oReArr)
        {
            uint wrLen; byte[] wrArr;
            switch (tranMode)
            {
                case eTranMode.NoTips:
                    wrLen = 2;
                    wrArr = new byte[wrLen];
                    wrArr[0] = (byte)(iSlvAddr << 1);
                    wrArr[1] = iStrAdr;
                    if (!CH341DLL.CH341StreamI2C(numPort, wrLen, wrArr, iReLen, oReArr)) return false;
                    break;
                case eTranMode.StepMsg:
                    wrLen = 1;
                    wrArr = new byte[wrLen];
                    wrArr[0] = iStrAdr;
                    if (!CH341EX.CH341StreamI2C(numPort, iSlvAddr, wrLen, wrArr, iReLen, ref oReArr)) return false;
                    break;
                default:
                    break;
            }
            return true;
        }
        // д�Ĵ�����1Byte����
        public static bool WriteReg1Byt(uint iSlvAddr, byte iRegAddr, byte iWrVal)
        {
            uint wrLen = 1;
            byte[] wrArr = new byte[wrLen];
            wrArr[0] = iWrVal;
            return WriteRegister(iSlvAddr, iRegAddr, wrLen, wrArr);
        }
        // ���Ĵ�����1Byte����
        public static bool ReadReg1Byt(uint iSlvAddr, byte iRegAddr, ref byte oReVal)
        {
            uint reLen = 1;
            byte[] reArr = new byte[reLen];
            if (!ReadRegister(iSlvAddr, iRegAddr, reLen, ref  reArr)) { return false; }
            oReVal = reArr[0];
            return true;
        }
        // ����PINֵ��ֻ�е�24λ
        public static bool SetPinStatus(int iNumBit, bool iStat)
        {
            return CH341EX.SetPinStatus(numPort, iNumBit, iStat);
        }
        // ����PIN����ֻ�е�16λ
        public static bool SetPinDirection(int iNumBit, bool iDir)
        {
            return CH341EX.SetPinDirection(numPort, iNumBit, iDir);
        }
        // ��ȡPINֵ��ֻ�е�24λ
        public static bool GetPinStatus(int iNumBit, out bool oStatus)
        {
            return CH341EX.GetPinStatus(numPort, iNumBit, out  oStatus);
        }
        //
        public static bool SetI2CPort(bool bSCL, bool bSDA)
        {
            if (!CH341EX.SetPinStatus(numPort, 18, bSCL)) { return false; }//SCL����Ϊλ18
            if (!CH341EX.SetPinStatus(numPort, 19, bSDA)) { return false; }//SDA����Ϊλ19��״̬Ϊλ23
            return true;
        }
    }
}

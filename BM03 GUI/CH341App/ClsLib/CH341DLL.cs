// 2004.05.28, 2004.10.20, 2005.01.08, 2005.03.25, 2005.04.28, 2005.07.18, 2005.07.28, 2005.09.19
//****************************************
//**  Copyright  (C)  W.ch  1999-2005   **
//**  Web:  http://www.winchiphead.com  **
//****************************************
//**  DLL for USB interface chip CH341  **
//**  C, VC5.0                          **
//****************************************
//
// USB���߽ӿ�оƬCH341����Ӧ�ò�ӿڿ� V1.9
// �Ͼ��ߺ�������޹�˾  ����: W.ch 2005.09
// CH341-DLL  V1.9
// ���л���: Windows 98/ME, Windows 2000/XP
// support USB chip: CH341, CH341A
// USB => Parallel, I2C, SPI, JTAG ...
//
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using NTSTATUS = System.Int32;				// ����״̬ 

namespace ClsLib
{
    internal static class CH341DLL
    // public static class CH341DLL
    {
#pragma warning disable  169 // �������о���
        //public struct mUspValue
        //{
        //    public byte mUspValueLow;              // 02H ֵ�������ֽ�
        //    public byte mUspValueHigh;             // 03H ֵ�������ֽ�
        //}
        //public struct mUspIndex
        //{
        //    public byte mUspIndexLow;              // 04H �����������ֽ�
        //    public byte mUspIndexHigh;             // 05H �����������ֽ�
        //}
        //public struct USB_SETUP_PKT // USB���ƴ���Ľ����׶ε�����������ṹ
        //{
        //    public byte mUspReqType;               // 00H ��������
        //    public byte mUspRequest;               // 01H �������
        //    public mUspValue mUspValue;            // 02H-03H ֵ����
        //    public mUspIndex mUspIndex;            // 04H-05H ��������
        //    public int mLength;                    // 06H-07H ���ݽ׶ε����ݳ���
        //}
        public struct WIN32_COMMAND // ����WIN32����ӿڽṹ
        {
            //public uint mFunction;               // ����ʱָ�����ܴ�����߹ܵ���
            ////���ʱ���ز���״̬
            //public uint mLength;                 // ��ȡ����,���غ������ݵĳ���
            //public byte[] mBuffer;              // mUSBIO_PACKET_LENGTH - 1 '���ݻ�����,����Ϊ0��255B
        }
#pragma warning restore 169 // �������о���

        public const uint FILE_DEVICE_UNKNOWN = 0x22;
        public const uint FILE_ANY_ACCESS = 0;
        public const uint METHOD_BUFFERED = 0;
        // WIN32Ӧ�ò�ӿ�����
        public const uint IOCTL_USBIO_COMMAND = (FILE_DEVICE_UNKNOWN * (2 ^ 16) + FILE_ANY_ACCESS * 2 ^ 14 + 0xF34 * 2 ^ 2 + METHOD_BUFFERED);    // ר�ýӿ�
        public const uint mWIN32_COMMAND_HEAD = 8;              // WIN32����ӿڵ�ͷ����

        // ���ݳ��ȶ���
        public const byte mCH341_BUCK_PACKET_LENGTH = 32;	    // CH341֧�ֵ����ݰ�(�����������)�ĳ���
        public const byte mCH341_CTRL_PACKET_LENGTH = 8;		// CH341֧�ֵĶ����ݰ�(���ƴ���)�ĳ���
        public const uint mCH341_MAX_NUMBER = 16;				// ���ͬʱ���ӵ�CH341��
        public const uint mMAX_BUFFER_LENGTH = 4096;		    // ���ݻ�������󳤶�4096
        public const uint mMAX_COMMAND_LENGTH = (mWIN32_COMMAND_HEAD + mMAX_BUFFER_LENGTH);          // ������ݳ��ȼ�������ṹͷ�ĳ���
        public const uint mDEFAULT_BUFFER_LEN = 1024;		    // ���ݻ�����Ĭ�ϳ���1024
        public const uint mDEFAULT_COMMAND_LEN = (mWIN32_COMMAND_HEAD + mDEFAULT_BUFFER_LEN);      // Ĭ�����ݳ��ȼ�������ṹͷ�ĳ���

        // CH341�˵��ַ
        public const byte mCH341_ENDP_INTER_UP = 0x81;		    // CH341���ж������ϴ��˵�ĵ�ַ
        public const byte mCH341_ENDP_INTER_DOWN = 0x01;		// CH341���ж������´��˵�ĵ�ַ
        public const byte mCH341_ENDP_DATA_UP = 0x82;			// CH341�����ݿ��ϴ��˵�ĵ�ַ
        public const byte mCH341_ENDP_DATA_DOWN = 0x02;		    // CH341�����ݿ��´��˵�ĵ�ַ

        // �豸��ӿ��ṩ�Ĺܵ���������
        public const uint mPipeDeviceCtrl = 0x00000004;	    // CH341���ۺϿ��ƹܵ�
        public const uint mPipeInterUp = 0x00000005;		// CH341���ж������ϴ��ܵ�
        public const uint mPipeDataUp = 0x00000006;		    // CH341�����ݿ��ϴ��ܵ�
        public const uint mPipeDataDown = 0x00000007;		// CH341�����ݿ��´��ܵ�

        // Ӧ�ò�ӿڵĹ��ܴ���
        public const uint mFuncNoOperation = 0x00000000;	// �޲���
        public const uint mFuncGetVersion = 0x00000001;	    // ��ȡ��������汾��
        public const uint mFuncGetConfig = 0x00000002;	    // ��ȡUSB�豸����������
        public const uint mFuncSetTimeout = 0x00000009;	    // ����USBͨѶ��ʱ
        public const uint mFuncSetExclusive = 0x0000000b;	// ���ö�ռʹ��
        public const uint mFuncResetDevice = 0x0000000c;	// ��λUSB�豸
        public const uint mFuncResetPipe = 0x0000000d;	    // ��λUSB�ܵ�
        public const uint mFuncAbortPipe = 0x0000000e;	    // ȡ��USB�ܵ�����������

        // CH341����ר�õĹ��ܴ���
        public const uint mFuncSetParaMode = 0x0000000f;	// ���ò���ģʽ
        public const uint mFuncReadData0 = 0x00000010;	    // �Ӳ��ڶ�ȡ���ݿ�0
        public const uint mFuncReadData1 = 0x00000011;	    // �Ӳ��ڶ�ȡ���ݿ�1
        public const uint mFuncWriteData0 = 0x00000012; 	// �򲢿�д�����ݿ�0
        public const uint mFuncWriteData1 = 0x00000013;	    // �򲢿�д�����ݿ�1
        public const uint mFuncWriteRead = 0x00000014;	    // �����������
        public const uint mFuncBufferMode = 0x00000020; 	// �趨�����ϴ�ģʽ����ѯ�������е����ݳ���
        public const uint mFuncBufferModeDn = 0x00000021;	// �趨�����´�ģʽ����ѯ�������е����ݳ���

        // USB�豸��׼�������
        public const byte mUSB_CLR_FEATURE = 0x01;
        public const byte mUSB_SET_FEATURE = 0x03;
        public const byte mUSB_GET_STATUS = 0x00;
        public const byte mUSB_SET_ADDRESS = 0x05;
        public const byte mUSB_GET_DESCR = 0x06;
        public const byte mUSB_SET_DESCR = 0x07;
        public const byte mUSB_GET_CONFIG = 0x08;
        public const byte mUSB_SET_CONFIG = 0x09;
        public const byte mUSB_GET_INTERF = 0x0a;
        public const byte mUSB_SET_INTERF = 0x0b;
        public const byte mUSB_SYNC_FRAME = 0x0c;

        // CH341���ƴ���ĳ���ר����������
        public const byte mCH341_VENDOR_READ = 0xC0;		// ͨ�����ƴ���ʵ�ֵ�CH341����ר�ö�����
        public const byte mCH341_VENDOR_WRITE = 0x40;		// ͨ�����ƴ���ʵ�ֵ�CH341����ר��д����

        // CH341���ƴ���ĳ���ר���������
        public const byte mCH341_PARA_INIT = 0xB1;			// ��ʼ������
        public const byte mCH341_I2C_STATUS = 0x52;			// ��ȡI2C�ӿڵ�״̬
        public const byte mCH341_I2C_COMMAND = 0x53;		// ����I2C�ӿڵ�����

        // CH341���ڲ����������
        public const byte mCH341_PARA_CMD_R0 = 0xAC;		// �Ӳ��ڶ�����0
        public const byte mCH341_PARA_CMD_R1 = 0xAD;		// �Ӳ��ڶ�����1
        public const byte mCH341_PARA_CMD_W0 = 0xA6;		// �򲢿�д����0
        public const byte mCH341_PARA_CMD_W1 = 0xA7;		// �򲢿�д����1
        public const byte mCH341_PARA_CMD_STS = 0xA0;		// ��ȡ����״̬

        // CH341A����������ʼ�������
        public const byte mCH341A_CMD_SET_OUTPUT = 0xA1;		// ���ò������
        public const byte mCH341A_CMD_IO_ADDR = 0xA2;			// MEM����ַ��д/�������,�Ӵ��ֽڿ�ʼΪ������
        public const byte mCH341A_CMD_PRINT_OUT = 0xA3;			// PRINT���ݴ�ӡ��ʽ���,�Ӵ��ֽڿ�ʼΪ������
        public const byte mCH341A_CMD_SPI_STREAM = 0xA8;		// SPI�ӿڵ������,�Ӵ��ֽڿ�ʼΪ������
        public const byte mCH341A_CMD_SIO_STREAM = 0xA9;		// SIO�ӿڵ������,�Ӵ��ֽڿ�ʼΪ������
        public const byte mCH341A_CMD_I2C_STREAM = 0xAA;		// I2C�ӿڵ������,�Ӵ��ֽڿ�ʼΪI2C������
        public const byte mCH341A_CMD_UIO_STREAM = 0xAB;		// UIO�ӿڵ������,�Ӵ��ֽڿ�ʼΪ������

        // CH341A���ƴ���ĳ���ר���������
        public const byte mCH341A_BUF_CLEAR = 0xB2;				// ���δ��ɵ�����
        public const byte mCH341A_I2C_CMD_X = 0x54;				// ����I2C�ӿڵ�����,����ִ��
        public const byte mCH341A_DELAY_MS = 0x5E;				// ������Ϊ��λ��ʱָ��ʱ��
        public const byte mCH341A_GET_VER = 0x5F;				// ��ȡоƬ�汾
        public const byte mCH341_EPP_IO_MAX = (mCH341_BUCK_PACKET_LENGTH - 1);// CH341��EPP/MEM��ʽ�µ��ζ�д���ݿ����󳤶�
        public const byte mCH341A_EPP_IO_MAX = 0xFF;			// CH341A��EPP/MEM��ʽ�µ��ζ�д���ݿ����󳤶�
        public const byte mCH341A_CMD_IO_ADDR_W = 0x00;			// MEM����ַ��д/���������������:д����,λ6-λ0Ϊ��ַ,��һ���ֽ�Ϊ��д����
        public const byte mCH341A_CMD_IO_ADDR_R = 0x80;			// MEM����ַ��д/���������������:������,λ6-λ0Ϊ��ַ,��������һ�𷵻�
        public const byte mCH341A_CMD_GET_INPUT = 0xA0;			// �Զ����ȡ�˿�״̬

        // I2C�ӿڵ�������:
        public const byte mCH341A_CMD_I2C_STM_STA = 0x74;		// ������ʼλ
        public const byte mCH341A_CMD_I2C_STM_STO = 0x75;		// ����ֹͣλ
        public const byte mCH341A_CMD_I2C_STM_OUT = 0x80;		// �������,λ5-λ0Ϊ����,�����ֽ�Ϊ����,0������ֻ����һ���ֽڲ�����Ӧ��
        public const byte mCH341A_CMD_I2C_STM_IN = 0xC0;		// ��������,λ5-λ0Ϊ����,0������ֻ����һ���ֽڲ�������Ӧ��
        public const byte mCH341A_CMD_I2C_STM_MAX = 0x3F < mCH341_BUCK_PACKET_LENGTH ? 0x3F : mCH341_BUCK_PACKET_LENGTH;	// ������������������ݵ���󳤶�
        public const byte mCH341A_CMD_I2C_STM_SET = 0x60;		// ���ò���,λ2=SPI��I/O��(0=���뵥��,1=˫��˫��),λ1λ0=I2C�ٶ�(00=����,01=��׼,10=����,11=����)
        public const byte mCH341A_CMD_I2C_STM_US = 0x40;		// ��΢��Ϊ��λ��ʱ,λ3-λ0Ϊ��ʱֵ
        public const byte mCH341A_CMD_I2C_STM_MS = 0x50;		// ������Ϊ��λ��ʱ,λ3-λ0Ϊ��ʱֵ
        public const byte mCH341A_CMD_I2C_STM_DLY = 0x0F;		// ����������ʱ�����ֵ
        public const byte mCH341A_CMD_I2C_STM_END = 0x00;		// �������ǰ����

        // UIO�ӿڵ�������:
        public const byte mCH341A_CMD_UIO_STM_IN = 0x00;		// ��������D7-D0
        public const byte mCH341A_CMD_UIO_STM_DIR = 0x40;		// �趨I/O����D5-D0,λ5-λ0Ϊ��������
        public const byte mCH341A_CMD_UIO_STM_OUT = 0x80;		// �������D5-D0,λ5-λ0Ϊ����
        public const byte mCH341A_CMD_UIO_STM_US = 0xC0;		// ��΢��Ϊ��λ��ʱ,λ5-λ0Ϊ��ʱֵ
        public const byte mCH341A_CMD_UIO_STM_END = 0x20;		// �������ǰ����

        // CH341���ڹ���ģʽ
        public const byte mCH341_PARA_MODE_EPP = 0x00;			// CH341���ڹ���ģʽΪEPP��ʽ
        public const byte mCH341_PARA_MODE_EPP17 = 0x00;		// CH341A���ڹ���ģʽΪEPP��ʽV1.7
        public const byte mCH341_PARA_MODE_EPP19 = 0x01;		// CH341A���ڹ���ģʽΪEPP��ʽV1.9
        public const byte mCH341_PARA_MODE_MEM = 0x02;			// CH341���ڹ���ģʽΪMEM��ʽ


        // I/O��������λ����,ֱ�������״̬�źŵ�λ����,ֱ�������λ���ݶ���
        public const uint mStateBitERR = 0x00000100;		// ֻ����д,ERR#��������״̬,1:�ߵ�ƽ,0:�͵�ƽ
        public const uint mStateBitPEMP = 0x00000200;		// ֻ����д,PEMP��������״̬,1:�ߵ�ƽ,0:�͵�ƽ
        public const uint mStateBitINT = 0x00000400;		// ֻ����д,INT#��������״̬,1:�ߵ�ƽ,0:�͵�ƽ
        public const uint mStateBitSLCT = 0x00000800;		// ֻ����д,SLCT��������״̬,1:�ߵ�ƽ,0:�͵�ƽ
        public const uint mStateBitWAIT = 0x00002000;		// ֻ����д,WAIT#��������״̬,1:�ߵ�ƽ,0:�͵�ƽ
        public const uint mStateBitDATAS = 0x00004000;	    // ֻд�ɶ�,DATAS#/READ#��������״̬,1:�ߵ�ƽ,0:�͵�ƽ
        public const uint mStateBitADDRS = 0x00008000;	    // ֻд�ɶ�,ADDRS#/ADDR/ALE��������״̬,1:�ߵ�ƽ,0:�͵�ƽ
        public const uint mStateBitRESET = 0x00010000;	    // ֻд,RESET#��������״̬,1:�ߵ�ƽ,0:�͵�ƽ
        public const uint mStateBitWRITE = 0x00020000;	    // ֻд,WRITE#��������״̬,1:�ߵ�ƽ,0:�͵�ƽ
        public const uint mStateBitSDA = 0x00800000;		// ֻ��,SDA��������״̬,1:�ߵ�ƽ,0:�͵�ƽ��ע�⣺λ19Ϊ���ã�λ23Ϊ״̬

        public const uint MAX_DEVICE_PATH_SIZE = 128;		// �豸���Ƶ�����ַ���
        public const uint MAX_DEVICE_ID_SIZE = 64;		    // �豸ID������ַ���

        public delegate void mPCH341_INT_ROUTINE(uint iStatus);
        // �жϷ������
        // iStatus �ж�״̬����,�ο������λ˵��
        // λ7-λ0��ӦCH341��D7-D0����
        // λ8��ӦCH341��ERR#����, λ9��ӦCH341��PEMP����, λ10��ӦCH341��INT#����, λ11��ӦCH341��SLCT����

        public delegate void mPCH341_NOTIFY_ROUTINE(uint iEventStatus);
        // �豸�¼�֪ͨ�ص�����
        // �豸�¼��͵�ǰ״̬(�����ж���): 0=�豸�γ��¼�, 3=�豸�����¼�

        public const uint CH341_DEVICE_ARRIVAL = 3;	// �豸�����¼�,�Ѿ�����
        public const uint CH341_DEVICE_REMOVE_PEND = 1;	// �豸��Ҫ�γ�
        public const uint CH341_DEVICE_REMOVE = 0;	// �豸�γ��¼�,�Ѿ��γ�

        [DllImport("CH341DLL.DLL")]
        public static extern IntPtr CH341OpenDevice(uint iIndex);
        // ��CH341�豸,���ؾ��,��������Ч
        // iIndex ָ��CH341�豸���,0��Ӧ��һ���豸

        [DllImport("CH341DLL.DLL")]
        public static extern void CH341CloseDevice(uint iIndex);
        // �ر�CH341�豸
        // iIndex ָ��CH341�豸���

        [DllImport("CH341DLL.DLL")]
        public static extern uint CH341GetVersion();  // ���DLL�汾��,���ذ汾��

        [DllImport("CH341DLL.DLL")]
        private static extern uint CH341DriverCommand(uint iIndex, WIN32_COMMAND ioCommand);
        // ֱ�Ӵ����������������,�����򷵻�0,���򷵻����ݳ���
        // iIndex   ָ��CH341�豸���,V1.6����DLLҲ�������豸�򿪺�ľ��
        // ioCommand   ����ṹ��ָ��
        // �ó����ڵ��ú󷵻����ݳ���,������Ȼ��������ṹ,����Ƕ�����,�����ݷ���������ṹ��,
        // ���ص����ݳ����ڲ���ʧ��ʱΪ0,�����ɹ�ʱΪ��������ṹ�ĳ���,�����һ���ֽ�,�򷵻�mWIN32_COMMAND_HEAD+1,
        // ����ṹ�ڵ���ǰ,�ֱ��ṩ:�ܵ��Ż�������ܴ���,��ȡ���ݵĳ���(��ѡ),����(��ѡ)
        // ����ṹ�ڵ��ú�,�ֱ𷵻�:����״̬����,�������ݵĳ���(��ѡ),
        // ����״̬��������WINDOWS����Ĵ���,���Բο�NTSTATUS.H,
        // �������ݵĳ�����ָ���������ص����ݳ���,���ݴ�������Ļ�������,����д����һ��Ϊ0

        [DllImport("CH341DLL.DLL")]
        public static extern uint CH341GetDrvVersion();  // �����������汾��,���ذ汾��,�����򷵻�0

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341ResetDevice(uint iIndex);
        // ��λUSB�豸
        // iIndex ָ��CH341�豸���

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341GetDeviceDescr(uint iIndex, byte[] oBuffer, ref uint ioLength);
        // ��ȡ�豸������
        // iIndex ָ��CH341�豸���
        // oBuffer ָ��һ���㹻��Ļ�����,���ڱ���������
        // ioLength ָ�򳤶ȵ�Ԫ,����ʱΪ׼����ȡ�ĳ���,���غ�Ϊʵ�ʶ�ȡ�ĳ���

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341GetConfigDescr(uint iIndex, byte[] oBuffer, ref uint ioLength);
        // ��ȡ����������
        // iIndex ָ��CH341�豸���
        // oBuffer ָ��һ���㹻��Ļ�����,���ڱ���������
        // ioLength ָ�򳤶ȵ�Ԫ,����ʱΪ׼����ȡ�ĳ���,���غ�Ϊʵ�ʶ�ȡ�ĳ���

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341SetIntRoutine(uint iIndex, mPCH341_INT_ROUTINE iIntRoutine);
        // �趨�жϷ������
        // iIndex ָ��CH341�豸���
        // iIntRoutine ָ���жϷ������,ΪNULL��ȡ���жϷ���,�������ж�ʱ���øó���

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341ReadInter(uint iIndex, out uint oStatus);
        // ��ȡ�ж�����
        // iIndex ָ��CH341�豸���
        // oStatus ָ��һ��˫�ֵ�Ԫ,���ڱ����ȡ���ж�״̬����,������
        // λ7-λ0��ӦCH341��D7-D0����
        // λ8��ӦCH341��ERR#����, λ9��ӦCH341��PEMP����, λ10��ӦCH341��INT#����, λ11��ӦCH341��SLCT����

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341AbortInter(uint iIndex);
        // �����ж����ݶ�����
        // iIndex ָ��CH341�豸���

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341SetParaMode(uint iIndex, uint iMode);
        // ���ò���ģʽ
        // iIndex ָ��CH341�豸���
        // iMode ָ������ģʽ: 0ΪEPPģʽ/EPPģʽV1.7, 1ΪEPPģʽV1.9, 2ΪMEMģʽ

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341InitParallel(uint iIndex, uint iMode);
        // ��λ����ʼ������,RST#����͵�ƽ����
        // iIndex ָ��CH341�豸���
        // iMode ָ������ģʽ: 0ΪEPPģʽ/EPPģʽV1.7, 1ΪEPPģʽV1.9, 2ΪMEMģʽ, >= 0x00000100 ���ֵ�ǰģʽ

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341ReadData0(uint iIndex, byte[] oBuffer, ref uint ioLength);

        // ��0#�˿ڶ�ȡ���ݿ�
        // iIndex ָ��CH341�豸���
        // oBuffer ָ��һ���㹻��Ļ�����,���ڱ����ȡ������
        // ioLength ָ�򳤶ȵ�Ԫ,����ʱΪ׼����ȡ�ĳ���,���غ�Ϊʵ�ʶ�ȡ�ĳ���


        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341ReadData1(uint iIndex, byte[] oBuffer, ref uint ioLength);

        // ��1#�˿ڶ�ȡ���ݿ�
        // iIndex ָ��CH341�豸���
        // oBuffer ָ��һ���㹻��Ļ�����,���ڱ����ȡ������
        // ioLength ָ�򳤶ȵ�Ԫ,����ʱΪ׼����ȡ�ĳ���,���غ�Ϊʵ�ʶ�ȡ�ĳ���

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341AbortRead(uint iIndex);
        // �������ݿ������
        // iIndex ָ��CH341�豸���

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341WriteData0(uint iIndex, byte[] iBuffer, ref uint ioLength);
        // ��0#�˿�д�����ݿ�
        // iIndex ָ��CH341�豸���
        // iBuffer ָ��һ��������,����׼��д��������
        // ioLength ָ�򳤶ȵ�Ԫ,����ʱΪ׼��д���ĳ���,���غ�Ϊʵ��д���ĳ���


        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341WriteData1(uint iIndex, byte[] iBuffer, ref uint ioLength);
        // ��1#�˿�д�����ݿ�
        // iIndex ָ��CH341�豸���
        // iBuffer ָ��һ��������,����׼��д��������
        // ioLength ָ�򳤶ȵ�Ԫ,����ʱΪ׼��д���ĳ���,���غ�Ϊʵ��д���ĳ���

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341AbortWrite(uint iIndex);
        // �������ݿ�д����
        // iIndex ָ��CH341�豸���

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341GetStatus(uint iIndex, out uint oStatus);
        // ͨ��CH341ֱ���������ݺ�״̬(���ƴ���)
        // iIndex ָ��CH341�豸���
        // oStatus ָ��һ��˫�ֵ�Ԫ,���ڱ���״̬����,�ο������λ˵��
        // λ7-λ0��ӦCH341��D7-D0����
        // λ8��ӦCH341��ERR#����, λ9��ӦCH341��PEMP����, λ10��ӦCH341��INT#����, λ11��ӦCH341��SLCT����, λ23��ӦCH341��SDA����
        // λ13��ӦCH341��BUSY/WAIT#����, λ14��ӦCH341��AUTOFD#/DATAS#����,λ15��ӦCH341��SLCTIN#/ADDRS#����

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341ReadI2C(uint iIndex, byte iDevice, byte iAddr, out byte oByte);

        // ��I2C�ӿڶ�ȡһ���ֽ�����
        // iIndex ָ��CH341�豸���
        // iDevice ��7λָ��I2C�豸��ַ
        // iAddr ָ�����ݵ�Ԫ�ĵ�ַ
        // oByte ָ��һ���ֽڵ�Ԫ,���ڱ����ȡ���ֽ�����

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341WriteI2C(uint iIndex, byte iDevice, byte iAddr, byte iByte);

        // ��I2C�ӿ�д��һ���ֽ�����
        // iIndex ָ��CH341�豸���
        // iDevice ��7λָ��I2C�豸��ַ
        // iAddr ָ�����ݵ�Ԫ�ĵ�ַ
        // iByte ��д����ֽ�����

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341EppReadData(uint iIndex, byte[] oBuffer, ref uint ioLength);
        // EPP��ʽ������: WR#=1, DS#=0, AS#=1, D0-D7=input
        // iIndex ָ��CH341�豸���
        // oBuffer ָ��һ���㹻��Ļ�����,���ڱ����ȡ������
        // ioLength ָ�򳤶ȵ�Ԫ,����ʱΪ׼����ȡ�ĳ���,���غ�Ϊʵ�ʶ�ȡ�ĳ���

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341EppReadAddr(uint iIndex, byte[] oBuffer, ref uint ioLength);
        // EPP��ʽ����ַ: WR#=1, DS#=1, AS#=0, D0-D7=input
        // iIndex ָ��CH341�豸���
        // oBuffer ָ��һ���㹻��Ļ�����,���ڱ����ȡ�ĵ�ַ����
        // ioLength ָ�򳤶ȵ�Ԫ,����ʱΪ׼����ȡ�ĳ���,���غ�Ϊʵ�ʶ�ȡ�ĳ���

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341EppWriteData(uint iIndex, byte[] iBuffer, ref uint ioLength);
        // EPP��ʽд����: WR#=0, DS#=0, AS#=1, D0-D7=output
        // iIndex ָ��CH341�豸���
        // iBuffer ָ��һ��������,����׼��д��������
        // ioLength ָ�򳤶ȵ�Ԫ,����ʱΪ׼��д���ĳ���,���غ�Ϊʵ��д���ĳ���

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341EppWriteAddr(uint iIndex, byte[] iBuffer, ref uint ioLength);
        // EPP��ʽд��ַ: WR#=0, DS#=1, AS#=0, D0-D7=output
        // iIndex ָ��CH341�豸���
        // iBuffer ָ��һ��������,����׼��д���ĵ�ַ����
        // ioLength ָ�򳤶ȵ�Ԫ,����ʱΪ׼��д���ĳ���,���غ�Ϊʵ��д���ĳ���

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341EppSetAddr(uint iIndex, byte iAddr);
        // EPP��ʽ���õ�ַ: WR#=0, DS#=1, AS#=0, D0-D7=output
        // iIndex ָ��CH341�豸���
        // iAddr ָ��EPP��ַ

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341MemReadAddr0(uint iIndex, byte[] oBuffer, ref uint ioLength);
        // MEM��ʽ����ַ0: WR#=1, DS#/RD#=0, AS#/ADDR=0, D0-D7=input
        // iIndex ָ��CH341�豸���
        // oBuffer ָ��һ���㹻��Ļ�����,���ڱ���ӵ�ַ0��ȡ������
        // ioLength ָ�򳤶ȵ�Ԫ,����ʱΪ׼����ȡ�ĳ���,���غ�Ϊʵ�ʶ�ȡ�ĳ���

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341MemReadAddr1(uint iIndex, byte[] oBuffer, ref uint ioLength);
        // MEM��ʽ����ַ1: WR#=1, DS#/RD#=0, AS#/ADDR=1, D0-D7=input
        // iIndex ָ��CH341�豸���
        // oBuffer ָ��һ���㹻��Ļ�����,���ڱ���ӵ�ַ1��ȡ������
        // ioLength ָ�򳤶ȵ�Ԫ,����ʱΪ׼����ȡ�ĳ���,���غ�Ϊʵ�ʶ�ȡ�ĳ���

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341MemWriteAddr0(uint iIndex, byte[] iBuffer, ref uint ioLength);
        // MEM��ʽд��ַ0: WR#=0, DS#/RD#=1, AS#/ADDR=0, D0-D7=output
        // iIndex ָ��CH341�豸���
        // iBuffer ָ��һ��������,����׼�����ַ0д��������
        // ioLength ָ�򳤶ȵ�Ԫ,����ʱΪ׼��д���ĳ���,���غ�Ϊʵ��д���ĳ���

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341MemWriteAddr1(uint iIndex, byte[] iBuffer, ref uint ioLength);
        // MEM��ʽд��ַ1: WR#=0, DS#/RD#=1, AS#/ADDR=1, D0-D7=output
        // iIndex ָ��CH341�豸���
        // iBuffer ָ��һ��������,����׼�����ַ1д��������
        // ioLength ָ�򳤶ȵ�Ԫ,����ʱΪ׼��д���ĳ���,���غ�Ϊʵ��д���ĳ���

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341SetExclusive(uint iIndex, uint iExclusive);
        // ���ö�ռʹ�õ�ǰCH341�豸
        // iIndex ָ��CH341�豸���
        // iExclusive Ϊ0���豸���Թ���ʹ��,��0���ռʹ��

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341SetTimeout(uint iIndex, uint iWriteTimeout, uint iReadTimeout);
        // ����USB���ݶ�д�ĳ�ʱ
        // iIndex ָ��CH341�豸���
        // iWriteTimeout ָ��USBд�����ݿ�ĳ�ʱʱ��,�Ժ���mSΪ��λ,0xFFFFFFFFָ������ʱ(Ĭ��ֵ)
        // iReadTimeout ָ��USB��ȡ���ݿ�ĳ�ʱʱ��,�Ժ���mSΪ��λ,0xFFFFFFFFָ������ʱ(Ĭ��ֵ)

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341ReadData(uint iIndex, byte[] oBuffer, ref uint ioLength);
        // ��ȡ���ݿ�
        // iIndex ָ��CH341�豸���
        // oBuffer ָ��һ���㹻��Ļ�����,���ڱ����ȡ������
        // ioLength ָ�򳤶ȵ�Ԫ,����ʱΪ׼����ȡ�ĳ���,���غ�Ϊʵ�ʶ�ȡ�ĳ���

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341WriteData(uint iIndex, byte[] iBuffer, ref uint ioLength);
        // д�����ݿ�
        // iIndex ָ��CH341�豸���
        // iBuffer ָ��һ��������,����׼��д��������
        // ioLength ָ�򳤶ȵ�Ԫ,����ʱΪ׼��д���ĳ���,���غ�Ϊʵ��д���ĳ���

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341GetDeviceName(uint iIndex);
        // ����ָ��CH341�豸���ƵĻ�����,�����򷵻�NULL
        // iIndex ָ��CH341�豸���,0��Ӧ��һ���豸

        [DllImport("CH341DLL.DLL")]
        public static extern uint CH341GetVerIC(uint iIndex);
        // ��ȡCH341оƬ�İ汾,����:0=�豸��Ч,0x10=CH341,0x20=CH341A,0x30=CH341T
        // iIndex ָ��CH341�豸���

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341FlushBuffer(uint iIndex);
        // ���CH341�Ļ�����
        //iIndex ָ��CH341�豸���

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341WriteRead(uint iIndex, uint iWriteLength, Byte[] iWriteBuffer, uint iReadStep, uint iReadTimes, out uint oReadLength, Byte[] oReadBuffer);
        // ִ������������,�����������
        // iIndex ָ��CH341�豸���
        // iWriteLength д����,׼��д���ĳ���
        // iWriteBuffer ָ��һ��������,����׼��д��������
        // iReadStep ׼����ȡ�ĵ�����ĳ���, ׼����ȡ���ܳ���Ϊ(iReadStep*iReadTimes)
        // iReadTimes ׼����ȡ�Ĵ���
        // oReadLength ָ�򳤶ȵ�Ԫ,���غ�Ϊʵ�ʶ�ȡ�ĳ���
        // oReadBuffer ָ��һ���㹻��Ļ�����,���ڱ����ȡ������

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341SetStream(uint iIndex, uint iMode);
        // ���ô�����ģʽ
        // iIndex ָ��CH341�豸���
        // iMode ָ��ģʽ,������
        // λ1-λ0: I2C�ӿ��ٶ�/SCLƵ��, 00=����/20KHz,01=��׼/100KHz(Ĭ��ֵ),10=����/400KHz,11=����/750KHz
        // λ2:     SPI��I/O��/IO����, 0=���뵥��(D3ʱ��/D5��/D7��)(Ĭ��ֵ),1=˫��˫��(D3ʱ��/D5��D4��/D7��D6��)
        // λ7:     SPI�ֽ��е�λ˳��, 0=��λ��ǰ, 1=��λ��ǰ
        // ��������,����Ϊ0

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341SetDelaymS(uint iIndex, uint iDelay);
        // ����Ӳ���첽��ʱ,���ú�ܿ췵��,������һ��������֮ǰ��ʱָ��������
        // iIndex ָ��CH341�豸���
        // iDelay ָ����ʱ�ĺ�����

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341StreamI2C(uint iIndex, uint iWriteLength, Byte[] iWriteBuffer, uint iReadLength, Byte[] oReadBuffer);
        // ����I2C������,2�߽ӿ�,ʱ����ΪSCL����,������ΪSDA����(׼˫��I/O),�ٶ�Լ56K�ֽ�
        // iIndex ָ��CH341�豸���
        // iWriteLength ׼��д���������ֽ���
        // iWriteBuffer ָ��һ��������,����׼��д��������,���ֽ�ͨ����I2C�豸��ַ����д����λ
        // iReadLength ׼����ȡ�������ֽ���
        // oReadBuffer ָ��һ��������,���غ��Ƕ��������

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341ReadEEPROM(uint iIndex, uint iEepromID, uint iAddr, uint iLength, Byte[] oBuffer);
        // ��EEPROM�ж�ȡ���ݿ�,�ٶ�Լ56K�ֽ�
        // iIndex ָ��CH341�豸���
        // iEepromID ָ��EEPROM�ͺ�
        // iAddr ָ�����ݵ�Ԫ�ĵ�ַ
        // iLength ׼����ȡ�������ֽ���
        // oBuffer ָ��һ��������,���غ��Ƕ��������

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341WriteEEPROM(uint iIndex, uint iEepromID, uint iAddr, uint iLength, Byte[] iBuffer);
        // ��EEPROM��д�����ݿ�
        // iIndex ָ��CH341�豸���
        // iEepromID ָ��EEPROM�ͺ�
        // iAddr ָ�����ݵ�Ԫ�ĵ�ַ
        // iLength ׼��д���������ֽ���
        // iBuffer ָ��һ��������,����׼��д��������

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341GetInput(uint iIndex, out uint iStatus);
        // ͨ��CH341ֱ���������ݺ�״̬(��������),Ч�ʱ�CH341GetStatus����
        // iIndex ָ��CH341�豸���
        // iStatus ָ��һ��˫��(U32)��Ԫ,���ڱ���״̬����,�ο������λ˵��
        // λ7-λ0��ӦCH341��D7-D0����
        // λ8��ӦCH341��ERR#����, λ9��ӦCH341��PEMP����, λ10��ӦCH341��INT#����, λ11��ӦCH341��SLCT����, λ23��ӦCH341��SDA����
        // λ13��ӦCH341��BUSY/WAIT#����, λ14��ӦCH341��AUTOFD#/DATAS#����,λ15��ӦCH341��SLCTIN#/ADDRS#����

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341SetOutput(uint iIndex, uint iEnable, uint iSetDirOut, uint iSetDataOut);
        // ����CH341��I/O����,��ͨ��CH341ֱ���������
        /* ***** ����ʹ�ø�API, ��ֹ�޸�I/O����ʹ�������ű�Ϊ������ŵ����������������֮���·����оƬ ***** */
        // iIndex ָ��CH341�豸���
        // iEnable ������Ч��־,�ο������λ˵��
        // λ0Ϊ1˵��iSetDataOut��λ15-λ8��Ч,�������
        // λ1Ϊ1˵��iSetDirOut��λ15-λ8��Ч,�������
        // λ2Ϊ1˵��iSetDataOut��7-λ0��Ч,�������
        // λ3Ϊ1˵��iSetDirOut��λ7-λ0��Ч,�������
        // λ4Ϊ1˵��iSetDataOut��λ23-λ16��Ч,�������(λ23-λ16�������÷���)
        // iSetDirOut ����I/O����,ĳλ��0���Ӧ����Ϊ����,ĳλ��1���Ӧ����Ϊ���,���ڷ�ʽ��Ĭ��ֵΪ0x000FC000,�ο������λ˵��
        // iSetDataOut �������,���I/O����Ϊ���,��ôĳλ��0ʱ��Ӧ��������͵�ƽ,ĳλ��1ʱ��Ӧ��������ߵ�ƽ,�ο������λ˵��
        // λ7-λ0��ӦCH341��D7-D0����
        // λ8��ӦCH341��TXD/ERR#����, λ9��ӦCH341��RXD/PEMP����, λ10��ӦCH341��INT#/ACK#/INI#����, λ11��ӦCH341��IN3/SLCT����
        // λ13��ӦCH341��TEN#/BUSY#/WAIT#����, λ14��ӦCH341��ROV#/AFD#/DS#����,λ15��ӦCH341��IN7/SIN#/AS#����
        // ��������ֻ�����,������I/O����: λ16��ӦCH341��TNOW/INI#/RST#����, λ17��ӦCH341��RDY#/STB#/WR#����
        // λ18��ӦCH341��SCL����, λ19(29�Ǵ����)��ӦCH341��SDA����,λ23��ӦSDA��ֵ

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341Set_D5_D0(uint iIndex, uint iSetDirOut, uint iSetDataOut);
        // ����CH341��D5-D0���ŵ�I/O����,��ͨ��CH341��D5-D0����ֱ���������,Ч�ʱ�CH341SetOutput����
        /* ***** ����ʹ�ø�API, ��ֹ�޸�I/O����ʹ�������ű�Ϊ������ŵ����������������֮���·����оƬ ***** */
        // iIndex ָ��CH341�豸���
        // iSetDirOut ����D5-D0�����ŵ�I/O����,ĳλ��0���Ӧ����Ϊ����,ĳλ��1���Ӧ����Ϊ���,���ڷ�ʽ��Ĭ��ֵΪ0x00ȫ������
        // iSetDataOut ����D5-D0�����ŵ��������,���I/O����Ϊ���,��ôĳλ��0ʱ��Ӧ��������͵�ƽ,ĳλ��1ʱ��Ӧ��������ߵ�ƽ
        // �������ݵ�λ5-λ0�ֱ��ӦCH341��D5-D0����

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341StreamSPI3(uint iIndex, uint iChipSelect, uint iLength, Byte[] ioBuffer);

        // ����SPI������,3�߽ӿ�,ʱ����ΪDCK2/SCL����,������ΪDIO/SDA����(׼˫��I/O),Ƭѡ��ΪD0/D1/D2,�ٶ�Լ51K�ֽ�
        /* SPIʱ��: DCK2/SCL����Ϊʱ�����, Ĭ��Ϊ�͵�ƽ, DIO/SDA������ʱ��������֮ǰ���, DIO/SDA������ʱ���½���֮������ */
        // iIndex ָ��CH341�豸���
        // iChipSelect Ƭѡ����, λ7Ϊ0�����Ƭѡ����, λ7Ϊ1�������Ч: λ1λ0Ϊ00/01/10�ֱ�ѡ��D0/D1/D2������Ϊ�͵�ƽ��ЧƬѡ
        // iLength ׼������������ֽ���
        // ioBuffer ָ��һ��������,����׼����DIOд��������,���غ��Ǵ�DIO���������

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341StreamSPI4(uint iIndex, uint iChipSelect, uint iLength, Byte[] ioBuffer);
        // ����SPI������,4�߽ӿ�,ʱ����ΪDCK/D3����,���������ΪDOUT/D5����,����������ΪDIN/D7����,Ƭѡ��ΪD0/D1/D2,�ٶ�Լ68K�ֽ�
        /* SPIʱ��: DCK/D3����Ϊʱ�����, Ĭ��Ϊ�͵�ƽ, DOUT/D5������ʱ��������֮ǰ���, DIN/D7������ʱ���½���֮������ */
        // iIndex ָ��CH341�豸���
        // iChipSelect Ƭѡ����, λ7Ϊ0�����Ƭѡ����, λ7Ϊ1�������Ч: λ1λ0Ϊ00/01/10�ֱ�ѡ��D0/D1/D2������Ϊ�͵�ƽ��ЧƬѡ
        // iLength ׼������������ֽ���
        // ioBuffer ָ��һ��������,����׼����DOUTд��������,���غ��Ǵ�DIN���������

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341StreamSPI5(uint iIndex, uint iChipSelect, uint iLength, Byte[] ioBuffer, Byte[] ioBuffer2);
        // ����SPI������,5�߽ӿ�,ʱ����ΪDCK/D3����,���������ΪDOUT/D5��DOUT2/D4����,����������ΪDIN/D7��DIN2/D6����,Ƭѡ��ΪD0/D1/D2,�ٶ�Լ30K�ֽ�*2
        /* SPIʱ��: DCK/D3����Ϊʱ�����, Ĭ��Ϊ�͵�ƽ, DOUT/D5��DOUT2/D4������ʱ��������֮ǰ���, DIN/D7��DIN2/D6������ʱ���½���֮������ */
        // ָ��CH341�豸���
        // iChipSelect Ƭѡ����, λ7Ϊ0�����Ƭѡ����, λ7Ϊ1�������Ч: λ1λ0Ϊ00/01/10�ֱ�ѡ��D0/D1/D2������Ϊ�͵�ƽ��ЧƬѡ
        // iLength ׼������������ֽ���
        // ioBuffer ָ��һ��������,����׼����DOUTд��������,���غ��Ǵ�DIN���������
        // ioBuffer2 ָ��ڶ���������,����׼����DOUT2д��������,���غ��Ǵ�DIN2���������

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341BitStreamSPI(uint iIndex, uint iLength, Byte[] ioBuffer);

        // ����SPIλ������,4��/5�߽ӿ�,ʱ����ΪDCK/D3����,���������ΪDOUT/DOUT2����,����������ΪDIN/DIN2����,Ƭѡ��ΪD0/D1/D2,�ٶ�Լ8Kλ*2
        // iIndex ָ��CH341�豸���
        // iLength ׼�����������λ��,һ�����896,���鲻����256
        // ioBuffer ָ��һ��������,����׼����DOUT/DOUT2/D2-D0д��������,���غ��Ǵ�DIN/DIN2���������
        /* SPIʱ��: DCK/D3����Ϊʱ�����, Ĭ��Ϊ�͵�ƽ, DOUT/D5��DOUT2/D4������ʱ��������֮ǰ���, DIN/D7��DIN2/D6������ʱ���½���֮������ */
        /* ioBuffer�е�һ���ֽڹ�8λ�ֱ��ӦD7-D0����, λ5�����DOUT, λ4�����DOUT2, λ2-λ0�����D2-D0, λ7��DIN����, λ6��DIN2����, λ3���ݺ��� */
        /* �ڵ��ø�API֮ǰ,Ӧ���ȵ���CH341Set_D5_D0����CH341��D5-D0���ŵ�I/O����,���������ŵ�Ĭ�ϵ�ƽ */

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341SetBufUpload(uint iIndex, uint iEnableOrClear);
        // �趨�ڲ������ϴ�ģʽ
        // iIndex ָ��CH341�豸���,0��Ӧ��һ���豸
        // iEnableOrClear Ϊ0���ֹ�ڲ������ϴ�ģʽ,ʹ��ֱ���ϴ�,��0�������ڲ������ϴ�ģʽ������������е���������
        // ��������ڲ������ϴ�ģʽ,��ôCH341�������򴴽��߳��Զ�����USB�ϴ����ݵ��ڲ�������,ͬʱ����������е���������,��Ӧ�ó������CH341ReadData���������ػ������е���������

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341QueryBufUpload(uint iIndex);
        // ��ѯ�ڲ��ϴ��������е��������ݰ�����,�ɹ��������ݰ�����,������-1
        //iIndex ָ��CH341�豸���,0��Ӧ��һ���豸

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341SetBufDownload(uint iIndex, uint iEnableOrClear);
        // �趨�ڲ������´�ģʽ
        // iIndex ָ��CH341�豸���,0��Ӧ��һ���豸
        // iEnableOrClear Ϊ0���ֹ�ڲ������´�ģʽ,ʹ��ֱ���´�,��0�������ڲ������´�ģʽ������������е���������
        // ��������ڲ������´�ģʽ,��ô��Ӧ�ó������CH341WriteData�󽫽����ǽ�USB�´����ݷŵ��ڲ�����������������,����CH341�������򴴽����߳��Զ�����ֱ�����

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341QueryBufDownload(uint iIndex);
        // ��ѯ�ڲ��´��������е�ʣ�����ݰ�����(��δ����),�ɹ��������ݰ�����,������-1
        // iIndex ָ��CH341�豸���,0��Ӧ��һ���豸

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341ResetInter(uint iIndex);
        // ��λ�ж����ݶ�����
        // iIndex ָ��CH341�豸���

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341ResetRead(uint iIndex);
        // ��λ���ݿ������
        // iIndex ָ��CH341�豸���

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341ResetWrite(uint iIndex);
        // ��λ���ݿ�д����
        // iIndex ָ��CH341�豸���

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341SetDeviceNotify(uint iIndex, Char[] iDeviceID, mPCH341_NOTIFY_ROUTINE iNotifyRoutine);
        // �趨�豸�¼�֪ͨ����
        // iIndex ָ��CH341�豸���,0��Ӧ��һ���豸
        // iDeviceID ��ѡ����,ָ���ַ���,ָ������ص��豸��ID,�ַ�����\0��ֹ
        // iNotifyRoutine ָ���豸�¼��ص�����,ΪNULL��ȡ���¼�֪ͨ,�����ڼ�⵽�¼�ʱ���øó���

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341SetupSerial(uint iIndex, uint iParityMode, uint iBaudRate);
        // �趨CH341�Ĵ�������,��APIֻ�����ڹ����ڴ��ڷ�ʽ��CH341оƬ
        // iIndex ָ��CH341�豸���,0��Ӧ��һ���豸
        // iParityMode ָ��CH341���ڵ�����У��ģʽ: NOPARITY/ODDPARITY/EVENPARITY/MARKPARITY/SPACEPARITY
        // iBaudRate ָ��CH341���ڵ�ͨѶ������ֵ,������50��3000000֮�������ֵ


        /*  ����API�������ڹ����ڴ��ڷ�ʽ��CH341оƬ,����֮���APIһ��ֻ�����ڲ��ڷ�ʽ��CH341оƬ
            CH341OpenDevice
            CH341CloseDevice
            CH341SetupSerial
            CH341ReadData
            CH341WriteData
            CH341SetBufUpload
            CH341QueryBufUpload
            CH341SetBufDownload
            CH341QueryBufDownload
            CH341SetDeviceNotify
            CH341GetStatus
        //  ��������ҪAPI,�����Ǵ�ҪAPI
            CH341GetVersion
            CH341DriverCommand
            CH341GetDrvVersion
            CH341ResetDevice
            CH341GetDeviceDescr
            CH341GetConfigDescr
            CH341SetIntRoutine
            CH341ReadInter
            CH341AbortInter
            CH341AbortRead
            CH341AbortWrite
            CH341ReadI2C
            CH341WriteI2C
            CH341SetExclusive
            CH341SetTimeout
            CH341GetDeviceName
            CH341GetVerIC
            CH341FlushBuffer
            CH341WriteRead
            CH341ResetInter
            CH341ResetRead
            CH341ResetWrite
        */
        // V1.1
    }
}
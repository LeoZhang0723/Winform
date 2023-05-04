// 2004.05.28, 2004.10.20, 2005.01.08, 2005.03.25, 2005.04.28, 2005.07.18, 2005.07.28, 2005.09.19
//****************************************
//**  Copyright  (C)  W.ch  1999-2005   **
//**  Web:  http://www.winchiphead.com  **
//****************************************
//**  DLL for USB interface chip CH341  **
//**  C, VC5.0                          **
//****************************************
//
// USB总线接口芯片CH341并口应用层接口库 V1.9
// 南京沁恒电子有限公司  作者: W.ch 2005.09
// CH341-DLL  V1.9
// 运行环境: Windows 98/ME, Windows 2000/XP
// support USB chip: CH341, CH341A
// USB => Parallel, I2C, SPI, JTAG ...
//
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using NTSTATUS = System.Int32;				// 返回状态 

namespace ClsLib
{
    internal static class CH341DLL
    // public static class CH341DLL
    {
#pragma warning disable  169 // 禁用所有警告
        //public struct mUspValue
        //{
        //    public byte mUspValueLow;              // 02H 值参数低字节
        //    public byte mUspValueHigh;             // 03H 值参数高字节
        //}
        //public struct mUspIndex
        //{
        //    public byte mUspIndexLow;              // 04H 索引参数低字节
        //    public byte mUspIndexHigh;             // 05H 索引参数高字节
        //}
        //public struct USB_SETUP_PKT // USB控制传输的建立阶段的数据请求包结构
        //{
        //    public byte mUspReqType;               // 00H 请求类型
        //    public byte mUspRequest;               // 01H 请求代码
        //    public mUspValue mUspValue;            // 02H-03H 值参数
        //    public mUspIndex mUspIndex;            // 04H-05H 索引参数
        //    public int mLength;                    // 06H-07H 数据阶段的数据长度
        //}
        public struct WIN32_COMMAND // 定义WIN32命令接口结构
        {
            //public uint mFunction;               // 输入时指定功能代码或者管道号
            ////输出时返回操作状态
            //public uint mLength;                 // 存取长度,返回后续数据的长度
            //public byte[] mBuffer;              // mUSBIO_PACKET_LENGTH - 1 '数据缓冲区,长度为0至255B
        }
#pragma warning restore 169 // 启用所有警告

        public const uint FILE_DEVICE_UNKNOWN = 0x22;
        public const uint FILE_ANY_ACCESS = 0;
        public const uint METHOD_BUFFERED = 0;
        // WIN32应用层接口命令
        public const uint IOCTL_USBIO_COMMAND = (FILE_DEVICE_UNKNOWN * (2 ^ 16) + FILE_ANY_ACCESS * 2 ^ 14 + 0xF34 * 2 ^ 2 + METHOD_BUFFERED);    // 专用接口
        public const uint mWIN32_COMMAND_HEAD = 8;              // WIN32命令接口的头长度

        // 数据长度定义
        public const byte mCH341_BUCK_PACKET_LENGTH = 32;	    // CH341支持的数据包(批量传输最大)的长度
        public const byte mCH341_CTRL_PACKET_LENGTH = 8;		// CH341支持的短数据包(控制传输)的长度
        public const uint mCH341_MAX_NUMBER = 16;				// 最多同时连接的CH341数
        public const uint mMAX_BUFFER_LENGTH = 4096;		    // 数据缓冲区最大长度4096
        public const uint mMAX_COMMAND_LENGTH = (mWIN32_COMMAND_HEAD + mMAX_BUFFER_LENGTH);          // 最大数据长度加上命令结构头的长度
        public const uint mDEFAULT_BUFFER_LEN = 1024;		    // 数据缓冲区默认长度1024
        public const uint mDEFAULT_COMMAND_LEN = (mWIN32_COMMAND_HEAD + mDEFAULT_BUFFER_LEN);      // 默认数据长度加上命令结构头的长度

        // CH341端点地址
        public const byte mCH341_ENDP_INTER_UP = 0x81;		    // CH341的中断数据上传端点的地址
        public const byte mCH341_ENDP_INTER_DOWN = 0x01;		// CH341的中断数据下传端点的地址
        public const byte mCH341_ENDP_DATA_UP = 0x82;			// CH341的数据块上传端点的地址
        public const byte mCH341_ENDP_DATA_DOWN = 0x02;		    // CH341的数据块下传端点的地址

        // 设备层接口提供的管道操作命令
        public const uint mPipeDeviceCtrl = 0x00000004;	    // CH341的综合控制管道
        public const uint mPipeInterUp = 0x00000005;		// CH341的中断数据上传管道
        public const uint mPipeDataUp = 0x00000006;		    // CH341的数据块上传管道
        public const uint mPipeDataDown = 0x00000007;		// CH341的数据块下传管道

        // 应用层接口的功能代码
        public const uint mFuncNoOperation = 0x00000000;	// 无操作
        public const uint mFuncGetVersion = 0x00000001;	    // 获取驱动程序版本号
        public const uint mFuncGetConfig = 0x00000002;	    // 获取USB设备配置描述符
        public const uint mFuncSetTimeout = 0x00000009;	    // 设置USB通讯超时
        public const uint mFuncSetExclusive = 0x0000000b;	// 设置独占使用
        public const uint mFuncResetDevice = 0x0000000c;	// 复位USB设备
        public const uint mFuncResetPipe = 0x0000000d;	    // 复位USB管道
        public const uint mFuncAbortPipe = 0x0000000e;	    // 取消USB管道的数据请求

        // CH341并口专用的功能代码
        public const uint mFuncSetParaMode = 0x0000000f;	// 设置并口模式
        public const uint mFuncReadData0 = 0x00000010;	    // 从并口读取数据块0
        public const uint mFuncReadData1 = 0x00000011;	    // 从并口读取数据块1
        public const uint mFuncWriteData0 = 0x00000012; 	// 向并口写入数据块0
        public const uint mFuncWriteData1 = 0x00000013;	    // 向并口写入数据块1
        public const uint mFuncWriteRead = 0x00000014;	    // 先输出再输入
        public const uint mFuncBufferMode = 0x00000020; 	// 设定缓冲上传模式及查询缓冲区中的数据长度
        public const uint mFuncBufferModeDn = 0x00000021;	// 设定缓冲下传模式及查询缓冲区中的数据长度

        // USB设备标准请求代码
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

        // CH341控制传输的厂商专用请求类型
        public const byte mCH341_VENDOR_READ = 0xC0;		// 通过控制传输实现的CH341厂商专用读操作
        public const byte mCH341_VENDOR_WRITE = 0x40;		// 通过控制传输实现的CH341厂商专用写操作

        // CH341控制传输的厂商专用请求代码
        public const byte mCH341_PARA_INIT = 0xB1;			// 初始化并口
        public const byte mCH341_I2C_STATUS = 0x52;			// 获取I2C接口的状态
        public const byte mCH341_I2C_COMMAND = 0x53;		// 发出I2C接口的命令

        // CH341并口操作命令代码
        public const byte mCH341_PARA_CMD_R0 = 0xAC;		// 从并口读数据0
        public const byte mCH341_PARA_CMD_R1 = 0xAD;		// 从并口读数据1
        public const byte mCH341_PARA_CMD_W0 = 0xA6;		// 向并口写数据0
        public const byte mCH341_PARA_CMD_W1 = 0xA7;		// 向并口写数据1
        public const byte mCH341_PARA_CMD_STS = 0xA0;		// 获取并口状态

        // CH341A批量传输起始命令代码
        public const byte mCH341A_CMD_SET_OUTPUT = 0xA1;		// 设置并口输出
        public const byte mCH341A_CMD_IO_ADDR = 0xA2;			// MEM带地址读写/输入输出,从次字节开始为命令流
        public const byte mCH341A_CMD_PRINT_OUT = 0xA3;			// PRINT兼容打印方式输出,从次字节开始为数据流
        public const byte mCH341A_CMD_SPI_STREAM = 0xA8;		// SPI接口的命令包,从次字节开始为数据流
        public const byte mCH341A_CMD_SIO_STREAM = 0xA9;		// SIO接口的命令包,从次字节开始为数据流
        public const byte mCH341A_CMD_I2C_STREAM = 0xAA;		// I2C接口的命令包,从次字节开始为I2C命令流
        public const byte mCH341A_CMD_UIO_STREAM = 0xAB;		// UIO接口的命令包,从次字节开始为命令流

        // CH341A控制传输的厂商专用请求代码
        public const byte mCH341A_BUF_CLEAR = 0xB2;				// 清除未完成的数据
        public const byte mCH341A_I2C_CMD_X = 0x54;				// 发出I2C接口的命令,立即执行
        public const byte mCH341A_DELAY_MS = 0x5E;				// 以亳秒为单位延时指定时间
        public const byte mCH341A_GET_VER = 0x5F;				// 获取芯片版本
        public const byte mCH341_EPP_IO_MAX = (mCH341_BUCK_PACKET_LENGTH - 1);// CH341在EPP/MEM方式下单次读写数据块的最大长度
        public const byte mCH341A_EPP_IO_MAX = 0xFF;			// CH341A在EPP/MEM方式下单次读写数据块的最大长度
        public const byte mCH341A_CMD_IO_ADDR_W = 0x00;			// MEM带地址读写/输入输出的命令流:写数据,位6-位0为地址,下一个字节为待写数据
        public const byte mCH341A_CMD_IO_ADDR_R = 0x80;			// MEM带地址读写/输入输出的命令流:读数据,位6-位0为地址,读出数据一起返回
        public const byte mCH341A_CMD_GET_INPUT = 0xA0;			// 自定义获取端口状态

        // I2C接口的命令流:
        public const byte mCH341A_CMD_I2C_STM_STA = 0x74;		// 产生起始位
        public const byte mCH341A_CMD_I2C_STM_STO = 0x75;		// 产生停止位
        public const byte mCH341A_CMD_I2C_STM_OUT = 0x80;		// 输出数据,位5-位0为长度,后续字节为数据,0长度则只发送一个字节并返回应答
        public const byte mCH341A_CMD_I2C_STM_IN = 0xC0;		// 输入数据,位5-位0为长度,0长度则只接收一个字节并发送无应答
        public const byte mCH341A_CMD_I2C_STM_MAX = 0x3F < mCH341_BUCK_PACKET_LENGTH ? 0x3F : mCH341_BUCK_PACKET_LENGTH;	// 单个命令输入输出数据的最大长度
        public const byte mCH341A_CMD_I2C_STM_SET = 0x60;		// 设置参数,位2=SPI的I/O数(0=单入单出,1=双入双出),位1位0=I2C速度(00=低速,01=标准,10=快速,11=高速)
        public const byte mCH341A_CMD_I2C_STM_US = 0x40;		// 以微秒为单位延时,位3-位0为延时值
        public const byte mCH341A_CMD_I2C_STM_MS = 0x50;		// 以亳秒为单位延时,位3-位0为延时值
        public const byte mCH341A_CMD_I2C_STM_DLY = 0x0F;		// 单个命令延时的最大值
        public const byte mCH341A_CMD_I2C_STM_END = 0x00;		// 命令包提前结束

        // UIO接口的命令流:
        public const byte mCH341A_CMD_UIO_STM_IN = 0x00;		// 输入数据D7-D0
        public const byte mCH341A_CMD_UIO_STM_DIR = 0x40;		// 设定I/O方向D5-D0,位5-位0为方向数据
        public const byte mCH341A_CMD_UIO_STM_OUT = 0x80;		// 输出数据D5-D0,位5-位0为数据
        public const byte mCH341A_CMD_UIO_STM_US = 0xC0;		// 以微秒为单位延时,位5-位0为延时值
        public const byte mCH341A_CMD_UIO_STM_END = 0x20;		// 命令包提前结束

        // CH341并口工作模式
        public const byte mCH341_PARA_MODE_EPP = 0x00;			// CH341并口工作模式为EPP方式
        public const byte mCH341_PARA_MODE_EPP17 = 0x00;		// CH341A并口工作模式为EPP方式V1.7
        public const byte mCH341_PARA_MODE_EPP19 = 0x01;		// CH341A并口工作模式为EPP方式V1.9
        public const byte mCH341_PARA_MODE_MEM = 0x02;			// CH341并口工作模式为MEM方式


        // I/O方向设置位定义,直接输入的状态信号的位定义,直接输出的位数据定义
        public const uint mStateBitERR = 0x00000100;		// 只读可写,ERR#引脚输入状态,1:高电平,0:低电平
        public const uint mStateBitPEMP = 0x00000200;		// 只读可写,PEMP引脚输入状态,1:高电平,0:低电平
        public const uint mStateBitINT = 0x00000400;		// 只读可写,INT#引脚输入状态,1:高电平,0:低电平
        public const uint mStateBitSLCT = 0x00000800;		// 只读可写,SLCT引脚输入状态,1:高电平,0:低电平
        public const uint mStateBitWAIT = 0x00002000;		// 只读可写,WAIT#引脚输入状态,1:高电平,0:低电平
        public const uint mStateBitDATAS = 0x00004000;	    // 只写可读,DATAS#/READ#引脚输入状态,1:高电平,0:低电平
        public const uint mStateBitADDRS = 0x00008000;	    // 只写可读,ADDRS#/ADDR/ALE引脚输入状态,1:高电平,0:低电平
        public const uint mStateBitRESET = 0x00010000;	    // 只写,RESET#引脚输入状态,1:高电平,0:低电平
        public const uint mStateBitWRITE = 0x00020000;	    // 只写,WRITE#引脚输入状态,1:高电平,0:低电平
        public const uint mStateBitSDA = 0x00800000;		// 只读,SDA引脚输入状态,1:高电平,0:低电平，注意：位19为设置，位23为状态

        public const uint MAX_DEVICE_PATH_SIZE = 128;		// 设备名称的最大字符数
        public const uint MAX_DEVICE_ID_SIZE = 64;		    // 设备ID的最大字符数

        public delegate void mPCH341_INT_ROUTINE(uint iStatus);
        // 中断服务程序
        // iStatus 中断状态数据,参考下面的位说明
        // 位7-位0对应CH341的D7-D0引脚
        // 位8对应CH341的ERR#引脚, 位9对应CH341的PEMP引脚, 位10对应CH341的INT#引脚, 位11对应CH341的SLCT引脚

        public delegate void mPCH341_NOTIFY_ROUTINE(uint iEventStatus);
        // 设备事件通知回调程序
        // 设备事件和当前状态(在下行定义): 0=设备拔出事件, 3=设备插入事件

        public const uint CH341_DEVICE_ARRIVAL = 3;	// 设备插入事件,已经插入
        public const uint CH341_DEVICE_REMOVE_PEND = 1;	// 设备将要拔出
        public const uint CH341_DEVICE_REMOVE = 0;	// 设备拔出事件,已经拔出

        [DllImport("CH341DLL.DLL")]
        public static extern IntPtr CH341OpenDevice(uint iIndex);
        // 打开CH341设备,返回句柄,出错则无效
        // iIndex 指定CH341设备序号,0对应第一个设备

        [DllImport("CH341DLL.DLL")]
        public static extern void CH341CloseDevice(uint iIndex);
        // 关闭CH341设备
        // iIndex 指定CH341设备序号

        [DllImport("CH341DLL.DLL")]
        public static extern uint CH341GetVersion();  // 获得DLL版本号,返回版本号

        [DllImport("CH341DLL.DLL")]
        private static extern uint CH341DriverCommand(uint iIndex, WIN32_COMMAND ioCommand);
        // 直接传递命令给驱动程序,出错则返回0,否则返回数据长度
        // iIndex   指定CH341设备序号,V1.6以上DLL也可以是设备打开后的句柄
        // ioCommand   命令结构的指针
        // 该程序在调用后返回数据长度,并且仍然返回命令结构,如果是读操作,则数据返回在命令结构中,
        // 返回的数据长度在操作失败时为0,操作成功时为整个命令结构的长度,例如读一个字节,则返回mWIN32_COMMAND_HEAD+1,
        // 命令结构在调用前,分别提供:管道号或者命令功能代码,存取数据的长度(可选),数据(可选)
        // 命令结构在调用后,分别返回:操作状态代码,后续数据的长度(可选),
        // 操作状态代码是由WINDOWS定义的代码,可以参考NTSTATUS.H,
        // 后续数据的长度是指读操作返回的数据长度,数据存放在随后的缓冲区中,对于写操作一般为0

        [DllImport("CH341DLL.DLL")]
        public static extern uint CH341GetDrvVersion();  // 获得驱动程序版本号,返回版本号,出错则返回0

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341ResetDevice(uint iIndex);
        // 复位USB设备
        // iIndex 指定CH341设备序号

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341GetDeviceDescr(uint iIndex, byte[] oBuffer, ref uint ioLength);
        // 读取设备描述符
        // iIndex 指定CH341设备序号
        // oBuffer 指向一个足够大的缓冲区,用于保存描述符
        // ioLength 指向长度单元,输入时为准备读取的长度,返回后为实际读取的长度

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341GetConfigDescr(uint iIndex, byte[] oBuffer, ref uint ioLength);
        // 读取配置描述符
        // iIndex 指定CH341设备序号
        // oBuffer 指向一个足够大的缓冲区,用于保存描述符
        // ioLength 指向长度单元,输入时为准备读取的长度,返回后为实际读取的长度

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341SetIntRoutine(uint iIndex, mPCH341_INT_ROUTINE iIntRoutine);
        // 设定中断服务程序
        // iIndex 指定CH341设备序号
        // iIntRoutine 指定中断服务程序,为NULL则取消中断服务,否则在中断时调用该程序

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341ReadInter(uint iIndex, out uint oStatus);
        // 读取中断数据
        // iIndex 指定CH341设备序号
        // oStatus 指向一个双字单元,用于保存读取的中断状态数据,见下行
        // 位7-位0对应CH341的D7-D0引脚
        // 位8对应CH341的ERR#引脚, 位9对应CH341的PEMP引脚, 位10对应CH341的INT#引脚, 位11对应CH341的SLCT引脚

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341AbortInter(uint iIndex);
        // 放弃中断数据读操作
        // iIndex 指定CH341设备序号

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341SetParaMode(uint iIndex, uint iMode);
        // 设置并口模式
        // iIndex 指定CH341设备序号
        // iMode 指定并口模式: 0为EPP模式/EPP模式V1.7, 1为EPP模式V1.9, 2为MEM模式

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341InitParallel(uint iIndex, uint iMode);
        // 复位并初始化并口,RST#输出低电平脉冲
        // iIndex 指定CH341设备序号
        // iMode 指定并口模式: 0为EPP模式/EPP模式V1.7, 1为EPP模式V1.9, 2为MEM模式, >= 0x00000100 保持当前模式

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341ReadData0(uint iIndex, byte[] oBuffer, ref uint ioLength);

        // 从0#端口读取数据块
        // iIndex 指定CH341设备序号
        // oBuffer 指向一个足够大的缓冲区,用于保存读取的数据
        // ioLength 指向长度单元,输入时为准备读取的长度,返回后为实际读取的长度


        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341ReadData1(uint iIndex, byte[] oBuffer, ref uint ioLength);

        // 从1#端口读取数据块
        // iIndex 指定CH341设备序号
        // oBuffer 指向一个足够大的缓冲区,用于保存读取的数据
        // ioLength 指向长度单元,输入时为准备读取的长度,返回后为实际读取的长度

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341AbortRead(uint iIndex);
        // 放弃数据块读操作
        // iIndex 指定CH341设备序号

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341WriteData0(uint iIndex, byte[] iBuffer, ref uint ioLength);
        // 向0#端口写出数据块
        // iIndex 指定CH341设备序号
        // iBuffer 指向一个缓冲区,放置准备写出的数据
        // ioLength 指向长度单元,输入时为准备写出的长度,返回后为实际写出的长度


        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341WriteData1(uint iIndex, byte[] iBuffer, ref uint ioLength);
        // 向1#端口写出数据块
        // iIndex 指定CH341设备序号
        // iBuffer 指向一个缓冲区,放置准备写出的数据
        // ioLength 指向长度单元,输入时为准备写出的长度,返回后为实际写出的长度

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341AbortWrite(uint iIndex);
        // 放弃数据块写操作
        // iIndex 指定CH341设备序号

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341GetStatus(uint iIndex, out uint oStatus);
        // 通过CH341直接输入数据和状态(控制传输)
        // iIndex 指定CH341设备序号
        // oStatus 指向一个双字单元,用于保存状态数据,参考下面的位说明
        // 位7-位0对应CH341的D7-D0引脚
        // 位8对应CH341的ERR#引脚, 位9对应CH341的PEMP引脚, 位10对应CH341的INT#引脚, 位11对应CH341的SLCT引脚, 位23对应CH341的SDA引脚
        // 位13对应CH341的BUSY/WAIT#引脚, 位14对应CH341的AUTOFD#/DATAS#引脚,位15对应CH341的SLCTIN#/ADDRS#引脚

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341ReadI2C(uint iIndex, byte iDevice, byte iAddr, out byte oByte);

        // 从I2C接口读取一个字节数据
        // iIndex 指定CH341设备序号
        // iDevice 低7位指定I2C设备地址
        // iAddr 指定数据单元的地址
        // oByte 指向一个字节单元,用于保存读取的字节数据

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341WriteI2C(uint iIndex, byte iDevice, byte iAddr, byte iByte);

        // 向I2C接口写入一个字节数据
        // iIndex 指定CH341设备序号
        // iDevice 低7位指定I2C设备地址
        // iAddr 指定数据单元的地址
        // iByte 待写入的字节数据

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341EppReadData(uint iIndex, byte[] oBuffer, ref uint ioLength);
        // EPP方式读数据: WR#=1, DS#=0, AS#=1, D0-D7=input
        // iIndex 指定CH341设备序号
        // oBuffer 指向一个足够大的缓冲区,用于保存读取的数据
        // ioLength 指向长度单元,输入时为准备读取的长度,返回后为实际读取的长度

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341EppReadAddr(uint iIndex, byte[] oBuffer, ref uint ioLength);
        // EPP方式读地址: WR#=1, DS#=1, AS#=0, D0-D7=input
        // iIndex 指定CH341设备序号
        // oBuffer 指向一个足够大的缓冲区,用于保存读取的地址数据
        // ioLength 指向长度单元,输入时为准备读取的长度,返回后为实际读取的长度

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341EppWriteData(uint iIndex, byte[] iBuffer, ref uint ioLength);
        // EPP方式写数据: WR#=0, DS#=0, AS#=1, D0-D7=output
        // iIndex 指定CH341设备序号
        // iBuffer 指向一个缓冲区,放置准备写出的数据
        // ioLength 指向长度单元,输入时为准备写出的长度,返回后为实际写出的长度

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341EppWriteAddr(uint iIndex, byte[] iBuffer, ref uint ioLength);
        // EPP方式写地址: WR#=0, DS#=1, AS#=0, D0-D7=output
        // iIndex 指定CH341设备序号
        // iBuffer 指向一个缓冲区,放置准备写出的地址数据
        // ioLength 指向长度单元,输入时为准备写出的长度,返回后为实际写出的长度

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341EppSetAddr(uint iIndex, byte iAddr);
        // EPP方式设置地址: WR#=0, DS#=1, AS#=0, D0-D7=output
        // iIndex 指定CH341设备序号
        // iAddr 指定EPP地址

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341MemReadAddr0(uint iIndex, byte[] oBuffer, ref uint ioLength);
        // MEM方式读地址0: WR#=1, DS#/RD#=0, AS#/ADDR=0, D0-D7=input
        // iIndex 指定CH341设备序号
        // oBuffer 指向一个足够大的缓冲区,用于保存从地址0读取的数据
        // ioLength 指向长度单元,输入时为准备读取的长度,返回后为实际读取的长度

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341MemReadAddr1(uint iIndex, byte[] oBuffer, ref uint ioLength);
        // MEM方式读地址1: WR#=1, DS#/RD#=0, AS#/ADDR=1, D0-D7=input
        // iIndex 指定CH341设备序号
        // oBuffer 指向一个足够大的缓冲区,用于保存从地址1读取的数据
        // ioLength 指向长度单元,输入时为准备读取的长度,返回后为实际读取的长度

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341MemWriteAddr0(uint iIndex, byte[] iBuffer, ref uint ioLength);
        // MEM方式写地址0: WR#=0, DS#/RD#=1, AS#/ADDR=0, D0-D7=output
        // iIndex 指定CH341设备序号
        // iBuffer 指向一个缓冲区,放置准备向地址0写出的数据
        // ioLength 指向长度单元,输入时为准备写出的长度,返回后为实际写出的长度

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341MemWriteAddr1(uint iIndex, byte[] iBuffer, ref uint ioLength);
        // MEM方式写地址1: WR#=0, DS#/RD#=1, AS#/ADDR=1, D0-D7=output
        // iIndex 指定CH341设备序号
        // iBuffer 指向一个缓冲区,放置准备向地址1写出的数据
        // ioLength 指向长度单元,输入时为准备写出的长度,返回后为实际写出的长度

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341SetExclusive(uint iIndex, uint iExclusive);
        // 设置独占使用当前CH341设备
        // iIndex 指定CH341设备序号
        // iExclusive 为0则设备可以共享使用,非0则独占使用

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341SetTimeout(uint iIndex, uint iWriteTimeout, uint iReadTimeout);
        // 设置USB数据读写的超时
        // iIndex 指定CH341设备序号
        // iWriteTimeout 指定USB写出数据块的超时时间,以毫秒mS为单位,0xFFFFFFFF指定不超时(默认值)
        // iReadTimeout 指定USB读取数据块的超时时间,以毫秒mS为单位,0xFFFFFFFF指定不超时(默认值)

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341ReadData(uint iIndex, byte[] oBuffer, ref uint ioLength);
        // 读取数据块
        // iIndex 指定CH341设备序号
        // oBuffer 指向一个足够大的缓冲区,用于保存读取的数据
        // ioLength 指向长度单元,输入时为准备读取的长度,返回后为实际读取的长度

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341WriteData(uint iIndex, byte[] iBuffer, ref uint ioLength);
        // 写出数据块
        // iIndex 指定CH341设备序号
        // iBuffer 指向一个缓冲区,放置准备写出的数据
        // ioLength 指向长度单元,输入时为准备写出的长度,返回后为实际写出的长度

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341GetDeviceName(uint iIndex);
        // 返回指向CH341设备名称的缓冲区,出错则返回NULL
        // iIndex 指定CH341设备序号,0对应第一个设备

        [DllImport("CH341DLL.DLL")]
        public static extern uint CH341GetVerIC(uint iIndex);
        // 获取CH341芯片的版本,返回:0=设备无效,0x10=CH341,0x20=CH341A,0x30=CH341T
        // iIndex 指定CH341设备序号

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341FlushBuffer(uint iIndex);
        // 清空CH341的缓冲区
        //iIndex 指定CH341设备序号

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341WriteRead(uint iIndex, uint iWriteLength, Byte[] iWriteBuffer, uint iReadStep, uint iReadTimes, out uint oReadLength, Byte[] oReadBuffer);
        // 执行数据流命令,先输出再输入
        // iIndex 指定CH341设备序号
        // iWriteLength 写长度,准备写出的长度
        // iWriteBuffer 指向一个缓冲区,放置准备写出的数据
        // iReadStep 准备读取的单个块的长度, 准备读取的总长度为(iReadStep*iReadTimes)
        // iReadTimes 准备读取的次数
        // oReadLength 指向长度单元,返回后为实际读取的长度
        // oReadBuffer 指向一个足够大的缓冲区,用于保存读取的数据

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341SetStream(uint iIndex, uint iMode);
        // 设置串口流模式
        // iIndex 指定CH341设备序号
        // iMode 指定模式,见下行
        // 位1-位0: I2C接口速度/SCL频率, 00=低速/20KHz,01=标准/100KHz(默认值),10=快速/400KHz,11=高速/750KHz
        // 位2:     SPI的I/O数/IO引脚, 0=单入单出(D3时钟/D5出/D7入)(默认值),1=双入双出(D3时钟/D5出D4出/D7入D6入)
        // 位7:     SPI字节中的位顺序, 0=低位在前, 1=高位在前
        // 其它保留,必须为0

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341SetDelaymS(uint iIndex, uint iDelay);
        // 设置硬件异步延时,调用后很快返回,而在下一个流操作之前延时指定毫秒数
        // iIndex 指定CH341设备序号
        // iDelay 指定延时的毫秒数

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341StreamI2C(uint iIndex, uint iWriteLength, Byte[] iWriteBuffer, uint iReadLength, Byte[] oReadBuffer);
        // 处理I2C数据流,2线接口,时钟线为SCL引脚,数据线为SDA引脚(准双向I/O),速度约56K字节
        // iIndex 指定CH341设备序号
        // iWriteLength 准备写出的数据字节数
        // iWriteBuffer 指向一个缓冲区,放置准备写出的数据,首字节通常是I2C设备地址及读写方向位
        // iReadLength 准备读取的数据字节数
        // oReadBuffer 指向一个缓冲区,返回后是读入的数据

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341ReadEEPROM(uint iIndex, uint iEepromID, uint iAddr, uint iLength, Byte[] oBuffer);
        // 从EEPROM中读取数据块,速度约56K字节
        // iIndex 指定CH341设备序号
        // iEepromID 指定EEPROM型号
        // iAddr 指定数据单元的地址
        // iLength 准备读取的数据字节数
        // oBuffer 指向一个缓冲区,返回后是读入的数据

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341WriteEEPROM(uint iIndex, uint iEepromID, uint iAddr, uint iLength, Byte[] iBuffer);
        // 向EEPROM中写入数据块
        // iIndex 指定CH341设备序号
        // iEepromID 指定EEPROM型号
        // iAddr 指定数据单元的地址
        // iLength 准备写出的数据字节数
        // iBuffer 指向一个缓冲区,放置准备写出的数据

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341GetInput(uint iIndex, out uint iStatus);
        // 通过CH341直接输入数据和状态(批量传输),效率比CH341GetStatus更高
        // iIndex 指定CH341设备序号
        // iStatus 指向一个双字(U32)单元,用于保存状态数据,参考下面的位说明
        // 位7-位0对应CH341的D7-D0引脚
        // 位8对应CH341的ERR#引脚, 位9对应CH341的PEMP引脚, 位10对应CH341的INT#引脚, 位11对应CH341的SLCT引脚, 位23对应CH341的SDA引脚
        // 位13对应CH341的BUSY/WAIT#引脚, 位14对应CH341的AUTOFD#/DATAS#引脚,位15对应CH341的SLCTIN#/ADDRS#引脚

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341SetOutput(uint iIndex, uint iEnable, uint iSetDirOut, uint iSetDataOut);
        // 设置CH341的I/O方向,并通过CH341直接输出数据
        /* ***** 谨慎使用该API, 防止修改I/O方向使输入引脚变为输出引脚导致与其它输出引脚之间短路而损坏芯片 ***** */
        // iIndex 指定CH341设备序号
        // iEnable 数据有效标志,参考下面的位说明
        // 位0为1说明iSetDataOut的位15-位8有效,否则忽略
        // 位1为1说明iSetDirOut的位15-位8有效,否则忽略
        // 位2为1说明iSetDataOut的7-位0有效,否则忽略
        // 位3为1说明iSetDirOut的位7-位0有效,否则忽略
        // 位4为1说明iSetDataOut的位23-位16有效,否则忽略(位23-位16不能设置方向)
        // iSetDirOut 设置I/O方向,某位清0则对应引脚为输入,某位置1则对应引脚为输出,并口方式下默认值为0x000FC000,参考下面的位说明
        // iSetDataOut 输出数据,如果I/O方向为输出,那么某位清0时对应引脚输出低电平,某位置1时对应引脚输出高电平,参考下面的位说明
        // 位7-位0对应CH341的D7-D0引脚
        // 位8对应CH341的TXD/ERR#引脚, 位9对应CH341的RXD/PEMP引脚, 位10对应CH341的INT#/ACK#/INI#引脚, 位11对应CH341的IN3/SLCT引脚
        // 位13对应CH341的TEN#/BUSY#/WAIT#引脚, 位14对应CH341的ROV#/AFD#/DS#引脚,位15对应CH341的IN7/SIN#/AS#引脚
        // 以下引脚只能输出,不考虑I/O方向: 位16对应CH341的TNOW/INI#/RST#引脚, 位17对应CH341的RDY#/STB#/WR#引脚
        // 位18对应CH341的SCL引脚, 位19(29是错误的)对应CH341的SDA引脚,位23对应SDA读值

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341Set_D5_D0(uint iIndex, uint iSetDirOut, uint iSetDataOut);
        // 设置CH341的D5-D0引脚的I/O方向,并通过CH341的D5-D0引脚直接输出数据,效率比CH341SetOutput更高
        /* ***** 谨慎使用该API, 防止修改I/O方向使输入引脚变为输出引脚导致与其它输出引脚之间短路而损坏芯片 ***** */
        // iIndex 指定CH341设备序号
        // iSetDirOut 设置D5-D0各引脚的I/O方向,某位清0则对应引脚为输入,某位置1则对应引脚为输出,并口方式下默认值为0x00全部输入
        // iSetDataOut 设置D5-D0各引脚的输出数据,如果I/O方向为输出,那么某位清0时对应引脚输出低电平,某位置1时对应引脚输出高电平
        // 以上数据的位5-位0分别对应CH341的D5-D0引脚

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341StreamSPI3(uint iIndex, uint iChipSelect, uint iLength, Byte[] ioBuffer);

        // 处理SPI数据流,3线接口,时钟线为DCK2/SCL引脚,数据线为DIO/SDA引脚(准双向I/O),片选线为D0/D1/D2,速度约51K字节
        /* SPI时序: DCK2/SCL引脚为时钟输出, 默认为低电平, DIO/SDA引脚在时钟上升沿之前输出, DIO/SDA引脚在时钟下降沿之后输入 */
        // iIndex 指定CH341设备序号
        // iChipSelect 片选控制, 位7为0则忽略片选控制, 位7为1则参数有效: 位1位0为00/01/10分别选择D0/D1/D2引脚作为低电平有效片选
        // iLength 准备传输的数据字节数
        // ioBuffer 指向一个缓冲区,放置准备从DIO写出的数据,返回后是从DIO读入的数据

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341StreamSPI4(uint iIndex, uint iChipSelect, uint iLength, Byte[] ioBuffer);
        // 处理SPI数据流,4线接口,时钟线为DCK/D3引脚,输出数据线为DOUT/D5引脚,输入数据线为DIN/D7引脚,片选线为D0/D1/D2,速度约68K字节
        /* SPI时序: DCK/D3引脚为时钟输出, 默认为低电平, DOUT/D5引脚在时钟上升沿之前输出, DIN/D7引脚在时钟下降沿之后输入 */
        // iIndex 指定CH341设备序号
        // iChipSelect 片选控制, 位7为0则忽略片选控制, 位7为1则参数有效: 位1位0为00/01/10分别选择D0/D1/D2引脚作为低电平有效片选
        // iLength 准备传输的数据字节数
        // ioBuffer 指向一个缓冲区,放置准备从DOUT写出的数据,返回后是从DIN读入的数据

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341StreamSPI5(uint iIndex, uint iChipSelect, uint iLength, Byte[] ioBuffer, Byte[] ioBuffer2);
        // 处理SPI数据流,5线接口,时钟线为DCK/D3引脚,输出数据线为DOUT/D5和DOUT2/D4引脚,输入数据线为DIN/D7和DIN2/D6引脚,片选线为D0/D1/D2,速度约30K字节*2
        /* SPI时序: DCK/D3引脚为时钟输出, 默认为低电平, DOUT/D5和DOUT2/D4引脚在时钟上升沿之前输出, DIN/D7和DIN2/D6引脚在时钟下降沿之后输入 */
        // 指定CH341设备序号
        // iChipSelect 片选控制, 位7为0则忽略片选控制, 位7为1则参数有效: 位1位0为00/01/10分别选择D0/D1/D2引脚作为低电平有效片选
        // iLength 准备传输的数据字节数
        // ioBuffer 指向一个缓冲区,放置准备从DOUT写出的数据,返回后是从DIN读入的数据
        // ioBuffer2 指向第二个缓冲区,放置准备从DOUT2写出的数据,返回后是从DIN2读入的数据

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341BitStreamSPI(uint iIndex, uint iLength, Byte[] ioBuffer);

        // 处理SPI位数据流,4线/5线接口,时钟线为DCK/D3引脚,输出数据线为DOUT/DOUT2引脚,输入数据线为DIN/DIN2引脚,片选线为D0/D1/D2,速度约8K位*2
        // iIndex 指定CH341设备序号
        // iLength 准备传输的数据位数,一次最多896,建议不超过256
        // ioBuffer 指向一个缓冲区,放置准备从DOUT/DOUT2/D2-D0写出的数据,返回后是从DIN/DIN2读入的数据
        /* SPI时序: DCK/D3引脚为时钟输出, 默认为低电平, DOUT/D5和DOUT2/D4引脚在时钟上升沿之前输出, DIN/D7和DIN2/D6引脚在时钟下降沿之后输入 */
        /* ioBuffer中的一个字节共8位分别对应D7-D0引脚, 位5输出到DOUT, 位4输出到DOUT2, 位2-位0输出到D2-D0, 位7从DIN输入, 位6从DIN2输入, 位3数据忽略 */
        /* 在调用该API之前,应该先调用CH341Set_D5_D0设置CH341的D5-D0引脚的I/O方向,并设置引脚的默认电平 */

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341SetBufUpload(uint iIndex, uint iEnableOrClear);
        // 设定内部缓冲上传模式
        // iIndex 指定CH341设备序号,0对应第一个设备
        // iEnableOrClear 为0则禁止内部缓冲上传模式,使用直接上传,非0则启用内部缓冲上传模式并清除缓冲区中的已有数据
        // 如果启用内部缓冲上传模式,那么CH341驱动程序创建线程自动接收USB上传数据到内部缓冲区,同时清除缓冲区中的已有数据,当应用程序调用CH341ReadData后将立即返回缓冲区中的已有数据

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341QueryBufUpload(uint iIndex);
        // 查询内部上传缓冲区中的已有数据包个数,成功返回数据包个数,出错返回-1
        //iIndex 指定CH341设备序号,0对应第一个设备

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341SetBufDownload(uint iIndex, uint iEnableOrClear);
        // 设定内部缓冲下传模式
        // iIndex 指定CH341设备序号,0对应第一个设备
        // iEnableOrClear 为0则禁止内部缓冲下传模式,使用直接下传,非0则启用内部缓冲下传模式并清除缓冲区中的已有数据
        // 如果启用内部缓冲下传模式,那么当应用程序调用CH341WriteData后将仅仅是将USB下传数据放到内部缓冲区并立即返回,而由CH341驱动程序创建的线程自动发送直到完毕

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341QueryBufDownload(uint iIndex);
        // 查询内部下传缓冲区中的剩余数据包个数(尚未发送),成功返回数据包个数,出错返回-1
        // iIndex 指定CH341设备序号,0对应第一个设备

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341ResetInter(uint iIndex);
        // 复位中断数据读操作
        // iIndex 指定CH341设备序号

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341ResetRead(uint iIndex);
        // 复位数据块读操作
        // iIndex 指定CH341设备序号

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341ResetWrite(uint iIndex);
        // 复位数据块写操作
        // iIndex 指定CH341设备序号

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341SetDeviceNotify(uint iIndex, Char[] iDeviceID, mPCH341_NOTIFY_ROUTINE iNotifyRoutine);
        // 设定设备事件通知程序
        // iIndex 指定CH341设备序号,0对应第一个设备
        // iDeviceID 可选参数,指向字符串,指定被监控的设备的ID,字符串以\0终止
        // iNotifyRoutine 指定设备事件回调程序,为NULL则取消事件通知,否则在检测到事件时调用该程序

        [DllImport("CH341DLL.DLL")]
        public static extern bool CH341SetupSerial(uint iIndex, uint iParityMode, uint iBaudRate);
        // 设定CH341的串口特性,该API只能用于工作于串口方式的CH341芯片
        // iIndex 指定CH341设备序号,0对应第一个设备
        // iParityMode 指定CH341串口的数据校验模式: NOPARITY/ODDPARITY/EVENPARITY/MARKPARITY/SPACEPARITY
        // iBaudRate 指定CH341串口的通讯波特率值,可以是50至3000000之间的任意值


        /*  以下API可以用于工作于串口方式的CH341芯片,除此之外的API一般只能用于并口方式的CH341芯片
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
        //  以上是主要API,以下是次要API
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
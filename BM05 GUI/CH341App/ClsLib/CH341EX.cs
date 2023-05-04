/* 本程序涉及到
 1、2线接口的一些自定义时序,处理IIC总线的应答位,以及了解2线接口的内部时序分析
 2、提供例子程序,操作2线接口IIC器件X76F640、PCF8574、PCA9554
 3、用EPP或者MEM并口进行多位输出或者多位输入,模拟只读或者只写的SPI时序
 4、用UIO通用I/O位流命令实现自定义的同步串行接口
 5、提供例子程序,操作类似SPI的非标准串行时序的器件TLC1549
 6、提供例子程序,通过CH341StreamSPI4操作4线接口SPI器件25C512、25C020
 7、提供例子程序,通过CH341BitStreamSPI操作类似SPI的非标准串行时序的器件ADC0831
 另外可以用CH341SetOutput设置CH341的I/O方向,并通过CH341的任何一个引脚直接输出数据,未提供例子,建议用CH341Set_D5_D0代替
*/

/* CH341并口驱动及DLL的API层次,按从低向高分为
 1、CH341DriverCommand直接传给WDM驱动程序层
 2、CH341WriteData只写数据, CH341ReadData只读数据, CH341WriteRead先写数据再读数据
 3、CH341StreamI2C先写IIC,可选地再读IIC (内部调用CH341WriteData和CH341WriteRead)
	CH341StreamSPI进行SPI传输,读写都是可选的 (内部调用CH341WriteRead)
 4、CH341ReadEEPROM读EEPROM数据, CH341WriteEEPROM写EEPROM数据 (内部调用CH341StreamI2C)
 本例子中的子程序将调用CH341WriteData、CH341WriteRead、CH341StreamI2C等DLL中的API */

/* 实测速度
   CH341StreamI2C     约56K字节
   CH341ReadEEPROM    约56K字节
   CH341WriteEEPROM   约5K字节(如果是RAM而非闪存那么与CH341ReadEEPROM速度相同)
   CH341StreamSPI4    约68K字节
   CH341StreamSPI5    每路约30K字节  * 2路输入和2路输出
   CH341BitStreamSPI  每路约8K位     * 至少2路输入和2路输出(最多7路输入4路输出)
*/

/* ********************************************************************************************** */
/* 例子:兼容IIC总线的通用操作时序 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ClsLib
{
    internal static class CH341EX
    {
        private static uint portVal = 0;
        private static uint portDir = 0;

        private static byte[] outBuf = new byte[CH341DLL.mCH341_BUCK_PACKET_LENGTH];
        private static byte[] inBuf = new byte[CH341DLL.mCH341_BUCK_PACKET_LENGTH];

        public static CH341DLL.mPCH341_NOTIFY_ROUTINE usbio_notify; // 在此库中以静态形式申明可以一事件（托管）多响应（消息）

        //*******************************************************************************************
        // 返回参数简单，异常消息单步提示，函数易用。
        //*******************************************************************************************

        public static bool InitialDevice(uint iIndex) // 初始化驱动设备
        {
            try
            {
                IntPtr ptr = CH341DLL.CH341OpenDevice(iIndex); // 如果驱动没有安装，则会发生异常中断。如果没有驱动器，返回值-1。
                if (ptr == (IntPtr)(-1))
                {
                    if (!ErrHdl.ErrorHandle(ErrHdl.ErrEnum.ErrDllCall, null)) { return false; } // 没有找到USB-I2C工具
                }
                if (!CH341DLL.CH341SetDeviceNotify(iIndex, null, usbio_notify))
                {
                    if (!ErrHdl.ErrorHandle(ErrHdl.ErrEnum.SysErr, null)) { return false; } // 绑定拔插消息
                }
                if (!CH341DLL.CH341ResetDevice(iIndex))
                {
                    if (!ErrHdl.ErrorHandle(ErrHdl.ErrEnum.SysErr, null)) { return false; } // 重置端口及设备
                }
                if (!CH341DLL.CH341SetTimeout(iIndex, 10000, 10000))
                {
                    if (!ErrHdl.ErrorHandle(ErrHdl.ErrEnum.SysErr, null)) { return false; }  // 设置超时参数
                }
            }
            catch (Exception)
            {
                CH341DLL.CH341CloseDevice(iIndex);
                if (!ErrHdl.ErrorHandle(ErrHdl.ErrEnum.ErrDllCall, null)) { return false; } // 动态库调用错误
            }
            return true;
        }
        private static bool IIC_IssueStart(uint iIndex) // 产生起始位
        {
            uint outLen = 3;
            outBuf[0] = CH341DLL.mCH341A_CMD_I2C_STREAM;  // 命令码
            outBuf[1] = CH341DLL.mCH341A_CMD_I2C_STM_STA;  // 产生起始位
            outBuf[2] = CH341DLL.mCH341A_CMD_I2C_STM_END;  // 当前包提前结束
            if (!CH341DLL.CH341WriteData(iIndex, outBuf, ref outLen)) { return false; } // 写出数据块
            return outLen == 3;
        }
        private static bool IIC_IssueStop(uint iIndex) // 产生停止位  
        {
            uint outLen = 3;
            outBuf[0] = CH341DLL.mCH341A_CMD_I2C_STREAM;  // 命令码
            outBuf[1] = CH341DLL.mCH341A_CMD_I2C_STM_STO;  // 产生停止位
            outBuf[2] = CH341DLL.mCH341A_CMD_I2C_STM_END;  // 当前包提前结束
            if (!CH341DLL.CH341WriteData(iIndex, outBuf, ref outLen)) { return false; }  // 写出数据块
            return outLen == 3;
        }

        private static bool IIC_OutBlockSkipAck(  // 输出数据块,不检查应答
            uint iIndex, // 指定CH341设备序号
            uint iOutLength, // 准备写出的数据字节数,单次必须小于29字节
            byte[] ibuckBuffer) // 指向一个缓冲区,放置准备写出的数据
        {
            if (iOutLength == 0 || iOutLength + 3 > (CH341DLL.mCH341_BUCK_PACKET_LENGTH)) { return false; }
            uint outLen = 1 + 1 + iOutLength + 1;
            outBuf[0] = CH341DLL.mCH341A_CMD_I2C_STREAM;  // 命令码
            outBuf[1] = (byte)(CH341DLL.mCH341A_CMD_I2C_STM_OUT | iOutLength);  // 输出数据,位5-位0为长度
            for (int i = 0; i < iOutLength; i++)
            {
                outBuf[i + 2] = (byte)ibuckBuffer[i];  // 数据						
            }
            outBuf[1 + 1 + iOutLength] = CH341DLL.mCH341A_CMD_I2C_STM_END;  // 当前包提前结束
            if (!CH341DLL.CH341WriteData(iIndex, outBuf, ref outLen)) { return false; }  // 写出数据块
            return outLen == iOutLength + 3;
        }

        private static bool IIC_OutByteCheckAck( // 输出一字节数据并检查SDA状态(应答是否有效)
            uint iIndex, // 指定CH341设备序号
            byte iOutByte,
            out bool oAck) // 准备写出的数据
        {
            oAck = false;
            uint outLen = 4;
            outBuf[0] = CH341DLL.mCH341A_CMD_I2C_STREAM;  // 命令码
            outBuf[1] = CH341DLL.mCH341A_CMD_I2C_STM_OUT;  // 输出数据,位5-位0为长度,0长度则只发送一个字节并返回应答
            outBuf[2] = iOutByte;  // 数据
            outBuf[3] = CH341DLL.mCH341A_CMD_I2C_STM_END;  // 当前包提前结束
            if (!CH341DLL.CH341WriteData(iIndex, outBuf, ref outLen)) { return false; }  // 写出数据块
            uint inLen = 1;
            if (!CH341DLL.CH341ReadData(iIndex, inBuf, ref inLen)) { return false; }  // 执行数据流命令,先输出再输入
            if (inLen != 1) { return false; } // 检查返回数据长度
            oAck = (inBuf[0] & 0x80) == 0; // 返回的位7代表SDA状态，ACK为SDA=0, NACK为SDA=1
            return true;
        }

        private static bool IIC_InBlockByAck(  // 输入数据块,每输入一个字节都产生有效应答
            uint iIndex,  // 指定CH341设备序号
            uint iInLength,  // 准备读取的数据字节数,单次必须小于32字节
            ref byte[] oInBuffer)  // 指向一个缓冲区,返回后是读入的数据
        {
            if (iInLength == 0 || iInLength > CH341DLL.mCH341A_CMD_I2C_STM_MAX) { return false; }
            uint outLen = 3;
            outBuf[0] = CH341DLL.mCH341A_CMD_I2C_STREAM;  // 命令码
            outBuf[1] = (byte)(CH341DLL.mCH341A_CMD_I2C_STM_IN | iInLength);  // 输入数据,位5-位0为长度
            outBuf[2] = CH341DLL.mCH341A_CMD_I2C_STM_END;  // 当前包提前结束
            if (!CH341DLL.CH341WriteData(iIndex, outBuf, ref outLen)) { return false; }  // 写出数据块
            uint inLen = iInLength;
            if (!CH341DLL.CH341ReadData(iIndex, inBuf, ref inLen)) { return false; }  // 执行数据流命令,先输出再输入
            if (inLen != iInLength) { return false; } // 检查返回数据长度
            for (int i = 0; i < iInLength; i++)
            {
                oInBuffer[i] = inBuf[i];  // 数据						
            }
            return true;
        }

        private static bool IIC_InByteByAck(  // 输入数据块,每输入一个字节都产生有效应答
            uint iIndex,  // 指定CH341设备序号
            out byte oByte)  // 指向一个缓冲区,返回后是读入的数据
        {
            oByte = 0;
            uint outLen = 3;
            outBuf[0] = CH341DLL.mCH341A_CMD_I2C_STREAM;  // 命令码
            outBuf[1] = (byte)(CH341DLL.mCH341A_CMD_I2C_STM_IN | 1);  // 输入数据,位5-位0为长度,长度为1
            outBuf[2] = CH341DLL.mCH341A_CMD_I2C_STM_END;  // 当前包提前结束
            if (!CH341DLL.CH341WriteData(iIndex, outBuf, ref outLen)) { return false; }  // 写出数据块
            uint inLen = 1;
            if (!CH341DLL.CH341ReadData(iIndex, inBuf, ref inLen)) { return false; }  // 执行数据流命令,先输出再输入
            if (inLen != 1) { return false; } // 检查返回数据长度
            oByte = inBuf[0];  // 数据
            return true;
        }

        private static bool IIC_InByteNoAck(  // 输入一字节数据,但是不产生应答
            uint iIndex,  // 指定CH341设备序号
            out byte oByte)  // 指向一个字节,返回后是读入的数据
        {
            oByte = 0;
            uint outLen = 3;
            outBuf[0] = CH341DLL.mCH341A_CMD_I2C_STREAM;  // 命令码
            outBuf[1] = CH341DLL.mCH341A_CMD_I2C_STM_IN;  // 输入数据,位5-位0为长度,0长度则只接收一个字节并发送无应答
            outBuf[2] = CH341DLL.mCH341A_CMD_I2C_STM_END;  // 当前包提前结束
            if (!CH341DLL.CH341WriteData(iIndex, outBuf, ref outLen)) { return false; }  // 写出数据块
            uint inLen = 1;
            if (!CH341DLL.CH341ReadData(iIndex, inBuf, ref inLen)) { return false; }  // 执行数据流命令,先输出再输入
            if (inLen != 1) { return false; } // 检查返回数据长度
            oByte = inBuf[0];  // 数据
            return true;
        }

        // 检查当前地址ACK状态
        public static bool CheckAddr(uint iIndex, uint iChkAddr, out bool oAckStat)
        {
            bool idleStat;
            oAckStat = false;
            SetSDAStatus(iIndex, true); // 空闲总线，先释放SDA，最释放SCL
            SetSCLStatus(iIndex, true);
            if (!GetBusStatus(iIndex, out idleStat)) { return false; } //I2C总线状态true/false = Idle/Busy
            if (!idleStat)
            {
                SetSDAStatus(iIndex, true); // 空闲总线，先释放SDA，最释放SCL
                SetSCLStatus(iIndex, true);
                if (!ErrHdl.ErrorHandle(ErrHdl.ErrEnum.BusBusy, null)) { return false; } // I2C总线忙状态
            }
            if (!IIC_IssueStart(iIndex)) { return false; } // 开始位
            if (!IIC_OutByteCheckAck(iIndex, (byte)(iChkAddr << 1), out oAckStat)) { return false; } // 检查ACK／NACK
            if (!IIC_IssueStop(iIndex)) { return false; } // 停止位
            return true;
        }

        //public static bool CH341StreamI2C32Byte(  // 处理I2C数据流,2线接口,时钟线为SCL引脚,数据线为SDA引脚(准双向I/O),速度约56K字节
        //uint iIndex,  // 指定CH341设备序号
        //uint iWriteLength,  // 准备写出的数据字节数
        //Byte[] iWriteBuffer,  // 指向一个缓冲区,放置准备写出的数据,首字节通常是I2C设备地址及读写方向位
        //uint iReadLength,  // 准备读取的数据字节数			
        //ref Byte[] oReadBuffer,
        //out StatusCode oStatusCode)  // 指向一个缓冲区,返回后是读入的数据
        //{
        //    bool ackStat;
        //    if (iWriteLength > 1) // 检查是否需要写地址和数据
        //    {
        //        // 开始位
        //        if (!IIC_IssueStart(iIndex)) { IIC_IssueStop(iIndex); return false; }
        //        iWriteBuffer[0] &= 0xFE;//确认地址位为写
        //        for (int i = 0; i < (int)iWriteLength; i++)
        //        {
        //            IIC_OutByteCheckAck(iIndex, iWriteBuffer[i], out ackStat);
        //            if (!ackStat)
        //            {
        //                if (i == 0)
        //                {
        //                    MessageBox.Show(ErrMsg.AdrWrNACK()); // 发送地址写错误
        //                }
        //                else
        //                {
        //                    MessageBox.Show(ErrMsg.DatWrNACK()); // 发送数据错误
        //                }
        //                IIC_IssueStop(iIndex);
        //                return false;
        //            }
        //        }
        //    }
        //    // 检查是否需要读数据
        //    if (iReadLength > 0)
        //    {
        //        // 重启动
        //        if (!IIC_IssueStart(iIndex)) return false;
        //        iWriteBuffer[0] |= 0x01;// 确认地址位为读
        //        IIC_OutByteCheckAck(iIndex, iWriteBuffer[0], out ackStat);// 发送位址+读,然后开始读操作
        //        if (!ackStat)
        //        {
        //            //MessageBox.Show(ErrMsg(AdrReNACK()); // 发送地址读错误
        //            IIC_IssueStop(iIndex);
        //            return false;
        //        }
        //        //分两次读完，先读前面的数据（最大数据长度小于32），最后读一位
        //        if (iReadLength > 1)
        //        {
        //            //读前面的数据，被动ACK。
        //            byte[] oBuffer = new byte[iReadLength];
        //            if (!IIC_InBlockByAck(iIndex, iReadLength - 1, ref oBuffer)) return false;
        //            for (int i = 0; i < iReadLength - 1; i++)
        //            {
        //                oReadBuffer[i] = oBuffer[i];
        //            }
        //        }
        //        //读最后一位，发送NACK。
        //        byte oByte;
        //        if (!IIC_InByteNoAck(iIndex, out oByte)) return false;
        //        oReadBuffer[iReadLength - 1] = oByte;
        //    }
        //    //停止位
        //    if (!IIC_IssueStop(iIndex)) return false;
        //    return true;
        //}
        // CH341WriteI2C 异常消息单步提示，立即暂停，函数简单易用。
        public static bool CH341WriteI2C(
            uint iIndex, // 指定CH341设备序号
            uint iSlvAdr, // I2C从地址，低7位。最大可以是11位地址
            uint iWrLen, // 准备写入的数据字节数
            Byte[] iWrArr, // 指向一个缓冲区,放置准备写出的数据
            bool NoStop) // 不发送停止位，应用于PMBus协议（停止位作为同步信号）或RandomRead等功能
        {
            bool idleStat, ackStat;
            SetSDAStatus(iIndex, true); // 空闲总线，先释放SDA，最释放SCL
            SetSCLStatus(iIndex, true);
            if (!GetBusStatus(iIndex, out idleStat)) { return false; } //I2C总线状态true/false = Idle/Busy
            if (!idleStat)
            {
                if (!ErrHdl.ErrorHandle(ErrHdl.ErrEnum.BusBusy, null)) { return false; } // I2C总线忙状态
                return false;
            }
            if (!IIC_IssueStart(iIndex)) { return false; } // 开始位
            if (!IIC_OutByteCheckAck(iIndex, (byte)(iSlvAdr << 1), out ackStat)) { return false; } // 地址+写
            if (!ackStat)
            {
                if (!ErrHdl.ErrorHandle(ErrHdl.ErrEnum.AdrWrNACK, null)) { return false; } // 地址+写错误
                if (!IIC_IssueStop(iIndex)) { return false; } // 异常发生，强制停止
                return false;
            }
            for (int i = 0; i < (int)iWrLen; i++)
            {
                if (!IIC_OutByteCheckAck(iIndex, iWrArr[i], out ackStat)) { return false; }
                if (!ackStat)
                {
                    if (!ErrHdl.ErrorHandle(ErrHdl.ErrEnum.DatWrNACK, null)) { return false; } // 发送数据错误
                    if (!IIC_IssueStop(iIndex)) { return false; } // 异常发生，强制停止
                    return false;
                }
            }
            if (!NoStop)
            {
                if (!IIC_IssueStop(iIndex)) { return false; } // 停止位
            }
            return true;
        }
        // CH341ReadI2C 异常消息单步提示，立即暂停，函数简单易用。
        public static bool CH341ReadI2C(
            uint iIndex,  // 指定CH341设备序号
            uint iSlvAdr,  // I2C从地址，低7位。最大可以是11位地址
            uint iReLen,   // 指向一个字节,返回后是读入的数据
            ref Byte[] oReArr)  // 指向一个缓冲区,返回后是读入的数据
        {
            bool idleStat, ackStat;
            SetSDAStatus(iIndex, true); // 空闲总线，先释放SDA，最释放SCL
            SetSCLStatus(iIndex, true);
            if (!GetBusStatus(iIndex, out idleStat)) { return false; } //I2C总线状态true/false = Idle/Busy
            if (!idleStat)
            {
                if (!ErrHdl.ErrorHandle(ErrHdl.ErrEnum.BusBusy, null)) { return false; } // I2C总线忙状态
                return false;
            }
            if (!IIC_IssueStart(iIndex)) { return false; } // 开始位
            if (!IIC_OutByteCheckAck(iIndex, (byte)(iSlvAdr << 1 | 0x01), out ackStat)) { return false; } // 址位+读 然后开始读操作
            if (!ackStat)
            {
                if (!ErrHdl.ErrorHandle(ErrHdl.ErrEnum.AdrReNACK, null)) { return false; } // 址位+读错误
                if (!IIC_IssueStop(iIndex)) { return false; } // 异常发生，强制停止
                return false;
            }
            for (int i = 0; i < iReLen; i++) // 每次读一位，读数据长度不限
            {
                byte oByte;
                if (i != (iReLen - 1))
                {
                    if (!IIC_InByteByAck(iIndex, out oByte)) { return false; } // 读前面的数据，被动ACK
                }
                else
                {
                    if (!IIC_InByteNoAck(iIndex, out oByte)) { return false; } // 读最后一位，发送NACK                    
                }
                oReArr[i] = oByte;
            }
            if (!IIC_IssueStop(iIndex)) { return false; } // 停止位
            return true;
        }
        // CH341StreamI2C 对异常进行即时处理，函数简单易用。适合单步提示，并能暂停的需求
        public static bool CH341StreamI2C(
            uint iIndex,   // 指定CH341设备序号
            uint iSlvAdr,   // I2C从地址，低7位。最大可以是11位地址
            uint iWrLen, // 准备写入的数据字节数
            Byte[] iWrArr, // 指向一个缓冲区,放置准备写出的数据
            uint iReLen,    // 指向一个字节,返回后是读入的数据
            ref Byte[] oReArr)  // 指向一个缓冲区,返回后是读入的数据
        {
            bool idleStat, ackStat = true;
            SetSDAStatus(iIndex, true); // 空闲总线，先释放SDA，最释放SCL
            SetSCLStatus(iIndex, true);
            if (!GetBusStatus(iIndex, out idleStat)) { return false; } //I2C总线状态true/false = Idle/Busy
            if (!idleStat)
            {
                if (!ErrHdl.ErrorHandle(ErrHdl.ErrEnum.BusBusy, null)) { return false; } // I2C总线忙状态
                return false;
            }
            if (iWrLen > 0) // 检查是否需要写地址和数据
            {
                if (!IIC_IssueStart(iIndex)) { return false; } // 开始位
                if (!IIC_OutByteCheckAck(iIndex, (byte)(iSlvAdr << 1), out ackStat)) { return false; } // 确认地址位+写
                if (!ackStat)
                {
                    if (!ErrHdl.ErrorHandle(ErrHdl.ErrEnum.AdrWrNACK, null)) { return false; } // 地址+写错误
                    if (!IIC_IssueStop(iIndex)) { return false; } // 异常发生，强制停止
                    return false;
                }
                for (int i = 0; i < (int)iWrLen; i++)
                {
                    if (!IIC_OutByteCheckAck(iIndex, iWrArr[i], out ackStat)) { return false; } // 写一位数据
                    if (!ackStat)
                    {
                        if (!ErrHdl.ErrorHandle(ErrHdl.ErrEnum.DatWrNACK, null)) { return false; }// 写数据错误
                        if (!IIC_IssueStop(iIndex)) { return false; } // 异常发生，强制停止
                        return false;
                    }
                }
            }
            if ((iReLen > 0) && ackStat) // 检查是否需要读数据, 并且写过程没有错误。
            {
                if (iWrLen > 0)
                {
                    if (!IIC_IssueStart(iIndex)) { return false; } // 重启动位
                }
                else
                {
                    if (!IIC_IssueStart(iIndex)) { return false; } // 开始位
                }
                if (!IIC_OutByteCheckAck(iIndex, (byte)(iSlvAdr << 1 | 0x01), out ackStat)) { return false; } // 地址+读 然后开始读操作
                if (!ackStat)
                {
                    if (!ErrHdl.ErrorHandle(ErrHdl.ErrEnum.AdrReNACK, null)) { return false; } // 地址+读错误
                    if (!IIC_IssueStop(iIndex)) { return false; } // 异常发生，强制停止
                    return false;
                }
                byte oByte;
                for (int i = 0; i < iReLen; i++) // 每次读一位，读数据长度不限
                {
                    if (i != (iReLen - 1))
                    {
                        if (!IIC_InByteByAck(iIndex, out oByte)) { return false; } // 读前面的数据，被动ACK
                    }
                    else
                    {
                        if (!IIC_InByteNoAck(iIndex, out oByte)) { return false; } // 读最后一位，发送NACK        
                    }
                    oReArr[i] = oByte;
                }
            }
            if (!IIC_IssueStop(iIndex)) { return false; } // 停止位
            return true;
        }
        // CH341StreamI2C 对所有异常不作处理，留给调用程序作分类处理。适合系统化应用程序调用。
        public static bool CH341StreamI2CNoMsg(
            uint iIndex,   // 指定CH341设备序号
            uint iSlvAdr,   // I2C从地址，低7位。最大可以是11位地址
            uint iWrLen, // 准备写入的数据字节数
            Byte[] iWrArr, // 指向一个缓冲区,放置准备写出的数据
            uint iReLen,    // 指向一个字节,返回后是读入的数据
            ref Byte[] oReArr,  // 指向一个缓冲区,返回后是读入的数据
            out ErrHdl.ErrEnum iStatus) // 返回一个自定义错误状态值
        {
            bool idleStat, ackStat = true;
            iStatus = ErrHdl.ErrEnum.SysErr;
            SetSDAStatus(iIndex, true); // 空闲总线，先释放SDA，最释放SCL
            SetSCLStatus(iIndex, true);
            if (!GetBusStatus(iIndex, out idleStat)) { return false; } //I2C总线状态true/false = Idle/Busy
            if (!idleStat)
            {
                if (!ErrHdl.ErrorHandle(ErrHdl.ErrEnum.BusBusy, null)) { return false; } // I2C总线忙状态
                return false;
            }
            if (iWrLen > 0) // 检查是否需要写地址和数据
            {
                if (!IIC_IssueStart(iIndex)) { return false; } // 开始位
                if (!IIC_OutByteCheckAck(iIndex, (byte)(iSlvAdr << 1), out ackStat)) { return false; } // 确认地址位+写
                if (!ackStat)
                {
                    iStatus = ErrHdl.ErrEnum.AdrWrNACK; // 地址+写错误
                    if (!IIC_IssueStop(iIndex)) { return false; } // 异常发生，强制停止
                    return false;
                }
                for (int i = 0; i < (int)iWrLen; i++)
                {
                    if (!IIC_OutByteCheckAck(iIndex, iWrArr[i], out ackStat)) { return false; } // 写一位数据
                    if (!ackStat)
                    {
                        iStatus = ErrHdl.ErrEnum.DatWrNACK; // 写数据错误
                        if (!IIC_IssueStop(iIndex)) { return false; } // 异常发生，强制停止
                        return false;
                    }
                }
            }
            if ((iReLen > 0) && ackStat) // 检查是否需要读数据, 并且写过程没有错误。
            {
                if (iWrLen > 0)
                {
                    if (!IIC_IssueStart(iIndex)) { return false; } // 重启动位
                }
                else
                {
                    if (!IIC_IssueStart(iIndex)) { return false; } // 开始位
                }
                if (!IIC_OutByteCheckAck(iIndex, (byte)(iSlvAdr << 1 | 0x01), out ackStat)) { return false; } // 地址+读 然后开始读操作
                if (!ackStat)
                {
                    iStatus = ErrHdl.ErrEnum.AdrReNACK; // 地址+读错误
                    if (!IIC_IssueStop(iIndex)) { return false; } // 异常发生，强制停止
                    return false;
                }
                byte oByte;
                for (int i = 0; i < iReLen; i++) // 每次读一位，读数据长度不限
                {
                    if (i != (iReLen - 1))
                    {
                        if (!IIC_InByteByAck(iIndex, out oByte)) { return false; } // 读前面的数据，被动ACK
                    }
                    else
                    {
                        if (!IIC_InByteNoAck(iIndex, out oByte)) { return false; } // 读最后一位，发送NACK        
                    }
                    oReArr[i] = oByte;
                }
            }
            if (!IIC_IssueStop(iIndex)) { return false; } // 停止位
            return true;
        }
        // 设置为低，下位SCL。设置为高，释放SCL
        public static bool SetSCLStatus(uint iIndex, bool iStatus)
        {
            return SetPinStatus(iIndex, 18, iStatus);//注意：SCL设置为位18，状态为位22
        }
        // 设置为低，下位SDA。设置为高，释放SDA
        public static bool SetSDAStatus(uint iIndex, bool iStatus)
        {
            return SetPinStatus(iIndex, 19, iStatus);//注意：SDA设置为位19，状态为位23
        }
        // 检查I2C总线状态检查,一般定义低为忙状态。高为空闲状态 T/F = Idle/Busy
        public static bool GetBusStatus(uint iIndex, out bool oIdle)
        {
            oIdle = false;
            bool sSCL, sSDA;
            if (!GetPinStatus(iIndex, 22, out sSCL)) { return false; }//注意：SCL设置为位18，状态为位22
            if (!GetPinStatus(iIndex, 23, out sSDA)) { return false; }//注意：SDA设置为位19，状态为位23
            oIdle = sSCL && sSDA;
            return true;
        }
        // 设置Byte0方向,注意这里方向值数据类型为U8
        public static bool SetByte0Direction(uint iIndex, byte iByteVal)
        {
            uint en = 0x08; // 位3为1说明iSetDirOut的位7-位0有效,否则忽略
            portDir = portDir & ~0x000000FFU | iByteVal;
            return CH341DLL.CH341SetOutput(iIndex, en, portDir, 0);
        }
        // 设置Byte1方向，注意这里方向值数据类型为U32
        public static bool SetByte1Direction(uint iIndex, uint iPortDir)
        {
            uint en = 0x02;  // 位1为1说明iSetDirOut的位15-位8有效,否则忽略
            portDir = portDir & ~0x0000FF00U | iPortDir;
            return CH341DLL.CH341SetOutput(iIndex, en, portDir, 0);
        }
        // 设置PIN方向，只有低16位可用
        public static bool SetPinDirection(uint iIndex, int iNumBit, bool iStatus)
        {
            uint en;
            if (iNumBit < 8) { en = 0x08; } // 位3为1说明iSetDirOut的位7-位0有效,否则忽略
            else if (iNumBit < 16) { en = 0x02; } // 位1为1说明iSetDirOut的位15-位8有效,否则忽略
            else { en = 0; } // 位23-位16不能设置方向
            if (iStatus) { portDir |= 1U << iNumBit; }
            else { portDir &= ~(1U << iNumBit); }
            return CH341DLL.CH341SetOutput(iIndex, en, portDir, 0);
        }
        // 设置Byte0值
        public static bool SetByte0Output(uint iIndex, byte iByteVal)
        {
            uint en = 0x04;  // 位2为1说明iSetDataOut的7-位0有效,否则忽略
            portVal = portVal & ~0x000000FFU | iByteVal & 0x000000FFU;
            return CH341DLL.CH341SetOutput(iIndex, en, 0, portVal);
        }
        // 设置Byte1值
        public static bool SetByte1Output(uint iIndex, uint iPortVal)
        {
            uint en = 0x01;  // 位0为1说明iSetDataOut的位15-位8有效,否则忽略
            portVal = portVal & ~0x0000FF00U | iPortVal & 0x0000FF00U;
            return CH341DLL.CH341SetOutput(iIndex, en, 0, portVal);
        }
        // 设置Byte2值
        public static bool SetByte2Output(uint iIndex, uint iPortVal)
        {
            uint en = 0x10;  // 位4为1说明iSetDataOut的位23-位16有效,否则忽略
            portVal = portVal & ~0x00FF0000U | iPortVal & 0x00FF0000U;
            return CH341DLL.CH341SetOutput(iIndex, en, 0, portVal);
        }
        // 设置PIN值，只有低24位可用
        public static bool SetPinStatus(uint iIndex, int iNumBit, bool iStatus)
        {
            uint en;
            if (iNumBit < 8) { en = 0x04; } // 位2为1说明iSetDataOut的7-位0有效,否则忽略
            else if (iNumBit < 16) { en = 0x01; } // 位0为1说明iSetDataOut的位15-位8有效,否则忽略
            else if (iNumBit < 24) { en = 0x10; } // 位4为1说明iSetDataOut的位23-位16有效,否则忽略
            else { en = 0; }
            //if (!CH341DLL.CH341GetInput(iIndex, out portVal)) { return false; } // 在写之前读入当前状态
            //if (!CH341DLL.CH341GetStatus(iIndex, out portVal)) { return false; } // 在写之前读入当前状态
            if (!CH341GetLevel(iIndex, out portVal)) { return false; } // 在写之前读入当前状态
            if (iStatus) { portVal |= 1U << iNumBit; }
            else { portVal &= ~(1U << iNumBit); }
            return CH341DLL.CH341SetOutput(iIndex, en, 0, portVal);
        }
        // 获取PIN值，只有低24位可用
        public static bool GetPinStatus(uint iIndex, int iNumBit, out bool oStatus)
        {
            oStatus = false;
            //if (!CH341DLL.CH341GetInput(iIndex, out portVal)) { return false; }
            //if (!CH341DLL.CH341GetStatus(iIndex, out portVal)) { return false; }
            if (!CH341GetLevel(iIndex, out portVal)) { return false; }
            oStatus = (portVal & 1U << iNumBit) > 0;
            return true;
        }

        // 自定义获取端口值
        // 由于CH341GetStatus和CH341GetInput两个函数屏蔽了SCL的返回值，无法获取I2C总线状态，因此重写此函数。
        public static bool CH341GetLevel(uint iIndex, out uint oPortLevel)
        {
            oPortLevel = 0;
            uint outLen = 1;
            outBuf[0] = CH341DLL.mCH341A_CMD_GET_INPUT;  // 命令码
            if (!CH341DLL.CH341WriteData(iIndex, outBuf, ref outLen)) { return false; }  // 写出数据块
            uint inLen = 6;
            if (!CH341DLL.CH341ReadData(iIndex, inBuf, ref inLen)) { return false; }  // 执行数据流命令,先输出再输入
            if (inLen != 6) { return false; } // 检查返回数据长度
            oPortLevel = (uint)(inBuf[0] | inBuf[1] << 8 | inBuf[2] << 16);
            return true;
        }
        /* ********************************************************************************************** */
        /* 操作加密存储器X76F640 */

        public static bool X76F640_AckPolling(  // 查询X76F640应答 (包括:输出起始位,输出一字节命令数据,检查应答是否有效)
           uint iIndex)  // 指定CH341设备序号
        {
            uint mLength, mInLen;
            outBuf[0] = CH341DLL.mCH341A_CMD_I2C_STREAM;  // 命令码
            outBuf[1] = CH341DLL.mCH341A_CMD_I2C_STM_STA;  // 产生起始位
            outBuf[2] = CH341DLL.mCH341A_CMD_I2C_STM_OUT;  // 输出数据,位5-位0为长度,0长度则只发送一个字节并返回应答
            outBuf[3] = 0xF0;  // 应答查询操作的命令码
            outBuf[4] = CH341DLL.mCH341A_CMD_I2C_STM_END;  // 当前包提前结束
            mLength = 5;
            mInLen = 0;
            if (!CH341DLL.CH341WriteRead(iIndex, mLength, outBuf, CH341DLL.mCH341A_CMD_I2C_STM_MAX, 1, out mInLen, outBuf)) { return false; }// 执行数据流命令,先输出再输入            
            if (mInLen > 0 && (outBuf[mInLen - 1] & 0x80) == 0) { return true; }  // 返回的数据的位7代表ACK应答位,ACK=0有效            
            return false;
        }

        public static bool X76F640_CheckPasswd(  // 发出操作命令并检查指定的密码 (包括:输出起始位,输出9字节数据(1命令+8密码),查询应答,输出2字节地址)
           uint iIndex,  // 指定CH341设备序号
           uint iCommand,  // 操作命令码
           byte[] iPasswdBuf,  // 指向一个缓冲区,提供8字节的密码数据
           uint iAddress)  // 指定操作地址或者密码后的2字节数据
        {
            uint i, mLength;
            i = 0;
            outBuf[i++] = CH341DLL.mCH341A_CMD_I2C_STREAM;  // 命令码
            outBuf[i++] = CH341DLL.mCH341A_CMD_I2C_STM_STA;  // 产生起始位
            outBuf[i++] = (byte)(CH341DLL.mCH341A_CMD_I2C_STM_OUT | 9);  // 输出数据,位5-位0为长度,9字节
            outBuf[i++] = (byte)iCommand;  // 操作命令码
            //memcpy(&outBuf[i], iPasswdBuf, 8);  // 8字节密码数据
            i += 8;
            outBuf[i++] = (byte)(CH341DLL.mCH341A_CMD_I2C_STM_MS | 10);  // 以亳秒为单位延时,位3-位0为延时值,延时10毫秒
            outBuf[i++] = CH341DLL.mCH341A_CMD_I2C_STM_END;  // 当前包提前结束
            mLength = i;
            if (CH341DLL.CH341WriteData(iIndex, outBuf, ref mLength))
            {  // 写出数据块
                if (X76F640_AckPolling(iIndex))
                {  // 查询应答有效
                    i = 0;
                    outBuf[i++] = CH341DLL.mCH341A_CMD_I2C_STREAM;  // 命令码
                    outBuf[i++] = (byte)(CH341DLL.mCH341A_CMD_I2C_STM_OUT | 2);  // 输出数据,位5-位0为长度
                    outBuf[i++] = (byte)(iAddress & 0x00FF);  // 地址低8位
                    outBuf[i++] = (byte)((iAddress >> 8) & 0x00FF);  // 地址高8位
                    outBuf[i++] = CH341DLL.mCH341A_CMD_I2C_STM_END;  // 当前包提前结束
                    mLength = i;
                    return (CH341DLL.CH341WriteData(iIndex, outBuf, ref mLength));  // 写出数据块
                }
                else IIC_IssueStop(iIndex);  // 应答无效
            }
            return false;
        }

        public static bool X76F640_WriteWithPasswd(  // 写X76F640的块,使用指定的密码
          uint iIndex,  // 指定CH341设备序号
          uint iWriteCommand,  // 块写命令码
          byte iPasswdBuf,  // 指向一个缓冲区,放置8字节的密码数据
          uint iAddress,  // 指定操作地址
          uint iOutLength,  // 准备写出的数据字节数,单次必须小于32字节(1个扇区)
          byte[] iOutBuffer)  // 指向一个缓冲区,放置准备写出的数据
        {
            byte[] mBuffer = new byte[CH341DLL.mDEFAULT_BUFFER_LEN];
            uint i, mLength;
            if (iOutLength == 0 || iOutLength > 32) { return false; }
            if (/*X76F640_CheckPasswd(iIndex, iWriteCommand, iPasswdBuf, iAddress)*/true)
            {  // 发出命令及密码检查通过
                if (iOutLength > (CH341DLL.mCH341_BUCK_PACKET_LENGTH - 1 - 1 - 1 - 1 - 1))
                {  // 去掉前2字节后3字节,一个包不够用
                    mLength = iOutLength - (CH341DLL.mCH341_BUCK_PACKET_LENGTH - 1 - 1 - 1 - 1 - 1);  // 多出的长度
                    iOutLength -= mLength;  // 第1个包的数据长度
                }
                else mLength = 0;  // 1个包就够用了
                i = 0;
                mBuffer[i++] = CH341DLL.mCH341A_CMD_I2C_STREAM;  // 命令码
                mBuffer[i++] = (byte)(CH341DLL.mCH341A_CMD_I2C_STM_OUT | iOutLength);  // 输出数据,位5-位0为长度

                for (int t = 0; t < iOutLength; t++) { iOutBuffer[t] = mBuffer[t]; } // 数据
                i += iOutLength;
                if (mLength > 0)
                {  // 第2包
                    mBuffer[i] = CH341DLL.mCH341A_CMD_I2C_STM_END;  // 当前包提前结束
                    i += CH341DLL.mCH341_BUCK_PACKET_LENGTH - i % CH341DLL.mCH341_BUCK_PACKET_LENGTH;  // 跳过当前包剩余部分
                    mBuffer[i++] = CH341DLL.mCH341A_CMD_I2C_STREAM;  // 第2个包的首字节仍然是命令码
                    mBuffer[i++] = (byte)(CH341DLL.mCH341A_CMD_I2C_STM_OUT | mLength);  // 输出数据,位5-位0为长度
                    // memcpy(&mBuffer[i], (Pbyte)iOutBuffer + iOutLength, mLength);  // 剩余数据
                    i += mLength;
                }
                mBuffer[i++] = CH341DLL.mCH341A_CMD_I2C_STM_STO;  // 产生停止位
                mBuffer[i++] = (byte)(CH341DLL.mCH341A_CMD_I2C_STM_MS | 10);  // 以亳秒为单位延时,位3-位0为延时值,延时10毫秒
                mBuffer[i++] = CH341DLL.mCH341A_CMD_I2C_STM_END;  // 当前包提前结束
                return (CH341DLL.CH341WriteData(iIndex, mBuffer, ref i));  // 写出数据块
            }
            //return false;
        }

        public static bool X76F640_ReadWithPasswd(  // 读X76F640的块,使用指定的密码 (包括:输出起始位,输出9字节数据(1命令+8密码),查询应答,输出2字节地址,读入数据块)
         uint iIndex,  // 指定CH341设备序号
         uint iReadCommand,  // 块读命令码
         byte iPasswdBuf,  // 指向一个缓冲区,放置8字节的密码数据
         uint iAddress,  // 指定操作地址
         uint iInLength,  // 准备读取的数据字节数,单次必须小于512字节 ( 每包32 * 16个包 = 512字节 )
         byte[] oInBuffer)  // 指向一个缓冲区,返回后是读入的数据
        {
            byte[] mBuffer = new byte[CH341DLL.mDEFAULT_BUFFER_LEN];
            uint i, mLength, mInLen;

            if (iInLength == 0 || iInLength > (16 * CH341DLL.mCH341_BUCK_PACKET_LENGTH)) { return false; }
            if (/*X76F640_CheckPasswd(iIndex, iReadCommand, iPasswdBuf, iAddress)*/true)
            {  // 发出命令及密码检查通过
                i = 0;
                mBuffer[i++] = CH341DLL.mCH341A_CMD_I2C_STREAM;  // 命令码
                for (mInLen = 1; mInLen < iInLength; )
                {
                    mLength = iInLength - mInLen >= CH341DLL.mCH341A_CMD_I2C_STM_MAX ? CH341DLL.mCH341A_CMD_I2C_STM_MAX : iInLength - mInLen;  // 本次输入有效数据长度
                    mBuffer[i++] = (byte)(CH341DLL.mCH341A_CMD_I2C_STM_IN | mLength);  // 输入数据,位5-位0为长度
                    mInLen += mLength;
                    if (mLength >= CH341DLL.mCH341A_CMD_I2C_STM_MAX)
                    {  // 当前包将满
                        mBuffer[i] = CH341DLL.mCH341A_CMD_I2C_STM_END;  // 当前包提前结束
                        i += CH341DLL.mCH341_BUCK_PACKET_LENGTH - i % CH341DLL.mCH341_BUCK_PACKET_LENGTH;  // 跳过当前包剩余部分
                        mBuffer[i++] = CH341DLL.mCH341A_CMD_I2C_STREAM;  // 新包的命令码
                    }
                }
                mBuffer[i++] = CH341DLL.mCH341A_CMD_I2C_STM_IN;  // 输入最后一个字节数据,只接收一个字节并发送无应答
                mBuffer[i++] = CH341DLL.mCH341A_CMD_I2C_STM_STO;  // 产生停止位
                mBuffer[i++] = CH341DLL.mCH341A_CMD_I2C_STM_END;  // 当前包提前结束
                mLength = 0;
                if (!CH341DLL.CH341WriteRead(iIndex, i, mBuffer, CH341DLL.mCH341A_CMD_I2C_STM_MAX, (iInLength + CH341DLL.mCH341A_CMD_I2C_STM_MAX - 1) / CH341DLL.mCH341A_CMD_I2C_STM_MAX, out mLength, oInBuffer)) { return false; }// 执行数据流命令,先输出再输入
                if (mLength == iInLength) { return true; }
            }
            return false;
        }

        /* ********************************************************************************************** */
        /* 例子:操作准双向I/O扩展PCF8574 */

        public static bool PCF8574_WriteIO(  // 输出PCF8574的I/O
         uint iIndex,  // 指定CH341设备序号
         uint iDeviceAddr,  // 设备地址,最低位为命令方向位
         uint iOutByte)  // 准备写出的I/O数据
        {  // 可以直接用CH341StreamI2C( iIndex, 2, outBuf, 0, NULL )实现
            uint mLength = 7;
            outBuf[0] = CH341DLL.mCH341A_CMD_I2C_STREAM;  // 命令码
            outBuf[1] = CH341DLL.mCH341A_CMD_I2C_STM_STA;  // 产生起始位
            outBuf[2] = (byte)(CH341DLL.mCH341A_CMD_I2C_STM_OUT | 2);  // 输出数据,位5-位0为长度,2字节
            outBuf[3] = (byte)(iDeviceAddr & 0xFE);  // 设备地址,写操作
            outBuf[4] = (byte)iOutByte;  // I/O数据
            outBuf[5] = CH341DLL.mCH341A_CMD_I2C_STM_STO;  // 产生停止位
            outBuf[6] = CH341DLL.mCH341A_CMD_I2C_STM_END;  // 当前包提前结束
            return (CH341DLL.CH341WriteData(iIndex, outBuf, ref mLength));  // 写出数据块
        }

        public static bool PCF8574_ReadIO(  // 输入PCF8574的I/O
         uint iIndex,  // 指定CH341设备序号
         uint iDeviceAddr,  // 设备地址,最低位为命令方向位
         out byte oInByte)  // 指向一个字节的缓冲区,返回后是读入的I/O数据
        {  // 可以直接用CH341StreamI2C( iIndex, 1, outBuf, 1, oInByte )实现
            oInByte = 0;
            uint mLength = 7;
            uint mInLen = 0;
            outBuf[0] = CH341DLL.mCH341A_CMD_I2C_STREAM;  // 命令码
            outBuf[1] = CH341DLL.mCH341A_CMD_I2C_STM_STA;  // 产生起始位
            outBuf[2] = (byte)(CH341DLL.mCH341A_CMD_I2C_STM_OUT | 1);  // 输出数据,位5-位0为长度,1字节
            outBuf[3] = (byte)(iDeviceAddr | 0x01);  // 设备地址,读操作
            outBuf[4] = (byte)(CH341DLL.mCH341A_CMD_I2C_STM_IN | 1);  // 输入数据,位5-位0为长度,1字节
            outBuf[5] = CH341DLL.mCH341A_CMD_I2C_STM_STO;  // 产生停止位
            outBuf[6] = CH341DLL.mCH341A_CMD_I2C_STM_END;  // 当前包提前结束

            if (!CH341DLL.CH341WriteRead(iIndex, mLength, outBuf, CH341DLL.mCH341A_CMD_I2C_STM_MAX, 1, out mInLen, outBuf)) { return false; }            // 执行数据流命令,先输出再输入
            if (mInLen != 0)
            {
                oInByte = outBuf[0];  // 返回的数据
                return true;
            }
            return false;
        }

        /* ********************************************************************************************** */
        /* 例子:操作双向I/O扩展PCA9554 */

        public static bool PCA9554_SetIO(  // 设置PCA9554的I/O方向
         uint iIndex,  // 指定CH341设备序号
         uint iDeviceAddr,  // 设备地址,最低位为命令方向位
         uint iSetByte)  // 方向数据
        {  // 也可以用CH341WriteI2C实现
            outBuf[0] = (byte)(iDeviceAddr & 0xFE);  // 设备地址,写操作
            outBuf[1] = 0x03;
            outBuf[2] = (byte)iSetByte;  // I/O方向数据
            return (CH341DLL.CH341StreamI2C(iIndex, 3, outBuf, 0, null));  // 处理I2C数据流
        }

        public static bool PCA9554_WriteIO(  // 输出PCA9554的I/O
         uint iIndex,  // 指定CH341设备序号
         uint iDeviceAddr,  // 设备地址,最低位为命令方向位
         uint iOutByte)  // 准备写出的I/O数据
        {  // 也可以用CH341WriteI2C实现
            outBuf[0] = (byte)(iDeviceAddr & 0xFE);  // 设备地址,写操作
            outBuf[1] = 0x01;
            outBuf[2] = (byte)iOutByte;  // I/O数据
            return (CH341DLL.CH341StreamI2C(iIndex, 3, outBuf, 0, null));  // 处理I2C数据流
        }

        public static bool PCA9554_ReadIO(  // 输入PCA9554的I/O
         uint iIndex,  // 指定CH341设备序号
         uint iDeviceAddr,  // 设备地址,最低位为命令方向位
          byte[] oInByte)  // 指向一个字节的缓冲区,返回后是读入的I/O数据
        {  // 也可以用CH341ReadI2C实现
            outBuf[0] = (byte)(iDeviceAddr & 0xFE);  // 设备地址,CH341StreamI2C自动处理读操作
            outBuf[1] = 0x00;
            return (CH341DLL.CH341StreamI2C(iIndex, 2, outBuf, 1, oInByte));  // 处理I2C数据流
        }

        /* ********************************************************************************************** */
        /* 用EPP或者MEM并口进行多位输出或者多位输入,模拟只读或者只写的SPI时序 */

        /* 下面是用EPP并口CH341EppWriteData模拟只是进行输出的SPI时序,参考下图波形(选择等宽的中文字体时才能看出)
               ___                                                           ___
          WR#     |_________________________________________________________|       SPI_CS
               ______    ___    ___    ___    ___    ___    ___    ___    ______
          DS#        |__|   |__|   |__|   |__|   |__|   |__|   |__|   |__|          SPI_CLK
               ____        ______ ______        ______                      ____
          D0       |______|      |      |______|      |______|______|______|        SPI_DOUT0
               ____ ______        ______ ______ ______        ______        ____
          D1       |      |______|      |      |      |______|      |______|        SPI_DOUT1
               ____                      ______                      ______ ____
          D5       |______|______|______|      |______|______|______|      |        SPI_DOUT5

          如果用CH341MemWriteAddr0代替CH341EppWriteData,那么波形如下
               ___                                                           ___
          ADDR    |_________________________________________________________|       SPI_CS
               ______    ___    ___    ___    ___    ___    ___    ___    ______
          WR#        |__|   |__|   |__|   |__|   |__|   |__|   |__|   |__|          SPI_CLK

           相应的源程序如下 */

        public static bool Exam_EppSerialOut(
         uint iIndex)  // 指定CH341设备序号
        {
            byte[] outBuf = new byte[256];
            uint mLength;
            outBuf[0] = 0x02;
            outBuf[1] = 0x01;
            outBuf[2] = 0x03;
            outBuf[3] = 0x22;
            outBuf[4] = 0x03;
            outBuf[5] = 0x00;
            outBuf[6] = 0x02;
            outBuf[7] = 0x20;
            mLength = 8;  /* 如果多于31个那么在WR#引脚的低电平中间将出现高电平脉冲 */
            return (CH341DLL.CH341EppWriteData(iIndex, outBuf, ref mLength));
        }

        /* 下面是用MEM并口CH341MemReadAddr0模拟只是进行输入的SPI时序,参考下图波形(选择等宽的中文字体时才能看出)
               ___                                                                  ___
          ADDR    |________________________________________________________________|       SPI_CS
               _____      __      __      __      __      __      __      __      _____
          RD#       |____|  |____|  |____|  |____|  |____|  |____|  |____|  |____|         SPI_CLK
               ______    ____    ____    ____    ____    ____    ____    ____    ______
          D0/in      |IN|    |IN|    |IN|    |IN|    |IN|    |IN|    |IN|    |IN|          SPI_DIN0
               ______    ____    ____    ____    ____    ____    ____    ____    ______
          D7/in      |IN|    |IN|    |IN|    |IN|    |IN|    |IN|    |IN|    |IN|          SPI_DIN7

          如果用CH341EppReadData代替CH341MemReadAddr0,那么波形如下
               _______________________________________________________________________
          WR#
               _____      __      __      __      __      __      __      __      _____
          DS#       |____|  |____|  |____|  |____|  |____|  |____|  |____|  |____|         SPI_CLK

           相应的源程序如下 */

        public static bool Exam_MemSerialIn(
         uint iIndex)  // 指定CH341设备序号
        {
            byte[] outBuf = new byte[256];
            uint mLength, i;
            mLength = 8;  /* 如果多于31个那么在ADDR引脚的低电平中间将出现高电平脉冲 */
            if (CH341DLL.CH341MemReadAddr0(iIndex, outBuf, ref mLength) == false) return false;
            for (i = 0; i < mLength; i++)
            {
                /*		printf( "D0 is %d, D1 is %d, D7 is %d\n", outBuf[i]&1, outBuf[i]>>1&1, mBuffer[i]>>7&1 );*/
            }
            return true;
        }

        /* ********************************************************************************************** */
        /* 用UIO通用I/O位流命令实现自定义的同步串行接口 */

        /* UIO方式共可以使用8个I/O引脚D7-D0,最多可以8个输入或者6个输出
           上位机以字节流控制CH341对最终位流进行输入和输出,有4种基本操作和1个结束操作:
        #define		mCH341A_CMD_UIO_STM_IN	0x00		// UIO接口的命令流:输入数据D7-D0
        #define		mCH341A_CMD_UIO_STM_DIR	0x40		// UIO接口的命令流:设定I/O方向D5-D0,位5-位0为方向数据
        #define		mCH341A_CMD_UIO_STM_OUT	0x80		// UIO接口的命令流:输出数据D5-D0,位5-位0为数据
        #define		mCH341A_CMD_UIO_STM_US	0xC0		// UIO接口的命令流:以微秒为单位延时,位5-位0为延时值
        #define		mCH341A_CMD_UIO_STM_END	0x20		// UIO接口的命令流:命令包提前结束

           例子:操作10位ADC芯片TLC1549,其时序为非标准
           连线: CH341_D0 <-> TLC1549_CS, CH341_D1 <-> TLC1549_IO_CLK, CH341_D7 <-> TLC1549_DOUT
           下面是用UIO通用I/O位流命令实现的任意波形,参考下图波形(选择等宽的中文字体时才能看出)
                 ______                                                                        ____________
          D0/out       |______________________________________________________________________|            |_________   TLC1549_CS#
                 ____     ___    ___    ___    ___    ___    ___    ___    ___    ___    ___    Delay 24uS    ___
          D1/out     |___| 1 |__| 2 |__| 3 |__| 4 |__| 5 |__| 6 |__| 7 |__| 8 |__| 9 |__| 10|________________| 1 |__|   TLC1549_I/O_CLOCK

          D7/in  ------| A9  |  A8  |  A7  |  A6  |  A5  |  A4  |  A3  |  A2  |  A1  |  A0  |__/-----------| B9  | B8   TLC1549_DATA_OUT

           相应的源程序如下 */

        public static bool TLC1549_ReadADC(  // 读取TLC1549的ADC结果
         uint iIndex,  // 指定CH341设备序号
         out uint oLastADC)  // 指向一个双字单元,返回读出的上次ADC的结果
        {
            const uint TLC1549_MAX_BIT = 10;	// 10位ADC
            byte[] mBuffer = new byte[CH341DLL.mCH341_BUCK_PACKET_LENGTH * 2];
            uint i, j, mLength;
            i = 0;
            mBuffer[i++] = CH341DLL.mCH341A_CMD_UIO_STREAM;  // 命令码
            mBuffer[i++] = CH341DLL.mCH341A_CMD_UIO_STM_OUT | 0x01;  // default status: D0=1, D1=0, CS#=HIGH, I/O_CLOCK=LOW
            mBuffer[i++] = CH341DLL.mCH341A_CMD_UIO_STM_DIR | 0x03;  // D0 output, D1 output, other input
            mBuffer[i++] = CH341DLL.mCH341A_CMD_UIO_STM_OUT | 0x00;  // D0=0, CS#=LOW
            for (j = 0; j < 8; j++)
            {  // input 8 bit
                mBuffer[i++] = CH341DLL.mCH341A_CMD_UIO_STM_OUT | 0x02;  // D1=1, I/O_CLOCK=HIGH
                mBuffer[i++] = CH341DLL.mCH341A_CMD_UIO_STM_IN;  // input 1 byte from D7-D0, input A9,A8,A7,A6,A5,A4,A3,A2
                mBuffer[i++] = CH341DLL.mCH341A_CMD_UIO_STM_OUT | 0x00;  // D1=0, I/O_CLOCK=LOW
            }
            mBuffer[i++] = CH341DLL.mCH341A_CMD_UIO_STM_END;  // 当前命令包提前结束,因为一个包放不下,所以分成两个包
            i = CH341DLL.mCH341_BUCK_PACKET_LENGTH;
            mBuffer[i++] = CH341DLL.mCH341A_CMD_UIO_STREAM;  // 命令码
            for (j = 0; j < TLC1549_MAX_BIT - 2; j++)
            {  // input 2 bit
                mBuffer[i++] = CH341DLL.mCH341A_CMD_UIO_STM_OUT | 0x02;  // D1=1, I/O_CLOCK=HIGH
                mBuffer[i++] = CH341DLL.mCH341A_CMD_UIO_STM_IN;  // input 1 byte from D7-D0, input A1,A0
                mBuffer[i++] = CH341DLL.mCH341A_CMD_UIO_STM_OUT | 0x00;  // D1=0, I/O_CLOCK=LOW
            }
            mBuffer[i++] = CH341DLL.mCH341A_CMD_UIO_STM_OUT | 0x01;  // D0=1, CS#=HIGH
            mBuffer[i++] = CH341DLL.mCH341A_CMD_UIO_STM_US | 24;  // delay 24uS,实际上这个延时完全不需要,因为USB传输每1mS一次,下次传输是在1mS之后
            mBuffer[i++] = CH341DLL.mCH341A_CMD_UIO_STM_END;  // 当前命令包提前结束
            mLength = 0;
            bool stat = CH341DLL.CH341WriteRead(iIndex, i, mBuffer, 8, 2, out mLength, mBuffer);  // 执行数据流命令,先输出再输入,执行两次输入,每次最多8字节
            oLastADC = 0;
            if (stat)
            {
                if (mLength == TLC1549_MAX_BIT)
                {  // 输入长度正确
                    for (i = 0; i < TLC1549_MAX_BIT; i++)
                    {  // 将每字节的位7合并为10位ADC结果数据
                        oLastADC = (oLastADC << 1) | (uint)(mBuffer[i] >> 7);  // 位7移到位0
                    }
                    return true;
                }
            }
            return false;
        }

        /* ********************************************************************************************** */
        /* 例子:操作4线接口SPI存储器25F512、25F020 */
        /* 连线: CH341_DCK/D3 <-> 25FXX_SCK, CH341_DOUT/D5 <-> 25FXX_SI, CH341_DIN/D7 <-> 25FXX_SO, CH341_D0 <-> 25FXX_CS# */

        public static bool AT25F512_ReadBlock(  // 读AT25F512的块 (包括:输出器件读命令码,输出3字节共24位地址,读入数据块)
         uint iIndex,  // 指定CH341设备序号
         uint iAddress,  // 指定操作地址
         uint iInLength,  // 准备读取的数据字节数,单次建议小于1024字节
         byte[] oInBuffer)  // 指向一个缓冲区,返回后是读入的数据
        {
            bool mTheFirst = true;
            if (mTheFirst)
            {  // 首次进入时需要设置I/O方向
                if (CH341DLL.CH341SetStream(iIndex, 0x81) == false) return false;  // 设置串口流模式:SPI为单入单出,SPI字节中的位顺序是高位在前
                mTheFirst = false;
            }
            if (iInLength == 0 || iInLength > CH341DLL.mDEFAULT_BUFFER_LEN) return false;
            oInBuffer[0] = 0x03;  // 读存储器命令码,注意各器件不一定命令码相同
            oInBuffer[1] = (byte)(iAddress >> 16 & 0xFF);  // 指定读操作的地址A23-A16
            oInBuffer[2] = (byte)(iAddress >> 8 & 0xFF);  // 指定读操作的地址A15-A8
            oInBuffer[3] = (byte)(iAddress & 0xFF);  // 指定读操作的地址A7-A0
            //	memset( (Pbyte)oInBuffer + 4, 0xFF, iInLength );  // 对于有些SPI器件,可能要求在读出数据时SI输入保持为1或者0,那么就要在此设置
            if (CH341DLL.CH341StreamSPI4(iIndex, 0x80, iInLength + 4, oInBuffer) == false) return false;  // 处理4线接口SPI数据流,自动片选为D0
            /* 对于25F512的读操作,要先输出4个字节命令及地址,再输入若干个字节数据,调用API和API返回时都是指总长度,所以返回数据的前4个字节是在输出命令及地址时输入的,应该丢弃 */
            //memmove(oInBuffer, (Pbyte)oInBuffer + 4, iInLength);  // 因为CH341的SPI是数据流,所以实际返回数据应该去掉自己发出的4个字节(命令和地址)
            return true;
        }

        /* ********************************************************************************************** */
        /* 例子:操作类似SPI的非标准串行时序的8位ADC芯片ADC0831、TLC0831 */
        /* 连线: CH341_DCK/D3 <-> ADC0831_CLK, CH341_DIN/D7 <-> ADC0831_DO, CH341_D2 <-> ADC0831_CS# */

        public static bool ADC0831_ReadADC(  // 读取ADC结果
         uint iIndex,  // 指定CH341设备序号
         out byte oADC)  // 指向一个字节单元,返回读出的ADC结果
        {
            bool mTheFirst = true;
            byte[] mBuffer = new byte[256];
            oADC = 0;
            uint i;
            if (mTheFirst)
            {  // 首次进入时需要设置I/O方向
                if (CH341DLL.CH341Set_D5_D0(iIndex, 0x0C, 0x04) == false) return false;  // 设置CH341的D5-D0引脚的I/O方向,DCK/D3输出默认为0,D2输出默认为1
                mTheFirst = false;
            }
            for (i = 0; i < 12; i++)
            {
                mBuffer[i] = 0x00;  // CS=0 for 1st bit to 12th bit
            }
            mBuffer[i++] = 0x04;  // CS=1 for 13th bit
            if (CH341DLL.CH341BitStreamSPI(iIndex, i, mBuffer) == false) return false;  // 处理SPI位数据流,输入输出共13位,实际未全部用到
            for (i = 0; i < 8; i++)
            {  // 将每字节的位7合并为8位ADC结果数据
                oADC = (byte)((oADC << 1) | (mBuffer[i + 1] >> 7) & 0xFF);  // 位7移到位0,跳过首字节无效数据,第2个时钟的输入数据才是MSB位
            }
            return true;
        }
    }
}
/* ********************************************************************************************** */

using System;
using System.Windows.Forms;

namespace ClsLib
{
    public static class ErrHdl
    {
        static string Lgu = System.Globalization.CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
        public enum ErrEnum { SysErr = -1, NoneErr = 0, ErrDllCall = 1, NoneDevice = 2, ExitApp = 3, BusBusy = 4, AdrWrNACK = 5, AdrReNACK = 6, DatWrNACK = 7, RmvEqu = 8, InTxtEpt = 9, InLnErr = 10, InHexErr = 11, CRCWrong=12 }
        public static bool scanfalse = true;

        public static bool ErrorHandle(ErrEnum msgEnum, Exception ex)
        {
            string str = "";
            switch (msgEnum)
            {
                case ErrEnum.ErrDllCall:
                    str = Lgu == "zh" ?
                        /*中文*/
                        "初始化CH341 USB-I2C端口错误，请检查相关设备的驱动程序是否已经成功的安装在系统中" :
                        /*英语*/
                        "Failed to initialize the USB-I2C CH341 equipment, please check the device driver has been successfully installed in the system. ";
                    break;
                case ErrEnum.NoneDevice:
                    str = Lgu == "zh" ?
                        /*中文*/
                        "没有检测到CH341 USB-I2C端口设备。可能原因：" +
                        "\r\n1. 相关设备没有找到。" +
                        "\r\n2. 相关设备发生过异常中断，在重启应用程序之前，请断开（拔除）相关设备。" :
                        /*英语*/
                        "CH341 USB-I2C equipment not detected. Possible reasons:" +
                        "\r\n1. You need to buy the devices, before you can use it." +
                        "\r\n2. the equipment has been interrupted abnormal, before restarting the application, please disconnect (remove) the the equipment on PC.";
                    break;
                case ErrEnum.BusBusy:
                    str = Lgu == "zh" ?
                        /*中文*/
                        "I2C总线正忙，请重试。可能原因：" +
                        "\r\n1, SDA或SCL被持续下拉。" +
                        "\r\n2, USB-I2C设备故障。" +
                        "\r\n3, DAT或SCL线上拉电阻阻值过大或开路。" +
                        "\r\n4, 电源电压不匹配。" +
                        "\r\n" +
                        "\r\n问题1的解决办法：请检查设备与模块之间的各信号线的联接是否正确，给模块供电的设备是否已经打开，模块是否损坏，模块是否有通信故障等。当然，你也可以断开设备与模块之间的I2C通信线排除由于模块产生的故障。" +
                        "\r\n问题2的解决办法：请关闭应用介面，拔除I2C设备，重新插入设备，重启应用程序。如果经常出现此故障，请检查控制电脑的GND与模块的GND之间是否有大的压差，如果这样，你需要增加I2C隔离器。" +
                        "\r\n问题3的解决办法：上拉电阻视情况而定，CH341可以承受的上拉电阻范围是（560-10kΩ,一般2kΩ比较适中。" :
                        /*英语*/
                        "I2C bus is busy(SDA or SCL is pull down). Possible reasons: " +
                        "\r\n1, The device is damaged resulted the bus is pull down." +
                        "\r\n2, Source voltage does not match." +
                        "\r\n3, The SDA or SCL pull-up resistance value is too large or damaged." +
                        "\r\n3, USB-I2C equipment failure.";
                    break;
                case ErrEnum.AdrWrNACK:
                    str = Lgu == "zh" ? /*中文*/
                        "发送从地址和写位后，发生了NACK错误。可能原因：" +
                        "\r\n1, 从机地址不正确,或从机损坏。" +
                        "\r\n2, 从机与设备之间端口连接不正确。" +
                        "\r\n3, 电压或者上拉电阻不匹配。" +
                        "\r\n4, USB-I2C设备故障。" :
                        /*英语*/
                        "After sending the slave address and write bit, a NACK error occurred. Possible reasons:" +
                        "\r\n1, The slave address is incorrect or the device is damaged." +
                        "\r\n2, The port of connection between the USB-I2C and the slave is incorrect." +
                        "\r\n3, Source voltage or pull up resistor does not match." +
                        "\r\n4, Communication interference or USB-I2C equipment failure.";
                    break;
                case ErrEnum.AdrReNACK:
                    str = Lgu == "zh" ?
                        /*中文*/
                        "发送从地址和读位后，发生了NACK错误。可能原因：" +
                        "\r\n1, 从机地址不正确,或从机损坏。" +
                        "\r\n2, 从机与设备之间端口连接不正确。" +
                        "\r\n3, 电压或者上拉电阻不匹配。" +
                        "\r\n4, USB-I2C设备故障。" :
                        /*英语*/
                        "After sending the slave address and read bit, a NACK error occurred. Possible reasons:" +
                        "\r\n1, The slave only write." +
                        "\r\n1, The slave address is incorrect or the device is damaged." +
                        "\r\n2, The port connection between the USB-I2C and the device is incorrect." +
                        "\r\n3, Source voltage or pull up resistor does not match." +
                        "\r\n4, Communication interference or USB-I2C equipment failure.";
                    break;
                case ErrEnum.DatWrNACK:
                    str = Lgu == "zh" ?
                        /*中文*/
                        "发送写数据NACK错误。可能原因：" +
                        "\r\n1.这种错误并不常见，从机在接收数据中途终止了通信服务。" +
                        "\r\n2.通信干扰或USB-I2C设备故障。" :
                        /*英语*/
                        "After sending the data byte, a NACK error occurred. Possible reasons:" +
                        "\r\n1. This error is not common, in the receiving the slave to terminate the communication services." +
                        "\r\n2. Communication interference or USB-I2C equipment failure.";
                    break;
                case ErrEnum.ExitApp:
                    str = Lgu == "zh" ? /*中文*/
                        "程序即将终止,请检查后重试。"
                        : /*英语*/
                        "The application will be abort, please check it and try again. ";
                    break;
                case ErrEnum.RmvEqu:
                    str = Lgu == "zh" ?
                        /*中文*/
                        "USB-I2C设备已经断开，程序即将终止。" :
                        /*英语*/
                        "The USB-I2C equipment has been disconnected, the program is about to terminate.";
                    break;
                case ErrEnum.InTxtEpt:
                    str = Lgu == "zh" ?
                        /*中文*/
                        "输入字符不能为空，请重试。" :
                        /*英语*/
                        "The input character cannot be empty. Please try again.";
                    break;

                case ErrEnum.InLnErr:
                    str = Lgu == "zh" ?
                        /*中文*/
                        "输入字符长度错误，要求输入16进制，两字符一组。请检查后重试。" :
                        /*英语*/
                        "Input character length error, request input HEX character, A group of two characters. Please check and try again.";
                    break;
                case ErrEnum.InHexErr:
                    str = Lgu == "zh" ?
                        /*中文*/
                        "在输入的字符中有不能格式化为16进制的字符。请检查后重试。" :
                        /*英语*/
                        "In the character of input there can not be formatted as a HEX character. Please check and try again.";
                    break;
                case ErrEnum.CRCWrong:
                    str = Lgu == "zh" ?
                        /*中文*/
                        "CRC校验错误。可能原因：" +
                        "\r\n通信干扰或USB-I2C设备故障。" :
                        /*英语*/
                        "CRC is checked to be wrong. Possible reasons:" +
                        "\r\n1. Communication interference or USB-I2C equipment failure.";
                    break;
                default:
                    break;
            }
            string msg = "";
            if (ex != null)
            {
                msg = Lgu == "zh" ?
                    /*中文*/
                   "错误描述：" :
                    /*英语*/
                   "Error description: ";
            }
            if (ex == null && str != "")
            {
                scanfalse = false;
                if (MessageBox.Show(str) == DialogResult.OK) { return false; }
            }
            else if (ex != null && str == "")
            {
                if (MessageBox.Show(msg + ex.Message) == DialogResult.OK) { return false; }
            }
            else if (ex != null && str != "")
            {
                if (MessageBox.Show(str + msg + ex.Message) == DialogResult.OK) { return false; }
            }
            return true;
        }
    }
}
using System;
using System.Windows.Forms;
using System.Linq;

namespace ClsLib
{
    public class FmtStr
    {
        // 检查输入数据
        public static bool FmtStr2Hex(ref string str, out byte[] arrData)
        {
            arrData = new byte[0];
            str = str.Replace(" ", "").ToUpper();
            if (str.Length == 0)
            {
                ErrHdl.ErrorHandle(ErrHdl.ErrEnum.InTxtEpt, null);
                return false;
            }
            if ((str.Length % 2 != 0))
            {
                ErrHdl.ErrorHandle(ErrHdl.ErrEnum.InLnErr, null);
                return false;
            }
            for (int i = 0; i < str.Length; i++)
            {
                if ("0123456789abcdefABCDEF".IndexOf(str.ElementAt(i)) < 0)
                {
                    ErrHdl.ErrorHandle(ErrHdl.ErrEnum.InHexErr, null);
                    return false;
                }
            }
            arrData = new byte[str.Length / 2];
            for (int i = 0; i < str.Length / 2; i++)
            {
                arrData[i] = Convert.ToByte(str.Substring(i * 2, 2), 16);
            }
            string s = "";
            for (int i = 0; i < str.Length; i++)
            {
                s = s + str.ElementAt(i);
                if ((i > 0) & ((i + 1) % 4 == 0))
                {
                    s = s + " ";
                }
            }
            str = s;
            return true;
        }
    }
}
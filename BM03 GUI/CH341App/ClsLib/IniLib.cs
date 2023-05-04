using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace ClsLib
{
    public static class IniLib //Initial information Operation Class
    {
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern uint GetPrivateProfileSection(string lpAppName, IntPtr lpReturnedString, uint nSize, string lpFileName);
        public static string ReadString(string section, string key, string filePath)
        {
            StringBuilder sb = new StringBuilder(1024);
            try
            {
                GetPrivateProfileString(section, key, "", sb, 1024, filePath);
            }
            catch
            {

            }
            return sb.ToString();
        }

    }
}

using System;
using System.IO;
using System.Windows.Forms;
namespace ClsLib
{
    public static class CfgFilLib
    {
        public static bool WriteRegCfg(uint iWrLen, byte[] iWrArr)
        {
            try
            {
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.InitialDirectory = Environment.CurrentDirectory;
                savedialog.Title = "Save Config";
                savedialog.DefaultExt = ".config";
                savedialog.Filter = "Config File(*.config)|*.config";

                if (savedialog.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = new FileStream(savedialog.FileName, FileMode.OpenOrCreate);
                    StreamWriter sw = new StreamWriter(fs);
                    for (int i = 0; i < iWrLen; i++)
                    {
                        sw.WriteLine(Convert.ToString(iWrArr[i]));
                    }
                    sw.Close();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("写配置数据错误。");
                return false;
            }
        }
        public static bool ReadRegCfg(uint iReLen, ref byte[] oReArr)
        {
            try
            {
                OpenFileDialog opendialog = new OpenFileDialog();
                opendialog.InitialDirectory = Environment.CurrentDirectory;
                opendialog.Title = "Open Config";
                opendialog.DefaultExt = ".config";
                opendialog.Filter = "Config File(*.config)|*.config";

                if (opendialog.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = new FileStream(opendialog.FileName, FileMode.Open);
                    StreamReader sr = new StreamReader(fs);
                    string strLine = "";
                    for (int i = 0; i < iReLen; i++)
                    {
                        strLine = sr.ReadLine();//Read data in line by line
                        if (strLine == null) { break; }
                        oReArr[i] = Convert.ToByte(strLine);
                    }
                    sr.Close();
                    fs.Close();
                    sr = null;
                    fs = null;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("读配置文件错误。");
                return false;
            }
        }
        public static bool WriteConfig(uint iWrLen, byte[] iWrArr)
        {
            try
            {
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.InitialDirectory = Environment.CurrentDirectory;
                savedialog.Title = "Save Config";
                savedialog.DefaultExt = ".config";
                savedialog.Filter = "Config File(*.txt)|*.txt";

                if (savedialog.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = new FileStream(savedialog.FileName, FileMode.OpenOrCreate);
                    StreamWriter sw = new StreamWriter(fs);
                    for (int i = 0; i < iWrLen; i++)
                    {
                        string strCnt = i.ToString("X2");
                        string strVal = iWrArr[i].ToString("X2");
                        sw.WriteLine(strCnt + strVal);
                    }
                    sw.Close();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("写配置数据错误。");
                return false;
            }
        }
        public static bool ReadConfig(uint iReLen, ref byte[] oReArr)
        {
            try
            {
                OpenFileDialog opendialog = new OpenFileDialog();
                opendialog.InitialDirectory = Environment.CurrentDirectory;
                opendialog.Title = "Open Config";
                opendialog.DefaultExt = ".config";
                opendialog.Filter = "Config File(*.txt)|*.txt";

                if (opendialog.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = new FileStream(opendialog.FileName, FileMode.Open);
                    StreamReader sr = new StreamReader(fs);
                    string strLine = "";
                    for (int i = 0; i < iReLen; i++)
                    {
                        strLine = sr.ReadLine();//Read data in line by line
                        if (strLine == null) { break; }
                        int count = Convert.ToInt32(strLine.Substring(0, 2), 16);
                        byte value = Convert.ToByte(strLine.Substring(2, 2), 16);
                        oReArr[count] = value;
                    }
                    sr.Close();
                    fs.Close();
                    sr = null;
                    fs = null;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("读配置文件错误。");
                return false;
            }
        }
        public static bool WriteRegCfg(uint iWrLen, uint[] iWrArr)
        {
            try
            {
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.Title = "Save Config";
                savedialog.DefaultExt = ".config";
                savedialog.Filter = "Config File(*.config)|*.config";

                if (savedialog.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = new FileStream(savedialog.FileName, FileMode.OpenOrCreate);
                    StreamWriter sw = new StreamWriter(fs);
                    for (int i = 0; i < iWrLen; i++)
                    {
                        sw.WriteLine(Convert.ToString(iWrArr[i]));
                    }
                    sw.Close();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("写配置数据错误。");
                return false;
            }
        }
        public static bool ReadRegCfg(uint iReLen, ref uint[] oReArr)
        {
            try
            {
                OpenFileDialog opendialog = new OpenFileDialog();
                opendialog.Title = "Open Config";
                opendialog.DefaultExt = ".config";
                opendialog.Filter = "Config File(*.config)|*.config";

                if (opendialog.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = new FileStream(opendialog.FileName, FileMode.Open);
                    StreamReader sr = new StreamReader(fs);
                    string strLine = "";
                    for (int i = 0; i < iReLen; i++)
                    {
                        strLine = sr.ReadLine();//Read data in line by line
                        if (strLine == null) { break; }
                        oReArr[i] = Convert.ToByte(strLine);
                    }
                    sr.Close();
                    fs.Close();
                    sr = null;
                    fs = null;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("读配置文件错误。");
                return false;
            }
        }
        public static bool WrCfgArr(uint iWrLen, string[] iWrArr)
        {
            try
            {
                SaveFileDialog savedialog = new SaveFileDialog();
                savedialog.InitialDirectory = Environment.CurrentDirectory;
                savedialog.Title = "Save Config";
                savedialog.DefaultExt = ".config";
                savedialog.Filter = "Config File(*.config)|*.config";

                if (savedialog.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = new FileStream(savedialog.FileName, FileMode.OpenOrCreate);
                    StreamWriter sw = new StreamWriter(fs);
                    for (int i = 0; i < iWrLen; i++)
                    {
                        sw.WriteLine(iWrArr[i]);
                    }
                    sw.Close();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("写配置数据错误。");
                return false;
            }
        }
        public static bool ReCfgArr(uint iReLen, ref string[] oReArr)
        {
            try
            {
                OpenFileDialog opendialog = new OpenFileDialog();
                opendialog.InitialDirectory = Environment.CurrentDirectory;
                opendialog.Title = "Open Config";
                opendialog.DefaultExt = ".config";
                opendialog.Filter = "Config File(*.config)|*.config";

                if (opendialog.ShowDialog() == DialogResult.OK)
                {
                    FileStream fs = new FileStream(opendialog.FileName, FileMode.Open);
                    StreamReader sr = new StreamReader(fs);
                    for (int i = 0; i < iReLen; i++)
                    {
                        oReArr[i] = sr.ReadLine();//Read data in line by line
                        if (oReArr[i] == null) { break; }
                    }
                    sr.Close();
                    fs.Close();
                    sr = null;
                    fs = null;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                MessageBox.Show("读配置文件错误。");
                return false;
            }
        }
    }
}
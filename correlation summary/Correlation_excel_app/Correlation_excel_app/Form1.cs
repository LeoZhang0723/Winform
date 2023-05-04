
//Remind：需要确认好TE AE原始数据的起始位置和当前程序一致，才能自动Correlation。检索的测试项名称不能错误（尤其是930）
//TE数据需要另存为xlsx格式，并重命名为“TE”

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using Spire.Xls;


namespace Correlation_excel_app
{

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        XLWorkbook ADC_Cor = new XLWorkbook();
        XLWorkbook TE = new XLWorkbook();
        XLWorkbook test = new XLWorkbook();
        

        int i;
        int j;
        private void button1_Click(object sender, EventArgs e)
        {
            ADC_Cor.AddWorksheet("Cor Result");
            ADC_Cor.AddWorksheet("AE原始测试数据记录");

            IXLWorksheet ADC_Cor_sheet = ADC_Cor.Worksheet(1);
            IXLWorksheet AE_sheet = ADC_Cor.Worksheet(2);

            

            //备注信息
            ADC_Cor_sheet.Cell(1, 1).Value = "part number:";
            ADC_Cor_sheet.Cell(2, 1).Value = "bake condition:";
            ADC_Cor_sheet.Cell(3, 1).Value = "Socket:";
            ADC_Cor_sheet.Cell(1, 4).Value = "测试日期:";
            ADC_Cor_sheet.Cell(2, 4).Value = "芯片编号:";
            ADC_Cor_sheet.Cell(3, 4).Value = "芯片版本/批次:";

        
           ADC_Cor_sheet.Cell(1, 2).Value = comboBox1.Text;
            ADC_Cor_sheet.Cell(2, 2).Value = textBox4.Text;
            ADC_Cor_sheet.Cell(3, 2).Value = textBox5.Text;
            ADC_Cor_sheet.Cell(1, 5).Value = textBox7.Text;
            ADC_Cor_sheet.Cell(2, 5).Value = comboBox3.Text;
            ADC_Cor_sheet.Cell(3, 5).Value = comboBox2.Text;



            ADC_Cor_sheet.Cell(4,3).Value = "AE焊接";
            ADC_Cor_sheet.Cell(4,4).Value = "TE";
            ADC_Cor_sheet.Cell(4,5).Value = "(AE-TE) error/mV";
            ADC_Cor_sheet.Cell(4, 6).Value = "BG折算后的OFFSET";


            


            if (comboBox1.Text == "SY68920")
            {
                ADC_Cor_sheet.Cell(5, 1).Value = "BG";
                j = 23;
            }

            else if (comboBox1.Text == "SY68930")
            {
                ADC_Cor_sheet.Cell(5, 1).Value = "Bottom BG";
                ADC_Cor_sheet.Cell(6, 1).Value = "Top BG";
                j = 41;
            }
            else
            {
                ADC_Cor_sheet.Cell(5, 1).Value = "Bottom BG";
                ADC_Cor_sheet.Cell(6, 1).Value = "Middle BG";
                ADC_Cor_sheet.Cell(7, 1).Value = "Top BG";

                j = 59;
            }


            for (i = 8; i <= j; i = i + 3)
            {
                ADC_Cor_sheet.Cell(i, 1).Value = "CELL" + ((i - 8) / 3 + 1);
                ADC_Cor_sheet.Cell(i, 2).Value = "2V error/mV";
                ADC_Cor_sheet.Cell(i + 1, 2).Value = "3.3V error/mV";
                ADC_Cor_sheet.Cell(i + 2, 2).Value = "5V error/mV";
            }

            //自动调整单元格列宽
            ADC_Cor_sheet.Columns().AdjustToContents();


            if (comboBox3.Text == "1#")
                ADC_Cor.SaveAs(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_1#.xlsx");
            if (comboBox3.Text == "2#")
                ADC_Cor.SaveAs(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_2#.xlsx");
            if (comboBox3.Text == "3#")
                ADC_Cor.SaveAs(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_3#.xlsx");
            if (comboBox3.Text == "4#")
                ADC_Cor.SaveAs(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_4#.xlsx");
            if (comboBox3.Text == "5#")
                ADC_Cor.SaveAs(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_5#.xlsx");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            if (comboBox1.Text == "SY68920")
            {

                //实例化参数
                TE = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\TE.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
                if (comboBox3.Text == "1#")
                    ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_1#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
                if (comboBox3.Text == "2#")
                    ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_2#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
                if (comboBox3.Text == "3#")
                    ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_3#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
                if (comboBox3.Text == "4#")
                    ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_4#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
                if (comboBox3.Text == "5#")
                    ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_5#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
                IXLWorksheet ADC_Cor_sheet = ADC_Cor.Worksheet(1);
                IXLWorksheet TE_sheet = TE.Worksheet(1);

                //读取TE的测量结果到Result表格

                for (i=1;i<=650;i++)
                {
                    //筛选出Vbg数据
                    if(Convert.ToString(TE_sheet.Cell(47, i).Value) == "vbg_bottom_aftrim") //要看一下TE的数据模板，标签行是不是第47行，否则会错
                    {

                        if (comboBox3.Text == "1#")
                            ADC_Cor_sheet.Cell(5, 4).Value = TE_sheet.Cell(53, i).Value;
                        if (comboBox3.Text == "2#")
                            ADC_Cor_sheet.Cell(5, 4).Value = TE_sheet.Cell(54, i).Value;
                        if (comboBox3.Text == "3#")
                            ADC_Cor_sheet.Cell(5, 4).Value = TE_sheet.Cell(55, i).Value;
                        if (comboBox3.Text == "4#")
                            ADC_Cor_sheet.Cell(5, 4).Value = TE_sheet.Cell(56, i).Value;
                        if (comboBox3.Text == "5#")
                            ADC_Cor_sheet.Cell(5, 4).Value = TE_sheet.Cell(57, i).Value;
                    }

                    //筛选出ADC error起始位置然后开始依次赋值
                    if (Convert.ToString(TE_sheet.Cell(47, i).Value) == "vc10_2v_error")
                    {

                        if (comboBox3.Text == "1#")
                        {
                            //CH1
                            ADC_Cor_sheet.Cell(8, 4).Value = TE_sheet.Cell(53, i).Value;
                            ADC_Cor_sheet.Cell(9, 4).Value = TE_sheet.Cell(53, i+1).Value;
                            ADC_Cor_sheet.Cell(10, 4).Value = TE_sheet.Cell(53, i + 2).Value;
                            //CH2
                            ADC_Cor_sheet.Cell(11, 4).Value = TE_sheet.Cell(53, i+3).Value;
                            ADC_Cor_sheet.Cell(12, 4).Value = TE_sheet.Cell(53, i + 4).Value;
                            ADC_Cor_sheet.Cell(13, 4).Value = TE_sheet.Cell(53, i + 5).Value;
                            //CH3
                            ADC_Cor_sheet.Cell(14, 4).Value = TE_sheet.Cell(53, i+6).Value;
                            ADC_Cor_sheet.Cell(15, 4).Value = TE_sheet.Cell(53, i + 7).Value;
                            ADC_Cor_sheet.Cell(16, 4).Value = TE_sheet.Cell(53, i + 8).Value;
                            //CH4
                            ADC_Cor_sheet.Cell(17, 4).Value = TE_sheet.Cell(53, i + 9).Value;
                            ADC_Cor_sheet.Cell(18, 4).Value = TE_sheet.Cell(53, i + 10).Value;
                            ADC_Cor_sheet.Cell(19, 4).Value = TE_sheet.Cell(53, i + 11).Value;
                            //CH5
                            ADC_Cor_sheet.Cell(20, 4).Value = TE_sheet.Cell(53, i + 12).Value;
                            ADC_Cor_sheet.Cell(21, 4).Value = TE_sheet.Cell(53, i + 13).Value;
                            ADC_Cor_sheet.Cell(22, 4).Value = TE_sheet.Cell(53, i + 14).Value;
                            //CH6
                            ADC_Cor_sheet.Cell(23, 4).Value = TE_sheet.Cell(53, i + 15).Value;
                            ADC_Cor_sheet.Cell(24, 4).Value = TE_sheet.Cell(53, i + 16).Value;
                            ADC_Cor_sheet.Cell(25, 4).Value = TE_sheet.Cell(53, i + 17).Value;
                        }

                        if (comboBox3.Text == "2#")
                        {
                            //CH1
                            ADC_Cor_sheet.Cell(8, 4).Value = TE_sheet.Cell(54, i).Value;
                            ADC_Cor_sheet.Cell(9, 4).Value = TE_sheet.Cell(54, i + 1).Value;
                            ADC_Cor_sheet.Cell(10, 4).Value = TE_sheet.Cell(54, i + 2).Value;
                            //CH2
                            ADC_Cor_sheet.Cell(11, 4).Value = TE_sheet.Cell(54, i + 3).Value;
                            ADC_Cor_sheet.Cell(12, 4).Value = TE_sheet.Cell(54, i + 4).Value;
                            ADC_Cor_sheet.Cell(13, 4).Value = TE_sheet.Cell(54, i + 5).Value;
                            //CH3
                            ADC_Cor_sheet.Cell(14, 4).Value = TE_sheet.Cell(54, i + 6).Value;
                            ADC_Cor_sheet.Cell(15, 4).Value = TE_sheet.Cell(54, i + 7).Value;
                            ADC_Cor_sheet.Cell(16, 4).Value = TE_sheet.Cell(54, i + 8).Value;
                            //CH4
                            ADC_Cor_sheet.Cell(17, 4).Value = TE_sheet.Cell(54, i + 9).Value;
                            ADC_Cor_sheet.Cell(18, 4).Value = TE_sheet.Cell(54, i + 10).Value;
                            ADC_Cor_sheet.Cell(19, 4).Value = TE_sheet.Cell(54, i + 11).Value;
                            //CH5
                            ADC_Cor_sheet.Cell(20, 4).Value = TE_sheet.Cell(54, i + 12).Value;
                            ADC_Cor_sheet.Cell(21, 4).Value = TE_sheet.Cell(54, i + 13).Value;
                            ADC_Cor_sheet.Cell(22, 4).Value = TE_sheet.Cell(54, i + 14).Value;
                            //CH6
                            ADC_Cor_sheet.Cell(23, 4).Value = TE_sheet.Cell(54, i + 15).Value;
                            ADC_Cor_sheet.Cell(24, 4).Value = TE_sheet.Cell(54, i + 16).Value;
                            ADC_Cor_sheet.Cell(25, 4).Value = TE_sheet.Cell(54, i + 17).Value;
                        }
                        if (comboBox3.Text == "3#")
                        {
                            //CH1
                            ADC_Cor_sheet.Cell(8, 4).Value = TE_sheet.Cell(55, i).Value;
                            ADC_Cor_sheet.Cell(9, 4).Value = TE_sheet.Cell(55, i + 1).Value;
                            ADC_Cor_sheet.Cell(10, 4).Value = TE_sheet.Cell(55, i + 2).Value;
                            //CH2
                            ADC_Cor_sheet.Cell(11, 4).Value = TE_sheet.Cell(55, i + 3).Value;
                            ADC_Cor_sheet.Cell(12, 4).Value = TE_sheet.Cell(55, i + 4).Value;
                            ADC_Cor_sheet.Cell(13, 4).Value = TE_sheet.Cell(55, i + 5).Value;
                            //CH3
                            ADC_Cor_sheet.Cell(14, 4).Value = TE_sheet.Cell(55, i + 6).Value;
                            ADC_Cor_sheet.Cell(15, 4).Value = TE_sheet.Cell(55, i + 7).Value;
                            ADC_Cor_sheet.Cell(16, 4).Value = TE_sheet.Cell(55, i + 8).Value;
                            //CH4
                            ADC_Cor_sheet.Cell(17, 4).Value = TE_sheet.Cell(55, i + 9).Value;
                            ADC_Cor_sheet.Cell(18, 4).Value = TE_sheet.Cell(55, i + 10).Value;
                            ADC_Cor_sheet.Cell(19, 4).Value = TE_sheet.Cell(55, i + 11).Value;
                            //CH5
                            ADC_Cor_sheet.Cell(20, 4).Value = TE_sheet.Cell(55, i + 12).Value;
                            ADC_Cor_sheet.Cell(21, 4).Value = TE_sheet.Cell(55, i + 13).Value;
                            ADC_Cor_sheet.Cell(22, 4).Value = TE_sheet.Cell(55, i + 14).Value;
                            //CH6
                            ADC_Cor_sheet.Cell(23, 4).Value = TE_sheet.Cell(55, i + 15).Value;
                            ADC_Cor_sheet.Cell(24, 4).Value = TE_sheet.Cell(55, i + 16).Value;
                            ADC_Cor_sheet.Cell(25, 4).Value = TE_sheet.Cell(55, i + 17).Value;
                        }
                        if (comboBox3.Text == "4#")
                        {
                            //CH1
                            ADC_Cor_sheet.Cell(8, 4).Value = TE_sheet.Cell(56, i).Value;
                            ADC_Cor_sheet.Cell(9, 4).Value = TE_sheet.Cell(56, i + 1).Value;
                            ADC_Cor_sheet.Cell(10, 4).Value = TE_sheet.Cell(56, i + 2).Value;
                            //CH2
                            ADC_Cor_sheet.Cell(11, 4).Value = TE_sheet.Cell(56, i + 3).Value;
                            ADC_Cor_sheet.Cell(12, 4).Value = TE_sheet.Cell(56, i + 4).Value;
                            ADC_Cor_sheet.Cell(13, 4).Value = TE_sheet.Cell(56, i + 5).Value;
                            //CH3
                            ADC_Cor_sheet.Cell(14, 4).Value = TE_sheet.Cell(56, i + 6).Value;
                            ADC_Cor_sheet.Cell(15, 4).Value = TE_sheet.Cell(56, i + 7).Value;
                            ADC_Cor_sheet.Cell(16, 4).Value = TE_sheet.Cell(56, i + 8).Value;
                            //CH4
                            ADC_Cor_sheet.Cell(17, 4).Value = TE_sheet.Cell(56, i + 9).Value;
                            ADC_Cor_sheet.Cell(18, 4).Value = TE_sheet.Cell(56, i + 10).Value;
                            ADC_Cor_sheet.Cell(19, 4).Value = TE_sheet.Cell(56, i + 11).Value;
                            //CH5
                            ADC_Cor_sheet.Cell(20, 4).Value = TE_sheet.Cell(56, i + 12).Value;
                            ADC_Cor_sheet.Cell(21, 4).Value = TE_sheet.Cell(56, i + 13).Value;
                            ADC_Cor_sheet.Cell(22, 4).Value = TE_sheet.Cell(56, i + 14).Value;
                            //CH6
                            ADC_Cor_sheet.Cell(23, 4).Value = TE_sheet.Cell(56, i + 15).Value;
                            ADC_Cor_sheet.Cell(24, 4).Value = TE_sheet.Cell(56, i + 16).Value;
                            ADC_Cor_sheet.Cell(25, 4).Value = TE_sheet.Cell(56, i + 17).Value;
                        }
                        if (comboBox3.Text == "5#")
                        {
                            //CH1
                            ADC_Cor_sheet.Cell(8, 4).Value = TE_sheet.Cell(57, i).Value;
                            ADC_Cor_sheet.Cell(9, 4).Value = TE_sheet.Cell(57, i + 1).Value;
                            ADC_Cor_sheet.Cell(10, 4).Value = TE_sheet.Cell(57, i + 2).Value;
                            //CH2
                            ADC_Cor_sheet.Cell(11, 4).Value = TE_sheet.Cell(57, i + 3).Value;
                            ADC_Cor_sheet.Cell(12, 4).Value = TE_sheet.Cell(57, i + 4).Value;
                            ADC_Cor_sheet.Cell(13, 4).Value = TE_sheet.Cell(57, i + 5).Value;
                            //CH3
                            ADC_Cor_sheet.Cell(14, 4).Value = TE_sheet.Cell(57, i + 6).Value;
                            ADC_Cor_sheet.Cell(15, 4).Value = TE_sheet.Cell(57, i + 7).Value;
                            ADC_Cor_sheet.Cell(16, 4).Value = TE_sheet.Cell(57, i + 8).Value;
                            //CH4
                            ADC_Cor_sheet.Cell(17, 4).Value = TE_sheet.Cell(57, i + 9).Value;
                            ADC_Cor_sheet.Cell(18, 4).Value = TE_sheet.Cell(57, i + 10).Value;
                            ADC_Cor_sheet.Cell(19, 4).Value = TE_sheet.Cell(57, i + 11).Value;
                            //CH5
                            ADC_Cor_sheet.Cell(20, 4).Value = TE_sheet.Cell(57, i + 12).Value;
                            ADC_Cor_sheet.Cell(21, 4).Value = TE_sheet.Cell(57, i + 13).Value;
                            ADC_Cor_sheet.Cell(22, 4).Value = TE_sheet.Cell(57, i + 14).Value;
                            //CH6
                            ADC_Cor_sheet.Cell(23, 4).Value = TE_sheet.Cell(57, i + 15).Value;
                            ADC_Cor_sheet.Cell(24, 4).Value = TE_sheet.Cell(57, i + 16).Value;
                            ADC_Cor_sheet.Cell(25, 4).Value = TE_sheet.Cell(57, i + 17).Value;
                        }

                    }

                }           

            }
            if (comboBox1.Text == "SY68930")
            {

                //实例化参数
                TE = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\TE.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
                IXLWorksheet TE_sheet = TE.Worksheet(1);


                if (comboBox3.Text == "1#")
                    ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_1#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
                if (comboBox3.Text == "2#")
                    ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_2#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
                if (comboBox3.Text == "3#")
                    ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_3#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
                if (comboBox3.Text == "4#")
                    ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_4#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
                if (comboBox3.Text == "5#")
                    ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_5#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格


                IXLWorksheet ADC_Cor_sheet = ADC_Cor.Worksheet(1);


                //读取TE的测量结果到Result表格

                for (i = 1; i <= 650; i++)
                {
                    //筛选出底层Vbg数据
                    if (Convert.ToString(TE_sheet.Cell(47, i).Value) == "vbg_aftrim") //要看一下TE的数据模板，标签行是不是第47行，否则会错
                    {

                        if (comboBox3.Text == "1#")
                            ADC_Cor_sheet.Cell(5, 4).Value = TE_sheet.Cell(53, i).Value;
                        if (comboBox3.Text == "2#")
                            ADC_Cor_sheet.Cell(5, 4).Value = TE_sheet.Cell(54, i).Value;
                        if (comboBox3.Text == "3#")
                            ADC_Cor_sheet.Cell(5, 4).Value = TE_sheet.Cell(55, i).Value;
                        if (comboBox3.Text == "4#")
                            ADC_Cor_sheet.Cell(5, 4).Value = TE_sheet.Cell(56, i).Value;
                        if (comboBox3.Text == "5#")
                            ADC_Cor_sheet.Cell(5, 4).Value = TE_sheet.Cell(57, i).Value;
                    }
                    //筛选出中层Vbg数据
                    if (Convert.ToString(TE_sheet.Cell(47, i).Value) == "vbg_mid_aftrim") //要看一下TE的数据模板，标签行是不是第47行，否则会错
                    {

                        if (comboBox3.Text == "1#")
                            ADC_Cor_sheet.Cell(6, 4).Value = TE_sheet.Cell(53, i).Value;
                        if (comboBox3.Text == "2#")
                            ADC_Cor_sheet.Cell(6, 4).Value = TE_sheet.Cell(54, i).Value;
                        if (comboBox3.Text == "3#")
                            ADC_Cor_sheet.Cell(6, 4).Value = TE_sheet.Cell(55, i).Value;
                        if (comboBox3.Text == "4#")
                            ADC_Cor_sheet.Cell(6, 4).Value = TE_sheet.Cell(56, i).Value;
                        if (comboBox3.Text == "5#")
                            ADC_Cor_sheet.Cell(6, 4).Value = TE_sheet.Cell(57, i).Value;
                    }

                    //筛选出ADC error起始位置然后开始依次赋值
                    if (Convert.ToString(TE_sheet.Cell(47, i).Value) == "vc10_2v_error")
                    {

                        if (comboBox3.Text == "1#")
                        {
                            //CH1
                            ADC_Cor_sheet.Cell(8, 4).Value = TE_sheet.Cell(53, i).Value;
                            ADC_Cor_sheet.Cell(9, 4).Value = TE_sheet.Cell(53, i + 1).Value;
                            ADC_Cor_sheet.Cell(10, 4).Value = TE_sheet.Cell(53, i + 2).Value;
                            //CH2
                            ADC_Cor_sheet.Cell(11, 4).Value = TE_sheet.Cell(53, i + 3).Value;
                            ADC_Cor_sheet.Cell(12, 4).Value = TE_sheet.Cell(53, i + 4).Value;
                            ADC_Cor_sheet.Cell(13, 4).Value = TE_sheet.Cell(53, i + 5).Value;
                            //CH3
                            ADC_Cor_sheet.Cell(14, 4).Value = TE_sheet.Cell(53, i + 6).Value;
                            ADC_Cor_sheet.Cell(15, 4).Value = TE_sheet.Cell(53, i + 7).Value;
                            ADC_Cor_sheet.Cell(16, 4).Value = TE_sheet.Cell(53, i + 8).Value;
                            //CH4
                            ADC_Cor_sheet.Cell(17, 4).Value = TE_sheet.Cell(53, i + 9).Value;
                            ADC_Cor_sheet.Cell(18, 4).Value = TE_sheet.Cell(53, i + 10).Value;
                            ADC_Cor_sheet.Cell(19, 4).Value = TE_sheet.Cell(53, i + 11).Value;
                            //CH5
                            ADC_Cor_sheet.Cell(20, 4).Value = TE_sheet.Cell(53, i + 12).Value;
                            ADC_Cor_sheet.Cell(21, 4).Value = TE_sheet.Cell(53, i + 13).Value;
                            ADC_Cor_sheet.Cell(22, 4).Value = TE_sheet.Cell(53, i + 14).Value;
                            //CH6
                            ADC_Cor_sheet.Cell(23, 4).Value = TE_sheet.Cell(53, i + 15).Value;
                            ADC_Cor_sheet.Cell(24, 4).Value = TE_sheet.Cell(53, i + 16).Value;
                            ADC_Cor_sheet.Cell(25, 4).Value = TE_sheet.Cell(53, i + 17).Value;
                            //CH7
                            ADC_Cor_sheet.Cell(26, 4).Value = TE_sheet.Cell(53, i + 18).Value;
                            ADC_Cor_sheet.Cell(27, 4).Value = TE_sheet.Cell(53, i + 19).Value;
                            ADC_Cor_sheet.Cell(28, 4).Value = TE_sheet.Cell(53, i + 20).Value;
                            //CH8
                            ADC_Cor_sheet.Cell(29, 4).Value = TE_sheet.Cell(53, i + 21).Value;
                            ADC_Cor_sheet.Cell(30, 4).Value = TE_sheet.Cell(53, i + 22).Value;
                            ADC_Cor_sheet.Cell(31, 4).Value = TE_sheet.Cell(53, i + 23).Value;
                            //CH9
                            ADC_Cor_sheet.Cell(32, 4).Value = TE_sheet.Cell(53, i + 24).Value;
                            ADC_Cor_sheet.Cell(33, 4).Value = TE_sheet.Cell(53, i + 25).Value;
                            ADC_Cor_sheet.Cell(34, 4).Value = TE_sheet.Cell(53, i + 26).Value;
                            //CH10
                            ADC_Cor_sheet.Cell(35, 4).Value = TE_sheet.Cell(53, i + 27).Value;
                            ADC_Cor_sheet.Cell(36, 4).Value = TE_sheet.Cell(53, i + 28).Value;
                            ADC_Cor_sheet.Cell(37, 4).Value = TE_sheet.Cell(53, i + 29).Value;
                            //CH11
                            ADC_Cor_sheet.Cell(38, 4).Value = TE_sheet.Cell(53, i + 30).Value;
                            ADC_Cor_sheet.Cell(39, 4).Value = TE_sheet.Cell(53, i + 31).Value;
                            ADC_Cor_sheet.Cell(40, 4).Value = TE_sheet.Cell(53, i + 32).Value;
                            //CH12
                            ADC_Cor_sheet.Cell(41, 4).Value = TE_sheet.Cell(53, i + 33).Value;
                            ADC_Cor_sheet.Cell(42, 4).Value = TE_sheet.Cell(53, i + 34).Value;
                            ADC_Cor_sheet.Cell(43, 4).Value = TE_sheet.Cell(53, i + 35).Value;
                        }

                        if (comboBox3.Text == "2#")
                        {
                            //CH1
                            ADC_Cor_sheet.Cell(8, 4).Value = TE_sheet.Cell(54, i).Value;
                            ADC_Cor_sheet.Cell(9, 4).Value = TE_sheet.Cell(54, i + 1).Value;
                            ADC_Cor_sheet.Cell(10, 4).Value = TE_sheet.Cell(54, i + 2).Value;
                            //CH2
                            ADC_Cor_sheet.Cell(11, 4).Value = TE_sheet.Cell(54, i + 3).Value;
                            ADC_Cor_sheet.Cell(12, 4).Value = TE_sheet.Cell(54, i + 4).Value;
                            ADC_Cor_sheet.Cell(13, 4).Value = TE_sheet.Cell(54, i + 5).Value;
                            //CH3
                            ADC_Cor_sheet.Cell(14, 4).Value = TE_sheet.Cell(54, i + 6).Value;
                            ADC_Cor_sheet.Cell(15, 4).Value = TE_sheet.Cell(54, i + 7).Value;
                            ADC_Cor_sheet.Cell(16, 4).Value = TE_sheet.Cell(54, i + 8).Value;
                            //CH4
                            ADC_Cor_sheet.Cell(17, 4).Value = TE_sheet.Cell(54, i + 9).Value;
                            ADC_Cor_sheet.Cell(18, 4).Value = TE_sheet.Cell(54, i + 10).Value;
                            ADC_Cor_sheet.Cell(19, 4).Value = TE_sheet.Cell(54, i + 11).Value;
                            //CH5
                            ADC_Cor_sheet.Cell(20, 4).Value = TE_sheet.Cell(54, i + 12).Value;
                            ADC_Cor_sheet.Cell(21, 4).Value = TE_sheet.Cell(54, i + 13).Value;
                            ADC_Cor_sheet.Cell(22, 4).Value = TE_sheet.Cell(54, i + 14).Value;
                            //CH6
                            ADC_Cor_sheet.Cell(23, 4).Value = TE_sheet.Cell(54, i + 15).Value;
                            ADC_Cor_sheet.Cell(24, 4).Value = TE_sheet.Cell(54, i + 16).Value;
                            ADC_Cor_sheet.Cell(25, 4).Value = TE_sheet.Cell(54, i + 17).Value;
                            //CH7
                            ADC_Cor_sheet.Cell(26, 4).Value = TE_sheet.Cell(54, i + 18).Value;
                            ADC_Cor_sheet.Cell(27, 4).Value = TE_sheet.Cell(54, i + 19).Value;
                            ADC_Cor_sheet.Cell(28, 4).Value = TE_sheet.Cell(54, i + 20).Value;
                            //CH8
                            ADC_Cor_sheet.Cell(29, 4).Value = TE_sheet.Cell(54, i + 21).Value;
                            ADC_Cor_sheet.Cell(30, 4).Value = TE_sheet.Cell(54, i + 22).Value;
                            ADC_Cor_sheet.Cell(31, 4).Value = TE_sheet.Cell(54, i + 23).Value;
                            //CH9
                            ADC_Cor_sheet.Cell(32, 4).Value = TE_sheet.Cell(54, i + 24).Value;
                            ADC_Cor_sheet.Cell(33, 4).Value = TE_sheet.Cell(54, i + 25).Value;
                            ADC_Cor_sheet.Cell(34, 4).Value = TE_sheet.Cell(54, i + 26).Value;
                            //CH10
                            ADC_Cor_sheet.Cell(35, 4).Value = TE_sheet.Cell(54, i + 27).Value;
                            ADC_Cor_sheet.Cell(36, 4).Value = TE_sheet.Cell(54, i + 28).Value;
                            ADC_Cor_sheet.Cell(37, 4).Value = TE_sheet.Cell(54, i + 29).Value;
                            //CH11
                            ADC_Cor_sheet.Cell(38, 4).Value = TE_sheet.Cell(54, i + 30).Value;
                            ADC_Cor_sheet.Cell(39, 4).Value = TE_sheet.Cell(54, i + 31).Value;
                            ADC_Cor_sheet.Cell(40, 4).Value = TE_sheet.Cell(54, i + 32).Value;
                            //CH12
                            ADC_Cor_sheet.Cell(41, 4).Value = TE_sheet.Cell(54, i + 33).Value;
                            ADC_Cor_sheet.Cell(42, 4).Value = TE_sheet.Cell(54, i + 34).Value;
                            ADC_Cor_sheet.Cell(43, 4).Value = TE_sheet.Cell(54, i + 35).Value;

                        }
                        if (comboBox3.Text == "3#")
                        {
                            //CH1
                            ADC_Cor_sheet.Cell(8, 4).Value = TE_sheet.Cell(55, i).Value;
                            ADC_Cor_sheet.Cell(9, 4).Value = TE_sheet.Cell(55, i + 1).Value;
                            ADC_Cor_sheet.Cell(10, 4).Value = TE_sheet.Cell(55, i + 2).Value;
                            //CH2
                            ADC_Cor_sheet.Cell(11, 4).Value = TE_sheet.Cell(55, i + 3).Value;
                            ADC_Cor_sheet.Cell(12, 4).Value = TE_sheet.Cell(55, i + 4).Value;
                            ADC_Cor_sheet.Cell(13, 4).Value = TE_sheet.Cell(55, i + 5).Value;
                            //CH3
                            ADC_Cor_sheet.Cell(14, 4).Value = TE_sheet.Cell(55, i + 6).Value;
                            ADC_Cor_sheet.Cell(15, 4).Value = TE_sheet.Cell(55, i + 7).Value;
                            ADC_Cor_sheet.Cell(16, 4).Value = TE_sheet.Cell(55, i + 8).Value;
                            //CH4
                            ADC_Cor_sheet.Cell(17, 4).Value = TE_sheet.Cell(55, i + 9).Value;
                            ADC_Cor_sheet.Cell(18, 4).Value = TE_sheet.Cell(55, i + 10).Value;
                            ADC_Cor_sheet.Cell(19, 4).Value = TE_sheet.Cell(55, i + 11).Value;
                            //CH5
                            ADC_Cor_sheet.Cell(20, 4).Value = TE_sheet.Cell(55, i + 12).Value;
                            ADC_Cor_sheet.Cell(21, 4).Value = TE_sheet.Cell(55, i + 13).Value;
                            ADC_Cor_sheet.Cell(22, 4).Value = TE_sheet.Cell(55, i + 14).Value;
                            //CH6
                            ADC_Cor_sheet.Cell(23, 4).Value = TE_sheet.Cell(55, i + 15).Value;
                            ADC_Cor_sheet.Cell(24, 4).Value = TE_sheet.Cell(55, i + 16).Value;
                            ADC_Cor_sheet.Cell(25, 4).Value = TE_sheet.Cell(55, i + 17).Value;
                            //CH7
                            ADC_Cor_sheet.Cell(26, 4).Value = TE_sheet.Cell(55, i + 18).Value;
                            ADC_Cor_sheet.Cell(27, 4).Value = TE_sheet.Cell(55, i + 19).Value;
                            ADC_Cor_sheet.Cell(28, 4).Value = TE_sheet.Cell(55, i + 20).Value;
                            //CH8
                            ADC_Cor_sheet.Cell(29, 4).Value = TE_sheet.Cell(55, i + 21).Value;
                            ADC_Cor_sheet.Cell(30, 4).Value = TE_sheet.Cell(55, i + 22).Value;
                            ADC_Cor_sheet.Cell(31, 4).Value = TE_sheet.Cell(55, i + 23).Value;
                            //CH9
                            ADC_Cor_sheet.Cell(32, 4).Value = TE_sheet.Cell(55, i + 24).Value;
                            ADC_Cor_sheet.Cell(33, 4).Value = TE_sheet.Cell(55, i + 25).Value;
                            ADC_Cor_sheet.Cell(34, 4).Value = TE_sheet.Cell(55, i + 26).Value;
                            //CH10
                            ADC_Cor_sheet.Cell(35, 4).Value = TE_sheet.Cell(55, i + 27).Value;
                            ADC_Cor_sheet.Cell(36, 4).Value = TE_sheet.Cell(55, i + 28).Value;
                            ADC_Cor_sheet.Cell(37, 4).Value = TE_sheet.Cell(55, i + 29).Value;
                            //CH11
                            ADC_Cor_sheet.Cell(38, 4).Value = TE_sheet.Cell(55, i + 30).Value;
                            ADC_Cor_sheet.Cell(39, 4).Value = TE_sheet.Cell(55, i + 31).Value;
                            ADC_Cor_sheet.Cell(40, 4).Value = TE_sheet.Cell(55, i + 32).Value;
                            //CH12
                            ADC_Cor_sheet.Cell(41, 4).Value = TE_sheet.Cell(55, i + 33).Value;
                            ADC_Cor_sheet.Cell(42, 4).Value = TE_sheet.Cell(55, i + 34).Value;
                            ADC_Cor_sheet.Cell(43, 4).Value = TE_sheet.Cell(55, i + 35).Value;

                        }
                        if (comboBox3.Text == "4#")
                        {
                            //CH1
                            ADC_Cor_sheet.Cell(8, 4).Value = TE_sheet.Cell(56, i).Value;
                            ADC_Cor_sheet.Cell(9, 4).Value = TE_sheet.Cell(56, i + 1).Value;
                            ADC_Cor_sheet.Cell(10, 4).Value = TE_sheet.Cell(56, i + 2).Value;
                            //CH2
                            ADC_Cor_sheet.Cell(11, 4).Value = TE_sheet.Cell(56, i + 3).Value;
                            ADC_Cor_sheet.Cell(12, 4).Value = TE_sheet.Cell(56, i + 4).Value;
                            ADC_Cor_sheet.Cell(13, 4).Value = TE_sheet.Cell(56, i + 5).Value;
                            //CH3
                            ADC_Cor_sheet.Cell(14, 4).Value = TE_sheet.Cell(56, i + 6).Value;
                            ADC_Cor_sheet.Cell(15, 4).Value = TE_sheet.Cell(56, i + 7).Value;
                            ADC_Cor_sheet.Cell(16, 4).Value = TE_sheet.Cell(56, i + 8).Value;
                            //CH4
                            ADC_Cor_sheet.Cell(17, 4).Value = TE_sheet.Cell(56, i + 9).Value;
                            ADC_Cor_sheet.Cell(18, 4).Value = TE_sheet.Cell(56, i + 10).Value;
                            ADC_Cor_sheet.Cell(19, 4).Value = TE_sheet.Cell(56, i + 11).Value;
                            //CH5
                            ADC_Cor_sheet.Cell(20, 4).Value = TE_sheet.Cell(56, i + 12).Value;
                            ADC_Cor_sheet.Cell(21, 4).Value = TE_sheet.Cell(56, i + 13).Value;
                            ADC_Cor_sheet.Cell(22, 4).Value = TE_sheet.Cell(56, i + 14).Value;
                            //CH6
                            ADC_Cor_sheet.Cell(23, 4).Value = TE_sheet.Cell(56, i + 15).Value;
                            ADC_Cor_sheet.Cell(24, 4).Value = TE_sheet.Cell(56, i + 16).Value;
                            ADC_Cor_sheet.Cell(25, 4).Value = TE_sheet.Cell(56, i + 17).Value;
                            //CH7
                            ADC_Cor_sheet.Cell(26, 4).Value = TE_sheet.Cell(56, i + 18).Value;
                            ADC_Cor_sheet.Cell(27, 4).Value = TE_sheet.Cell(56, i + 19).Value;
                            ADC_Cor_sheet.Cell(28, 4).Value = TE_sheet.Cell(56, i + 20).Value;
                            //CH8
                            ADC_Cor_sheet.Cell(29, 4).Value = TE_sheet.Cell(56, i + 21).Value;
                            ADC_Cor_sheet.Cell(30, 4).Value = TE_sheet.Cell(56, i + 22).Value;
                            ADC_Cor_sheet.Cell(31, 4).Value = TE_sheet.Cell(56, i + 23).Value;
                            //CH9
                            ADC_Cor_sheet.Cell(32, 4).Value = TE_sheet.Cell(56, i + 24).Value;
                            ADC_Cor_sheet.Cell(33, 4).Value = TE_sheet.Cell(56, i + 25).Value;
                            ADC_Cor_sheet.Cell(34, 4).Value = TE_sheet.Cell(56, i + 26).Value;
                            //CH10
                            ADC_Cor_sheet.Cell(35, 4).Value = TE_sheet.Cell(56, i + 27).Value;
                            ADC_Cor_sheet.Cell(36, 4).Value = TE_sheet.Cell(56, i + 28).Value;
                            ADC_Cor_sheet.Cell(37, 4).Value = TE_sheet.Cell(56, i + 29).Value;
                            //CH11
                            ADC_Cor_sheet.Cell(38, 4).Value = TE_sheet.Cell(56, i + 30).Value;
                            ADC_Cor_sheet.Cell(39, 4).Value = TE_sheet.Cell(56, i + 31).Value;
                            ADC_Cor_sheet.Cell(40, 4).Value = TE_sheet.Cell(56, i + 32).Value;
                            //CH12
                            ADC_Cor_sheet.Cell(41, 4).Value = TE_sheet.Cell(56, i + 33).Value;
                            ADC_Cor_sheet.Cell(42, 4).Value = TE_sheet.Cell(56, i + 34).Value;
                            ADC_Cor_sheet.Cell(43, 4).Value = TE_sheet.Cell(56, i + 35).Value;

                        }
                        if (comboBox3.Text == "5#")
                        {
                            //CH1
                            ADC_Cor_sheet.Cell(8, 4).Value = TE_sheet.Cell(57, i).Value;
                            ADC_Cor_sheet.Cell(9, 4).Value = TE_sheet.Cell(57, i + 1).Value;
                            ADC_Cor_sheet.Cell(10, 4).Value = TE_sheet.Cell(57, i + 2).Value;
                            //CH2
                            ADC_Cor_sheet.Cell(11, 4).Value = TE_sheet.Cell(57, i + 3).Value;
                            ADC_Cor_sheet.Cell(12, 4).Value = TE_sheet.Cell(57, i + 4).Value;
                            ADC_Cor_sheet.Cell(13, 4).Value = TE_sheet.Cell(57, i + 5).Value;
                            //CH3
                            ADC_Cor_sheet.Cell(14, 4).Value = TE_sheet.Cell(57, i + 6).Value;
                            ADC_Cor_sheet.Cell(15, 4).Value = TE_sheet.Cell(57, i + 7).Value;
                            ADC_Cor_sheet.Cell(16, 4).Value = TE_sheet.Cell(57, i + 8).Value;
                            //CH4
                            ADC_Cor_sheet.Cell(17, 4).Value = TE_sheet.Cell(57, i + 9).Value;
                            ADC_Cor_sheet.Cell(18, 4).Value = TE_sheet.Cell(57, i + 10).Value;
                            ADC_Cor_sheet.Cell(19, 4).Value = TE_sheet.Cell(57, i + 11).Value;
                            //CH5
                            ADC_Cor_sheet.Cell(20, 4).Value = TE_sheet.Cell(57, i + 12).Value;
                            ADC_Cor_sheet.Cell(21, 4).Value = TE_sheet.Cell(57, i + 13).Value;
                            ADC_Cor_sheet.Cell(22, 4).Value = TE_sheet.Cell(57, i + 14).Value;
                            //CH6
                            ADC_Cor_sheet.Cell(23, 4).Value = TE_sheet.Cell(57, i + 15).Value;
                            ADC_Cor_sheet.Cell(24, 4).Value = TE_sheet.Cell(57, i + 16).Value;
                            ADC_Cor_sheet.Cell(25, 4).Value = TE_sheet.Cell(57, i + 17).Value;
                            //CH7
                            ADC_Cor_sheet.Cell(26, 4).Value = TE_sheet.Cell(57, i + 18).Value;
                            ADC_Cor_sheet.Cell(27, 4).Value = TE_sheet.Cell(57, i + 19).Value;
                            ADC_Cor_sheet.Cell(28, 4).Value = TE_sheet.Cell(57, i + 20).Value;
                            //CH8
                            ADC_Cor_sheet.Cell(29, 4).Value = TE_sheet.Cell(57, i + 21).Value;
                            ADC_Cor_sheet.Cell(30, 4).Value = TE_sheet.Cell(57, i + 22).Value;
                            ADC_Cor_sheet.Cell(31, 4).Value = TE_sheet.Cell(57, i + 23).Value;
                            //CH9
                            ADC_Cor_sheet.Cell(32, 4).Value = TE_sheet.Cell(57, i + 24).Value;
                            ADC_Cor_sheet.Cell(33, 4).Value = TE_sheet.Cell(57, i + 25).Value;
                            ADC_Cor_sheet.Cell(34, 4).Value = TE_sheet.Cell(57, i + 26).Value;
                            //CH10
                            ADC_Cor_sheet.Cell(35, 4).Value = TE_sheet.Cell(57, i + 27).Value;
                            ADC_Cor_sheet.Cell(36, 4).Value = TE_sheet.Cell(57, i + 28).Value;
                            ADC_Cor_sheet.Cell(37, 4).Value = TE_sheet.Cell(57, i + 29).Value;
                            //CH11
                            ADC_Cor_sheet.Cell(38, 4).Value = TE_sheet.Cell(57, i + 30).Value;
                            ADC_Cor_sheet.Cell(39, 4).Value = TE_sheet.Cell(57, i + 31).Value;
                            ADC_Cor_sheet.Cell(40, 4).Value = TE_sheet.Cell(57, i + 32).Value;
                            //CH12
                            ADC_Cor_sheet.Cell(41, 4).Value = TE_sheet.Cell(57, i + 33).Value;
                            ADC_Cor_sheet.Cell(42, 4).Value = TE_sheet.Cell(57, i + 34).Value;
                            ADC_Cor_sheet.Cell(43, 4).Value = TE_sheet.Cell(57, i + 35).Value;
                        }

                    }

                }

            }
            if (comboBox1.Text == "SY68940")
            {

                //实例化参数
                TE = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\TE.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
                if (comboBox3.Text == "1#")
                    ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_1#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
                if (comboBox3.Text == "2#")
                    ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_2#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
                if (comboBox3.Text == "3#")
                    ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_3#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
                if (comboBox3.Text == "4#")
                    ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_4#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
                if (comboBox3.Text == "5#")
                    ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_5#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
                IXLWorksheet ADC_Cor_sheet = ADC_Cor.Worksheet(1);
                IXLWorksheet TE_sheet = TE.Worksheet(1);

                //读取TE的测量结果到Result表格

                for (i = 1; i <= 650; i++)
                {
                    //筛选出底层Vbg数据
                    if (Convert.ToString(TE_sheet.Cell(47, i).Value) == "vbg_bottom_aftrim") //要看一下TE的数据模板，标签行是不是第47行，否则会错
                    {

                        if (comboBox3.Text == "1#")
                            ADC_Cor_sheet.Cell(5, 4).Value = TE_sheet.Cell(53, i).Value;
                        if (comboBox3.Text == "2#")
                            ADC_Cor_sheet.Cell(5, 4).Value = TE_sheet.Cell(54, i).Value;
                        if (comboBox3.Text == "3#")
                            ADC_Cor_sheet.Cell(5, 4).Value = TE_sheet.Cell(55, i).Value;
                        if (comboBox3.Text == "4#")
                            ADC_Cor_sheet.Cell(5, 4).Value = TE_sheet.Cell(56, i).Value;
                        if (comboBox3.Text == "5#")
                            ADC_Cor_sheet.Cell(5, 4).Value = TE_sheet.Cell(57, i).Value;
                    }
                    //筛选出中层Vbg数据
                    if (Convert.ToString(TE_sheet.Cell(47, i).Value) == "vbg_mid_aftrim") //要看一下TE的数据模板，标签行是不是第47行，否则会错
                    {

                        if (comboBox3.Text == "1#")
                            ADC_Cor_sheet.Cell(6, 4).Value = TE_sheet.Cell(53, i).Value;
                        if (comboBox3.Text == "2#")
                            ADC_Cor_sheet.Cell(6, 4).Value = TE_sheet.Cell(54, i).Value;
                        if (comboBox3.Text == "3#")
                            ADC_Cor_sheet.Cell(6, 4).Value = TE_sheet.Cell(55, i).Value;
                        if (comboBox3.Text == "4#")
                            ADC_Cor_sheet.Cell(6, 4).Value = TE_sheet.Cell(56, i).Value;
                        if (comboBox3.Text == "5#")
                            ADC_Cor_sheet.Cell(6, 4).Value = TE_sheet.Cell(57, i).Value;
                    }
                    //筛选出上层Vbg数据
                    if (Convert.ToString(TE_sheet.Cell(47, i).Value) == "vbg_top_aftrim") //要看一下TE的数据模板，标签行是不是第47行，否则会错
                    {

                        if (comboBox3.Text == "1#")
                            ADC_Cor_sheet.Cell(7, 4).Value = TE_sheet.Cell(53, i).Value;
                        if (comboBox3.Text == "2#")
                            ADC_Cor_sheet.Cell(7, 4).Value = TE_sheet.Cell(54, i).Value;
                        if (comboBox3.Text == "3#")
                            ADC_Cor_sheet.Cell(7, 4).Value = TE_sheet.Cell(55, i).Value;
                        if (comboBox3.Text == "4#")
                            ADC_Cor_sheet.Cell(7, 4).Value = TE_sheet.Cell(56, i).Value;
                        if (comboBox3.Text == "5#")
                            ADC_Cor_sheet.Cell(7, 4).Value = TE_sheet.Cell(57, i).Value;
                    }

                    //筛选出ADC error起始位置然后开始依次赋值
                    if (Convert.ToString(TE_sheet.Cell(47, i).Value) == "vc10_2v_error")
                    {

                        if (comboBox3.Text == "1#")
                        {
                            //CH1
                            ADC_Cor_sheet.Cell(8, 4).Value = TE_sheet.Cell(53, i).Value;
                            ADC_Cor_sheet.Cell(9, 4).Value = TE_sheet.Cell(53, i + 1).Value;
                            ADC_Cor_sheet.Cell(10, 4).Value = TE_sheet.Cell(53, i + 2).Value;
                            //CH2
                            ADC_Cor_sheet.Cell(11, 4).Value = TE_sheet.Cell(53, i + 3).Value;
                            ADC_Cor_sheet.Cell(12, 4).Value = TE_sheet.Cell(53, i + 4).Value;
                            ADC_Cor_sheet.Cell(13, 4).Value = TE_sheet.Cell(53, i + 5).Value;
                            //CH3
                            ADC_Cor_sheet.Cell(14, 4).Value = TE_sheet.Cell(53, i + 6).Value;
                            ADC_Cor_sheet.Cell(15, 4).Value = TE_sheet.Cell(53, i + 7).Value;
                            ADC_Cor_sheet.Cell(16, 4).Value = TE_sheet.Cell(53, i + 8).Value;
                            //CH4
                            ADC_Cor_sheet.Cell(17, 4).Value = TE_sheet.Cell(53, i + 9).Value;
                            ADC_Cor_sheet.Cell(18, 4).Value = TE_sheet.Cell(53, i + 10).Value;
                            ADC_Cor_sheet.Cell(19, 4).Value = TE_sheet.Cell(53, i + 11).Value;
                            //CH5
                            ADC_Cor_sheet.Cell(20, 4).Value = TE_sheet.Cell(53, i + 12).Value;
                            ADC_Cor_sheet.Cell(21, 4).Value = TE_sheet.Cell(53, i + 13).Value;
                            ADC_Cor_sheet.Cell(22, 4).Value = TE_sheet.Cell(53, i + 14).Value;
                            //CH6
                            ADC_Cor_sheet.Cell(23, 4).Value = TE_sheet.Cell(53, i + 15).Value;
                            ADC_Cor_sheet.Cell(24, 4).Value = TE_sheet.Cell(53, i + 16).Value;
                            ADC_Cor_sheet.Cell(25, 4).Value = TE_sheet.Cell(53, i + 17).Value;
                            //CH7
                            ADC_Cor_sheet.Cell(26, 4).Value = TE_sheet.Cell(53, i+18).Value;
                            ADC_Cor_sheet.Cell(27, 4).Value = TE_sheet.Cell(53, i + 19).Value;
                            ADC_Cor_sheet.Cell(28, 4).Value = TE_sheet.Cell(53, i + 20).Value;
                            //CH8
                            ADC_Cor_sheet.Cell(29, 4).Value = TE_sheet.Cell(53, i + 21).Value;
                            ADC_Cor_sheet.Cell(30, 4).Value = TE_sheet.Cell(53, i +22).Value;
                            ADC_Cor_sheet.Cell(31, 4).Value = TE_sheet.Cell(53, i + 23).Value;
                            //CH9
                            ADC_Cor_sheet.Cell(32, 4).Value = TE_sheet.Cell(53, i + 24).Value;
                            ADC_Cor_sheet.Cell(33, 4).Value = TE_sheet.Cell(53, i + 25).Value;
                            ADC_Cor_sheet.Cell(34, 4).Value = TE_sheet.Cell(53, i + 26).Value;
                            //CH10
                            ADC_Cor_sheet.Cell(35, 4).Value = TE_sheet.Cell(53, i + 27).Value;
                            ADC_Cor_sheet.Cell(36, 4).Value = TE_sheet.Cell(53, i + 28).Value;
                            ADC_Cor_sheet.Cell(37, 4).Value = TE_sheet.Cell(53, i + 29).Value;
                            //CH11
                            ADC_Cor_sheet.Cell(38, 4).Value = TE_sheet.Cell(53, i + 30).Value;
                            ADC_Cor_sheet.Cell(39, 4).Value = TE_sheet.Cell(53, i + 31).Value;
                            ADC_Cor_sheet.Cell(40, 4).Value = TE_sheet.Cell(53, i + 32).Value;
                            //CH12
                            ADC_Cor_sheet.Cell(41, 4).Value = TE_sheet.Cell(53, i + 33).Value;
                            ADC_Cor_sheet.Cell(42, 4).Value = TE_sheet.Cell(53, i + 34).Value;
                            ADC_Cor_sheet.Cell(43, 4).Value = TE_sheet.Cell(53, i + 35).Value;
                            //CH13
                            ADC_Cor_sheet.Cell(44, 4).Value = TE_sheet.Cell(53, i+36).Value;
                            ADC_Cor_sheet.Cell(45, 4).Value = TE_sheet.Cell(53, i + 37).Value;
                            ADC_Cor_sheet.Cell(46, 4).Value = TE_sheet.Cell(53, i + 38).Value;
                            //CH14
                            ADC_Cor_sheet.Cell(47, 4).Value = TE_sheet.Cell(53, i + 39).Value;
                            ADC_Cor_sheet.Cell(48, 4).Value = TE_sheet.Cell(53, i + 40).Value;
                            ADC_Cor_sheet.Cell(49, 4).Value = TE_sheet.Cell(53, i + 41).Value;
                            //CH15
                            ADC_Cor_sheet.Cell(50, 4).Value = TE_sheet.Cell(53, i + 42).Value;
                            ADC_Cor_sheet.Cell(51, 4).Value = TE_sheet.Cell(53, i + 43).Value;
                            ADC_Cor_sheet.Cell(52, 4).Value = TE_sheet.Cell(53, i + 44).Value;
                            //CH16
                            ADC_Cor_sheet.Cell(53, 4).Value = TE_sheet.Cell(53, i + 45).Value;
                            ADC_Cor_sheet.Cell(54, 4).Value = TE_sheet.Cell(53, i + 46).Value;
                            ADC_Cor_sheet.Cell(55, 4).Value = TE_sheet.Cell(53, i + 47).Value;
                            //CH17
                            ADC_Cor_sheet.Cell(56, 4).Value = TE_sheet.Cell(53, i + 48).Value;
                            ADC_Cor_sheet.Cell(57, 4).Value = TE_sheet.Cell(53, i + 49).Value;
                            ADC_Cor_sheet.Cell(58, 4).Value = TE_sheet.Cell(53, i + 50).Value;
                            //CH18
                            ADC_Cor_sheet.Cell(59, 4).Value = TE_sheet.Cell(53, i + 51).Value;
                            ADC_Cor_sheet.Cell(60, 4).Value = TE_sheet.Cell(53, i + 52).Value;
                            ADC_Cor_sheet.Cell(61, 4).Value = TE_sheet.Cell(53, i + 53).Value;
                        }

                        if (comboBox3.Text == "2#")
                        {
                            //CH1
                            ADC_Cor_sheet.Cell(8, 4).Value = TE_sheet.Cell(54, i).Value;
                            ADC_Cor_sheet.Cell(9, 4).Value = TE_sheet.Cell(54, i + 1).Value;
                            ADC_Cor_sheet.Cell(10, 4).Value = TE_sheet.Cell(54, i + 2).Value;
                            //CH2
                            ADC_Cor_sheet.Cell(11, 4).Value = TE_sheet.Cell(54, i + 3).Value;
                            ADC_Cor_sheet.Cell(12, 4).Value = TE_sheet.Cell(54, i + 4).Value;
                            ADC_Cor_sheet.Cell(13, 4).Value = TE_sheet.Cell(54, i + 5).Value;
                            //CH3
                            ADC_Cor_sheet.Cell(14, 4).Value = TE_sheet.Cell(54, i + 6).Value;
                            ADC_Cor_sheet.Cell(15, 4).Value = TE_sheet.Cell(54, i + 7).Value;
                            ADC_Cor_sheet.Cell(16, 4).Value = TE_sheet.Cell(54, i + 8).Value;
                            //CH4
                            ADC_Cor_sheet.Cell(17, 4).Value = TE_sheet.Cell(54, i + 9).Value;
                            ADC_Cor_sheet.Cell(18, 4).Value = TE_sheet.Cell(54, i + 10).Value;
                            ADC_Cor_sheet.Cell(19, 4).Value = TE_sheet.Cell(54, i + 11).Value;
                            //CH5
                            ADC_Cor_sheet.Cell(20, 4).Value = TE_sheet.Cell(54, i + 12).Value;
                            ADC_Cor_sheet.Cell(21, 4).Value = TE_sheet.Cell(54, i + 13).Value;
                            ADC_Cor_sheet.Cell(22, 4).Value = TE_sheet.Cell(54, i + 14).Value;
                            //CH6
                            ADC_Cor_sheet.Cell(23, 4).Value = TE_sheet.Cell(54, i + 15).Value;
                            ADC_Cor_sheet.Cell(24, 4).Value = TE_sheet.Cell(54, i + 16).Value;
                            ADC_Cor_sheet.Cell(25, 4).Value = TE_sheet.Cell(54, i + 17).Value;
                            //CH7
                            ADC_Cor_sheet.Cell(26, 4).Value = TE_sheet.Cell(54, i + 18).Value;
                            ADC_Cor_sheet.Cell(27, 4).Value = TE_sheet.Cell(54, i + 19).Value;
                            ADC_Cor_sheet.Cell(28, 4).Value = TE_sheet.Cell(54, i + 20).Value;
                            //CH8
                            ADC_Cor_sheet.Cell(29, 4).Value = TE_sheet.Cell(54, i + 21).Value;
                            ADC_Cor_sheet.Cell(30, 4).Value = TE_sheet.Cell(54, i + 22).Value;
                            ADC_Cor_sheet.Cell(31, 4).Value = TE_sheet.Cell(54, i + 23).Value;
                            //CH9
                            ADC_Cor_sheet.Cell(32, 4).Value = TE_sheet.Cell(54, i + 24).Value;
                            ADC_Cor_sheet.Cell(33, 4).Value = TE_sheet.Cell(54, i + 25).Value;
                            ADC_Cor_sheet.Cell(34, 4).Value = TE_sheet.Cell(54, i + 26).Value;
                            //CH10
                            ADC_Cor_sheet.Cell(35, 4).Value = TE_sheet.Cell(54, i + 27).Value;
                            ADC_Cor_sheet.Cell(36, 4).Value = TE_sheet.Cell(54, i + 28).Value;
                            ADC_Cor_sheet.Cell(37, 4).Value = TE_sheet.Cell(54, i + 29).Value;
                            //CH11
                            ADC_Cor_sheet.Cell(38, 4).Value = TE_sheet.Cell(54, i + 30).Value;
                            ADC_Cor_sheet.Cell(39, 4).Value = TE_sheet.Cell(54, i + 31).Value;
                            ADC_Cor_sheet.Cell(40, 4).Value = TE_sheet.Cell(54, i + 32).Value;
                            //CH12
                            ADC_Cor_sheet.Cell(41, 4).Value = TE_sheet.Cell(54, i + 33).Value;
                            ADC_Cor_sheet.Cell(42, 4).Value = TE_sheet.Cell(54, i + 34).Value;
                            ADC_Cor_sheet.Cell(43, 4).Value = TE_sheet.Cell(54, i + 35).Value;
                            //CH13
                            ADC_Cor_sheet.Cell(44, 4).Value = TE_sheet.Cell(54, i + 36).Value;
                            ADC_Cor_sheet.Cell(45, 4).Value = TE_sheet.Cell(54, i + 37).Value;
                            ADC_Cor_sheet.Cell(46, 4).Value = TE_sheet.Cell(54, i + 38).Value;
                            //CH14
                            ADC_Cor_sheet.Cell(47, 4).Value = TE_sheet.Cell(54, i + 39).Value;
                            ADC_Cor_sheet.Cell(48, 4).Value = TE_sheet.Cell(54, i + 40).Value;
                            ADC_Cor_sheet.Cell(49, 4).Value = TE_sheet.Cell(54, i + 41).Value;
                            //CH15
                            ADC_Cor_sheet.Cell(50, 4).Value = TE_sheet.Cell(54, i + 42).Value;
                            ADC_Cor_sheet.Cell(51, 4).Value = TE_sheet.Cell(54, i + 43).Value;
                            ADC_Cor_sheet.Cell(52, 4).Value = TE_sheet.Cell(54, i + 44).Value;
                            //CH16
                            ADC_Cor_sheet.Cell(53, 4).Value = TE_sheet.Cell(54, i + 45).Value;
                            ADC_Cor_sheet.Cell(54, 4).Value = TE_sheet.Cell(54, i + 46).Value;
                            ADC_Cor_sheet.Cell(55, 4).Value = TE_sheet.Cell(54, i + 47).Value;
                            //CH17
                            ADC_Cor_sheet.Cell(56, 4).Value = TE_sheet.Cell(54, i + 48).Value;
                            ADC_Cor_sheet.Cell(57, 4).Value = TE_sheet.Cell(54, i + 49).Value;
                            ADC_Cor_sheet.Cell(58, 4).Value = TE_sheet.Cell(54, i + 50).Value;
                            //CH18
                            ADC_Cor_sheet.Cell(59, 4).Value = TE_sheet.Cell(54, i + 51).Value;
                            ADC_Cor_sheet.Cell(60, 4).Value = TE_sheet.Cell(54, i + 52).Value;
                            ADC_Cor_sheet.Cell(61, 4).Value = TE_sheet.Cell(54, i + 53).Value;
                        }
                        if (comboBox3.Text == "3#")
                        {
                            //CH1
                            ADC_Cor_sheet.Cell(8, 4).Value = TE_sheet.Cell(55, i).Value;
                            ADC_Cor_sheet.Cell(9, 4).Value = TE_sheet.Cell(55, i + 1).Value;
                            ADC_Cor_sheet.Cell(10, 4).Value = TE_sheet.Cell(55, i + 2).Value;
                            //CH2
                            ADC_Cor_sheet.Cell(11, 4).Value = TE_sheet.Cell(55, i + 3).Value;
                            ADC_Cor_sheet.Cell(12, 4).Value = TE_sheet.Cell(55, i + 4).Value;
                            ADC_Cor_sheet.Cell(13, 4).Value = TE_sheet.Cell(55, i + 5).Value;
                            //CH3
                            ADC_Cor_sheet.Cell(14, 4).Value = TE_sheet.Cell(55, i + 6).Value;
                            ADC_Cor_sheet.Cell(15, 4).Value = TE_sheet.Cell(55, i + 7).Value;
                            ADC_Cor_sheet.Cell(16, 4).Value = TE_sheet.Cell(55, i + 8).Value;
                            //CH4
                            ADC_Cor_sheet.Cell(17, 4).Value = TE_sheet.Cell(55, i + 9).Value;
                            ADC_Cor_sheet.Cell(18, 4).Value = TE_sheet.Cell(55, i + 10).Value;
                            ADC_Cor_sheet.Cell(19, 4).Value = TE_sheet.Cell(55, i + 11).Value;
                            //CH5
                            ADC_Cor_sheet.Cell(20, 4).Value = TE_sheet.Cell(55, i + 12).Value;
                            ADC_Cor_sheet.Cell(21, 4).Value = TE_sheet.Cell(55, i + 13).Value;
                            ADC_Cor_sheet.Cell(22, 4).Value = TE_sheet.Cell(55, i + 14).Value;
                            //CH6
                            ADC_Cor_sheet.Cell(23, 4).Value = TE_sheet.Cell(55, i + 15).Value;
                            ADC_Cor_sheet.Cell(24, 4).Value = TE_sheet.Cell(55, i + 16).Value;
                            ADC_Cor_sheet.Cell(25, 4).Value = TE_sheet.Cell(55, i + 17).Value;
                            //CH7
                            ADC_Cor_sheet.Cell(26, 4).Value = TE_sheet.Cell(55, i + 18).Value;
                            ADC_Cor_sheet.Cell(27, 4).Value = TE_sheet.Cell(55, i + 19).Value;
                            ADC_Cor_sheet.Cell(28, 4).Value = TE_sheet.Cell(55, i + 20).Value;
                            //CH8
                            ADC_Cor_sheet.Cell(29, 4).Value = TE_sheet.Cell(55, i + 21).Value;
                            ADC_Cor_sheet.Cell(30, 4).Value = TE_sheet.Cell(55, i + 22).Value;
                            ADC_Cor_sheet.Cell(31, 4).Value = TE_sheet.Cell(55, i + 23).Value;
                            //CH9
                            ADC_Cor_sheet.Cell(32, 4).Value = TE_sheet.Cell(55, i + 24).Value;
                            ADC_Cor_sheet.Cell(33, 4).Value = TE_sheet.Cell(55, i + 25).Value;
                            ADC_Cor_sheet.Cell(34, 4).Value = TE_sheet.Cell(55, i + 26).Value;
                            //CH10
                            ADC_Cor_sheet.Cell(35, 4).Value = TE_sheet.Cell(55, i + 27).Value;
                            ADC_Cor_sheet.Cell(36, 4).Value = TE_sheet.Cell(55, i + 28).Value;
                            ADC_Cor_sheet.Cell(37, 4).Value = TE_sheet.Cell(55, i + 29).Value;
                            //CH11
                            ADC_Cor_sheet.Cell(38, 4).Value = TE_sheet.Cell(55, i + 30).Value;
                            ADC_Cor_sheet.Cell(39, 4).Value = TE_sheet.Cell(55, i + 31).Value;
                            ADC_Cor_sheet.Cell(40, 4).Value = TE_sheet.Cell(55, i + 32).Value;
                            //CH12
                            ADC_Cor_sheet.Cell(41, 4).Value = TE_sheet.Cell(55, i + 33).Value;
                            ADC_Cor_sheet.Cell(42, 4).Value = TE_sheet.Cell(55, i + 34).Value;
                            ADC_Cor_sheet.Cell(43, 4).Value = TE_sheet.Cell(55, i + 35).Value;
                            //CH13
                            ADC_Cor_sheet.Cell(44, 4).Value = TE_sheet.Cell(55, i + 36).Value;
                            ADC_Cor_sheet.Cell(45, 4).Value = TE_sheet.Cell(55, i + 37).Value;
                            ADC_Cor_sheet.Cell(46, 4).Value = TE_sheet.Cell(55, i + 38).Value;
                            //CH14
                            ADC_Cor_sheet.Cell(47, 4).Value = TE_sheet.Cell(55, i + 39).Value;
                            ADC_Cor_sheet.Cell(48, 4).Value = TE_sheet.Cell(55, i + 40).Value;
                            ADC_Cor_sheet.Cell(49, 4).Value = TE_sheet.Cell(55, i + 41).Value;
                            //CH15
                            ADC_Cor_sheet.Cell(50, 4).Value = TE_sheet.Cell(55, i + 42).Value;
                            ADC_Cor_sheet.Cell(51, 4).Value = TE_sheet.Cell(55, i + 43).Value;
                            ADC_Cor_sheet.Cell(52, 4).Value = TE_sheet.Cell(55, i + 44).Value;
                            //CH16
                            ADC_Cor_sheet.Cell(53, 4).Value = TE_sheet.Cell(55, i + 45).Value;
                            ADC_Cor_sheet.Cell(54, 4).Value = TE_sheet.Cell(55, i + 46).Value;
                            ADC_Cor_sheet.Cell(55, 4).Value = TE_sheet.Cell(55, i + 47).Value;
                            //CH17
                            ADC_Cor_sheet.Cell(56, 4).Value = TE_sheet.Cell(55, i + 48).Value;
                            ADC_Cor_sheet.Cell(57, 4).Value = TE_sheet.Cell(55, i + 49).Value;
                            ADC_Cor_sheet.Cell(58, 4).Value = TE_sheet.Cell(55, i + 50).Value;
                            //CH18
                            ADC_Cor_sheet.Cell(59, 4).Value = TE_sheet.Cell(55, i + 51).Value;
                            ADC_Cor_sheet.Cell(60, 4).Value = TE_sheet.Cell(55, i + 52).Value;
                            ADC_Cor_sheet.Cell(61, 4).Value = TE_sheet.Cell(55, i + 53).Value;
                        }
                        if (comboBox3.Text == "4#")
                        {
                            //CH1
                            ADC_Cor_sheet.Cell(8, 4).Value = TE_sheet.Cell(56, i).Value;
                            ADC_Cor_sheet.Cell(9, 4).Value = TE_sheet.Cell(56, i + 1).Value;
                            ADC_Cor_sheet.Cell(10, 4).Value = TE_sheet.Cell(56, i + 2).Value;
                            //CH2
                            ADC_Cor_sheet.Cell(11, 4).Value = TE_sheet.Cell(56, i + 3).Value;
                            ADC_Cor_sheet.Cell(12, 4).Value = TE_sheet.Cell(56, i + 4).Value;
                            ADC_Cor_sheet.Cell(13, 4).Value = TE_sheet.Cell(56, i + 5).Value;
                            //CH3
                            ADC_Cor_sheet.Cell(14, 4).Value = TE_sheet.Cell(56, i + 6).Value;
                            ADC_Cor_sheet.Cell(15, 4).Value = TE_sheet.Cell(56, i + 7).Value;
                            ADC_Cor_sheet.Cell(16, 4).Value = TE_sheet.Cell(56, i + 8).Value;
                            //CH4
                            ADC_Cor_sheet.Cell(17, 4).Value = TE_sheet.Cell(56, i + 9).Value;
                            ADC_Cor_sheet.Cell(18, 4).Value = TE_sheet.Cell(56, i + 10).Value;
                            ADC_Cor_sheet.Cell(19, 4).Value = TE_sheet.Cell(56, i + 11).Value;
                            //CH5
                            ADC_Cor_sheet.Cell(20, 4).Value = TE_sheet.Cell(56, i + 12).Value;
                            ADC_Cor_sheet.Cell(21, 4).Value = TE_sheet.Cell(56, i + 13).Value;
                            ADC_Cor_sheet.Cell(22, 4).Value = TE_sheet.Cell(56, i + 14).Value;
                            //CH6
                            ADC_Cor_sheet.Cell(23, 4).Value = TE_sheet.Cell(56, i + 15).Value;
                            ADC_Cor_sheet.Cell(24, 4).Value = TE_sheet.Cell(56, i + 16).Value;
                            ADC_Cor_sheet.Cell(25, 4).Value = TE_sheet.Cell(56, i + 17).Value;
                            //CH7
                            ADC_Cor_sheet.Cell(26, 4).Value = TE_sheet.Cell(56, i + 18).Value;
                            ADC_Cor_sheet.Cell(27, 4).Value = TE_sheet.Cell(56, i + 19).Value;
                            ADC_Cor_sheet.Cell(28, 4).Value = TE_sheet.Cell(56, i + 20).Value;
                            //CH8
                            ADC_Cor_sheet.Cell(29, 4).Value = TE_sheet.Cell(56, i + 21).Value;
                            ADC_Cor_sheet.Cell(30, 4).Value = TE_sheet.Cell(56, i + 22).Value;
                            ADC_Cor_sheet.Cell(31, 4).Value = TE_sheet.Cell(56, i + 23).Value;
                            //CH9
                            ADC_Cor_sheet.Cell(32, 4).Value = TE_sheet.Cell(56, i + 24).Value;
                            ADC_Cor_sheet.Cell(33, 4).Value = TE_sheet.Cell(56, i + 25).Value;
                            ADC_Cor_sheet.Cell(34, 4).Value = TE_sheet.Cell(56, i + 26).Value;
                            //CH10
                            ADC_Cor_sheet.Cell(35, 4).Value = TE_sheet.Cell(56, i + 27).Value;
                            ADC_Cor_sheet.Cell(36, 4).Value = TE_sheet.Cell(56, i + 28).Value;
                            ADC_Cor_sheet.Cell(37, 4).Value = TE_sheet.Cell(56, i + 29).Value;
                            //CH11
                            ADC_Cor_sheet.Cell(38, 4).Value = TE_sheet.Cell(56, i + 30).Value;
                            ADC_Cor_sheet.Cell(39, 4).Value = TE_sheet.Cell(56, i + 31).Value;
                            ADC_Cor_sheet.Cell(40, 4).Value = TE_sheet.Cell(56, i + 32).Value;
                            //CH12
                            ADC_Cor_sheet.Cell(41, 4).Value = TE_sheet.Cell(56, i + 33).Value;
                            ADC_Cor_sheet.Cell(42, 4).Value = TE_sheet.Cell(56, i + 34).Value;
                            ADC_Cor_sheet.Cell(43, 4).Value = TE_sheet.Cell(56, i + 35).Value;
                            //CH13
                            ADC_Cor_sheet.Cell(44, 4).Value = TE_sheet.Cell(56, i + 36).Value;
                            ADC_Cor_sheet.Cell(45, 4).Value = TE_sheet.Cell(56, i + 37).Value;
                            ADC_Cor_sheet.Cell(46, 4).Value = TE_sheet.Cell(56, i + 38).Value;
                            //CH14
                            ADC_Cor_sheet.Cell(47, 4).Value = TE_sheet.Cell(56, i + 39).Value;
                            ADC_Cor_sheet.Cell(48, 4).Value = TE_sheet.Cell(56, i + 40).Value;
                            ADC_Cor_sheet.Cell(49, 4).Value = TE_sheet.Cell(56, i + 41).Value;
                            //CH15
                            ADC_Cor_sheet.Cell(50, 4).Value = TE_sheet.Cell(56, i + 42).Value;
                            ADC_Cor_sheet.Cell(51, 4).Value = TE_sheet.Cell(56, i + 43).Value;
                            ADC_Cor_sheet.Cell(52, 4).Value = TE_sheet.Cell(56, i + 44).Value;
                            //CH16
                            ADC_Cor_sheet.Cell(53, 4).Value = TE_sheet.Cell(56, i + 45).Value;
                            ADC_Cor_sheet.Cell(54, 4).Value = TE_sheet.Cell(56, i + 46).Value;
                            ADC_Cor_sheet.Cell(55, 4).Value = TE_sheet.Cell(56, i + 47).Value;
                            //CH17
                            ADC_Cor_sheet.Cell(56, 4).Value = TE_sheet.Cell(56, i + 48).Value;
                            ADC_Cor_sheet.Cell(57, 4).Value = TE_sheet.Cell(56, i + 49).Value;
                            ADC_Cor_sheet.Cell(58, 4).Value = TE_sheet.Cell(56, i + 50).Value;
                            //CH18
                            ADC_Cor_sheet.Cell(59, 4).Value = TE_sheet.Cell(56, i + 51).Value;
                            ADC_Cor_sheet.Cell(60, 4).Value = TE_sheet.Cell(56, i + 52).Value;
                            ADC_Cor_sheet.Cell(61, 4).Value = TE_sheet.Cell(56, i + 53).Value;
                        }
                        if (comboBox3.Text == "5#")
                        {
                            //CH1
                            ADC_Cor_sheet.Cell(8, 4).Value = TE_sheet.Cell(57, i).Value;
                            ADC_Cor_sheet.Cell(9, 4).Value = TE_sheet.Cell(57, i + 1).Value;
                            ADC_Cor_sheet.Cell(10, 4).Value = TE_sheet.Cell(57, i + 2).Value;
                            //CH2
                            ADC_Cor_sheet.Cell(11, 4).Value = TE_sheet.Cell(57, i + 3).Value;
                            ADC_Cor_sheet.Cell(12, 4).Value = TE_sheet.Cell(57, i + 4).Value;
                            ADC_Cor_sheet.Cell(13, 4).Value = TE_sheet.Cell(57, i + 5).Value;
                            //CH3
                            ADC_Cor_sheet.Cell(14, 4).Value = TE_sheet.Cell(57, i + 6).Value;
                            ADC_Cor_sheet.Cell(15, 4).Value = TE_sheet.Cell(57, i + 7).Value;
                            ADC_Cor_sheet.Cell(16, 4).Value = TE_sheet.Cell(57, i + 8).Value;
                            //CH4
                            ADC_Cor_sheet.Cell(17, 4).Value = TE_sheet.Cell(57, i + 9).Value;
                            ADC_Cor_sheet.Cell(18, 4).Value = TE_sheet.Cell(57, i + 10).Value;
                            ADC_Cor_sheet.Cell(19, 4).Value = TE_sheet.Cell(57, i + 11).Value;
                            //CH5
                            ADC_Cor_sheet.Cell(20, 4).Value = TE_sheet.Cell(57, i + 12).Value;
                            ADC_Cor_sheet.Cell(21, 4).Value = TE_sheet.Cell(57, i + 13).Value;
                            ADC_Cor_sheet.Cell(22, 4).Value = TE_sheet.Cell(57, i + 14).Value;
                            //CH6
                            ADC_Cor_sheet.Cell(23, 4).Value = TE_sheet.Cell(57, i + 15).Value;
                            ADC_Cor_sheet.Cell(24, 4).Value = TE_sheet.Cell(57, i + 16).Value;
                            ADC_Cor_sheet.Cell(25, 4).Value = TE_sheet.Cell(57, i + 17).Value;
                            //CH7
                            ADC_Cor_sheet.Cell(26, 4).Value = TE_sheet.Cell(57, i + 18).Value;
                            ADC_Cor_sheet.Cell(27, 4).Value = TE_sheet.Cell(57, i + 19).Value;
                            ADC_Cor_sheet.Cell(28, 4).Value = TE_sheet.Cell(57, i + 20).Value;
                            //CH8
                            ADC_Cor_sheet.Cell(29, 4).Value = TE_sheet.Cell(57, i + 21).Value;
                            ADC_Cor_sheet.Cell(30, 4).Value = TE_sheet.Cell(57, i + 22).Value;
                            ADC_Cor_sheet.Cell(31, 4).Value = TE_sheet.Cell(57, i + 23).Value;
                            //CH9
                            ADC_Cor_sheet.Cell(32, 4).Value = TE_sheet.Cell(57, i + 24).Value;
                            ADC_Cor_sheet.Cell(33, 4).Value = TE_sheet.Cell(57, i + 25).Value;
                            ADC_Cor_sheet.Cell(34, 4).Value = TE_sheet.Cell(57, i + 26).Value;
                            //CH10
                            ADC_Cor_sheet.Cell(35, 4).Value = TE_sheet.Cell(57, i + 27).Value;
                            ADC_Cor_sheet.Cell(36, 4).Value = TE_sheet.Cell(57, i + 28).Value;
                            ADC_Cor_sheet.Cell(37, 4).Value = TE_sheet.Cell(57, i + 29).Value;
                            //CH11
                            ADC_Cor_sheet.Cell(38, 4).Value = TE_sheet.Cell(57, i + 30).Value;
                            ADC_Cor_sheet.Cell(39, 4).Value = TE_sheet.Cell(57, i + 31).Value;
                            ADC_Cor_sheet.Cell(40, 4).Value = TE_sheet.Cell(57, i + 32).Value;
                            //CH12
                            ADC_Cor_sheet.Cell(41, 4).Value = TE_sheet.Cell(57, i + 33).Value;
                            ADC_Cor_sheet.Cell(42, 4).Value = TE_sheet.Cell(57, i + 34).Value;
                            ADC_Cor_sheet.Cell(43, 4).Value = TE_sheet.Cell(57, i + 35).Value;
                            //CH13
                            ADC_Cor_sheet.Cell(44, 4).Value = TE_sheet.Cell(57, i + 36).Value;
                            ADC_Cor_sheet.Cell(45, 4).Value = TE_sheet.Cell(57, i + 37).Value;
                            ADC_Cor_sheet.Cell(46, 4).Value = TE_sheet.Cell(57, i + 38).Value;
                            //CH14
                            ADC_Cor_sheet.Cell(47, 4).Value = TE_sheet.Cell(57, i + 39).Value;
                            ADC_Cor_sheet.Cell(48, 4).Value = TE_sheet.Cell(57, i + 40).Value;
                            ADC_Cor_sheet.Cell(49, 4).Value = TE_sheet.Cell(57, i + 41).Value;
                            //CH15
                            ADC_Cor_sheet.Cell(50, 4).Value = TE_sheet.Cell(57, i + 42).Value;
                            ADC_Cor_sheet.Cell(51, 4).Value = TE_sheet.Cell(57, i + 43).Value;
                            ADC_Cor_sheet.Cell(52, 4).Value = TE_sheet.Cell(57, i + 44).Value;
                            //CH16
                            ADC_Cor_sheet.Cell(53, 4).Value = TE_sheet.Cell(57, i + 45).Value;
                            ADC_Cor_sheet.Cell(54, 4).Value = TE_sheet.Cell(57, i + 46).Value;
                            ADC_Cor_sheet.Cell(55, 4).Value = TE_sheet.Cell(57, i + 47).Value;
                            //CH17
                            ADC_Cor_sheet.Cell(56, 4).Value = TE_sheet.Cell(57, i + 48).Value;
                            ADC_Cor_sheet.Cell(57, 4).Value = TE_sheet.Cell(57, i + 49).Value;
                            ADC_Cor_sheet.Cell(58, 4).Value = TE_sheet.Cell(57, i + 50).Value;
                            //CH18
                            ADC_Cor_sheet.Cell(59, 4).Value = TE_sheet.Cell(57, i + 51).Value;
                            ADC_Cor_sheet.Cell(60, 4).Value = TE_sheet.Cell(57, i + 52).Value;
                            ADC_Cor_sheet.Cell(61, 4).Value = TE_sheet.Cell(57, i + 53).Value;
                        }

                    }

                }

            }


            ADC_Cor.Save();

            //     IXLWorksheet TE_Result = TE.Worksheet(1);

            //     TE_Result.Cell(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text)).Value = textBox3.Text;


        }





        private void button2_Click(object sender, EventArgs e)//访问指定位置的excel，在指定位置写入指定内容
        {

            /*g_wb = new XLWorkbook(@"C:\Users\Administrator\Desktop\ADC Correlation Result\ADC Cor.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
            IXLWorksheet sheet = g_wb.Worksheet(1);
            sheet.Cell(Convert.ToInt32(textBox1.Text), Convert.ToInt32(textBox2.Text)).Value = textBox3.Text;
            g_wb.Save();*/
        }

        private void button3_Click(object sender, EventArgs e)//直接删除整个sheet
        {

        }

        private void button4_Click(object sender, EventArgs e) //设置cell的背景色和字体的颜色
        {

 
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text == "1#")
                ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_1#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
            if (comboBox3.Text == "2#")
                ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_2#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
            if (comboBox3.Text == "3#")
                ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_3#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
            if (comboBox3.Text == "4#")
                ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_4#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
            if (comboBox3.Text == "5#")
                ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_5#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
            IXLWorksheet ADC_Cor_sheet = ADC_Cor.Worksheet(1);

            if (comboBox1.Text == "SY68920")
                textBox9.Text = Convert.ToString(    1000*Convert.ToSingle(textBox8.Text)- 1000*Convert.ToSingle(ADC_Cor_sheet.Cell(5, 4).Value)  );
            if (comboBox1.Text == "SY68930")
            {
                textBox9.Text = Convert.ToString(1000 * Convert.ToSingle(textBox8.Text) - 1000 * Convert.ToSingle(ADC_Cor_sheet.Cell(5, 4).Value));
                textBox14.Text = Convert.ToString(1000 * Convert.ToSingle(textBox15.Text) - 1000 * Convert.ToSingle(ADC_Cor_sheet.Cell(6, 4).Value));
            }
            if (comboBox1.Text == "SY68940")
            {
                textBox9.Text = Convert.ToString(1000 * Convert.ToSingle(textBox8.Text) - 1000 * Convert.ToSingle(ADC_Cor_sheet.Cell(5, 4).Value));
                textBox14.Text = Convert.ToString(1000 * Convert.ToSingle(textBox15.Text) - 1000 * Convert.ToSingle(ADC_Cor_sheet.Cell(6, 4).Value));
                textBox18.Text = Convert.ToString(1000 * Convert.ToSingle(textBox19.Text) - 1000 * Convert.ToSingle(ADC_Cor_sheet.Cell(7, 4).Value));
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text == "1#")
                ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_1#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
            if (comboBox3.Text == "2#")
                ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_2#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
            if (comboBox3.Text == "3#")
                ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_3#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
            if (comboBox3.Text == "4#")
                ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_4#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
            if (comboBox3.Text == "5#")
                ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_5#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
            IXLWorksheet ADC_Cor_sheet = ADC_Cor.Worksheet(1);

            if (comboBox1.Text == "SY68920")
                textBox16.Text = Convert.ToString(1000 * Convert.ToSingle(textBox17.Text) - 1000 * Convert.ToSingle(ADC_Cor_sheet.Cell(5, 4).Value));
            if (comboBox1.Text == "SY68930")
            {
                textBox16.Text = Convert.ToString(1000 * Convert.ToSingle(textBox17.Text) - 1000 * Convert.ToSingle(ADC_Cor_sheet.Cell(5, 4).Value));
                textBox12.Text = Convert.ToString(1000 * Convert.ToSingle(textBox13.Text) - 1000 * Convert.ToSingle(ADC_Cor_sheet.Cell(6, 4).Value));
            }
            if (comboBox1.Text == "SY68940")
            {
                textBox16.Text = Convert.ToString(1000 * Convert.ToSingle(textBox17.Text) - 1000 * Convert.ToSingle(ADC_Cor_sheet.Cell(5, 4).Value));
                textBox12.Text = Convert.ToString(1000 * Convert.ToSingle(textBox13.Text) - 1000 * Convert.ToSingle(ADC_Cor_sheet.Cell(6, 4).Value));
                textBox10.Text = Convert.ToString(1000 * Convert.ToSingle(textBox11.Text) - 1000 * Convert.ToSingle(ADC_Cor_sheet.Cell(7, 4).Value));
            }
        }

        public int x = 15;
        private void button6_Click(object sender, EventArgs e)//导入AE测试数据并计算error及offset
        {
            if (comboBox3.Text == "1#")
                ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_1#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
            if (comboBox3.Text == "2#")
                ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_2#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
            if (comboBox3.Text == "3#")
                ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_3#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
            if (comboBox3.Text == "4#")
                ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_4#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
            if (comboBox3.Text == "5#")
                ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_5#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
            IXLWorksheet ADC_Cor_sheet = ADC_Cor.Worksheet(1);

            test = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\test.xlsx");
            IXLWorksheet test_sheet = test.Worksheet(1);


           //代码值转换及error计算，同时需要清除单元格内的公式，只保留数据
            Workbook workbook = new Workbook();
            workbook.LoadFromFile("test.xlsx");
            Worksheet H2D = workbook.Worksheets[0];
 

            StringBuilder currentFormula = new StringBuilder("=HEX2DEC(B1)*6.4/65535");//进制转换
            StringBuilder currentFormula1 = new StringBuilder("=ROUND(D1,5)");//用来四舍五入
            StringBuilder currentFormula2 = new StringBuilder("=1000*(D1-C1)");//用来求ADC error 代码值-输入值
            string num;
            string num1;
            
              for (i = 1; i <= 6000; i++)
              {
                  currentFormula.Remove(10, currentFormula.Length - 10);
                  num = Convert.ToString(i);
                  currentFormula.Append(num);
                  currentFormula.Append(")*6.4/65535");
                  H2D.Range[i, 4].Formula = Convert.ToString(currentFormula);


                  currentFormula2.Remove(8, currentFormula2.Length - 8);
                  num = Convert.ToString(i);
                  currentFormula2.Append(num);
                  currentFormula2.Append("-C");
                  currentFormula2.Append(num);
                  currentFormula2.Append(")");
                  H2D.Range[i, 5].Formula = Convert.ToString(currentFormula2);



            }

         

            //检索第一列的文本，然后去求error的平均值，写入相应的位置。
            for (i = 1; i <= 6000; i++)
            {
                if(H2D.Range[i, 1].Text=="CH1_2V")
                {
                    StringBuilder currentFormula3= new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i+5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[3, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH1_3.3V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[4, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH1_5V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[5, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH2_2V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[6, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH2_3.3V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[7, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH2_5V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[8, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH3_2V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[9, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH3_3.3V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[10, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH3_5V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[11, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH4_2V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[12, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH4_3.3V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[13, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH4_5V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[14, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH5_2V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[15, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH5_3.3V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[16, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH5_5V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[17, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH6_2V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[18, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH6_3.3V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[19, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH6_5V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[20, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH7_2V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[21, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH7_3.3V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[22, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH7_5V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[23, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH8_2V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[24, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH8_3.3V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[25, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH8_5V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[26, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH9_2V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[27, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH9_3.3V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[28, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH9_5V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[29, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH10_2V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[30, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH10_3.3V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[31, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH10_5V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[32, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH11_2V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[33, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH11_3.3V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[34, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH11_5V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[35, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH12_2V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[36, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH12_3.3V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[37, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH12_5V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[38, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH13_2V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[39, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH13_3.3V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[40, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH13_5V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[41, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH14_2V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[42, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH14_3.3V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[43, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH14_5V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[44, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH15_2V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[45, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH15_3.3V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[46, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH15_5V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[47, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH16_2V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[48, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH16_3.3V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[49, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH16_5V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[50, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH17_2V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[51, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH17_3.3V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[52, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH17_5V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[53, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH18_2V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[54, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH18_3.3V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[55, 11].Formula = Convert.ToString(currentFormula3);

                }
                if (H2D.Range[i, 1].Text == "CH18_5V")
                {
                    StringBuilder currentFormula3 = new StringBuilder("=AVERAGEA(E12:E62)");//用来求ADC error平均值
                    currentFormula3.Remove(11, currentFormula3.Length - 11);
                    num = Convert.ToString(i + 5);
                    currentFormula3.Append(num);
                    currentFormula3.Append(":E");
                    num1 = Convert.ToString(i + x);
                    currentFormula3.Append(num1);
                    currentFormula3.Append(")");
                    H2D.Range[56, 11].Formula = Convert.ToString(currentFormula3);

                }
            }
            workbook.SaveToFile("test.xlsx", ExcelVersion.Version2013);




            //在结果表sheet2中计算平均值和添加文本，并把平均值填入sheet1



            if (comboBox1.Text == "SY68940")
            {
                //Vbg
                ADC_Cor_sheet.Cell(5, 5).Value = (Convert.ToDouble(ADC_Cor_sheet.Cell(5, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(5, 4).Value)) * 1000;
                ADC_Cor_sheet.Cell(6, 5).Value = (Convert.ToDouble(ADC_Cor_sheet.Cell(6, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(6, 4).Value)) * 1000;
                ADC_Cor_sheet.Cell(7, 5).Value = (Convert.ToDouble(ADC_Cor_sheet.Cell(7, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(7, 4).Value)) * 1000;

                //CH1 
                ADC_Cor_sheet.Cell(8, 3).Value = Convert.ToDouble(H2D.Range[3, 11].FormulaValue);
                ADC_Cor_sheet.Cell(8, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(8, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(8, 4).Value);//error
                ADC_Cor_sheet.Cell(8, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(8, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 2;//offset@2V 

                ADC_Cor_sheet.Cell(9, 3).Value = Convert.ToDouble(H2D.Range[4, 11].FormulaValue);
                ADC_Cor_sheet.Cell(9, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(9, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(9, 4).Value);
                ADC_Cor_sheet.Cell(9, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(9, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 3.3;//offset@3.3V 

                ADC_Cor_sheet.Cell(10, 3).Value = Convert.ToDouble(H2D.Range[5, 11].FormulaValue);
                ADC_Cor_sheet.Cell(10, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(10, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(10, 4).Value);
                ADC_Cor_sheet.Cell(10, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(10, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 5;//offset@3.3V 

                //CH2 
                ADC_Cor_sheet.Cell(11, 3).Value = Convert.ToDouble(H2D.Range[6, 11].FormulaValue);
                ADC_Cor_sheet.Cell(11, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(11, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(11, 4).Value);//error
                ADC_Cor_sheet.Cell(11, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(11, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 2;//offset@2V 

                ADC_Cor_sheet.Cell(12, 3).Value = Convert.ToDouble(H2D.Range[7, 11].FormulaValue);
                ADC_Cor_sheet.Cell(12, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(12, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(12, 4).Value);
                ADC_Cor_sheet.Cell(12, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(12, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(13, 3).Value = Convert.ToDouble(H2D.Range[8, 11].FormulaValue);
                ADC_Cor_sheet.Cell(13, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(13, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(13, 4).Value);
                ADC_Cor_sheet.Cell(13, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(13, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 5;//offset@3.3V

                //CH3 
                ADC_Cor_sheet.Cell(14, 3).Value = Convert.ToDouble(H2D.Range[9, 11].FormulaValue);
                ADC_Cor_sheet.Cell(14, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(14, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(14, 4).Value);
                ADC_Cor_sheet.Cell(14, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(14, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 2;//offset@2V

                ADC_Cor_sheet.Cell(15, 3).Value = Convert.ToDouble(H2D.Range[10, 11].FormulaValue);
                ADC_Cor_sheet.Cell(15, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(15, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(15, 4).Value);
                ADC_Cor_sheet.Cell(15, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(15, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(16, 3).Value = Convert.ToDouble(H2D.Range[11, 11].FormulaValue);
                ADC_Cor_sheet.Cell(16, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(16, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(16, 4).Value);
                ADC_Cor_sheet.Cell(16, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(16, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 5;//offset@5V

                //CH4 
                ADC_Cor_sheet.Cell(17, 3).Value = Convert.ToDouble(H2D.Range[12, 11].FormulaValue);
                ADC_Cor_sheet.Cell(17, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(17, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(17, 4).Value);
                ADC_Cor_sheet.Cell(17, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(17, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 2;//offset@2V

                ADC_Cor_sheet.Cell(18, 3).Value = Convert.ToDouble(H2D.Range[13, 11].FormulaValue);
                ADC_Cor_sheet.Cell(18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(18, 4).Value);
                ADC_Cor_sheet.Cell(18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(19, 3).Value = Convert.ToDouble(H2D.Range[14, 11].FormulaValue);
                ADC_Cor_sheet.Cell(19, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(19, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(19, 4).Value);
                ADC_Cor_sheet.Cell(19, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(19, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 5;//offset@5V

                //CH5 
                ADC_Cor_sheet.Cell(20, 3).Value = Convert.ToDouble(H2D.Range[15, 11].FormulaValue);
                ADC_Cor_sheet.Cell(20, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(20, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(20, 4).Value);
                ADC_Cor_sheet.Cell(20, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(20, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 2;//offset@2V

                ADC_Cor_sheet.Cell(21, 3).Value = Convert.ToDouble(H2D.Range[16, 11].FormulaValue);
                ADC_Cor_sheet.Cell(21, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(21, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(21, 4).Value);
                ADC_Cor_sheet.Cell(21, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(21, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(22, 3).Value = Convert.ToDouble(H2D.Range[17, 11].FormulaValue);
                ADC_Cor_sheet.Cell(22, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(22, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(22, 4).Value);
                ADC_Cor_sheet.Cell(22, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(22, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 5;//offset@5V

                //CH6 
                ADC_Cor_sheet.Cell(23, 3).Value = Convert.ToDouble(H2D.Range[18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(23, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(23, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(23, 4).Value);
                ADC_Cor_sheet.Cell(23, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(23, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 2;//offset@2V

                ADC_Cor_sheet.Cell(24, 3).Value = Convert.ToDouble(H2D.Range[19, 11].FormulaValue);
                ADC_Cor_sheet.Cell(24, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(24, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(24, 4).Value);
                ADC_Cor_sheet.Cell(24, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(24, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(25, 3).Value = Convert.ToDouble(H2D.Range[20, 11].FormulaValue);
                ADC_Cor_sheet.Cell(25, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(25, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(25, 4).Value);
                ADC_Cor_sheet.Cell(25, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(25, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 5;//offset@5V


                //CH7
                ADC_Cor_sheet.Cell(8 + 18, 3).Value = Convert.ToDouble(H2D.Range[3 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(8 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(8 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(8 + 18, 4).Value);//error
                ADC_Cor_sheet.Cell(8 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(8 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 2;//offset@2V 

                ADC_Cor_sheet.Cell(9 + 18, 3).Value = Convert.ToDouble(H2D.Range[4 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(9 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(9 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(9 + 18, 4).Value);
                ADC_Cor_sheet.Cell(9 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(9 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 3.3;//offset@3.3V 

                ADC_Cor_sheet.Cell(10 + 18, 3).Value = Convert.ToDouble(H2D.Range[5 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(10 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(10 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(10 + 18, 4).Value);
                ADC_Cor_sheet.Cell(10 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(10 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 5;//offset@3.3V 

                //CH8 
                ADC_Cor_sheet.Cell(11 + 18, 3).Value = Convert.ToDouble(H2D.Range[6 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(11 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(11 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(11 + 18, 4).Value);//error
                ADC_Cor_sheet.Cell(11 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(11 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 2;//offset@2V 

                ADC_Cor_sheet.Cell(12 + 18, 3).Value = Convert.ToDouble(H2D.Range[7 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(12 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(12 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(12 + 18, 4).Value);
                ADC_Cor_sheet.Cell(12 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(12 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(13 + 18, 3).Value = Convert.ToDouble(H2D.Range[8 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(13 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(13 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(13 + 18, 4).Value);
                ADC_Cor_sheet.Cell(13 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(13 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 5;//offset@3.3V

                //CH9
                ADC_Cor_sheet.Cell(14 + 18, 3).Value = Convert.ToDouble(H2D.Range[9 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(14 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(14 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(14 + 18, 4).Value);
                ADC_Cor_sheet.Cell(14 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(14 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 2;//offset@2V

                ADC_Cor_sheet.Cell(15 + 18, 3).Value = Convert.ToDouble(H2D.Range[10 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(15 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(15 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(15 + 18, 4).Value);
                ADC_Cor_sheet.Cell(15 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(15 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(16 + 18, 3).Value = Convert.ToDouble(H2D.Range[11 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(16 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(16 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(16 + 18, 4).Value);
                ADC_Cor_sheet.Cell(16 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(16 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 5;//offset@5V

                //CH10
                ADC_Cor_sheet.Cell(17 + 18, 3).Value = Convert.ToDouble(H2D.Range[12 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(17 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(17 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(17 + 18, 4).Value);
                ADC_Cor_sheet.Cell(17 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(17 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 2;//offset@2V

                ADC_Cor_sheet.Cell(18 + 18, 3).Value = Convert.ToDouble(H2D.Range[13 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(18 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(18 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(18 + 18, 4).Value);
                ADC_Cor_sheet.Cell(18 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(18 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(19 + 18, 3).Value = Convert.ToDouble(H2D.Range[14 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(19 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(19 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(19 + 18, 4).Value);
                ADC_Cor_sheet.Cell(19 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(19 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 5;//offset@5V

                //CH11 
                ADC_Cor_sheet.Cell(20 + 18, 3).Value = Convert.ToDouble(H2D.Range[15 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(20 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(20 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(20 + 18, 4).Value);
                ADC_Cor_sheet.Cell(20 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(20 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 2;//offset@2V

                ADC_Cor_sheet.Cell(21 + 18, 3).Value = Convert.ToDouble(H2D.Range[16 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(21 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(21 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(21 + 18, 4).Value);
                ADC_Cor_sheet.Cell(21 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(21 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(22 + 18, 3).Value = Convert.ToDouble(H2D.Range[17 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(22 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(22 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(22 + 18, 4).Value);
                ADC_Cor_sheet.Cell(22 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(22 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 5;//offset@5V

                //CH12
                ADC_Cor_sheet.Cell(23 + 18, 3).Value = Convert.ToDouble(H2D.Range[18 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(23 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(23 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(23 + 18, 4).Value);
                ADC_Cor_sheet.Cell(23 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(23 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 2;//offset@2V

                ADC_Cor_sheet.Cell(24 + 18, 3).Value = Convert.ToDouble(H2D.Range[19 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(24 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(24 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(24 + 18, 4).Value);
                ADC_Cor_sheet.Cell(24 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(24 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(25 + 18, 3).Value = Convert.ToDouble(H2D.Range[20 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(25 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(25 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(25 + 18, 4).Value);
                ADC_Cor_sheet.Cell(25 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(25 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 5;//offset@5V


                //CH13
                ADC_Cor_sheet.Cell(8 + 36, 3).Value = Convert.ToDouble(H2D.Range[3 + 36, 11].FormulaValue);
                ADC_Cor_sheet.Cell(8 + 36, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(8 + 36, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(8 + 36, 4).Value);//error
                ADC_Cor_sheet.Cell(8 + 36, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(8 + 36, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 2, 5).Value) / 1.225 * 2;//offset@2V 

                ADC_Cor_sheet.Cell(9 + 36, 3).Value = Convert.ToDouble(H2D.Range[4 + 36, 11].FormulaValue);
                ADC_Cor_sheet.Cell(9 + 36, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(9 + 36, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(9 + 36, 4).Value);
                ADC_Cor_sheet.Cell(9 + 36, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(9 + 36, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 2, 5).Value) / 1.225 * 3.3;//offset@3.3V 

                ADC_Cor_sheet.Cell(10 + 36, 3).Value = Convert.ToDouble(H2D.Range[5 + 36, 11].FormulaValue);
                ADC_Cor_sheet.Cell(10 + 36, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(10 + 36, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(10 + 36, 4).Value);
                ADC_Cor_sheet.Cell(10 + 36, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(10 + 36, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 2, 5).Value) / 1.225 * 5;//offset@3.3V 

                //CH14 
                ADC_Cor_sheet.Cell(11 + 36, 3).Value = Convert.ToDouble(H2D.Range[6 + 36, 11].FormulaValue);
                ADC_Cor_sheet.Cell(11 + 36, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(11 + 36, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(11 + 36, 4).Value);//error
                ADC_Cor_sheet.Cell(11 + 36, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(11 + 36, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 2, 5).Value) / 1.225 * 2;//offset@2V 

                ADC_Cor_sheet.Cell(12 + 36, 3).Value = Convert.ToDouble(H2D.Range[7 + 36, 11].FormulaValue);
                ADC_Cor_sheet.Cell(12 + 36, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(12 + 36, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(12 + 36, 4).Value);
                ADC_Cor_sheet.Cell(12 + 36, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(12 + 36, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 2, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(13 + 36, 3).Value = Convert.ToDouble(H2D.Range[8 + 36, 11].FormulaValue);
                ADC_Cor_sheet.Cell(13 + 36, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(13 + 36, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(13 + 36, 4).Value);
                ADC_Cor_sheet.Cell(13 + 36, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(13 + 36, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 2, 5).Value) / 1.225 * 5;//offset@3.3V

                //CH15
                ADC_Cor_sheet.Cell(14 + 36, 3).Value = Convert.ToDouble(H2D.Range[9 + 36, 11].FormulaValue);
                ADC_Cor_sheet.Cell(14 + 36, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(14 + 36, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(14 + 36, 4).Value);
                ADC_Cor_sheet.Cell(14 + 36, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(14 + 36, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 2, 5).Value) / 1.225 * 2;//offset@2V

                ADC_Cor_sheet.Cell(15 + 36, 3).Value = Convert.ToDouble(H2D.Range[10 + 36, 11].FormulaValue);
                ADC_Cor_sheet.Cell(15 + 36, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(15 + 36, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(15 + 36, 4).Value);
                ADC_Cor_sheet.Cell(15 + 36, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(15 + 36, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 2, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(16 + 36, 3).Value = Convert.ToDouble(H2D.Range[11 + 36, 11].FormulaValue);
                ADC_Cor_sheet.Cell(16 + 36, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(16 + 36, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(16 + 36, 4).Value);
                ADC_Cor_sheet.Cell(16 + 36, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(16 + 36, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 2, 5).Value) / 1.225 * 5;//offset@5V

                //CH16
                ADC_Cor_sheet.Cell(17 + 36, 3).Value = Convert.ToDouble(H2D.Range[12 + 36, 11].FormulaValue);
                ADC_Cor_sheet.Cell(17 + 36, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(17 + 36, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(17 + 36, 4).Value);
                ADC_Cor_sheet.Cell(17 + 36, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(17 + 36, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 2, 5).Value) / 1.225 * 2;//offset@2V

                ADC_Cor_sheet.Cell(18 + 36, 3).Value = Convert.ToDouble(H2D.Range[13 + 36, 11].FormulaValue);
                ADC_Cor_sheet.Cell(18 + 36, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(18 + 36, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(18 + 36, 4).Value);
                ADC_Cor_sheet.Cell(18 + 36, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(18 + 36, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 2, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(19 + 36, 3).Value = Convert.ToDouble(H2D.Range[14 + 36, 11].FormulaValue);
                ADC_Cor_sheet.Cell(19 + 36, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(19 + 36, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(19 + 36, 4).Value);
                ADC_Cor_sheet.Cell(19 + 36, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(19 + 36, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 2, 5).Value) / 1.225 * 5;//offset@5V

                //CH17
                ADC_Cor_sheet.Cell(20 + 36, 3).Value = Convert.ToDouble(H2D.Range[15 + 36, 11].FormulaValue);
                ADC_Cor_sheet.Cell(20 + 36, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(20 + 36, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(20 + 36, 4).Value);
                ADC_Cor_sheet.Cell(20 + 36, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(20 + 36, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 2, 5).Value) / 1.225 * 2;//offset@2V

                ADC_Cor_sheet.Cell(21 + 36, 3).Value = Convert.ToDouble(H2D.Range[16 + 36, 11].FormulaValue);
                ADC_Cor_sheet.Cell(21 + 36, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(21 + 36, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(21 + 36, 4).Value);
                ADC_Cor_sheet.Cell(21 + 36, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(21 + 36, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 2, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(22 + 36, 3).Value = Convert.ToDouble(H2D.Range[17 + 36, 11].FormulaValue);
                ADC_Cor_sheet.Cell(22 + 36, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(22 + 36, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(22 + 36, 4).Value);
                ADC_Cor_sheet.Cell(22 + 36, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(22 + 36, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 2, 5).Value) / 1.225 * 5;//offset@5V

                //CH18
                ADC_Cor_sheet.Cell(23 + 36, 3).Value = Convert.ToDouble(H2D.Range[18 + 36, 11].FormulaValue);
                ADC_Cor_sheet.Cell(23 + 36, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(23 + 36, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(23 + 36, 4).Value);
                ADC_Cor_sheet.Cell(23 + 36, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(23 + 36, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 2, 5).Value) / 1.225 * 2;//offset@2V

                ADC_Cor_sheet.Cell(24 + 36, 3).Value = Convert.ToDouble(H2D.Range[19 + 36, 11].FormulaValue);
                ADC_Cor_sheet.Cell(24 + 36, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(24 + 36, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(24 + 36, 4).Value);
                ADC_Cor_sheet.Cell(24 + 36, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(24 + 36, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 2, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(25 + 36, 3).Value = Convert.ToDouble(H2D.Range[20 + 36, 11].FormulaValue);
                ADC_Cor_sheet.Cell(25 + 36, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(25 + 36, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(25 + 36, 4).Value);
                ADC_Cor_sheet.Cell(25 + 36, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(25 + 36, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 2, 5).Value) / 1.225 * 5;//offset@5V

            }

            if (comboBox1.Text == "SY68930")
            {
                //Vbg
                ADC_Cor_sheet.Cell(5, 5).Value = (Convert.ToDouble(ADC_Cor_sheet.Cell(5, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(5, 4).Value)) * 1000;
                ADC_Cor_sheet.Cell(6, 5).Value = (Convert.ToDouble(ADC_Cor_sheet.Cell(6, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(6, 4).Value)) * 1000;

                //CH1 
                ADC_Cor_sheet.Cell(8, 3).Value = Convert.ToDouble(H2D.Range[3, 11].FormulaValue);
                ADC_Cor_sheet.Cell(8, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(8, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(8, 4).Value);//error
                ADC_Cor_sheet.Cell(8, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(8, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 2;//offset@2V 

                ADC_Cor_sheet.Cell(9, 3).Value = Convert.ToDouble(H2D.Range[4, 11].FormulaValue);
                ADC_Cor_sheet.Cell(9, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(9, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(9, 4).Value);
                ADC_Cor_sheet.Cell(9, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(9, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 3.3;//offset@3.3V 

                ADC_Cor_sheet.Cell(10, 3).Value = Convert.ToDouble(H2D.Range[5, 11].FormulaValue);
                ADC_Cor_sheet.Cell(10, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(10, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(10, 4).Value);
                ADC_Cor_sheet.Cell(10, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(10, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 5;//offset@3.3V 

                //CH2 
                ADC_Cor_sheet.Cell(11, 3).Value = Convert.ToDouble(H2D.Range[6, 11].FormulaValue);
                ADC_Cor_sheet.Cell(11, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(11, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(11, 4).Value);//error
                ADC_Cor_sheet.Cell(11, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(11, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 2;//offset@2V 

                ADC_Cor_sheet.Cell(12, 3).Value = Convert.ToDouble(H2D.Range[7, 11].FormulaValue);
                ADC_Cor_sheet.Cell(12, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(12, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(12, 4).Value);
                ADC_Cor_sheet.Cell(12, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(12, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(13, 3).Value = Convert.ToDouble(H2D.Range[8, 11].FormulaValue);
                ADC_Cor_sheet.Cell(13, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(13, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(13, 4).Value);
                ADC_Cor_sheet.Cell(13, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(13, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 5;//offset@3.3V

                //CH3 
                ADC_Cor_sheet.Cell(14, 3).Value = Convert.ToDouble(H2D.Range[9, 11].FormulaValue);
                ADC_Cor_sheet.Cell(14, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(14, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(14, 4).Value);
                ADC_Cor_sheet.Cell(14, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(14, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 2;//offset@2V

                ADC_Cor_sheet.Cell(15, 3).Value = Convert.ToDouble(H2D.Range[10, 11].FormulaValue);
                ADC_Cor_sheet.Cell(15, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(15, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(15, 4).Value);
                ADC_Cor_sheet.Cell(15, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(15, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(16, 3).Value = Convert.ToDouble(H2D.Range[11, 11].FormulaValue);
                ADC_Cor_sheet.Cell(16, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(16, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(16, 4).Value);
                ADC_Cor_sheet.Cell(16, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(16, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 5;//offset@5V

                //CH4 
                ADC_Cor_sheet.Cell(17, 3).Value = Convert.ToDouble(H2D.Range[12, 11].FormulaValue);
                ADC_Cor_sheet.Cell(17, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(17, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(17, 4).Value);
                ADC_Cor_sheet.Cell(17, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(17, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 2;//offset@2V

                ADC_Cor_sheet.Cell(18, 3).Value = Convert.ToDouble(H2D.Range[13, 11].FormulaValue);
                ADC_Cor_sheet.Cell(18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(18, 4).Value);
                ADC_Cor_sheet.Cell(18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(19, 3).Value = Convert.ToDouble(H2D.Range[14, 11].FormulaValue);
                ADC_Cor_sheet.Cell(19, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(19, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(19, 4).Value);
                ADC_Cor_sheet.Cell(19, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(19, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 5;//offset@5V

                //CH5 
                ADC_Cor_sheet.Cell(20, 3).Value = Convert.ToDouble(H2D.Range[15, 11].FormulaValue);
                ADC_Cor_sheet.Cell(20, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(20, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(20, 4).Value);
                ADC_Cor_sheet.Cell(20, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(20, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 2;//offset@2V

                ADC_Cor_sheet.Cell(21, 3).Value = Convert.ToDouble(H2D.Range[16, 11].FormulaValue);
                ADC_Cor_sheet.Cell(21, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(21, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(21, 4).Value);
                ADC_Cor_sheet.Cell(21, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(21, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(22, 3).Value = Convert.ToDouble(H2D.Range[17, 11].FormulaValue);
                ADC_Cor_sheet.Cell(22, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(22, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(22, 4).Value);
                ADC_Cor_sheet.Cell(22, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(22, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 5;//offset@5V

                //CH6 
                ADC_Cor_sheet.Cell(23, 3).Value = Convert.ToDouble(H2D.Range[18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(23, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(23, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(23, 4).Value);
                ADC_Cor_sheet.Cell(23, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(23, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 2;//offset@2V

                ADC_Cor_sheet.Cell(24, 3).Value = Convert.ToDouble(H2D.Range[19, 11].FormulaValue);
                ADC_Cor_sheet.Cell(24, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(24, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(24, 4).Value);
                ADC_Cor_sheet.Cell(24, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(24, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(25, 3).Value = Convert.ToDouble(H2D.Range[20, 11].FormulaValue);
                ADC_Cor_sheet.Cell(25, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(25, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(25, 4).Value);
                ADC_Cor_sheet.Cell(25, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(25, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 5;//offset@5V

                //CH7
                ADC_Cor_sheet.Cell(8 + 18, 3).Value = Convert.ToDouble(H2D.Range[3 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(8 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(8 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(8 + 18, 4).Value);//error
                ADC_Cor_sheet.Cell(8 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(8 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 2;//offset@2V 

                ADC_Cor_sheet.Cell(9 + 18, 3).Value = Convert.ToDouble(H2D.Range[4 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(9 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(9 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(9 + 18, 4).Value);
                ADC_Cor_sheet.Cell(9 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(9 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 3.3;//offset@3.3V 

                ADC_Cor_sheet.Cell(10 + 18, 3).Value = Convert.ToDouble(H2D.Range[5 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(10 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(10 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(10 + 18, 4).Value);
                ADC_Cor_sheet.Cell(10 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(10 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 5;//offset@3.3V 

                //CH8 
                ADC_Cor_sheet.Cell(11 + 18, 3).Value = Convert.ToDouble(H2D.Range[6 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(11 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(11 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(11 + 18, 4).Value);//error
                ADC_Cor_sheet.Cell(11 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(11 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 2;//offset@2V 

                ADC_Cor_sheet.Cell(12 + 18, 3).Value = Convert.ToDouble(H2D.Range[7 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(12 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(12 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(12 + 18, 4).Value);
                ADC_Cor_sheet.Cell(12 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(12 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(13 + 18, 3).Value = Convert.ToDouble(H2D.Range[8 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(13 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(13 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(13 + 18, 4).Value);
                ADC_Cor_sheet.Cell(13 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(13 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 5;//offset@3.3V

                //CH9
                ADC_Cor_sheet.Cell(14 + 18, 3).Value = Convert.ToDouble(H2D.Range[9 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(14 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(14 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(14 + 18, 4).Value);
                ADC_Cor_sheet.Cell(14 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(14 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 2;//offset@2V

                ADC_Cor_sheet.Cell(15 + 18, 3).Value = Convert.ToDouble(H2D.Range[10 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(15 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(15 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(15 + 18, 4).Value);
                ADC_Cor_sheet.Cell(15 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(15 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(16 + 18, 3).Value = Convert.ToDouble(H2D.Range[11 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(16 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(16 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(16 + 18, 4).Value);
                ADC_Cor_sheet.Cell(16 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(16 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 5;//offset@5V

                //CH10
                ADC_Cor_sheet.Cell(17 + 18, 3).Value = Convert.ToDouble(H2D.Range[12 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(17 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(17 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(17 + 18, 4).Value);
                ADC_Cor_sheet.Cell(17 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(17 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 2;//offset@2V

                ADC_Cor_sheet.Cell(18 + 18, 3).Value = Convert.ToDouble(H2D.Range[13 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(18 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(18 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(18 + 18, 4).Value);
                ADC_Cor_sheet.Cell(18 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(18 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(19 + 18, 3).Value = Convert.ToDouble(H2D.Range[14 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(19 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(19 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(19 + 18, 4).Value);
                ADC_Cor_sheet.Cell(19 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(19 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 5;//offset@5V

                //CH11 
                ADC_Cor_sheet.Cell(20 + 18, 3).Value = Convert.ToDouble(H2D.Range[15 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(20 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(20 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(20 + 18, 4).Value);
                ADC_Cor_sheet.Cell(20 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(20 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 2;//offset@2V

                ADC_Cor_sheet.Cell(21 + 18, 3).Value = Convert.ToDouble(H2D.Range[16 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(21 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(21 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(21 + 18, 4).Value);
                ADC_Cor_sheet.Cell(21 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(21 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(22 + 18, 3).Value = Convert.ToDouble(H2D.Range[17 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(22 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(22 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(22 + 18, 4).Value);
                ADC_Cor_sheet.Cell(22 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(22 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 5;//offset@5V

                //CH12
                ADC_Cor_sheet.Cell(23 + 18, 3).Value = Convert.ToDouble(H2D.Range[18 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(23 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(23 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(23 + 18, 4).Value);
                ADC_Cor_sheet.Cell(23 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(23 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 2;//offset@2V

                ADC_Cor_sheet.Cell(24 + 18, 3).Value = Convert.ToDouble(H2D.Range[19 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(24 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(24 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(24 + 18, 4).Value);
                ADC_Cor_sheet.Cell(24 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(24 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(25 + 18, 3).Value = Convert.ToDouble(H2D.Range[20 + 18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(25 + 18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(25 + 18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(25 + 18, 4).Value);
                ADC_Cor_sheet.Cell(25 + 18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(25 + 18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5 + 1, 5).Value) / 1.225 * 5;//offset@5V


            }
            if (comboBox1.Text == "SY68920")
            {
                //Vbg
                ADC_Cor_sheet.Cell(5, 5).Value = (Convert.ToDouble(ADC_Cor_sheet.Cell(5, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(5, 4).Value)) * 1000;
                //CH1 
                ADC_Cor_sheet.Cell(8, 3).Value = Convert.ToDouble(H2D.Range[3, 11].FormulaValue);
                ADC_Cor_sheet.Cell(8, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(8, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(8, 4).Value);//error
                ADC_Cor_sheet.Cell(8, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(8, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 2;//offset@2V 

                ADC_Cor_sheet.Cell(9, 3).Value = Convert.ToDouble(H2D.Range[4, 11].FormulaValue);
                ADC_Cor_sheet.Cell(9, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(9, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(9, 4).Value);
                ADC_Cor_sheet.Cell(9, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(9, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 3.3;//offset@3.3V 

                ADC_Cor_sheet.Cell(10, 3).Value = Convert.ToDouble(H2D.Range[5, 11].FormulaValue);
                ADC_Cor_sheet.Cell(10, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(10, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(10, 4).Value);
                ADC_Cor_sheet.Cell(10, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(10, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 5;//offset@3.3V 

                //CH2 
                ADC_Cor_sheet.Cell(11, 3).Value = Convert.ToDouble(H2D.Range[6, 11].FormulaValue);
                ADC_Cor_sheet.Cell(11, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(11, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(11, 4).Value);//error
                ADC_Cor_sheet.Cell(11, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(11, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 2;//offset@2V 

                ADC_Cor_sheet.Cell(12, 3).Value = Convert.ToDouble(H2D.Range[7, 11].FormulaValue);
                ADC_Cor_sheet.Cell(12, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(12, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(12, 4).Value);
                ADC_Cor_sheet.Cell(12, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(12, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(13, 3).Value = Convert.ToDouble(H2D.Range[8, 11].FormulaValue);
                ADC_Cor_sheet.Cell(13, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(13, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(13, 4).Value);
                ADC_Cor_sheet.Cell(13, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(13, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 5;//offset@3.3V

                //CH3 
                ADC_Cor_sheet.Cell(14, 3).Value = Convert.ToDouble(H2D.Range[9, 11].FormulaValue);
                ADC_Cor_sheet.Cell(14, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(14, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(14, 4).Value);
                ADC_Cor_sheet.Cell(14, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(14, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 2;//offset@2V

                ADC_Cor_sheet.Cell(15, 3).Value = Convert.ToDouble(H2D.Range[10, 11].FormulaValue);
                ADC_Cor_sheet.Cell(15, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(15, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(15, 4).Value);
                ADC_Cor_sheet.Cell(15, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(15, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(16, 3).Value = Convert.ToDouble(H2D.Range[11, 11].FormulaValue);
                ADC_Cor_sheet.Cell(16, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(16, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(16, 4).Value);
                ADC_Cor_sheet.Cell(16, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(16, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 5;//offset@5V

                //CH4 
                ADC_Cor_sheet.Cell(17, 3).Value = Convert.ToDouble(H2D.Range[12, 11].FormulaValue);
                ADC_Cor_sheet.Cell(17, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(17, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(17, 4).Value);
                ADC_Cor_sheet.Cell(17, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(17, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 2;//offset@2V

                ADC_Cor_sheet.Cell(18, 3).Value = Convert.ToDouble(H2D.Range[13, 11].FormulaValue);
                ADC_Cor_sheet.Cell(18, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(18, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(18, 4).Value);
                ADC_Cor_sheet.Cell(18, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(18, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(19, 3).Value = Convert.ToDouble(H2D.Range[14, 11].FormulaValue);
                ADC_Cor_sheet.Cell(19, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(19, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(19, 4).Value);
                ADC_Cor_sheet.Cell(19, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(19, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 5;//offset@5V

                //CH5 
                ADC_Cor_sheet.Cell(20, 3).Value = Convert.ToDouble(H2D.Range[15, 11].FormulaValue);
                ADC_Cor_sheet.Cell(20, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(20, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(20, 4).Value);
                ADC_Cor_sheet.Cell(20, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(20, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 2;//offset@2V

                ADC_Cor_sheet.Cell(21, 3).Value = Convert.ToDouble(H2D.Range[16, 11].FormulaValue);
                ADC_Cor_sheet.Cell(21, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(21, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(21, 4).Value);
                ADC_Cor_sheet.Cell(21, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(21, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(22, 3).Value = Convert.ToDouble(H2D.Range[17, 11].FormulaValue);
                ADC_Cor_sheet.Cell(22, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(22, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(22, 4).Value);
                ADC_Cor_sheet.Cell(22, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(22, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 5;//offset@5V

                //CH6 
                ADC_Cor_sheet.Cell(23, 3).Value = Convert.ToDouble(H2D.Range[18, 11].FormulaValue);
                ADC_Cor_sheet.Cell(23, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(23, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(23, 4).Value);
                ADC_Cor_sheet.Cell(23, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(23, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 2;//offset@2V

                ADC_Cor_sheet.Cell(24, 3).Value = Convert.ToDouble(H2D.Range[19, 11].FormulaValue);
                ADC_Cor_sheet.Cell(24, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(24, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(24, 4).Value);
                ADC_Cor_sheet.Cell(24, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(24, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 3.3;//offset@3.3V

                ADC_Cor_sheet.Cell(25, 3).Value = Convert.ToDouble(H2D.Range[20, 11].FormulaValue);
                ADC_Cor_sheet.Cell(25, 5).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(25, 3).Value) - Convert.ToDouble(ADC_Cor_sheet.Cell(25, 4).Value);
                ADC_Cor_sheet.Cell(25, 6).Value = Convert.ToDouble(ADC_Cor_sheet.Cell(25, 5).Value) + Convert.ToDouble(ADC_Cor_sheet.Cell(5, 5).Value) / 1.225 * 5;//offset@5V
            }



            //AE原始测试数据备份
            IXLWorksheet AE_sheet = ADC_Cor.Worksheet(2);
            for (j=1;j<=3;j++)
            {
                if(j==1 || j==3)
               {
                for (i=1;i<=6000;i++)
                    AE_sheet.Cell(i, j).Value = test_sheet.Cell(i, j).Value;
                }
                
                if (j==2)
               {
                    for (i = 1; i <6000; i++)
                    {

                        AE_sheet.Cell(i, j).Style.NumberFormat.Format = "@";//单元格格式设置为文本
                        AE_sheet.Cell(i, j).Value = test_sheet.Cell(i, j).Value;
                    }
                }

            }
            ADC_Cor.Save();

        }

        private void button9_Click(object sender, EventArgs e) //保存并重启
        {
            ADC_Cor.Save();
            System.Diagnostics.Process.Start(Application.ExecutablePath);
            System.Diagnostics.Process.GetCurrentProcess().Kill();


        }

        private void button3_Click_1(object sender, EventArgs e)//保存并关闭
        {
            ADC_Cor.Save();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (comboBox3.Text == "1#")
                ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_1#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
            if (comboBox3.Text == "2#")
                ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_2#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
            if (comboBox3.Text == "3#")
                ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_3#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
            if (comboBox3.Text == "4#")
                ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_4#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
            if (comboBox3.Text == "5#")
                ADC_Cor = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\ADC Cor_5#.xlsx");//实例化的时候参数填上Excel地址，可以读取excel表格
            IXLWorksheet ADC_Cor_sheet = ADC_Cor.Worksheet(1);

            test = new XLWorkbook(@"E:\Correlation_excel_app\Correlation_excel_app\Correlation_excel_app\bin\Debug\test.xlsx");
            IXLWorksheet test_sheet = test.Worksheet(1);


            //先把输入到GUI的BG数据写入excel
            if (comboBox1.Text == "SY68940")
            {
                ADC_Cor_sheet.Cell(5, 3).Value = textBox17.Text;
                ADC_Cor_sheet.Cell(6, 3).Value = textBox13.Text;
                ADC_Cor_sheet.Cell(7, 3).Value = textBox11.Text;
            }
            if (comboBox1.Text == "SY68930")
            {
                ADC_Cor_sheet.Cell(5, 3).Value = textBox17.Text;
                ADC_Cor_sheet.Cell(6, 3).Value = textBox13.Text;
            }
            if (comboBox1.Text == "SY68920")
            {
                ADC_Cor_sheet.Cell(5, 3).Value = textBox17.Text;
            }

            ADC_Cor.Save();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
